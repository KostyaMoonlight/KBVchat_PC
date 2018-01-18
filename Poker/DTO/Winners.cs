using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.DTO
{
    public class Winners
    {
        public IEnumerable<int> Ids { get; set; }
        public IEnumerable<string> Names { get; set; }
        public double Money { get; set; }
        public Winners()
        {
            Money = 40000;
        }
    }
}
