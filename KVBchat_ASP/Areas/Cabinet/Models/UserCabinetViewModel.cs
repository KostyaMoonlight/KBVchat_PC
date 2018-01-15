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
        [DisplayName("Your balance")]
        [DataType(DataType.Currency)]
        public double Balance { get; set; }

        [DisplayName("Your card number")]
        [DataType(DataType.CreditCard)]
        public string Card { get; set; }

        [DisplayName("Your card date")]
        public string CardDate { get; set; }

        [DisplayName("Your card SVV")]
        [MinLength(3)]
        [MaxLength(3)]
        public string CardSVV { get; set; }
    }
}