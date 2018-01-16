using BusinessLogic.DTO.Poker;
using Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Base
{
    interface IPokerService : IGameService<Game>
    {
        int AddRoom(int bet);
        PokerViewModel AddUserToRoom(int userId, string nickname, int roomId);
        PokerViewModel GetRoomState(int id);
    }
}
