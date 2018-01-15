using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.User
{
    public class UserInfoViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime LastTimeAccess { get; set; }

        public int UnreadMessages { get; set; }

        public int IsOnline { get; set; }

        public string Nickname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Balance { get; set; }

        public string Card { get; set; }

        public string CardExpirationDate { get; set; }

        public string CardCVV { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
