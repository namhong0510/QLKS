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
using System.Data;
namespace QLKS
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();

        }

        //Khi an vao button dang nhap se thuc hien show form tuong ung va an form hien tai
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Form f = nextForm(txbTK.Text, txbMK.Text);
            if (f == null)
                return;
            f.FormClosed += f_FormClosed; //xu ly khi dong form thi se chay event show lai form nay
            f.StartPosition = FormStartPosition.CenterScreen; //set vi tri form mo ra luon la o giua
            f.Show();
            this.Hide();

            ClearTextBox();
        }

        
        void f_FormClosed(object sender, EventArgs e)
        {
            this.Show();
        }
        void ClearTextBox()
        {
            txbTK.Clear();
            txbMK.Clear();
        }
        Form nextForm(string id, string pass)
        {
            Form f = new Form();
            int roll = 0;
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                var t = db.Users.Where(p => p.IDNhanVien.Equals(id) && p.Pass.Equals(pass));
                User u = t.Count() == 1? t.Single() : null;
                if (u != null)
                {
                    NhanVien nv = db.NhanViens.Where(p => u.IDNhanVien.Equals(p.ID)).Single();
                    roll = (int)nv.ChucDanh.Roll;
                    Cons.LoginNhanvien = nv;
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng");
                    return null;
                }
            }
                switch (roll)
                {

                    case 1:
                        f = new QuanLy();
                        break;
                    case 2:
                        f = new FrmMain();
                        break;
                    case 3:
                        f = new ThongKeBaoCao();
                        break;
                }
            return f;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txbTK.Select();
        }

        private void txtTK_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
