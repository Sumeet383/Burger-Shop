using BurgerShop.Models;
using System;

namespace BurgerShop.ViewModels
{
    public class BurgerPageViewModel
    {
        public User User { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
