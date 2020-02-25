using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using QuizApi.Models;




namespace QuizApi.CommonLib.BLL
{
    public class QuizBLL
    {
        public IEnumerable<Quiz> GetQuizzes()
        {
            List<Quiz> quizzess = new List<Quiz>();
            string strSql = "SELECT QuizID, Description, Instructions, CreatedBy, CreatedOn FROM Quiz";
            DAL dba = new DAL();
            DataTable dtQuiz = dba.ExecuteCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            if (dtQuiz.Rows.Count > 0)
            {

                foreach (DataRow quizRow in dtQuiz.Rows)
                {
                    quizzess.Add(new Quiz
                    {
                        QuizID = Convert.ToInt32(quizRow["QuizID"]),
                        Description = quizRow["Description"].ToString(),
                        Instructions = quizRow["Instructions"].ToString(),
                        CreatedBy = quizRow["CreatedBy"].ToString(),
                        CreatedOn = quizRow["CreatedOn"].ToString()
                    }
                    );
                }

            }
            return quizzess;
        }

        public Quiz GetQuiz(int quizID)
        {
            Quiz quiz = null;
            string strSql = "SELECT * FROM Quiz WHERE QuizID=" + quizID.ToString();
            DAL dba = new DAL();
            DataTable dtQuiz = dba.ExecuteCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            if (dtQuiz.Rows.Count > 0)
            {
                quiz = new Quiz();
                quiz.QuizID = Convert.ToInt32(dtQuiz.Rows[0]["QuizID"]);
                quiz.Description = dtQuiz.Rows[0]["Description"].ToString();
                quiz.Instructions = dtQuiz.Rows[0]["Instructions"].ToString();
                quiz.CreatedBy = dtQuiz.Rows[0]["CreatedBy"].ToString();
                quiz.CreatedOn = dtQuiz.Rows[0]["CreatedOn"].ToString();

            }
            return quiz;
        }
        public int AddQuiz(Quiz quiz)
        {
            string strSql = "Insert into Quiz(Description, Instructions, CreatedBy, CreatedOn) values ('" + quiz.Description + "','" + quiz.Instructions + "','" + quiz.CreatedBy + "','" + DateTime.Now + "')";
            DAL dba = new DAL();
            int returnValue = dba.ExecuteNonQueryCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            return returnValue;
        }
        public int DeleteQuiz(int quizID)
        {
            string strSql = "Delete from Quiz where QuizID=" + quizID.ToString();
            DAL dba = new DAL();
            int returnValue = dba.ExecuteNonQueryCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            return returnValue;
        }
        public int UpdateQuiz(Quiz quiz)
        {
            string strSql = "Update Quiz set Description ='" + quiz.Description + "', Instructions='" + quiz.Instructions + "', CreatedBy ='" + quiz.CreatedBy + "', CreatedOn='" + DateTime.Now + "' where QuizID =" + quiz.QuizID;
            DAL dba = new DAL();
            int returnValue = dba.ExecuteNonQueryCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            return returnValue;
        }
    }
}