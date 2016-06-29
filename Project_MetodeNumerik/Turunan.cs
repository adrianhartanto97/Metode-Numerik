using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_MetodeNumerik
{
    class Turunan
    {
        private int N { set; get; }
        private double[] x;
        private double[] y;
        private double xx;
        private int pangkat { set; get; }
        private double delta { set; get; }
        private double[,] matriks;
        private double[,] matriks_temp;
        private double[] matriks_hasil;
        private double detA;
        private double[] ans;
        private double[] turunan1;
        private double[] turunan2;
        private double[] turunan3;
        private double[] turunan4;

        public Turunan(int n, int pangkat, double delta, double xx)
        {
            N = n;
            x = new double[n];
            y = new double[n];
            this.pangkat = pangkat;
            this.delta = delta;
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

        public List<string> tampilKoefRegresi()
        {
            List<string> str = new List<string>();
            for (int i = 0; i < ans.Length; i++)
            {
                string s = String.Format("a{0}  =  {1}", i, ans[i]);
                str.Add(s);
            }
            return str;
        }

        public List<string> tampilTabelRegresi()
        {
            List<string> arr = new List<string>();
            string str;
            double total_error = 0.0;
            str = String.Format("{0,-20}{1,-20}{2,-20}{3,-20}", "x", "y", "y'", "|y-y'|");
            arr.Add(str);
            str = "";
            for (int i = 0; i < 80; i++) str += "=";
            arr.Add(str);
            for (int i = 0; i < N; i++)
            {
                str = "";
                double selisih = Math.Abs(y[i] - fungsi(ans, x[i]));
                str += String.Format("{0,-20}{1,-20}{2,-20}{3,-20}", x[i], y[i], Math.Round(fungsi(ans, x[i]), 7), Math.Round(selisih, 8));
                total_error += selisih;
                arr.Add(str);
            }
            arr.Add("");
            str = String.Format("Total Error = {0:F20}", total_error);
            arr.Add(str);
            return arr;
        }

        public string tampilFungsiRegresi()
        {
            string str = "y' = ";
            for (int i = 0; i < ans.Length; i++)
            {
                str += ans[i].ToString();
                if (i >= 1) str += "x";
                if (i > 1)
                    str += "^" + i.ToString();
                if (i != ans.Length - 1) str += "  +  ";
            }
            return str;
        }

        public void generateTurunan()
        {
            turunan1 = new double[ans.Length - 1];
            for (int i = 1; i < ans.Length; i++)
            {
                turunan1[i - 1] = ans[i] * i;
            }

            turunan2 = new double[turunan1.Length - 1];
            for (int i = 1; i < turunan1.Length; i++)
            {
                turunan2[i - 1] = turunan1[i] * i;
            }

            string s = "";
            turunan3 = new double[turunan2.Length - 1];
            for (int i = 1; i < turunan2.Length; i++)
            {
                turunan3[i - 1] = turunan2[i] * i;
            }

            turunan4 = new double[turunan3.Length - 1];
            for (int i = 1; i < turunan3.Length; i++)
            {
                turunan4[i - 1] = turunan3[i] * i;
            }
        }

        public List<string> cetakEksak(string t)
        {
            string str;
            List<string> arr = new List<string>();
            str = "Solusi Eksak : ";
            arr.Add(str); arr.Add("");
            str = "f(x) = ";
            
            for (int i = 0; i < ans.Length; i++)
            {
                str += ans[i].ToString();
                if (i >= 1) str += "x";
                if (i > 1)
                    str += "^" + i.ToString();
                if (i != ans.Length - 1) str += "  +  ";
            }
            arr.Add(str);
            arr.Add("");

            double[] ans_baru;
            if (t == "pertama")
            { ans_baru = turunan1; str = "f'(x)"; }
            else if (t == "kedua")
            { ans_baru = turunan2; str = "f''(x)"; }
            else if (t == "ketiga")
            { ans_baru = turunan3; str = "f'''(x)"; }
            else
            { ans_baru = turunan4; str = "f''''(x)"; }
            str += " = ";
            for (int i = 0; i < ans_baru.Length; i++)
            {
                str += ans_baru[i].ToString();
                if (i >= 1) str += "x";
                if (i > 1)
                    str += "^" + i.ToString();
                if (i != ans_baru.Length - 1) str += "  +  ";
            }
            arr.Add(str);
            return arr;
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

        public double hasilturunan1(double xx)
        {
            return fungsi(turunan1, xx);
        }

        public double hasilturunan2(double xx)
        {
            return fungsi(turunan2, xx);
        }

        public double hasilturunan3(double xx)
        {
            return fungsi(turunan3, xx);
        }

        public double hasilturunan4(double xx)
        {
            return fungsi(turunan4, xx);
        }

        public List<string> turunan1FDA()
        {
            List<string> arr = new List<string>();
            double eksak = hasilturunan1(xx);
            string str;
            arr.Add("Metode FDA (cara I) : ");
            arr.Add(" ");
            double f0 = fungsi(ans, xx);
            str = String.Format("f({0}) = {1}", xx, f0);
            arr.Add(str);
            double f1 = fungsi(ans, xx + delta);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            str = String.Format("FDA : f'({0}) = ({1}  -  {2}) / {3}", xx, f1, f0, delta);
            arr.Add(str);
            double hasil_fda = (f1 - f0) / delta;
            str = String.Format("          f'({0}) = {1}", xx, hasil_fda);
            arr.Add(str); arr.Add("");
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_fda, eksak, Math.Abs((eksak - hasil_fda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");

            arr.Add("Metode FDA (cara II) : ");
            arr.Add("");
            str = String.Format("f({0}) = {1}", xx, f0);
            arr.Add(str);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            double f2 = fungsi(ans, xx + delta + delta);
            str = String.Format("f({0} + 2 * delta x) = {1}", xx, f2);
            arr.Add(str);

            hasil_fda = (f2 * -1 + 4 * f1 - 3 * f0) / (2*delta);
            str = String.Format("FDA : f'(x)  = ( -1 * {0} + 4 * {1} - 3 * {2} ) / 2*delta",f2,f1,f0);
            arr.Add(str);
            str = String.Format("          f'({0}) = {1}", xx, hasil_fda);
            arr.Add(str); arr.Add("");
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_fda, eksak, Math.Abs((eksak - hasil_fda) * 100 / eksak));
            arr.Add(str);

            return arr;
        }

        public List<string> turunan1BDA()
        {
            List<string> arr = new List<string>();
            double eksak = hasilturunan1(xx);
            string str;
            arr.Add("Metode BDA (cara I) : ");
            arr.Add(" ");
            double f0 = fungsi(ans, xx);
            str = String.Format("f({0}) = {1}", xx, f0);
            arr.Add(str);

            double fmin1 = fungsi(ans, xx - delta);
            str = String.Format("f({0} - delta x) = {1}", xx, fmin1);
            arr.Add(str);
            str = String.Format("BDA : f'({0}) = ({1}  -  {2}) / {3}", xx, f0, fmin1, delta);
            arr.Add(str);
            double hasil_bda = (f0 - fmin1) / delta;
            str = String.Format("          f'({0}) = {1}", xx, hasil_bda);
            arr.Add(str);
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_bda, eksak, Math.Abs((eksak - hasil_bda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");

            arr.Add("Metode BDA (cara II) : ");
            arr.Add(" ");
            str = String.Format("f({0}) = {1}", xx, f0);
            arr.Add(str);
            str = String.Format("f({0} - delta x) = {1}", xx, fmin1);
            arr.Add(str);
            double fmin2 = fungsi(ans, xx - delta - delta);
            str = String.Format("f({0} - delta x - delta x) = {1}", xx, fmin2);
            arr.Add(str);
            hasil_bda = (fmin2  - 4 * fmin1 + 3 * f0) / (2 * delta);

            str = String.Format("BDA : f'(x)  = ( {0} - 4 * {1} + 3 * {2} ) / 2*delta", fmin2, fmin1, f0);
            arr.Add(str);
            str = String.Format("          f'({0}) = {1}", xx, hasil_bda);
            arr.Add(str); arr.Add("");
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_bda, eksak, Math.Abs((eksak - hasil_bda) * 100 / eksak));
            arr.Add(str);

            return arr;
        }

        public List<string> turunan1CDA()
        {
            List<string> arr = new List<string>();
            double eksak = hasilturunan1(xx);
            string str;
            arr.Add("Metode CDA (cara I) : ");
            arr.Add(" ");

            double f1 = fungsi(ans, xx + delta);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            double fmin1 = fungsi(ans, xx - delta);
            str = String.Format("f({0} - delta x) = {1}", xx, fmin1);
            arr.Add(str);

            str = String.Format("CDA : f'({0}) = ({1}  -  {2}) / {3}", xx, f1, fmin1, 2 * delta);
            arr.Add(str);
            double hasil_cda = (f1 - fmin1) / (delta * 2);
            str = String.Format("          f'({0}) = {1}", xx, hasil_cda);
            arr.Add(str);
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_cda, eksak, Math.Abs((eksak - hasil_cda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");

            arr.Add("Metode CDA (cara II) : ");
            arr.Add(" ");
            double f2 = fungsi(ans, xx + delta + delta);
            str = String.Format("f({0} + 2 * delta x) = {1}", xx, f2);
            arr.Add(str);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            str = String.Format("f({0} - delta x) = {1}", xx, fmin1);
            arr.Add(str);
            double fmin2 = fungsi(ans, xx - delta - delta);
            str = String.Format("f({0} - 2 * delta x) = {1}", xx, fmin2);
            arr.Add(str);

            str = String.Format("CDA : f'({0}) = (-1 * {1} + 8 * {2} - 8 * {3} + {4}) / {5}", xx, f2,f1,fmin1,fmin2,12*delta);
            arr.Add(str);
            hasil_cda = (-1 * f2 + 8*f1 - 8*(fmin1) + fmin2)/(12*delta);
            str = String.Format("          f'({0}) = {1}", xx, hasil_cda);
            arr.Add(str);
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_cda, eksak, Math.Abs((eksak - hasil_cda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");

            return arr;
        }

        public List<string> turunan2FDA()
        {
            List<string> arr = new List<string>();
            double eksak = hasilturunan2(xx);
            string str;
            arr.Add("Metode FDA (cara I) : ");
            arr.Add(" ");
            double f0 = fungsi(ans, xx);
            str = String.Format("f({0}) = {1}", xx, f0);
            arr.Add(str);
            double f1 = fungsi(ans, xx + delta);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            double f2 = fungsi(ans, xx+delta+delta);
            str = String.Format("f({0} + 2*delta x) = {1}", xx, f2);
            arr.Add(str);

            str = String.Format("FDA : f''({0}) = {1} - 2 * {2} + {3} / {4}^2", xx, f2, f1, f0, delta);
            arr.Add(str);
            double hasil_fda = (f2 - 2*f1 + f0)/(delta*delta);
            str = String.Format("          f''({0}) = {1}", xx, hasil_fda);
            arr.Add(str); arr.Add("");
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_fda, eksak, Math.Abs((eksak - hasil_fda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");

            arr.Add("Metode FDA (cara II) : ");
            arr.Add(" ");
            str = String.Format("f({0}) = {1}", xx, f0);
            arr.Add(str);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            str = String.Format("f({0} + 2*delta x) = {1}", xx, f2);
            arr.Add(str);
            double f3 = fungsi(ans, xx + delta + delta + delta);
            str = String.Format("f({0} + 3*delta x) = {1}", xx, f3);
            arr.Add(str);

            str = String.Format("FDA : f''({0}) = -1 * {1} + 4 * {2} - 5 * {3} + 2 * {4} / {5}^2", xx,f3, f2, f1, f0, delta);
            arr.Add(str);
            hasil_fda = (-1*f3 + 4*f2 - 5*f1 + 2*f0) / (delta * delta);
            str = String.Format("          f''({0}) = {1}", xx, hasil_fda);
            arr.Add(str); arr.Add("");
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_fda, eksak, Math.Abs((eksak - hasil_fda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");
            return arr;
        }

        public List<string> turunan2CDA()
        {
            List<string> arr = new List<string>();
            double eksak = hasilturunan2(xx);
            string str;
            arr.Add("Metode CDA (cara I) : ");
            arr.Add(" ");
            double f1 = fungsi(ans, xx + delta);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            double f0 = fungsi(ans, xx);
            str = String.Format("f({0}) = {1}", xx, f0);
            arr.Add(str);
            double fmin1 = fungsi(ans, xx - delta);
            str = String.Format("f({0} - delta x) = {1}", xx, fmin1);
            arr.Add(str);

            str = String.Format("CDA : f''({0}) = {1} - 2 * {2} + {3} / {4}^2", xx, f1, f0, fmin1, delta);
            arr.Add(str);
            double hasil_cda = (f1 - 2 * f0 + fmin1) / (delta * delta);
            str = String.Format("          f''({0}) = {1}", xx, hasil_cda);
            arr.Add(str); arr.Add("");
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_cda, eksak, Math.Abs((eksak - hasil_cda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");

            arr.Add("Metode CDA (cara II) : ");
            arr.Add(" ");
            double f2 = fungsi(ans, xx + delta + delta);
            str = String.Format("f({0} + 2*delta x) = {1}", xx, f2);
            arr.Add(str);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            str = String.Format("f({0}) = {1}", xx, f0);
            arr.Add(str);
            str = String.Format("f({0} - delta x) = {1}", xx, fmin1);
            arr.Add(str);
            double fmin2 = fungsi(ans, xx - delta - delta);
            str = String.Format("f({0} - 2*delta x) = {1}", xx, fmin2);
            arr.Add(str);

            str = String.Format("CDA : f''({0}) = -1 * {1} + 16 * {2} - 30 * {3} - 16 * {4} - {5} / 12 * {6}^2", xx, f2, f1, f0, fmin1, fmin2, delta);
            arr.Add(str);
            hasil_cda = (-1*f2 + 16*f1 -30*f0 + 16*fmin1 - fmin2 ) / (12 * delta * delta);
            str = String.Format("          f''({0}) = {1}", xx, hasil_cda);
            arr.Add(str); arr.Add("");
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_cda, eksak, Math.Abs((eksak - hasil_cda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");

            return arr;
        }

        public List<string> turunan3FDA()
        {
            List<string> arr = new List<string>();
            double eksak = hasilturunan3(xx);
            string str;
            arr.Add("Metode FDA (cara I) : ");
            arr.Add(" ");
            double f3 = fungsi(ans, xx + delta + delta + delta);
            str = String.Format("f({0} + 3*delta x) = {1}", xx, f3);
            arr.Add(str);
            double f2 = fungsi(ans, xx + delta + delta);
            str = String.Format("f({0} + 2*delta x) = {1}", xx, f2);
            arr.Add(str);
            double f1 = fungsi(ans, xx + delta);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            double f0 = fungsi(ans, xx);
            str = String.Format("f({0}) = {1}", xx, f0);
            arr.Add(str);

            str = String.Format("FDA : f'''({0}) = {1} - 3 * {2} + 3 * {3} - {4} / {5}^3", xx, f3,f2,f1,f0,delta);
            arr.Add(str);
            double hasil_fda = (f3 - 3*f2 + 3*f1 -f0) / (delta * delta*delta);
            str = String.Format("          f'''({0}) = {1}", xx, hasil_fda);
            arr.Add(str); arr.Add("");
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_fda, eksak, Math.Abs((eksak - hasil_fda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");
            return arr;

        }

        public List<string> turunan3CDA()
        {
            List<string> arr = new List<string>();
            double eksak = hasilturunan3(xx);
            string str;
            arr.Add("Metode CDA (cara I) : ");
            arr.Add(" ");
            
            double f2 = fungsi(ans, xx + delta + delta);
            str = String.Format("f({0} + 2*delta x) = {1}", xx, f2);
            arr.Add(str);
            double f1 = fungsi(ans, xx + delta);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            double fmin1 = fungsi(ans, xx - delta);
            str = String.Format("f({0} - delta x) = {1}", xx, fmin1);
            arr.Add(str);
            double fmin2 = fungsi(ans, xx - delta - delta);
            str = String.Format("f({0} - 2*delta x) = {1}", xx, fmin2);
            arr.Add(str);

            str = String.Format("CDA : f'''({0}) = {1} -2 * {2} + 2 * {3} - {4} / 2 * {5}^3", xx, f2, f1, fmin1, fmin2, delta);
            arr.Add(str);
            double hasil_cda = (f2 - 2*f1 + 2*fmin1 - fmin2) / (2 * delta * delta * delta);
            str = String.Format("          f'''({0}) = {1}", xx, hasil_cda);
            arr.Add(str); arr.Add("");
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_cda, eksak, Math.Abs((eksak - hasil_cda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");
            return arr;
        }

        public List<string> turunan4FDA()
        {
            List<string> arr = new List<string>();
            double eksak = hasilturunan4(xx);
            string str;
            arr.Add("Metode FDA (cara I) : ");
            arr.Add(" ");

            double f4 = fungsi(ans, xx + delta + delta + delta + delta);
            str = String.Format("f({0} + 4*delta x) = {1}", xx, f4);
            arr.Add(str);
            double f3 = fungsi(ans, xx + delta + delta + delta);
            str = String.Format("f({0} + 3*delta x) = {1}", xx, f3);
            arr.Add(str);
            double f2 = fungsi(ans, xx + delta + delta);
            str = String.Format("f({0} + 2*delta x) = {1}", xx, f2);
            arr.Add(str);
            double f1 = fungsi(ans, xx + delta);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            double f0 = fungsi(ans, xx);
            str = String.Format("f({0}) = {1}", xx, f0);
            arr.Add(str);

            str = String.Format("FDA : f''''({0}) = {1} - 4 * {2} + 6 * {3} - 4 * {4} + {5} / {6}^4", xx, f4, f3, f2, f1, f0, delta);
            arr.Add(str);
            double hasil_fda = (f4 - 4*f3 + 6*f2 - 4*f1 + f0) / (delta * delta * delta*delta);
            str = String.Format("          f''''({0}) = {1}", xx, hasil_fda);
            arr.Add(str); arr.Add("");
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_fda, eksak, Math.Abs((eksak - hasil_fda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");
            return arr;

            return arr;
        }

        public List<string> turunan4CDA()
        {
            List<string> arr = new List<string>();
            double eksak = hasilturunan4(xx);
            string str;
            arr.Add("Metode CDA (cara I) : ");
            arr.Add(" ");
            double f2 = fungsi(ans, xx + delta + delta);
            str = String.Format("f({0} + 2*delta x) = {1}", xx, f2);
            arr.Add(str);
            double f1 = fungsi(ans, xx + delta);
            str = String.Format("f({0} + delta x) = {1}", xx, f1);
            arr.Add(str);
            double f0 = fungsi(ans, xx);
            str = String.Format("f({0}) = {1}", xx, f0);
            arr.Add(str);
            double fmin1 = fungsi(ans, xx - delta);
            str = String.Format("f({0} - delta x) = {1}", xx, fmin1);
            arr.Add(str);
            double fmin2 = fungsi(ans, xx - delta - delta);
            str = String.Format("f({0} - 2*delta x) = {1}", xx, fmin2);
            arr.Add(str);

            str = String.Format("CDA : f''''({0}) = {1} - 4 * {2} + 6 * {3} - 4 * {4} + {5} / {6}^4", xx, f2, f1, f0, fmin1, fmin2, delta);
            arr.Add(str);
            double hasil_cda = (f2 - 4*f1 + 6*f0 -4*fmin1 + fmin2) / (delta*delta*delta * delta);
            str = String.Format("          f''''({0}) = {1}", xx, hasil_cda);
            arr.Add(str); arr.Add("");
            str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_cda, eksak, Math.Abs((eksak - hasil_cda) * 100 / eksak));
            arr.Add(str); arr.Add(""); arr.Add("");


            return arr;
        }
    }
}
