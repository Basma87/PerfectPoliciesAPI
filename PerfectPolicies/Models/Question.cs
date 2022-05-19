using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicies.Models
{
    public class Question
    {

        public int QuestionID { get; set; }

        public string QuestionTopic { get; set; }

        public string QuestionText { get; set; }

        public string? ImagePath { get; set; }

        /// <summary>
        ///  navigation Property to get Options of a question
        /// </summary>
        public ICollection<Option> Options { get; set; }
        /// <summary>
        /// navigation Property to get quiz related to the question
        /// </summary>
        public Quiz Quiz { get; set; }


        /// <summary>
        ///  Foreign Key of QuizID in Question table
        /// </summary>

        public int QuizID { get; set; }

       

    }
}
