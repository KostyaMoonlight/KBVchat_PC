using BusinessLogic.DTO.Poker;
using Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Base
{
   public interface IPokerService : IGameService<Game>
    {
        int AddRoom(int bet,int maxPlayersCount);
        PokerViewModel AddUserToRoom(int userId, string nickname, int roomId);
        PokerViewModel GetRoomState(int id);
        IEnumerable<PokerRoomSearchViewModel> GetPokerRooms();
    }
}
