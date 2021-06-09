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
    public partial class ThongKeBaoCao : Form
    {
        public ThongKeBaoCao()
        {
            InitializeComponent();
        }

        private void tpChi_Click(object sender, EventArgs e)
        {

        }

        private void vềCửaSổChínhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cons.LoginNhanvien = null;
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ThongKeBaoCao_Load(object sender, EventArgs e)
        {
            tsmiTenKeToan.Text = Cons.LoginNhanvien.Ten;
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            DoiMatKhau f = new DoiMatKhau();
            f.ShowDialog();
        }

        private void chiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void thuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DP dp = new DP();
            dp.StartPosition = FormStartPosition.CenterScreen;
            dp.ShowDialog();
        }
    }
}
