using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.User
{
    public class FriendShortInfoViewModel
    {
        public int Id { get; set; }

        public DateTime LastTimeAccess { get; set; }

        public int IsOnline { get; set; }

        public string Nickname { get; set; }

    }
}
