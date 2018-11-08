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
        public Admin()
        {
            InitializeComponent();
            InitConnection();
            conn.Open();
            start();
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
        }
        private void start()
        {
            try
            {
                String sql = "Select * from pembeli_has_barang";
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
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((sender as TabControl).SelectedIndex)
            {
                case 0:
                    // Do nothing here (let's suppose that TabPage index 0 is the address information which is already loaded.
                    break;
                case 1:
                    try
                    {
                        conn.Open();
                        String sql = "Select idBarang, namaBarang, stock from barang";
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
                    break;
                case 2:
                    try
                    {
                        conn.Open();
                        String sql = "Select idBarang, namaBarang, Supplier_idSupplier from barang";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        DataTable dataTable = new DataTable();
                        dataTable.Load(cmd.ExecuteReader());
                        dataGridView3.DataSource = dataTable;
                        conn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
            }
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
