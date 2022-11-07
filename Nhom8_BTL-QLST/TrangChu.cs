using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetBrowser;
using System.Windows.Forms;
using DotNetBrowser.Browser;
using DotNetBrowser.Engine;


namespace Nhom8_BTL_QLST
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Bật/tắt các nút nhân sự, thú, chuồng, báo cáo
        /// </summary>
        private void buttonForAdmin(bool type)
        {
            btnNhanSu.Enabled = type;
            btnThu.Enabled = type;
            btnChuong.Enabled = type;
            btnBaoCao.Enabled = type;

            btnNhanSu.BackColor = Color.LightGray;
            btnThu.BackColor = Color.LightGray;
            btnChuong.BackColor = Color.LightGray;
            btnBaoCao.BackColor = Color.LightGray;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                buttonForAdmin(false);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var login = new DangNhap();
            login.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //public void showPanelMain(bool type)
        //{
        //    pnMain.Controls.Clear();
        //    panel6.Visible = type;
        //    panel8.Visible = type;
        //}
        private void btnNhanSu_Click(object sender, EventArgs e)
        {
            //Ẩn panel main và show footer
            pnFooter.Visible = true;
            pnMain.Controls.Clear();

            //fill User control nhân viên vào main
            PageNhanVien pageNhanVien = new PageNhanVien();
            pageNhanVien.Dock = DockStyle.Fill;
            pnMain.Controls.Add(pageNhanVien);
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            //Ẩn panel main và show footer
            pnFooter.Visible = false;
            pnMain.Controls.Clear();

            //fill User control trang chủ vào main
            PageTrangChu pageTrangChu = new PageTrangChu();
            pageTrangChu.Dock = DockStyle.Fill;
            pnMain.Controls.Add(pageTrangChu);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Ẩn panel main và show footer
            pnFooter.Visible = true;
            pnMain.Controls.Clear();

            //fill User control thú vào main
            PageThu pageThu = new PageThu();
            pageThu.Dock = DockStyle.Fill;
            pnMain.Controls.Add(pageThu);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Ẩn panel main và show footer
            pnFooter.Visible = true;
            pnMain.Controls.Clear();

            //fill User control thú vào main
            PageChuong pageChuong = new PageChuong();
            pageChuong.Dock = DockStyle.Fill;
            pnMain.Controls.Add(pageChuong);
        }

        private void btnNhapLieu_Click(object sender, EventArgs e)
        {
            //Ẩn panel main và show footer
            pnFooter.Visible = true;
            pnMain.Controls.Clear();

            //fill User control thú vào main
            PageThongKe pageThongKe = new PageThongKe();
            pageThongKe.Dock = DockStyle.Fill;
            pnMain.Controls.Add(pageThongKe);
        }

        private void btnLienHe_Click(object sender, EventArgs e)
        {

        }
    }
}
