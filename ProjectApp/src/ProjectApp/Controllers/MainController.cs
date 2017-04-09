using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectApp.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectApp.Controllers
{
    public class MainController : Controller
    {
        private ApplicationDbContext _myContext;

        public MainController(ApplicationDbContext context)
        {
            this._myContext = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            SeedData.Seed(_myContext);
            return View();
        }

        public IActionResult Authenticate()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "LoggedIn");
            }
            else
            {
                return RedirectToAction("Index", "Main");
            }
        }
    }
}
