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

    public partial class User : Form
    {
        String id;
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
        public User(String id)
        {
            this.id = id;
            InitializeComponent();
            InitConnection();
            start();
        }
        private void start()
        {
            try
            {
                conn.Open();
                String sql = "Select * from barang2";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                DataTable dataTable = new DataTable();
                dataTable.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dataTable;
                label2.Text = id;
                conn.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                String sql = "Select * from barang2";
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
        
        private void label7_Click(object sender, EventArgs e)
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
                String sql = "update barang2 set stock = stock -'"+textBox2.Text+"' where idBarang = '"+textBox1.Text+"'";
                String sql2 ="insert into penjualan(idUser, idBarang, Information, Qty) values('"+id+"', '"+textBox1.Text+"', 'Sold', '"+textBox2.Text+"')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                conn.Close();
                textBox1.Text = "0";
                textBox2.Text = "0";
                start();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
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
