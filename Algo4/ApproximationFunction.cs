using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algo4
{
    class ApproximationFunction
    {
        public double[] Polynominal;

        public ApproximationFunction(double[] polynominal)
        {
            Polynominal = polynominal;
        }

        public double GetResult(double argument)
        {
            var result = 0.0;

            for(var i = 0; i < Polynominal.Length; i++)
            {
                result += Polynominal[i] * Math.Pow(argument, i);
            }

            return result;
        }

        public string GetFunctionString()
        {
            var output = string.Empty;

            for(var i = 0; i < Polynominal.Length; i++)
            {
                output += $"{Polynominal[i]}*x^{i} + ";
            }

            return output;
        }
    }
}