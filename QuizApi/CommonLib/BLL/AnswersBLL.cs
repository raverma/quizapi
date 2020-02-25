using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using QuizApi.Models;


namespace QuizApi.CommonLib.BLL
{
    public class AnswersBLL
    {
        public IEnumerable<Answers> GetAnswers()
        {
            List<Answers> answers = new List<Answers>();
            string strSql = "SELECT AnswerID, QuestionID, OptionSequence, AnswerText, IsCorrect, CreatedBy, CreatedOn FROM Answers";
            DAL dba = new DAL();
            DataTable dtAnswr = dba.ExecuteCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            if (dtAnswr.Rows.Count > 0)
            {

                foreach (DataRow answRow in dtAnswr.Rows)
                {
                    answers.Add(new Answers
                    {
                        AnswerID = Convert.ToInt32(answRow["AnswerID"]),
                        QuestionID = Convert.ToInt32(answRow["QuestionID"]),
                        OptionSequence = Convert.ToInt32(answRow["OptionSequence"]),
                        AnswerText = answRow["AnswerText"].ToString(),
                        IsCorrect = Convert.ToInt32(answRow["IsCorrect"]),
                        CreatedBy = answRow["CreatedBy"].ToString(),
                        CreatedOn = answRow["CreatedOn"].ToString()
                    }
                    );
                }

            }
            return answers;
        }
        public Answers GetAnswer(int answerID)
        {
            Answers answer = null;
            string strSql = "SELECT * FROM Answers WHERE AnswerID=" + answerID.ToString();
            DAL dba = new DAL();
            DataTable dtAnswr = dba.ExecuteCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            if (dtAnswr.Rows.Count > 0)
            {
                answer = new Answers();
                answer.AnswerID = Convert.ToInt32(dtAnswr.Rows[0]["AnswerID"]);
                answer.QuestionID = Convert.ToInt32(dtAnswr.Rows[0]["QuestionID"]);
                answer.OptionSequence = Convert.ToInt32(dtAnswr.Rows[0]["OptionSequence"]);
                answer.AnswerText = dtAnswr.Rows[0]["AnswerText"].ToString();
                answer.IsCorrect = Convert.ToInt32(dtAnswr.Rows[0]["IsCorrect"]);
                answer.CreatedBy = dtAnswr.Rows[0]["CreatedBy"].ToString();
                answer.CreatedOn = dtAnswr.Rows[0]["CreatedOn"].ToString();

            }
            return answer;
        }
        public int AddAnswer(Answers answers)
        {
            string strSql = "Insert into Answers(QuestionID,OptionSequence, AnswerText, IsCorrect, CreatedBy, CreatedOn) values ('" + answers.QuestionID + "','" + answers.OptionSequence + "','" + answers.AnswerText + "','" + answers.IsCorrect + "', '" + answers.CreatedBy + "','" + DateTime.Now + "')";
            DAL dba = new DAL();
            int returnValue = dba.ExecuteNonQueryCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            return returnValue;
        }
        public int DeleteAnswer(int answerID)
        {
            string strSql = "Delete from Answers where answerID=" + answerID.ToString();
            DAL dba = new DAL();
            int returnValue = dba.ExecuteNonQueryCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            return returnValue;
        }
        public int UpdateAnswer(Answers answers)
        {
            string strSql = "Update Answers set OptionSequence ='" + answers.OptionSequence + "', AnswerText='" + answers.AnswerText + "', IsCorrect='" + answers.IsCorrect + "',  CreatedBy ='" + answers.CreatedBy + "', CreatedOn='" + DateTime.Now + "' where AnswerID =" + answers.AnswerID;
            DAL dba = new DAL();
            int returnValue = dba.ExecuteNonQueryCommand(strSql, ConfigurationManager.ConnectionStrings["QuizDB"].ConnectionString);
            return returnValue;
        }

    }
}