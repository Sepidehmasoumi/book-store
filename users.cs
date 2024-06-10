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
    public partial class users : Form
    {
        public users()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)/MSSQLLocalDB;AttachDbFilename=C: /Users/PARS NOVIN\Documents/bookshopdb.mdf;Integrated Security=True;Connect Timeout=30 ");
        private void populate()
        {
            con.Open();
            string query = "select*from usertbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            userdgv.DataSource = ds.Tables[0];
            con.Close();
        }
            private void savebtn_Click(object sender, EventArgs e)
        {
            if (unametb.Text == "" || addtb.Text == "" || phonetb.Text == "" || passtb.Text == "")
            {
                MessageBox.Show("اطلاعات ناقص است");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into usertbl values('" + phonetb.Text + "', '" + unametb.Text + "','" +addtb.Text+ "', " + passtb.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("کاربر با موفقیت ذخیره شد");
                    con.Close();
                    populate();
                    //reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        private void reset()
        {
            unametb.Text = "";
            passtb.Text = "";
            phonetb.Text = "";
            addtb.Text = "";
        }
        private void resetbtn_Click(object sender, EventArgs e)
        {
            reset();
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
                    string query = "delete from usertbl where uid=" + key + ";";
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
        int key = 0;

         private void userdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            unametb.Text = userdgv.SelectedRows[0].Cells[1].Value.ToString();
            phonetb.Text = userdgv.SelectedRows[0].Cells[2].Value.ToString();
            addtb.Text = userdgv.SelectedRows[0].Cells[3].Value.ToString();
            passtb.Text = userdgv.SelectedRows[0].Cells[4].Value.ToString();
           
            if (unametb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32( userdgv.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
