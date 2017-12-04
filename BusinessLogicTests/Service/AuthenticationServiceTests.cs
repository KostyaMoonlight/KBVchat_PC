using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Service.Base;
using DataAccess.Repositories.Base;
using Domain.Entities;
using Utility;

namespace BusinessLogic.Service.Tests
{
    [TestClass()]
    public class AuthenticationServiceTests
    {
        [TestMethod()]
        public void AuthenticateTest_ValidData_True()
        {
            string email = "email1";
            string pass = "pass1";
            IAuthenticationService service = new AuthenticationService(new AuthenticationRepositoryTests());

            var result = service.Authenticate(email, pass);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void AuthenticateTest_UnvalidData_False()
        {
            string email = "email1";
            string pass = "pass2";
            IAuthenticationService service = new AuthenticationService(new AuthenticationRepositoryTests());

            var result = service.Authenticate(email, pass);

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void AuthenticateTest_EmptyData_False()
        {
            IAuthenticationService service = new AuthenticationService(new AuthenticationRepositoryTests());

            var result = service.Authenticate(null, null);

            Assert.IsFalse(result);
        }
    }

    public class AuthenticationRepositoryTests
        : IAuthenticationRepository
        {
            public User GetUser(string login)
            {
                return new User { Email = "email1", Password = "pass1".EncryptPassword() };
            }
        }
}