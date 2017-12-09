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
    public partial class Edit_Profile : Form
    {
        public Login ll;
        string cs2 = @"Data Source=DESKTOP-EVT1G0T;Initial Catalog=TSE2101;Integrated Security=True;Pooling=False";
        string namewantedresident = Login.GetName();

        public Edit_Profile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please do not leave any blank and fill all your profile info.", "Error");
                return;
            }

            SqlConnection con4 = new SqlConnection(cs2);
            con4.Open();
            String query4 = "UPDATE Resident SET Resident_phone = '" + textBox2.Text + "', Resident_email ='" + textBox3.Text + "'WHERE Username ='" + namewantedresident + "'";
            SqlDataAdapter sda4 = new SqlDataAdapter(query4, con4);
            sda4.SelectCommand.ExecuteNonQuery();
            con4.Close();
            MessageBox.Show("Edit Sucessfully!", "Edit Profile");
            textBox2.Text = "";
            textBox3.Text = "";
            this.Close();
        }

        private DataTable DisplayProfile()
        {
            SqlConnection conpro = new SqlConnection(cs2);
            conpro.Open();
            string querypro = "SELECT Resident_name, Resident_phone, Resident_unit, Resident_email, Resident_status from Resident WHERE Username = '" + namewantedresident + "'";
            SqlCommand cmdpro = new SqlCommand(querypro, conpro);
            SqlDataAdapter dapro = new SqlDataAdapter(cmdpro);
            DataTable dtpro = new DataTable();
            dapro.Fill(dtpro);
            conpro.Close();
            return dtpro;
        }
    }
}
