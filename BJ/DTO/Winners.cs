using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.DTO
{
    public class Winners
    {
        public IEnumerable<int> Ids { get; set; }
        public IEnumerable<string> Names { get; set; }
        public double Money { get; set; }
    }
}
