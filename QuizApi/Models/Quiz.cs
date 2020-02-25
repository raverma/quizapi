using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApi.Models
{
    public class Quiz
    {
        public int QuizID { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    }
}