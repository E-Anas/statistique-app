using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace statistiqueV2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Tab5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            histo.Series["Nombre"].Points.Clear();
            pie.Series["Nb"].Points.Clear();
            int t = (int)total("ni");
            Decimal va = 0;
            Decimal[] tab = new Decimal[container.Rows.Count - 1];
            container.Rows[container.Rows.Count - 1].Cells["ni"].Value = t;
            for (int i = 0; i < container.Rows.Count - 1; i++)
            {
                tab[i] = Decimal.Parse(container.Rows[i].Cells["ni"].Value.ToString());
                container.Rows[i].Cells["fi"].Value = Math.Round((Decimal.Parse(container.Rows[i].Cells["ni"].Value.ToString()) / (Decimal)t) * 100, 2);
                if (i == 0)
                {
                    container.Rows[i].Cells["fc"].Value = container.Rows[i].Cells["fi"].Value.ToString();
                }
                else
                {
                    container.Rows[i].Cells["fc"].Value = float.Parse(container.Rows[i - 1].Cells["fc"].Value.ToString()) + float.Parse(container.Rows[i].Cells["fi"].Value.ToString());
                }
                histo.Series["Nombre"].Points.AddXY(container.Rows[i].Cells["xi"].Value.ToString(), Int16.Parse(container.Rows[i].Cells["ni"].Value.ToString()));
                pie.Series["Nb"].Points.AddXY(container.Rows[i].Cells["xi"].Value.ToString(), Int16.Parse(container.Rows[i].Cells["ni"].Value.ToString()));
            }

            container.Rows[container.Rows.Count - 1].Cells["fi"].Value = total("fi");
    
            for (int i = 0; i < container.Rows.Count - 1; i++)
                if (max(tab) == Decimal.Parse(container.Rows[i].Cells["ni"].Value.ToString()))
                    mode.Text = container.Rows[i].Cells["xi"].Value.ToString();

        }

        public Decimal total(String cell)
        {
            Decimal sum = 0;
            for (int i = 0; i < container.Rows.Count - 1; i++)
            {
                sum += Decimal.Parse(container.Rows[i].Cells[cell].Value.ToString());
            }
            return Math.Round(sum, 2);
        }

        public Decimal max(Decimal[] tab)
        {
            Decimal max = tab[0];
            for (int i = 1; i < tab.Length; i++)
                if (max < tab[i])
                    max = tab[i];
            return max;
        }

        public Decimal total1(String cell)
        {
            Decimal sum = 0;
            for (int i = 0; i < container1.Rows.Count - 1; i++)
            {
                sum += Decimal.Parse(container1.Rows[i].Cells[cell].Value.ToString());
            }
            return Math.Round(sum, 2);
        }

        public void tri(Decimal[] tab)
        {
            Decimal tmp;
            for (int i = 0; i < tab.Length - 1; i++)
            {
                if (tab[i] > tab[i + 1])
                {
                    tmp = tab[i];
                    tab[i] = tab[i + 1];
                    tab[i + 1] = tmp;
                }
            }

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            qualitatif.Visible = true;
            qt_c.Visible = false;
            qt_d.Visible = false;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            qualitatif.Visible = false;
            qt_c.Visible = false;
            qt_d.Visible = true;
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            qualitatif.Visible = false;
            qt_c.Visible = true;
            qt_d.Visible = false;
        }

        

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            int t = (int)total1("ni1");
            Decimal va = 0;
            Decimal[] tab = new Decimal[container1.Rows.Count - 1];

            barre.Series["Nombre"].Points.Clear();
            cum.Series.Clear();

            container1.Rows[container1.Rows.Count - 1].Cells["ni1"].Value = t;
            moy1.Text = Math.Round((Double)t / (Double)(container1.Rows.Count - 1),2).ToString();
            barre.Series["Nombre"]["PixelPointWidth"] = "4";
            for (int i = 0; i < container1.Rows.Count - 1; i++)
            {
                va = (Decimal)Math.Pow((Double.Parse(container1.Rows[i].Cells["ni1"].Value.ToString()) - Double.Parse(moy1.Text.ToString())), 2);
                tab[i] = Decimal.Parse(container1.Rows[i].Cells["xi1"].Value.ToString());
                container1.Rows[i].Cells["fi1"].Value = Math.Round((Decimal.Parse(container1.Rows[i].Cells["ni1"].Value.ToString()) / (Decimal)t) * 100, 2);
                if (i == 0)
                {
                    container1.Rows[i].Cells["fc1"].Value = container1.Rows[i].Cells["fi1"].Value.ToString();
                }
                else
                {
                    container1.Rows[i].Cells["fc1"].Value = float.Parse(container1.Rows[i - 1].Cells["fc1"].Value.ToString()) + float.Parse(container1.Rows[i].Cells["fi1"].Value.ToString());
                }
                barre.Series["Nombre"].Points.AddXY(container1.Rows[i].Cells["xi1"].Value.ToString(), Int16.Parse(container1.Rows[i].Cells["ni1"].Value.ToString()));

                cum.Series.Add(i.ToString());
                cum.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                cum.Series[i].BorderWidth = 4;
                cum.Series[i].Color = System.Drawing.Color.Red;
                if (i < (container1.Rows.Count - 2))
                {
                    cum.Series[i].Points.AddXY(int.Parse(container1.Rows[i].Cells["xi1"].Value.ToString()), Double.Parse(container1.Rows[i].Cells["fc1"].Value.ToString()));
                    cum.Series[i].Points.AddXY(int.Parse(container1.Rows[i + 1].Cells["xi1"].Value.ToString()), Double.Parse(container1.Rows[i].Cells["fc1"].Value.ToString()));
                }
                else
                {
                    cum.Series[i].Points.AddXY(int.Parse(container1.Rows[i].Cells["xi1"].Value.ToString()), Double.Parse(container1.Rows[i].Cells["fc1"].Value.ToString()));
                    cum.Series[i].Points.AddXY(int.Parse(container1.Rows[i].Cells["xi1"].Value.ToString()) + 1, Double.Parse(container1.Rows[i].Cells["fc1"].Value.ToString()));
                }
            }
            var1.Text = Math.Round(va / t, 2).ToString();
            ecart1.Text = Math.Round(Math.Sqrt(Double.Parse(var1.Text.ToString())), 2).ToString();
            container1.Rows[container1.Rows.Count - 1].Cells["fi1"].Value = total1("fi1");

            tri(tab);
            if (((container1.Rows.Count - 1) % 2) != 0)
            {
                    med1.Text = tab[(container1.Rows.Count/2)-1].ToString();
            }
            else
            {
                med1.Text = ((tab[(container1.Rows.Count / 2)+1] + tab[(container1.Rows.Count/ 2)-1]) / (Decimal)2).ToString();
            }
            for (int i = 0; i < container1.Rows.Count - 1; i++)
                if (max(tab) == Decimal.Parse(container1.Rows[i].Cells["ni1"].Value.ToString()))
                    mode1.Text = container1.Rows[i].Cells["xi1"].Value.ToString();

            cum.Legends.Clear();
        }


        public Decimal total2(String cell,int f)
        {
            Decimal sum = 0;
            for (int i = 0; i < f - 1; i++)
            {
                sum += Decimal.Parse(container2.Rows[i].Cells[cell].Value.ToString());
            }
            return Math.Round(sum, 2);
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            histo2.Series["Nombre"].Points.Clear();
            cum2.Series["Nb"].Points.Clear();
            histo2.Series["Nombre"]["PointWidth"] = "1";
            int t = (int)total2("ni2", container2.Rows.Count);
            Decimal va = 0,max=0;
            int index=0,flag=0,pgcd=0;
            Decimal in1, in_1;
            container2.Rows[container2.Rows.Count - 1].Cells["ni2"].Value = t;
            
            for (int i = 0; i < container2.Rows.Count - 1; i++)
            {
                
                container2.Rows[i].Cells["ci"].Value = (Decimal.Parse(container2.Rows[i].Cells["xi2"].Value.ToString())+ Decimal.Parse(container2.Rows[i].Cells["xi_"].Value.ToString()))/2;
                container2.Rows[i].Cells["nici"].Value = Decimal.Parse(container2.Rows[i].Cells["ni2"].Value.ToString()) * Decimal.Parse(container2.Rows[i].Cells["ci"].Value.ToString());
                container2.Rows[i].Cells["ai"].Value = Decimal.Parse(container2.Rows[i].Cells["xi_"].Value.ToString()) - Decimal.Parse(container2.Rows[i].Cells["xi2"].Value.ToString());
                if (i > 0)
                pgcd = PGCD((int)pgcd,int.Parse(container2.Rows[i].Cells["ai"].Value.ToString()));
                container2.Rows[i].Cells["fi2"].Value = Math.Round((Decimal.Parse(container2.Rows[i].Cells["ni2"].Value.ToString()) / (Decimal)t) * 100, 2);
                if (i == 0)
                {
                    container2.Rows[i].Cells["fc2"].Value = container2.Rows[i].Cells["fi2"].Value.ToString();
                }
                else
                {
                    container2.Rows[i].Cells["fc2"].Value = float.Parse(container2.Rows[i - 1].Cells["fc2"].Value.ToString()) + float.Parse(container2.Rows[i].Cells["fi2"].Value.ToString());
                }
                if ((Decimal.Parse(container2.Rows[i].Cells["fc2"].Value.ToString())>=50)&&flag==0) {
                    index = i;
                    flag = 1;
                }
                
            }
            container2.Rows[container2.Rows.Count - 1].Cells["ai"].Value = "ar="+pgcd;

            container2.Rows[container2.Rows.Count - 1].Cells["fi2"].Value = Math.Round(total2("fi2", container2.Rows.Count),2);
            container2.Rows[container2.Rows.Count - 1].Cells["nici"].Value = total2("nici", container2.Rows.Count);
            moy2.Text = (float.Parse(container2.Rows[container2.Rows.Count - 1].Cells["nici"].Value.ToString()) / (float)t).ToString();

            //med2.Text = total2("ni2", index + 2).ToString()+" /" + index;
            //med2.Text = Math.Round((Decimal.Parse(container2.Rows[index].Cells["xi2"].Value.ToString())+(Decimal.Parse(container2.Rows[index].Cells["ai"].Value.ToString())*((Decimal)(50- Decimal.Parse(container2.Rows[index].Cells["fc2"].Value.ToString()))/(Decimal)(Decimal.Parse(container2.Rows[index+1].Cells["fc2"].Value.ToString())- Decimal.Parse(container2.Rows[index].Cells["fc2"].Value.ToString()))))),2).ToString();

            if (index - 1 < 0)
                in1 = 0;
            else
                in1 = Decimal.Parse(container2.Rows[index - 1].Cells["xi_"].Value.ToString());

          

            med2.Text = Math.Round((((((decimal.Parse(container2.Rows[index].Cells["xi_"].Value.ToString()) - in1)
                * ((t / 2) - total2("ni2", index + 1)))
                / (total2("ni2", index + 2) - total2("ni2", index + 1))))
                + Decimal.Parse(container2.Rows[index].Cells["xi2"].Value.ToString())),2).ToString();

     
            for (int i = 0; i < container2.Rows.Count - 1; i++)
            { 
                va += (Decimal)(Double.Parse(container2.Rows[i].Cells["fi2"].Value.ToString())*Math.Pow((Double.Parse(container2.Rows[i].Cells["ci"].Value.ToString()) - Double.Parse(moy2.Text.ToString())), 2));
                container2.Rows[i].Cells["hi"].Value = (Decimal.Parse(container2.Rows[i].Cells["fi2"].Value.ToString()) * pgcd) / Decimal.Parse(container2.Rows[i].Cells["ai"].Value.ToString());
                if (max < Decimal.Parse(container2.Rows[i].Cells["hi"].Value.ToString()))
                {
                    max = Decimal.Parse(container2.Rows[i].Cells["hi"].Value.ToString());
                    index = i;
                }
                for (int j = 0; j < (int.Parse(container2.Rows[i].Cells["ai"].Value.ToString())/ pgcd); j++)
                {
                    histo2.Series["Nombre"].Points.AddXY(container2.Rows[i].Cells["ci"].Value.ToString(), Int16.Parse(container2.Rows[i].Cells["ni2"].Value.ToString()));
                    cum2.Series["Nb"].Points.AddXY(container2.Rows[i].Cells["ci"].Value.ToString(), Decimal.Parse(container2.Rows[i].Cells["fc2"].Value.ToString()));
                    histo2.Series["polygone"].Points.AddXY(container2.Rows[i].Cells["ci"].Value.ToString(), Int16.Parse(container2.Rows[i].Cells["ni2"].Value.ToString()));
                }
            }
            histo2.Series["polygone"].BorderWidth = 2;
            histo2.Series["polygone"].Color = System.Drawing.Color.Red;
            
            if (index - 1 < 0)
                in1 = 0;
            else
                in1 = Decimal.Parse(container2.Rows[index - 1].Cells["hi"].Value.ToString());

            if (index + 1 > (container2.Rows.Count-2))
                in_1 = 0;
            else
                 in_1 = Decimal.Parse(container2.Rows[index + 1].Cells["hi"].Value.ToString());

            Decimal h = Decimal.Parse(container2.Rows[index].Cells["ai"].Value.ToString()) * (Decimal.Parse(container2.Rows[index].Cells["hi"].Value.ToString()) - in1);
            Decimal b = 2 * Decimal.Parse(container2.Rows[index].Cells["hi"].Value.ToString()) - in_1 - in1;
            mode2.Text = Math.Round(Decimal.Parse(container2.Rows[index].Cells["xi2"].Value.ToString()) + ((Decimal)h)/b,2).ToString();
            var2.Text = Math.Round(va/100,2).ToString();
                //Math.Round(va / t, 2).ToString();
            ecart2.Text = Math.Round(Math.Sqrt(Double.Parse(var2.Text.ToString())), 2).ToString();
           
        }

        public static int PGCD(int a, int b)
        {
            int temp = a % b;
            if (temp == 0)
                return b;
            return PGCD(b, temp);
        }
    }
}

