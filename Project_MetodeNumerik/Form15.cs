using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.CodeDom.Compiler;
using System.Text;
using Microsoft.CSharp;
using System.Reflection;

namespace Project_MetodeNumerik
{
    public partial class Form15 : Form
    {
        int segmen;
        double hasil_akhir;
        double bawah, atas, eksak;
        double a, b;
        double delta;
        int counter;
        string s="";
        List<double> t = new List<double>();
        List<double> x = new List<double>();
        List<string> kumpulan_ganjil = new List<string>();
        List<string> kumpulan_genap = new List<string>();
        List<double> nilai_ganjil = new List<double>();
        List<double> nilai_genap = new List<double>();
        double true_error, absolute_error;
        public Form15()
        {
            InitializeComponent();
        }

        public double fungsi(double x)
        {
            string s = textBox1.Text;
            try
            {
                return (Convert.ToDouble(Eval(s, x).ToString()));
            }
            catch
            {
                //MessageBox.Show("Fungsi yang diinput tidak dapat dikenali !!");
                return 0.0; 
            }
        }

        public double fungsi_integral(double x)
        {
            string s = textBox2.Text;
            try
            {
                return (Convert.ToDouble(Eval(s, x).ToString()));
            }
            catch
            {
                //MessageBox.Show("Fungsi yang diinput tidak dapat dikenali !!");
                return 0.0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t.Clear(); x.Clear();
            kumpulan_ganjil.Clear(); kumpulan_genap.Clear();
            nilai_ganjil.Clear(); nilai_genap.Clear();
            s = "";
            bool sah = true;
            double blabla;
            try
            {
                blabla = fungsi(0);
                blabla = fungsi_integral(0);
            }
            catch
            { 
                MessageBox.Show("Fungsi yang diinput tidak dapat dikenali !!");
                sah = false;
            }

            try
            {
                bawah = Convert.ToDouble(textBox3.Text);
                atas = Convert.ToDouble(textBox4.Text);
                segmen = Convert.ToInt16(textBox5.Text);
                counter = 2*segmen+2;
                delta = (atas - bawah) / segmen;
            }
            catch
            { MessageBox.Show("Data tidak valid !!"); }

            if (sah && segmen % 3 == 0)
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = 100;
                progressBar1.Step = 1;
                backgroundWorker1.RunWorkerAsync();
            }
            else {
                MessageBox.Show("Segmen harus kelipatan 3!!");
            }
            
        }

        public static object Eval(string sCSCode, double ab)
        {

            CSharpCodeProvider c = new CSharpCodeProvider();
            ICodeCompiler icc = c.CreateCompiler();
            CompilerParameters cp = new CompilerParameters();

            cp.ReferencedAssemblies.Add("system.dll");
            cp.ReferencedAssemblies.Add("system.xml.dll");
            cp.ReferencedAssemblies.Add("system.data.dll");
            cp.ReferencedAssemblies.Add("system.windows.forms.dll");
            cp.ReferencedAssemblies.Add("system.drawing.dll");

            cp.CompilerOptions = "/t:library";
            cp.GenerateInMemory = true;

            StringBuilder sb = new StringBuilder("");
            sb.Append("using System;\n");
            sb.Append("using System.Xml;\n");
            sb.Append("using System.Data;\n");
            sb.Append("using System.Data.SqlClient;\n");
            sb.Append("using System.Windows.Forms;\n");
            sb.Append("using System.Drawing;\n");

            sb.Append("namespace CSCodeEvaler{ \n");
            sb.Append("public class CSCodeEvaler{ \n");
            sb.Append("public object EvalCode(){\n");
            sb.Append("double hasil;\n");
            sb.Append("double x = " + ab.ToString() + ";");
            sb.Append("return " + sCSCode + "; \n");
            sb.Append("} \n");
            sb.Append("} \n");
            sb.Append("}\n");

            CompilerResults cr = icc.CompileAssemblyFromSource(cp, sb.ToString());
            
            if (cr.Errors.Count > 0)
            {
                /*
                MessageBox.Show("ERROR: " + cr.Errors[0].ErrorText,
                   "Error evaluating cs code", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);*/
                return null;
            }

            System.Reflection.Assembly a = cr.CompiledAssembly;
            object o = a.CreateInstance("CSCodeEvaler.CSCodeEvaler");

            Type t = o.GetType();
            MethodInfo mi = t.GetMethod("EvalCode");

            object s = mi.Invoke(o, null);
            return s;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            a = fungsi_integral(atas);
            backgroundWorker.ReportProgress(1*100/counter);
            b = fungsi_integral(bawah);
            backgroundWorker.ReportProgress(2*100/counter);
            eksak = a - b;

            double temp = bawah; 
            
            int c = 2;
            while (temp <= atas)
            {
                t.Add(Math.Round(temp,3));
                x.Add(Math.Round(fungsi(temp), 7));
                temp += delta;
                c++;
                backgroundWorker1.ReportProgress((int)Math.Ceiling((double)(c * 100) / counter));
            }
            double sigma = 0.0;
            double xx = bawah;
            for (int i = 1; i <= segmen - 1; i++)
            {
                xx += delta;
                if (i % 3 == 0)
                {
                    double te = x[i] * 2;
                    nilai_ganjil.Add(te);
                    sigma += te;
                    kumpulan_ganjil.Add(String.Format("2*F({0})", Math.Round(xx,3).ToString()));
                }
                else
                {
                    double te = x[i] * 3;
                    nilai_genap.Add(te);
                    sigma += te;
                    kumpulan_genap.Add(String.Format("3*F({0})", Math.Round(xx,3).ToString()));
                }
                c++;
                backgroundWorker1.ReportProgress((int)Math.Ceiling((double)(c * 100) / counter));
            }
            double I = (fungsi(atas) + fungsi(bawah) + sigma) * 3 * delta / 8;
            hasil_akhir = I;

            backgroundWorker1.ReportProgress(100);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { 
            int counter_a = 0, counter_b = 0;
            for (int i = 0; i < t.Count; i++)
            {
                dgv.Rows.Add();
                dgv[0, i].Value = t[i];
                dgv[1, i].Value = x[i];

                if (i!=0 && i % 3 == 0 && i != t.Count - 1)
                { dgv[3, i].Value = nilai_ganjil[counter_a]; counter_a++; }
                if (i % 3 != 0 && i != 0 && i != t.Count - 1)
                    dgv[2, i].Value = nilai_genap[counter_b++];
                if (i == 0)
                {
                    dgv[4, 0].Value = hasil_akhir;
                    dgv[4, 0].Style.BackColor = Color.Yellow;
                }
            }
            string ss = String.Format("Penyelesaian   :   {0}/{1} [ F({2})", ((atas - bawah)*3).ToString(), (8 * segmen).ToString(), bawah.ToString());
            foreach (string i in kumpulan_genap)
                ss += String.Format(" + {0}", i);
            foreach (string i in kumpulan_ganjil)
                ss += String.Format(" + {0}", i);

            ss += String.Format(" + F({0}) ]", atas.ToString());
            label13.Visible = true;
            label13.Text = ss;
            label12.Text = "Eksak  =  " + eksak.ToString();
            label12.Visible = true;
            true_error = eksak - hasil_akhir;
            absolute_error = Math.Abs(true_error / eksak * 100);
            label10.Text = String.Format("True Error                              =  {0:F20}",true_error);
            label11.Text = String.Format("Absolute Relative Error       =  {0:F20} %" , absolute_error);
        }

        private void Form15_Load(object sender, EventArgs e)
        {      
            /*textBox1.Text = "2*Math.Log(Math.Pow(x,Math.E)) - Math.Log((2000+x)/x)";
            //textBox1.Text = "2000 * (Math.Log(140000) - Math.Log(140000 - 2100 * x)) - 9.8 * x";
            textBox2.Text = "-x*Math.Log((x+2000)/x)-2000*Math.Log(x+2000)+2*Math.E*(x*Math.Log(x)-x)";
            textBox3.Text = "8";
            textBox4.Text = "30";
            textBox5.Text = "9";*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Dispose();
            f1.Show();
        }

        private void Form15_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult pilihan = MessageBox.Show("Apakah Anda yakin untuk keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pilihan == DialogResult.Yes)
                Application.ExitThread();
            else
                e.Cancel = true;
        }
    }
}
