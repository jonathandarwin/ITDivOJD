using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BinusOJD.Models;
using BinusOJD.Services;
using System.Threading.Tasks;

namespace BinusOJD.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService UserService;
        public LoginController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        // GET: Login
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(User user)
        {
            if(string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                ModelState.AddModelError("Username", " Please input your username/password");
            }
            else
            {
                user = await UserService.DoLogin(user);

                if(user == null)
                {
                    ModelState.AddModelError("Username", " Error occur while login. Please try again.");
                }
                else if (user.Username == null || user.Username == "")
                {
                    ModelState.AddModelError("Username", " Incorrect Username/Password");
                }
                else if (user.Status == false)
                {
                    ModelState.AddModelError("Username", "User is not active");
                }
                else
                {
                    Session.Add("IDUser", user.IDUser);
                    Session.Add("Username", user.Username);
                    Session.Add("Role", user.Role);
                    Session.Add("PhotoPath", user.PhotoPath);

                    SESSION.IDUser = user.IDUser;
                    SESSION.Username = user.Username;
                    SESSION.Role = user.Role;
                    SESSION.PhotoPath = user.PhotoPath;

                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Index", user);
        }

        public async Task<ActionResult> DoLogout()
        {
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();

            SESSION.IDUser = 0;
            SESSION.Username = null;
            SESSION.Role = 0;
            SESSION.PhotoPath = null;

            return RedirectToAction("Index", "Login");
        }
    }
}