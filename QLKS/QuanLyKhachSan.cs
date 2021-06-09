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
    public partial class FrmMain : Form
    {

        
   

        public FrmMain()
        {
            InitializeComponent();
        }
        #region Method
        void LoadLoaiPhong(ComboBox cb)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                cb.DataSource = db.LoaiPhongs.ToList();
                cb.DisplayMember = "Ten";

            }
        }
        void LoadTinhTrangPhong(ComboBox cb)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                cb.DataSource = db.TinhTrangPhongs.ToList();
                cb.DisplayMember = "Ten";
            }
        }
        void BindingKH(DataGridView dtgv)
        {
            Binding bdID = new Binding("Text", dtgv.DataSource, "ID", true, DataSourceUpdateMode.OnPropertyChanged);
            txbIDHK.DataBindings.Clear();
            txbIDHK.DataBindings.Add(bdID);
        }
        void BindingPhong(DataGridView dtgv)
        {
            Binding bdID = new Binding("Text", dtgv.DataSource, "ID", true, DataSourceUpdateMode.OnPropertyChanged);
            txbIDPhong.DataBindings.Clear();
            txbIDPhong.DataBindings.Add(bdID);

            Binding bdTen = new Binding("Text", dtgv.DataSource, "Tên", true, DataSourceUpdateMode.OnPropertyChanged);
            txbTenPhong.DataBindings.Clear();
            txbTenPhong.DataBindings.Add(bdTen);

            Binding bdGia = new Binding("Text", dtgv.DataSource, "Giá", true, DataSourceUpdateMode.OnPropertyChanged);
            txbGia.DataBindings.Clear();
            txbGia.DataBindings.Add(bdGia);

            Binding bdSoGiuong = new Binding("Text", dtgv.DataSource, "SốGiường", true, DataSourceUpdateMode.OnPropertyChanged);
            txbSoGiuong.DataBindings.Clear();
            txbSoGiuong.DataBindings.Add(bdSoGiuong);

        }

        //Loai thong tin phong trong dataGridView tuong ung
        void LoadPhong(DataGridView dtgv)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                var source = from p in db.Phongs
                             from l in db.LoaiPhongs
                             from t in db.TinhTrangPhongs
                             where p.IDLoai.Equals(l.ID) && p.IDTinhTrang.Equals(t.ID)
                             select new
                             {
                                 Id = p.ID,
                                 Tên = p.Ten,
                                 Loại = l.Ten,
                                 TìnhTrạng = t.Ten,
                                 Giá = l.Gia,
                                 SốGiường = l.SoGiuong

                             };
                dtgv.DataSource = source.ToList();
            }
            BindingPhong(dtgvPhong);
            
            
        }
        //load thong tin co so vat chat trong dtgv tuong ung
        void LoadCoSoVatChat(DataGridView dtgv)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                var source = from c in db.CoSoVatChats
                             from p in db.Phongs
                             from tt in db.TrangThaiThuePhongs
                             from pvt in db.PhongVatTus
                             where pvt.IDPhong.Equals(p.ID) && pvt.IDCoSoVatChat.Equals(c.ID) && c.IDTinhTrang.Equals(tt.ID)
                             select new
                             {
                                 Id = c.ID,
                                 Tên = c.Ten,
                                 GiáBồiThường = c.GiaBoiThuong,
                                 TìnhTrạng = tt.Ten
                             };
                dtgv.DataSource = source.ToList();
            }
            
        }
        //Load thong tin thue phong
        void LoadKhachHang(DataGridView dtgv)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                var source = from k in db.KhachHangs                            
                             
                             select new
                             {
                                 Id = k.ID,
                                 Tên = k.Ten,
                                 NgàySinh = k.NgaySinh,
                                 Email = k.Email,
                                 CMND = k.CMND,
                                 Sdt = k.SDT,
                                 QuốcTịch = k.QuocTich,
                                 GhiChú = k.GhiChu

                             };
                dtgv.DataSource = source.ToList();
            }
            BindingKH(dtgvKhachHang);
        }
        void LoadDichVu(DataGridView dtgv) {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                var source = from d in db.DichVus
                             from l in db.LoaiDichVus
                             from b in db.BangThuePhongs
                             
                             where d.IDLoai.Equals(l.ID) && b.ID.Equals(d.ID)
                             select new
                             {
                                 Id = d.ID,
                                 Tên = d.Ten,
                                 Loại = l.Ten,
                                 Giá = d.Gia,
                                 Khách = b.KhachHang.Ten
                                

                             };
                dtgv.DataSource = source.ToList();
            }
        }
        void LoadDV(DataGridView dtgv)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                var source = from d in db.DichVus
                             from l in db.LoaiDichVus

                             where d.IDLoai.Equals(l.ID) 
                             select new
                             {
                                 Id = d.ID,
                                 Tên = d.Ten,
                                 Loại = l.Ten,
                                 Giá = d.Gia,

                             };
                dtgv.DataSource = source.ToList();
            }
               
        }
        #endregion

        #region event
        private void FrmMain_Load(object sender, EventArgs e)
        {
            tsmiTenNhanVien.Text = Cons.LoginNhanvien.Ten;
            LoadPhong(dtgvPhong);
            LoadPhong(dtgvThuePhong);
            LoadPhong(dtgvPhongVT);
            
            LoadCoSoVatChat(dtgvCoSoVatChat);
            LoadKhachHang(dtgvKhachHang);
            LoadLoaiPhong(cbLoaiPhong);
            LoadTinhTrangPhong(cbTinhTrang);
            LoadDichVu(dtgvDichVu);
            LoadDV(dtgvDV);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using(QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                
                string id = (Int32.Parse(db.Phongs.Select(idp => idp.ID).Max())+1).ToString();
                string ten = txbTenPhong.Text;
                if(db.Phongs.Select(tp => tp.Ten).Contains(ten))
                {
                    MessageBox.Show("Tên phòng đã tồn tại");
                    return;
                }
                int loai = (cbLoaiPhong.SelectedValue as LoaiPhong).ID;

                int tinhtrang = (cbTinhTrang.SelectedValue as TinhTrangPhong).ID;
                Phong p = new Phong()
                {   
                    ID = id,
                    Ten = ten,
                    IDLoai = loai,
                    IDTinhTrang = tinhtrang

                };
                db.Phongs.Add(p);
                db.SaveChanges();
                MessageBox.Show("Thêm phòng thành công");
                LoadPhong(dtgvPhong);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {

                string id = txbIDPhong.Text;
                db.Phongs.Remove(db.Phongs.Find(id));
                db.SaveChanges();
                MessageBox.Show("Xóa phòng thành công");
                LoadPhong(dtgvPhong);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {

                string id = txbIDPhong.Text;
                Phong phong = db.Phongs.Find(id);
                int loai = (cbLoaiPhong.SelectedValue as LoaiPhong).ID;
                int tinhtrang = (cbTinhTrang.SelectedValue as TinhTrangPhong).ID;
                phong.IDLoai = loai;
                phong.IDTinhTrang = tinhtrang;
                
                db.SaveChanges();
                MessageBox.Show("Sửa phòng thành công");
                LoadPhong(dtgvPhong);
            }
        }


 


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

 

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            DoiMatKhau f = new DoiMatKhau();
            f.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cons.LoginNhanvien = null;
            this.Close();
        }


        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dtgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            if (cbLoaiPhong.DataSource == null || dtgvPhong.SelectedCells.Count == 0)
                return;
            String loai = dtgvPhong.SelectedCells[0].OwningRow.Cells["Loại"].Value.ToString();
            int index = 0;
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                index = db.LoaiPhongs.Select(p => p.Ten).ToList().IndexOf(loai);
            }
            cbLoaiPhong.SelectedIndex = index;

            if (cbTinhTrang.DataSource == null || dtgvPhong.SelectedCells.Count == 0)
                return;
            String loaiTinhTrang = dtgvPhong.SelectedCells[0].OwningRow.Cells["Loại"].Value.ToString();
            int index_ = 0;
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {
                index_ = db.LoaiPhongs.Select(p => p.Ten).ToList().IndexOf(loaiTinhTrang);
            }
            cbTinhTrang.SelectedIndex = index_;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

      
        private void btnThemKH_Click(object sender, EventArgs e)
        {
           
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {

                string id = (Int32.Parse(db.KhachHangs.Select(idp => idp.ID).Max()) + 1).ToString();
                string ten = txbTenKH.Text;
                string sdt = txbSDT.Text;
                string ngaysinh = dtpHK.Value.ToString("dd/MM/yyyy");


                string email = txbEmail.Text;
                string cmnd = txbCMND.Text;
                string quoctich = txbQuocTich.Text;
                string ghichu = txbGhiChu.Text;
                KhachHang k = new KhachHang()
                {
                    ID = id,
                    Ten = ten,
                    SDT = sdt,
                    //NgaySinh = ngaysinh,
                    CMND = cmnd,
                    Email = email,
                    QuocTich = quoctich,
                    GhiChu = ghichu

                   
                    

                };
                db.KhachHangs.Add(k);
                db.SaveChanges();
                MessageBox.Show("Thêm phòng thành công");
                LoadKhachHang(dtgvKhachHang);
            }
        }

        private void cbGioiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {

                string id = txbIDHK.Text;
                db.KhachHangs.Remove(db.KhachHangs.Find(id));
                /*string ten = txbTenKH.Text;
                db.KhachHangs.Remove(db.KhachHangs.Find(ten));
                string email = txbEmail.Text;
                db.KhachHangs.Remove(db.KhachHangs.Find(email));
                string cmnd = txbCMND.Text;
                db.KhachHangs.Remove(db.KhachHangs.Find(cmnd));
                string sdt = txbSDT.Text;
                db.KhachHangs.Remove(db.KhachHangs.Find(sdt));
                string quoctich = txbQuocTich.Text;
                db.KhachHangs.Remove(db.KhachHangs.Find(quoctich));
                string ghichu = txbGhiChu.Text;
                db.KhachHangs.Remove(db.KhachHangs.Find(ghichu));*/
                db.SaveChanges();
                MessageBox.Show("Xóa phòng thành công");
                LoadKhachHang(dtgvKhachHang);
            }
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {

                string id = txbIDHK.Text;
                KhachHang k = db.KhachHangs.Find(id);
                string ten = txbTenKH.Text;
                string sdt = txbSDT.Text;
                string email = txbEmail.Text;
                string cmnd = txbCMND.Text;
                string quoctich = txbQuocTich.Text;
                string ghichu = txbGhiChu.Text;

                k.Ten = ten;
                k.SDT = sdt;
                k.Email = email;
                k.CMND = cmnd;
                k.QuocTich = quoctich;
                k.GhiChu = ghichu;

                db.SaveChanges();
                MessageBox.Show("Sửa phòng thành công");
                LoadKhachHang(dtgvKhachHang);
            }
        }

        private void txbIDPhong_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            using (QuanLyKhachSanEntities db = new QuanLyKhachSanEntities())
            {

                string id = (Int32.Parse(db.Phongs.Select(idp => idp.ID).Max()) + 1).ToString();
                string ten = txbTenPhong.Text;
                if (db.Phongs.Select(tp => tp.Ten).Contains(ten))
                {
                    MessageBox.Show("Tên phòng đã tồn tại");
                    return;
                }
                int loai = (cbLoaiPhong.SelectedValue as LoaiPhong).ID;

                int tinhtrang = (cbTinhTrang.SelectedValue as TinhTrangPhong).ID;
                Phong p = new Phong()
                {
                    ID = id,
                    Ten = ten,
                    IDLoai = loai,
                    IDTinhTrang = tinhtrang

                };
                db.Phongs.Add(p);
                db.SaveChanges();
                MessageBox.Show("Đặt phòng thành công");
                LoadPhong(dtgvPhong);
            }
        }

        private void btnThuePhong_Click(object sender, EventArgs e)
        {
            DP f = new DP();
            f.ShowDialog();
            this.Hide();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {
                    }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            DP f = new DP();
            f.ShowDialog();
            this.Hide();
        }
    }
    #endregion
}
