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
    public partial class Form12 : Form
    {
        int n, pangkat = 1;
        double delta = 1.0;
        double xx;
        List<double> x = new List<double>();
        List<double> y = new List<double>();
        double[] arrEksak;
        double[] turunan1;
        double[] turunan2;
        double[] turunan3;
        double[] turunan4;
        int waktu;
        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            /*
            textBox1.Text = "0  -10.1";
            textBox2.Text = "1  -1.98";
            textBox3.Text = "2  6";
            textBox4.Text = "3  146";
            textBox5.Text = "4  910";
            textBox6.Text = "5  3391";
            textBox12.Text = "5"; textBox11.Text = "2.55"; textBox13.Text = "2";
            textBox14.Text = "2 -5 2 -1 10 -10";
             */
        }

        private void Form12_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult pilihan = MessageBox.Show("Apakah Anda yakin untuk keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pilihan == DialogResult.Yes)
                Application.ExitThread();
            else
                e.Cancel = true;
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

            arrEksak = new double[n];

            label3.Visible = true; label14.Visible = true; textBox11.Visible = true;
            label15.Visible = true; textBox12.Visible = true;
            label16.Visible = true; textBox13.Visible = true;
            label17.Visible = true; textBox14.Visible = true;
            label19.Visible = true; comboBox2.Visible = true;
            button2.Visible = true;

            button1.Enabled = false;
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
            x.Add(Convert.ToDouble(kiri));
            y.Add(Convert.ToDouble(kanan));
        }

        public void keArrayfungsi(string teks)
        {
            teks = teks.Trim();
            List<string> arr = new List<string>();
            bool lanjut = true;
            string kiri = ""; string kanan = teks;
            while (lanjut)
            {
                lanjut=false;
                string s = ""; kiri = "";
                bool ketemu = false;
                for (int i = 0; i < kanan.Length; i++)
                {
                    if (kanan[i] == ' ')
                    {
                        ketemu = true;
                        for (int j = 0; j < i; j++) kiri += kanan[j];
                        for (int j = i; j < kanan.Length; j++) s += kanan[j];
                        s = s.Trim();
                        kanan = s;
                        arr.Add(kiri);
                        //MessageBox.Show(kiri+ " dan " + kanan);
                        break;
                    }
                }
                if (!ketemu)
                {
                    for (int j = 0; j < kanan.Length; j++) s += kanan[j];
                    arr.Add(s);
                    break;
                }
                int c = 0;
                for (int i = 0; i < kanan.Length; i++)
                {
                    if (kanan[i] != ' ') { lanjut = true; break; }
                }
            }
            for (int i = 0; i < arrEksak.Length;i++ )
            {
                arrEksak[i] = Convert.ToDouble(arr[arr.Count-1-i]);
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            bool sah = true;
            waktu = 5;
            dgv1.Rows.Clear();
            dgv2.Rows.Clear();
            x.Clear();
            y.Clear();
            try
            {
                if (n > 1)//2
                {
                    keArray(textBox1.Text);
                    keArray(textBox2.Text);
                }
                if (n > 2)//3
                {
                    keArray(textBox3.Text);
                }
                if (n > 3)//4
                { keArray(textBox4.Text); }
                if (n > 4)//5
                { keArray(textBox5.Text); }
                if (n > 5)//6
                { keArray(textBox6.Text); }
                if (n > 6)//7
                { keArray(textBox7.Text); }
                if (n > 7)//8
                { keArray(textBox8.Text); }
                if (n > 8)//9
                { keArray(textBox9.Text); }
                if (n > 9)//10
                { keArray(textBox10.Text); }

                xx = Convert.ToDouble(textBox11.Text);
                pangkat = Convert.ToInt16(textBox12.Text);
                delta = Convert.ToDouble(textBox13.Text);
                keArrayfungsi(textBox14.Text);
            }
                
            catch
            { MessageBox.Show("Data tidak Valid!!"); sah = false; }

            if (sah)
            {
                if (n > 1) //2
                {
                    turunan1 = new double[arrEksak.Length - 1];
                    for (int i = 1; i < arrEksak.Length; i++)
                    {
                        turunan1[i - 1] = arrEksak[i] * i;
                    }
                }

                if (n > 2) //3
                {
                    turunan2 = new double[turunan1.Length - 1];
                    for (int i = 1; i < turunan1.Length; i++)
                    {
                        turunan2[i - 1] = turunan1[i] * i;
                    }
                }

                if (n > 3) //4
                {
                    turunan3 = new double[turunan2.Length - 1];
                    for (int i = 1; i < turunan2.Length; i++)
                    {
                        turunan3[i - 1] = turunan2[i] * i;
                    }
                }

                if (n > 4) //5
                {
                    turunan4 = new double[turunan3.Length - 1];
                    for (int i = 1; i < turunan3.Length; i++)
                    {
                        turunan4[i - 1] = turunan3[i] * i;
                    }
                }

                Turunanv2 turunan = new Turunanv2(n, pangkat, xx);
                turunan.tambahKoordinat(x, y);
                turunan.createRegresi();
                label18.Visible = true;
                label18.Text = String.Format("Fungsi Prediksi Regresi :   {0}", turunan.tampilFungsiRegresi());

                double delta_temp = delta;
                    timer1.Enabled = true;
                    timer1.Start();
                    int counter = 0, cacah = 0;
                    double epsilon = 0.5;
                    bool selesai1 = false, selesai2 = false, selesai3 = false, selesai4 = false, selesai5 = false, selesai6=false;
               // /*
                    if (comboBox2.Text == "pertama")
                    {
                        double eksak = fungsi(turunan1, xx);
                        while (cacah < 6)
                        {
                            dgv1.Rows.Add();

                            dgv1[0, counter].Value = counter;
                            dgv1[1, counter].Value = delta;
                            
                            dgv1[2, counter].Value = eksak;

                            if (!selesai1)
                            {
                                double t = turunan.turunan1FDA1(delta);
                                dgv1[3, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv1[3, counter].Style.BackColor = Color.Yellow; selesai1 = true; }
                            }
                            else
                            {
                                dgv1[3, counter].Value = "Sudah Konvergen";
                            }

                            if (!selesai2)
                            {
                                double t = turunan.turunan1FDA2(delta);
                                dgv1[4, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv1[4, counter].Style.BackColor = Color.Yellow; selesai2 = true; }
                            }
                            else
                            {
                                dgv1[4, counter].Value = "Sudah Konvergen";
                            }

                            if (!selesai3)
                            {
                                double t = turunan.turunan1CDA1(delta);
                                dgv1[5, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv1[5, counter].Style.BackColor = Color.Yellow; selesai3 = true; }
                            }
                            else
                            {
                                dgv1[5, counter].Value = "Sudah Konvergen";
                            }

                            if (!selesai4)
                            {
                                double t = turunan.turunan1CDA2(delta);
                                dgv1[6, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv1[6, counter].Style.BackColor = Color.Yellow; selesai4 = true; }
                            }
                            else
                                dgv1[6, counter].Value = "Sudah Konvergen";

                            if (!selesai5)
                            {
                                double t = turunan.turunan1BDA1(delta);
                                dgv1[7, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv1[7, counter].Style.BackColor = Color.Yellow; selesai5 = true; }
                            }
                            else
                                dgv1[7, counter].Value = "Sudah Konvergen";

                            if (!selesai6)
                            {
                                double t = turunan.turunan1BDA2(delta);
                                dgv1[8, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv1[8, counter].Style.BackColor = Color.Yellow; selesai6 = true; }
                            }
                            else
                                dgv1[8, counter].Value = "Sudah Konvergen";

                            delta /= 2;
                            counter++;
                        }
                        timer1.Stop();
                        tabControl1.SelectedTab = tabPage1;
                        //*/
                    }

                    else if (comboBox2.Text == "kedua")
                    {
                        tabControl1.SelectedTab = tabPage2;
                        counter = 0; cacah = 0;
                        epsilon = 0.5;
                        bool selesai7 = false, selesai8 = false, selesai9 = false, selesai10 = false, selesai11 = false, selesai12 = false;
                        delta = delta_temp;

                        double eksak = fungsi(turunan2, xx);
                        while (cacah < 6)
                        {
                            dgv2.Rows.Add();

                            dgv2[0, counter].Value = counter;
                            dgv2[1, counter].Value = delta;
                            dgv2[2, counter].Value = eksak;

                            if (!selesai7)
                            {
                                double t = turunan.turunan2FDA1(delta);
                                dgv2[3, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv2[3, counter].Style.BackColor = Color.Yellow; selesai7 = true; }
                            }
                            else
                            {
                                dgv2[3, counter].Value = "Sudah Konvergen";
                            }

                            if (!selesai8)
                            {
                                double t = turunan.turunan2FDA2(delta);
                                dgv2[4, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv2[4, counter].Style.BackColor = Color.Yellow; selesai8 = true; }
                            }
                            else
                            {
                                dgv2[4, counter].Value = "Sudah Konvergen";
                            }

                            if (!selesai9)
                            {
                                double t = turunan.turunan2CDA1(delta);
                                dgv2[5, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv2[5, counter].Style.BackColor = Color.Yellow; selesai9 = true; }
                            }
                            else
                            {
                                dgv2[5, counter].Value = "Sudah Konvergen";
                            }

                            if (!selesai10)
                            {
                                double t = turunan.turunan2CDA2(delta);
                                dgv2[6, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv2[6, counter].Style.BackColor = Color.Yellow; selesai10 = true; }
                            }
                            else
                                dgv2[6, counter].Value = "Sudah Konvergen";

                            if (!selesai11)
                            {
                                double t = turunan.turunan2BDA1(delta);
                                dgv2[7, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv2[7, counter].Style.BackColor = Color.Yellow; selesai11 = true; }
                            }
                            else
                                dgv2[7, counter].Value = "Sudah Konvergen";

                            if (!selesai12)
                            {
                                double t = turunan.turunan2BDA2(delta);
                                dgv2[8, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv2[8, counter].Style.BackColor = Color.Yellow; selesai12 = true; }
                            }
                            else
                                dgv2[8, counter].Value = "Sudah Konvergen";

                            delta /= 2;
                            counter++;
                        }
                    }

                    else if (comboBox2.Text == "ketiga")
                    {
                        counter = 0; cacah = 0;
                        epsilon = 0.5;
                        selesai1 = false; selesai2 = false; selesai3 = false;
                        delta = delta_temp;

                        double eksak = fungsi(turunan3, xx);
                        while (cacah < 3)
                        {
                            dgv3.Rows.Add();

                            dgv3[0, counter].Value = counter;
                            dgv3[1, counter].Value = delta;                      
                            dgv3[2, counter].Value = eksak;

                            if (!selesai1)
                            {
                                double t = turunan.turunan3FDA(delta);
                                dgv3[3, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv3[3, counter].Style.BackColor = Color.Yellow; selesai1 = true; }
                            }
                            else
                            {
                                dgv3[3, counter].Value = "Sudah Konvergen";
                            }

                            if (!selesai2)
                            {
                                double t = turunan.turunan3CDA(delta);
                                dgv3[4, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv3[4, counter].Style.BackColor = Color.Yellow; selesai2 = true; }
                            }
                            else
                            {
                                dgv3[4, counter].Value = "Sudah Konvergen";
                            }

                            if (!selesai3)
                            {
                                double t = turunan.turunan3BDA(delta);
                                dgv3[5, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv3[5, counter].Style.BackColor = Color.Yellow; selesai3 = true; }
                            }
                            else
                                dgv3[5, counter].Value = "Sudah Konvergen";

                            delta /= 2;
                            counter++;
                        }
                        tabControl1.SelectedTab = tabPage3;
                    }
                    else if (comboBox2.Text == "keempat")
                    {
                        counter = 0; cacah = 0;
                        epsilon = 0.6;
                        selesai1 = false; selesai2 = false; selesai3 = false;
                        delta = delta_temp;

                        double eksak = fungsi(turunan4, xx);
                        while (cacah < 3)
                        {
                            dgv4.Rows.Add();

                            dgv4[0, counter].Value = counter;
                            dgv4[1, counter].Value = delta;
                            dgv4[2, counter].Value = eksak;

                            if (!selesai1)
                            {
                                double t = turunan.turunan4FDA(delta);
                                dgv4[3, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv4[3, counter].Style.BackColor = Color.Yellow; selesai1 = true; }
                            }
                            else
                            {
                                dgv4[3, counter].Value = "Sudah Konvergen";
                            }

                            if (!selesai2)
                            {
                                double t = turunan.turunan4CDA(delta);
                                dgv4[4, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv4[4, counter].Style.BackColor = Color.Yellow; selesai2 = true; }
                            }
                            else
                            {
                                dgv4[4, counter].Value = "Sudah Konvergen";
                            }

                            if (!selesai3)
                            {
                                double t = turunan.turunan4BDA(delta);
                                dgv4[5, counter].Value = t;
                                if (Math.Abs(t - eksak) < epsilon) { cacah++; dgv4[5, counter].Style.BackColor = Color.Yellow; selesai3 = true; }
                            }
                            else
                                dgv4[5, counter].Value = "Sudah Konvergen";

                            delta /= 2;
                            counter++;
                        }
                        tabControl1.SelectedTab = tabPage4;
                    }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            waktu--;
            if (waktu == 0) dgv1.Rows.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Dispose();
            f1.Show();
        }
    }
}
