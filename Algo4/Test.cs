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
        private double valueFromFile;
        private double[] valuesFromFile;

        public double[] LoadData(string fileName)
        {
            var values = File.ReadAllLines(fileName)
            .SelectMany(a => a.Split(';')
            .Select(str => double.TryParse(str, out valueFromFile) ? valueFromFile : 0));

            valuesFromFile = values.ToArray();

            return valuesFromFile;
        }

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

        public void GenerateMatrixTimeTest(int numberOfAgents, int count)
        {
            MatrixGenerator mg = new MatrixGenerator(numberOfAgents);
            MyMatrix<double> macierz = new MyMatrix<double>(mg.size, mg.size);

            double[] czasy = new double[count];
            double suma = 0.0;
            double srednia = 0.0;

            for (var i = 0; i < count; i++)
            {
                var watchDouble = Stopwatch.StartNew();
                macierz = mg.GenerateMatrix();
                watchDouble.Stop();
                var elapsedMsDouble = watchDouble.ElapsedMilliseconds;
                czasy[i] = elapsedMsDouble;
                suma += czasy[i];
            }

            srednia = suma / count;

            StreamWriter writer = new StreamWriter("CzasGenerowaniaMacierzy.csv", append: true);
            if (writer != null)
            {
                writer.WriteLine(srednia);
            }
            writer.Close();

            Console.WriteLine("Średni czas generowania macierzy" +
                ": " + srednia + "ms");

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
                writer.WriteLine(srednia);
            }
            writer.Close();

            Console.WriteLine("Średni czas GaussPartialPivot" +
                ": " + srednia + "ms");
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
                writer.WriteLine(srednia);
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
                writer.WriteLine(srednia);
            }
            writer.Close();

            Console.WriteLine("Średni czas Seidel " + accuracy + ": " + srednia + "ms");
        }

        public void GaussPartialPivotApproximation()
        {
            double[] rozmiaryMacierzy = LoadData("rozmiary.csv");
            double[] czasWykonania = LoadData("CzasGaussPartialPivot.csv");

            var p = Approximator.GetApproximation(3, rozmiaryMacierzy, czasWykonania);
            Console.WriteLine("Wielomian dla Gauss Partial Pivot: " + p.GetFunctionString());
            Console.WriteLine("Szacowany czas 100000x100000: " + (p.GetResult(100000))/3600000);
            Console.WriteLine("Błąd aproksymacji dla Gauss Partial Pivot: " + p.ApproximationError()/1000);
            Console.WriteLine();

            StreamWriter writer = new StreamWriter("ApproximatePartialPivot.csv", append: false);
            if (writer != null)
            {
                Console.WriteLine(rozmiaryMacierzy[0] + " " + rozmiaryMacierzy.Length);
                for (var i = 0; i < rozmiaryMacierzy.Length; i++)
                {
                    writer.WriteLine(String.Format(p.GetResult(rozmiaryMacierzy[i]).ToString()));
                }

            }
            writer.Close();
        }

        public void GaussPartialPivotSparseApproximation()
        {
            double[] rozmiaryMacierzy = LoadData("rozmiary.csv");
            double[] czasWykonania = LoadData("CzasGaussPartialPivotSparse.csv");

            var p = Approximator.GetApproximation(2, rozmiaryMacierzy, czasWykonania);
            Console.WriteLine("Wielomian dla Gauss Partial Pivot Sparse: " + p.GetFunctionString());
            Console.WriteLine("Szacowany czas 100000x100000: " + (p.GetResult(100000)) / 3600000);
            Console.WriteLine("Błąd aproksymacji dla Gauss Partial Pivot Sparse: " + p.ApproximationError()/1000);
            Console.WriteLine();

            StreamWriter writer = new StreamWriter("ApproximatePivotSparse.csv", append: false);
            if (writer != null)
            {
                Console.WriteLine(rozmiaryMacierzy[0] + " " + rozmiaryMacierzy.Length);
                for (var i = 0; i < rozmiaryMacierzy.Length; i++)
                {
                    writer.WriteLine(String.Format(p.GetResult(rozmiaryMacierzy[i]).ToString()));
                }

            }
            writer.Close();
        }

        public void GaussSeidelApproximation()
        {
            double[] rozmiaryMacierzy = LoadData("rozmiary.csv");
            double[] czasWykonania = LoadData("CzasSeidel1e-10.csv");

            var p = Approximator.GetApproximation(2, rozmiaryMacierzy, czasWykonania);
            Console.WriteLine("Wielomian dla Gauss Seidel 1e-10: " + p.GetFunctionString());
            Console.WriteLine("Szacowany czas 100000x100000: " + (p.GetResult(100000)) / 3600000);
            Console.WriteLine("Błąd aproksymacji dla Gauss Seidel 1e-10: " + p.ApproximationError()/1000);
            Console.WriteLine();

            StreamWriter writer = new StreamWriter("ApproximateSeidel.csv", append: false);
            if (writer != null)
            {
                Console.WriteLine(rozmiaryMacierzy[0] + " " + rozmiaryMacierzy.Length);
                for (var i = 0; i < rozmiaryMacierzy.Length; i++)
                {
                    writer.WriteLine(String.Format(p.GetResult(rozmiaryMacierzy[i]).ToString()));
                }

            }
            writer.Close();
        }
    }
}