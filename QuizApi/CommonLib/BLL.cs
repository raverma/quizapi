using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using QuizApi.Models;
namespace QuizApi.CommonLib
{
    public class BLL
    {
        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();
            string strSql = "SELECT UserID, FirstName, LastName, Email, Email2, Phone FROM Users";
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
                            Phone = userRow["Phone"].ToString()
                        }
                    );
                }
                
            }
            return users;
        }
    }
}