using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KVBchat_ASP.Areas.UserSearchEngine.Models.User
{
    public class UserSearchViewModel
    {
        [Required]
        public string FullName { get; set; }

        public int? Age { get; set; }
    }
}