using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        private Product[] products =
        {
            new Product {Name = "Kayak", Category = "WaterSports", Price = 10},
            new Product {Name = "Checkers", Category = "MindGames", Price = 20}
        };

        // GET: Home
        public ActionResult Index()
        {
            //IValueCalculator calc = new LinqValueCalculator();
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();

            ShoppingCart cart = new ShoppingCart(ninjectKernel.Get<IValueCalculator>()) {Products = products};
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }
    }
}