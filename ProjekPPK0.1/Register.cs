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
    public partial class Register : Form
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
        public Register()
        {
            InitializeComponent();
            InitConnection();
            conn.Open();
            makeItPassword();
        }

        private void makeItPassword()
        {
            textBox5.Text = "";
            // The password character is an asterisk.  
            textBox5.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                //ini salah sqlnya
                String sql = "select * from daftar where username= '" + textBox6.Text + "' or email = '"+textBox4.Text+"'";
                String sql2 = "insert into daftar (firstName, lastName, umur, nomorTelepon, username, password, email) values ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox7.Text + "', '" + textBox6.Text + "', '" + textBox5.Text + "', '" + textBox4.Text + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i > 0)
                {
                    label3.Text = "Username or email has been used";
                }
                else
                {
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                    DataTable dt2 = new DataTable();
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Registration success");
                    this.Hide();
                    Login login = new Login();
                    login.Show();
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            conn.Close();
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void label2_Click(object sender, EventArgs e)
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
