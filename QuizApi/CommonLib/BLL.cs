using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using QuizApi.Models;
using System.Data.SqlClient;

namespace QuizApi.CommonLib
{
    public class BLL
    {
        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();
            string strSql = "SELECT UserID, FirstName, LastName, Email, Password, Email2, Phone FROM Users";
            DAL dba = new DAL();
            DataTable dtUsers = dba.ExecuteCommand(strSql,ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            if (dtUsers.Rows.Count > 0)
            {
                
                foreach(DataRow userRow in dtUsers.Rows)
                {
                    users.Add(new User
                        {
                            UserID = Convert.ToInt32(userRow["UserID"]),
                            FirstName = userRow["FirstName"].ToString(),
                            LastName = userRow["LastName"].ToString(),
                            Email = userRow["Email"].ToString(),
                            Password = userRow["Password"].ToString(),
                            Phone = userRow["Phone"].ToString()
                        }
                    );
                }
                
            }
            return users;
        }

        public void Register(User user)
        {
            string strSql = "insert into [dbo].[Users](FirstName,LastName,Email,Password, Email2,Phone)values('" + user.FirstName + "','" + user.LastName + "','" + user.Email + "','" + user.Password + "','','9898989898')";
            DAL dba = new DAL();
            dba.ExecuteCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
        }

        public string Login(string email, string password)
        {
            var user = GetUsers().Where(m => m.Email == email && m.Password == password).FirstOrDefault();
            if (user != null)
            {
                return "Success";
            }
            else
            {
                return "Fail";
            }
        }
    }
}