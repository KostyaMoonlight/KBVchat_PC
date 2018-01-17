using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace KVBchat_ASP.Areas.Cabinet.Models
{
    public class UserDepositViewModel
    {
        public int Id { get; set; }

        [DisplayName("Your balance")]
        public double Balance { get; set; }

        [DisplayName("Amount")]
        //[DataType(DataType.Currency)]
        public double Deposit { get; set; }
    }
}