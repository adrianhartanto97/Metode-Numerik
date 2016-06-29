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
    public partial class Form10 : Form
    {
        int n, cacah, pangkat = 1;
        double delta = 1.0;
        double[] x = new double[11];
        double[] y = new double[11];
        double xx;
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult pilihan = MessageBox.Show("Apakah Anda yakin untuk keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pilihan == DialogResult.Yes)
                Application.ExitThread();
            else
                e.Cancel = true;
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            /*
            textBox1.Text = "0.89   1.06";
            textBox2.Text = "1.27   2.33";
            textBox3.Text = "2.56   3.88";
            textBox4.Text = "3.98   4.77";
            textBox5.Text = "4.87   5.02";
            textBox6.Text = "5.31   6.21";
            textBox7.Text = "6.54   7.17";
            textBox8.Text = "7.58   8.66";
            textBox12.Text = "2"; textBox11.Text = "10.2"; textBox13.Text = "4";
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Dispose();
            f1.Show();
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
        private void button1_Click(object sender, EventArgs e)
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
            label15.Visible = true; textBox12.Visible = true;
            label16.Visible = true; textBox13.Visible = true;

            button1.Enabled = false;
        }
        static double jumlah(double[] x, int pangkat)
        {
            double t = 0.0;
            for (int i = 0; i < x.Length; i++)
                t += (Math.Pow(x[i], pangkat));
            return t;
        }

        static double sigmahasil(double[] x, double[] y, int pangkat)
        {
            double t = 0.0;
            for (int i = 0; i < x.Length; i++)
                t += (Math.Pow(x[i], pangkat)) * (y[i]);
            return t;
        }

        static double det(double[,] arr, int uk)
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

        static void cetak_v1(ListBox list, double[,] arr, int n, double[] hasil)
        {
            string s;
            for (int i = 0; i < n; i++)
            {
                s = "";
                for (int j = 0; j < n; j++)
                {
                    s += String.Format("{0,-15:F2}", arr[i, j]);
                }
                s += String.Format("     {0:F2}", hasil[i]);
                list.Items.Add(s);
            }
        }

        static void cetak_v2(ListBox list, double[,] arr, int n)
        {
            string s;
            for (int i = 0; i < n; i++)
            {
                s = "";
                for (int j = 0; j < n; j++)
                    s += String.Format("{0,-15:F2}", arr[i, j]);
                list.Items.Add(s);
            }
        }

        static double fungsi(double[] ans, double x)
        {
            double t = 0.0;
            for (int i = 0; i < ans.Length; i++)
            {
                t += (ans[i] * Math.Pow(x, i));
            }
            return t;
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
                pangkat = Convert.ToInt16(textBox12.Text);
                delta = Convert.ToDouble(textBox13.Text);
            }
            catch
            { MessageBox.Show("Data tidak Valid!!"); sah = false; }

            if (sah)
            {
                double[,] matriks = new double[pangkat + 1, pangkat + 1];
                double[,] matriks_temp = new double[pangkat + 1, pangkat + 1];
                double[] matriks_hasil = new double[pangkat + 1];
                matriks[0, 0] = n;
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
                //listBox1.Items.Add("Cetak Matriks A");
                //listBox1.Items.Add("");
                //cetak_v1(listBox1, matriks, pangkat + 1, matriks_hasil);
                double detA = det(matriks, pangkat + 1);
                //Console.WriteLine(detA);
                //listBox1.Items.Add(String.Format("Determinan A = {0}", detA));
                //listBox1.Items.Add(""); listBox1.Items.Add("");
                double[] ans = new double[pangkat + 1];
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
                    //cetak_v2(listBox1, matriks_temp, pangkat + 1);
                   // listBox1.Items.Add("");
                    //listBox1.Items.Add(String.Format("Determinan Ax{0} = {1}", i, det_ini));
                    //listBox1.Items.Add(""); listBox1.Items.Add("");
                    //string s = String.Format("a{0}  =  {1}", i, ans[i]);
                    //listBox1.Items.Add(s);
                }
                for (int i = 0; i < ans.Length; i++)
                {
                    string s = String.Format("a{0}  =  {1}", i, ans[i]);
                    listBox1.Items.Add(s);
                }
                listBox1.Items.Add(""); 
                string str = "y' = ";
                for (int i = 0; i < ans.Length; i++)
                {
                    str += ans[i].ToString();
                    if (i >= 1) str += "x";
                    if (i > 1)
                        str += "^" + i.ToString();
                    if (i != ans.Length - 1) str += "  +  ";
                }
                double total_error = 0.0;
                listBox1.Items.Add("");
                listBox1.Items.Add(str);
                listBox1.Items.Add("");
                str = String.Format("{0,-20}{1,-20}{2,-20}{3,-20}", "x", "y", "y'", "|y-y'|");
                listBox1.Items.Add(str);
                str = "";
                for (int i = 0; i < 80; i++) str += "=";
                listBox1.Items.Add(str);
                for (int i = 0; i < n; i++)
                {
                    str = "";
                    double selisih = Math.Abs(y[i] - fungsi(ans, x[i]));
                    str += String.Format("{0,-20}{1,-20}{2,-20}{3,-20}", x[i], y[i], Math.Round(fungsi(ans, x[i]), 7), Math.Round(selisih, 8));
                    total_error += selisih;
                    listBox1.Items.Add(str);
                }
                listBox1.Items.Add("");
                str = String.Format("Total Error = {0:F20}", total_error);
                listBox1.Items.Add(str); listBox1.Items.Add("");

                double[] ans_baru = new double[ans.Length - 1];
                for (int i = 1; i < ans.Length; i++)
                {
                    ans_baru[i - 1] = ans[i] * i;
                }
                str = "f(x) = ";
                for (int i = 0; i < ans.Length; i++)
                {
                    str += ans[i].ToString();
                    if (i >= 1) str += "x";
                    if (i > 1)
                        str += "^" + i.ToString();
                    if (i != ans.Length - 1) str += "  +  ";
                }
                listBox1.Items.Add(str);
                str = "f'(x) = ";
                for (int i = 0; i < ans_baru.Length; i++)
                {
                    str += ans_baru[i].ToString();
                    if (i >= 1) str += "x";
                    if (i > 1)
                        str += "^" + i.ToString();
                    if (i != ans_baru.Length - 1) str += "  +  ";
                }
                listBox1.Items.Add(str);
                double eksak = fungsi(ans_baru, xx);
                str = String.Format("Nilai eksak untuk f'({0}) = {1}", xx, eksak);
                listBox1.Items.Add(str); listBox1.Items.Add("");
                listBox1.Items.Add("Metode FDA : ");
                double sekarang = fungsi(ans, xx);
                str = String.Format("f({0}) = {1}",xx,sekarang );
                listBox1.Items.Add(str);
                double maju = fungsi(ans, xx + delta);
                str = String.Format("f({0} + delta x) = {1}",xx, maju);
                listBox1.Items.Add(str);
                str = String.Format("FDA : f'({0}) = ({1}  -  {2}) / {3}",xx,maju,sekarang,delta);
                listBox1.Items.Add(str);
                double hasil_fda = (maju-sekarang)/delta;
                str = String.Format("          f'({0}) = {1}", xx, hasil_fda);
                listBox1.Items.Add(str);
                str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %",eksak,hasil_fda,eksak,Math.Abs((eksak-hasil_fda)*100/eksak));
                listBox1.Items.Add(str); listBox1.Items.Add("");

                listBox1.Items.Add("Metode BDA : ");
                sekarang = fungsi(ans, xx);
                str = String.Format("f({0}) = {1}", xx, sekarang);
                listBox1.Items.Add(str);
                double mundur = fungsi(ans, xx - delta);
                str = String.Format("f({0} - delta x) = {1}", xx, mundur);
                listBox1.Items.Add(str);
                str = String.Format("BDA : f'({0}) = ({1}  -  {2}) / {3}", xx, sekarang, mundur, delta);
                listBox1.Items.Add(str);
                double hasil_bda = (sekarang-mundur) / delta;
                str = String.Format("          f'({0}) = {1}", xx, hasil_bda);
                listBox1.Items.Add(str);
                str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_bda, eksak, Math.Abs((eksak - hasil_bda) * 100 / eksak));
                listBox1.Items.Add(str); listBox1.Items.Add("");

                listBox1.Items.Add("Metode CDA : ");
                maju = fungsi(ans, xx + delta);
                str = String.Format("f({0} + delta x) = {1}", xx, maju);
                listBox1.Items.Add(str);
                mundur = fungsi(ans, xx - delta);
                str = String.Format("f({0} - delta x) = {1}", xx, mundur);
                listBox1.Items.Add(str);
                str = String.Format("CDA : f'({0}) = ({1}  -  {2}) / {3}", xx, maju, mundur, 2*delta);
                listBox1.Items.Add(str);
                double hasil_cda = (maju - mundur) / (delta*2);
                str = String.Format("          f'({0}) = {1}", xx, hasil_cda);
                listBox1.Items.Add(str);
                str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", eksak, hasil_cda, eksak, Math.Abs((eksak - hasil_cda) * 100 / eksak));
                listBox1.Items.Add(str); listBox1.Items.Add(""); listBox1.Items.Add("");
                double terkecil = double.MaxValue;
                string keterangan="";
                listBox1.Items.Add("FDA vs BDA");
                double temp = Math.Abs((hasil_fda - hasil_bda) * 100 / hasil_fda);
                if (temp < terkecil) {terkecil = temp; keterangan = "FDA vs BDA";}
                str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", hasil_fda, hasil_bda, hasil_fda,temp );
                listBox1.Items.Add(str); listBox1.Items.Add("");

                listBox1.Items.Add("FDA vs CDA");
                temp = Math.Abs((hasil_fda - hasil_cda) * 100 / hasil_fda);
                if (temp < terkecil) { terkecil = temp; keterangan = "FDA vs CDA";}
                str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", hasil_fda, hasil_cda, hasil_fda, temp);
                listBox1.Items.Add(str); listBox1.Items.Add("");

                listBox1.Items.Add("BDA vs FDA");
                temp = Math.Abs((hasil_bda - hasil_fda) * 100 / hasil_bda);
                if (temp < terkecil) { terkecil = temp; keterangan = "BDA vs FDA"; }
                str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", hasil_bda, hasil_fda, hasil_bda, temp);
                listBox1.Items.Add(str); listBox1.Items.Add("");

                listBox1.Items.Add("BDA vs CDA");
                temp = Math.Abs((hasil_bda - hasil_cda) * 100 / hasil_bda);
                if (temp < terkecil) { terkecil = temp; keterangan = "BDA vs CDA"; }
                str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", hasil_bda, hasil_cda, hasil_bda, temp);
                listBox1.Items.Add(str); listBox1.Items.Add("");

                listBox1.Items.Add("CDA vs FDA");
                temp = Math.Abs((hasil_cda - hasil_fda) * 100 / hasil_cda);
                if (temp < terkecil) { terkecil = temp; keterangan = "CDA vs FDA"; }
                str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", hasil_cda, hasil_fda, hasil_cda, temp);
                listBox1.Items.Add(str); listBox1.Items.Add("");

                listBox1.Items.Add("CDA vs BDA");
                temp = Math.Abs((hasil_cda - hasil_bda) * 100 / hasil_cda);
                if (temp < terkecil) { terkecil = temp; keterangan = "CDA vs BDA"; }
                str = String.Format("Relative Error = |{0}  -  {1}| / {2}  * 100 %  = {3} %", hasil_cda, hasil_bda, hasil_cda, temp);
                listBox1.Items.Add(str); listBox1.Items.Add("");
                listBox1.Items.Add("Kesimpulan :");
                str = String.Format("Jenis pendekatan yang menghasilkan absolute relative error paling minimum adalah {0}", keterangan);
                listBox1.Items.Add(str);
                str = String.Format("Dengan persentase absolute relative error sebesar {0}", terkecil);
                listBox1.Items.Add(str);
            }
        }
    }
}
