using Business_Layer;
using DataAccess_Layer;
using Microsoft.AspNetCore.Mvc;
using Minesweeper_ArinPatrick.Models;
using System.Collections.Generic;
using System.Linq;


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

            if  (bs.GetUserByUserPass(user))
            {
                return View("Minesweeper", user);
            }
            else
            {
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
