using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DataAccess.Repositories.Base;
using DataAccess.Context;
using System.Data.Entity;

namespace DataAccess.Repositories
{
    public class GroupRepository
        : IGroupRepository
    {
        KVBchatDbContext _context = null;

        public GroupRepository(KVBchatDbContext context)
        {
            _context = context;
        }

        public void AddUserToGroup(int groupId, int memberId)
        {
            if (!_context.UsersGroups.Any(x=>x.IdUser == memberId && x.IdGroup == groupId))
                _context.UsersGroups.Add(new UsersGroup { IdGroup = groupId, IdUser = memberId });
            SaveChanges();
        }

        public void RemoveUserFromGroup(int userId, int groupId)
        {
            var user = _context.UsersGroups.FirstOrDefault(x => x.IdGroup == groupId && x.IdUser == userId);
            if (user != null)
            {
                _context.UsersGroups.Remove(user);
                SaveChanges();
            }

        }

        public void RemoveGroupAndMessages(int groupId)
        {
            var group = _context.Groups.FirstOrDefault(x => x.Id == groupId);
            if (group != null)
            {
                _context.Groups.Remove(group);
                var messages = _context.Messages.Where(x => x.IdGroup == groupId);
                _context.Messages.RemoveRange(messages);
                SaveChanges();
            }
        }

        public int AddGroup(int creatorId, string name)
        {
            var group = _context.Groups.Add(new Group { IdAdmin = creatorId, Name = name });
            SaveChanges();
            var groupId = group.Id;
            return groupId;
        }

        public Group GetGroup(int id)
        {
            return _context.Groups.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Group> GetGroups()
        {
            return _context.Groups.ToArray();
        }

        public IEnumerable<Group> GetGroups(Expression<Func<Group, bool>> func)
        {
            return _context.Groups.Where(func).ToArray();

        }

        public IEnumerable<Group> GetUsersGroups(int idUser)
        {
            return _context.UsersGroups
                .Include(x => x.Group)
                .Where(x => x.IdUser == idUser)
                .Select(x => x.Group)
                .Include(x=>x.Messages)
                .ToList();
        }

        public IEnumerable<Group> GetUsersGroups(Expression<Func<Group, bool>> func)
        {
            return _context.Groups.Where(func).ToArray();
        }

        public IEnumerable<UsersGroup> GetUsersGroupsIncludeUsers(Expression<Func<UsersGroup, bool>> func)
        {
            return _context.UsersGroups.Include(x=>x.User).Where(func).ToArray();

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }


    }
}
