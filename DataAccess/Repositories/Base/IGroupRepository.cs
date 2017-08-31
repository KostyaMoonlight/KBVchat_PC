﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Base
{
    public interface IGroupRepository
    {
        Group GetGroup(int id);
        IEnumerable<Group> GetGroups();
        IEnumerable<Group> GetGroups(Expression<Func<Group, bool>> func);
    }
}
