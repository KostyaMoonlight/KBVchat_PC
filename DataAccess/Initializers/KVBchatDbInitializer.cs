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
                        Email ="Email1",
                        IsOnline = 1,
                        Password = Convert.ToBase64String(Encoding.UTF8.GetBytes("pass1")),
                        Phone = "Phone1",
                        UnreadMessages = 0,
                        LastTimeAccess = DateTime.Now,
                        Birthdate = DateTime.Now.AddYears(-10),
                        FirstName = "Fn1",
                        MiddleName = "Mn1",
                        ThirdName = "Tn1",
                        Nickname = "Nn1"
                    },
                    new User
                    {
                        Email ="Email2",
                        IsOnline = 2,
                        Password = Convert.ToBase64String(Encoding.UTF8.GetBytes("pass2")),
                        Phone = "Phone2",
                        UnreadMessages = 1,
                        LastTimeAccess = DateTime.Now,
                        Birthdate = DateTime.Now.AddYears(-20),
                        FirstName = "Fn2",
                        MiddleName = "Mn2",
                        ThirdName = "Tn2",
                        Nickname = "Nn2"
                    }
                });

            context.SaveChanges();

            context.Friends.Add(
                new Friend { IdFirst = 1, IdSecond = 2, IsConfirmed = true }
                );

            context.SaveChanges();

            context.Groups.AddRange(new List<Group>
            {
                new Group{ Name = "Group1", IdAdmin = 1},
                new Group{ Name = "Group2" }
            });

            context.SaveChanges();

            context.Messages.AddRange(new List<Message>
            {
                new Message
                {
                    IdGroup =2,
                    IdSender = 1,
                    IsDelivered = false,
                    IsRead = false,
                    Text = "Text1",
                    Time = DateTime.Now,
                },
                new Message
                {
                    IdGroup =1,
                    IdSender = 2,
                    IsDelivered = false,
                    IsRead = false,
                    Text = "Text2",
                    Time = DateTime.Now,
                }
            });

            context.SaveChanges();

            
        }
    }
}
