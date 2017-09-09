using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.User
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        public string Nickname { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string ThirdName { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
