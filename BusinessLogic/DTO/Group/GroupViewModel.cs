using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.Group
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        public int LastSenderId { get; set; }

        public string Name { get; set; }

        public string AdminName { get; set; }

        public int UnreadMessagesCount { get; set; }
    }
}
