using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BinusOJD.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Mvc;

namespace BinusOJD.Services
{
    public interface IProjectService
    {
        List<Project> GetAllAuthProject(int IDUser);
        List<Project> SearchProject(string Project, int IDUser);
        bool InsertUpdateProject(Project project, int IDUser);
        bool InsertUpdateAuthProject(int IDUser, int IDProject, bool isAuth);
        Project GetProjectByID(int IDProject);

        Sprint GetSprintByID(int IDSprint);
        List<Sprint> GetSprintByIDProject(int IDProject);
        bool UpdateSprint(Sprint sprint);

        WorkItem GetWorkItemByID(int IDWorkItem);
        List<WorkItem> GetWorkItemByIDSprint(int IDSprint);
        bool InsertUpdateWorkItem(WorkItem workItem);
        bool DeleteWorkItem(int IDWorkItem);

        Task GetTaskByID(int IDTask);
        List<Task> GetTaskByIDWorkItem(int IDWorkItem);
        bool InsertUpdateTask(Task task);
        bool DeleteTask(int IDTask);

        List<SelectListItem> GetStateDropDown(string type);
    }
    public class ProjectService : IProjectService
    {
        public List<Project> GetAllAuthProject(int IDUser)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_GetAllAuthProject";
            List<Project> listProject = new List<Project>();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDUser", SqlDbType.Int).Value = IDUser;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
                if (ProjectDR.HasRows)
                {
                    while (ProjectDR.Read())
                    {
                        Project project = new Project();
                        project.IDProject = Convert.ToInt32(ProjectDR["IDProject"]);
                        project.ProjectName = ProjectDR["ProjectName"].ToString();
                        project.Description = ProjectDR["Description"].ToString();

                        listProject.Add(project);
                    }
                }
            }
            catch (Exception ex)
            {
                listProject = null;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return listProject;
        }

        public List<Project> SearchProject(string Project, int IDUser)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_SearchProject";
            List<Project> listProject = new List<Project>();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ProjectName", SqlDbType.VarChar, 50).Value = Project;
            sqlCommand.Parameters.Add("@IDUser", SqlDbType.Int).Value = IDUser;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
                if (ProjectDR.HasRows)
                {
                    while (ProjectDR.Read())
                    {
                        Project project = new Project();
                        project.IDProject = Convert.ToInt32(ProjectDR["IDProject"]);
                        project.ProjectName = ProjectDR["ProjectName"].ToString();
                        project.Description = ProjectDR["Description"].ToString();

                        listProject.Add(project);
                    }
                }
            }
            catch (Exception ex)
            {
                listProject = null;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return listProject;
        }

        public bool InsertUpdateProject(Project project, int IDUser)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_InsertUpdateProject";                       

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDProject", SqlDbType.Int).Value = project.IDProject;
            sqlCommand.Parameters.Add("@ProjectName", SqlDbType.VarChar, 50).Value = project.ProjectName;
            sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar, int.MaxValue).Value = project.Description;
            sqlCommand.Parameters.Add("@IDUser", SqlDbType.Int).Value = IDUser;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();                
            }
            catch (Exception ex)
            {
                ProjectDR.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();

                return false;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return true;
        }

        public bool InsertUpdateAuthProject(int IDUser, int IDProject, bool isAuth)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_InsertUpdateAuthProject";            

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDUser", SqlDbType.Int).Value = IDUser;
            sqlCommand.Parameters.Add("@IDProject", SqlDbType.Int).Value = IDProject;
            sqlCommand.Parameters.Add("@isAuth", SqlDbType.Bit).Value = isAuth;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                ProjectDR.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();

                return false;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return true;
        }

        public Project GetProjectByID(int IDProject)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_GetProjectByID";
            Project project = new Project();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDProject", SqlDbType.Int).Value = IDProject;            
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
                if (ProjectDR.HasRows)
                {
                    while (ProjectDR.Read())
                    {
                        project.IDProject = IDProject;
                        project.ProjectName = ProjectDR["ProjectName"].ToString();                        
                    }
                }
            }
            catch (Exception ex)
            {
                project = null;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return project;
        }

        public Sprint GetSprintByID(int IDSprint)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_GetSprintByID";
            Sprint sprint = new Sprint();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDSprint", SqlDbType.Int).Value = IDSprint;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
                if (ProjectDR.HasRows)
                {
                    while (ProjectDR.Read())
                    {
                        sprint.IDSprint = IDSprint;
                        sprint.IDProject = Convert.ToInt32(ProjectDR["IDProject"]);
                        sprint.SprintName = ProjectDR["SprintName"].ToString();
                        if (ProjectDR["StartDate"] == DBNull.Value)
                        {
                            sprint.StartDate = DateTime.MinValue;
                        }
                        else
                        {
                            sprint.StartDate = Convert.ToDateTime(ProjectDR["StartDate"]);
                        }

                        if (ProjectDR["EndDate"] == DBNull.Value)
                        {
                            sprint.EndDate = DateTime.MinValue;
                        }
                        else
                        {
                            sprint.EndDate = Convert.ToDateTime(ProjectDR["EndDate"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sprint = null;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return sprint;
        }

        public WorkItem GetWorkItemByID(int IDWorkItem)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_GetWorkItemByID";
            WorkItem workItem = new WorkItem();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDWorkItem", SqlDbType.Int).Value = IDWorkItem;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
                if (ProjectDR.HasRows)
                {
                    while (ProjectDR.Read())
                    {
                        workItem.IDWorkItem = IDWorkItem;
                        workItem.Title = ProjectDR["Title"].ToString();
                        workItem.IDState = Convert.ToInt32(ProjectDR["IDState"]);
                        workItem.StateName = ProjectDR["StateName"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                workItem = null;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return workItem;
        }

        public Task GetTaskByID(int IDTask)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_GetTaskByID";
            Task task = new Task();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDTask", SqlDbType.Int).Value = IDTask;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
                if (ProjectDR.HasRows)
                {
                    while (ProjectDR.Read())
                    {
                        task.IDTask = IDTask;
                        task.Title = ProjectDR["Title"].ToString();
                        task.IDState = Convert.ToInt32(ProjectDR["IDState"]);
                        if(ProjectDR["AssignTo"] != DBNull.Value)
                        {
                            task.AssignTo = Convert.ToInt32(ProjectDR["AssignTo"]);
                        }                        
                        task.UsernameAssignTo = ProjectDR["Username"].ToString();
                        task.StateName = ProjectDR["StateName"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                task = null;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return task;
        }

        public List<Sprint> GetSprintByIDProject(int IDProject)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_GetSprintByIDProject";
            List<Sprint> listSprint = new List<Sprint>();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDProject", SqlDbType.Int).Value = IDProject;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
                if (ProjectDR.HasRows)
                {
                    while (ProjectDR.Read())
                    {
                        Sprint sprint = new Sprint();
                        sprint.IDSprint = Convert.ToInt32(ProjectDR["IDSprint"]);
                        sprint.IDProject = Convert.ToInt32(ProjectDR["IDProject"]);
                        sprint.SprintName = ProjectDR["SprintName"].ToString();
                        if(ProjectDR["StartDate"] == DBNull.Value)
                        {
                            sprint.StartDate = DateTime.MinValue;
                        }
                        else
                        {
                            sprint.StartDate = Convert.ToDateTime(ProjectDR["StartDate"]);                            
                        }

                        if (ProjectDR["EndDate"] == DBNull.Value)
                        {
                            sprint.EndDate = DateTime.MinValue;
                        }
                        else
                        {
                            sprint.EndDate = Convert.ToDateTime(ProjectDR["EndDate"]);
                        }

                        if ((sprint.StartDate == DateTime.MinValue && sprint.EndDate == DateTime.MinValue) ||
                            (sprint.StartDate > DateTime.Today && sprint.EndDate > DateTime.Today))
                        {
                            // FUTURE - KALO DIA START DAN END KOSONG MASUK FUTURE
                            sprint.Section = 3;
                        }
                        else if(DateTime.Today >= sprint.StartDate && DateTime.Today <= sprint.EndDate)
                        {
                            // CURRENT
                            sprint.Section = 2;
                        }
                        else
                        {
                            // PAST
                            sprint.Section = 1;
                        }

                        listSprint.Add(sprint);
                    }
                }
            }
            catch (Exception ex)
            {
                listSprint = null;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return listSprint;
        }

        public List<WorkItem> GetWorkItemByIDSprint(int IDSprint)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_GetWorkItemByIDSprint";
            List<WorkItem> listWorkItem = new List<WorkItem>();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDSprint", SqlDbType.Int).Value = IDSprint;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
                if (ProjectDR.HasRows)
                {
                    while (ProjectDR.Read())
                    {
                        WorkItem workItem = new WorkItem();
                        workItem.IDSprint = IDSprint;
                        workItem.IDWorkItem = Convert.ToInt32(ProjectDR["IDWorkItem"]);
                        workItem.IDState = Convert.ToInt32(ProjectDR["IDState"]);
                        workItem.StateName = ProjectDR["StateName"].ToString();
                        workItem.Title = ProjectDR["Title"].ToString();

                        listWorkItem.Add(workItem);
                    }
                }
            }
            catch (Exception ex)
            {
                listWorkItem = null;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return listWorkItem;
        }

        public List<Task> GetTaskByIDWorkItem(int IDWorkItem)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_GetTaskByIDWorkItem";
            List<Task> listTask = new List<Task>();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDWorkItem", SqlDbType.Int).Value = IDWorkItem;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
                if (ProjectDR.HasRows)
                {
                    while (ProjectDR.Read())
                    {
                        Task task = new Task();
                        task.IDWorkItem= IDWorkItem;
                        task.IDTask = Convert.ToInt32(ProjectDR["IDTask"]);
                        task.IDState = Convert.ToInt32(ProjectDR["IDState"]);
                        task.AssignTo = ProjectDR["AssignTo"] == DBNull.Value ? 0 : Convert.ToInt32(ProjectDR["AssignTo"]);
                        task.UsernameAssignTo = ProjectDR["Username"].ToString();
                        task.StateName = ProjectDR["StateName"].ToString();
                        task.Title = ProjectDR["Title"].ToString();

                        listTask.Add(task);
                    }
                }
            }
            catch (Exception ex)
            {
                listTask = null;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return listTask;
        }

        public List<SelectListItem> GetStateDropDown(string type)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_GetStateDropdown";
            List<SelectListItem> listDropdown = new List<SelectListItem>();
            listDropdown.Add(new SelectListItem() { Text = "Select State", Value = "0" });

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@Type", SqlDbType.VarChar, 20).Value = type;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
                if (ProjectDR.HasRows)
                {
                    while (ProjectDR.Read())
                    {
                        listDropdown.Add(new SelectListItem() { Text = ProjectDR["StateName"].ToString(), Value = ProjectDR["IDState"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                listDropdown = null;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return listDropdown;
        }

        public bool InsertUpdateWorkItem(WorkItem workItem)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_InsertUpdateWorkItem";            

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDWorkItem", SqlDbType.Int).Value = workItem.IDWorkItem;
            sqlCommand.Parameters.Add("@IDSprint", SqlDbType.Int).Value = workItem.IDSprint;
            sqlCommand.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value = workItem.Title;
            sqlCommand.Parameters.Add("@IDState", SqlDbType.Int).Value = workItem.IDState;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();                
            }
            catch (Exception ex)
            {
                ProjectDR.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();

                return false;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return true;
        }

        public bool InsertUpdateTask(Task task)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_InsertUpdateTask";

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDWorkItem", SqlDbType.Int).Value = task.IDWorkItem;
            sqlCommand.Parameters.Add("@IDTask", SqlDbType.Int).Value = task.IDTask;
            sqlCommand.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value = task.Title;
            if(task.AssignTo != 0)
            {
                sqlCommand.Parameters.Add("@AssignTo", SqlDbType.Int).Value = task.AssignTo;
            }            
            sqlCommand.Parameters.Add("@IDState", SqlDbType.Int).Value = task.IDState;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                ProjectDR.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();

                return false;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return true;
        }

        public bool DeleteWorkItem(int IDWorkItem)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_DeleteWorkItem";
            bool result = false;

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDWorkItem", SqlDbType.Int).Value = IDWorkItem; 
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
                if (ProjectDR.HasRows)
                {
                    while (ProjectDR.Read())
                    {
                        if(ProjectDR["Result"].ToString() == "true")
                        {
                            result = true;
                        }
                        else if(ProjectDR["Result"].ToString() == "false")
                        {
                            result = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProjectDR.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();

                return false;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return result;
        }

        public bool DeleteTask(int IDTask)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_DeleteTask";            

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDTask", SqlDbType.Int).Value = IDTask;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();                
            }
            catch (Exception ex)
            {
                ProjectDR.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();

                return false;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return true;
        }


        public bool UpdateSprint(Sprint sprint)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "Project_UpdateSprint";

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader ProjectDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDSprint", SqlDbType.Int).Value = sprint.IDSprint;
            sqlCommand.Parameters.Add("@SprintName", SqlDbType.VarChar, 50).Value = sprint.SprintName;
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = sprint.StartDate;
            sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = sprint.EndDate;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                ProjectDR = sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                ProjectDR.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();

                return false;
            }

            ProjectDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return true;
        }
    }
}