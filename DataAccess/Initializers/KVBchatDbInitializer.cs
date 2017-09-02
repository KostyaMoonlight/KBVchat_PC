using DataAccess.Context;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Initializers
{
    public class KVBchatDbInitializer
        : DropCreateDatabaseAlways<KVBchatDbContext>
    {
        protected override void Seed(KVBchatDbContext context)
        {
            context.Users.AddRange(
                new List<User>{
                    new User
                    {
                        Id=1,
                        Email ="Email1",
                        IsOnline = 1,
                        Password = "pass1",
                        Phone = "Phone1",
                        UnreadMessages = 0,
                        LastTimeAccess = DateTime.Now
                    },
                    new User
                    {
                        Id= 2,
                        Email ="Email2",
                        IsOnline = 2,
                        Password = "pass2",
                        Phone = "Phone2",
                        UnreadMessages = 1,
                        LastTimeAccess = DateTime.Now
                    }
                });

            context.UsersInfo.AddRange(
                new List<UserInfo>
                {
                    new UserInfo
                    {
                        Id = 1,
                        Birthdate = DateTime.Now.AddYears(-10),
                        FirstName = "Fn1",
                        MiddleName = "Mn1",
                        ThirdName = "Tn1",
                        Nickname = "Nn1"
                    },
                    new UserInfo
                    {
                        Id = 2,
                        Birthdate = DateTime.Now.AddYears(-20),
                        FirstName = "Fn2",
                        MiddleName = "Mn2",
                        ThirdName = "Tn2",
                        Nickname = "Nn2"
                    }
                }
                );
        }
    }
}
