using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Algo4
{
    class Test
    {
        public double[] GenerateVector(int size)
        {
            double[] vector = new double[size];

            for (int i = 0; i < size - 1; i++)
            {
                vector[i] = 0.0;
            }

            vector[size - 1] = 1.0;

            return vector;
        }

        public void GaussPartialPivotTimeTest(int numberOfAgents, int count)
        {
            MatrixGenerator mg = new MatrixGenerator(numberOfAgents);

            MyMatrix<double> macierz = new MyMatrix<double>(mg.size, mg.size);
            MyMatrix<double> macierzKopia = new MyMatrix<double>(mg.size, mg.size);
            double[] wektor = new double[mg.size];
            double[] wektorKopia = new double[mg.size];

            //przygotowanie macierzy i wektorów
            macierz = mg.GenerateMatrix();
            macierzKopia = mg.GenerateMatrix();
            wektor = GenerateVector(mg.size);
            wektorKopia = GenerateVector(mg.size);

            //liczenie czasu
            double[] czasy = new double[count];
            double suma = 0.0;
            double srednia = 0.0;

            for (int i = 0; i < count; i++)
            {
                var watchDouble = Stopwatch.StartNew();
                macierz.GaussPartialPivot(wektor);
                watchDouble.Stop();
                var elapsedMsDouble = watchDouble.ElapsedMilliseconds;
                czasy[i] = elapsedMsDouble;

                for (var j = 0; j < macierz.Rows(); j++)
                {
                    for (var k = 0; k < macierz.Columns(); k++)
                    {
                        macierz[j, k] = macierzKopia[j, k];
                        wektor[j] = wektorKopia[j];
                    }
                }
                suma += czasy[i];
            }

            srednia = suma / count;

            StreamWriter writer = new StreamWriter("CzasGaussPartialPivot.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(mg.size + ";" + srednia + ";"));
            }
            writer.Close();

            Console.WriteLine("Średni czas GaussPartialPivot: " + srednia + "ms");
        }

        public void GaussPartialPivotSparseTimeTest(int numberOfAgents, int count)
        {
            MatrixGenerator mg = new MatrixGenerator(numberOfAgents);

            MyMatrix<double> macierz = new MyMatrix<double>(mg.size, mg.size);
            MyMatrix<double> macierzKopia = new MyMatrix<double>(mg.size, mg.size);
            double[] wektor = new double[mg.size];
            double[] wektorKopia = new double[mg.size];

            //przygotowanie macierzy i wektorów
            macierz = mg.GenerateMatrix();
            macierzKopia = mg.GenerateMatrix();
            wektor = GenerateVector(mg.size);
            wektorKopia = GenerateVector(mg.size);

            //liczenie czasu
            double[] czasy = new double[count];
            double suma = 0.0;
            double srednia = 0.0;

            for (int i = 0; i < count; i++)
            {
                var watchDouble = Stopwatch.StartNew();
                macierz.GaussPartialPivotSparse(wektor);
                watchDouble.Stop();
                var elapsedMsDouble = watchDouble.ElapsedMilliseconds;
                czasy[i] = elapsedMsDouble;

                for (var j = 0; j < macierz.Rows(); j++)
                {
                    for (var k = 0; k < macierz.Columns(); k++)
                    {
                        macierz[j, k] = macierzKopia[j, k];
                        wektor[j] = wektorKopia[j];
                    }
                }
                suma += czasy[i];
            }

            srednia = suma / count;

            StreamWriter writer = new StreamWriter("CzasGaussPartialPivotSparse.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(mg.size + ";" + srednia + ";"));
            }
            writer.Close();

            Console.WriteLine("Średni czas GaussPartialPivotSparse" +
                ": " + srednia + "ms");
        }

        public void SeidelTimeTest(int numberOfAgents, int count, double accuracy)
        {
            MatrixGenerator mg = new MatrixGenerator(numberOfAgents);

            MyMatrix<double> macierz = new MyMatrix<double>(mg.size, mg.size);
            double[] wektor = new double[mg.size];


            //przygotowanie macierzy i wektorów
            macierz = mg.GenerateMatrix();
            wektor = GenerateVector(mg.size);

            //liczenie czasu
            double[] czasy = new double[count];
            double suma = 0.0;
            double srednia = 0.0;

            for (int i = 0; i < count; i++)
            {
                var watchDouble = Stopwatch.StartNew();
                macierz.SeidelAccuracy(wektor, accuracy);
                watchDouble.Stop();
                var elapsedMsDouble = watchDouble.ElapsedMilliseconds;
                czasy[i] = elapsedMsDouble;

                suma += czasy[i];
                wektor = GenerateVector(mg.size);
            }

            srednia = suma / count;

            string name = "CzasSeidel" + accuracy + ".csv";

            StreamWriter writer = new StreamWriter(name, append: true);
            if (writer != null)
            {
                writer.WriteLine(String.Format(mg.size + ";" + srednia + ";"));
            }
            writer.Close();

            Console.WriteLine("Średni czas Seidel " + accuracy + ": " + srednia + "ms");
        }

        //public double LoadData(string fileName)
        //{
        //    var reader = new StreamReader(fileName);
        //    double[] data = new double[];
        //}

        //public void GaussPartialPivotApproximation()
        //{
        //    double[] rozmiarMacierzy;
        //    double[] czasWykonania;

        //    var p = Approximator.GetApproximation(3, rozmiarMacierzy, czasWykonania);
        //    Console.WriteLine(p.GetFunctionString());
        //    Console.WriteLine(p.ApproximationError());
        //}
    }
}