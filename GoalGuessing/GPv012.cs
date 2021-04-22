using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms; //SWF

namespace GoalGuessing
{
    public partial class FormGG : Form
    {
        //form properties
        private int host = 0, guest = 0; //goals
        private float time_now = 0.0F, game_time = 90.0F; //in minutes
        private int from_ = 0, to_ = 0; //goal range
        private const int GMAX = 10;
        private float[,] stat = new float[GMAX+1, GMAX+1]; //input statistics
        private float hwins = 0F, gwins = 0F, draw = 0F, outside = 0F; //results
        private List<GroupBox> goalsGroup = new List<GroupBox>(); //goal records
        private ModelGG model1;
        public FormGG()
        {
            InitializeComponent();
            textBox1.Text = time_now.ToString();
            initStat();
            initChart();
            labelInfo.Text = "Games statistics. Input current game.";
            initGoals();
            model1 = new ModelGG();
            model1.initModel(stat, game_time, GMAX);
            model1.prntModel();
            //Console.WriteLine(ABOUT());
        }
        private void initGoals()
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
        private void initStat()
        {
            string[] lines = File.ReadAllLines("data\\stat_tab.dat");
            for (int i = 0; i <= GMAX; i++) for (int j = 0; j <= GMAX; j++) stat[i, j] = 0F;
            float pt = 0F;
            foreach(string line in lines)
            {
                string[] items = line.Split();
                if (items[0] == "Host") continue;
                int hh = int.Parse(items[0]);
                int gg = int.Parse(items[1]);
                float pp = float.Parse(items[2]);
                stat[hh, gg] = pp / 100.0F;
                pt += pp;
                Console.WriteLine($"{hh}:{gg} {pp} %");
            }
            Console.WriteLine($"100% ? {pt}");
        }
        private void initChart()
        {
            //Console.WriteLine("test");
            hwins = gwins = draw = outside = 0F;
            for ( int h = 0; h <= GMAX; h++)
            {
                for ( int g = 0; g <= GMAX; g++)
                {
                    if (h == g) draw += stat[h,g];
                    else if (h > g) hwins += stat[h, g];
                    else gwins += stat[h, g];
                }
            }
            hwins = (float)Math.Round( 100.0F * hwins, 2);
            gwins = (float)Math.Round( 100.0F * gwins, 2);
            draw = (float)Math.Round( 100.0F * draw, 2);
            outside = (float)Math.Round(100.0F * outside, 2);

            chart1.Series["answers"].Points.AddXY("HOST WINS", hwins );
            chart1.Series["answers"].Points.AddXY("GUEST WINS", gwins );
            chart1.Series["answers"].Points.AddXY("DRAWS", draw );
            chart1.Series["answers"].Points.AddXY("...", outside );
            labelHW.Text = hwins.ToString() + " %";
            labelGW.Text = gwins.ToString() + " %";
            labelDR.Text = draw.ToString() + " %";
            labelOR.Text = outside.ToString() + " %";
        }
        private void _chartRenew()
        {
            chart1.Series["answers"].Points.Clear();
            chart1.Series["answers"].Points.AddXY("HOST WINS", hwins);
            chart1.Series["answers"].Points.AddXY("GUEST WINS", gwins);
            chart1.Series["answers"].Points.AddXY("DRAW", draw);
            chart1.Series["answers"].Points.AddXY("OUT OF RANGE", outside);
            labelHW.Text = Math.Round(hwins, 2).ToString() + " %";
            labelGW.Text = Math.Round(gwins, 2).ToString() + " %";
            labelDR.Text = Math.Round(draw, 2).ToString() + " %";
            labelOR.Text = Math.Round(outside, 2).ToString() + " %";
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
            time_now = float.Parse($"{textBox1.Text}");
            if (time_now < 0F) time_now = 0F;
            else if (time_now > 90F) time_now = 90F;
            Console.WriteLine("time now "+time_now.ToString());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            labelInfo.Text = "Calculating prediction...";
            var inpt = new ArrayList(); //input for prediction
            
            inpt.Add(time_now);
            inpt.Add(from_);
            inpt.Add(to_);
            inpt.Add(host);
            inpt.Add(guest);
            //predict from model
            if (to_ < host + guest )
            {
                labelInfo.Text = $"Range {to_} less than total score now!";
                return;
            }
            if (from_ < host + guest)
            {
                labelInfo.Text = $"Fix range lower limit, must be from {host+guest}";
                return;
            }
            var answer = model1.Predict(inpt); //returns Tuple with 4 items below
            labelInfo.Text = "Done!";
            hwins = 100.0F * (float)answer.Item1; //host wins
            gwins = 100.0F * (float)answer.Item2; //guest wins
            draw = 100.0F * (float)answer.Item3;
            outside = 100.0F * (float)answer.Item4; //outside the range
            Console.WriteLine(hwins);
            Console.WriteLine(gwins);
            Console.WriteLine(draw);
            Console.WriteLine(outside);
            _chartRenew();
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

        private void radioButtonRA7_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRB7_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRA8_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void radioButtonRB8_CheckedChanged(object sender, EventArgs e)
        {
            _range_set();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
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
        private void FormGG_FormClosed(object sender, FormClosingEventArgs e)
        {
            Console.Write(ABOUT());
        }
        static string ABOUT()
        {
            string me = "Dr. Sergey Postnikov";
            string date = "22-Apr-2021";
            string version = "1.0.1";
            string UUID = "9f0ed698-6285-4e81-b820-1136ccd948b4";
            string HASH = "..."; //replace with "..." then check hash
            return $"Author {me},\n date {date},\n version {version}," +
                   $"\n UUID {UUID},\n hash {HASH}\n";
        }
    }

    public class ModelGG
    {
        // model is incremental in development, this is 0 increment
        // for goal_rate probabilites that depend on current score
        // first index is total goals, second is a host team goals 
        // third is 0 for host and 1 for guest team
        private const int H = 0, G = 1;
        private const float T = 90F; //game length time in min.
        private const float TOTMAX = 8; //total goals limit
        //factorial
        /*
        double[] fac = {1, 1, 2, 6, 24, 120,
                        720, 5040, 40320, 362880,3628800,39916800,
                        479001600 };
        */
        private float[][,] goal_rate = new float[10][,];
        private int[][,] mask = new int[10][,];
        private float[,] stat_matrix = new float[11,11];
        private int GMAX=10;
        private List<double> facs = new List<double>();
        public bool initModel(float[,] stat_given, float t_game, int gmax)
        {
            //total goals maximum
            this.GMAX = gmax;
            //list for factorials
            for (int i = 0; i <= 2*GMAX-1; i++) this.facs.Add(i == 0 ? 1 : this.facs[i - 1] * i);
            if (this.GMAX > 10) return false;
            // saving input stats to stat. matrix
            for (int i=0; i < this.GMAX; i++) for (int j=0; j < this.GMAX; j++) this.stat_matrix[i, j] = stat_given[i,j];
            // goal rate probability matrix, mask instances and zeroed
            for (int i = 0; i < this.GMAX; i++)
            {
                this.goal_rate[i] = new float[i + 1, 2];
                this.mask[i] = new int[i + 1, 2];
            }
            for (int i = 0; i < this.GMAX; i++) for(int j =0; j<=i; j++) for(int k=0; k<=1; k++)
            {
                this.goal_rate[i][j,k] = 0.0F;
                this.mask[i][j,k] = 0;
            }
            // goal rates are calculated from statistics given
            // hg - host goals, tot - total goals
            for (int tot = 0; tot < this.GMAX; tot++) for (int hg = tot; hg >= 0; hg--)
                {
                    //gg - guest goals
                    int gg;
                    gg = tot - hg;
                    // learning parameters, will be as a[tot,gg], now assuming all 1
                    float a = 1;
                    //tree like model (network)
                    float den = 1F; //denominator variable, non-zero
                    // probabilities from given stats.
                    if (tot == 0)
                    {
                        this.goal_rate[tot][0, H] = stat_given[1, 0] / T;
                        this.goal_rate[tot][0, G] = stat_given[0, 1] / T;
                    }
                    else
                    {
                        if (hg == tot)
                        {
                            den = stat_given[hg, 0];
                            this.goal_rate[tot][0, H] = den != 0 ? (stat_given[hg + 1, 0] / den) : 0;
                            this.goal_rate[tot][0, H] *= (2 * tot + 1) * (2 * tot) / T;
                            den = (stat_given[tot, 0] + a * stat_given[tot - 1, 1]);
                            this.goal_rate[tot][0, G] = den != 0 ? (stat_given[tot, 1] / den) : 0;
                            this.goal_rate[tot][0, G] *= (2 * tot + 1) * (2 * tot) / T;
                        }
                        else if (hg == 0)
                        {
                            den = stat_given[0, gg];
                            this.goal_rate[tot][tot, G] = den != 0 ? (stat_given[0, gg + 1] / den) : 0;
                            this.goal_rate[tot][tot, G] *= (2 * tot + 1) * (2 * tot) / T;
                            this.goal_rate[tot][tot, H] = a * this.goal_rate[tot][tot - 1, G];
                        }
                        else
                        {
                            den = (stat_given[hg, gg] + a * stat_given[hg - 1, gg + 1]);
                            this.goal_rate[tot][gg, G] = den != 0 ? (stat_given[hg, gg + 1] / den) : 0;
                            this.goal_rate[tot][gg, G] *= (2 * tot + 1) * (2 * tot) / T;
                            this.goal_rate[tot][gg, H] = a * this.goal_rate[tot][gg - 1, G];
                        }
                    }
                    //stat mask
                    this.mask[tot][hg, H] = stat_given[hg + 1, gg] == 0 ? 0 : 1;
                    this.mask[tot][hg, G] = stat_given[hg, gg + 1] == 0 ? 0 : 1;
                }
            return true;
        }
        public void prntModel()
        {
            for (int t = 0; t < 9; t++)
            {
                for (int g = 0; g <= t; g++)
                {
                    Console.Write(goal_rate[t][g,H].ToString() + ":H ");
                    Console.Write(goal_rate[t][g,G].ToString() + ":G,");
                }
                Console.Write(";\n");
            }
        }
        public Tuple<float, float, float , float> Predict(ArrayList inpt)
        {
            float time;
            time = (float)inpt[0];  //game time
            int ri;
            ri = (int)inpt[1]; // goal range initial
            int rf;
            rf = (int)inpt[2]; // goal range final
            int hg;
            hg = (int)inpt[3]; // host goals now
            int gg;
            gg = (int)inpt[4]; // guest goals now
            ////
            Console.WriteLine("Predict input:");
            Console.WriteLine(time);
            Console.WriteLine(hg);
            Console.WriteLine(gg);
            Console.WriteLine((ri,rf));
            ////
            int tt, goals;
            tt = hg + gg; // goal sum range [ri...rf]
            double phw, pgw, pdr, pos, pp, pu;
            //range probabilities init.
            phw = 0;
            pgw = 0;
            pdr = 0;
            pos = 0;
            int n, hnew, gnew, hmore, gmore;
            string s, ss, smax;
            n = -1;
            List<string> outcomes = new List<string>();
            smax = "";
            s = "";
            for (int i = tt; i < 8 - 2*0; i++) smax += "G";/// 8 - 2? 
            //cumulative
            pu = 0.0;
            do
            {
                //outcome path to final score
                s = s == "H" ? "G" : "H";
                if (n == -1)
                {
                    outcomes.Add(s);
                }
                else
                {
                    outcomes.Add(outcomes[n] + s);
                }
                if (s == "G") n++;
                ss = outcomes.Last();
                pp = GoalProbabilty(time, hg, gg, ss);
                goals = tt + ss.Length;
                Console.WriteLine($"GoalProb. = {pp}");///
                hmore = ss.Split('H').Length - 1;
                gmore = ss.Split('G').Length - 1;
                hnew = hg + hmore;
                gnew = gg + gmore;
                if (pp > 0.0)
                {
                    pu += pp;
                    if (goals >= ri && goals <= rf)
                    {
                        Console.WriteLine($"goals = {goals} +h={hmore} +g={gmore}");///
                        if (hnew > gnew) phw += pp;
                        else if (hnew < gnew) pgw += pp;
                        else pdr += pp;
                    }
                    else
                    {
                        pos += pp; //outside the range
                    }
                }
                else {
                    if (pp<0) Console.WriteLine($"!!! pp = {pp}");///
                }
                Console.WriteLine($"phw = {phw} pgw={pgw} pdr={pdr} pos={pos}");///
            } while (ss != smax);
            //no goals scored
            Console.WriteLine($"pu={pu}");///
            //probability normaliztion
            double k = 1;
            if (tt == 0)
            {
                pp = 1.0 - pu; // no more goals chance
            }
            else
            {
                pp = this.stat_matrix[hg, gg]; // Statistics(hg:gg) ///room for model improvements
                pp = pp + (1-pp)*(time/T);/// //no more goals in time remaining
                k = (1 - pp) / pu;
                phw *= k;
                pgw *= k;
                pdr *= k;
                pos *= k;
            }
            if (tt >= ri && tt <= rf)
            {
                if (hg > gg) phw += pp;
                else if (hg < gg) pgw += pp;
                else pdr += pp;
            }
            else
            {
                pos += pp;
            };
            //host wins, guest wins, draw, outside of the range
            return Tuple.Create((float)(phw), (float)(pgw), (float)(pdr), (float)(pos));
        }        
        private double GoalProbabilty(float time, int hg, int gg, string events)
        {
            ////
            Console.WriteLine(events);
            Console.WriteLine(time);
            Console.WriteLine(hg);
            Console.WriteLine(gg);
            ////
            double p = 0, pp = 1;
            int fac;
            int team, goals, now, Hgoals, Ggoals;
            goals = events.Length; // possible goal events in order
            now = hg + gg; //total goals now
            switch (goals)
            {
                case 0: //no goals
                    ////add later if needed
                    break;
                case 1: //one goal case
                    team = events[0] == 'H' ? 0 : 1;
                    p = this.goal_rate[now][gg, team] * (T - time);
                    //stat mask
                    //p *= this.mask[now][gg, team];///
                    break;
                default: // over one goal case
                    pp = 1.0;
                    Hgoals = 0;
                    Ggoals = 0;
                    foreach (char tm in events)
                    {
                        team = tm == 'H' ? 0 : 1;
                        Console.WriteLine($"rate={this.goal_rate[now + Hgoals + Ggoals][gg + Ggoals, team]}");
                        pp *= this.goal_rate[now + Hgoals + Ggoals][gg + Ggoals, team];
                        //stats mask
                        //pp *= this.mask[now + Hgoals + Ggoals][gg + Hgoals, team];///
                        Console.WriteLine($"goals={goals} Hg ={Hgoals} Gg={Ggoals} pp={pp}");
                        Hgoals += (1 - team);
                        Ggoals += (team);
                    }
                    pp /= Math.Pow(T, goals - 1);
                    //factorial
                    ///fac = 1;
                    ///for (int i = 1; i <= 2 * goals - 1; i++) fac *= i;
                    pp /= this.facs[2 * goals - 1];
                    p = pp * Math.Pow((T - time), goals*2-1);
                    break;
            }
            //p *= this.stat_matrix[hg, gg];
            return p;
        }
    }
}
