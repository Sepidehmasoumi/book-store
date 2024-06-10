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
namespace book_store
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
            populate();
        }
       
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)/MSSQLLocalDB;AttachDbFilename=C: /Users/PARS NOVIN\Documents/bookshopdb.mdf;Integrated Security=True;Connect Timeout=30 ");
        private void populate()
        {
            con.Open();
            string query = "select*from booktbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            bookdgv.DataSource = ds.Tables[0];

            con.Close();
        }
        int n = 0;
        private void savebtn_Click(object sender, EventArgs e)
        {
            int n = 0;
            if (qtytb.Text == "" || Convert.ToInt32(qtytb.Text) > stock)
            {
                MessageBox.Show("اطلاعات کافی نیست");
            }
            else
            {
                int total = Convert.ToInt32(qtytb.Text) * Convert.ToInt32(pricetb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(billdgv);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = btitletb.Text;
                newRow.Cells[2].Value = qtytb.Text;
                newRow.Cells[3].Value = pricetb.Text;
                newRow.Cells[4].Value = total;
                billdgv.Rows.Add(newRow);
                n++;

            }
        }
        int key = 0,stock=0;
        private void bookdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btitletb.Text = bookdgv.SelectedRows[0].Cells[1].Value.ToString();
         
            //qtytb.Text = bookdgv.SelectedRows[0].Cells[4].Value.ToString();
            pricetb.Text = bookdgv.SelectedRows[0].Cells[5].Value.ToString();
            if (btitletb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(bookdgv.SelectedRows[0].Cells[0].Value.ToString());
                stock= Convert.ToInt32(bookdgv.SelectedRows[0].Cells[4].Value.ToString());
            }
        
       }
        private void reset()
        {
            btitletb.Text = "";
            qtytb.Text = "";
            pricetb.Text = "";
            clientnametb.Text = "";
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
   }
