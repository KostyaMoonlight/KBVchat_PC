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

        [DisplayName("Last name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter data")]
        [MinLength(2, ErrorMessage = "Min length is 2 chars")]
        [MaxLength(50, ErrorMessage ="Max length is 50 chars")]
        public string LastName { get; set; }

        [DisplayName("Birthdate")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter data")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [DisplayName("Your card number")]
        [DataType(DataType.CreditCard)]
        [RegularExpression("^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11})$", ErrorMessage = "Wrong card fromat")]
        public string Card { get; set; }

        [DisplayName("Your card expiration date")]
        [RegularExpression("^(0[1-9]|1[0-2])//?([0-9]{2})$", ErrorMessage = "Wrong expiration date format")]
        public string CardExpirationDate { get; set; }

        [DisplayName("Your card CVV")]
        [RegularExpression("^[0-9]{3,4}$", ErrorMessage = "Wrong CVV fromat")]
        public string CardCVV { get; set; }
    }
}