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
    public class AnswersController : ApiController
    {
        private AnswersBLL answrbll;
        public AnswersController()
        {
            this.answrbll = new AnswersBLL();
        }

        public AnswersController(AnswersBLL answrbll)
        {
            this.answrbll = answrbll;
        }

        // GET /api/users   
        public IEnumerable<Answers> GetAnswers()
        {
            answrbll = new AnswersBLL();
            return answrbll.GetAnswers();
        }
        public Answers GetAnswer(int answerID)
        {
            answrbll = new AnswersBLL();
            return answrbll.GetAnswer(answerID);
        }
        [HttpPost]
        public int AddAnswer(Answers answers)
        {
            answrbll = new AnswersBLL();
            try
            {
                return answrbll.AddAnswer(answers);

            }
            catch
            {
                return 0;
            }
        }
        [HttpDelete]
        public int DeleteAnswer(int answerID)
        {
            answrbll = new AnswersBLL();
            try
            {
                return answrbll.DeleteAnswer(answerID);

            }
            catch
            {
                return 0;
            }

        }
        [HttpPut]
        public int UpdateAnswer(Answers answers)
        {
            answrbll = new AnswersBLL();
            try
            {
                return answrbll.UpdateAnswer(answers);

            }
            catch
            {
                return 0;
            }
        }
    }
}
