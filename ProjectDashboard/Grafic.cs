using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDashboard
{
    public partial class Grafic : Form
    {
        Graphics gr;
        Bitmap bmp;
        double[] vect = new double[30];
        double[] vec = new double[30];
        int nr = 0;
        int nrElem = 0;
        bool vb = false;
        const int marg = 10;

        Font font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
        Color culoare = Color.Blue;
        public Grafic()
        {
            InitializeComponent();
            bmp = new Bitmap(this.Width, this.Height);
            gr = Graphics.FromImage(bmp);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (vb == true)
            {
                Graphics gr = e.Graphics;
                Rectangle rec = new Rectangle(panel1.ClientRectangle.X + marg,
                    panel1.ClientRectangle.Y + 2 * marg,
                    panel1.ClientRectangle.Width - 2 * marg,
                    panel1.ClientRectangle.Height - 1 * marg);
                Pen pen = new Pen(Color.GreenYellow, 3);
                gr.DrawRectangle(pen, rec);

                double latime = rec.Width / nrElem / 6;
                double distanta = (rec.Width - nrElem * latime) / (nrElem + 1);
                double valMax = vect.Max();

                Brush br = new SolidBrush(culoare);
                Rectangle[] recs = new Rectangle[nrElem];
                for (int i = 0; i < nrElem; i++)
                {
                    recs[i] = new Rectangle((int)(rec.Location.X + (i + 1) * distanta + i * latime),
                        (int)(rec.Location.Y + rec.Height - vect[i] / valMax * rec.Height),
                        (int)latime,
                        (int)(vect[i] / valMax * rec.Height));
                    //gr.FillRectangle(br, recs[i]);
                    gr.FillEllipse(br, recs[i]);
                    gr.DrawString(vect[i].ToString(), font,
                        br, new Point(recs[i].Location.X, recs[i].Location.Y - font.Height));
                }
                for (int i = 0; i < nrElem - 1; i++)
                    gr.DrawLine(pen, new Point((int)(recs[i].Location.X + latime / 2), recs[i].Location.Y),
                        new Point((int)(recs[i + 1].Location.X + latime / 2), recs[i + 1].Location.Y));
            }
        }


       

        private void incarcareDateToolStripMenuItem1_Click(object sender, EventArgs e)
        {

                StreamReader sr = new StreamReader("raport.txt");
                string linie = null;
                while ((linie = sr.ReadLine()) != null)
                {
                    string a = sr.ReadLine();
                    string b = sr.ReadLine();
                    string c = sr.ReadLine();
                    vect[nr] = Convert.ToDouble(sr.ReadLine());
                    vect[nrElem] = Convert.ToDouble(sr.ReadLine());
                    nr++;
                    nrElem++;
                    vb = true;

                }
                sr.Close();
                MessageBox.Show("Date incarcate! " + nrElem + " valori");
                panel1.Invalidate();
         

        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new SecondForm();
            form.Show();
        }
    }
}
