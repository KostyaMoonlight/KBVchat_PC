using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackpropagationNetwork.Base
{
    public class ActivationFunctions
    {
        public double Sng(double value)
        {
            return value > 0.5 ? 1 : 0;
        }

        public double Line(double value)
        {
            return value;
        }

        public double Sigmoid(double value)
        {
            return 1 / (1 + Math.Exp(-value * 0.05));
        }

        public double HyperbolicTangent(double value)
        {
            return (Math.Exp(2 * value) - 1) / (Math.Exp(2 * value) + 1);
        }
    }
}
