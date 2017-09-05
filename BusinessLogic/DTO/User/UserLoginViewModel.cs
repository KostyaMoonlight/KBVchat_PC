using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.User
{
    public class UserLoginViewModel
    {
        [DisplayName("Login")]
        public string Login { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
