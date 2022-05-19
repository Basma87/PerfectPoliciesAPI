using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicies.Models
{
    public class Quiz
    {
        public int QuizID { get; set; }
        public string QuizTitle { get; set; }
        public string QuizTopic { get; set; }

        public string CreatorName { get; set; }

        public DateTime Created { get; set; }

        public int PassPercentage { get; set; }

        /// <summary>
        /// Navigation  property to get list of questions of a quiz
        /// </summary>
        public ICollection<Question> Questions { get; set; }

    }
}
