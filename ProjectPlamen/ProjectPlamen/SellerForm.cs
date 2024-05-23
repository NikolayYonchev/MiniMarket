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

namespace MiniMarket
{
    public partial class SellerForm : Form
    {
        DBConnect DBCon = new DBConnect();
        public SellerForm()
        {
            InitializeComponent();
        }
        private void getTable()
        {
            string selectQuery = "SELECT * FROM Seller";
            SqlCommand command = new SqlCommand(selectQuery, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGridView_seller.DataSource = Table;

        }
        private void Clear()
        {
            textBox_id.Clear();
            textBox_name.Clear();
            textBox_age.Clear();
            textBox_phoneNo.Clear();
            textBox_password.Clear();
        }
        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = $"INSERT INTO Seller VALUES('{textBox_id.Text}','{textBox_name.Text}','{textBox_age.Text}','{textBox_phoneNo.Text}','{textBox_password.Text}')";
                SqlCommand command = new SqlCommand(insertQuery, DBCon.GetCon());
                DBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Seller Added Successfully", "Add information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getTable();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_id.Text == string.Empty || textBox_name.Text == string.Empty || textBox_age.Text == string.Empty || textBox_phoneNo.Text == string.Empty || textBox_password.Text == string.Empty)
                {
                    MessageBox.Show("Missing Information", "Missing information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string updateQuery = $"UPDATE Seller SET SellerName='{textBox_name.Text}', SellerAge='{textBox_age.Text}', SellerPhone='{textBox_phoneNo.Text}', SellerPass='{textBox_password.Text}' WHERE SellerId={textBox_id.Text}";
                    SqlCommand command = new SqlCommand(updateQuery, DBCon.GetCon());
                    DBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Seller Updated Successfully", "Update information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DBCon.CloseCon();
                    getTable();
                    Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_id.Text == string.Empty)
                {
                    MessageBox.Show("Please choose a seller", "Missing information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM Seller WHERE SellerId=" + textBox_id.Text + "";
                        SqlCommand command = new SqlCommand(deleteQuery, DBCon.GetCon());
                        DBCon.OpenCon();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Seller Deleted Successfully", "Delete information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DBCon.CloseCon();
                        getTable();
                        Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            getTable();
        }

        private void dataGridView_seller_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_id.Text = dataGridView_seller.SelectedRows[0].Cells[0].Value.ToString();
            textBox_name.Text = dataGridView_seller.SelectedRows[0].Cells[1].Value.ToString();
            textBox_age.Text = dataGridView_seller.SelectedRows[0].Cells[2].Value.ToString();
            textBox_phoneNo.Text = dataGridView_seller.SelectedRows[0].Cells[3].Value.ToString();
            textBox_password.Text = dataGridView_seller.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void label_exit_MouseHover(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Goldenrod;
        }

        private void label_logout_MouseEnter(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;
        }

        private void label_logout_MouseLeave(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Goldenrod;
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_logout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void button_product_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.Show();
            this.Hide();
        }

        private void button_category_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            categoryForm.Show();
            this.Hide();
        }

        private void button_selling_Click(object sender, EventArgs e)
        {
            SellingForm sellingForm = new SellingForm();
            sellingForm.Show();
            this.Hide();
        }
    }
}
