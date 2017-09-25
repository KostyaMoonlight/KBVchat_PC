using BusinessLogic.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Base
{
    public interface IAuthenticationService
    {
        bool Authenticate(string login, string password);
    }
}
