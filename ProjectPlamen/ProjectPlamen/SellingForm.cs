using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;

namespace MiniMarket
{
    public partial class SellingForm : Form
    {
        DBConnect DBCon = new DBConnect();
        DGVPrinter printer = new DGVPrinter();
        public SellingForm()
        {
            InitializeComponent();
        }

        private void getCategory()
        {
            string selectQuery = "SELECT * FROM Category";
            SqlCommand command = new SqlCommand(selectQuery, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            comboBox_categories.DataSource = Table;
            comboBox_categories.ValueMember = "CatName";

        }
        private void getTable()
        {
            string selectQuery = "SELECT ProdName, ProdPrice FROM Product";
            SqlCommand command = new SqlCommand(selectQuery, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            DataGridView_product.DataSource = Table;

        }
        private void getSellTable()
        {
            string selectQuery = "SELECT * FROM Bill";
            SqlCommand command = new SqlCommand(selectQuery, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            DataGridView_sellList.DataSource = Table;

        }
        private void SellingForm_Load(object sender, EventArgs e)
        {

            label_date.Text = DateTime.Today.ToShortDateString();
            label_seller.Text = LoginForm.sellerName;
            getTable();
            getCategory();
            getSellTable();
        }

        private void DataGridView_product_Click(object sender, EventArgs e)
        {
            textBox_name.Text = DataGridView_product.SelectedRows[0].Cells[0].Value.ToString();
            textBox_price.Text = DataGridView_product.SelectedRows[0].Cells[1].Value.ToString();


        }
        int grandTotal = 0;
        int n = 0;
        private void button_add_order_Click(object sender, EventArgs e)
        {
            if (textBox_name.Text == string.Empty || textBox_qty.Text == string.Empty)
            {
                MessageBox.Show("Missing Information", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int total = int.Parse(textBox_price.Text) * int.Parse(textBox_qty.Text);
                DataGridViewRow addRow = new DataGridViewRow();
                addRow.CreateCells(dataGridView_order);
                addRow.Cells[0].Value = n++;
                addRow.Cells[1].Value = textBox_name.Text;
                addRow.Cells[2].Value = textBox_price.Text;
                addRow.Cells[3].Value = textBox_qty.Text;
                addRow.Cells[4].Value = total;
                dataGridView_order.Rows.Add(addRow);
                grandTotal += total;
                label_amount.Text = $"{grandTotal} Kgs";
            }

        }



        private void button_add_Click(object sender, EventArgs e)
        {

            try
            {
                string insertQuery = $"INSERT INTO Bill VALUES('{textBox_id.Text}','{label_seller.Text}','{label_date.Text}','{grandTotal}')";
                SqlCommand command = new SqlCommand(insertQuery, DBCon.GetCon());
                DBCon.OpenCon();
                command.ExecuteNonQuery();
                MessageBox.Show("Order Added Successfully", "Order information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getSellTable();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button_print_Click(object sender, EventArgs e)
        {
            printer.Title = "Minimarket Sell List";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Nikolay Yonchev";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_sellList);
        }

        private void label_logout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
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

        private void label_logout_MouseEnter(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Red;
        }

        private void label_logout_MouseLeave(object sender, EventArgs e)
        {
            label_logout.ForeColor = Color.Goldenrod;
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            getTable();
        }

        private void comboBox_categories_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectQuery = $"SELECT ProdName,ProdPrice FROM Product WHERE ProdCat='{comboBox_categories.SelectedValue.ToString()}'";
            SqlCommand command = new SqlCommand(selectQuery, DBCon.GetCon());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            DataGridView_product.DataSource = Table;
        }

        
    }
}
