using DataAccess.Context;
using DataAccess.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccess.Repositories
{
    public class FriendRepository
        :IFriendRepository
    {
        KVBchatDbContext _context = null;

        public FriendRepository(KVBchatDbContext context)
        {
            _context = context;
        }

        public void AddFriends(int firstId, int secondId)
        {
            _context.Friends.Add(new Friend()
            {
                IdFirst = firstId,
                IdSecond = secondId,
                IsConfirmed = false
            });
            SaveChanges();
        }

        public bool ConfirmeFriend(int firstId, int secondId)
        {
            var friends = _context.Friends.FirstOrDefault(x => x.IdFirst == firstId && x.IdSecond == secondId);
            if (friends != null)
            {
                friends.IsConfirmed = true;
                SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveFriend(int firstId, int secondId)
        {
            var friends = _context.Friends.FirstOrDefault(x => x.IdFirst == firstId && x.IdSecond == secondId);
            if (friends == null)
            {
                return false;
            }
            _context.Friends.Remove(friends);
            SaveChanges();
            return true;
        }

        public IEnumerable<User> GetUsersFriends(int id)
        {
            var users = _context.Friends
                .Include(x => x.SecondUser)
                .Where(x => x.IdFirst == id)
                .Select(x => x.SecondUser)
                .ToList();


            var users2 = _context.Friends
                .Include(x => x.FirstUser)
                .Where(x => x.IdSecond == id)
                .Select(x => x.FirstUser)
                .ToList();
            users.AddRange(users2);

            return users;

        }

        public IEnumerable<int> GetUsersFriendsId(Expression<Func<Friend, bool>> whereIdFunc, Expression<Func<Friend, bool>> whereFunc, Expression<Func<Friend, int>> selectFunc)
        {
            return _context.Friends
                .Where(whereIdFunc)//x => x.IdFirst == id || x.IdSecond == id
                .Where(whereFunc)
                .Select(selectFunc);// x => x.IdSecond == id ? x.IdFirst : x.IdSecond
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
