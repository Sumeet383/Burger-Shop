using BurgerShop.BusinessLayer;
using BurgerShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BurgerShop.Controllers
{
    public class AdminController : Controller
    {

        private readonly IBurgerShopService _burgerShopService;
        private readonly IBurgerRepository _burgerRepository;

        public AdminController(IBurgerShopService burgerShopService, IBurgerRepository burgerRepository)
        {
            _burgerShopService = burgerShopService;
            _burgerRepository = burgerRepository;
        }
        // GET
        public IActionResult AdminLogin()
        {
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult AdminPost(string? MobileNo,string? mobileNo2)
        {
            if (MobileNo == "0987654321")
            {
                _burgerShopService.UpdateUserByMobileNo(mobileNo2);
                TempData["success"] = "User is registered as Admin Sucessfully";
                return RedirectToAction("Login", "BurgerShop");
            }
            else
            {
                TempData["error"] = $"It isn't the Authenticated Mobile Number. {MobileNo}";
                return RedirectToAction("AdminLogin", "Admin");
            }

        }

        // GET
        public IActionResult MenuItem(int userId)
        {
            ViewBag.UserId = userId; // Store the id in ViewBag to access it in the view
            return View();
        }
        [HttpPost]
        public IActionResult CreateMenuItem(Menu menu, int userId) {
            if (ModelState.IsValid)
            {
                _burgerRepository.CreateBurger(menu);
                TempData["success"] = "Menu item created successfully!";
                return RedirectToAction("BurgerPage", "BurgerShop", new { id = userId });
            }
            else
            {
                TempData["error"] = "Failed to add Menu item!";
                return View(menu); // This will return the CreateMenuItem view
            }
        }

        public IActionResult DeleteBurger(int id, int userId)
        {
            // Validate the ID (you might want to convert it to an int if that's your ID type)
            if (id == null)
            {
                TempData["error"] = "Invalid burger ID.";
                return RedirectToAction("BurgerPage", "BurgerShop",new { id = userId}); // Redirect to the menu item page
            }

            // Call the repository to delete the burger
            try
            {
                _burgerRepository.DeleteBurger(id);
                TempData["success"] = "Burger deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Error deleting burger: {ex.Message}";
            }

            return RedirectToAction("BurgerPage", "BurgerShop", new { id = userId }); // Redirect to the menu item page
        }


        // GET
        public IActionResult EditBurger(int burgerId,int userId)
        {
            ViewBag.UserId = userId; // Store the id in ViewBag to access it in the view
            var menu = _burgerRepository.GetBurgerById(burgerId);

            if (menu == null)
            {
                TempData["error"] = "Burger not found.";
                return RedirectToAction("BurgerPage", "BurgerShop", new { id = userId });
            }
            return View(menu);
        }

        [HttpPost]
        public IActionResult EditMenuItem(Menu menu, int userId)
        {
            if (ModelState.IsValid)
            {
                // Ensure the burger ID is set correctly
                var existingBurger = _burgerRepository.GetBurgerById(menu.Id);
                if (existingBurger != null)
                {
                    existingBurger.BurgerName = menu.BurgerName;
                    existingBurger.Description = menu.Description;
                    existingBurger.BasePrice = menu.BasePrice;
                    // You can also update other properties if needed

                    _burgerRepository.UpdateBurger(existingBurger);
                    TempData["success"] = "Menu item updated successfully!";
                    return RedirectToAction("BurgerPage", "BurgerShop", new { id = userId });
                }
                else
                {
                    TempData["error"] = "Burger not found.";
                }
            }
            else
            {
                TempData["error"] = "Failed to update menu item!";
            }

            return View(menu); // Return the same view with the model to show validation errors
        }

    }
}
