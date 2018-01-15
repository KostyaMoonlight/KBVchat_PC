using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KVBchat_ASP.Models.User
{
    public class UserEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [DisplayName("Nickname")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your nickname")]
        [MinLength(2, ErrorMessage = "Min length is 6 chars")]
        [MaxLength(50, ErrorMessage = "Max length is 50 chars")]
        public string Nickname { get; set; }

        [DisplayName("First name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your first name")]
        [MinLength(2, ErrorMessage = "Min length is 6 chars")]
        [MaxLength(50, ErrorMessage = "Max length is 50 chars")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter your middle name")]
        [MinLength(2, ErrorMessage = "Min length is 6 chars")]
        [MaxLength(50, ErrorMessage = "Max length is 50 chars")]
        public string LastName { get; set; }

        [DisplayName("Birthdate")]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }
    }
}