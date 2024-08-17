using BurgerShop.BusinessLayer;
using BurgerShop.Data;
using BurgerShop.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BurgerShop.BusinessLayer
{
    public class BurgerShopService : IBurgerShopService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public BurgerShopService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public User? GetUserByMobileNo(string mobileNo)
        {
            return _db.Users.FirstOrDefault(u => u.MobileNo == mobileNo);
        }

        public bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            var hashedEnteredPassword = HashPasswordManual(enteredPassword);
            return hashedEnteredPassword == storedPassword;
        }

        private string HashPasswordManual(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public string GenerateJwtToken(User user, out DateTime expiresAt)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.MobilePhone, user.MobileNo),
                new Claim("UserId", user.Id.ToString())
            };
            expiresAt = DateTime.UtcNow.AddMinutes(5);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims, expires: expiresAt,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void PlaceOrder(List<Order> orders)
        {
            foreach (var order in orders)
            {
                _db.Orders.Add(order);
            }
            _db.SaveChanges();
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return _db.Orders.Where(o => o.UserId == userId).ToList();
        }

        public User? GetUserById(int id)
        {
            return _db.Users.Find(id);
        }

        public List<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }

        public void AddUser(User user)
        {
            user.Password = HashPassword(user.Password); // Hash the password before saving
            user.Role = "User";
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void UpdateUserByMobileNo(string mobileNo)
        {
            User? user = _db.Users.FirstOrDefault(u => u.MobileNo == mobileNo);
            user.Role = "Admin";
            _db.Users.Update(user);
            _db.SaveChanges();
        }
    }
}
