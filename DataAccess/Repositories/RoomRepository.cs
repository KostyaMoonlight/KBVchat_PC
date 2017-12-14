using DataAccess.Context;
using DataAccess.Repositories.Base;
using Domain.Entities;
using System.Linq;

namespace DataAccess.Repositories
{
    public class RoomRepository
        : IRoomRepository
    {
        KVBchatDbContext _context = null;

        public RoomRepository(KVBchatDbContext context)
        {
            _context = context;
        }

        public void DeleteRoom(int id)
        {
            var room = _context.Rooms.FirstOrDefault(x => x.Id == id);
            _context.Rooms.Remove(room);
            SaveChanges();
        }

        public Room GetRoomById(int id)
        {
            return _context.Rooms.FirstOrDefault(x => x.Id==id);
        }
     
        public void UpdateRoom(Room room)
        {
            var oldRoom = _context.Rooms.FirstOrDefault(x => x.Id == room.Id);
            oldRoom.Name = room.Name;
            oldRoom.State = room.State;
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
