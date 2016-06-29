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
    public partial class Form7 : Form
    {
        double[] x = new double[11];
        double[] y = new double[11];
        double[] x_lengkap = new double[11];
        double[] y_lengkap = new double[11];
        double[,] M = new double[11, 11];
        double[,] N = new double[11, 11];
        double[] hasil_akhir = new double[11];
        double xx;
        int n, cacah,cacah2, n_temp;
        int htg;
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
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
            string kiri="", kanan="";
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
        public void keArray2(string teks)
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
            x_lengkap[cacah2] = Convert.ToDouble(kiri);
            y_lengkap[cacah2] = Convert.ToDouble(kanan);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(comboBox1.Text);
            n = i;
            n_temp = n;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
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
                    keArray(textBox2.Text);cacah++;
                }
                if (n > 2)//3
                {
                    keArray(textBox3.Text);cacah++;
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
                int i, k;
                n = n_temp - 1;
                double[,] ST = new double[n + 1, n + 1];
                double jumlah, suku;
                for (k = 0; k <= n; k++)
                {
                    ST[k, 0] = y[k];
                }

                for (k = 1; k <= n; k++)
                {
                    for (i = 0; i <= (n - k); i++)
                    {
                        ST[i, k] = Math.Round((ST[i + 1, k - 1] - ST[i, k - 1]) / (x[i + k] - x[i]), 6);
                    }
                }
                jumlah = ST[0, 0];
                for (int j = 2; j <= n; j++)
                {
                    listBox1.Items.Add(""); listBox1.Items.Add("");
                    listBox1.Items.Add("Interpolasi Newton pangkat " + j.ToString());
                    jumlah = ST[0, 0];
                    listBox1.Items.Add("b0 = " + jumlah.ToString());
                    for (i = 1; i <= j; i++)
                    {
                        suku = ST[0, i];
                        for (k = 0; k <= i - 1; k++)
                            suku *= (xx - x[k]);
                        jumlah += suku;
                        string s = String.Format("b{0} = {1}", i, ST[0, i]);
                        listBox1.Items.Add(s);
                    }
                    listBox1.Items.Add("");
                    listBox1.Items.Add("Hasil  : " + jumlah.ToString());
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(comboBox2.Text);
            n = i;
            n_temp = n;
            label25.Visible = true; textBox22.Visible = true; checkBox1.Visible = true;
            label24.Visible = true; textBox21.Visible = true; checkBox2.Visible = true;
            if (i > 2)
            { label23.Visible = true; textBox20.Visible = true; checkBox3.Visible = true; }
            if (i > 3)
            { label22.Visible = true; textBox19.Visible = true; checkBox4.Visible = true; }
            if (i > 4)
            { label21.Visible = true; textBox18.Visible = true; checkBox5.Visible = true; }
            if (i > 5)
            { label20.Visible = true; textBox17.Visible = true; checkBox6.Visible = true; }
            if (i > 6)
            { label19.Visible = true; textBox16.Visible = true; checkBox7.Visible = true; }
            if (i > 7)
            { label18.Visible = true; textBox15.Visible = true; checkBox8.Visible = true; }
            if (i > 8)
            { label17.Visible = true; textBox14.Visible = true; checkBox9.Visible = true; }
            if (i > 9)
            { label16.Visible = true; textBox13.Visible = true; checkBox10.Visible = true; }

            label26.Visible = true; label15.Visible = true; textBox12.Visible = true;

            button5.Enabled = false;
            button4.Visible = true;
        }
        public void keArrayM(double[] x)
        {
            for (int i = 0; i < cacah; i++)
            {
                int c=0;
                for (int j = 0; j < cacah; j++)
                {
                    M[i, j] = Math.Pow(x[i], c);
                    c++;
                }
            }
        }
        private double[,] invers(double[,] arr, int n)
        {
            double[,] a = new double[n, n * 2];
            double[,] h = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = arr[i, j];
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = n; j < 2 * n; j++)
                {
                    if (i == j - n)
                        a[i, j] = 1;
                    else
                        a[i, j] = 0;
                }
            }
            for (int i = 0; i < n; i++)
            {
                double t = a[i, i];
                for (int j = i; j < 2 * n; j++)
                {
                    a[i, j] /= t;
                }
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        t = a[j, i];
                        for (int k = 0; k < 2 * n; k++)
                        {
                            a[j, k] -= t * a[i, k];
                        }
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = n; j < 2 * n; j++)
                {
                    h[i, j - n] = a[i, j];
                }
            }
            return h;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            cacah = 0;
            cacah2 = 0;
            htg = 0;
            listBox2.Items.Clear();
            bool sah = true;
            try
            {
                if (n > 1)
                { keArray2(textBox22.Text); cacah2++; keArray2(textBox21.Text); cacah2++; }
                if (n > 2)
                { keArray2(textBox20.Text); cacah2++; }
                if (n > 3)
                { keArray2(textBox19.Text); cacah2++; }
                if (n > 4)
                { keArray2(textBox18.Text); cacah2++; }
                if (n > 5)
                { keArray2(textBox17.Text); cacah2++; }
                if (n > 6)
                { keArray2(textBox16.Text); cacah2++; }
                if (n > 7)
                { keArray2(textBox15.Text); cacah2++; }
                if (n > 8)
                { keArray2(textBox14.Text); cacah2++; }
                if (n > 9)
                { keArray2(textBox13.Text); cacah2++; }
                if (checkBox1.Checked==true)//1
                {
                    keArray(textBox22.Text); cacah++; htg++;
                }
                if (checkBox2.Checked == true)//2
                {
                    keArray(textBox21.Text); cacah++; htg++;
                }
                if (checkBox3.Checked == true)//3
                {
                    keArray(textBox20.Text); cacah++; htg++;
                }
                if (checkBox4.Checked == true)//4
                { keArray(textBox19.Text); cacah++; htg++; }
                if (checkBox5.Checked == true)//5
                { keArray(textBox18.Text); cacah++; htg++; }
                if (checkBox6.Checked == true)//6
                { keArray(textBox17.Text); cacah++; htg++; }
                if (checkBox7.Checked == true)//7
                { keArray(textBox16.Text); cacah++; htg++; }
                if (checkBox8.Checked == true)//8
                { keArray(textBox15.Text); cacah++; htg++; }
                if (checkBox9.Checked == true)//9
                { keArray(textBox14.Text); cacah++; htg++; }
                if (checkBox10.Checked == true)//10
                { keArray(textBox13.Text); cacah++; htg++; }

                xx = Convert.ToDouble(textBox12.Text);

                if (htg < 2) throw new Exception("Titik yang dipilih kurang");
            }
            catch (Exception q)
            { 
                if (q.Message=="Titik yang dipilih kurang")
                    MessageBox.Show(q.Message);
                else
                    MessageBox.Show("Data tidak Valid!!");
                sah = false;
            }
            if (sah)
            {
                keArrayM(x);
                N = invers(M, cacah);
                string s;
                listBox2.Items.Add("Matriks : "); listBox2.Items.Add("");
                s = "";
                for (int i = 0; i < cacah; i++)
                {
                    s = "";
                    for (int j = 0; j < cacah; j++)
                    {
                        s += String.Format("{0,-20:F6}   ", M[i, j]);
                    }
                    listBox2.Items.Add(s);
                }

                listBox2.Items.Add(""); listBox2.Items.Add("");
                listBox2.Items.Add("Matriks Invers : "); listBox2.Items.Add("");
                s="";
                for (int i = 0; i < cacah; i++)
                {
                    s = "";
                    for (int j = 0; j < cacah; j++)
                    {
                        s += String.Format("{0,-20:F6}   ", N[i, j]);
                    }
                    listBox2.Items.Add(s);
                }

                listBox2.Items.Add(""); listBox2.Items.Add("");
                listBox2.Items.Add("Setelah dikalikan dengan matriks y : "); listBox2.Items.Add("");
                List<double> koleksi = new List<double>();
                double jlh = 0.0;
               
                for (int i = 0; i < cacah; i++)
                {
                    double d = 0;
                    for (int j = 0; j < cacah; j++)
                        d += (N[i, j] * y[j]);

                    hasil_akhir[i] = d;
                    s = String.Format("a{0}  = {1}", i, Math.Round(d, 6));
                    koleksi.Add(d);
                    listBox2.Items.Add(s);
                    jlh += (d * Math.Pow(xx, i));
                    //MessageBox.Show(s.ToString());
                }

                listBox2.Items.Add("");
                s = String.Format("Hasil F({0})  = {1}", xx, jlh);
                listBox2.Items.Add(s);
                listBox2.Items.Add("");
                listBox2.Items.Add("Pengecekan");
                label28.Text = s;

                double error=0;
                for (int k = 0; k < n; k++)
                {
                    jlh = 0;
                    for (int i = 0; i < cacah; i++)
                        jlh += (koleksi[i] * Math.Pow(x_lengkap[k], i));

                    error += Math.Abs(jlh - y_lengkap[k]);
                    s = String.Format("Hasil F({0})  = {1}", x_lengkap[k], jlh);
                    listBox2.Items.Add(s);
                }
                listBox2.Items.Add("Total Error = " + error.ToString());
            }
            
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
