using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.DTO
{
    public class Winners
    {
        public IEnumerable<int> Ids { get; set; }
        public double Money { get; set; }
    }
}
