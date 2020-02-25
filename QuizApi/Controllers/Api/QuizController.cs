using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using QuizApi.Models;
using System.Web.Http;
using QuizApi.CommonLib.BLL;

namespace QuizApi.Controllers.Api
{
    public class QuizController : ApiController
    {
        private QuizBLL quizbll;
        public QuizController()
        {
            this.quizbll = new QuizBLL();
        }

        public QuizController(QuizBLL quizbll)
        {
            this.quizbll = quizbll;
        }

        // GET /api/users   
        public IEnumerable<Quiz> GetQuizzes()
        {
            quizbll = new QuizBLL();
            return quizbll.GetQuizzes();
        }
        public Quiz GetQuiz(int quizID)
        {
            quizbll = new QuizBLL();
            return quizbll.GetQuiz(quizID);
        }
        [HttpPost]
        public int AddQuiz(Quiz quiz)
        {
            quizbll = new QuizBLL();
            try
            {
                return quizbll.AddQuiz(quiz);

            }
            catch
            {
                return 0;
            }
        }
        [HttpDelete]
        public int DeleteQuiz( int quizID)
        {
            quizbll = new QuizBLL();
            try
            {
                return quizbll.DeleteQuiz(quizID);
                  
            }catch 
            {
                return 0;
            }
            
        }
        [HttpPut]
        public int UpdateQuiz(Quiz quiz)
        {
            quizbll = new QuizBLL();
            try
            {
                return quizbll.UpdateQuiz(quiz);

            }
            catch
            {
                return 0;
            }
        }
    }
}
