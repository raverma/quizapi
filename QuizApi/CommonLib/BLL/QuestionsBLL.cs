using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using QuizApi.Models;


namespace QuizApi.CommonLib.BLL
{
    public class QuestionsBLL
    {
        public IEnumerable<Questions> GetQuestions()
        {
            List<Questions> questions = new List<Questions>();
            string strSql = "SELECT QuestionID, Type, QuestionText, Category, MaxScore, CreatedBy, CreatedOn FROM Questions";
            DAL dba = new DAL();
            DataTable dtQstn = dba.ExecuteCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            if (dtQstn.Rows.Count > 0)
            {

                foreach (DataRow qstnRow in dtQstn.Rows)
                {
                    questions.Add(new Questions
                    {
                        QuestionID = Convert.ToInt32(qstnRow["QuestionID"]),
                        Type = qstnRow["Type"].ToString(),
                        QuestionText = qstnRow["QuestionText"].ToString(),
                        Category = qstnRow["Category"].ToString(),
                        MaxScore = Convert.ToInt32(qstnRow["MaxScore"]),
                        CreatedBy = qstnRow["CreatedBy"].ToString(),
                        CreatedOn = qstnRow["CreatedOn"].ToString()
                    }
                    );
                }

            }
            return questions;
        }
        public Questions GetQuestion(int questionID)
        {
            Questions question = null;
            string strSql = "SELECT * FROM Questions WHERE QuestionID=" + questionID.ToString();
            DAL dba = new DAL();
            DataTable dtQstn = dba.ExecuteCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            if (dtQstn.Rows.Count > 0)
            {
                question = new Questions();
                question.QuestionID = Convert.ToInt32(dtQstn.Rows[0]["QuestionID"]);
                question.Type = dtQstn.Rows[0]["Type"].ToString();
                question.QuestionText = dtQstn.Rows[0]["QuestionText"].ToString();
                question.Category = dtQstn.Rows[0]["Category"].ToString();
                question.MaxScore = Convert.ToInt32(dtQstn.Rows[0]["MaxScore"]);
                question.CreatedBy = dtQstn.Rows[0]["CreatedBy"].ToString();
                question.CreatedOn = dtQstn.Rows[0]["CreatedOn"].ToString();

            }
            return question;
        }
        public int AddQuestion(Questions questions)
        {
            string strSql = "Insert into Questions(Type, QuestionText, Category, MaxScore,CreatedBy, CreatedOn) values ('" + questions.Type + "','" + questions.QuestionText + "','" + questions.Category + "','" + questions.MaxScore + "','" + questions.CreatedBy + "','" + DateTime.Now + "')";
            DAL dba = new DAL();
            int returnValue = dba.ExecuteNonQueryCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            return returnValue;
        }
        public int DeleteQuestion(int questionID)
        {
            string strSql = "Delete from Questions where QuestionID=" + questionID.ToString();
            DAL dba = new DAL();
            int returnValue = dba.ExecuteNonQueryCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            return returnValue;
        }
        public int UpdateQuestion(Questions questions)
        {
            string strSql = "Update Questions set Description ='" + questions.Type + "', Instructions='" + questions.QuestionText + "', '" + questions.Category + "', Instructions='" + questions.MaxScore + "', CreatedBy ='" + questions.CreatedBy + "', CreatedOn='" + DateTime.Now + "' where QuestionID =" + questions.QuestionID;
            DAL dba = new DAL();
            int returnValue = dba.ExecuteNonQueryCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            return returnValue;
        }

    }
}