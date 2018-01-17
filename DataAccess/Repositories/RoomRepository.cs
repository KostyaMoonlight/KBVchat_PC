using DataAccess.Context;
using DataAccess.Repositories.Base;
using Domain.Entities;
using System.Collections.Generic;
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

        public Room AddRoom(string name, string state)
        {
            var room = _context.Rooms.Add(new Room { Name = name, State = state });
            SaveChanges();
            return room;
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

        public IEnumerable<Room> GetBlakJackRooms()
        {
            return _context.Rooms.Where(room => room.Name.Equals("Blackjack"));
        }

        public IEnumerable<Room> GetPokerRooms()
        {
            return _context.Rooms.Where(room => room.Name.Equals("Poker"));
        }
    }
}
