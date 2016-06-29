using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project_MetodeNumerik
{
    public partial class Form8 : Form
    {
        double xx;
        int n, cacah, cacah2, n_temp;
        double[] x = new double[11];
        double[] y = new double[11];
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult pilihan = MessageBox.Show("Apakah Anda yakin untuk keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pilihan == DialogResult.Yes)
                Application.ExitThread();
            else
                e.Cancel = true;
        }

        public void keArray(string teks)
        {
            teks = teks.Trim();
            string kiri = "", kanan = "";
            for (int i = 0; i < teks.Length; i++)
            {
                if (teks[i] == ' ')
                {
                    for (int j = 0; j < i; j++)
                        kiri += teks[j].ToString();
                    for (int j = i + 1; j < teks.Length; j++)
                    {
                        if (teks[j] == ' ') continue;
                        else
                            kanan += teks[j].ToString();
                    }
                    break;
                }
            }
            x[cacah] = Convert.ToDouble(kiri);
            y[cacah] = Convert.ToDouble(kanan);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(comboBox1.Text);
            n = i;
            label13.Visible = true; textBox1.Visible = true;
            label4.Visible = true; textBox2.Visible = true;
            if (i > 2)
            { label5.Visible = true; textBox3.Visible = true; }
            if (i > 3)
            { label6.Visible = true; textBox4.Visible = true; }
            if (i > 4)
            { label7.Visible = true; textBox5.Visible = true; }
            if (i > 5)
            { label8.Visible = true; textBox6.Visible = true; }
            if (i > 6)
            { label9.Visible = true; textBox7.Visible = true; }
            if (i > 7)
            { label10.Visible = true; textBox8.Visible = true; }
            if (i > 8)
            { label11.Visible = true; textBox9.Visible = true; }
            if (i > 9)
            { label12.Visible = true; textBox10.Visible = true; }

            label3.Visible = true; label14.Visible = true; textBox11.Visible = true;

            button1.Enabled = false;
        }

        public static double[,] Langrange(List<double> arr, List<double> arr2, int n, double xx)
        {
            double[,] array = new double[10, 10];
            double sigma = 0.0;
            int counter = 0;
            double atas;
            List<double> a = new List<double>();
            List<double> b = new List<double>();
            List<double> c = new List<double>();
            for (int i = 0; i < n; i++)
            {
                int htg = 0;
                a.Clear();
                double bawah = 1;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        bawah *= (arr[i] - arr[j]);
                        if (htg == 0) { a.Add(1); a.Add(-1 * arr[j]); }
                        else
                        {
                            c.Clear(); b.Clear();
                            c.Add(1); c.Add(-1 * arr[j]);
                            //Console.Write(a.Count);
                            for (int q = 0; q < a.Count; q++)
                            {

                                for (int r = 0; r < c.Count; r++)
                                {

                                    b.Add(a[q] * c[r]);
                                }
                            }

                            //Console.WriteLine();
                            if (b.Count != 0)
                            {
                                a.Clear();
                                a.Add(1);
                                for (int q = 1; q < b.Count - 1; q += 2)
                                {
                                    a.Add(b[q] + b[q + 1]);
                                }
                                a.Add(b[b.Count - 1]);
                            }

                        }
                        htg++;
                    }
                }

                int z;
                for (z = 0; z < a.Count; z++)
                    array[counter, z] = a[z];
                array[counter, z] = bawah;
                counter++;
            }
            return array;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            cacah = 0;
            listBox1.Items.Clear();
            bool sah = true;
            try
            {
                if (n > 1)//2
                {
                    keArray(textBox1.Text); cacah++;
                    keArray(textBox2.Text); cacah++;
                }
                if (n > 2)//3
                {
                    keArray(textBox3.Text); cacah++;
                }
                if (n > 3)//4
                { keArray(textBox4.Text); cacah++; }
                if (n > 4)//5
                { keArray(textBox5.Text); cacah++; }
                if (n > 5)//6
                { keArray(textBox6.Text); cacah++; }
                if (n > 6)//7
                { keArray(textBox7.Text); cacah++; }
                if (n > 7)//8
                { keArray(textBox8.Text); cacah++; }
                if (n > 8)//9
                { keArray(textBox9.Text); cacah++; }
                if (n > 9)//10
                { keArray(textBox10.Text); cacah++; }

                xx = Convert.ToDouble(textBox11.Text);
            }
            catch
            { MessageBox.Show("Data tidak Valid!!"); sah = false; }

            if (sah)
            {
                int batas = -1, kemungkinan = 1;
                List<double> arr = new List<double>();
                List<double> arr2 = new List<double>();
                double[,] terbaik = new double[11, 11];
                double error_terbaik = Double.MaxValue;
                int c = 1;
                for (int i = 2; i <= n - 1; i++)
                {
                    for (int j = 0; j <= n - i; j++)
                    {
                        arr.Clear();
                        arr2.Clear();
                        //Console.WriteLine("\n\nKemungkinan {0}, titik yang dipilih :", c++);
                        string s = String.Format("\n\nKemungkinan {0}, titik yang dipilih :", c++);
                        listBox1.Items.Add(s);
                        for (int k = j; k < j + i; k++)
                        {
                            arr.Add(x[k]); arr2.Add(y[k]);
                            //Console.Write("x = {0}, y = {1};   ", x[k], y[k]);
                            s = String.Format("x = {0}, y = {1};   ", x[k], y[k]);
                            listBox1.Items.Add(s);
                        }
                        listBox1.Items.Add("");

                        double[,] koef = Langrange(arr, arr2, i, 2);
                        double total_error = 0.0;
                        //Console.WriteLine("{0,-20}{1,-20}{2,-20}", "y interpolasi", "y sebenarnya", "Error");
                        s = String.Format("{0,-20}{1,-20}{2,-20}", "y interpolasi", "y sebenarnya", "Error");
                        listBox1.Items.Add(s);
                        s = "";
                        for (int z = 0; z < 60; z++) s += "=";
                        //Console.WriteLine();
                        listBox1.Items.Add(s);
                        for (int q = 0; q < x.Length; q++)
                        {
                            double sigma1 = 0.0;
                            int d = 0;

                            for (int w = 0; w < i; w++)
                            {
                                double sigma2 = 0.0;
                                int pangkat = i - 1;
                                for (int z = 0; z < i; z++)
                                {
                                    sigma2 += (koef[w, z] * Math.Pow(x[q], pangkat));
                                    pangkat--;
                                    //Console.Write("{0} ",koef[w,z]);
                                }
                                //
                                sigma2 *= y[d++];

                                sigma2 /= koef[w, i];
                                sigma1 += sigma2;
                            }
                            total_error += Math.Abs(sigma1 - y[q]);
                            //Console.WriteLine("{0,-20}{1,-20}{2,-20}", sigma1, y[q], Math.Abs(sigma1 - y[q]));
                            s = String.Format("{0,-20}{1,-20}{2,-20}", Math.Round(sigma1,5), Math.Round(y[q],5), Math.Round(Math.Abs(sigma1 - y[q]),5));
                            listBox1.Items.Add(s);
                        }
                        for (int z = 0; z < 60; z++) Console.Write("=");
                        //Console.WriteLine("\nTotal Error = {0}", total_error);
                        listBox1.Items.Add("");
                        s = String.Format("\nTotal Error = {0}", total_error);
                        listBox1.Items.Add(s);
                        listBox1.Items.Add(""); listBox1.Items.Add("");
                        if (total_error < error_terbaik)
                        {
                            error_terbaik = total_error;
                            terbaik = koef;
                            batas = i + 1;
                            kemungkinan = c - 1;
                        }
                    }
                }
                //Console.WriteLine("\n\nJadi,Error terkecil = {0} (kemungkinan {1})", error_terbaik, kemungkinan);
                string st = String.Format("Jadi,Error terkecil = {0} (kemungkinan {1})", error_terbaik, kemungkinan);
                listBox1.Items.Add(st);
                double sg1 = 0.0;
                int f = 0;
                for (int w = 0; w < batas - 1; w++)
                {
                    double sigma2 = 0.0;
                    int pangkat = batas - 2;
                    for (int z = 0; z < batas - 1; z++)
                    {
                        sigma2 += (terbaik[w, z] * Math.Pow(xx, pangkat));
                        pangkat--;
                    }
                    sigma2 *= y[f++];

                    sigma2 /= terbaik[w, batas - 1];
                    sg1 += sigma2;
                }
                //Console.WriteLine("Hasil = {0}", sg1);
                st = String.Format("Hasil = {0}", sg1);
                listBox1.Items.Add("");
                listBox1.Items.Add(st);
                label15.Text = st;
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(comboBox2.Text);
            n = i;
            label26.Visible = true; textBox22.Visible = true;
            label25.Visible = true; textBox21.Visible = true;
            if (i > 2)
            { label24.Visible = true; textBox20.Visible = true; }
            if (i > 3)
            { label23.Visible = true; textBox19.Visible = true; }
            if (i > 4)
            { label22.Visible = true; textBox18.Visible = true; }
            if (i > 5)
            { label21.Visible = true; textBox17.Visible = true; }
            if (i > 6)
            { label20.Visible = true; textBox16.Visible = true; }
            if (i > 7)
            { label19.Visible = true; textBox15.Visible = true; }
            if (i > 8)
            { label18.Visible = true; textBox14.Visible = true; }
            if (i > 9)
            { label17.Visible = true; textBox13.Visible = true; }

            label27.Visible = true; label16.Visible = true; textBox12.Visible = true;
            button5.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cacah = 0;
            listBox1.Items.Clear();
            bool sah = true;
            try
            {
                if (n > 1)//2
                {
                    keArray(textBox22.Text); cacah++;
                    keArray(textBox21.Text); cacah++;
                }
                if (n > 2)//3
                {
                    keArray(textBox20.Text); cacah++;
                }
                if (n > 3)//4
                { keArray(textBox19.Text); cacah++; }
                if (n > 4)//5
                { keArray(textBox18.Text); cacah++; }
                if (n > 5)//6
                { keArray(textBox17.Text); cacah++; }
                if (n > 6)//7
                { keArray(textBox16.Text); cacah++; }
                if (n > 7)//8
                { keArray(textBox15.Text); cacah++; }
                if (n > 8)//9
                { keArray(textBox14.Text); cacah++; }
                if (n > 9)//10
                { keArray(textBox13.Text); cacah++; }

                xx = Convert.ToDouble(textBox12.Text);
            }
            catch
            { MessageBox.Show("Data tidak Valid!!"); sah = false; }

            if (sah)
            {
                double hasil = 0;
                for (int i = 0; i < n - 1; i++)
                {
                    if (x[i] <= xx && x[i + 1] >= xx)
                    {
                        hasil = y[i] + ((y[i + 1] - y[i]) * (xx - x[i]) / (x[i + 1] - x[i]));
                        break;
                    }
                }
                string s = "Spline Linear";
                listBox2.Items.Add(s);
                s = "=============";
                listBox2.Items.Add(s);
                s = String.Format("Hasil = {0}\n", hasil);
                listBox2.Items.Add(s);
                listBox2.Items.Add("");
                s = "Spline Quadratic";
                listBox2.Items.Add(s);
                s = "================";
                listBox2.Items.Add(s);


                int n_total = 3 * n - 3;
                int n_sisa = (n - 2);
                double[,] arr = new double[n_total, n_total];
                int idx_geserangka = 0;
                int offset = 0;
                for (int i = 0; i < n_total - n_sisa - 1; i++)
                {
                    if (i % 2 == 1) idx_geserangka++;

                    arr[i, offset] = x[idx_geserangka] * x[idx_geserangka];
                    arr[i, offset + 1] = x[idx_geserangka];
                    arr[i, offset + 2] = 1;
                    if (i % 2 == 1) offset += 3;
                }
                idx_geserangka = 1;
                offset = 0;
                for (int i = n_total - n_sisa - 1; i < n_total - 1; i++)
                {
                    arr[i, offset] = 2 * x[idx_geserangka];
                    arr[i, offset + 1] = 1;
                    arr[i, offset + 3] = -1 * 2 * x[idx_geserangka];
                    arr[i, offset + 4] = -1;
                    idx_geserangka++;
                    offset += 3;
                }

                arr[n_total - 1, 0] = 1;

                //Console.WriteLine("\nFinal Set of Equations : \n");
                listBox2.Items.Add("");
                listBox2.Items.Add("Final Set of Equations : ");
                listBox2.Items.Add("");

                for (int i = 0; i < n_total; i++)
                {
                    s = "";
                    for (int j = 0; j < n_total; j++)
                        s += String.Format("{0,-8} ", arr[i, j]);
                        //Console.Write("{0,-8} ", arr[i, j]);
                    listBox2.Items.Add(s);
                }
                
                double[] koef = new double[n_total];
                double[] arrhasil = new double[n_total];
                idx_geserangka = 0;
                for (int i = 0; i < n_total - n_sisa - 1; i++)
                {
                    arrhasil[i] = y[idx_geserangka];
                    if (i % 2 == 0) idx_geserangka++;
                }
                koef[0] = 0;
                koef[1] = (arrhasil[0] - arrhasil[1]) / (arr[0, 1] - arr[1, 1]);
                koef[2] = (arrhasil[0] - (arr[0, 0] * koef[0]));
                koef[2] -= (arr[0, 1] * koef[1]);
                cacah = 3;
                offset = 3;
                int m = n_total - n_sisa - 1;
                for (int i = 2; i <= n_total - n_sisa - 3; i += 2)
                {
                    double p = arr[i, offset] - arr[i + 1, offset];
                    double q = arr[i, offset + 1] - arr[i + 1, offset + 1];
                    double r = arrhasil[i] - arrhasil[i + 1];

                    double pengali = -1 * q;
                    double ss = arr[m, offset] * pengali;
                    double u = ((arr[m, offset] * koef[offset - 3]) + (arr[m, offset + 1] * koef[offset - 2])) * pengali;

                    double a = (r - u) / (p - ss);
                    double b = (r - (p * a)) / q;
                    double c = (arrhasil[i] - arr[i, offset] * a - arr[i, offset + 1] * b);

                    koef[cacah] = Math.Round(a, 5);
                    koef[cacah + 1] = Math.Round(b, 5);
                    koef[cacah + 2] = Math.Round(c, 5);
                    //Console.WriteLine("{0} {1} {2}", koef[cacah], koef[cacah+1], koef[cacah+2]);
                    cacah += 3;
                    m++;
                    offset += 3;
                }

                //Console.WriteLine("\n\nKoefisien dari Spline :");
                listBox2.Items.Add(""); listBox2.Items.Add("");
                listBox2.Items.Add("Koefisien dari Spline :");
                listBox2.Items.Add("");
                s = String.Format("{0,-10}{1,-10}{2,-10}{3,-10}", "i", "ai", "bi", "ci");
                //Console.WriteLine("{0,-10}{1,-10}{2,-10}{3,-10}", "i", "ai", "bi", "ci");
                listBox2.Items.Add(s);
                s = "";
                for (int i = 0; i < 50; i++) s+="=";
                listBox2.Items.Add(s);
                int co = 1;
                for (int i = 0; i < n_total - 2; i += 3)
                {
                    //Console.WriteLine("{0,-10}{1,-10}{2,-10}{3,-10}", co++, koef[i], koef[i + 1], koef[i + 2]);
                    s = String.Format("{0,-10}{1,-10}{2,-10}{3,-10}", co++, koef[i], koef[i + 1], koef[i + 2]);
                    listBox2.Items.Add(s);
                }

                for (int i = 0; i < n - 1; i++)
                {
                    if (x[i] <= xx && x[i + 1] >= xx)
                    {
                        double a = koef[i * 3];
                        double b = koef[i * 3 + 1];
                        double c = koef[i * 3 + 2];

                        double hsl = a * xx * xx + b * xx + c;
                        //Console.WriteLine("\n\nf({0}) = {1}({2})^2  + ({3})({4}) + ({5})", xx, a, xx, b, xx, c);
                        listBox2.Items.Add("");
                        listBox2.Items.Add("");
                        s = String.Format("f({0}) = {1}({2})^2  + ({3})({4}) + ({5})", xx, a, xx, b, xx, c);
                        listBox2.Items.Add(s);
                        //Console.WriteLine("Hasil = {0}", hsl);
                        listBox2.Items.Add(String.Format("Hasil = {0}", hsl));
                        break;
                    }
                }
            }
        }
    }
}
