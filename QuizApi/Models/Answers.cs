using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApi.Models
{
    public class Answers
    {
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }
        public int OptionSequence { get; set; }
        public string AnswerText { get; set; }
        public int IsCorrect { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    }
}