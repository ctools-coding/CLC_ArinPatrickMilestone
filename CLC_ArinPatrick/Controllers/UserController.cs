using Microsoft.AspNetCore.Mvc;
using Minesweeper_ArinPatrick.Models;
using Minesweeper_ArinPatrick.Services.Business;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Minesweeper_ArinPatrick.Controllers
{

    public class UserController : Controller
    {
       
        UserData userDAL = new UserData();

        public IActionResult Index()
        {
            List<Models.UserModel> userList = new List<Models.UserModel>();

            userList = userDAL.GetAllUsers().ToList();

            return View(userList);
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult ProcessLogin(Models.UserModel user)
        {
            CommWDataAccess bs = new CommWDataAccess();
            
            if(bs.GetUserByUserPass(user))
            {
                HttpContext.Session.SetString("username", user.Username);
                ViewBag.session = HttpContext.Session.GetString("username");
                return View("LoginSuccess", user);
            }
            else
            {
                HttpContext.Session.Remove("username");
                return View("LoginFailure", user);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create([Bind] Models.UserModel objUser)
        {
            if(ModelState.IsValid)
            {
                 userDAL.AddUser(objUser);
                 return RedirectToAction("Index");
            }

            return View(objUser);
        }
    }
}
