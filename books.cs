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
    public partial class books : Form
    {
        public books()
        {
            InitializeComponent();
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
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
        private void filter()
        {
            con.Open();
            string query = "select*from booktbl where b cat='"+catcbsearchcb.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            bookdgv.DataSource = ds.Tables[0];

            con.Close();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if(btitletb.Text==""||bauthtb.Text==""||qtytb.Text==""||pricetb.Text==""||bcatcb.SelectedIndex == -1)
            {
                MessageBox.Show("اطلاعات ناقص است");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into booktbl values('" + btitletb.Text + "', '" + bauthtb.Text + "','" + bcatcb.SelectedItem.ToString() + "', " + qtytb.Text + " ," + pricetb.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, con); 
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("کتاب با موفقیت ذخیره شد");
                    con.Close();
                    populate();
                    reset();
                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void catcbsearchcb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filter();
        }
        private void reset()
        {
            btitletb.Text = "";
            bauthtb.Text = "";
            bcatcb.SelectedIndex = -1;
            pricetb.Text = "";
            qtytb.Text = "";

        }
        private void resetbtn_Click(object sender, EventArgs e)
        {
            reset();
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
            catcbsearchcb.SelectedIndex = -1;
        }
        int key = 0;
        private void bookdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btitletb.Text = bookdgv.SelectedRows[0].Cells[1].Value.ToString();
            bauthtb.Text= bookdgv.SelectedRows[0].Cells[2].Value.ToString();
            bcatcb.SelectedItem = bookdgv.SelectedRows[0].Cells[3].Value.ToString();
            qtytb.Text = bookdgv.SelectedRows[0].Cells[4].Value.ToString();
            pricetb.Text = bookdgv.SelectedRows[0].Cells[5].Value.ToString();
            if (btitletb.Text == "")
            {
                key = 0; 
            }else
            {
                key = Convert.ToInt32( bookdgv.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            { 
                MessageBox.Show("اطلاعات ناقص است");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("کتاب با موفقیت ذخیره شد");
                    con.Close();
                    populate();
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void editbtn_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
