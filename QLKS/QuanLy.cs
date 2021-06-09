using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS
{
    public partial class QuanLy : Form
    {
        public QuanLy()
        {
            InitializeComponent();
        }

        private void tpNhanSu_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void thốngKêBáoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongKeBaoCao f = new ThongKeBaoCao();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            
        }

        private void quảnLýThuêPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMain f = new FrmMain();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
        }

        

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cons.LoginNhanvien = null;
            this.Close();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            DoiMatKhau f = new DoiMatKhau();
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void QuanLy_Load(object sender, EventArgs e)
        {
            tsmiTenManager.Text = Cons.LoginNhanvien.Ten;
            LoadNhanVien(dtgvNhanVien);
            LoadBangLuong(dtgvBangLuong);
        }
        void LoadBangLuong(DataGridView dtgv)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                var source = from n in db.NhanViens
                             from l in db.BangLuongs
                             from c in db.ChucDanhs

                             where n.ID.Equals(l.IDNhanVien) && c.ID.Equals(n.IDChucDanh)
                             select new
                             {
                                 Id = l.ID,
                                 ChứcDanh = n.ChucDanh.Ten,
                                 Tên = n.Ten,
                                 Ngày = l.Ngay,
                                 Lương = l.Luong,
                                 TrợCấp = l.TroCap

                             };
                dtgv.DataSource = source.ToList();
            }
        }
        void LoadNhanVien(DataGridView dtgv)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                var source = from n in db.NhanViens
                             from c in db.ChucDanhs
                             from b in db.BoPhans
                             
                             where n.IDBoPhan.Equals(b.ID) && n.IDChucDanh.Equals(c.ID)
                             select new
                             {
                                 Id = n.ID,
                                 BộPhận = b.Ten,
                                 ChứcDanh = c.Ten,
                                 Tên = n.Ten,
                                 GiớiTính = n.GioiTinh,
                                 NgàySinh = n.NgaySinh,
                                 ĐịaChỉ = n.DiaChi,
                                 SDT = n.SDT

                             };
                dtgv.DataSource = source.ToList();
            }
        }
    }
}
