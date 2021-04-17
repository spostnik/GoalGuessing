using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoalGuessing
{
    public partial class FormGG : Form
    {
        private int host = 0, guest = 0;
        private float time = 0.0F;
        private int a = 0, b = 0;
        private float[,] stat = new float[7,7];
        private float hwins = 50F, gwins = 50F, draw = 0F, outside = 0F; 

        public FormGG()
        {
            InitializeComponent();
            _initStat();
            _initChart();
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
            chart1.Series["answers"].Points.AddXY("host wins", hwins);
            chart1.Series["answers"].Points.AddXY("guest wins", gwins);
            chart1.Series["answers"].Points.AddXY("draw", draw);
            chart1.Series["answers"].Points.AddXY("outside", outside);
            labelHW.Text = gwins.ToString() + " %";
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
            int h = 0;
            int g = 0;
            //I would rather cycle through all radio buttons, but
            if (radioButton1H.Checked) ++h;
            if (radioButton1G.Checked) ++g;
            if (radioButton2H.Checked) ++h;
            if (radioButton2G.Checked) ++g;
            if (radioButton3H.Checked) ++h;
            if (radioButton3G.Checked) ++g;
            if (radioButton4H.Checked) ++h;
            if (radioButton4G.Checked) ++g;
            if (radioButton5H.Checked) ++h;
            if (radioButton5G.Checked) ++g;
            if (radioButton6H.Checked) ++h;
            if (radioButton6G.Checked) ++g;
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

        }

        private void radioButtonRA0_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void _range_set()
        {
            //I would rather cycle through all radio buttons, but
            if (radioButtonRA0.Checked) a = 0;
            if (radioButtonRB0.Checked) b = 0;
            if (radioButtonRA1.Checked) a = 1;
            if (radioButtonRB1.Checked) b = 1;
            if (radioButtonRA2.Checked) a = 2;
            if (radioButtonRB2.Checked) b = 2;
            if (radioButtonRA3.Checked) a = 3;
            if (radioButtonRB3.Checked) b = 3;
            if (radioButtonRA4.Checked) a = 4;
            if (radioButtonRB4.Checked) b = 4;
            if (radioButtonRA5.Checked) a = 5;
            if (radioButtonRB5.Checked) b = 5;
            if (radioButtonRA6.Checked) a = 6;
            if (radioButtonRB6.Checked) b = 6;
            if (a > b)
            {
                int c = a;
                a = b;
                b = c;
            }
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
    }
}
