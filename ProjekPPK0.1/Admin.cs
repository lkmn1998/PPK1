using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjekPPK0._1
{
    public partial class Admin : Form
    {
        MySqlConnection conn;
        String uid = "root";
        String passwd = "";
        //nama databasenya ganti
        String dbase = "tokokomputer2";
        String server = "127.0.0.1";
        String connString;
        String id;
        private void InitConnection()
        {
            try
            {
                conn = new MySqlConnection();
                connString = "server=" + server + ";" +
                             "database=" + dbase + ";" +
                             "uid=" + uid + ";" +
                             "password=" + passwd + ";" +
                             "SslMode=none";
                conn.ConnectionString = connString;
                conn.Open();
                //MessageBox.Show("Connection Success");
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public Admin(String id)
        {
            InitializeComponent();
            InitConnection();
            //conn.Open();
            start();
            this.id = id;
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
        }
        private void start()
        {
            try
            {
                conn.Open();
                String sql = "SELECT b.firstName as Name, c.idBarang as ItemID, c.namaBarang as ItemName, a.Information, a.Qty FROM penjualan a INNER JOIN daftar b ON a.IdUser = idDaftar INNER JOIN barang2 c ON a.IdBarang = c.IdBarang";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                DataTable dataTable = new DataTable();
                dataTable.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dataTable;
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void itemStock()
        {
            try
            {
                //conn.Open();
                String sql = "Select idBarang, namaBarang, stock from barang2";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                DataTable dataTable = new DataTable();
                dataTable.Load(cmd.ExecuteReader());
                dataGridView2.DataSource = dataTable;
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((sender as TabControl).SelectedIndex)
            {
                case 0:
                    // Do nothing here (let's suppose that TabPage index 0 is the address information which is already loaded.
                    break;
                case 1:
                    conn.Open();
                    itemStock();
                    break;
            }
        }
                
        private void label1_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // Use this since we are a WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Use this since we are a console app
                System.Environment.Exit(1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                String sql = "update barang2 set stock = stock +'"+textBox2.Text+"' where idBarang = '" + textBox1.Text + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                String sql2 = "insert into penjualan(idUser, idBarang, Information, Qty) values('" + id + "', '" + textBox1.Text + "', 'Add Stock', '" + textBox2.Text + "')";
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd2.ExecuteNonQuery();
                itemStock();
                start();
                //conn.Close();
                textBox1.Text = "0";
                textBox2.Text = "0";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows.Count > 0 && dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                if (!string.IsNullOrEmpty(dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                {
                    //gets a collection that contains all the rows
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    //populate the textbox from specific value of the coordinates of column and row.
                    textBox1.Text = row.Cells[0].Value.ToString();
                    textBox2.Text = "1";
                }
            }
        }
    }
}
