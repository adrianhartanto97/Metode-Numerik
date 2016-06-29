using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_MetodeNumerik
{
    class Turunanv2
    {
        private int N { set; get; }
        private double[] x;
        private double[] y;
        private double xx;
        private int pangkat { set; get; }
        private double[,] matriks;
        private double[,] matriks_temp;
        private double[] matriks_hasil;
        private double detA;
        private double[] ans;

        public Turunanv2(int n, int pangkat, double xx)
        {
            N = n;
            x = new double[n];
            y = new double[n];
            this.pangkat = pangkat;
            matriks = new double[pangkat + 1, pangkat + 1];
            matriks_temp = new double[pangkat + 1, pangkat + 1];
            matriks_hasil = new double[pangkat + 1];
            this.xx = xx;
        }

        public void tambahKoordinat(List<double> a, List<double> b)
        {
            for (int i = 0; i < N; i++)
            {
                x[i] = a[i];
                y[i] = b[i];
            }
        }

        public void createRegresi()
        {
            matriks[0, 0] = N;
            matriks_hasil[0] = sigmahasil(x, y, 0);
            for (int i = 1; i <= pangkat; i++)
            {
                matriks[0, i] = jumlah(x, i);
                //Console.WriteLine(matriks[0, i]);
            }

            int c = 1;
            for (int i = 1; i <= pangkat; i++)
            {
                for (int j = 0; j <= pangkat; j++)
                {
                    matriks[i, j] = jumlah(x, c + j);
                    //Console.WriteLine(matriks[i, j]);
                }
                c++;
                matriks_hasil[i] = sigmahasil(x, y, i);

            }


            for (int i = 0; i < pangkat + 1; i++)
            {
                for (int j = 0; j < pangkat + 1; j++)
                {
                    matriks_temp[i, j] = matriks[i, j];
                }

            }
            detA = det(matriks, pangkat + 1);
            ans = new double[pangkat + 1];
            for (int i = 0; i < pangkat + 1; i++)
            {
                if (i != 0)
                {
                    for (int j = 0; j < pangkat + 1; j++)
                    {
                        matriks_temp[j, i - 1] = matriks[j, i - 1];
                    }
                }
                for (int j = 0; j < pangkat + 1; j++)
                {
                    matriks_temp[j, i] = matriks_hasil[j];
                }
                double det_ini = det(matriks_temp, pangkat + 1);
                ans[i] = Math.Round(det_ini / detA, 8);
            }
        }

        public string tampilFungsiRegresi()
        {
            string str = "y' = ";
            for (int i = 0; i < ans.Length; i++)
            {
                str += Math.Round(ans[i],4).ToString();
                if (i >= 1) str += "x";
                if (i > 1)
                    str += "^" + i.ToString();
                if (i != ans.Length - 1) str += "  +  ";
            }
            return str;
        }

        private double fungsi(double[] ans, double x)
        {
            double t = 0.0;
            for (int i = 0; i < ans.Length; i++)
            {
                t += (ans[i] * Math.Pow(x, i));
            }
            return t;
        }

        private double jumlah(double[] x, int pangkat)
        {
            double t = 0.0;
            for (int i = 0; i < x.Length; i++)
                t += (Math.Pow(x[i], pangkat));
            return t;
        }

        private double sigmahasil(double[] x, double[] y, int pangkat)
        {
            double t = 0.0;
            for (int i = 0; i < x.Length; i++)
                t += (Math.Pow(x[i], pangkat)) * (y[i]);
            return t;
        }

        private double det(double[,] arr, int uk)
        {
            if (uk == 1)
            {
                return arr[0, 0];
            }
            else if (uk == 2)
            {
                return arr[0, 0] * arr[1, 1] - arr[0, 1] * arr[1, 0];
            }
            else
            {
                double[,] tmp = new double[100, 100];
                double res = 0;
                //kolom
                for (int i = 0; i < uk; i++)
                {
                    //baris
                    for (int j = 1; j < uk; j++)
                    {
                        //kolom
                        for (int k = 0; k < uk; k++)
                        {
                            if (k < i) tmp[j - 1, k] = arr[j, k];
                            //kurang 1 kolom kalau lbh besar dari kolom yang diskip
                            else if (k > i) tmp[j - 1, k - 1] = arr[j, k];
                        }
                    }
                    if (i % 2 == 0) res += arr[0, i] * det(tmp, uk - 1);
                    else res -= arr[0, i] * det(tmp, uk - 1);
                }
                return res;
            }
        }

        public double turunan1FDA1(double delta)
        {
            double f0 = fungsi(ans, xx);
            double f1 = fungsi(ans, xx + delta);
            return (f1 - f0) / delta;
        }

        public double turunan1FDA2(double delta)
        {
            double f0 = fungsi(ans, xx);
            double f1 = fungsi(ans, xx + delta);
            double f2 = fungsi(ans, xx + delta + delta);
            return (f2 * -1 + 4 * f1 - 3 * f0) / (2 * delta);
        }

        public double turunan1CDA1(double delta)
        {
            double f1 = fungsi(ans, xx + delta);
            double fmin1 = fungsi(ans, xx - delta);
            return (f1 - fmin1) / (delta * 2);
        }

        public double turunan1CDA2(double delta)
        {
            double f2 = fungsi(ans, xx + delta + delta);
            double f1 = fungsi(ans, xx + delta);
            double fmin1 = fungsi(ans, xx - delta);
            double fmin2 = fungsi(ans, xx - delta - delta);
            return (-1 * f2 + 8 * f1 - 8 * (fmin1) + fmin2) / (12 * delta);
        }

        public double turunan1BDA1(double delta)
        {
            double f0 = fungsi(ans, xx);
            double fmin1 = fungsi(ans, xx - delta);
            return (f0 - fmin1) / delta;
        }

        public double turunan1BDA2(double delta)
        {
            double f0 = fungsi(ans, xx);
            double fmin1 = fungsi(ans, xx - delta);
            double fmin2 = fungsi(ans, xx - delta - delta);
            return (3 * f0 - 4 * fmin1 + fmin2) / (2 * delta);
        }

        public double turunan2FDA1(double delta)
        {
            double f2 = fungsi(ans, xx + delta + delta);
            double f1 = fungsi(ans, xx + delta);
            double f0 = fungsi(ans, xx);
            return (f2 - 2 * f1 + f0) / (delta * delta);
        }

        public double turunan2FDA2(double delta)
        {
            double f3 = fungsi(ans, xx + delta + delta + delta);
            double f2 = fungsi(ans, xx + delta + delta);
            double f1 = fungsi(ans, xx + delta);
            double f0 = fungsi(ans, xx);
            return (-1 * f3 + 4 * f2 - 5 * f1 + 2 * f0) / (delta * delta);
        }

        public double turunan2CDA1(double delta)
        {
            double f1 = fungsi(ans, xx + delta);
            double f0 = fungsi(ans, xx);
            double fmin1 = fungsi(ans, xx - delta);
            return (f1 - 2 * f0 + fmin1) / (delta * delta);
        }

        public double turunan2CDA2(double delta)
        {
            double f2 = fungsi(ans, xx + delta + delta);
            double f1 = fungsi(ans, xx + delta);
            double f0 = fungsi(ans, xx);
            double fmin1 = fungsi(ans, xx - delta);
            double fmin2 = fungsi(ans, xx - delta - delta);
            return (-1 * f2 + 16 * f1 - 30 * f0 + 16 * fmin1 - fmin2) / (12 * delta * delta);
        }

        public double turunan2BDA1(double delta)
        {
            double f0 = fungsi(ans, xx);
            double fmin1 = fungsi(ans, xx - delta);
            double fmin2 = fungsi(ans, xx - delta - delta);
            return (fmin2 - 2 * fmin1 + f0)/(delta * delta);
        }

        public double turunan2BDA2(double delta)
        {
            double f0 = fungsi(ans, xx);
            double fmin1 = fungsi(ans, xx - delta);
            double fmin2 = fungsi(ans, xx - delta - delta);
            double fmin3 = fungsi(ans, xx - delta - delta - delta);
            return (-1 * fmin3 + 4 * fmin2 - 5 * fmin1 + 2 * f0) / (delta * delta);
        }

        public double turunan3FDA(double delta)
        {
            double f3 = fungsi(ans, xx + delta + delta + delta);
            double f2 = fungsi(ans, xx + delta + delta);
            double f1 = fungsi(ans, xx + delta);
            double f0 = fungsi(ans, xx);
            return (f3 - 3 * f2 + 3 * f1 - f0) / (delta * delta * delta);
        }

        public double turunan3CDA(double delta)
        {
            double f2 = fungsi(ans, xx + delta + delta);
            double f1 = fungsi(ans, xx + delta);
            double fmin1 = fungsi(ans, xx - delta);
            double fmin2 = fungsi(ans, xx - delta - delta);
            return (f2 - 2 * f1 + 2 * fmin1 - fmin2) / (2 * delta * delta * delta);
        }

        public double turunan3BDA(double delta)
        {
            double f0 = fungsi(ans, xx);
            double fmin1 = fungsi(ans, xx - delta);
            double fmin2 = fungsi(ans, xx - delta - delta);
            double fmin3 = fungsi(ans, xx - delta - delta - delta);
            return (-1*fmin3 + 3*fmin2 -3 * fmin1 + f0)/(delta*delta*delta);
        }

        public double turunan4FDA(double delta)
        {
            double f4 = fungsi(ans, xx + delta + delta + delta + delta);
            double f3 = fungsi(ans, xx + delta + delta + delta);
            double f2 = fungsi(ans, xx + delta + delta);
            double f1 = fungsi(ans, xx + delta);
            double f0 = fungsi(ans, xx);
            return (f4 - 4 * f3 + 6 * f2 - 4 * f1 + f0) / (delta * delta * delta * delta);
        }

        public double turunan4CDA(double delta)
        {
            double f2 = fungsi(ans, xx + delta + delta);
            double f1 = fungsi(ans, xx + delta);
            double f0 = fungsi(ans, xx);
            double fmin1 = fungsi(ans, xx - delta);
            double fmin2 = fungsi(ans, xx - delta - delta);
            return (f2 - 4 * f1 + 6 * f0 - 4 * fmin1 + fmin2) / (delta * delta * delta * delta);
        }

        public double turunan4BDA(double delta)
        {
            double f0 = fungsi(ans, xx);
            double fmin1 = fungsi(ans, xx - delta);
            double fmin2 = fungsi(ans, xx - delta - delta);
            double fmin3 = fungsi(ans, xx - delta - delta - delta);
            double fmin4 = fungsi(ans, xx - delta - delta - delta - delta);
            return (fmin4 - 4 * fmin3 + 6 * fmin2 - 4 * fmin1 + f0) / (delta * delta * delta * delta);
        }
    }
}
