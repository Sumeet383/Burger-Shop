using System;
using BurgerShop.Models;

namespace BurgerShop.BusinessLayer
{
    public interface IBurgerRepository
    {
        List<Menu> GetAllBurgers();
        Menu GetBurgerById(int id);
        void CreateBurger(Menu burger);
        void UpdateBurger(Menu burger);
        void DeleteBurger(int id);
    }
}
