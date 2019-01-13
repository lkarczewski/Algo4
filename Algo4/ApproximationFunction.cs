using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Algo4
{
    class ApproximationFunction
    {
        public readonly double[] Polynomial;
        public double[] Arguments;
        public double[] Values;

        public ApproximationFunction(double[] polynomial, double[] arguments, double[] values)
        {
            Polynomial = polynomial;
            Arguments = arguments;
            Values = values;
        }

        public double GetResult(double argument)
        {
            var result = 0.0;
            for (var i = 0; i < Polynomial.Length; i++)
            {
                result += Polynomial[i] * Math.Pow(argument, i);
            }

            return result;
        }

        public double ApproximationError()
        {
            double approximationError = 0.0;

            for (var i = 0; i < Values.Length; i++)
            {
                approximationError += Math.Pow(Values[i] - GetResult(Arguments[i]), 2);
            }

            return approximationError;
        }

        public string GetFunctionString()
        {
            var output = string.Empty;
            for (var i = 0; i < Polynomial.Length; i++)
            {
                output += $"{Math.Round(Polynomial[i],4)}*x^{i} + ";
            }

            return output;
        }
    }
}