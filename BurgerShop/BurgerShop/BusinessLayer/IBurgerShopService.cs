using BurgerShop.Models;

namespace BurgerShop.BusinessLayer
{
    public interface IBurgerShopService
    {
        List<User> GetAllUsers();
        User GetUserByMobileNo(string mobileNo);
        bool VerifyPassword(string plainPassword, string hashedPassword);
        string GenerateJwtToken(User user, out DateTime expiresAt);
        void AddUser(User user);

        void UpdateUserByMobileNo(string mobileNo);
        User GetUserById(int id);
        List<Order> GetOrdersByUserId(int userId);
        void PlaceOrder(List<Order> orders);
    }
}
