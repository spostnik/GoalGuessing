using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoalGuessing
{
    public partial class FormGG : Form
    {
        private int host = 0, guest = 0;
        private float time = 0.0F;
        private int from_ = 0, to_ = 0;
        private float[,] stat = new float[7,7];
        private float hwins = 50F, gwins = 50F, draw = 0F, outside = 0F;
        private List<GroupBox> goalsGroup = new List<GroupBox>();

        public FormGG()
        {
            InitializeComponent();
            _initStat();
            _initChart();
            _initGoals();
            Console.WriteLine(ABOUT());
        }
        private void _initGoals()
        {
            //add checkboxes to game goals list
            goalsGroup.Add(groupBox1);
            goalsGroup.Add(groupBox2);
            goalsGroup.Add(groupBox3);
            goalsGroup.Add(groupBox4);
            goalsGroup.Add(groupBox5);
            goalsGroup.Add(groupBox6);
        }
        private void _initRange()
        {
            //add checkboxes to range list
            goalsGroup.Add(groupBox1);
            goalsGroup.Add(groupBox2);
            goalsGroup.Add(groupBox3);
            goalsGroup.Add(groupBox4);
            goalsGroup.Add(groupBox5);
            goalsGroup.Add(groupBox6);
        }
        private void _initStat()
        {
            string[] lines = File.ReadAllLines("data\\stat_tab.dat");
            foreach(string line in lines)
            {
                string[] items = line.Split();
                if (items[0] == "Host") continue;
                int hh = int.Parse(items[0]);
                int gg = int.Parse(items[1]);
                float pp = float.Parse(items[2]);
                stat[hh, gg] = pp / 100.0F;
                //Console.WriteLine(items[0]);
            }
        }
        private void _initChart()
        {
            //Console.WriteLine("test");
            hwins = gwins = draw = outside = 0F;
            for ( int h = 0; h < 7; h++)
            {
                for ( int g = 0; g < 7; g++)
                {
                    if (h == g) draw += stat[h,g];
                    else if (h > g) hwins += stat[h, g];
                    else gwins += stat[h, g];
                }
            }
            hwins = (float)Math.Round( 100.0F * hwins, 1);
            gwins = (float)Math.Round( 100.0F * gwins, 1);
            draw = (float)Math.Round( 100.0F * draw, 1);
            outside = (float)Math.Round(outside, 1);

            chart1.Series["answers"].Points.AddXY("host wins", hwins );
            chart1.Series["answers"].Points.AddXY("guest wins", gwins );
            chart1.Series["answers"].Points.AddXY("draw", draw );
            chart1.Series["answers"].Points.AddXY("outside", outside );
            labelHW.Text = hwins.ToString() + " %";
            labelGW.Text = gwins.ToString() + " %";
            labelDR.Text = draw.ToString() + " %";
            labelOR.Text = outside.ToString() + " %";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1H_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }
        private void radioButton1H_Click(object sender, EventArgs e)
        {
            radioButton1H.Checked = !radioButton1H.Checked;
        }
        private void _update_score()
        {
            int h = 0; //host goals
            int g = 0; //guest goals
            foreach (var grp in goalsGroup)
            {
                foreach (var radioButt in grp.Controls.OfType<RadioButton>()) 
                {
                    if (radioButt.Name.Contains('H')) if (radioButt.Checked) ++h;
                    if (radioButt.Name.Contains('G')) if (radioButt.Checked) ++g;
                }
            }
            host = h;
            guest = g;
            labelH.Text = h.ToString();
            labelG.Text = g.ToString();
        }

        private void radioButton1G_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }

        private void radioButton2H_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }

        private void radioButton2G_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }

        private void radioButton3H_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }

        private void radioButton3G_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }

        private void radioButton4H_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }

        private void radioButton4G_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton5H_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }

        private void radioButton5G_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }

        private void radioButton6H_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }

        private void radioButton6G_CheckedChanged(object sender, EventArgs e)
        {
            _update_score();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            time = float.Parse(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelInfo.Text = "Calculating prediction...";
            //System.Threading.Thread.Sleep(2000);
             

        }

        private void radioButtonRA0_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void _range_set()
        {
            //I would rather cycle through all radio buttons, but
            int a, b;
            a = b = 0;
            foreach (var radioButt in groupBoxRi.Controls.OfType<RadioButton>()) 
            {
                //Console.WriteLine(radioButt.Name);
                if (radioButt.Checked) a = int.Parse(Regex.Match(radioButt.Name, @"\d+").Value);
            }
            foreach (var radioButt in groupBoxRf.Controls.OfType<RadioButton>())
            {
                //Console.WriteLine(radioButt.Checked.ToString());
                if (radioButt.Checked) b = int.Parse(Regex.Match(radioButt.Name, @"\d+").Value);
            }
            Console.WriteLine(a.ToString());
            Console.WriteLine(b.ToString());
            if (a > b)
            {
                int c = a;
                a = b;
                b = c;
            }
            from_ = a;
            to_ = b;
            labelRA.Text = a.ToString();
            labelRB.Text = b.ToString();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonRB0_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonRA1_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRB1_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRA2_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRB2_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRA3_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRB3_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRA4_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRB4_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRA5_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRB5_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void groupBoxRi_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelInfo_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxHG_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click_1(object sender, EventArgs e)
        {

        }

        private void radioButtonRA6_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRB6_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            //_chart_renew();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        static string ABOUT()
        {
            string me = "Dr. Sergey Postnikov";
            string date = "17-Apr-2021";
            string version = "0.1.2";
            string UUID = "9f0ed698-6285-4e81-b820-1136ccd948b4";
            string HASH = "..."; //replace with "..." then check hash
            return $"Author {me},\n date {date},\n version {version},\n UUID {UUID},\n hash {HASH}\n";
        }
    }
}
