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
    public partial class DP : Form
    {
        public DP()
        {
            InitializeComponent();
        }
        void LoadLoaiPhong(ComboBox cb)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                cb.DataSource = db.LoaiPhongs.ToList();
                cb.DisplayMember = "Ten";

            }
        }
        void LoadPhong(DataGridView dtgv)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                var source = from p in db.Phongs
                             from l in db.LoaiPhongs
                             from t in db.TrangThaiThuePhongs
                             from b in db.BangThuePhongs
                             from k in db.KhachHangs
                             where p.IDLoai.Equals(l.ID) && p.ID.Equals(b.IDPhong) && k.ID.Equals(b.IDKhachHang) && b.IDTrangThai.Equals(t.ID)
                             select new
                             {
                                 Id = p.ID,
                                 Tên = p.Ten,
                                 Loại = l.Ten,
                                 KháchHàng = k.Ten,
                                 Giá = l.Gia,
                                 TrạngThái = t.Ten,
                                 SốGiường = l.SoGiuong

                             };
                dtgv.DataSource = source.ToList();
            }
        }
        private void txtKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void DP_Load(object sender, EventArgs e)
        {
            LoadLoaiPhong(cbLoai);
            LoadPhong(dgv);
        }



        private void dvg_(object sender, EventArgs e)
        {
            if (cbLoai.DataSource == null || dgv.SelectedCells.Count == 0)
                return;
            String loai = dgv.SelectedCells[0].OwningRow.Cells["Loại"].Value.ToString();
            int index = 0;
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                index = db.LoaiPhongs.Select(p => p.Ten).ToList().IndexOf(loai);
            }
            cbLoai.SelectedIndex = index;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {

                string id = (Int32.Parse(db.Phongs.Select(idp => idp.ID).Max()) + 1).ToString();
                string ten = txtPhong.Text;
                if (db.Phongs.Select(tp => tp.Ten).Contains(ten))
                {
                    MessageBox.Show("Tên phòng đã tồn tại");
                    return;
                }
                int loai = (cbLoai.SelectedValue as LoaiPhong).ID;


                Phong p = new Phong()
                {
                    ID = id,
                    Ten = ten,
                    IDLoai = loai,


                };
                db.Phongs.Add(p);
                db.SaveChanges();
                MessageBox.Show("Thêm phòng thành công");
                LoadPhong(dgv);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {

                string tphong = txtPhong.Text;
                db.Phongs.Remove(db.Phongs.Find(tphong));
                db.SaveChanges();
                MessageBox.Show("Xóa phòng thành công");
                LoadPhong(dgv);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {

                string tphong = txtPhong.Text;
                Phong phong = db.Phongs.Find(tphong);
                int loai = (cbLoai.SelectedValue as LoaiPhong).ID;

                phong.IDLoai = loai;


                db.SaveChanges();
                MessageBox.Show("Sửa phòng thành công");
                LoadPhong(dgv);
            }
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                db.SaveChanges();
                this.Refresh();
            }
        }

        private void btnTT_Click(object sender, EventArgs e)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {



                DateTime ngayDen = Convert.ToDateTime(DateDen.Text);
                DateTime ngayDi = Convert.ToDateTime(DateDi.Text);

                TimeSpan time = ngayDi.Subtract(ngayDen);
                int ngay = time.Days;
                txtTT.Text = (ngay * 1500000).ToString();

            }
        }
    }
}
