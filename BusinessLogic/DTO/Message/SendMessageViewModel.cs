using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.Message
{
    public class SendMessageViewModel
    {
        public string Sender { get; set; }

        public string Group { get; set; }

        public string Text { get; set; }
    }
}
