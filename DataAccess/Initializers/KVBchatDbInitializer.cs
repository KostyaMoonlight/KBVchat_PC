using DataAccess.Context;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

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
                        Password = "pass1".EncryptPassword(),
                        Phone = "Phone1",
                        UnreadMessages = 0,
                        LastTimeAccess = DateTime.Now,
                        Birthdate = DateTime.Now.AddYears(-10),
                        FirstName = "Kostya",
                        LastName = "Zdor",
                        Nickname = "Moonlight",
                        Balance = 200,
                        Card = "5550212334324322",
                        CardExpirationDate = "09/18",
                        CardCVV = "456",
                        RoomId=null
                    },
                    new User
                    {
                        Email ="Email2",
                        IsOnline = 2,
                        Password = "pass2".EncryptPassword(),
                        Phone = "Phone2",
                        UnreadMessages = 1,
                        LastTimeAccess = DateTime.Now,
                        Birthdate = DateTime.Now.AddYears(-20),
                        FirstName = "Vlad",
                        LastName = "Gromadskiy",
                        Nickname = "Grom",
                        Balance = 200,
                        Card = "5550212132114444",
                        CardExpirationDate = "10/18",
                        CardCVV = "233",
                        RoomId=null

                    },
                    new User
                    {
                        Email ="Email3",
                        IsOnline = 2,
                        Password = "pass3".EncryptPassword(),
                        Phone = "Phone3",
                        UnreadMessages = 1,
                        LastTimeAccess = DateTime.Now,
                        Birthdate = DateTime.Now.AddYears(-20),
                        FirstName = "Bodya",
                        LastName = "Shuker",
                        Nickname = "Melkiy",
                        Balance = 200,
                        Card = "5550212334324322",
                        CardExpirationDate = "09/18",
                        CardCVV = "456",
                        RoomId=null
                    },
                    new User
                    {
                        Email ="Email4",
                        IsOnline = 2,
                        Password = "pass4".EncryptPassword(),
                        Phone = "Phone4",
                        UnreadMessages = 1,
                        LastTimeAccess = DateTime.Now,
                        Birthdate = DateTime.Now.AddYears(-20),
                        FirstName = "Vlad",
                        LastName = "Betin",
                        Nickname = "Mandarin",
                        Balance = 200,
                        Card = "5550212334324322",
                        CardExpirationDate = "09/18",
                        CardCVV = "456",
                        RoomId=null
                    }
                });

            context.SaveChanges();

            context.Friends.AddRange(new List<Friend> {
                new Friend { IdFirst = 1, IdSecond = 2, IsConfirmed = true },
                new Friend { IdFirst = 1, IdSecond = 3, IsConfirmed = true },
                new Friend { IdFirst = 1, IdSecond = 4, IsConfirmed = true },
                new Friend { IdFirst = 2, IdSecond = 3, IsConfirmed = true },
                new Friend { IdFirst = 3, IdSecond = 4, IsConfirmed = true }
                }
                );

            context.SaveChanges();



            context.Groups.AddRange(new List<Group>
            {
                new Group{ Name = "GroupTV51", IdAdmin = 1},
                new Group{ Name = "Group324" }
            });

            context.SaveChanges();

            context.UsersGroups.AddRange(new List<UsersGroup>
            {
                new UsersGroup{ IdUser = 1, IdGroup = 1},
                new UsersGroup{ IdUser = 2, IdGroup = 1},
                new UsersGroup{ IdUser = 3, IdGroup = 1},
                new UsersGroup{ IdUser = 1, IdGroup = 2},
                new UsersGroup{ IdUser = 2, IdGroup = 2}
            });

            context.SaveChanges();

            context.Messages.AddRange(new List<Message>
            {
                new Message
                {
                    IdGroup =1,
                    IdSender = 1,
                    IsDelivered = false,
                    IsRead = false,
                    Text = "Text asd asd asd fa f",
                    Time = DateTime.Now,
                },
                new Message
                {
                    IdGroup =1,
                    IdSender = 2,
                    IsDelivered = false,
                    IsRead = false,
                    Text = "Tdfasd f afg adcxas as",
                    Time = DateTime.Now,
                },
                new Message
                {
                    IdGroup =1,
                    IdSender = 1,
                    IsDelivered = false,
                    IsRead = false,
                    Text = "Tesdsdkgj sjfj asbkj ",
                    Time = DateTime.Now,
                },
                new Message
                {
                    IdGroup =1,
                    IdSender = 2,
                    IsDelivered = false,
                    IsRead = false,
                    Text = "f kjsd  aS DaS Das asd D",
                    Time = DateTime.Now,
                },
                new Message
                {
                    IdGroup =1,
                    IdSender = 1,
                    IsDelivered = false,
                    IsRead = false,
                    Text = "Text asd asd asd fa f",
                    Time = DateTime.Now,
                },
                new Message
                {
                    IdGroup =1,
                    IdSender = 2,
                    IsDelivered = false,
                    IsRead = false,
                    Text = "Tdfzha  dfvc ws sc f f wef q",
                    Time = DateTime.Now,
                },
                new Message
                {
                    IdGroup =2,
                    IdSender = 1,
                    IsDelivered = false,
                    IsRead = false,
                    Text = "Text asd asd asd fa f",
                    Time = DateTime.Now,
                },
                new Message
                {
                    IdGroup =1,
                    IdSender = 2,
                    IsDelivered = false,
                    IsRead = false,
                    Text = "Tdfzha  dfvc ws sc f f wef q",
                    Time = DateTime.Now,
                }
            });

            context.SaveChanges();


        }
    }
}
