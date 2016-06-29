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
    public partial class Form6 : Form
    {
        double[,] M = new double[10, 10];
        double[] hasil;
        double[] hasil_akhir;
        int n;
        char[] pemisah;
        char[] tanda;
        char[] variabel = new char[10];
        List<List<double>> xi = new List<List<double>>();
        List<double> baris = new List<double>();
        int cacah = 0;
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult pilihan = MessageBox.Show("Apakah Anda yakin untuk keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pilihan == DialogResult.Yes)
                Application.ExitThread();
            else
                e.Cancel = true;
        }
        public void keArray(string kalimat)
        {
            pemisah = new char[2] { '+', '-' };
            tanda = new char[1] { '+' };
            string kal = "";
            if (kalimat[0] == '-')
            {
                for (int i = 1; i < kalimat.Length; i++)
                {
                    if (kalimat[i] != '=')
                        kal += kalimat[i];
                    else
                        break;
                }
            }
            else
            {
                for (int i = 0; i < kalimat.Length; i++)
                {
                    if (kalimat[i] != '=')
                        kal += kalimat[i];
                    else
                        break;
                }
            }

            string[] kata = kal.Split(pemisah);
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

            for (int i = 0; i < kata.Length; i++)
            {
                double var; int j = 0;
                if (kata[i].Length == 1)
                {
                    var = 1;
                    if (tanda[i] == '-') var *= -1;
                    variabel[i] = Convert.ToChar(kata[i]);
                }
                else if (kata[i].Length == 2 && kata[i][0] == '-')
                { var = -1; variabel[i] = kata[i][1]; }
                else
                {
                    string s = "";
                    for (j = 0; j < kata[i].Length - 1; j++)
                    {
                        s += kata[i][j];
                    }
                    var = Convert.ToDouble(s);
                    if (tanda[i] == '-') var *= -1;

                    variabel[i] = kata[i][j];
                }
                M[cacah, i] = var;
            }
        }

        public void keArrayHasil(string kal)
        {
            int i = kal.Length - 1;
            string s = "";
            while (kal[i] != ' ' && kal[i] != '=')
            {
                s = kal[i].ToString() + s;
                i--;
            }
            hasil[cacah] = Convert.ToDouble(s);
        }

        public void keArrayxi(string kal)
        {
            baris.Add(Convert.ToDouble(kal));
        }

        static bool diagonal_dominant(double[,] A, int n)
        {
            int c = 0;
            int minimal = 0;
            for (int i = 0; i <n; i++)
            {
                double sum = 0;
                for (int j = 0; j <n; j++)
                    if (j != i)
                        sum += Math.Abs(A[i, j]);
                if (Math.Abs(A[i, i]) >= sum)
                    c++;
                if (Math.Abs(A[i,i]) > sum)
                    minimal++;
            }
            if (c == n && minimal >0) return true;
            else return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(comboBox1.Text);
            n = i;
            hasil = new double[n];
            label3.Visible = true; textBox1.Visible = true; textBox11.Visible = true;
            label4.Visible = true; textBox2.Visible = true; textBox12.Visible = true;
            label14.Visible = true; 
            if (i > 2)
            { label5.Visible = true; textBox3.Visible = true; textBox13.Visible = true; }
            if (i > 3)
            { label6.Visible = true; textBox4.Visible = true; textBox14.Visible = true; }
            if (i > 4)
            { label7.Visible = true; textBox5.Visible = true; textBox15.Visible = true; }
            if (i > 5)
            { label8.Visible = true; textBox6.Visible = true; textBox16.Visible = true; label15.Visible = true; }
            if (i > 6)
            { label9.Visible = true; textBox7.Visible = true; textBox17.Visible = true; }
            if (i > 7)
            { label10.Visible = true; textBox8.Visible = true; textBox18.Visible = true; }
            if (i > 8)
            { label11.Visible = true; textBox9.Visible = true; textBox19.Visible = true; }
            if (i > 9)
            { label12.Visible = true; textBox10.Visible = true; textBox20.Visible = true; }

            button1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cacah = 0;
            listBox1.Items.Clear();
            baris.Clear();
            xi.Clear();
            label13.Text = "";
            bool sah = true;
            try
            {
                if (n > 1)//2
                {
                    keArray(textBox1.Text); keArrayHasil(textBox1.Text); keArrayxi(textBox11.Text);
                    cacah++;
                    keArray(textBox2.Text); keArrayHasil(textBox2.Text);keArrayxi(textBox12.Text);
                    cacah++;
                }
                if (n > 2)//3
                {
                    keArray(textBox3.Text); keArrayHasil(textBox3.Text); keArrayxi(textBox13.Text);
                    cacah++;
                }
                if (n > 3)//4
                { keArray(textBox4.Text); keArrayHasil(textBox4.Text); keArrayxi(textBox14.Text); cacah++; }
                if (n > 4)//5
                { keArray(textBox5.Text); keArrayHasil(textBox5.Text); keArrayxi(textBox15.Text); cacah++; }
                if (n > 5)//6
                { keArray(textBox6.Text); keArrayHasil(textBox6.Text); keArrayxi(textBox16.Text); cacah++; }
                if (n > 6)//7
                { keArray(textBox7.Text); keArrayHasil(textBox7.Text); keArrayxi(textBox17.Text); cacah++; }
                if (n > 7)//8
                { keArray(textBox8.Text); keArrayHasil(textBox8.Text); keArrayxi(textBox18.Text); cacah++; }
                if (n > 8)//9
                { keArray(textBox9.Text); keArrayHasil(textBox9.Text); keArrayxi(textBox19.Text); cacah++; }
                if (n > 9)//10
                { keArray(textBox10.Text); keArrayHasil(textBox10.Text); keArrayxi(textBox20.Text); cacah++; }
            }
            catch
            { MessageBox.Show("Data tidak Valid!!"); sah = false; }
            xi.Add(baris);

            bool lanjut = true;
            int c = 1;

            if (!diagonal_dominant(M, n))
            {
                lanjut = false;
                listBox1.Items.Add("Tidak Diagonal Dominan");
            }
            string z = "";
            for (int i = 0; i < n; i++)
            {
                z += String.Format(" {0,-25}", "x" + (i + 1).ToString());
            }
            listBox1.Items.Add(z);
            z = "";
            for (int i = 0; i < 25 * n; i++)
                z += "-";
            listBox1.Items.Add(z);

            bool selesai = false;
            while (lanjut && sah)
            {
                List<double> ba = new List<double>(n);
                for (int i = 0; i < n; i++)
                {
                    double t = 0.0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                        {
                            t += (M[i, j] * xi[c - 1][j]);
                            //Console.WriteLine("t: {0}", t);
                        }
                    }

                    double x = Math.Round((hasil[i] - t) / M[i, i], 10);
                    ba.Add(x);
                    //listBox1.Items.Add(c.ToString() + " " + x.ToString());
                }
                xi.Add(ba);
                string s="";
                for (int i = 0; i < ba.Count; i++)
                    s += String.Format("{0,-15:F10} ",ba[i]);
                listBox1.Items.Add(s);
                //cek epsilon
                int counter = 0;
                for (int k = 0; k < n; k++)
                {
                    if (xi[c][k] - xi[c - 1][k] == 0.0) counter++;
                    else break;
                }
                if (counter == n) lanjut = false;
                c++;
                selesai = true;
            }

            if (sah && selesai)
            {
                string a = "Hasil : ";
                for (int i = 0; i < n; i++)
                    a += (variabel[i] + " = " + Math.Round(xi[c - 1][i], 3).ToString() + " ; ");
                label13.Text = a;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(comboBox2.Text);
            n = i;
            hasil = new double[n];
            label27.Visible = true; textBox40.Visible = true; textBox30.Visible = true;
            label26.Visible = true; textBox39.Visible = true; textBox29.Visible = true;
            label17.Visible = true;
            if (i > 2)
            { label25.Visible = true; textBox38.Visible = true; textBox28.Visible = true; }
            if (i > 3)
            { label24.Visible = true; textBox37.Visible = true; textBox27.Visible = true; }
            if (i > 4)
            { label23.Visible = true; textBox36.Visible = true; textBox26.Visible = true; }
            if (i > 5)
            { label22.Visible = true; textBox35.Visible = true; textBox25.Visible = true; label16.Visible = true; }
            if (i > 6)
            { label21.Visible = true; textBox34.Visible = true; textBox24.Visible = true; }
            if (i > 7)
            { label20.Visible = true; textBox33.Visible = true; textBox23.Visible = true; }
            if (i > 8)
            { label19.Visible = true; textBox32.Visible = true; textBox22.Visible = true; }
            if (i > 9)
            { label18.Visible = true; textBox31.Visible = true; textBox21.Visible = true; }

            button5.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cacah = 0;
            listBox2.Items.Clear();
            baris.Clear();
            xi.Clear();
            label29.Text = "";
            bool sah = true;
            try
            {
                if (n > 1)//2
                {
                    keArray(textBox40.Text); keArrayHasil(textBox40.Text); keArrayxi(textBox30.Text);
                    cacah++;
                    keArray(textBox39.Text); keArrayHasil(textBox39.Text); keArrayxi(textBox29.Text);
                    cacah++;
                }
                if (n > 2)//3
                {
                    keArray(textBox38.Text); keArrayHasil(textBox38.Text); keArrayxi(textBox28.Text);
                    cacah++;
                }
                if (n > 3)//4
                { keArray(textBox37.Text); keArrayHasil(textBox37.Text); keArrayxi(textBox27.Text); cacah++; }
                if (n > 4)//5
                { keArray(textBox36.Text); keArrayHasil(textBox36.Text); keArrayxi(textBox26.Text); cacah++; }
                if (n > 5)//6
                { keArray(textBox35.Text); keArrayHasil(textBox35.Text); keArrayxi(textBox25.Text); cacah++; }
                if (n > 6)//7
                { keArray(textBox34.Text); keArrayHasil(textBox34.Text); keArrayxi(textBox24.Text); cacah++; }
                if (n > 7)//8
                { keArray(textBox33.Text); keArrayHasil(textBox33.Text); keArrayxi(textBox23.Text); cacah++; }
                if (n > 8)//9
                { keArray(textBox32.Text); keArrayHasil(textBox32.Text); keArrayxi(textBox22.Text); cacah++; }
                if (n > 9)//10
                { keArray(textBox31.Text); keArrayHasil(textBox31.Text); keArrayxi(textBox21.Text); cacah++; }
            }
            catch
            { MessageBox.Show("Data tidak Valid!!"); sah = false; }
            xi.Add(baris);

            bool lanjut = true;
            int c = 1;

            if (!diagonal_dominant(M, n))
            {
                lanjut = false;
                listBox2.Items.Add("Tidak Diagonal Dominan");
            }

            string z = "";
            for (int i = 0; i < n; i++)
            {
                z += String.Format(" {0,-25}","x"+(i+1).ToString());
            }
            listBox2.Items.Add(z);
            z="";
            for (int i = 0; i < 25 * n; i++)
                z += "-";
            listBox2.Items.Add(z);
            bool selesai = false;
            while (lanjut && sah)
            {
                List<double> ba = new List<double>(n);
                for (int i = 0; i < n; i++)
                {
                    double t = 0.0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                        {
                            if (j < i)
                                t += (M[i, j] * ba[j]);
                            else
                                t += (M[i, j] * xi[c - 1][j]);
                            //Console.WriteLine("t: {0}", t);
                        }
                    }

                    double x = Math.Round((hasil[i] - t) / M[i, i], 10);
                    ba.Add(x);
                }
                xi.Add(ba);
                string s = "";
                for (int i = 0; i < ba.Count; i++)
                    s += String.Format("{0,-15:F10} ", ba[i]);
                listBox2.Items.Add(s);
                //cek epsilon
                int counter = 0;
                for (int k = 0; k < n; k++)
                {
                    if (xi[c][k] - xi[c - 1][k] == 0.0) counter++;
                    else break;
                }
                if (counter == n) lanjut = false;
                c++;
                selesai = true;
            }

            if (sah && selesai)
            {
                string a = "Hasil : ";
                for (int i = 0; i < n; i++)
                    a += (variabel[i] + " = " + Math.Round(xi[c - 1][i], 3).ToString() + " ; ");
                label29.Text = a;
            }
        }
    }
}
