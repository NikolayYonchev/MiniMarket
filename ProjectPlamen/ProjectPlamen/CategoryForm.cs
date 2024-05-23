using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniMarket
{
    public partial class CategoryForm : Form
    {
        DBConnect DBCon = new DBConnect();
        public CategoryForm()
        {
            InitializeComponent();
        }
        private void getTable()
        {
            string selectQuery = "SELECT * FROM CATEGORY";
            SqlCommand command = new SqlCommand(selectQuery, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            DataGridView_category.DataSource = Table;

        }
        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_id.Text == string.Empty || textBox_name.Text == string.Empty || textBox_description.Text == string.Empty)
                {
                    MessageBox.Show("Missing Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string insertQuery =$"INSERT INTO Category VALUES ({textBox_id.Text}, '{textBox_name.Text}', '{textBox_description.Text}')";

                    SqlCommand command = new SqlCommand(insertQuery, DBCon.GetCon());
                    DBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Category Added Successfully", "Add information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            getTable();
        }
        private void Clear()
        {
            textBox_id.Clear();
            textBox_name.Clear();
            textBox_description.Clear();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                string updateQuery = $"UPDATE Category SET CatName='{textBox_name.Text}', CatDes='{textBox_description.Text}' WHERE CatId={textBox_id.Text}";
                SqlCommand command = new SqlCommand(updateQuery, DBCon.GetCon());
                DBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Category Updated Successfully", "Update information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DBCon.CloseCon();
                getTable();
                Clear();
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
                    MessageBox.Show("Please choose a category", "Missing information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM Category WHERE CatId=" + textBox_id.Text + "";
                        SqlCommand command = new SqlCommand(deleteQuery, DBCon.GetCon());
                        DBCon.OpenCon();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Category Deleted Successfully", "Delete information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void label_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_exit_MouseEnter(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Red;
        }

        private void label_exit_MouseLeave(object sender, EventArgs e)
        {
            label_exit.ForeColor = Color.Goldenrod;
        }

        private void label_logout_MouseHover(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;
        }

        private void label_logout_MouseLeave(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Goldenrod;
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

        private void button_seller_Click(object sender, EventArgs e)
        {
            SellerForm sellerForm = new SellerForm();
            sellerForm.Show();
            this.Hide();
        }

        private void DataGridView_category_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_id.Text = DataGridView_category.SelectedRows[0].Cells[0].Value.ToString();
            textBox_name.Text = DataGridView_category.SelectedRows[0].Cells[1].Value.ToString();
            textBox_description.Text = DataGridView_category.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button_selling_Click(object sender, EventArgs e)
        {
            SellingForm sellingForm = new SellingForm();
            sellingForm.Show();
            this.Hide();
        }
    }
}
