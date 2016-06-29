using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class Form16 : Form
    {
        double x0, y0, xx,segmen, eksak;
        List<double> kumpulan_x = new List<double>();
        List<double> kumpulan_y = new List<double>();
        public Form16()
        {
            InitializeComponent();
        }

        private void Form16_Load(object sender, EventArgs e)
        {
            /*
            textBox1.Text = "Math.Pow(x,2) * y";
            textBox2.Text = "0";
            textBox3.Text = "1";
            textBox4.Text = "2";
            textBox5.Text = "4";
            textBox6.Text = "5";
            */
        }

        private void Form16_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult pilihan = MessageBox.Show("Apakah Anda yakin untuk keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pilihan == DialogResult.Yes)
                Application.ExitThread();
            else
                e.Cancel = true;
        }

        public static object Eval(string sCSCode, double ab, double bc)
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
            sb.Append("double y = " + bc.ToString() + ";");
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

        private void button1_Click(object sender, EventArgs e)
        {
            kumpulan_x.Clear(); kumpulan_y.Clear();
            label10.Text = "Error = ";
            dgv.Rows.Clear();
            bool sah = true;
            try
            {
                x0 = Convert.ToDouble(textBox2.Text);
                y0 = Convert.ToDouble(textBox3.Text);
                xx = Convert.ToDouble(textBox4.Text);
                segmen = Convert.ToDouble(textBox5.Text);
                eksak = Convert.ToDouble(textBox6.Text);
                double tes = fungsi(0, 0);
            }
            catch
            { MessageBox.Show("Data tidak valid !!"); sah = false; }

            if (sah)
            {
                kumpulan_x.Add(x0); kumpulan_y.Add(y0);
                progressBar1.Value = 0;
                progressBar1.Maximum = 100;
                progressBar1.Step = 1;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        public double fungsi(double x, double y)
        {
            string s = textBox1.Text;
            return (Convert.ToDouble(Eval(s, x, y).ToString()));
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            double delta = (xx - x0) / segmen;
            for (int i = 1; i <= segmen; i++)
            {
                double x_prev = kumpulan_x[kumpulan_x.Count - 1];
                double x1 =  x_prev + delta;
                kumpulan_x.Add(x1);

                double y_prev = kumpulan_y[kumpulan_y.Count - 1];
                double y1 = y_prev + (fungsi(x_prev,y_prev) * delta);
                kumpulan_y.Add(Math.Round(y1,6));

                backgroundWorker1.ReportProgress((int)Math.Ceiling((double)(i * 100) / segmen));
            }
            backgroundWorker1.ReportProgress(100);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            for (int i = 0; i < kumpulan_x.Count; i++)
            {
                dgv.Rows.Add();
                dgv[0, i].Value = i;
                dgv[1, i].Value = kumpulan_x[i];
                dgv[2, i].Value = kumpulan_y[i];
            }
            double error = Math.Abs(kumpulan_y[kumpulan_y.Count - 1] - eksak);
            label10.Text = String.Format("Error = {0}", error.ToString());
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label9.Text = String.Format("y({0})", textBox4.Text);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Dispose();
            f1.Show();
        }
    }
}
