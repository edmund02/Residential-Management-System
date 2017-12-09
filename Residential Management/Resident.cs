using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Residential_Management
{

    public partial class Resident : Form
    {
        private String FeeType;
        private double FeeAmount = 0;
        
        private string TransactionDate, Username, Type, BankName, PaymentStatus, TransactionID, ReceiptNo, Amount, DepositDate;

        public Login l;
        string cs2 = @"Data Source=DESKTOP-EVT1G0T;Initial Catalog=TSE2101;Integrated Security=True;Pooling=False";
        string namewantedresident = Login.GetName();

        public Resident()
        {
            InitializeComponent();
        }

        private void Resident_Load(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToString();
            label8.Text = DateTime.Now.ToString();
            dataGridView1.DataSource = DisplayBulletin();
            dataGridView2.DataSource = DisplayProfile();
            dataGridView3.DataSource = DisplayHistory();
            label2.Text = "Username :  " + Login.GetName(); // 2 static needed to add in Login function.

        }

        private DataTable DisplayBulletin()
        {
            SqlConnection condisp = new SqlConnection(cs2);
            condisp.Open();
            string querydisp = "SELECT * from News";
            SqlCommand cmddisp = new SqlCommand(querydisp, condisp);
            SqlDataAdapter dadisp = new SqlDataAdapter(cmddisp);
            DataTable dtdisp = new DataTable();
            dadisp.Fill(dtdisp);
            condisp.Close();
            return dtdisp;
        }
        public DataTable DisplayProfile()
        {
            SqlConnection conpro = new SqlConnection(cs2);
            conpro.Open();
            string querypro = "SELECT Resident_name, Resident_phone, Resident_unit, Resident_email from Resident WHERE Username = '" + namewantedresident + "'";
            SqlCommand cmdpro = new SqlCommand(querypro, conpro);
            SqlDataAdapter dapro = new SqlDataAdapter(cmdpro);
            DataTable dtpro = new DataTable();
            dapro.Fill(dtpro);
            conpro.Close();
            return dtpro;
        }

        public DataTable DisplayHistory()
        {
            SqlConnection condh = new SqlConnection(cs2);
            condh.Open();
            string querydh = "SELECT * from [Transaction] WHERE Username = '" + Login.GetName() + "' AND Payment_Status = 'Approve'";
            SqlCommand cmddh = new SqlCommand(querydh, condh);
            SqlDataAdapter dadh = new SqlDataAdapter(cmddh);
            DataTable dtdh = new DataTable();
            dadh.Fill(dtdh);
            condh.Close();
            return dtdh;
        }

        private void button1_Click(object sender, EventArgs e) //SEARCH
        {
            try
            {
                SqlConnection conr1 = new SqlConnection(cs2);
                conr1.Open();
                string queryr1 = "SELECT * from News WHERE Content LIKE '%" + textBox1.Text + "%' AND Date_created = '" + this.dateTimePicker2.Text + "'";
                SqlCommand cmdr1 = new SqlCommand(queryr1, conr1);
                SqlDataAdapter dar1 = new SqlDataAdapter(cmdr1);
                DataTable dtr1 = new DataTable();
                dar1.Fill(dtr1);
                dataGridView1.DataSource = dtr1;
                conr1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = DisplayBulletin();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login ll = new Login();
            this.Close();
            ll.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Edit_Profile ep = new Edit_Profile();
            ep.Show();
            dataGridView2.DataSource = DisplayProfile();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string s1 = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            MessageBox.Show(s1, "News");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = DisplayProfile();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && checkBox2.Checked == false)
            {
                label10.Text = "RM50";
                FeeType = "Security Fee";
                FeeAmount = 50;
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == true)
            {
                label10.Text = "RM200";
                FeeType = "Maintenance Fee";
                FeeAmount = 200;
            }
            else if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                label10.Text = "RM250";
                FeeType = "Security Fee, Maintenance Fee";
                FeeAmount = 250;
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == false)
            {
                label10.Text = "RM0";
                FeeAmount = 0;
                MessageBox.Show("Please select any one of the fee!", "Error");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime myDate = DateTime.Now;
            TimeSpan myTime = DateTime.Now.TimeOfDay;
            if (comboBox1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please provide Bank account name and bank account number", "Error");
                return;
            } else if (FeeAmount == 0)
            {
                MessageBox.Show("Please select a fee to pay.", "Error");
                return;
            }
            try
            {
                SqlConnection con1 = new SqlConnection(cs2);
                con1.Open();
                string queryT = "INSERT INTO [Transaction] (Transaction_Date, Username, Fee_Type, Fee_Amount, Bank_Name, Receipt_No, Deposit_Date) VALUES ( '" + myDate + "', '" + Login.GetName() + "', '" + FeeType + "', '" + FeeAmount + "', '" + comboBox1.Text + "', '" + textBox3.Text + "', '" + dateTimePicker1.Value + "')";
                SqlDataAdapter sda1 = new SqlDataAdapter(queryT, con1);
                sda1.SelectCommand.ExecuteNonQuery();
                con1.Close();
                MessageBox.Show("Transaction Successfully! Please wait admin approve.", "Transactions");
                               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string CompanyName = "TSE2101";

            PdfPTable table = new PdfPTable(2);
            PdfPCell cell = new PdfPCell(new Phrase(CompanyName));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            table.AddCell("Transaction ID:");
            table.AddCell(TransactionID);

            table.AddCell("Transaction Date&Time:");
            table.AddCell(TransactionDate);

            table.AddCell("Username:");
            table.AddCell(Username);

            table.AddCell("Fee Type:");
            table.AddCell(Type);

            table.AddCell("Fee Amount:");
            table.AddCell("RM" + Amount);

            table.AddCell("Bank Name:");
            table.AddCell(BankName);

            table.AddCell("Receipt Number:");
            table.AddCell(ReceiptNo);

            table.AddCell("Deposit Date:");
            table.AddCell(DepositDate);

            table.AddCell("Payment Status");
            table.AddCell(PaymentStatus);

            FileStream fs = new FileStream("Report.pdf", FileMode.Create);
            Document doc = new Document(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            doc.Add(table);
            doc.Close();

            Report report = new Report();
            report.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = DisplayHistory();
        }

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TransactionID = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            TransactionDate = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            Username = dataGridView3.SelectedRows[0].Cells[2].Value.ToString();
            Type = dataGridView3.SelectedRows[0].Cells[3].Value.ToString();
            Amount = dataGridView3.SelectedRows[0].Cells[4].Value.ToString();
            BankName = dataGridView3.SelectedRows[0].Cells[5].Value.ToString();
            ReceiptNo = dataGridView3.SelectedRows[0].Cells[6].Value.ToString();
            PaymentStatus = dataGridView3.SelectedRows[0].Cells[7].Value.ToString();
            DepositDate = dataGridView3.SelectedRows[0].Cells[8].Value.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
    }
