using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace home_business
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        public int num = 0;
        public int customer_id;
        SqlConnection con = new SqlConnection(@"Data Source=KSHITIZ\SQLEXPRESS;Initial Catalog=home_business_lai;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            get_record();

        }

        private void get_record()
        {
            SqlCommand cmd = new SqlCommand("SELECT * from home_business_table", con);

            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);

            con.Close();

            recorddatagridview.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO home_business_table VALUES (@first_name, @last_name, @aaddress, @pproduct, @kkg)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@first_name", f_name.Text);
                cmd.Parameters.AddWithValue("@last_name", l_name.Text);
                cmd.Parameters.AddWithValue("@aaddress", address_box.Text);
                cmd.Parameters.AddWithValue("@pproduct", product_box.Text);
                cmd.Parameters.AddWithValue("@kkg", kg_box.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                num++;

                get_record();
                MessageBox.Show("New Record Added Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clearform();

            }
        }

        private bool IsValid()
        {
            if (f_name.Text == string.Empty)
            {
                MessageBox.Show("First_Name is required.", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clearform();
        }

        private void Clearform()
        {
            customer_id = 0;
            f_name.Clear();
            l_name.Clear();
            address_box.Clear();
            product_box.Clear();
            kg_box.Clear();
            f_name.Focus();
        }

        private void recorddatagridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            customer_id = Convert.ToInt32(recorddatagridview.SelectedRows[0].Cells[0].Value);
            f_name.Text = recorddatagridview.SelectedRows[0].Cells[1].Value.ToString();
            l_name.Text = recorddatagridview.SelectedRows[0].Cells[2].Value.ToString();
            address_box.Text = recorddatagridview.SelectedRows[0].Cells[3].Value.ToString();
            product_box.Text = recorddatagridview.SelectedRows[0].Cells[4].Value.ToString();
            kg_box.Text = recorddatagridview.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (customer_id > 0)
            {
                SqlCommand cmd = new SqlCommand(" UPDATE home_business_table set First_Name = @first_name, Last_Name = @last_name, Address = @aaddress, Product = @pproduct, KG = @kkg WHERE ID = @cust_id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@first_name", f_name.Text);
                cmd.Parameters.AddWithValue("@last_name", l_name.Text);
                cmd.Parameters.AddWithValue("@aaddress", address_box.Text);
                cmd.Parameters.AddWithValue("@pproduct", product_box.Text);
                cmd.Parameters.AddWithValue("@kkg", kg_box.Text);
                cmd.Parameters.AddWithValue("@cust_id", this.customer_id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Record Updated Successfully.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                get_record();
                Clearform();
            }
            else
            {
                MessageBox.Show("Select a Record to Update.", "Can't Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (customer_id > 0)
            {
                SqlCommand cmd = new SqlCommand(" DELETE FROM home_business_table WHERE ID = @cust_id", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@cust_id", this.customer_id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully.", "Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                get_record();
                Clearform();
            }
            else
            {
                MessageBox.Show("Select a Record to Delete.", "Can't Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

        }
    }
}