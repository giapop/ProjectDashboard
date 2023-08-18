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
    public partial class MaterieDB : Form
    {
        string connString;
        public MaterieDB()
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
                SQLiteCommand comanda = new SQLiteCommand("SELECT* FROM Materie;");
                comanda.Connection = conexiune;
                SQLiteDataReader reader = comanda.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem itm = new ListViewItem(reader["Id"].ToString());
                    itm.SubItems.Add(reader["Denumire"].ToString());
                    itm.SubItems.Add(reader["NrCredite"].ToString());
                    itm.SubItems.Add(reader["Nota"].ToString());
                    listView1.Items.Add(itm);
                }
            }
            catch (Exception ex)
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
                SQLiteCommand comanda = new SQLiteCommand("SELECT MAX(Id) FROM Materie;");
                comanda.Connection = conexiune;
                int nr = Convert.ToInt32(comanda.ExecuteScalar());
                comanda.CommandText = "INSERT INTO Materie(Id,Denumire,NrCredite,Nota) VALUES(@Id,@Denumire,@NrCredite,@Nota);";
                comanda.Parameters.Add(new SQLiteParameter("@Id", DbType.UInt32));
                comanda.Parameters["@Id"].Value = nr + 1;
                comanda.Parameters.Add(new SQLiteParameter("@Denumire", DbType.String, 20));
                comanda.Parameters["@Denumire"].Value = tbDen.Text;
                comanda.Parameters.Add(new SQLiteParameter("@NrCredite", DbType.UInt32));
                comanda.Parameters["@NrCredite"].Value = tbCredite.Text;
                comanda.Parameters.Add(new SQLiteParameter("@Nota", DbType.UInt32));
                comanda.Parameters["@Nota"].Value = tbNot.Text;
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
                        comanda.CommandText = "UPDATE Materie SET Nota='" + tbNota.Text + "' WHERE Id = " + nr;
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

        private void btnDelete_Click(object sender, EventArgs e)
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
                        comanda.CommandText = "DELETE FROM Materie WHERE Id= " + nr;
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
    }
}
