using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_MetodeNumerik
{
    class fungsi
    {
        public string kalimat;
        string[] kata;
        char[] pemisah;
        char[] tanda;

        public double CariJumlahPerKata(int i, double x, string[] kata)
        {
            double jumlah = 0;
            if (kata[i].Length == 1 && kata[i] == "x")
                jumlah += x;
            else
            {
                bool ketemu = false;
                int index = -1;
                for (int j = 0; j < kata[i].Length; j++)
                {
                    if (kata[i][j] == 'x')
                    {
                        ketemu = true; index = j; break;
                    }
                }
                if (ketemu)
                {
                    string angka = "";
                    if (index == kata[i].Length - 1)
                    {
                        for (int j = 0; j < index; j++) angka += kata[i][j];
                        jumlah += (Convert.ToDouble(angka) * x);
                    }
                    else if (index == 0)
                    {
                        angka = "";
                        index += 2;
                        for (int j = index; j < kata[i].Length; j++) angka += kata[i][j];
                        jumlah += Math.Pow(x, Convert.ToDouble(angka));
                    }
                    else
                    {
                        angka = "";
                        for (int j = index + 2; j < kata[i].Length; j++)
                            angka += kata[i][j];
                        double t = Math.Pow(x, Convert.ToDouble(angka));

                        angka = "";
                        for (int j = 0; j < index; j++)
                            angka += kata[i][j];
                        jumlah += (Convert.ToDouble(angka) * t);
                    }
                }
                else
                    jumlah += Convert.ToDouble(kata[i]);
            }
            return jumlah;
        }

        public double fx(double x)
        {
            double total = 0.0;
            for (int i = 0; i < kata.Length; i++)
            {
                if (tanda[i] == '+')
                    total += CariJumlahPerKata(i, x, kata);
                else
                    total -= CariJumlahPerKata(i, x, kata);
            }


            return total;
        }
        public void manipulasi()
        {
            pemisah = new char[2] { '+', '-' };
            tanda = new char[1] { '+' };
            string kal = "";
            if (kalimat[0] == '-')
            {
                for (int i = 1; i < kalimat.Length; i++) kal += kalimat[i];
            }
            else
                kal = kalimat;
            kata = kal.Split(pemisah);
            if (kalimat[0] == '-')
            {
                kata[0] = "-" + kata[0];
            }

            for (int i = 0; i < kata.Length; i++)
                kata[i] = kata[i].Trim();



            int m = 0;
            if (kalimat[0] == '-') m = 1;
            for (int i = m; i < kalimat.Length; i++)
            {
                if (kalimat[i] == '+' || kalimat[i] == '-')
                {
                    Array.Resize(ref tanda, tanda.Length + 1);
                    tanda[tanda.GetUpperBound(0)] = kalimat[i];
                }
            }
        }
    }
}
