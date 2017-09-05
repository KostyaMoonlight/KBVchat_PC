using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class AuthenticationRepository
        : IAuthenticationRepository
    {
        KVBchatDbContext _context = null;

        public AuthenticationRepository(KVBchatDbContext context)
        {
            _context = context;
        }

        public User GetUser(string login)
        {
            return _context.Users.FirstOrDefault(x => x.Email == login || x.Phone == login);
        }
    }
}
