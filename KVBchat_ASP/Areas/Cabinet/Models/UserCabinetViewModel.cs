using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KVBchat_ASP.Areas.Cabinet.Models
{
    public class UserCabinetViewModel
    {
        public int Id { get; set; }

        [DisplayName("Your balance")]
        [DataType(DataType.Currency)]
        public double Balance { get; set; }

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