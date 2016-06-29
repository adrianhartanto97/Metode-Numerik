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
    public partial class Form11 : Form
    {
        int n, pangkat = 1;
        double delta = 1.0;
        double xx;
        List<double> x = new List<double>();
        List<double> y = new List<double>();
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_FormClosing(object sender, FormClosingEventArgs e)
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

            label3.Visible = true; label14.Visible = true; textBox11.Visible = true;
            label15.Visible = true; textBox12.Visible = true;
            label16.Visible = true; textBox13.Visible = true;
            button3.Visible = true;
            label17.Visible = true; label18.Visible = true; comboBox2.Visible = true; comboBox3.Visible = true;

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

        private void button3_Click(object sender, EventArgs e)
        {
            bool sah = true;
            listBox1.Items.Clear();
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
                { keArray(textBox4.Text);  }
                if (n > 4)//5
                { keArray(textBox5.Text);  }
                if (n > 5)//6
                { keArray(textBox6.Text);  }
                if (n > 6)//7
                { keArray(textBox7.Text);  }
                if (n > 7)//8
                { keArray(textBox8.Text);  }
                if (n > 8)//9
                { keArray(textBox9.Text);  }
                if (n > 9)//10
                { keArray(textBox10.Text);  }

                xx = Convert.ToDouble(textBox11.Text);
                pangkat = Convert.ToInt16(textBox12.Text);
                delta = Convert.ToDouble(textBox13.Text);
            }
            catch
            { MessageBox.Show("Data tidak Valid!!"); sah = false; }

            if (sah)
            {
                Turunan turunan = new Turunan(n, pangkat, delta, xx);
                turunan.tambahKoordinat(x, y);
                turunan.createRegresi();
                List<string> str = turunan.tampilKoefRegresi();
                for (int i = 0; i < str.Count; i++)
                    listBox1.Items.Add(str[i]);
                listBox1.Items.Add("");

                listBox1.Items.Add(turunan.tampilFungsiRegresi()); listBox1.Items.Add("");

                str = turunan.tampilTabelRegresi();
                for (int i = 0; i < str.Count; i++)
                    listBox1.Items.Add(str[i]);
                listBox1.Items.Add(""); listBox1.Items.Add("");

                turunan.generateTurunan();

                if (comboBox2.Text == "pertama")
                {
                    str = turunan.cetakEksak("pertama");
                    for (int i = 0; i < str.Count; i++)
                        listBox1.Items.Add(str[i]);
                    listBox1.Items.Add("");
                    listBox1.Items.Add(String.Format("Hasil Eksak f'({0})  =  {1}", xx, turunan.hasilturunan1(xx)));
                    listBox1.Items.Add("------------------------------------------------------------------");

                    listBox1.Items.Add("");

                    if (comboBox3.Text == "FDA")
                    {
                        str = turunan.turunan1FDA();
                        for (int i = 0; i < str.Count; i++)
                            listBox1.Items.Add(str[i]);
                    }
                    else if (comboBox3.Text == "BDA")
                    {
                        str = turunan.turunan1BDA();
                        for (int i = 0; i < str.Count; i++)
                            listBox1.Items.Add(str[i]);
                    }
                    else
                    {
                        str = turunan.turunan1CDA();
                        for (int i = 0; i < str.Count; i++)
                            listBox1.Items.Add(str[i]);
                    }
                }

                else if (comboBox2.Text == "kedua")
                {
                    str = turunan.cetakEksak("kedua");
                    for (int i = 0; i < str.Count; i++)
                        listBox1.Items.Add(str[i]);
                    listBox1.Items.Add("");
                    listBox1.Items.Add(String.Format("Hasil Eksak f''({0})  =  {1}",xx,turunan.hasilturunan2(xx)));
                    listBox1.Items.Add("------------------------------------------------------------------");

                    listBox1.Items.Add("");

                    if (comboBox3.Text == "FDA")
                    {
                        str = turunan.turunan2FDA();
                        for (int i = 0; i < str.Count; i++)
                            listBox1.Items.Add(str[i]);
                    }

                    else if (comboBox3.Text == "CDA")
                    {
                        str = turunan.turunan2CDA();
                        for (int i = 0; i < str.Count; i++)
                            listBox1.Items.Add(str[i]);
                    }
                }

                else if (comboBox2.Text == "ketiga")
                {
                    str = turunan.cetakEksak("ketiga");
                    for (int i = 0; i < str.Count; i++)
                        listBox1.Items.Add(str[i]);
                    listBox1.Items.Add("");
                    listBox1.Items.Add(String.Format("Hasil Eksak f'''({0})  =  {1}", xx, turunan.hasilturunan3(xx)));
                    listBox1.Items.Add("------------------------------------------------------------------");

                    listBox1.Items.Add("");

                    if (comboBox3.Text == "FDA")
                    {
                        str = turunan.turunan3FDA();
                        for (int i = 0; i < str.Count; i++)
                            listBox1.Items.Add(str[i]);
                    }
                    else if (comboBox3.Text == "CDA")
                    {
                        str = turunan.turunan3CDA();
                        for (int i = 0; i < str.Count; i++)
                            listBox1.Items.Add(str[i]);
                    }
                }
                else
                {
                    str = turunan.cetakEksak("keempat");
                    for (int i = 0; i < str.Count; i++)
                        listBox1.Items.Add(str[i]);
                    listBox1.Items.Add("");
                    listBox1.Items.Add(String.Format("Hasil Eksak f''''({0})  =  {1}", xx, turunan.hasilturunan4(xx)));
                    listBox1.Items.Add("------------------------------------------------------------------");

                    listBox1.Items.Add("");

                    if (comboBox3.Text == "FDA")
                    {
                        str = turunan.turunan4FDA();
                        for (int i = 0; i < str.Count; i++)
                            listBox1.Items.Add(str[i]);
                    }
                    else if (comboBox3.Text == "CDA")
                    {
                        str = turunan.turunan4CDA();
                        for (int i = 0; i < str.Count; i++)
                            listBox1.Items.Add(str[i]);
                    }
                }
            }
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            /*
            textBox1.Text = "0  -10";
            textBox2.Text = "1  -2";
            textBox3.Text = "2  6";
            textBox4.Text = "3  146";
            textBox5.Text = "4  910";
            textBox6.Text = "5  3390";
            textBox12.Text = "5"; textBox11.Text = "0"; textBox13.Text = "0.0001";
            */
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Dispose();
        }
    }
}
