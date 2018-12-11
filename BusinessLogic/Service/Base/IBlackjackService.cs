using Blackjack;
using BusinessLogic.DTO.BJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Base
{
    public interface IBlackjackService
        :IGameService<Game>
    {
        int AddRoom(int bet, int maxPlayersCount);
        BlackjackViewModel AddUserToRoom(int userId, double balance, string nickname, int roomId);
        BlackjackViewModel RemoveUserFromRoom(int userId, int roomId);
        BlackjackViewModel GetRoomState(int id);
        void Double(int roomId, int userId);
        void Hit(int roomId, int userId);
        void Stand(int roomId, int userId);
        IEnumerable<BlackJackSearchViewModel> GetBlackJackRooms();
        Task<IEnumerable<string>> GetHintFromNN(double casino, double player);
    }
}
