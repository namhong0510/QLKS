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
namespace QLKS
{
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-5T3PHNN;Initial Catalog=QLKS;Integrated Security=True");

        private void btnXN_Click(object sender, EventArgs e)
        {
            if (txtMK.Text == "") MessageBox.Show("Chưa nhập mật khẩu");
            else if (txtMKM.Text == "") MessageBox.Show("Chưa nhập mật khẩu mới");
            else if (txtNLMK.Text == "") MessageBox.Show("Chưa nhập lại mật khẩu mới");
            else if (txtMKM.Text != txtNLMK.Text) MessageBox.Show("Nhập mật khẩu không trùng khớp");
            else
            {



                SqlDataAdapter da = new SqlDataAdapter("Select Count (*) From [Users] where  Pass ='" + txtMK.Text + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                errorProvider1.Clear();
                while (dt.Rows[0][0].ToString() == "1")
                {
                    while (txtMKM.Text == txtNLMK.Text)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("update [Users] set Pass ='" + txtMKM.Text + "' where Pass = '" + txtMK.Text + "'", conn);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }
    }
}
