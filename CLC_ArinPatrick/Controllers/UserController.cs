using Microsoft.AspNetCore.Mvc;
using Minesweeper_ArinPatrick.Models;
using Minesweeper_ArinPatrick.Services.Business;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using Minesweeper_ArinPatrick.Utility;

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
            //logs when soneone attempts to login
            MyLogger.GetInstance().info("Processing a login attempt");
            MyLogger.GetInstance().info(user.toString());

            CommWDataAccess bs = new CommWDataAccess();
            
            //log if the user is successful, we will log that user
            if(bs.GetUserByUserPass(user))
            {
                MyLogger.GetInstance().info("Login Success");
                HttpContext.Session.SetString("username", user.Username);
                ViewBag.session = HttpContext.Session.GetString("username");
                return View("LoginSuccess", user);
            }
            else
            {
                //if someone fails at a login, we will record what they tried to use
                MyLogger.GetInstance().info("Login Failed");
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
            //logs when soneone attempts to login
            MyLogger.GetInstance().info("User is about to create a profile");
            

            if (ModelState.IsValid)
            {
                 userDAL.AddUser(objUser);
                //logs when soneone attempts to login
                MyLogger.GetInstance().info("Processing a Register attempt");
                MyLogger.GetInstance().info("First Name: " + objUser.First);
                MyLogger.GetInstance().info("Last Name: " + objUser.Last);
                MyLogger.GetInstance().info("UserName: " + objUser.Username);
                MyLogger.GetInstance().info("Password: " + objUser.Password);
                MyLogger.GetInstance().info("State: " + objUser.State);
                MyLogger.GetInstance().info("Gender: " + objUser.Gender);
                return RedirectToAction("Index");
            }

            return View(objUser);
        }
    }
}
