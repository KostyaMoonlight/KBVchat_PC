using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KVBchat_ASP.Areas.Authentication.Models
{
    public class UserRegistrationViewModel
    {
        [DisplayName("Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter data")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Phone nubmer")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter data")]
        [Phone]
        public string Phone { get; set; }

        [DisplayName("Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter data")]
        [MinLength(6, ErrorMessage = "Min length is 6 chars")]
        [MaxLength(50, ErrorMessage ="Max length is 50 chars")]
        public string Password { get; set; }

        [DisplayName("Nickname")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter data")]
        [MinLength(2, ErrorMessage = "Min length is 2 chars")]
        [MaxLength(50, ErrorMessage ="Max length is 50 chars")]
        public string Nickname { get; set; }

        [DisplayName("First name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter data")]
        [MinLength(2, ErrorMessage = "Min length is 2 chars")]
        [MaxLength(50, ErrorMessage ="Max length is 50 chars")]
        public string FirstName { get; set; }

        [DisplayName("Middle name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter data")]
        [MinLength(2, ErrorMessage = "Min length is 2 chars")]
        [MaxLength(50, ErrorMessage ="Max length is 50 chars")]
        public string MiddleName { get; set; }

        [DisplayName("Third name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter data")]
        [MinLength(2, ErrorMessage = "Min length is 2 chars")]
        [MaxLength(50, ErrorMessage ="Max length is 50 chars")]
        public string ThirdName { get; set; }

        [DisplayName("Birthdate")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter data")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}