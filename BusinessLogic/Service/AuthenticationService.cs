using BusinessLogic.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DTO.User;
using DataAccess.Repositories.Base;
using Utility;

namespace BusinessLogic.Service
{
    public class AuthenticationService
        : IAuthenticationService
    {
        IAuthenticationRepository _repository = null;

        public AuthenticationService(IAuthenticationRepository repository)
        {
            _repository = repository;
        }

        public bool Authenticate(string login, string password)
        {
            if (login == null || password == null)
            {
                return false;
            }
            var passwordBase64 = password.EncryptPassword();

            var user = _repository.GetUser(login);

            if (user == null)
            {
                return false;
            }

            return user.Password == passwordBase64;
        }
    }
}
