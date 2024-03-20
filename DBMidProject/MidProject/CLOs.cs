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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MidProject
{
    public partial class CLOs : Form
    {
        string constr = "Data Source=TAYYAB-PROGRAMM;Initial Catalog=StuManage;Integrated Security=True;";
        public CLOs()
        {
            InitializeComponent();
            ShowTable();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Clo values(@Name,GetDate(), GetDate())", conn);
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Added Successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ShowTable();
        }
        private void ShowTable()
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select * From Clo", conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dGV1.DataSource = dataTable;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete CLO where Name=@Name", conn);
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    MessageBox.Show("CLO Deleted Successfully", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                else
                    MessageBox.Show("No CLO Student Found", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ShowTable();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Home form1 = new Home();
            form1.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ManageStudent form2 = new ManageStudent();
            form2.ShowDialog();
        }

        private void CLOs_Load(object sender, EventArgs e)
        {

        }
    }
}
