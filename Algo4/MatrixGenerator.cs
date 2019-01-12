using System;
using System.Collections.Generic;

namespace Algo4
{
    class MatrixGenerator
    {
        public int numberOfAgents;
        public List<string> Keys;
        public Dictionary<string, int> KeyToIndex;
        public Dictionary<int, string> IndexToKey;
        double BinomNoOfAgentsOver2; // n nad 2, potrzebne do liczenia prawdopodobieństwa
        public int size;

        public MatrixGenerator(int _numberOfAgents)
        {
            numberOfAgents = _numberOfAgents;
            GenerateKeyList();
            GenerateKeyMaps();
            size = Keys.Count;
            BinomNoOfAgentsOver2 = Binominal(_numberOfAgents, 2);
        }

        public MyMatrix<double> GenerateMatrix()
        {
            MyMatrix<double> macierz = new MyMatrix<double>(size, size);

            for (int i = 0; i < macierz.Rows(); i++)
            {
                for (int j = 0; j < macierz.Columns(); j++)
                {
                    macierz[i, j] = GenerateValue(i, j); //generate value
                }
            }

            return macierz;
        }

        public double GenerateValue(int i, int j)
        {
            //Pobranie danych o przypadku, którego wiersz jest aktualnie sprawdzany ("przed")
            string currentCaseR = IndexToKey[i];
            string[] countsR = currentCaseR.Split(',');
            int yesCountR = int.Parse(countsR[0]);
            int noCountR = int.Parse(countsR[1]);
            int unknownCountR = numberOfAgents - yesCountR - noCountR;

            //Pobranie danych o przypadku, którego kolumna jest aktualnie sprawdzana ("po")
            string currentCaseC = IndexToKey[j];
            string[] countsC = currentCaseC.Split(',');
            int yesCountC = int.Parse(countsC[0]);
            int noCountC = int.Parse(countsC[1]);

            //Wszyscy na TAK - koniec
            if (yesCountR == numberOfAgents && noCountR == 0 && i == j)
                return 1.0;
            //Wszyscy na NIE - koniec
            else if (yesCountR == 0 && noCountR == numberOfAgents && i == j)
                return 1.0;
            //Wszyscy na NIE WIEM - koniec
            else if (yesCountR == 0 && noCountR == 0 && i == j)
                return 1.0;
            //Liczba agentów nie zmienia się: wylosowano agentów z tym samym stanem ( (Y,Y) || (N,N) || (U,U) )
            else if (yesCountR == yesCountC && noCountR == noCountC && (yesCountR > 1 || noCountR > 1 || unknownCountR > 1))
                return Case1(numberOfAgents, yesCountR, noCountR);
            //Liczba agentów na TAK zwiększa się o 1 (Y,U)
            else if (yesCountR + 1 == yesCountC && noCountR == noCountC && yesCountR > 0 && yesCountR + noCountR < numberOfAgents)
                return Case2(numberOfAgents, yesCountR, noCountR);
            //Liczba agentów na NIE zwiększa się o 1 (N,U)
            else if (yesCountR == yesCountC && noCountR + 1 == noCountC && noCountR > 0 && yesCountR + noCountR < numberOfAgents)
                return Case3(numberOfAgents, yesCountR, noCountR);
            //Liczba agentów na NIE WIEM zwiększa się o 2 (Y,N)
            else if (yesCountR - 1 == yesCountC && noCountR - 1 == noCountC && yesCountR > 0 && noCountR > 0)
                return Case4(numberOfAgents, yesCountR, noCountR);
            // Unikalny przypadek, gdy jest po jednym agencie, i nie możliwy jest powrót do tego samego stanu(Zwróci -1)
            else if (i == j)
                return -1.0;
            else
                return 0.0;
        }

        //Przypadek 1: stan się nie zmienia
        public double Case1(int numberOfAgents, int numberOfYes, int numberOfNo)
        {
            double prob = -1.0;

            if (numberOfYes > 1) //jeśli jest możliwe, że wybrano 2x"tak"
                prob += Binominal(numberOfYes, 2) / BinomNoOfAgentsOver2;

            if (numberOfNo > 1) //jeśli jest możliwe, że wybrano 2x"nie"
                prob += Binominal(numberOfNo, 2) / BinomNoOfAgentsOver2;

            if (numberOfAgents - numberOfYes - numberOfNo > 1) //jeśli jest możliwe, że wybrano 2x"niezdecydowany"
                prob += Binominal(numberOfAgents - numberOfYes - numberOfNo, 2) / BinomNoOfAgentsOver2;


            return prob;
        }

        //Przypadek 2: liczba agentów na TAK zwiększa się o 1
        public double Case2(int numberOfAgents, int numberOfYes, int numberOfNo)
        {
            double prob = 0.0;

            if (numberOfYes > 0 && numberOfAgents - numberOfYes - numberOfNo > 0) //jeśli jest możliwe, że wybrano 1x"tak" i 1x"niezdecydowany"
                prob += (numberOfYes * (numberOfAgents - numberOfYes - numberOfNo)) / BinomNoOfAgentsOver2;

            return prob;
        }

        //Przypadek 3: liczba agentów na NIE zwiększa się o 1
        public double Case3(int numberOfAgents, int numberOfYes, int numberOfNo)
        {
            double prob = 0.0;

            if (numberOfNo > 0 && numberOfAgents - numberOfYes - numberOfNo > 0) //jeśli jest możliwe, że wybrano 1x"nie" i 1x"niezdecydowany"
                prob += (numberOfNo * (numberOfAgents - numberOfYes - numberOfNo)) / BinomNoOfAgentsOver2;

            return prob;
        }

        //Przypadek 2: liczba agentów na NIE WIEM zwiększa się o 2
        public double Case4(int numberOfAgents, int numberOfYes, int numberOfNo)
        {
            double prob = 0.0;

            if (numberOfNo > 0 && numberOfYes > 0) //jeśli jest możliwe, że wybrano 1x"nie" i 1x"niezdecydowany"
                prob += (numberOfNo * numberOfYes) / BinomNoOfAgentsOver2;

            return prob;
        }

        public double[] GenerateVector()
        {
            double[] wektor = new double[size];

            for (int i = 0; i < size - 1; i++)
            {
                wektor[i] = 0.0;
            }
            wektor[size - 1] = 1.0;

            return wektor;
        }

        public void GenerateKeyList()
        {
            Keys = new List<string>();

            for (int i = 0; i <= numberOfAgents; i++)
            {
                for (int j = 0; j <= numberOfAgents - i; j++)
                {
                    Keys.Add($"{i},{j}");
                    //Console.WriteLine($"{i},{j}");
                }
            }
        }

        public void GenerateKeyMaps()
        {
            KeyToIndex = new Dictionary<string, int>();
            IndexToKey = new Dictionary<int, string>();

            int i = 0;
            foreach (string s in Keys)
            {
                KeyToIndex[s] = i;
                IndexToKey[i] = s;
                //Console.WriteLine(IndexToKey[i]);
                //Console.WriteLine(KeyToIndex[s]);
                i++;
            }
        }

        public static double Binominal(int n, int k)
        {
            double result = 1;

            for (int i = 1; i <= k; i++)
            {
                result *= n - (k - i);
                result /= i;
            }

            return result;
        }
    }
}