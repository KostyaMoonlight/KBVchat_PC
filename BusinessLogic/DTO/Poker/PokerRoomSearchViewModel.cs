using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.Poker
{
 public class PokerRoomSearchViewModel
    {
        public int GameId { get; set; }
        public double Bet { get; set; }
        public int PlayersCount { get; set; }
        public int MaxPlayersCount { get; set; }
    }
}
