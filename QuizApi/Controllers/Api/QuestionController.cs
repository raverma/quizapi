using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizApi.Models;
using QuizApi.CommonLib.BLL;

namespace QuizApi.Controllers.Api
{
    public class QuestionController : ApiController
    {
        private QuestionsBLL qustnbll;
        public QuestionController()
        {
            this.qustnbll = new QuestionsBLL();
        }

        public QuestionController(QuestionsBLL qustnbll)
        {
            this.qustnbll = qustnbll;
        }

        // GET /api/users   
        public IEnumerable<Questions> GetQuestions()
        {
            qustnbll = new QuestionsBLL();
            return qustnbll.GetQuestions();
        }
        public Questions GetQuestion(int questionID)
        {
            qustnbll = new QuestionsBLL();
            return qustnbll.GetQuestion(questionID);
        }
        [HttpPost]
        public int AddQuestion(Questions questions)
        {
            qustnbll = new QuestionsBLL();
            try
            {
                return qustnbll.AddQuestion(questions);

            }
            catch
            {
                return 0;
            }
        }
        [HttpDelete]
        public int DeleteQuestion(int questionID)
        {
            qustnbll = new QuestionsBLL();
            try
            {
                return qustnbll.DeleteQuestion(questionID);

            }
            catch
            {
                return 0;
            }

        }
        [HttpPut]
        public int UpdateQuestion(Questions questions)
        {
            qustnbll = new QuestionsBLL();
            try
            {
                return qustnbll.UpdateQuestion(questions);

            }
            catch
            {
                return 0;
            }
        }
    }
}
