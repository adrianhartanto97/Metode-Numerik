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
    public partial class Form13 : Form
    {
        double bawah, atas, p, q, r, s, segmen, hasil, eksak, a, b;
        double true_error, absolute_error;
        List<double> t = new List<double>();
        List<double> x = new List<double>();
        List<double> kumpulan_segmen = new List<double>();
        public Form13()
        {
            InitializeComponent();
            /*
            textBox1.Text = "8";
            textBox2.Text = "30";
            textBox3.Text = "2000";
            textBox4.Text = "140000";
            textBox5.Text = "2100";
            textBox6.Text = "9.8";
            textBox7.Text = "2";*/
        }

        private void Form13_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult pilihan = MessageBox.Show("Apakah Anda yakin untuk keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pilihan == DialogResult.Yes)
                Application.ExitThread();
            else
                e.Cancel = true;
        }

        public double fungsi(double ab)
        {
            return p * (Math.Log(q) - Math.Log(q - r * ab)) - s * ab;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t.Clear();
            x.Clear();
            kumpulan_segmen.Clear();
            dgv.Rows.Clear();
            bool sah = true;
            try
            {
                bawah = Convert.ToDouble(textBox1.Text);
                atas = Convert.ToDouble(textBox2.Text);
                p = Convert.ToDouble(textBox3.Text);
                q = Convert.ToDouble(textBox4.Text);
                r = Convert.ToDouble(textBox5.Text);
                s = Convert.ToDouble(textBox6.Text);
                segmen = Convert.ToDouble(textBox7.Text);
            }
            catch
            { MessageBox.Show("Data tidak valid !!"); sah = false; }

            if (sah)
            {
                a = p * ((r*atas + Math.Log(q - r*atas) * (q-r*atas) - q)/r  + Math.Log(q) * atas) - ((s/2)*atas*atas);
                b = p * ((r * bawah + Math.Log(q - r * bawah) * (q - r * bawah) - q) / r + Math.Log(q) * bawah) - ((s/2) * bawah * bawah);
                eksak = a - b;

                double temp = bawah, delta = (atas - bawah)/segmen;
                while (temp <= atas)
                {
                    t.Add(temp);
                    x.Add(Math.Round(fungsi(temp),7));
                    temp += delta;
                }

                hasil=0.0;
                for (int i = 0; i < x.Count - 1; i++)
                { 
                    double f = (t[i+1] - t[i])*(x[i+1] + x[i])/2;
                    kumpulan_segmen.Add(f);
                    hasil += f;
                }
                true_error = eksak - hasil;
                absolute_error = Math.Abs(true_error / eksak * 100);

                dgv.Columns[2].HeaderText = "Segmen 1-"+segmen.ToString();

                for (int i = 0; i < t.Count; i++)
                {
                    dgv.Rows.Add();
                    dgv[0,i].Value = t[i];
                    dgv[1,i].Value = x[i];

                    if (i < t.Count - 1)
                        dgv[2, i].Value = kumpulan_segmen[i];
                    if (i == 0)
                    {
                        dgv[3, 0].Value = hasil;
                        dgv[3, 0].Style.BackColor = Color.Yellow;
                    }
                }
                label10.Text = String.Format("True Error                              =  {0:F20}", true_error);
                label11.Text = String.Format("Absolute Relative Error       =  {0:F20} %", absolute_error);
                label12.Text = "Eksak  =  " + eksak.ToString();
                label12.Visible = true;
            }
        }

        private void Form13_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Dispose();
            f1.Show();
        }

        
    }
}
