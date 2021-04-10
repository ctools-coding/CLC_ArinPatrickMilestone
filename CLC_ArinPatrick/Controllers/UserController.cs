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
        //instantiate Logger
        //private static Logger logger = LogManager.GetLogger("MinesweeperRule");
       
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
            MyLogger.GetInstance().info("Processing a login attempt");
            MyLogger.GetInstance().info(user.toString());

            CommWDataAccess bs = new CommWDataAccess();
            
            if(bs.GetUserByUserPass(user))
            {
                MyLogger.GetInstance().info("Login Success");
                HttpContext.Session.SetString("username", user.Username);
                ViewBag.session = HttpContext.Session.GetString("username");
                return View("LoginSuccess", user);
            }
            else
            {
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
            if(ModelState.IsValid)
            {
                 userDAL.AddUser(objUser);
                 return RedirectToAction("Index");
            }

            return View(objUser);
        }
    }
}
