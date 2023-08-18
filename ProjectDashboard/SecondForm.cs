using ProjectDashboard.Entities;
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
using System.Xml.Serialization;

namespace ProjectDashboard
{
    public partial class SecondForm : Form
    {
        private Principal principal;
        private int _currentParticipantIndex;
        public SecondForm()
        {
            principal = new Principal(1);
            InitializeComponent();
        }
        public void Refresh()
        {
            dgvDash.Rows.Clear();
            foreach (var dash in principal.lista)
            {
                int id = dgvDash.Rows.Add(new object[] { dash.student.NrMatricol, dash.student.Nume, dash.student.Prenume, dash.materie.Denumire,dash.materie.NrCredite, dash.materie.Nota });
                dgvDash.Rows[id].Tag = dash;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        { 
            bool nnou = false;
            Dashboard dash = null;
            if (dgvDash.SelectedRows.Count > 0)
            {
                dash = (Dashboard)dgvDash.SelectedRows[0].Tag;
            }

            if (dash == null)
            {
                dash = new Dashboard();
                nnou = true;
            }
            Form newform = new AddForm(dash);
            if (newform.ShowDialog() == DialogResult.OK)
            {
                if (nnou)
                {
                    principal.lista.Add(dash);
                }
                tool.Text = "Add loading...";
                Refresh();
            }
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
                 if(dgvDash.SelectedRows.Count>0)
            {
                var dash = (Dashboard)dgvDash.SelectedRows[0].Tag;
                if (
                    MessageBox.Show("Are you sure you want to delete this ?",
                    "Delete confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    principal.lista.Remove(dash);
                    Refresh();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dgvDash.SelectedRows.Count > 0)
            {
                var dash = (Dashboard)dgvDash.SelectedRows[0].Tag;
                if (
                    MessageBox.Show("Are you sure you want to delete this ?",
                    "Delete confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    principal.lista.Remove(dash);
                    Refresh();
                }
            }
        }

       

      

        private void SecondForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.D)
                btnDelete.PerformClick();
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Principal));
                using (FileStream fs = File.Create(saveFileDialog.FileName))
                {
                    xmlSerializer.Serialize(fs, principal);
                }
            }
        }

        private void xMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Principal));
                using (FileStream fs = File.OpenRead(openFileDialog.FileName))
                {
                    principal = (Principal)xmlSerializer.Deserialize(fs);
                    Refresh();
                    
                }
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog.PageSettings = printDocument.DefaultPageSettings;

            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
                printDocument.DefaultPageSettings = pageSetupDialog.PageSettings;

        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("Microsoft Sans Serif", 11);

            var pageSettings = e.PageSettings;
            var printAreaHeight = e.MarginBounds.Height;
            var printAreaWidth = e.MarginBounds.Width;
            var marginLeft = pageSettings.Margins.Left;
            var marginTop = pageSettings.Margins.Top;
     
            if (pageSettings.Landscape)
            {
                var intTemp = printAreaHeight;
                printAreaHeight = printAreaWidth;
                printAreaWidth = intTemp;
            }

            const int rowHeight = 30;
            var columnWidth = printAreaWidth / 6;

            StringFormat fmt = new StringFormat(StringFormatFlags.LineLimit);
            fmt.Trimming = StringTrimming.EllipsisCharacter;

            var currentY = marginTop;
            while (_currentParticipantIndex < principal.lista.Count)
            {
                
                var currentX = marginLeft;
                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);
                
                e.Graphics.DrawString(
                    principal.lista[_currentParticipantIndex].student.NrMatricol,
                    font,
                    Brushes.Black,
                    new RectangleF(currentX, currentY, columnWidth, rowHeight),
                    fmt);
                //Update the horizontal drawing coordinate
                currentX += columnWidth;

                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);
                e.Graphics.DrawString(
                    principal.lista[_currentParticipantIndex].student.Nume,
                    font,
                    Brushes.Black,
                    currentX,
                    currentY,
                    fmt);
                currentX += columnWidth;

                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);
                e.Graphics.DrawString(
                    principal.lista[_currentParticipantIndex].student.Prenume,
                    font,
                    Brushes.Black,
                    currentX,
                    currentY,
                    fmt);
                currentX += columnWidth;

                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);
                e.Graphics.DrawString(
                    principal.lista[_currentParticipantIndex].materie.Denumire,
                    font,
                    Brushes.Black,
                    currentX,
                    currentY,
                    fmt);
                currentX += columnWidth;

                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);
                e.Graphics.DrawString(
                    principal.lista[_currentParticipantIndex].materie.NrCredite.ToString(),
                    font,
                    Brushes.Black,
                    currentX,
                    currentY,
                    fmt);
                currentX += columnWidth;

                e.Graphics.DrawRectangle(
                    Pens.Black,
                    currentX,
                    currentY,
                    columnWidth,
                    rowHeight);
                e.Graphics.DrawString(
                    principal.lista[_currentParticipantIndex].materie.Nota.ToString(),
                    font,
                    Brushes.Black,
                    currentX,
                    currentY,
                    fmt);
            
                _currentParticipantIndex++;
              
                currentY += rowHeight;

               
                if (currentY + rowHeight > printAreaHeight)
                {
                    e.HasMorePages = true;
                    break;
                }
            }
        }

        private void printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _currentParticipantIndex = 0;
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                printPreviewDialog.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while trying to load the document for Print Preview. Make sure you currently have access to a printer. A printer must be connected and accessible for Print Preview to work.");
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDocument.Print();
        }

        private void xMLToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Principal));
                using (FileStream fs = File.Create(saveFileDialog.FileName))
                {
                    xmlSerializer.Serialize(fs, principal);
                }
            }
        }

        private void xMLToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Principal));
                using (FileStream fs = File.OpenRead(openFileDialog.FileName))
                {
                    principal = (Principal)xmlSerializer.Deserialize(fs);
                    Refresh();

                }
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
            //this.Close();
        }

        private void btnGrafic_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new Grafic();
            form.Show();
        }

        private void gRAPHICSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new Grafic();
            form.Show();

        }

        private void tXTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "(*.txt)|*.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(dlg.FileName);
                for (int i = 0; i < dgvDash.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dgvDash.Columns.Count; j++)
                    {
                        sw.WriteLine($"{dgvDash.Rows[i].Cells[j].Value.ToString()}");
                    }
                }


                sw.Close();
             
            }
        }

        private void btnDateS_Click(object sender, EventArgs e)
        {

            this.Hide();
            var form = new StudentDB();
            form.Show();

        }

        private void btnMaterie_Click(object sender, EventArgs e)
        {

            this.Hide();
            var form = new MaterieDB();
            form.Show();
        }
    }
}
