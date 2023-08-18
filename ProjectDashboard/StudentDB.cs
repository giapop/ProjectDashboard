using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectDashboard
{
    public partial class StudentDB : Form
    {
        string connString;
        private const int CtrlKey = 8;
        public StudentDB()
        {
            InitializeComponent();
            connString = "Data Source=proiect.db; Version=3";
        }

        private void btnPreluare_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            SQLiteConnection conexiune = new SQLiteConnection(connString);
            try
            {
                conexiune.Open();
                SQLiteCommand comanda = new SQLiteCommand("SELECT* FROM Student;");
                comanda.Connection = conexiune;
                SQLiteDataReader reader = comanda.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem itm = new ListViewItem(reader["NrMatricol"].ToString());
                    itm.SubItems.Add(reader["Nume"].ToString());
                    itm.SubItems.Add(reader["Prenume"].ToString());
                    listView1.Items.Add(itm);
                }
            }
            catch(Exception ex)
            { 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexiune.Close();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
           
            SQLiteConnection conexiune = new SQLiteConnection(connString);
            try
            {
                conexiune.Open();
                SQLiteCommand comanda = new SQLiteCommand("SELECT MAX(NrMatricol) FROM Student;");
                comanda.Connection = conexiune;
                int nr = Convert.ToInt32(comanda.ExecuteScalar());
                comanda.CommandText = "INSERT INTO Student(NrMatricol,Nume,Prenume) VALUES(@NrMatricol,@Nume,@Prenume);";
                comanda.Parameters.Add(new SQLiteParameter("@NrMatricol", DbType.UInt32));
                comanda.Parameters["@NrMatricol"].Value = nr + 1;
                comanda.Parameters.Add(new SQLiteParameter("@Nume", DbType.String,20));
                comanda.Parameters["@Nume"].Value = tbNumeS.Text;
                comanda.Parameters.Add(new SQLiteParameter("@Prenume", DbType.String, 20));
                comanda.Parameters["@Prenume"].Value = tbPrenumeS.Text;
                comanda.ExecuteNonQuery();
                MessageBox.Show("S-a inserat!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexiune.Close();
            }
            btnPreluare_Click(sender, e);

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SQLiteConnection conexiune = new SQLiteConnection(connString);
            try
            {
                conexiune.Open();
                SQLiteCommand comanda = new SQLiteCommand();
                comanda.Connection = conexiune;
                foreach (ListViewItem itm in listView1.Items)
                    if (itm.Selected)
                    {
                        int nr = Convert.ToInt32(itm.SubItems[0].Text);
                        comanda.CommandText = "UPDATE Student SET Nume='" + tbAdd.Text + "' WHERE NrMatricol = " + nr;
                        comanda.ExecuteNonQuery();
                    }
                MessageBox.Show("S-a updatat!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexiune.Close();
            }
            btnPreluare_Click(sender, e);
        }

        private void btnDeleteS_Click(object sender, EventArgs e)
        {
            SQLiteConnection conexiune = new SQLiteConnection(connString);
            try
            {
                conexiune.Open();
                SQLiteCommand comanda = new SQLiteCommand();
                comanda.Connection = conexiune;
                foreach (ListViewItem itm in listView1.Items)
                    if (itm.Checked || itm.Selected)
                    {
                        int nr = Convert.ToInt32(itm.SubItems[0].Text);
                        comanda.CommandText = "DELETE FROM Student WHERE NrMatricol= "+nr;
                        comanda.ExecuteNonQuery();
                    }
                MessageBox.Show("S-a sters!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexiune.Close();
            }
            btnPreluare_Click(sender, e);


        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new SecondForm();
            form.Show();
        }

        private void tbAdd_MouseDown(object sender, MouseEventArgs e)
        {
            tbAdd.DoDragDrop(tbAdd.Text, DragDropEffects.Copy);
        }

       

       

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            richTextBox1.Text = "  Copy the new name in listBox \r\n \r\nAllowed Effect: \r\n" + e.AllowedEffect + "\r\nAvailable Formats:\r\n";
            foreach (string availableFormat in e.Data.GetFormats(true))
            {
                richTextBox1.Text += "\t" + availableFormat + "\r\n";
            }
            if (e.Data.GetDataPresent(DataFormats.Text, true))
            {
                if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move && (e.KeyState & CtrlKey) != CtrlKey)
                {
                  
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                 
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.Text, true))
            {
                
                listBox1.Items.Add(e.Data.GetData(DataFormats.Text));
            }
        }

        private void StudentDB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt == true && e.KeyCode == Keys.P)
                btnPreluare.PerformClick();
        }
    }
    
}
