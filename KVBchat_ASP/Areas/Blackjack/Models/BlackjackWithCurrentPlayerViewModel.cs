using BusinessLogic.DTO.BJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KVBchat_ASP.Areas.Blackjack.Models
{
    public class BlackjackWithCurrentPlayerViewModel
    {
        public BlackjackViewModel BlackjackViewModel { get; set; }
        public int CurrentUserId { get; set; }
    }
}