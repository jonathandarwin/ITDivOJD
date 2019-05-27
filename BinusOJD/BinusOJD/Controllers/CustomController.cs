using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BinusOJD.Models;
using BinusOJD.Services;

namespace BinusOJD.Controllers
{
    public class CustomController : Controller
    {                
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string baseURL = Request.Url.Authority;
            string partialUrl = Request.Url.AbsolutePath;

            // CHECK SESSION
            if (Session["Username"] == null)
            {
                Session.RemoveAll();
                Session.Clear();
                Session.Abandon();

                SESSION.IDUser = 0;
                SESSION.Username = null;
                SESSION.Role = 0;
                SESSION.PhotoPath = null;

                filterContext.Result = new RedirectResult(baseURL + "/Login");                
            }
                        
            IUserService UserService = new UserService();            

            if(UserService.CheckPrivilege(partialUrl) && Convert.ToInt32(Session["Role"]) == 2)
            {
                filterContext.Result = new RedirectResult(baseURL + "/Home");
            }
            
        }

        
    }
}