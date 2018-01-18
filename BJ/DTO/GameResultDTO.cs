using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DTO
{
    public class GameResultDTO
    {
        public double Action { get; set; }
        public double Player { get; set; }
        public IEnumerable<double> Enemies { get; set; }
        public double Casino { get => Enemies.First(); }
        public double GameResult { get; set; }
    }
}
