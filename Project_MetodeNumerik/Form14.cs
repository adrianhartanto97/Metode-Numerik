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
    public partial class Form14 : Form
    {
        double bawah, atas, p, q, r, s, segmen, hasil, eksak, a, b;
        double true_error, absolute_error;
        List<double> t = new List<double>();
        List<double> x = new List<double>();
        List<string> kumpulan_ganjil = new List<string>();
        List<string> kumpulan_genap = new List<string>();
        List<double> nilai_ganjil = new List<double>();
        List<double> nilai_genap = new List<double>();
        public Form14()
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

        private void Form14_Load(object sender, EventArgs e)
        {
 
        }

        private void Form14_FormClosing(object sender, FormClosingEventArgs e)
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
            kumpulan_ganjil.Clear(); kumpulan_genap.Clear();
            nilai_ganjil.Clear(); nilai_genap.Clear();
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

                sah = false;
                if (segmen % 2 == 0) sah = true;
                if (!sah)
                    MessageBox.Show("Segmen harus genap !!");
            }
            catch
            { MessageBox.Show("Data tidak valid !!"); sah = false; }

            if (sah)
            {
                a = p * ((r * atas + Math.Log(q - r * atas) * (q - r * atas) - q) / r + Math.Log(q) * atas) - ((s / 2) * atas * atas);
                b = p * ((r * bawah + Math.Log(q - r * bawah) * (q - r * bawah) - q) / r + Math.Log(q) * bawah) - ((s / 2) * bawah * bawah);
                eksak = a - b;
                

                double temp = bawah, delta = (atas - bawah) / segmen;
                while (temp <= atas)
                {
                    t.Add(temp);
                    x.Add(Math.Round(fungsi(temp), 7));
                    temp += delta;
                }

                double xx = bawah;
                double I = fungsi(atas) + fungsi(bawah);
                double sigma = 0;
                for (int i = 1; i <= segmen - 1; i++)
                {
                    xx += delta;
                    if (i % 2 == 1)
                    {
                        sigma += 4 * fungsi(xx);
                        kumpulan_ganjil.Add(String.Format("4*F({0})",xx.ToString()));
                        nilai_ganjil.Add(4 * fungsi(xx));
                    }
                    else
                    {
                        sigma += 2 * fungsi(xx);
                        kumpulan_genap.Add(String.Format("2*F({0})", xx.ToString()));
                        nilai_genap.Add(2 * fungsi(xx));
                    }
                }
                I = (I + sigma) * (delta / 3);
                string ss = String.Format("Penyelesaian   :   {0}/{1} [ F({2})",(atas-bawah).ToString(),(3*segmen).ToString(),bawah.ToString());
                foreach (string i in kumpulan_ganjil)
                    ss += String.Format(" + {0}",i);
                foreach (string i in kumpulan_genap)
                    ss += String.Format(" + {0}", i);

                ss += String.Format(" + F({0}) ]",atas.ToString());
                label13.Visible = true;
                label13.Text = ss;
                hasil = I;
                true_error = eksak - hasil;
                absolute_error = Math.Abs(true_error / eksak * 100);

                int counter_a = 0, counter_b = 0;
                for (int i = 0; i < t.Count; i++)
                {
                    dgv.Rows.Add();
                    dgv[0, i].Value = t[i];
                    dgv[1, i].Value = x[i];
                    if (i%2==1)
                        dgv[2, i].Value = nilai_ganjil[counter_a++];
                    if (i % 2 == 0 && i != 0 && i != t.Count - 1)
                        dgv[3, i].Value = nilai_genap[counter_b++];
                    if (i == 0)
                    {
                        dgv[4, 0].Value = hasil;
                        dgv[4, 0].Style.BackColor = Color.Yellow;
                    }
                }
                label10.Text = String.Format("True Error                              =  {0:F20}", true_error);
                label11.Text = String.Format("Absolute Relative Error       =  {0:F20} %", absolute_error);
                label12.Text = "Eksak  =  " + eksak.ToString();
                label12.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form f1 = new Form1();
            f1.Show();
            this.Dispose();
        }

        public static object Eval(string sCSCode)
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
            sb.Append("return " + sCSCode + "; \n");
            sb.Append("} \n");
            sb.Append("} \n");
            sb.Append("}\n");

            CompilerResults cr = icc.CompileAssemblyFromSource(cp, sb.ToString());
            if (cr.Errors.Count > 0)
            {
                MessageBox.Show("ERROR: " + cr.Errors[0].ErrorText,
                   "Error evaluating cs code", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return null;
            }

            System.Reflection.Assembly a = cr.CompiledAssembly;
            object o = a.CreateInstance("CSCodeEvaler.CSCodeEvaler");

            Type t = o.GetType();
            MethodInfo mi = t.GetMethod("EvalCode");

            object s = mi.Invoke(o, null);
            return s;

        }
    }
}
