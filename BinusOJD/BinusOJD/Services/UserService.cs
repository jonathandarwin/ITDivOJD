using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BinusOJD.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace BinusOJD.Services
{
    public interface IUserService
    {
        Task<User> DoLogin(User user);
        List<User> GetAllUser();
        User GetUserByID(int IDUser);
        List<User> SearchUser(string Search);
        bool InsertUpdateUser(User user);
        List<User> GetAllUserWithAuth(int IDProject);
        List<SelectListItem> GetUserByIDProject(int IDProject);
        bool CheckPrivilege(string URL);
    }
    public class UserService : IUserService
    {
        public async Task<User> DoLogin(User user)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "User_DoLogin";
            User retval = new User();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader UserDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = user.Username;
            sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = user.Password;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                UserDR = sqlCommand.ExecuteReader();
                if (UserDR.HasRows)
                {
                    while (UserDR.Read())
                    {                        
                        retval.IDUser = Convert.ToInt32(UserDR["IDUser"]);
                        retval.Username = user.Username;
                        retval.Password = user.Password;
                        retval.PhotoPath = UserDR["PhotoPath"].ToString();
                        retval.Role = Convert.ToInt32(UserDR["Role"]);
                        retval.Status = Convert.ToBoolean(UserDR["Status"]);                        
                    }                                        
                }
            }
            catch (Exception ex)
            {
                user = null;
            }

            UserDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return retval;
        }

        public List<User> GetAllUser()
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "User_GetAllUser";
            List<User> listUser = new List<User>();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader UserDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                UserDR = sqlCommand.ExecuteReader();
                if (UserDR.HasRows)
                {
                    while (UserDR.Read())
                    {
                        User user = new User();
                        user.IDUser = Convert.ToInt32(UserDR["IDUser"]);
                        user.Username = UserDR["Username"].ToString();
                        user.Password = UserDR["Password"].ToString();
                        user.Role = Convert.ToInt32(UserDR["Role"]);
                        user.Status = Convert.ToBoolean(UserDR["Status"]);
                        user.PhotoPath = UserDR["PhotoPath"].ToString();

                        listUser.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                listUser = null;
            }

            UserDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return listUser;
        }

        public User GetUserByID(int IDUser)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "User_GetUserByID";
            User user = new User();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader UserDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDUser", SqlDbType.Int).Value = IDUser;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                UserDR = sqlCommand.ExecuteReader();
                if (UserDR.HasRows)
                {
                    while (UserDR.Read())
                    {
                        user.IDUser = Convert.ToInt32(UserDR["IDUser"]);
                        user.Username = UserDR["Username"].ToString();
                        user.Password = UserDR["Password"].ToString();
                        user.PhotoPath = UserDR["PhotoPath"].ToString();
                        user.Role = Convert.ToInt32(UserDR["Role"]);
                        user.Status = Convert.ToBoolean(UserDR["Status"]);
                    }
                }
            }
            catch (Exception ex)
            {
                user = null;
            }

            UserDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return user;
        }

        public List<User> SearchUser(string Search)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "User_SearchUser";
            List<User> listUser = new List<User>();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader UserDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = Search;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                UserDR = sqlCommand.ExecuteReader();
                if (UserDR.HasRows)
                {
                    while (UserDR.Read())
                    {
                        User user = new User();
                        user.IDUser = Convert.ToInt32(UserDR["IDUser"]);
                        user.Username = UserDR["Username"].ToString();
                        user.Password = UserDR["Password"].ToString();
                        user.Role = Convert.ToInt32(UserDR["Role"]);
                        user.Status = Convert.ToBoolean(UserDR["Status"]);
                        user.PhotoPath = UserDR["PhotoPath"].ToString();

                        listUser.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                listUser = null;
            }

            UserDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return listUser;
        }

        public bool InsertUpdateUser(User user)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "User_InsertUpdateUser";                                              

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader UserDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IDUser", SqlDbType.Int).Value = user.IDUser;
            sqlCommand.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = user.Username;
            sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = user.Password;
            sqlCommand.Parameters.Add("@Role", SqlDbType.Int).Value = user.Role;
            sqlCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = user.Status;
            sqlCommand.Parameters.Add("@PhotoPath", SqlDbType.VarChar, 100).Value = user.PhotoPath;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                UserDR = sqlCommand.ExecuteReader();                
            }
            catch (Exception ex)
            {
                UserDR.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();

                return false;
            }

            UserDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return true;
        }

        public List<User> GetAllUserWithAuth(int IDProject)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "User_GetAllUserWithAuth";
            List<User> listUser = new List<User>();

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader UserDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add("@IDProject", SqlDbType.Int).Value = IDProject;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                UserDR = sqlCommand.ExecuteReader();
                if (UserDR.HasRows)
                {
                    while (UserDR.Read())
                    {
                        User user = new User();
                        user.IDUser = Convert.ToInt32(UserDR["IDUser"]);
                        user.Username = UserDR["Username"].ToString();                        
                        user.isAuth = Convert.ToBoolean(UserDR["isAuth"]);
                        user.PhotoPath = UserDR["PhotoPath"].ToString();
                        listUser.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                listUser = null;
            }

            UserDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return listUser;
        }

        public List<SelectListItem> GetUserByIDProject(int IDProject)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "User_GetUserByIDProject";
            List<SelectListItem> listUser = new List<SelectListItem>();
            listUser.Add(new SelectListItem() { Text = "Select User", Value = "0" });

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader UserDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add("@IDProject", SqlDbType.VarChar, 50).Value = IDProject;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                UserDR = sqlCommand.ExecuteReader();
                if (UserDR.HasRows)
                {
                    while (UserDR.Read())
                    {
                        listUser.Add(new SelectListItem() { Text = UserDR["Username"].ToString(), Value = UserDR["IDUser"].ToString() });                        
                    }
                }
            }
            catch (Exception ex)
            {
                listUser = null;
            }

            UserDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return listUser;
        }

        public bool CheckPrivilege(string URL)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            string cmd = "User_CheckPrivilege";

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader UserDR = null;

            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@URL", SqlDbType.VarChar, 100).Value = URL;            
            sqlCommand.CommandText = cmd;
            sqlConnection.Open();

            try
            {
                UserDR = sqlCommand.ExecuteReader();
                if (UserDR.HasRows)
                {
                    UserDR.Close();
                    sqlCommand.Dispose();
                    sqlConnection.Close();

                    return true;
                }
            }
            catch (Exception ex)
            {
                                
            }

            UserDR.Close();
            sqlCommand.Dispose();
            sqlConnection.Close();

            return false;

        }
    }
}