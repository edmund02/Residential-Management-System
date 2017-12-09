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
    public partial class ConfirmPayment : Form
    {
        string cs2 = @"Data Source=DESKTOP-EVT1G0T;Initial Catalog=TSE2101;Integrated Security=True;Pooling=False";

        public ConfirmPayment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select one of the two options.", "Error");
                return;
            }

            SqlConnection con4 = new SqlConnection(cs2);
            con4.Open();
            String query4 = "UPDATE [Transaction] SET Payment_Status = '" + comboBox1.Text + "'";
            SqlDataAdapter sda4 = new SqlDataAdapter(query4, con4);
            sda4.SelectCommand.ExecuteNonQuery();
            con4.Close();
            MessageBox.Show("Update Sucessfully!", "Payment Confirmation");
            this.Close();
        }
    }
}
