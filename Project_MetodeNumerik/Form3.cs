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
    public partial class Form3 : Form
    {
        string kalimat;
        char[] pemisah;
        char[] tanda;
        public Form3()
        {
            InitializeComponent();
        }

        public bool validasi(string teks)
        {
            bool sah = true;
            double N;
            try
            {
                N = Convert.ToDouble(teks);
            }
            catch (Exception e)
            {
                sah = false;
                return false;
            }

            if (N <= 0) sah = false;

            return sah;
        }

        public bool validasi_x(string teks)
        {
            bool sah = true;
            double N;
            try
            {
                N = Convert.ToDouble(teks);
            }
            catch (Exception e)
            {
                sah = false;
                return false;
            }

            return sah;
        }

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

        public double fungsi(double x)
        { 
            pemisah = new char[2] {'+','-'};
            tanda = new char[1] {'+'};
            string kal = "";
            if (kalimat[0] == '-')
            {
                for (int i = 1; i < kalimat.Length; i++) kal += kalimat[i];
            }
            else
                kal = kalimat;
            string[] kata = kal.Split(pemisah);
            if (kalimat[0] == '-')
            {
                kata[0] = "-" + kata[0];
            }

            for (int i = 0; i < kata.Length; i++)
                kata[i] = kata[i].Trim();

            

            int m=0;
            if (kalimat[0] == '-') m = 1;
            for (int i=m;i<kalimat.Length;i++)
            {
                if (kalimat[i] == '+' || kalimat[i] == '-')
                {
                    Array.Resize(ref tanda, tanda.Length + 1);
                    tanda[tanda.GetUpperBound(0)] = kalimat[i];
                }
            }

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

        private void Form3_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult pilihan = MessageBox.Show("Apakah Anda yakin untuk keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pilihan == DialogResult.Yes)
                Application.ExitThread();
            else
                e.Cancel = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            label6.Visible = false; label7.Visible = false; label8.Visible = false;
            board.Rows.Clear();
            kalimat = textBox1.Text;
            bool input_sah = true;
            try
            {
                fungsi(1);
            }
            catch
            { input_sah = false;}
            if (!validasi_x(textBox2.Text)) label6.Visible = true;
            if (!validasi_x(textBox3.Text)) label7.Visible = true;
            if (!validasi(textBox4.Text)) label8.Visible = true;

            if (validasi_x(textBox2.Text) && validasi_x(textBox3.Text) && validasi(textBox4.Text) && input_sah)
            {
                double a = Convert.ToDouble(textBox2.Text);
                double b = Convert.ToDouble(textBox3.Text);
                double c= (a+b)/2;
                decimal ep = (decimal)b - (decimal)c;
                double epsilon = Convert.ToDouble(textBox4.Text);
                int counter = 0;
                while (ep > (decimal)epsilon)
                {
                    c = (a + b) / 2.0;
                    bool selesai = false;

                    board.Rows.Add();
                    board[0, counter].Value = counter;
                    board[1, counter].Value = a.ToString();
                    board[2, counter].Value = c.ToString();
                    board[3, counter].Value = b.ToString();
                    decimal fa = (decimal)fungsi(a), fb = (decimal)fungsi(b), fc = (decimal)fungsi(c);
                    string range = "", alasan = "", lanjut = "";

                    board[4, counter].Value = fa.ToString();
                    board[5, counter].Value = fc.ToString();
                    board[6, counter].Value = fb.ToString();

                    ep = (decimal)b - (decimal)c; 

                    if (fc == 0.0m)
                    { selesai = true; range = "[c]"; alasan = "akar sudah ditemukan"; lanjut = "Stop"; }
                    else if (fa * fc <= 0.0m)
                    { b = c; range = "[a,c]"; alasan = "f(a) dan f(c) beda tanda"; lanjut = "Go"; }
                    else
                    { a = c; range = "[c,b]"; alasan = "f(c) dan f(b) beda tanda"; lanjut = "Go"; }

                    board[7, counter].Value = range;
                    board[8, counter].Value = ep.ToString();

                    if (ep > (decimal)epsilon)
                    {
                        board[9, counter].Value = alasan;
                        board[10, counter].Value = lanjut;
                    }
                    else
                    {
                        board[9, counter].Value = "Stop, epsilon sudah dicapai";
                        board[10, counter].Value = "Stop";
                        board[2, counter].Style.ForeColor = Color.Red; 
                    }


                    if (selesai)
                    {
                        board[2, counter].Style.ForeColor = Color.Red;
                        break;
                    }
                    counter++;
                }
                textBox5.Text = c.ToString();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label10.Visible = false; label11.Visible = false; label12.Visible = false; label17.Visible = false;
            boards.Rows.Clear();
            kalimat = textBox9.Text;
            bool input_sah = true;
            try
            {
                fungsi(1);
            }
            catch
            { input_sah = false; }
            if (!validasi_x(textBox8.Text)) label12.Visible = true;
            if (!validasi_x(textBox7.Text)) label11.Visible = true;
            if (!validasi(textBox6.Text)) label10.Visible = true;
            if (!validasi(textBox10.Text)) label17.Visible = true;

            if (validasi_x(textBox8.Text) && validasi_x(textBox7.Text) && validasi(textBox6.Text) && validasi(textBox10.Text) && input_sah)
            {
                double a = Convert.ToDouble(textBox8.Text);
                double b = Convert.ToDouble(textBox7.Text);
                double c = b - (fungsi(b)*(b-a))/(fungsi(b) - fungsi(a));
                decimal ep = (decimal)b - (decimal)c;

                double epsilon1 = Convert.ToDouble(textBox6.Text);
                double epsilon2 = Convert.ToDouble(textBox10.Text);
                int counter = 0;

                while (ep > (decimal)epsilon1)
                {
                    c = b - (fungsi(b) * (b - a)) / (fungsi(b) - fungsi(a));
                    bool selesai = false;

                    boards.Rows.Add();
                    boards[0, counter].Value = counter;
                    boards[1, counter].Value = a.ToString();
                    boards[2, counter].Value = c.ToString();
                    boards[3, counter].Value = b.ToString();
                    decimal fa = (decimal)fungsi(a), fb = (decimal)fungsi(b), fc = (decimal)fungsi(c);
                    decimal absfc = Math.Abs(fc);
                    string range = "", alasan = "", lanjut = "";

                    boards[4, counter].Value = fa.ToString();
                    boards[5, counter].Value = fc.ToString();
                    boards[6, counter].Value = fb.ToString();

                    ep = (decimal)b - (decimal)c;

                    if (Math.Abs(fc) < (decimal)epsilon2)
                    { selesai = true; range = "[c]"; alasan = "Stop, epsilon2 sudah dicapai"; lanjut = "Stop"; }
                    else
                    { 
                        if (fc == 0.0m)
                        { selesai = true; range = "[c]"; alasan = "akar sudah ditemukan"; lanjut = "Stop"; }
                        else if (fa * fc <= 0.0m)
                        { b = c; range = "[a,c]"; alasan = "f(a) dan f(c) beda tanda"; lanjut = "Go"; }
                        else
                        { a = c; range = "[c,b]"; alasan = "f(c) dan f(b) beda tanda"; lanjut = "Go"; }
                    }


                    boards[7, counter].Value = range;
                    boards[8, counter].Value = ep.ToString();

                    if (ep > (decimal)epsilon1)
                    {
                        boards[9, counter].Value = absfc.ToString();
                        boards[10, counter].Value = lanjut;
                    }
                    else
                    {
                        boards[9, counter].Value = absfc.ToString();
                        boards[10, counter].Value = "Stop";
                        boards[2, counter].Style.ForeColor = Color.Red;
                    }


                    if (selesai)
                    {
                        boards[2, counter].Style.ForeColor = Color.Red;
                        break;
                    }
                    counter++;
                }

                textBox11.Text = c.ToString();
            }
        }


    }
}
