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
    public partial class Login : Form
    {
        MySqlConnection conn;
        String uid = "root";
        String passwd = "";
        //nama databasenya ganti
        String dbase = "tokokomputer2";
        String server = "127.0.0.1";
        String connString;
        int i;
        String id;
        String isAdmin;
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

        public Login()
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                i = 0;
                //Login
                String sql = "select * from daftar where username= '" + textBox6.Text + "' and password='" + textBox5.Text + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                {
                    label2.Text = "You have entered wrong username or password";
                }
                else if (i == 1)
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    id = dr["idDaftar"].ToString();
                    isAdmin = dr["isAdmin"].ToString();
                    //MessageBox.Show(isAdmin);
                    //int isAdmin2 = Convert.ToInt32(isAdmin);
                    if (isAdmin.Equals("True"))
                    {
                        conn.Close();
                        this.Hide();
                        Admin admin = new Admin(id);
                        admin.Show();
                    }
                    else
                    {
                        conn.Close();
                        this.Hide();
                        User user = new User(id);
                        user.Show();
                    }
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
            Register register = new Register();
            register.Show();
            
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
    }
}
