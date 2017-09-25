using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.Message
{
    public class MessageViewModel
    {

        public int IdSender { get; set; }

        public string Sender { get; set; }

        public int IdGroup { get; set; }

        public bool IsRead { get; set; }

        public bool IsDelivered { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }

    }
}
