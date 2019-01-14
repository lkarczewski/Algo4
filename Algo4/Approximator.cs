using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo4
{
    class Approximator
    {
        public static ApproximationFunction GetApproximation(int polynomialDegree, double[] arguments, double[] values)
        {
            var sUnknowns = polynomialDegree * 2 + 1;
            var sMatrix = new double[arguments.Length, sUnknowns];
            var sVector = new double[sUnknowns];

            for (var i = 0; i < arguments.Length; i++)
            {
                for (var j = 0; j < sUnknowns; j++)
                {
                    sMatrix[i, j] = Math.Pow(arguments[i], j);
                    sVector[j] += sMatrix[i, j];
                }
            }


            var tUnknowns = polynomialDegree + 1;
            var tMatrix = new double[arguments.Length, sUnknowns];
            var tVector = new double[sUnknowns];

            for (var i = 0; i < arguments.Length; i++)
            {
                for (var j = 0; j < tUnknowns; j++)
                {
                    tMatrix[i, j] = values[i] * sMatrix[i, j];
                    tVector[j] += tMatrix[i, j];
                }
            }


            var rMatrix = new double[tUnknowns, tUnknowns];
            var rVector = new double[tUnknowns];
            var offset = 0;

            for (var i = 0; i < tUnknowns; i++)
            {
                for (var j = 0; j < tUnknowns; j++)
                {
                    rMatrix[i, j] = sVector[j + offset];
                }

                rVector[i] = tVector[i];
                offset += 1;
            }

            var matrix = new MyMatrix<double>(rMatrix);
            matrix.GaussPartialPivot(rVector);

            return new ApproximationFunction(rVector, arguments, values);
        }
    }
}
