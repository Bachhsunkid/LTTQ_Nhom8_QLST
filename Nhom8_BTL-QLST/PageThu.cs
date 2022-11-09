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
                cbbNguonGoc1.Items.Add(dataTable.Rows[i][0].ToString());
                cbbNguonGoc2.Items.Add(dataTable.Rows[i][0].ToString());
            }
        }
        //Lấy danh sách thú hiển thị lên datagridview
        private void GetListAnimal()
        {
            //Hien thi danh sach thu len datagridview
            DataTable dataTable = new DataTable();
            dataTable = processDatabase.docBang("select * from view_thu");
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
            cbbNguonGoc2.SelectedIndex = -1;

            //xóa bỏ text
            cbbTenThu.Text = "";
            cbbLoaiThu2.Text = "";
            cbbKieuSinh2.Text = "";
            cbbNguonGoc2.Text = "";
        }

        private void PageThu_Load_1(object sender, EventArgs e)
        {

        }

        private Thu GetThu()
        {
            Thu thu = new Thu();
            thu.MaThu = txtMaThu.Text;
            thu.TenThu = txtTenThu.Text;
            //convert tu ten loai sang ma loai
            thu.MaLoai = processDatabase.docBang("Select MaLoai from Loai where TenLoai = N'" + cbbLoaiThu1.Text + "' ").Rows[0][0].ToString();
            thu.SoLuong = int.Parse(txtSoLuong.Text);
            if (rdbCo.Checked == true)
            {
                thu.SachDo = 1;
            }
            else
            {
                thu.SachDo = 0;
            }
            thu.TenKH = txtTenKH.Text;
            thu.TenTA = txtTenTA.Text;
            //convert tu ten kieu sinh sang ma kieu sinh
            thu.MaKS = processDatabase.docBang("Select MaKieuSinh from KieuSinh where TenKieuSinh = N'" + cbbKieuSinh1.Text + "' ").Rows[0][0].ToString();
            thu.GioiTinh = cbbGioiTinh.Text;
            thu.NgayVao = Convert.ToDateTime(dtpNgayVao.Value.ToString());
            //convert tu ten nguon goc sang ma nguon goc
            thu.MaNG = processDatabase.docBang("Select MaNguonGoc from NguonGoc where TenNguonGoc = N'" + cbbNguonGoc1.Text + "' ").Rows[0][0].ToString();
            thu.DacDiem = txtDacDiem.Text;
            thu.NgaySinh = Convert.ToDateTime(dtpNgaySinh.Value.ToString());
            thu.TuoiTho = int.Parse(txtTuoiTho.Text == "" ? "0" : txtTuoiTho.Text);
            thu.Anh = txtAnh.Text;
            return thu;
        }

        //Binding du lieu khi nhan vao 1 dong trong datagridview
        private void dgvDanhSachThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaThu.Text = dgvDanhSachThu.CurrentRow.Cells[0].Value.ToString();
            txtTenThu.Text = dgvDanhSachThu.CurrentRow.Cells[1].Value.ToString();
            //xu ly ma loai convert sang ten loai
            cbbLoaiThu1.Text = dgvDanhSachThu.CurrentRow.Cells[2].Value.ToString();

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
            cbbKieuSinh1.Text = dgvDanhSachThu.CurrentRow.Cells[7].Value.ToString();

            cbbGioiTinh.Text = dgvDanhSachThu.CurrentRow.Cells[8].Value.ToString();
            //Xu li binding to datetimepicker ngay vao
            dtpNgayVao.Text = Convert.ToDateTime(dgvDanhSachThu.CurrentRow.Cells[9].Value.ToString()).ToString();

            cbbNguonGoc1.Text = dgvDanhSachThu.CurrentRow.Cells[10].Value.ToString();
            txtDacDiem.Text = dgvDanhSachThu.CurrentRow.Cells[11].Value.ToString();
            //Xu li binding to datetimepicker ngay sinh
            dtpNgaySinh.Text = Convert.ToDateTime(dgvDanhSachThu.CurrentRow.Cells[12].Value.ToString()).ToString();

            txtTuoiTho.Text = dgvDanhSachThu.CurrentRow.Cells[13].Value.ToString();

            //binding picture
            string pictureURL = "C:\\Users\\ADMIN\\OneDrive\\Documents\\GitHub\\" + dgvDanhSachThu.CurrentRow.Cells[14].Value.ToString();
            ptbThu.ImageLocation = pictureURL;

            txtAnh.Text = pictureURL;
        }
        private bool ValidateDataNull_Thu()
        {
            try
            {
                if (txtMaThu.Text.Equals(""))
                {
                    MessageBox.Show(("Bạn cần phải nhập mã thú"));
                    txtMaThu.Focus();
                    return false;
                }
                if (txtTenThu.Text.Equals(""))
                {
                    MessageBox.Show(("Bạn cần phải nhập tên thú"));
                    txtTenThu.Focus();
                    return false;
                }
                if (cbbLoaiThu1.Text.Equals(""))
                {
                    MessageBox.Show(("Bạn cần phải chọn loài thú"));
                    cbbLoaiThu1.Focus();
                    return false;
                }
                if (txtSoLuong.Text.Equals(""))
                {
                    MessageBox.Show(("Bạn cần phải nhập số lượng thú"));
                    txtSoLuong.Focus();
                    return false;
                }
                if (cbbKieuSinh1.Text.Equals(""))
                {
                    MessageBox.Show(("Bạn cần phải nhập kiểu sinh thú"));
                    cbbGioiTinh.Focus();
                    return false;
                }
                if (cbbNguonGoc1.Text.Equals(""))
                {
                    MessageBox.Show(("Bạn cần phải nhập nguồn gốc thú"));
                    cbbGioiTinh.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return true;
        }

        private bool ValidateDuplicateKey_Thu()
        {
            try
            {
                DataTable dataTable = processDatabase.docBang("Select MaThu from Thu where" + " MaThu='" + txtMaThu.Text + "'");
                if (dataTable.Rows.Count > 0)
                {
                    MessageBox.Show("Mã thú này đã tồn tại, bạn hãy nhập mã khác!");
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

        private bool ValidateNotExistKey_Thu()
        {
            try
            {
                DataTable dataTable = processDatabase.docBang("Select MaThu from Thu where" + " MaThu='" + txtMaThu.Text + "'");
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Mã thú này không tồn tại, bạn hãy nhập mã khác!");
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
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateDataNull_Thu() && ValidateDuplicateKey_Thu())
                {
                    Thu t = new Thu();
                    t = GetThu();
                    string query = "insert into Thu(mathu, TenThu, MaLoai, SoLuong, SachDo, TenKhoaHoc, TenTA, MaKieuSinh, " +
                        "GioiTinh, NgayVao, MaNguonGoc, DacDiem, NgaySinh, TuoiTho, Anh) " +
                        "values(N'" + t.MaThu + "', N'" + t.TenThu + "',N'" + t.MaLoai + "', '" + t.SoLuong + "','" + t.SachDo + "', " +
                        "N'" + t.TenKH + "',N'" + t.TenTA + "', N'" + t.MaKS + "',N'" + t.GioiTinh + "', '" + t.NgayVao + "',N'" + t.MaNG + "', " +
                        "N'" + t.DacDiem + "', '" + t.NgaySinh + "','" + t.TuoiTho + "', N'" + t.Anh + "')";
                    processDatabase.thucThiSQL(query);
                    GetListAnimal();
                    MessageBox.Show("Thêm mới thành công!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        //khoa khong cho nhap chu ở ô số lượng
        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        //khoa khong cho nhap chu ở ô tuổi thọ
        private void txtTuoiTho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string id = dgvDanhSachThu.CurrentRow.Cells[0].Value.ToString();
                if (ValidateDataNull_Thu() && ValidateNotExistKey_Thu() && MessageBox.Show("Ban co muon sua khong ?", "Thong bao", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                {

                    Thu t = new Thu();
                    t = GetThu();
                    string query = "update Thu " +
                        "set TenThu = N'" + t.TenThu + "'," +
                        "MaLoai = N'" + t.MaLoai + "'," +
                        "SoLuong = N'" + t.SoLuong + "'," +
                        "SachDo = N'" + t.SachDo + "'," +
                        "TenKhoaHoc = N'" + t.TenKH + "'," +
                        "TenTA = N'" + t.TenTA + "'," +
                        "MaKieuSinh = N'" + t.MaKS + "'," +
                        "GioiTinh = N'" + t.GioiTinh + "'," +
                        "NgayVao = N'" + t.NgayVao + "'," +
                        "MaNguonGoc = N'" + t.MaNG + "'," +
                        "DacDiem = N'" + t.DacDiem + "'," +
                        "NgaySinh = N'" + t.NgaySinh + "'," +
                        "TuoiTho = N'" + t.TuoiTho + "'," +
                        "Anh = N'" + t.Anh + "'" +
                        "where mathu = N'" + id + "'";
                    processDatabase.thucThiSQL(query);
                    GetListAnimal();
                    MessageBox.Show("Sửa thông tin thành công!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string id = dgvDanhSachThu.CurrentRow.Cells[0].Value.ToString();
                if (ValidateNotExistKey_Thu() && MessageBox.Show("Ban co muon xóa khong ?", "Thong bao", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                {

                    Thu t = new Thu();
                    t = GetThu();
                    string query = "delete from Thu where mathu = N'" + id + "'";
                    processDatabase.thucThiSQL(query);
                    GetListAnimal();
                    MessageBox.Show("Xóa thông tin thành công!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {

        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {

        }
    }
}
