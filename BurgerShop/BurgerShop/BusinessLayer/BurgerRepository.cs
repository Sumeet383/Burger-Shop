using BurgerShop.Data;
using BurgerShop.Models;
using System;
using System.Drawing;

namespace BurgerShop.BusinessLayer
{
    public class BurgerRepository : IBurgerRepository
    {
        private readonly ApplicationDbContext _db;

        public BurgerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Menu> GetAllBurgers()
        {
            return _db.Menus.ToList();
        }

        public Menu GetBurgerById(int id)
        {
            return _db.Menus.Find(id);
        }

        public void CreateBurger(Menu menu)
        {
            menu.ImageUrl = "~/images/burger_image.png";
            _db.Menus.Add(menu);
            _db.SaveChanges();
        }

        public void UpdateBurger(Menu menu)
        {
            _db.Menus.Update(menu);
            _db.SaveChanges();
        }

        public void DeleteBurger(int id)
        {
            Menu burger = _db.Menus.Find(id);
            _db.Menus.Remove(burger);
            _db.SaveChanges();
        }
    }
}
