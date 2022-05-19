using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicies.Models
{
    public class Option
    { 
        public int OptionID { get; set; }

        public string OptionText { get; set; }

        public int OptionNumber { get; set; }

        public bool CorrectAnswer { get; set; }

        /// <summary>
        ///  navigation property to get questions related to an option
        /// </summary>
        public Question Question { get; set; }

        /// <summary>
        ///  define foreign Key of questionID in Option table
        /// </summary>
        public int QuestionID { get; set; }
        
    }
}
