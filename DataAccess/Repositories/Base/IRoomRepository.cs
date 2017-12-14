using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Base
{
    public interface IRoomRepository
        :ISaveChanges
    {
        Room GetRoomById(int id);
        void UpdateRoom(Room room);
        void DeleteRoom(int id);
    }
}
