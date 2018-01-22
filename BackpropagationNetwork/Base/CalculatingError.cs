using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackpropagationNetwork.Base
{
    public class CalculatingError
    {
        public double MSE(double[] real, double[] expected)
        {
            if (real.Count() == 0)
                throw new ArgumentException("Length of sets can't be 0.");

            if (real.Count() != expected.Count())
                throw new ArgumentException($"Differentent length of sets: real={real.Count()} expected={expected.Count()}.");

            var sum = 0.0;
            var setsCount = real.Count();
            for (int i = 0; i < setsCount; i++)
            {
                sum += Math.Pow(real[i] - expected[i], 2);
            }
            return sum / setsCount;
        }

        public double RootMSE(double[] real, double[] expected)
        {
            var MSE_Result = MSE(real, expected);
            return Math.Sqrt(MSE_Result);
        }

        public double Arctan(double[] real, double[] expected)
        {
            if (real.Count() == 0)
                throw new ArgumentException("Length of sets can't be 0.");

            if (real.Count() != expected.Count())
                throw new ArgumentException($"Differentent length of sets: real={real.Count()} expected={expected.Count()}.");

            var sum = 0.0;
            var setsCount = real.Count();
            for (int i = 0; i < setsCount; i++)
            {
                sum += Math.Pow(Math.Atan(real[i] - expected[i]), 2);
            }
            return sum / setsCount;
        }
    }
}
