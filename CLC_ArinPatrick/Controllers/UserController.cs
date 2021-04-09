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
        public const string SessionKeyName = "_Name";
        public string SessionInfo_Name { get; private set; }

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
                OnGet(user);
                ViewBag.session = HttpContext.Session.GetString(SessionKeyName);
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

        public IActionResult Create([Bind] Models.UserModel objUser)
        {
            if(ModelState.IsValid)
            {
                 userDAL.AddUser(objUser);
                   return RedirectToAction("Index");
            }

            return View(objUser);
        }

        public void OnGet(UserModel user)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                HttpContext.Session.SetString(SessionKeyName, user.Username);
            }
            var username = HttpContext.Session.GetInt32(SessionKeyName);
        }
    }
}
