using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Residential_Management
{
    public partial class Login : Form
    {
        string cs = @"Data Source=DESKTOP-EVT1G0T;Initial Catalog=TSE2101;Integrated Security=True;Pooling=False";
        public static string namewanted;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please provide username and password", "Login Error");
                return;
            }
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM Admin WHERE admin_id='" + textBox1.Text + "' AND admin_password='" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();  
            sda.Fill(dt);
     
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT COUNT(*) FROM Resident WHERE Username='" + textBox1.Text + "' AND Password='" + textBox2.Text + "'", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);

            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                Admin aa = new Admin(); //call Main class
                aa.Show();
            }
            else if (dt1.Rows[0][0].ToString() == "1")
            {
                namewanted = textBox1.Text;
                this.Hide();
                Resident rr = new Resident(); //call Main class
                rr.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Error");
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public static string GetName()
        {
            return namewanted;
        }
    }
}
