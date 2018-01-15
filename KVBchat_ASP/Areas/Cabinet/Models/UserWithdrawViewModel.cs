using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KVBchat_ASP.Areas.Cabinet.Models
{
    public class UserWithdrawViewModel
    {
        public int Id { get; set; }

        [DisplayName("Your balance")]
        [DataType(DataType.Currency)]
        public double Balance { get; set; }

        [DisplayName("Amount")]
        [DataType(DataType.Currency)]
        public double Withdraw { get; set; }
    }
}