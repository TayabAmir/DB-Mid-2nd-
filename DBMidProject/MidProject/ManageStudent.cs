using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace MidProject
{
    public partial class ManageStudent : Form
    {
            string constr = "Data Source=TAYYAB-PROGRAMM;Initial Catalog=StuManage;Integrated Security=True;";
        public ManageStudent()
        {
            InitializeComponent();
            ShowTable();
            textBox3.TextChanged += TextBox3_TextChanged;
            textBox4.TextChanged += TextBox4_TextChanged;
            textBox5.TextChanged += TextBox5_TextChanged;
        }
        private void TextBox5_TextChanged1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsValidEmail(textBox5.Text))
            {
                MessageBox.Show("Email is not in correct format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            if (!IsValidContact(textBox4.Text))
            {
                MessageBox.Show("Contact No must be 11 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Student values(@FirstName,@LastName, @Contact, @Email, @RegistrationNumber, @Status)", conn);
                cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
                cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
                cmd.Parameters.AddWithValue("@RegistrationNumber", textBox3.Text);
                cmd.Parameters.AddWithValue("@Contact", textBox4.Text);
                cmd.Parameters.AddWithValue("@Email", textBox5.Text);
                cmd.Parameters.AddWithValue("@Status", int.Parse(textBox6.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Added Successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ShowTable();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete Student where RegistrationNumber=@RegistrationNumber", conn);
                cmd.Parameters.AddWithValue("@RegistrationNumber", textBox3.Text);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    MessageBox.Show("Student Deleted Successfully", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                else
                    MessageBox.Show("No Student Found", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ShowTable();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update Student set FirstName=@FirstName,LastName=@LastName,Contact=@Contact, Email=@Email ,Status=@Status where RegistrationNumber = @RegistrationNumber", conn);
                cmd.Parameters.AddWithValue("@FirstName", textBox1.Text);
                cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
                cmd.Parameters.AddWithValue("@RegistrationNumber", textBox3.Text);
                cmd.Parameters.AddWithValue("@Contact", textBox4.Text);
                cmd.Parameters.AddWithValue("@Email", textBox5.Text);
                cmd.Parameters.AddWithValue("@Status", int.Parse(textBox6.Text));
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    MessageBox.Show("Student Updated Successfully", "Update", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                else
                    MessageBox.Show("No Student Found of this Registration Number Found", "Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ShowTable();
        }
        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (!IsValidEmail(textBox5.Text))
            {
                errorProvider1.SetError(textBox5, "Email is not in correct format.");
            }
            else
            {
                errorProvider1.SetError(textBox5, "");
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (!IsValidContact(textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "Contact number is not in correct format.");
            }
            else
            {
                errorProvider1.SetError(textBox4, "");
            }
        }

        private bool IsValidContact(string contact)
        {
            return contact.Length == 13;
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (!IsValidRegistrationNumber(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Registration number is not in correct format.");
            }
            else
            {
                errorProvider1.SetError(textBox3, "");
            }
        }

        private bool IsValidRegistrationNumber(string regNumber)
        {
            // Define a regular expression pattern for the registration number format
            string pattern = @"^\d{4}-[A-Za-z]{2}-\d+$";

            // Check if the registration number matches the pattern
            return System.Text.RegularExpressions.Regex.IsMatch(regNumber, pattern);
        }

        private void ShowTable()
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * From Student",conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dGV1.DataSource = dataTable;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Home form1 = new Home();
            form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Attendance form3 = new Attendance();
            form3.ShowDialog();
        }
    }
}
