using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BinusOJD.Models;
using BinusOJD.Services;
using System.IO;

namespace BinusOJD.Controllers
{
    public class UserManagementController : CustomController
    {
        private readonly IUserService UserService;

        public UserManagementController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        public List<SelectListItem> DropdownRole()
        {
            List<SelectListItem> listRole = new List<SelectListItem>();            
            listRole.Add(new SelectListItem() { Text = "Admin", Value = "1" });
            listRole.Add(new SelectListItem() { Text = "User", Value = "2" });

            return listRole;
        }

        // GET: UserManagement
        public ActionResult Index()
        {
            ViewBag.listRole = DropdownRole();
            return View();
        }

        [HttpPost]
        public ActionResult GetAllUser()
        {
            List<User> listUser = UserService.GetAllUser();
            listUser = listUser.Where(model => model.IDUser != Convert.ToInt32(Session["IDUser"])).ToList();
            
            return Json(listUser);
        }

        [HttpPost]
        public ActionResult SearchUser(string Search)
        {
            List<User> listUser = UserService.SearchUser(Search);
            listUser = listUser.Where(model => model.IDUser != Convert.ToInt32(Session["IDUser"])).ToList();

            return Json(listUser);
        }       
        
        public ActionResult EditUser(int IDUser)
        {
            User user = UserService.GetUserByID(IDUser);
            ViewBag.listRole = DropdownRole();

            ViewBag.OpenModal = true;
            ViewBag.Type = "Update";

            return View("Index", user);
        }

        public ActionResult NewUser()
        {
            User user = new User();
            ViewBag.listRole = DropdownRole();

            ViewBag.OpenModal = true;
            ViewBag.Type = "Insert";

            return View("Index", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertUpdateUser(User user, HttpPostedFileBase filePhoto)
        {
            #region Validation            
            ViewBag.OpenModal = true;

            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password) || user.Role == 0)
            {
                ModelState.AddModelError("Username", "Please fill username, password, and role");
            }
            
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                bool upper = false;
                bool lower = false;
                bool symbol = false;
                bool digit = false;
                for(int i=0; i<user.Password.Length; i++)
                {
                    if(user.Password[i] >= 'A' && user.Password[i] <= 'Z')
                    {
                        upper = true;
                    }
                    else if (user.Password[i] >= 'a' && user.Password[i] <= 'z')
                    {
                        lower = true;
                    }
                    else if (user.Password[i] >= '0' && user.Password[i] <= '9')
                    {
                        digit = true;
                    }
                    else if(user.Password[i] != ' ')
                    {
                        symbol = true;
                    }
                }

                if(!upper || !lower || !digit || !symbol)
                {
                    ModelState.AddModelError("Username", "Password should contain uppercase, lowercase, digit, and symbol");
                }
            }

            if (!string.IsNullOrWhiteSpace(user.Username) && user.IDUser == 0)
            {
                List<User> listUser = UserService.GetAllUser();
                listUser = listUser.Where(model => model.Username == user.Username).ToList();

                if (listUser.Count >= 1)
                {
                    ModelState.AddModelError("Username", "Username is already taken");
                }
            }
           
            #endregion          

            if (ModelState.IsValid)
            {
                // SAVE PHOTO
                if (filePhoto != null)
                {
                    // DELETE PREVIOUS PHOTO
                    string filePath = Server.MapPath("~/Image/" + user.Username + ".jpg");
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    // SAVE CURRENT PHOTO
                    string fileName = user.Username + ".jpg";

                    var path = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    filePhoto.SaveAs(path);                                                            

                    // UPDATE PHOTO PATH
                    user.PhotoPath = fileName;                                        
                }

                ViewBag.OpenModal = false;
                bool Result = UserService.InsertUpdateUser(user);                
                if (Result)
                {                    
                    ViewBag.Success = true;
                }
                else
                {
                    ViewBag.Success = false;
                }
            }                        
            
            
            if(user.IDUser != 0)
            {
                // UPDATE
                ViewBag.Type = "Update";
            }
            else
            {
                // INSERT
                ViewBag.Type = "Insert";
            }

            ViewBag.listRole = DropdownRole();            
            return View("Index", user);
        }

        [HttpPost]
        public ActionResult GetUserByID(int IDUser)
        {
            User user = UserService.GetUserByID(IDUser);

            return Json(user);
        }
    }
}