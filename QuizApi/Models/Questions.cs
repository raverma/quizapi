using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApi.Models
{
    public class Questions
    {
        public int QuestionID { get; set; }
        public string Type { get; set; }
        public string QuestionText { get; set; }
        public string Category { get; set; }
        public int MaxScore { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    }
}