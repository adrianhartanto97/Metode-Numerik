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
    public partial class Form5 : Form
    {
        double[,] M = new double[10, 11];
        double[,] N;
        double[] hasil;
        double[] hasil_akhir;
        int n;
        char[] pemisah;
        char[] tanda;
        char[] variabel=new char[10];
        int cacah = 0;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult pilihan = MessageBox.Show("Apakah Anda yakin untuk keluar ? ", "Konfirmasi", MessageBoxButtons.YesNo);
            if (pilihan == DialogResult.Yes)
                Application.ExitThread();
            else
                e.Cancel = true;
        }
        public static void cetak(double[,] M, int n,ListBox ls)
        {
            for (int i = 0; i < n; i++)
            {
                string s = "";
                for (int j = 0; j < n + 1; j++)
                {
                    if (j == n)
                        s += ("-> ");
                    if (CountDigitsAfterDecimal(M[i, j]) == 0)
                        s += String.Format("{0,-8:0.#} ", M[i, j]);
                    else
                    {
                        s += String.Format("{0,-7:0.##} ", M[i, j]);
                    }
                    
                }
                ls.Items.Add(s);
            }
            ls.Items.Add(" ");
        }
        public static void cetak2(double[,] M, int n, ListBox ls)
        {
            for (int i = 0; i < n; i++)
            {
                string s = "";
                for (int j = 0; j < n; j++)
                {
                        s += String.Format("{0,-9:0.##} ", M[i, j]);

                }
                ls.Items.Add(s);
            }
            ls.Items.Add(" ");
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        public static int CountDigitsAfterDecimal(double value)
        {
            bool start = false;
            int count = 0;
            foreach (var s in value.ToString())
            {
                if (s == '.')
                {
                    start = true;
                }
                else if (start)
                {
                    count++;
                }
            }

            return count;
        }

        static long gcd(long a, long b)
        {
            long t;
            while (b != 0)
            {
                t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        static long lcm(long a, long b)
        {
            return (a * b / gcd(a, b));
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

            for (int i = 0; i < kata.Length;i++ )
            {
                double var; int j=0;
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

        public void keArrayJumlah(string kal)
        {
            int i = kal.Length-1;
            string s = "";
            while (kal[i] != ' ' && kal[i] != '=')
            {
                s = kal[i].ToString() + s;
                i--;
            }
            M[cacah,n] = Convert.ToDouble(s);
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

        private void button1_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(comboBox1.Text);
            n = i;
                label3.Visible = true; textBox1.Visible = true;
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
            listBox1.Items.Clear(); listBox2.Items.Clear();
            cacah = 0;
            try
            {
                if (n > 1)//2
                {
                    keArray(textBox1.Text); keArrayJumlah(textBox1.Text);
                    cacah++;
                    keArray(textBox2.Text); keArrayJumlah(textBox2.Text);
                    cacah++;
                }
                if (n > 2)//3
                {
                    keArray(textBox3.Text); keArrayJumlah(textBox3.Text);
                    cacah++;
                }
                if (n > 3)//4
                { keArray(textBox4.Text); keArrayJumlah(textBox4.Text); cacah++; }
                if (n > 4)//5
                { keArray(textBox5.Text); keArrayJumlah(textBox5.Text); cacah++; }
                if (n > 5)//6
                { keArray(textBox6.Text); keArrayJumlah(textBox6.Text); cacah++; }
                if (n > 6)//7
                { keArray(textBox7.Text); keArrayJumlah(textBox7.Text); cacah++; }
                if (n > 7)//8
                { keArray(textBox8.Text); keArrayJumlah(textBox8.Text); cacah++; }
                if (n > 8)//9
                { keArray(textBox9.Text); keArrayJumlah(textBox9.Text); cacah++; }
                if (n > 9)//10
                { keArray(textBox10.Text); keArrayJumlah(textBox10.Text); cacah++; }
            }
            catch
            { MessageBox.Show("Data tidak Valid!!"); }
            bool sesat = false;
            for (int i = 1; i < n; i++)
            {
                int c = 0;
                for (int j = 0; j < i; j++)
                {
                    double s = M[i, j];
                    double t = M[c, j];
                    double s_1, t_1;
                    if (t == 0)
                    { sesat = true; break; }

                    if (s != 0)
                    {
                        int da = CountDigitsAfterDecimal(s);
                        int db = CountDigitsAfterDecimal(t);
                        int dc = da > db ? da : db;
                        int pengali = (int)Math.Pow(10, dc);
                        /*
                        long s_baru = Convert.ToInt32(s * pengali);
                        long t_baru = Convert.ToInt32(t * pengali);
                        
                        long kpk = lcm(s_baru, t_baru);
                        //kpk /= pengali;
                        double kpk_b = (double)kpk / pengali;
                        s_1 = kpk_b / t; t_1 = kpk_b / s;
                        s = s_1; t = t_1;
                        MessageBox.Show(s + " " + t);*/
                        for (int k = 0; k < n + 1; k++)
                        {
                            M[i, k] = (-1 * s * M[c, k]) + (t * M[i, k]);
                            M[i, k] = Math.Round(M[i, k], 5);
                        }
                    }
                    c++;
                    cetak(M, n,listBox1);
                }
                if (sesat) break;
            }
            if (sesat) listBox1.Items.Add("Tidak Terdefinisi");

            if (!sesat)
            {
                for (int i = 0; i < n; i++)
                {
                    double f = M[i, i];
                    for (int k = 0; k < n + 1; k++)
                    {
                        M[i, k] /= f;
                        M[i, k] = Math.Round(M[i, k], 3);
                    }
                    cetak(M, n, listBox2);
                }
            }

            if (!sesat)
            {
                sesat = false;
                for (int i = n - 2; i >= 0; i--)
                {
                    int c = n - 1;
                    for (int j = n - 1; j > i; j--)
                    {
                        double s = M[i, j];
                        double t = M[c, j];
                        double s_1, t_1;
                        if (t == 0)
                        { sesat = true; break; }

                        if (s != 0)
                        {
                            int da = CountDigitsAfterDecimal(s);
                            int db = CountDigitsAfterDecimal(t);
                            int dc = da > db ? da : db;
                            long pengali = (long)Math.Pow(10, dc);
                            for (int k = n; k >= 0; k--)
                            {
                                M[i, k] = (-1 * s * M[c, k]) + (t * M[i, k]);
                                M[i, k] = Math.Round(M[i, k], 3);

                            }
                        }
                        c--;
                        cetak(M, n, listBox2);
                    }
                    if (sesat) break;
                }
                if (sesat) listBox1.Items.Add("Tidak Terdefinisi");
            }
            string a = "Hasil : ";
            if (!sesat)
            {
                for (int i = 0; i < n; i++)
                {
                    a += (variabel[i] + " = " + Math.Round(M[i, n],3).ToString() + " ; ");
                }
            }
            else
                a += "Tidak ditemukan";
            label13.Text = a;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(comboBox2.Text);
            n = i;
            hasil = new double[n];
            hasil_akhir = new double[n];
            label27.Visible = true; textBox20.Visible = true;
            label26.Visible = true; textBox19.Visible = true;
            if (i > 2)
            { label25.Visible = true; textBox18.Visible = true; }
            if (i > 3)
            { label24.Visible = true; textBox17.Visible = true; }
            if (i > 4)
            { label23.Visible = true; textBox16.Visible = true; }
            if (i > 5)
            { label22.Visible = true; textBox15.Visible = true; }
            if (i > 6)
            { label21.Visible = true; textBox14.Visible = true; }
            if (i > 7)
            { label20.Visible = true; textBox13.Visible = true; }
            if (i > 8)
            { label19.Visible = true; textBox12.Visible = true; }
            if (i > 9)
            { label18.Visible = true; textBox11.Visible = true; }

            button5.Enabled = false;
        }

        static void copy_v1(ref double[,] N,ref double[,] M, int n)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    N[i,j] = M[i, j];
        }
        static void copy_v2(ref double[,] N, ref double[,] M, int n)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    N[i + 1, j + 1] = M[i, j];
        }

        static void manipulasi(int i,int n,ref double[,]N, ref double[]hasil)
        {
            for (int j = 0; j < n; j++)
            {
                N[j, i] = hasil[j];
            }
        }

        static double DeterminantGaussElimination(double[,] matrix)
        {
            int n = (int)Math.Sqrt(matrix.Length);
            int nm1 = n - 1;
            int kp1;
            double p;
            double det = 1;
            for (int k = 0; k < nm1; k++)
            {
                kp1 = k + 1;
                for (int i = kp1; i < n; i++)
                {
                    p = matrix[i, k] / matrix[k, k];
                    for (int j = kp1; j < n; j++)
                        matrix[i, j] = matrix[i, j] - p * matrix[k, j];
                }
            }
            for (int i = 0; i < n; i++)
                det = det * matrix[i, i];
            return det;
        }

        static double determinant(double[,]f,int x)
        {
          double pr,d=0;
          int p,j,q,t;
          double []c = new double[20];
          double [,]b = new double[20,20];
          if(x==2)
          {
            d=0;
            d=(f[1,1]*f[2,2])-(f[1,2]*f[2,1]);
            return(d);
           }
          else
          {
            for(j=1;j<=x;j++)
            {
              int r=1,s=1;
              for(p=1;p<=x;p++)
                {
                  for(q=1;q<=x;q++)
                    {
                      if(p!=1&&q!=j)
                      {
                        b[r,s]=f[p,q];
                        s++;
                        if(s>x-1)
                         {
                           r++;
                           s=1;
                          }
                       }
                     }
                 }
             for(t=1,pr=1;t<=(1+j);t++)
             pr=(-1)*pr;
             c[j]=pr*determinant(b,x-1);
             }
             for(j=1,d=0;j<=x;j++)
             {
               d=d+(f[1,j]*c[j]);
              }
             return(d);
           }
        }

        static double determinant_v2(double[,]a,int k)
        {
            double s = 1, det = 0;
            double[,] b = new double[25, 25];
            int i,j,m,n,c;
          if (k==1)
            {
             return (a[0,0]);
            }
          else
            {
             det=0;
             for (c=0;c<k;c++)
               {
                m=0;
                n=0;
                for (i=0;i<k;i++)
                  {
                    for (j=0;j<k;j++)
                      {
                        b[i,j]=0;
                        if (i != 0 && j != c)
                         {
                           b[m,n]=a[i,j];
                           if (n<(k-2))
                            n++;
                           else
                            {
                             n=0;
                             m++;
                             }
                           }
                       }
                     }
                  det=det + s * (a[0,c] * determinant(b,k-1));
                  s=-1 * s;
                  }
            }
            return (det);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            cacah = 0;
            N = new double[n, n];
            try
            {
                if (n > 1)//2
                {
                    keArray(textBox20.Text); keArrayHasil(textBox20.Text);
                    cacah++;
                    keArray(textBox19.Text); keArrayHasil(textBox19.Text);
                    cacah++;
                }
                if (n > 2)//3
                {
                    keArray(textBox18.Text); keArrayHasil(textBox18.Text);
                    cacah++;
                }
                if (n > 3)//4
                { keArray(textBox17.Text); keArrayHasil(textBox17.Text); cacah++; }
                if (n > 4)//5
                { keArray(textBox16.Text); keArrayHasil(textBox16.Text); cacah++; }
                if (n > 5)//6
                { keArray(textBox15.Text); keArrayHasil(textBox15.Text); cacah++; }
                if (n > 6)//7
                { keArray(textBox14.Text); keArrayHasil(textBox14.Text); cacah++; }
                if (n > 7)//8
                { keArray(textBox13.Text); keArrayHasil(textBox13.Text); cacah++; }
                if (n > 8)//9
                { keArray(textBox12.Text); keArrayHasil(textBox12.Text); cacah++; }
                if (n > 9)//10
                { keArray(textBox11.Text); keArrayHasil(textBox11.Text); cacah++; }
            }
           catch
           { MessageBox.Show("Data tidak Valid!!"); }
            double[,] temp = new double[n+1, n+1];
            copy_v2(ref temp, ref M, n);
            //double determinan = DeterminantGaussElimination(temp);
            double determinan = determinant(temp,n);
            listBox3.Items.Add("|A| = "+determinan.ToString());
            listBox3.Items.Add("");
            string s = "";
            for (int i = 0; i < n; i++)
            {
                copy_v1(ref N, ref M, n);
                manipulasi(i, n, ref N, ref hasil);              
                cetak2(N, n, listBox3);
                //double det = DeterminantGaussElimination(N);
                copy_v2(ref temp, ref N, n);
                double det = determinant(temp, n);
                listBox3.Items.Add("|Ax" + (i + 1).ToString() + "| = " + det);
                double x = det / determinan;
                listBox3.Items.Add(variabel[i] + " = " + "|Ax" + (i + 1).ToString() + "| / |A| = "+x.ToString());
                hasil_akhir[i] = Math.Round(x,3);
                listBox3.Items.Add("");
                listBox3.Items.Add("");
            }
            string a = "Hasil : ";
            for (int i = 0; i < n; i++)
            {
                a += (variabel[i] + " = " + hasil_akhir[i].ToString() + " ; ");
            }
            label16.Text = a;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt16(comboBox3.Text);
            n = i;
            hasil = new double[n];
            hasil_akhir = new double[n];
            label37.Visible = true; textBox30.Visible = true;
            label36.Visible = true; textBox29.Visible = true;
            if (i > 2)
            { label35.Visible = true; textBox28.Visible = true; }
            if (i > 3)
            { label34.Visible = true; textBox27.Visible = true; }
            if (i > 4)
            { label33.Visible = true; textBox26.Visible = true; }
            if (i > 5)
            { label32.Visible = true; textBox25.Visible = true; }
            if (i > 6)
            { label31.Visible = true; textBox24.Visible = true; }
            if (i > 7)
            { label30.Visible = true; textBox23.Visible = true; }
            if (i > 8)
            { label29.Visible = true; textBox22.Visible = true; }
            if (i > 9)
            { label17.Visible = true; textBox21.Visible = true; }

            button5.Enabled = false;
        }

        static void cofactor(double [,] num,int f, ref double[,]N, ref double[]hasil, ref double[] hasil_akhir )
        {
            double[,] b = new double[25, 25];
            double[,] fac = new double[25, 25];
            int p,q,m,n,i,j;
            for (q=0;q<f;q++)
            {
            for (p=0;p<f;p++)
            {
                m=0;
                n=0;
                for (i=0;i<f;i++)
                {
                for (j=0;j<f;j++)
                {
                    if (i != q && j != p)
                    {
                    b[m,n]=num[i,j];
                    if (n<(f-2))
                        n++;
                    else
                        {
                        n=0;
                        m++;
                        }
                    }
                }
                }
                double[,] temp = new double[f + 1, f + 1];
                copy_v2(ref temp, ref b, f);
                double det = determinant(temp,f-1);
                fac[q,p]=Math.Pow(-1,q + p) * det;
            }
            }
            transpose(num,fac,f,ref N,ref hasil,ref hasil_akhir);
        }

        static void transpose(double [,]num, double [,]fac,int r, ref double[,]N, ref double[]hasil, ref double[] hasil_akhir)
        {
          int i,j;
          double [,]b = new double[25,25];
          double d;
          double [,]inverse = new double[25,25];

          for (i=0;i<r;i++)
            {
             for (j=0;j<r;j++)
               {
                 b[i,j]=fac[j,i];
                }
            }
          double[,] temp = new double[r + 1, r + 1];
          copy_v2(ref temp, ref num, r);
            d=determinant(temp,r);
            
          for (i=0;i<r;i++)
            {
             for (j=0;j<r;j++)
               {
                    inverse[i,j]=b[i,j] / d;
                }
            }

           for (i=0;i<r;i++)
            {
             for (j=0;j<r;j++)
              {
                 N[i,j] = inverse[i,j];
              }
             }

            int k;
            for (i=0;i<r;i++)
            {
                double s=0;
                for (j=0;j<r;j++)
                    s+=(inverse[i,j] * hasil[j]);

                hasil_akhir[i] = s;
                //MessageBox.Show(s.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox4.Items.Clear();
            cacah = 0;
            N = new double[n, n];
            try
            {
                if (n > 1)//2
                {
                    keArray(textBox30.Text); keArrayHasil(textBox30.Text);
                    cacah++;
                    keArray(textBox29.Text); keArrayHasil(textBox29.Text);
                    cacah++;
                }
                if (n > 2)//3
                {
                    keArray(textBox28.Text); keArrayHasil(textBox28.Text);
                    cacah++;
                }
                if (n > 3)//4
                { keArray(textBox27.Text); keArrayHasil(textBox27.Text); cacah++; }
                if (n > 4)//5
                { keArray(textBox26.Text); keArrayHasil(textBox26.Text); cacah++; }
                if (n > 5)//6
                { keArray(textBox25.Text); keArrayHasil(textBox25.Text); cacah++; }
                if (n > 6)//7
                { keArray(textBox24.Text); keArrayHasil(textBox24.Text); cacah++; }
                if (n > 7)//8
                { keArray(textBox23.Text); keArrayHasil(textBox23.Text); cacah++; }
                if (n > 8)//9
                { keArray(textBox22.Text); keArrayHasil(textBox22.Text); cacah++; }
                if (n > 9)//10
                { keArray(textBox21.Text); keArrayHasil(textBox21.Text); cacah++; }
            }
            catch
            { MessageBox.Show("Data tidak Valid!!"); }
            
            cofactor(M, n, ref N, ref hasil,ref hasil_akhir);
            listBox4.Items.Add("Matriks Awal:");
            cetak2(M, n, listBox4);
            listBox4.Items.Add("");
            listBox4.Items.Add("Matriks Invers:");
            cetak2(N, n, listBox4);
            string a = "Hasil : ";
            for (int i = 0; i < n; i++)
                a += (variabel[i] + " = " + Math.Round(hasil_akhir[i],3).ToString() + " ; ");
            label39.Text = a;
        }

        
    }
}
