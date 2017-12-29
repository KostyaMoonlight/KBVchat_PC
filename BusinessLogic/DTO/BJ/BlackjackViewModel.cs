﻿using Blackjack.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.BJ
{
    public class BlackjackViewModel
    {
        public int RoomId { get; set; }
        public Player Casino { get; set; }
        public List<Player> Players { get; set; }
        public List<Card> Cards { get; set; }
        public int CurrentPlayer { get; set; }
        public int CurrentPlayerId { get => Players[CurrentPlayer > 0 ? CurrentPlayer : 0].Id; }
        public string Winners { get; set; }
    }
}
