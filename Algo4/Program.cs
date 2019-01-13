using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo4
{
    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test();

            //TESTY WYDAJNOŚCI

            //StreamWriter writer1 = new StreamWriter("CzasGaussPartialPivot.csv", append: true);
            //writer1.WriteLine();
            //writer1.WriteLine("rozmiar;czas");
            //writer1.Close();
            //for (int i = 3; i <= 70; i++)
            //{
            //    Console.WriteLine("Ilość agentów: " + i);
            //    t.GaussPartialPivotTimeTest(i, 1);
            //}

            //StreamWriter writer2 = new StreamWriter("CzasGaussPartialPivotSparse.csv", append: true);
            //writer2.WriteLine();
            //writer2.WriteLine("rozmiar;czas");
            //writer2.Close();
            //for (int i = 3; i <= 62; i++)
            //{
            //    Console.WriteLine("Ilość agentów: " + i);
            //    t.GaussPartialPivotSparseTimeTest(i, 1);
            //}

            //StreamWriter writer3 = new StreamWriter("CzasSeidel1e-10.csv", append: true);
            //writer3.WriteLine();
            //writer3.WriteLine("rozmiar", "czas");
            //writer3.Close();
            //for (int i = 3; i <= 62; i ++)
            //{
            //    Console.WriteLine("Ilość agentów: " + i);
            //    t.SeidelTimeTest(i, 1, 1e-10);
            //}

            //StreamWriter writer4 = new StreamWriter("CzasSeidel1e-10.csv", append: true);
            //writer4.WriteLine();
            //writer4.WriteLine("rozmiar", "czas");
            //writer4.Close();
            //for (int i = 3; i <= 62; i++)
            //{
            //    Console.WriteLine("Ilość agentów: " + i);
            //    t.GenerateMatrixTimeTest(i,1);
            //}

            //APROKSYMACJA

            t.GaussPartialPivotApproximation();
            t.GaussPartialPivotSparseApproximation();
            t.GaussSeidelApproximation();

            //double[] argumenty = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //double[] wartosci = { 1.3, 3.5, 4.2, 5.0, 7.0, 8.8, 10.1, 12.5, 13.0, 15.6 };
            //double[] wynik = new double[2];

            //var p = Approximator.GetApproximation(1, argumenty, wartosci);
            //Console.WriteLine(p.GetFunctionString());
            //Console.WriteLine(p.ApproximationError());
            //Console.WriteLine(p.GetResult(100000));

            //double[] argumenty = { 0.0, 0.25, 0.50, 0.75, 1.00 };
            //double[] wartosci = { 1.0, 1.284, 1.6487, 2.1170, 2.7183 };
            //double[] wynik = new double[3];

            //var p = Approximator.GetApproximation(2, argumenty, wartosci);
            //Console.WriteLine(p.GetResult(2));
            //Console.WriteLine(p.GetFunctionString());

            //double[,] test = new double[2, 2] { { 10, 55 }, { 55, 385 }};
            //double[] vector = new double[] { 81, 572.4 };
            //double[] vector1 = (dynamic)vector.Clone();

            //MyMatrix<double> macierz1 = new MyMatrix<double>(test);
            //macierz1.GaussPartialPivot(vector);

            //for(var i = 0; i < vector.Length; i++)
            //{
            //    Console.WriteLine(vector[i]);
            //}

        }
    }
}
