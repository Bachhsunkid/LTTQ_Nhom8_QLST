using Nhom8_BTL_QLST.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Nhom8_BTL_QLST
{
    public partial class PageChuong : UserControl
    {
        ProcessDatabase processDatabase = new ProcessDatabase();
        public PageChuong()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private bool ValidateDataNull_Chuong()
        {
            try
            {
                if (txtMaChuong.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập mã chuồng");
                    txtMaChuong.Focus();
                    return false;
                }
                if (cbbLoai.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập mã loài");
                    cbbLoai.Focus();
                    return false;
                }
                if (cbbKhu.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập mã khu");
                    cbbKhu.Focus();
                    return false;
                }
                if (txtDienTich.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập diện tích");
                    txtDienTich.Focus();
                    return false;
                }
                if (txtChieuCao.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập chiều cao");
                    txtChieuCao.Focus();
                    return false;
                }
                if (cbbTrangThai.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập trạng thái");
                    cbbTrangThai.Focus();
                    return false;
                }
                if (cbbNhanVien1.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập nhân viên");
                    cbbNhanVien1.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return true;
        }
        private bool ValidateDuplicateKey_Chuong()
        {
            try
            {
                DataTable dataTable = processDatabase.docBang("Select machuong from chuong where machuong='" + txtMaChuong.Text + "'");
                if (dataTable.Rows.Count > 0)
                {
                    MessageBox.Show("Mã chuồng này đã tồn tại, bạn hãy nhập mã khác!");
                    txtMaChuong.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return true;
        }
        private bool ValidateNotExistKey_Chuong()
        {
            try
            {
                DataTable dataTable = processDatabase.docBang("Select machuong from chuong where machuong='" + txtMaChuong.Text + "'");
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Mã chuồng này không tồn tại, bạn hãy nhập mã khác!");
                    txtMaChuong.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateDataNull_Chuong() && ValidateDuplicateKey_Chuong())
                {
                    Chuong c = new Chuong();
                    c = GetCage();
                    //insert to chuong
                    string queryToChuong = "insert into Chuong(machuong, maloai, makhu, dientich, chieucao, SoLuongThu, matrangthai, manhanvien, ghichu) " +
                        "values(N'" + c.MaChuong + "', N'" + c.MaLoai + "',N'" + c.MaKhu + "', '" +c.DienTich + "','" + c.ChieuCao + "', " +
                        "N'" + 0 + "',N'" + c.MaTrangThai + "', N'" + c.MaNhanVien + "',N'" + c.GhiChu + "')";
                    processDatabase.thucThiSQL(queryToChuong);
                    GetListCage();
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

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtChieuCao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtDienTich_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void GetListCage()
        {
            //Hien thi danh sach thu len datagridview
            DataTable dataTable = new DataTable();
            dataTable = processDatabase.docBang("select * from View_Chuong_DanhSachChuong");
            dgvChuong.DataSource = dataTable;
        }
        private Chuong GetCage()
        {
            Chuong chuong = new Chuong();
            chuong.MaChuong = txtMaChuong.Text;
            chuong.MaLoai = processDatabase.docBang("Select MaLoai from Loai where TenLoai = N'" + cbbLoai.Text + "' ").Rows[0][0].ToString();
            chuong.MaKhu = processDatabase.docBang("Select MaKhu from Khu where TenKhu = N'" + cbbKhu.Text + "' ").Rows[0][0].ToString();
            chuong.DienTich = int.Parse(txtDienTich.Text);
            chuong.ChieuCao = int.Parse(txtChieuCao.Text);
            chuong.MaTrangThai = processDatabase.docBang("Select MaTrangThai from Trangthai where tentrangthai = N'" + cbbTrangThai.Text + "' ").Rows[0][0].ToString();
            chuong.MaNhanVien = processDatabase.docBang("Select Manhanvien from nhanvien where tennhanvien = N'" + cbbNhanVien1.Text + "' ").Rows[0][0].ToString();
            chuong.GhiChu = txtGhiChu.Text;
            return chuong;
        }
        private void FillDataToCombobox()
        {
            DataTable dataTable = new DataTable();
            //Fill dữ liệu từ DB vào combobox Mã Loài
            dataTable = processDatabase.docBang("Select distinct TenLoai from Loai");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbLoai.Items.Add(dataTable.Rows[i][0].ToString());
            }
            dataTable = processDatabase.docBang("Select distinct TenKhu from Khu");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbKhu.Items.Add(dataTable.Rows[i][0].ToString());
            }
            dataTable = processDatabase.docBang("Select distinct TenTrangThai from Trangthai");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbTrangThai.Items.Add(dataTable.Rows[i][0].ToString());
            }
            dataTable = processDatabase.docBang("Select distinct TenNhanVien from NhanVien");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbNhanVien1.Items.Add(dataTable.Rows[i][0].ToString());
                cbbNhanVien2.Items.Add(dataTable.Rows[i][0].ToString());
            }
            dataTable = processDatabase.docBang("Select distinct MaThu from Thu");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbThu.Items.Add(dataTable.Rows[i][0].ToString());
            }
        }
        private void PageChuong_Load(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //Lấy danh sách thú hiển thị lên datagridview
            GetListCage();
            //Fill dữ liệu từ DB vào các combobox
            FillDataToCombobox();

            txtMaChuong.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string id = dgvChuong.CurrentRow.Cells[0].Value.ToString();
                if (ValidateDataNull_Chuong() && ValidateNotExistKey_Chuong() && MessageBox.Show("Ban co muon sua khong ?", "Thong bao", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                {
                    Chuong c = new Chuong();
                    c = GetCage();
                    //insert to chuong
                    string queryToChuong = "update Chuong set maloai = N'" + c.MaLoai + "', " +
                        "makhu = N'" + c.MaKhu + "', " +
                        "dientich = '" + c.DienTich + "', " +
                        "chieucao = '" + c.ChieuCao + "', " +
                        "matrangthai = N'" + c.MaTrangThai + "', " +
                        "manhanvien = N'" + c.MaNhanVien + "', " +
                        "ghichu = N'" + c.GhiChu + "'" +
                        " where machuong = N'" + id + "'";
                    processDatabase.thucThiSQL(queryToChuong);
                    GetListCage();
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
                string id = dgvChuong.CurrentRow.Cells[0].Value.ToString();
                if (ValidateNotExistKey_Chuong() && MessageBox.Show("Ban co muon xóa khong ?", "Thong bao", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                {
                    string queryToThu = "delete from Chuong where maChuong = N'" + id + "'";
                    processDatabase.thucThiSQL(queryToThu);
                    GetListCage();
                    MessageBox.Show("Xóa thông tin thành công!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void dgvChuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaChuong.Text = dgvChuong.CurrentRow.Cells[0].Value.ToString();
            cbbLoai.Text = dgvChuong.CurrentRow.Cells[1].Value.ToString();
            cbbKhu.Text = dgvChuong.CurrentRow.Cells[2].Value.ToString();
            txtDienTich.Text = dgvChuong.CurrentRow.Cells[3].Value.ToString();
            txtChieuCao.Text = dgvChuong.CurrentRow.Cells[4].Value.ToString();
            cbbTrangThai.Text = dgvChuong.CurrentRow.Cells[6].Value.ToString();
            cbbNhanVien1.Text = dgvChuong.CurrentRow.Cells[7].Value.ToString();
            txtGhiChu.Text = dgvChuong.CurrentRow.Cells[8].Value.ToString();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                string maThu = cbbThu.Text;
                string tenNhanVien = cbbNhanVien2.Text;
                string soLuong = txtSoLuong.Text;

                string query = "exec Proc_Chuong_filter N'" + maThu + "',N'" + tenNhanVien + "', '" + soLuong + "'";

                DataTable dataTable = new DataTable();
                dataTable = processDatabase.docBang(query);
                
                dgvChuong.DataSource = dataTable;
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Không có thông tin bạn đang tìm kiếm");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {

                Excel.Application exApp = new Excel.Application();

                Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1]; //thao tác với worksheet trang đầu tiên
                                                                                 //Excel.Range tenTruong = (Excel.Range)exSheet.Cells[1, 1];
                exSheet.get_Range("B3:P4").Font.Bold = true;
                exSheet.get_Range("F3").Value = "Danh sách chuồng";
                exSheet.get_Range("A4").Value = "STT";
                exSheet.get_Range("B4").Value = "Mã Chuồng";
                exSheet.get_Range("C4").Value = "Tên loài";
                exSheet.get_Range("D4").Value = "Tên khu";
                exSheet.get_Range("E4").Value = "Diện tích";
                exSheet.get_Range("F4").Value = "Chiều cao";
                exSheet.get_Range("G4").Value = "Số lượng thú";
                exSheet.get_Range("H4").Value = "Trạng thái";
                exSheet.get_Range("I4").Value = "Nhân viên";
                exSheet.get_Range("J4").Value = "Ghi chú";


                int n = dgvChuong.Rows.Count;
                for (int i = 0; i < n; i++)
                {
                    exSheet.get_Range("A" + (i + 5).ToString()).Value = (i + 1).ToString();
                    exSheet.get_Range("B" + (i + 5).ToString()).Value = dgvChuong.Rows[i].Cells[0].Value;
                    exSheet.get_Range("C" + (i + 5).ToString()).Value = dgvChuong.Rows[i].Cells[1].Value;
                    exSheet.get_Range("D" + (i + 5).ToString()).Value = dgvChuong.Rows[i].Cells[2].Value;
                    exSheet.get_Range("E" + (i + 5).ToString()).Value = dgvChuong.Rows[i].Cells[3].Value;
                    exSheet.get_Range("F" + (i + 5).ToString()).Value = dgvChuong.Rows[i].Cells[4].Value;
                    exSheet.get_Range("G" + (i + 5).ToString()).Value = dgvChuong.Rows[i].Cells[5].Value;
                    exSheet.get_Range("H" + (i + 5).ToString()).Value = dgvChuong.Rows[i].Cells[6].Value;
                    exSheet.get_Range("I" + (i + 5).ToString()).Value = dgvChuong.Rows[i].Cells[7].Value;
                    exSheet.get_Range("J" + (i + 5).ToString()).Value = dgvChuong.Rows[i].Cells[8].Value;
                    
                }
                //auto fit columns
                foreach (Excel.Worksheet ws in exBook.Worksheets)
                {
                    Excel.Range range = ws.UsedRange;
                    range.Columns.AutoFit();
                }

                exBook.Activate();
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.ShowDialog();
                exBook.SaveAs(saveFileDialog.FileName.ToString());
                exApp.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
