using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BinusOJD.Models;
using BinusOJD.Services;
using BinusOJD.ViewModels;

namespace BinusOJD.Controllers
{
    public class BackLogController : CustomController
    {
        private readonly IProjectService ProjectService;
        private readonly IUserService UserService;        

        public BackLogController(IProjectService ProjectService, IUserService UserService)
        {
            this.ProjectService = ProjectService;
            this.UserService = UserService;
        }        

        // GET: BackLog
        public ActionResult Index(int IDProject)
        {
            Project project = ProjectService.GetProjectByID(IDProject);            

            ViewBag.ProjectName = project.ProjectName;
            ViewBag.IDProject = IDProject;

            List<Sprint> listSprint = ProjectService.GetSprintByIDProject(IDProject);
            ViewBag.PastSprint = listSprint.Where(model => model.Section == 1).ToList();
            ViewBag.CurrentSprint = listSprint.Where(model => model.Section == 2).ToList();
            ViewBag.FutureSprint = listSprint.Where(model => model.Section == 3).ToList();          

            return View();
        }

        public ActionResult _DetailSprint(int IDSprint)
        {
            BackLogModels backlogModels = new BackLogModels();            

            backlogModels.Sprint = ProjectService.GetSprintByID(IDSprint);
            ViewBag.SprintName = backlogModels.Sprint.SprintName;
            ViewBag.IDSprint = IDSprint;
            ViewBag.IDProject = backlogModels.Sprint.IDProject;

            List<WorkItem> listWorkItem = ProjectService.GetWorkItemByIDSprint(IDSprint);            
            for(int i=0; i<listWorkItem.Count; i++)
            {
                listWorkItem[i].Task = ProjectService.GetTaskByIDWorkItem(listWorkItem[i].IDWorkItem);
            }
            ViewBag.listWorkItem = listWorkItem;

            // SET DROPDOWN
            ViewBag.listStateWorkItem = ProjectService.GetStateDropDown("WorkItem");
            ViewBag.listStateTask = ProjectService.GetStateDropDown("Task");
            ViewBag.listUser = UserService.GetUserByIDProject(backlogModels.Sprint.IDProject);
            
            return PartialView(backlogModels);
        }

        [HttpPost]
        public ActionResult GetWorkItemByID(int IDWorkItem)
        {
            WorkItem workItem = ProjectService.GetWorkItemByID(IDWorkItem);
            return Json(workItem);
        }

        [HttpPost]
        public ActionResult GetTaskByID(int IDTask)
        {
            Task task = ProjectService.GetTaskByID(IDTask);
            return Json(task);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult InsertUpdateWorkItem(BackLogModels backLogModels)
        {
            WorkItem workItem = backLogModels.WorkItem;
            string message = "";
            string type = "";

            if (string.IsNullOrWhiteSpace(workItem.Title))
            {
                message = "empty";   
            }
            else if(workItem.IDState == 0)
            {
                message = "unselected";
            }

            if(message == "")
            {
                type = (workItem.IDWorkItem == 0) ? "insert" : "update";

                workItem.IDSprint = backLogModels.Sprint.IDSprint;

                bool Result = ProjectService.InsertUpdateWorkItem(workItem);
                message = (Result) ? "true" : "false";
            }

            return Json(new { status = message, type = type });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult InsertUpdateTask(BackLogModels backLogModels)
        {
            string message = "";
            string type = "";      
            Task task = backLogModels.Task;

            if (string.IsNullOrWhiteSpace(task.Title))
            {
                message = "empty";
            }
            else if (task.IDState == 0)
            {
                message = "unselected";
            }

            if(message == "")
            {
                type = (task.IDTask == 0) ? "insert" : "update";

                bool Result = ProjectService.InsertUpdateTask(task);

                message = (Result) ? "true" : "false";
            }

            return Json(new { status = message, type = type });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateSprint(BackLogModels backLogModels)
        {
            string message = "";
            string type = "update";
            Sprint sprint = backLogModels.Sprint;

            if (string.IsNullOrWhiteSpace(sprint.SprintName))
            {
                message = "empty";
            }
            else if (sprint.StartDate == DateTime.MinValue)
            {
                message = "startdate";
            }
            else if (sprint.EndDate == DateTime.MinValue)
            {
                message = "enddate";
            }
            else if (sprint.StartDate > sprint.EndDate)
            {
                message = "larger";
            }

            if(message == "")
            {
                bool Result = ProjectService.UpdateSprint(sprint);
                message = Result ? "true" : "false";                
            }

            return Json(new { status = message, type = type });
        }

        [HttpPost]
        public ActionResult DeleteWorkItem(int IDWorkItem)
        {
            string message = "";

            bool Result = ProjectService.DeleteWorkItem(IDWorkItem);
            message = Result ? "true" : "false";

            return Json(new { status = message });
        }

        [HttpPost]
        public ActionResult DeleteTask(int IDTask)
        {
            string message = "";

            bool Result = ProjectService.DeleteTask(IDTask);
            message = Result ? "true" : "false";

            return Json(new { status = message });
        }
    }
}