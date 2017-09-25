using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KVBchat_ASP.Models.User
{
    public class UserRegistrationViewModel
    {
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage ="Enter email")]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Nickname { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string ThirdName { get; set; }

        public DateTime Birthdate { get; set; }
    }
}