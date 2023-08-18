using ProjectDashboard.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDashboard
{
    public partial class AddForm : Form
    {
        private Dashboard dashboard;
        public AddForm(Dashboard dash)
        {
            dashboard = dash;
            InitializeComponent();
            tbNr.Text = dashboard.student.NrMatricol;
            tbNume.Text = dashboard.student.Nume;
            tbPrenume.Text = dashboard.student.Prenume;
            tbMat.Text = dashboard.materie.Denumire;
            nmCredite.Value = dashboard.materie.NrCredite;
            nmNota.Value = dashboard.materie.Nota;

         
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            dashboard.student.NrMatricol = tbNr.Text;
            dashboard.student.Nume = tbNume.Text;
            dashboard.student.Prenume = tbPrenume.Text;
            dashboard.materie.Denumire = tbMat.Text;
            dashboard.materie.NrCredite = (int)nmCredite.Value;
            dashboard.materie.Nota = (int)nmNota.Value;


        }

        private void tbNr_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNr.Text)||(tbNr.Text).Length<5)
                errorProvider1.SetError((Control)sender, "Nr. must be valid, must contain at least 5 characters! ");
            else
                errorProvider1.Clear();
        }

        private void tbNume_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbNume.Text))
                errorProvider1.SetError((Control)sender, "Name must be valid!");
            else
                errorProvider1.Clear();
        }

        private void tbPrenume_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNume.Text))
                errorProvider1.SetError((Control)sender, "First Name must be valid!");
            else
                errorProvider1.Clear();
        }

        private void nmCredite_Validating(object sender, CancelEventArgs e)
        {
            if (nmCredite.Value > 6)
                errorProvider1.SetError(nmCredite, "Introduceti nr de credite:");
            else
            {
                errorProvider1.Clear();
                try
                {
                    dashboard.materie.NrCredite = (int)nmCredite.Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    dashboard.materie.NrCredite = (int)nmCredite.Value;
                    // nmCredite.Value = 0;
                }
            }


        }

        private void tbMat_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbMat.Text))
                errorProvider1.SetError((Control)sender, "String must be valid!");
            else
                errorProvider1.Clear();
        }

        private void nmNota_Validating(object sender, CancelEventArgs e)
        {
            if (nmNota.Value > 10)
                errorProvider1.SetError(nmNota, "Introduceti nota corect");
            else
            {
                errorProvider1.Clear();
                try
                {
                    dashboard.materie.Nota = (int)nmNota.Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    dashboard.materie.Nota = (int)nmNota.Value;
                    //  nmNota.Value = 1;
                }
            }
        }

      
    }
}
