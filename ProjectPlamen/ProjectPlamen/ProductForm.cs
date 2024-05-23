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
    public partial class ProductForm : Form
    {
        DBConnect DBCon = new DBConnect();
        public ProductForm()
        {
            InitializeComponent();
        }

        private void button_category_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            categoryForm.Show();
            this.Hide();
        }
        private void getCategory()
        {
            string selectQuery = "SELECT * FROM CATEGORY";
            SqlCommand command = new SqlCommand(selectQuery, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            comboBox_categories.DataSource = Table;
            comboBox_categories.ValueMember = "CatName";
            comboBox_search.DataSource = Table;
            comboBox_search.ValueMember = "CatName";
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            getCategory();
            getTable();
        }
        private void Clear()
        {
            textBox_id.Clear();
            textBox_name.Clear();
            textBox_price.Clear();
            textBox_qty.Clear();
            comboBox_categories.SelectedIndex = 0;
        }
        private void getTable()
        {
            string selectQuery = "SELECT * FROM Product";
            SqlCommand command = new SqlCommand(selectQuery, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGridView_product.DataSource = Table;

        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery = $"INSERT INTO Product VALUES({textBox_id.Text}, '{textBox_name.Text}', {textBox_price.Text}, {textBox_qty.Text}, '{comboBox_categories.Text}')";
                SqlCommand command = new SqlCommand(insertQuery, DBCon.GetCon());
                DBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully", "Add information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (textBox_id.Text == string.Empty || textBox_name.Text == string.Empty || textBox_price.Text == string.Empty || textBox_qty.Text == string.Empty)
                {
                    MessageBox.Show("Missing Information", "Missing information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string updateQuery = $"UPDATE Product SET ProdName='{textBox_name.Text}', ProdPrice='{textBox_price.Text}', ProdQty='{textBox_qty.Text}', ProdCat='{comboBox_categories.Text}' WHERE ProdId={textBox_id.Text}";
                    SqlCommand command = new SqlCommand(updateQuery, DBCon.GetCon());
                    DBCon.OpenCon();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Product Updated Successfully", "Update information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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



        private void dataGridView_product_Click_1(object sender, EventArgs e)
        {
            textBox_id.Text = dataGridView_product.SelectedRows[0].Cells[0].Value.ToString();
            textBox_name.Text = dataGridView_product.SelectedRows[0].Cells[1].Value.ToString();
            textBox_price.Text = dataGridView_product.SelectedRows[0].Cells[2].Value.ToString();
            textBox_qty.Text = dataGridView_product.SelectedRows[0].Cells[3].Value.ToString();
            comboBox_categories.SelectedValue = dataGridView_product.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            getTable();
        }

        private void comboBox_categories_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuery = $"SELECT * FROM Product WHERE ProdCat='{comboBox_categories.SelectedValue.ToString()}'";
            SqlCommand command = new SqlCommand(selectQuery, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            dataGridView_product.DataSource = Table;
        }

        private void label_exit_MouseClick(object sender, MouseEventArgs e)
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

        private void label_logout_MouseEnter(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;
        }

        private void label_logout_MouseLeave(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Goldenrod;
        }

        private void label_logout_MouseClick(object sender, MouseEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_id.Text == string.Empty)
                {
                    MessageBox.Show("Please choose a product", "Missing information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are you sure?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string deleteQuery = "DELETE FROM Product WHERE ProdId=" + textBox_id.Text + "";
                        SqlCommand command = new SqlCommand(deleteQuery, DBCon.GetCon());
                        DBCon.OpenCon();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product Deleted Successfully", "Delete information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button_seller_Click(object sender, EventArgs e)
        {
            SellerForm sellerForm = new SellerForm();
            sellerForm.Show();
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
