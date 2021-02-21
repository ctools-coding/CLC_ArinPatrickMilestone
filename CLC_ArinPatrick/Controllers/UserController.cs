using Business_Layer;
using DataAccess_Layer;
using Microsoft.AspNetCore.Mvc;
using Minesweeper_ArinPatrick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper_ArinPatrick.Controllers
{
    public class UserController : Controller
    {
        UserData userDAL = new UserData();

        public IActionResult Index()
        {
            List<UserModel> userList = new List<UserModel>();

            userList = userDAL.GetAllUsers().ToList();

            return View(userList);
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult ProcessLogin(UserModel user)
        {
            CommWDataAccess bs = new CommWDataAccess();

            if  (bs.GetUserByUserPass(user))
            {
                return View("LoginSuccess", user);
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


        public IActionResult Create([Bind]UserModel objUser)
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
