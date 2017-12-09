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
    public partial class Admin : Form
    {
        string cs1 = @"Data Source=DESKTOP-EVT1G0T;Initial Catalog=TSE2101;Integrated Security=True;Pooling=False";

        DateTime myDate = DateTime.Now;

        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToString(); //Date
            label3.Text = DateTime.Now.ToString(); //Date
            dataGridView1.DataSource = DisplayBulletin();
            dataGridView2.DataSource = DisplayResident();
            dataGridView3.DataSource = DisplayProcessingPayment();
        }

        private DataTable DisplayBulletin()
        {
            // con, query, cmd, da & dt = variable name as db
            SqlConnection condb = new SqlConnection(cs1);
            condb.Open();
            string querydb = "SELECT * from News";
            SqlCommand cmddb = new SqlCommand(querydb, condb);
            SqlDataAdapter dadb = new SqlDataAdapter(cmddb);
            DataTable dtdb = new DataTable();
            dadb.Fill(dtdb);
            condb.Close();
            return dtdb;
        }

        private DataTable DisplayResident()
        {
            // con, query, cmd, da & dt = variable name as dr
            SqlConnection condr = new SqlConnection(cs1);
            condr.Open();
            string querydr = "SELECT * from Resident";
            SqlCommand cmddr = new SqlCommand(querydr, condr);
            SqlDataAdapter dadr = new SqlDataAdapter(cmddr);
            DataTable dtdr = new DataTable();
            dadr.Fill(dtdr);
            condr.Close();
            return dtdr;
        }

        private DataTable DisplayProcessingPayment()
        {
            SqlConnection condpp = new SqlConnection(cs1);
            condpp.Open();
            string querydp = "SELECT * from [Transaction] WHERE Payment_Status = 'processing'";
            SqlCommand cmddp = new SqlCommand(querydp, condpp);
            SqlDataAdapter dadr = new SqlDataAdapter(cmddp);
            DataTable dtdp = new DataTable();
            dadr.Fill(dtdp);
            condpp.Close();
            return dtdp;
        }

        private void button1_Click(object sender, EventArgs e) //ADD
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please provide ID and News", "Error");
                return;
            }
            try
            {
                SqlConnection con1 = new SqlConnection(cs1);
                con1.Open();
                String query1 = "INSERT INTO News (News_id, Content, Date_created) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + myDate + "')";
                SqlDataAdapter sda1 = new SqlDataAdapter(query1, con1);
                sda1.SelectCommand.ExecuteNonQuery();
                con1.Close();
                MessageBox.Show("Insert Successfully!", "Add News");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            textBox1.Text = "";
            textBox2.Text = "";
            dataGridView1.DataSource = DisplayBulletin();
        }

        private void button2_Click(object sender, EventArgs e) //DELETE
        {
            // for now, delete news by it News_ID
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please provide News ID", "Error");
                return;
            }
            try
            {
                SqlConnection con2 = new SqlConnection(cs1);
                con2.Open();
                String query2 = "DELETE FROM News WHERE News_id ='" + textBox1.Text + "'";
                SqlDataAdapter sda2 = new SqlDataAdapter(query2, con2);
                sda2.SelectCommand.ExecuteNonQuery();
                con2.Close();
                MessageBox.Show("Record Sucessfully Delete!", "Delete News");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            textBox1.Text = "";
            textBox2.Text = "";
            dataGridView1.DataSource = DisplayBulletin();
        }

        private void button3_Click(object sender, EventArgs e) //UPDATE or EDIT
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please provide ID and News", "Error");
                return;
            }
            try
            {
                SqlConnection con3 = new SqlConnection(cs1);
                con3.Open();
                String query3 = "UPDATE News SET Content = '" + textBox2.Text + "'WHERE News_Id = '" + textBox1.Text + "'";
                SqlDataAdapter sda3 = new SqlDataAdapter(query3, con3);
                sda3.SelectCommand.ExecuteNonQuery();
                con3.Close();
                MessageBox.Show("Update Sucessfully!", "Update News");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            textBox1.Text = "";
            textBox2.Text = "";
            dataGridView1.DataSource = DisplayBulletin();
        }

        private void button4_Click(object sender, EventArgs e) //REFRESH or VIEW
        {
            dataGridView1.DataSource = DisplayBulletin();
        }

        private void button5_Click(object sender, EventArgs e) //SEARCH
        {
            try
            {
                SqlConnection con5 = new SqlConnection(cs1);
                con5.Open();
                string query5 = "SELECT * from News WHERE Content LIKE '%" + textBox2.Text + "%' AND Date_created = '" + this.dateTimePicker1.Text + "'";
                SqlCommand cmd5 = new SqlCommand(query5, con5);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                dataGridView1.DataSource = dt5;
                con5.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Login ll = new Login();
            this.Close();
            ll.Show();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string s1 = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            MessageBox.Show(s1, "News");
        }

        private void button7_Click(object sender, EventArgs e) //ADD
        {
            if (textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please provide Username , Password and the Name", "Error");
                return;
            }
            try
            {
                SqlConnection con7 = new SqlConnection(cs1);
                con7.Open();
                String query7 = "INSERT INTO Resident (Username, Password, Resident_unit, Resident_name) VALUES ('" + textBox3.Text + "', '" + textBox4.Text + "','" + textBox3.Text + "','" + textBox5.Text + "')";
                SqlDataAdapter sda7 = new SqlDataAdapter(query7, con7);
                sda7.SelectCommand.ExecuteNonQuery();
                con7.Close();
                MessageBox.Show("An Account is Successfully Added!", "Add Account");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            dataGridView2.DataSource = DisplayResident();
        }

        private void button8_Click(object sender, EventArgs e) //DELETE
        {
            // for now delete resident by it Username
            if (textBox3.Text == "")
            {
                MessageBox.Show("Please provide Username and Password", "Error");
                return;
            }
            try
            {
                SqlConnection con8 = new SqlConnection(cs1);
                con8.Open();
                String query8 = "DELETE FROM Resident WHERE Username ='" + textBox3.Text + "'";
                SqlDataAdapter sda8 = new SqlDataAdapter(query8, con8);
                sda8.SelectCommand.ExecuteNonQuery();
                con8.Close();
                MessageBox.Show("Username Sucessfully Delete!", "Delete Account");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            textBox3.Text = "";
            textBox4.Text = "";
            dataGridView2.DataSource = DisplayResident();
        }

        private void button9_Click(object sender, EventArgs e) // REFRESH or VIEW
        {
            dataGridView2.DataSource = DisplayResident();
        }

        private void button10_Click(object sender, EventArgs e) //SEARCH
        {
            // for now search by it resident username
            if (textBox3.Text == "")
            {
                MessageBox.Show("Please provide some information to search", "Error");
                return;
            }

            try
            {
                SqlConnection con10 = new SqlConnection(cs1);
                con10.Open();
                string query10 = "SELECT * from Resident WHERE Username LIKE '%" + textBox3.Text + "%'";
                SqlCommand cmd10 = new SqlCommand(query10, con10);
                SqlDataAdapter da10 = new SqlDataAdapter(cmd10);
                DataTable dt10 = new DataTable();
                da10.Fill(dt10);
                dataGridView2.DataSource = dt10;
                con10.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Login ll = new Login();
            this.Close();
            ll.Show();
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox3.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            textBox4.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ConfirmPayment confirmPayment = new ConfirmPayment();
            confirmPayment.Show();
            dataGridView3.DataSource = DisplayProcessingPayment();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = DisplayProcessingPayment();
        }
    }
}
