using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicies.Models
{
    /// <summary>
    /// Class that represent user's login Data
    /// </summary>
    public class UserInfo
    {
        public int UserInfoID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
