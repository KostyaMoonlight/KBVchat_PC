using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KVBchat_ASP.Models.User
{
    public class UserViewModel
    {
        public bool IsAuthenticated { get; set; }
        public string Nickname { get; set; }
    }
}