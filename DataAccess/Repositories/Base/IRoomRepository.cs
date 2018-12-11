using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Base
{
    public interface IRoomRepository
    {
        Room AddRoom(string name, string state);
        Room GetRoomById(int id);
        void UpdateRoom(Room room);
        void DeleteRoom(int id);
        IEnumerable<Room> GetBlakJackRooms();
        IEnumerable<Room> GetPokerRooms();
    }
}
