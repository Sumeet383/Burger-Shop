using BurgerShop.Data;
using Microsoft.AspNetCore.Mvc;
using BurgerShop.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using Microsoft.CodeAnalysis.Scripting;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using static NuGet.Packaging.PackagingConstants;
using BurgerShop.BusinessLayer;
using BurgerShop.BusinessLayer;
using BurgerShop.ViewModels;

namespace BurgerShop.Controllers
{
    public class BurgerShopController : Controller
    {
        private readonly IBurgerShopService _burgerShopService;
        private readonly IBurgerRepository _burgerRepository;

        public BurgerShopController(IBurgerShopService burgerShopService,IBurgerRepository burgerRepository)
        {
            _burgerShopService = burgerShopService;
            _burgerRepository = burgerRepository;
        }

        // Test Route
        public IActionResult Index()
        {
            List<User> objUserList = _burgerShopService.GetAllUsers(); // You might want to implement this method in the service
            return View(objUserList);
        }

        // GET
        public IActionResult Login()
        {
            //List<User> objUserList = _db.Users.ToList();
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult LoginPost(string? MobileNo, string? Password)
        {
            if (MobileNo == null || Password == null)
            {
                TempData["error"] = "Mobile number or password cannot be empty.";
                return RedirectToAction("Login");
            }

            User? user = _burgerShopService.GetUserByMobileNo(MobileNo);
            bool passwordVerify = user != null && _burgerShopService.VerifyPassword(Password, user.Password);
            // ...
            

            if (user == null || !passwordVerify)
            {
                TempData["error"] = "Invalid mobile number or password.";
                return RedirectToAction("Login");
            }

            // Generate JWT token for the user
            //var token = GenerateJwtToken(user, out DateTime expiresAt);
            var token = _burgerShopService.GenerateJwtToken(user, out DateTime expiresAt);
            // Set the JWT token as an HttpOnly cookie
            HttpContext.Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,// Ensure the cookie is accessible only by the server
                Secure = true, // Set to true if your site is served over Https
                SameSite = SameSiteMode.Strict,// Prevents CSRF
                Expires = expiresAt // Set the token's expiration time
                
            });
            // Redirect to BurgerPage with the JWT token
            return RedirectToAction("BurgerPage", "BurgerShop", new { id = user.Id });
            //return RedirectToAction("BurgerPage", "BurgerShop", new { id = user.Id });
        }
        // GET
        public IActionResult BurgerPage(int id)
        {


            // Retrieve the user based on the ID

            User? user = _burgerShopService.GetUserById(id);
            List<Menu>? Menu  = _burgerRepository.GetAllBurgers();

            var viewModel = new BurgerPageViewModel
            {
                User = user, // Method to get user data
                Menus =  Menu // Method to get burger list
            };
            if (user == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
        // GET
        public IActionResult Cart()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public IActionResult PlaceOrder([FromBody] List<Order> orders)
        {
            if (orders == null || !orders.Any())
            {
                return BadRequest("No order data received.");
            }

            _burgerShopService.PlaceOrder(orders);
            return Ok();

        }

        public IActionResult Register() { 
            return View();
        }
        // POST
        [HttpPost]
        public IActionResult RegisterPost(User obj)
        {
            if (ModelState.IsValid)
            {
                User? user = _burgerShopService.GetUserByMobileNo(obj.MobileNo);
                if (user != null)
                {
                    TempData["error"] = "User Already Exist(Go to login page)";
                    return RedirectToAction("Register");
                }
                else
                {
                    _burgerShopService.AddUser(obj); 
                    TempData["success"] = "User Registered Successfully";
                    return RedirectToAction("Login");
                }
            }

            return View();
        }


        [Authorize]
        public IActionResult ViewOrders(int id)
        {

            User? user = _burgerShopService.GetUserById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var orders = _burgerShopService.GetOrdersByUserId(user.Id);

            return View(orders);

        }

        public IActionResult Logout()
        {
            // Clear the JWT token cookie
            HttpContext.Response.Cookies.Delete("jwtToken");

            // Redirect to the login page
            return RedirectToAction("Login","BurgerShop");
        }
    }
}
