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
    public partial class Form4 : Form
    {
        string kalimat;
        string turunan;
        double koef, pangkat, sign=1;
        char[] pemisah;
        char[] tanda;
        string[] kata = new string[0];
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult pilihan = MessageBox.Show("Apakah Anda yakin untuk keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pilihan == DialogResult.Yes)
                Application.ExitThread();
            else
                e.Cancel = true;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        public double CariJumlahPerKata(int i, double x)
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
            int pos = 0 ;

            for (int i = kata.Length - 1; i >= 0; i--)
            {
                if (kata[i].IndexOf('x') != -1)
                { pos = i; break; }          
            }

            if (tanda[pos] == '+') sign = -1;
            else sign = 1;

            int pos_x = kata[pos].IndexOf('x');
            string s;

            if (kata[pos] == "x")
            { koef = 1; pangkat = 1; }
            else if (pos_x == 0)
            { 
                koef = 1;  s="";
                for (int i = 2; i < kata[pos].Length; i++) s += kata[pos][i];
                pangkat = Convert.ToDouble(s);
            }
            else if (pos_x == kata[pos].Length - 1)
            {
                pangkat = 1; s = "";
                for (int i = 0; i < pos_x; i++) s += kata[pos][i];
                koef = Convert.ToDouble(s);
            }
            else
            {
                s = "";
                for (int i = 0; i < pos_x; i++) s += kata[pos][i];
                koef = Convert.ToDouble(s);
                s = "";
                for (int i = pos_x+2 ; i < kata[pos].Length; i++) s += kata[pos][i];
                pangkat = Convert.ToDouble(s);
            }

            for (int i = pos; i < kata.Length - 1; i++)
            {
                tanda[i] = tanda[i + 1];
                kata[i] = kata[i+1];
            }
            Array.Resize(ref kata, kata.Length - 1);
            label6.Visible = true;

            label6.Text = "";
            if (sign == -1) label6.Text += "-1 * ";
            label6.Text += "(" + kata[0];
            for (int i = 1; i < kata.Length; i++)
            { 
                label6.Text += (" " + tanda[i].ToString() + " ");
                label6.Text += kata[i];
            }
            label6.Text += ")";
            if (koef != 1) label6.Text += (" / " + koef.ToString());
            if (pangkat != 1)
            {
                label6.Text = "Akar pangkat " + pangkat.ToString() + " dari [" + label6.Text;
                label6.Text += "]";
            }
            label6.Text = "g(x) = " + label6.Text;

        }

        public void manipulasi_turunan()
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
            turunan="";

            for (int i = 0; i < kata.Length; i++)
            {
                if (kata[i].Length == 1 && kata[i] == "x")
                {
                  if (i != 0)
                        turunan += " " + tanda[i] + " ";
                  turunan += "1"; }
                else
                {
                    int p = kata[i].IndexOf('x');
                    if (p == -1) continue;
                    if (p == 0)
                    {
                        string s = "";
                        for (int j = p + 2; j < kata[i].Length; j++) 
                            s+= kata[i][j];
                        if (i!=0)
                            turunan += " " +tanda[i]+" ";
                        turunan += s; turunan += "x";
                    }
                    else if (p == kata[i].Length - 1)
                    {
                        if (i != 0)
                            turunan += " " + tanda[i] + " ";
                        for (int j = 0; j < p; j++) turunan += kata[i][j];
                    }
                    else
                    {
                        string s = "";
                        for (int j = p+2; j < kata[i].Length; j++)
                            s += kata[i][j];
                        double pangkat = Convert.ToDouble(s);
                        s = "";
                        for (int j = 0; j < p; j++) s += kata[i][j];
                        double koef = pangkat * Convert.ToDouble(s);
                        pangkat -= 1;
                        if (i != 0)
                            turunan += " " + tanda[i] + " ";
                        turunan += koef.ToString(); turunan += "x";
                        if (pangkat != 1)
                        { turunan += "^"; turunan += pangkat.ToString(); }
                    }
                }
            }

        }


        public double fungsi_gx(double x)
        {
            manipulasi();
            double total = 0.0;

            for (int i = 0; i < kata.Length; i++)
            {
                if (tanda[i] == '+')
                    total += CariJumlahPerKata(i, x);
                else
                    total -= CariJumlahPerKata(i, x);
            }
            total *= sign;
            total /= koef;

            int w=1;

            if (total < 0.0) w = -1;
            total = Math.Pow(Math.Abs(total), (1.0/pangkat));

            return (w*total);
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

        private void button1_Click(object sender, EventArgs e)
        {
            kalimat = textBox1.Text;
            
            bool input_sah = true;
            label5.Visible = false;
            board1.Rows.Clear();
            try { manipulasi(); }
            catch { input_sah = false; }
  
            if (!validasi_x(textBox2.Text)) label5.Visible = true;

            if (validasi_x(textBox2.Text) && input_sah)
            {
                int i = 1;
                decimal x0 = Convert.ToDecimal(textBox2.Text), xi = (decimal)/*Math.Round(*/fungsi_gx((double)Math.Abs(x0))/*, 10)*/;
                bool divergen = false;
                board1.Rows.Add();
                board1[0, 0].Value = 0;
                board1[1, 0].Value = x0;
                while (true && i<=300)
                {
                    try { xi = (decimal)/*Math.Round(*/fungsi_gx((double)Math.Abs(x0))/*, 10)*/; }
                    catch { break; }
                    decimal selisih = Math.Abs((decimal)xi - x0);
                    x0 = xi;

                    board1.Rows.Add();
                    board1[0, i].Value = i.ToString();
                    board1[1, i].Value = xi.ToString();
                    board1[2, i].Value = selisih.ToString();

                    if (selisih == 0.0m)
                    {
                        board1[1, i - 1].Style.BackColor = Color.Yellow;
                        board1[1, i].Style.BackColor = Color.Yellow;
                        board1[2, i].Style.BackColor = Color.Yellow;
                        break;
                    }
                    if (selisih > 100000000000) { divergen = true; break; }
                    i++;
                }
                if (!divergen)
                    textBox3.Text = board1[1, i-1].Value.ToString();
                else
                    textBox3.Text = "Divergen !!";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kalimat = textBox6.Text;
            bool input_sah = true;
            try
            {
                manipulasi_turunan();
            }
            catch { input_sah = false; }
            label9.Visible = false;
            board2.Rows.Clear();
            label7.Visible = true;
            label7.Text = "f'(x) = " + turunan;

            if (!validasi_x(textBox5.Text)) label9.Visible = true;

            if (validasi_x(textBox5.Text) && input_sah)
            {
                fungsi f = new fungsi();
                f.kalimat = kalimat;
                f.manipulasi();

                fungsi t = new fungsi();
                t.kalimat = turunan;
                t.manipulasi();

                decimal x0 = Convert.ToDecimal(textBox5.Text), xi;
                board2.Rows.Add();
                board2[0, 0].Value = 0;
                board2[1, 0].Value = x0;
                int i = 1;
                while (i < 200)
                {
                    xi = x0 - (decimal)(f.fx((double)x0) / t.fx((double)x0));
                    decimal selisih = Math.Abs(xi - x0);
                    x0 = xi;

                    board2.Rows.Add();
                    board2[0, i].Value = i.ToString();
                    board2[1, i].Value = xi.ToString();
                    board2[2, i].Value = selisih.ToString();

                    if (selisih == 0.0m)
                    {
                        board2[1, i - 1].Style.BackColor = Color.Yellow;
                        board2[1, i].Style.BackColor = Color.Yellow;
                        board2[2, i].Style.ForeColor = Color.Red;
                        break;
                    }
                    i++;
                }
                textBox4.Text = Math.Round(Convert.ToDecimal(board2[1, i-1].Value),15).ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kalimat = textBox7.Text;
            label14.Visible = false; label16.Visible = false;
            dataGridView1.Rows.Clear();
            if (!validasi_x(textBox8.Text)) label14.Visible = true;
            if (!validasi_x(textBox9.Text)) label16.Visible = true;
            bool input_sah = true;
            fungsi f = new fungsi();
            try
            {          
                f.kalimat = kalimat;
                f.manipulasi();
            }
            catch { input_sah = false; }

            if (input_sah && validasi_x(textBox8.Text) && validasi_x(textBox9.Text))
            {
                decimal x0 = Convert.ToDecimal(textBox8.Text);
                decimal x1 = Convert.ToDecimal(textBox9.Text);
                decimal xi=0;
                dataGridView1.Rows.Add();
                dataGridView1[0, 0].Value = 0;
                dataGridView1[1, 0].Value = x0;
                dataGridView1.Rows.Add();
                dataGridView1[0, 1].Value = 1;
                dataGridView1[1, 1].Value = x1;
                dataGridView1[2, 1].Value = (x1 - x0).ToString();
                bool berhenti = false;

                int i = 2;
                while (i <= 200)
                {
                    if (Math.Abs((f.fx((double)x1) - f.fx((double)x0))) == 0.000000000000) { berhenti = true; break; }
                    try
                    {
                        xi = x1 - (((decimal)f.fx((double)x1) * (x1 - x0)) / (decimal)(f.fx((double)x1) - f.fx((double)x0)));
                    }
                    catch { berhenti = true; break; }
                    decimal selisih = Math.Abs(xi - x1);
                    x0 = x1;
                    x1 = xi;

                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = i.ToString();
                    dataGridView1[1, i].Value = xi.ToString();
                    dataGridView1[2, i].Value = selisih.ToString();

                    if (selisih == 0.0m)
                    {
                        dataGridView1[1, i - 1].Style.BackColor = Color.Yellow;
                        dataGridView1[1, i].Style.BackColor = Color.Yellow;
                        dataGridView1[2, i].Style.ForeColor = Color.Red;
                        break;
                    }
                    i++;
                }
                string s;
                if (berhenti) s = "Divided by Zero";
                else if (i > 200) s = "Divergen";
                else s = Math.Round(xi,10).ToString();
                textBox10.Text = s;
            }
            

        }
    }
}
