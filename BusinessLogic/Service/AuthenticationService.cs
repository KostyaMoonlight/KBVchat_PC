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

        public bool Authenticate(UserLoginViewModel userViewModel)
        {
            var passwordBase64 = userViewModel.Password.EncryptPassword();

            var user = _repository.GetUser(userViewModel.Login);

            if (user == null)
            {
                return false;
            }

            return user.Password == passwordBase64;
        }
    }
}
