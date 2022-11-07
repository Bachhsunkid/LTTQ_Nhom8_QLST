using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Nhom8_BTL_QLST
{
    public partial class PageThu : UserControl
    {
        ProcessDatabase processDatabase = new ProcessDatabase();
        public PageThu()
        {
            InitializeComponent();
        }
        //Kiem tra ma thu nhap vao da ton tai hay chua
        public bool validateExistKey()
        {
            try
            {
                string maThu = txtMaThu.Text;

                DataTable dataTable = processDatabase.docBang("Select MaThu from Thu where" + " MaThu='" + maThu + "'");
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Mã khách hàng này không tồn tại, bạn hãy nhập mã khác!");
                    txtMaThu.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        //Fill dữ liệu từ DB vào các combobox
        private void FillDataToCombobox()
        {
            DataTable dataTable = new DataTable();
            //Fill dữ liệu từ DB vào combobox Mã Loài
            dataTable = processDatabase.docBang("Select distinct TenLoai from Loai");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbLoaiThu1.Items.Add(dataTable.Rows[i][0].ToString());
                cbbLoaiThu2.Items.Add(dataTable.Rows[i][0].ToString());
            }

            //Fill dữ liệu từ DB vào combobox Kiểu Sinh
            dataTable = processDatabase.docBang("Select distinct TenKieuSinh from KieuSinh");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbKieuSinh1.Items.Add(dataTable.Rows[i][0].ToString());
                cbbKieuSinh2.Items.Add(dataTable.Rows[i][0].ToString());
            }

            //Fill dữ liệu từ DB vào combobox Giới tính
            dataTable = processDatabase.docBang("Select distinct GioiTinh from Thu");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbGioiTinh.Items.Add(dataTable.Rows[i][0].ToString());

            }
            //Fill dữ liệu từ DB vào combobox Tên thú
            dataTable = processDatabase.docBang("Select distinct TenThu from Thu");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbTenThu.Items.Add(dataTable.Rows[i][0].ToString());

            }
            //Fill dữ liệu từ DB vào combobox NguonGoc
            dataTable = processDatabase.docBang("Select distinct TenNguonGoc from NguonGoc");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbNguonGoc.Items.Add(dataTable.Rows[i][0].ToString());

            }
        }
        //Lấy danh sách thú hiển thị lên datagridview
        private void GetListAnimal()
        {
            //Hien thi danh sach thu len datagridview
            DataTable dataTable = new DataTable();
            dataTable = processDatabase.docBang("select MaThu, TenThu, MaLoai, SoLuong, SachDo, TenKhoaHoc, TenTA, MaKieuSinh, GioiTinh, NgayVao, MaNguonGoc, DacDiem, NgaySinh, TuoiTho, Anh from thu");
            dgvDanhSachThu.DataSource = dataTable;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //Lấy danh sách thú hiển thị lên datagridview
            GetListAnimal();
            //Fill dữ liệu từ DB vào các combobox
            FillDataToCombobox();
        }


        private void PageThu_Load(object sender, EventArgs e)
        {
            
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            //Lấy danh sách thú hiển thị lên datagridview
            GetListAnimal();

            //Xóa bỏ selected
            cbbTenThu.SelectedIndex = -1;
            cbbLoaiThu2.SelectedIndex = -1;
            cbbKieuSinh2.SelectedIndex = -1;
            cbbNguonGoc.SelectedIndex = -1;

            //xóa bỏ text
            cbbTenThu.Text = "";
            cbbLoaiThu2.Text = "";
            cbbKieuSinh2.Text = "";
            cbbNguonGoc.Text = "";
        }

        private void PageThu_Load_1(object sender, EventArgs e)
        {

        }

        //Lay Ten loai theo ma loai
        private string MaLoaiToTenLoai(string maLoai)
        {
            string tenLoai="";
            tenLoai = processDatabase.docBang("Select TenLoai from Loai where maloai =  N'" +maLoai+ "' ").Rows[0][0].ToString();
            return tenLoai;
        }
        //Lay Ten kieu sinh theo ma kieu sinh
        private string MaKSToTenKS(string maKS)
        {
            string tenKieuSinh = "";
            tenKieuSinh = processDatabase.docBang("Select TenKieuSinh from KieuSinh where MaKieuSinh =  N'" + maKS + "' ").Rows[0][0].ToString();
            return tenKieuSinh;
        }

        //Binding du lieu khi nhan vao 1 dong trong datagridview
        private void dgvDanhSachThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaThu.Text = dgvDanhSachThu.CurrentRow.Cells[0].Value.ToString();
            txtTenThu.Text = dgvDanhSachThu.CurrentRow.Cells[1].Value.ToString();
            //xu ly ma loai convert sang ten loai
            string maLoai = dgvDanhSachThu.CurrentRow.Cells[2].Value.ToString();
            cbbLoaiThu1.Text = MaLoaiToTenLoai(maLoai);

            txtSoLuong.Text = dgvDanhSachThu.CurrentRow.Cells[3].Value.ToString();
            //Xu ly binding radio button
            if (dgvDanhSachThu.CurrentRow.Cells[4].Value.ToString().Equals("True"))
            {
                rdbCo.Checked = true;
                rdbKhong.Checked = false;
            }
            else
            {
                rdbCo.Checked = false;
                rdbKhong.Checked = true;
            }
            txtTenKH.Text = dgvDanhSachThu.CurrentRow.Cells[5].Value.ToString();
            txtTenTA.Text = dgvDanhSachThu.CurrentRow.Cells[6].Value.ToString();

            //xu ly ma kieu sinh convert sang ten kieu sinh
            string maKS = dgvDanhSachThu.CurrentRow.Cells[7].Value.ToString();
            cbbKieuSinh1.Text = MaKSToTenKS(maKS);

            cbbGioiTinh.Text = dgvDanhSachThu.CurrentRow.Cells[8].Value.ToString();
            //Xu li binding to datetimepicker ngay vao
            dtpNgayVao.Text = Convert.ToDateTime(dgvDanhSachThu.CurrentRow.Cells[9].Value.ToString()).ToString();

            txtNguonGoc.Text = dgvDanhSachThu.CurrentRow.Cells[10].Value.ToString();
            txtDacDiem.Text = dgvDanhSachThu.CurrentRow.Cells[11].Value.ToString();
            //Xu li binding to datetimepicker ngay sinh
            dtpNgaySinh.Text = Convert.ToDateTime(dgvDanhSachThu.CurrentRow.Cells[12].Value.ToString()).ToString();

            txtTuoiTho.Text = dgvDanhSachThu.CurrentRow.Cells[13].Value.ToString();

            //binding picture
            //string pictureURL = dgvDanhSachThu.CurrentRow.Cells[14].Value.ToString();
            //ptbThu.ImageLocation = pictureURL;
        }
    }
}
