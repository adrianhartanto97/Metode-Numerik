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
    public partial class Form2 : Form
    {
        double N,galat;
        double derajat, radian;
        public Form2()
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label6.Visible = false;
            label7.Visible = false;
            textBox3.Text = "";
            if (!validasi(textBox1.Text)) label6.Visible = true;

            if (!validasi(textBox2.Text)) label7.Visible = true;

            if (validasi(textBox1.Text) && validasi(textBox2.Text))
            {
                N = Convert.ToDouble(textBox1.Text);
                galat = Convert.ToDouble(textBox2.Text);

                double S, T, eps;
                S = 1; T = N / S;
                eps = Math.Abs(T - S);
                while (eps > galat)
                {
                    S = (S + T) / 2;
                    T = N / S;
                    eps = Math.Abs(T - S);
                }

                textBox3.Text = S.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult pilihan = MessageBox.Show("Apakah Anda yakin untuk keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pilihan == DialogResult.Yes)
                Application.ExitThread();
            else
                e.Cancel = true;
        }

        static Int64 faktorial(int x)
        {
            if (x == 0) return 1;
            return x * faktorial(x - 1);
        }

        public double sederhanakan(double x)
        {
            while (x >= 360)
                x -= 360;

            return x;
        }

        public decimal fungsi_sinus(double derajat)
        {
            decimal nilai, sum = 0.0M, epsilon = Convert.ToDecimal(textBox8.Text);
            radian = derajat * Math.PI / 180.0;
            listBox1.Items.Add("Pangkat Faktorial\t\tNilai");
            int hitung = 1, c = 1;
            nilai = (decimal)Math.Pow(radian, hitung) / faktorial(hitung);
            listBox1.Items.Add(hitung.ToString() + "\t\t\t" + nilai.ToString());

            while (Math.Abs(nilai) > epsilon)
            {
                sum += nilai;
                hitung += 2;
                c++;

                nilai = (decimal)Math.Pow(radian, hitung) / faktorial(hitung);
                if (c % 2 == 0)
                    nilai *= -1;
                listBox1.Items.Add(hitung.ToString() + "\t\t\t" + nilai.ToString());
            }

            return sum;
        }

        public decimal fungsi_cosinus(double derajat)
        {
            decimal nilai, sum = 0.0M, epsilon = Convert.ToDecimal(textBox8.Text);
            radian = derajat * Math.PI / 180.0;
            listBox1.Items.Add("Pangkat Faktorial\t\tNilai");
            int hitung = 0, c = 1;
            nilai = (decimal)Math.Pow(radian, hitung) / faktorial(hitung);
            listBox1.Items.Add(hitung.ToString() + "\t\t\t" + nilai.ToString());
            while (Math.Abs(nilai) > epsilon)
            {
                sum += nilai;
                hitung += 2;
                c++;

                nilai = (decimal)Math.Pow(radian, hitung) / faktorial(hitung);
                if (c % 2 == 0)
                    nilai *= -1;
                listBox1.Items.Add(hitung.ToString() + "\t\t\t" + nilai.ToString());
            }

            return sum;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label11.Visible = false; label12.Visible = false; textBox4.Text = "";
            listBox1.Items.Clear();
            if (radioButton1.Checked == true && textBox5.Enabled == true) //sinus derajat
            {
                if (!validasi_x(textBox5.Text)) label11.Visible = true;
                if (!validasi(textBox8.Text)) label12.Visible = true;

                if (validasi_x(textBox5.Text) && validasi(textBox8.Text))
                {
                    derajat = Convert.ToDouble(textBox5.Text); 
                    bool flag = false;
                    if (derajat < 0.0) flag = true;
                    derajat = Math.Abs(derajat);  derajat = sederhanakan(derajat);
                    
                    decimal hasil=0;
                    if (derajat >= 0.0 && derajat <= 90.0) //kuadran 1
                        hasil = fungsi_sinus(derajat);
                    else if (derajat >= 91.0 && derajat <= 180.0) //kuadran 2
                        hasil = fungsi_cosinus(derajat - 90);
                    else if (derajat >= 181.0 && derajat <= 270.0) //kuadran 3
                        hasil = -1 * fungsi_sinus(derajat - 180);
                    else if (derajat >= 271.0 && derajat <= 360.0) //kuadran 4
                        hasil = -1 * fungsi_cosinus(derajat - 270);

                    if (flag) hasil *= -1;

                    textBox4.Text = hasil.ToString();
                }
            }

            else if (radioButton1.Checked == true && textBox6.Enabled == true) // sinus radian
            {
                if (!validasi_x(textBox6.Text)) label11.Visible = true;
                if (!validasi(textBox8.Text)) label12.Visible = true;

                if (validasi_x(textBox6.Text) && validasi(textBox8.Text))
                {
                    radian = Convert.ToDouble(textBox6.Text);
                    derajat = radian * 180.0 / Math.PI;
                    bool flag = false;
                    if (derajat < 0.0) flag = true;
                    derajat = Math.Abs(derajat);
                    derajat = sederhanakan(derajat);

                    decimal hasil = 0;
                    if (derajat >= 0.0 && derajat <= 90.0) //kuadran 1
                        hasil = fungsi_sinus(derajat);
                    else if (derajat >= 91.0 && derajat <= 180.0) //kuadran 2
                        hasil = fungsi_cosinus(derajat - 90);
                    else if (derajat >= 181.0 && derajat <= 270.0) //kuadran 3
                        hasil = -1 * fungsi_sinus(derajat - 180);
                    else if (derajat >= 271.0 && derajat <= 360.0) //kuadran 4
                        hasil = -1 * fungsi_cosinus(derajat - 270);

                    if (flag) hasil *= -1;

                    textBox4.Text = hasil.ToString();
                }
            }

            else if (radioButton2.Checked == true && textBox5.Enabled == true) //cosinus derajat
            {
                if (!validasi_x(textBox5.Text)) label11.Visible = true;
                if (!validasi(textBox8.Text)) label12.Visible = true;

                if (validasi_x(textBox5.Text) && validasi(textBox8.Text))
                {
                    derajat = Math.Abs(Convert.ToDouble(textBox5.Text)); 
                    derajat = sederhanakan(derajat);

                    decimal hasil = 0;
                    if (derajat >= 0.0 && derajat <= 90.0) //kuadran 1
                        hasil = fungsi_cosinus(derajat);
                    else if (derajat >= 91.0 && derajat <= 180.0) //kuadran 2
                        hasil = -1 * fungsi_sinus(derajat - 90);
                    else if (derajat >= 181.0 && derajat <= 270.0) //kuadran 3
                        hasil = -1 * fungsi_cosinus(derajat - 180);
                    else if (derajat >= 271.0 && derajat <= 360.0) //kuadran 4
                        hasil = fungsi_sinus(derajat - 270);

                    textBox4.Text = hasil.ToString();
                }
            }

            else if (radioButton2.Checked == true && textBox6.Enabled == true) // cosinus radian
            {
                if (!validasi_x(textBox6.Text)) label11.Visible = true;
                if (!validasi(textBox8.Text)) label12.Visible = true;

                if (validasi_x(textBox6.Text) && validasi(textBox8.Text))
                {
                    radian = Convert.ToDouble(textBox6.Text);
                    derajat = Math.Abs(radian * 180.0 / Math.PI); derajat = sederhanakan(derajat);

                    decimal hasil = 0;
                    if (derajat >= 0.0 && derajat <= 90.0) //kuadran 1
                        hasil = fungsi_cosinus(derajat);
                    else if (derajat >= 91.0 && derajat <= 180.0) //kuadran 2
                        hasil = -1 * fungsi_sinus(derajat - 90);
                    else if (derajat >= 181.0 && derajat <= 270.0) //kuadran 3
                        hasil = -1 * fungsi_cosinus(derajat - 180);
                    else if (derajat >= 271.0 && derajat <= 360.0) //kuadran 4
                        hasil = fungsi_sinus(derajat - 270);

                    textBox4.Text = hasil.ToString();
                }
            }
            else if (radioButton3.Checked == true)
            {
                if (!validasi_x(textBox7.Text)) label11.Visible = true;
                if (!validasi(textBox8.Text)) label12.Visible = true;

                if (validasi_x(textBox7.Text) && validasi(textBox8.Text))
                {
                    double X = Convert.ToDouble(textBox7.Text);
                    int i = 0;
                    decimal nilai, sum = 0.0M, epsilon = Convert.ToDecimal(textBox8.Text);
                    listBox1.Items.Add("Pangkat Faktorial\t\tNilai");
                    nilai = (decimal)Math.Pow(X, i) / faktorial(i);

                    while (Math.Abs(nilai) > epsilon)
                    {
                        nilai = (decimal)Math.Pow(X, i) / faktorial(i);
                        sum += nilai;
                        listBox1.Items.Add(i.ToString() + "\t\t\t" + nilai.ToString());
                        i++;
                    }

                    textBox4.Text = sum.ToString();

                }

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                groupBox1.Visible = true;
                textBox8.MaxLength = 12;
            }
            else groupBox1.Visible = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true) textBox5.Enabled = true;
            else textBox5.Enabled = false;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true) textBox6.Enabled = true;
            else textBox6.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                groupBox1.Visible = true;
                textBox8.MaxLength = 12;
            }
            else groupBox1.Visible = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                groupBox2.Visible = true;
                textBox8.Text = "";
                textBox8.MaxLength = 6;
            }
            else groupBox2.Visible = false;
        }
    }
}
