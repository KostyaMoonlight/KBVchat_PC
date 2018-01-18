using BusinessLogic.DTO.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KVBchat_ASP.Areas.Poker.Models
{
    public class PokerWithCurrentPlayerViewModel
    {
        public PokerViewModel BlackjackViewModel { get; set; }
        public int CurrentUserId { get; set; }
    }
}