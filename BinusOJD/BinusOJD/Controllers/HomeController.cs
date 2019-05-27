using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BinusOJD.Models;
using BinusOJD.Services;

namespace BinusOJD.Controllers
{
    public class HomeController : CustomController
    {
        private readonly IProjectService ProjectService;
        private readonly IUserService UserService;

        public HomeController(IProjectService ProjectService, IUserService UserService)
        {
            this.ProjectService = ProjectService;
            this.UserService = UserService;
        }

        // GET: Home
        public ActionResult Index()
        {
            List<Project> listProject = ProjectService.GetAllAuthProject(Convert.ToInt32(Session["IDUser"]));            
            return View();
        }

        [HttpPost]
        public ActionResult SearchProject(string Project = "", int IDUser = 0)
        {
            List<Project> listProject = ProjectService.SearchProject(Project, IDUser);
            return Json(listProject);
        }

        [HttpPost]
        public ActionResult InsertUpdateProject(Project project)
        {
            #region Validation
            string message = "";
            if (string.IsNullOrWhiteSpace(project.ProjectName))
            {
                message = "empty";
            }
            #endregion

            if (message == "")
            {
                bool Result = ProjectService.InsertUpdateProject(project, Convert.ToInt32(Session["IDUser"]));
                if (Result)
                {
                    // GIVE AUTH

                    message = "true";
                }
                else
                {
                    message = "false";
                }
            }

            return Json(new { status = message });
        }

        [HttpPost]
        public ActionResult InsertUpdateAuthProject(int IDUser, int IDProject, bool isAuth)
        {
            string message = "";

            bool Result = ProjectService.InsertUpdateAuthProject(IDUser, IDProject, isAuth);
            if (Result)
            {
                message = "true";
            }
            else
            {
                message = "false";
            }

            return Json(new { status = message });
        }

        [HttpPost]
        public ActionResult GetAllUserWithAuth(int IDProject)
        {
            List<User> listUser = UserService.GetAllUserWithAuth(IDProject);
            listUser = listUser.Where(model => model.IDUser != Convert.ToInt32(Session["IDUser"])).ToList();

            return Json(listUser);
        }
    }
}