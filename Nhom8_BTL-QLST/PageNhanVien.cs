using Nhom8_BTL_QLST.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Nhom8_BTL_QLST
{
    public partial class PageNhanVien : UserControl
    {
        ProcessDatabase processDatabase = new ProcessDatabase();
        public PageNhanVien()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = dgvNV.CurrentRow.Cells[0].Value.ToString();
            try
            {
                if (ValidateDataNull_NV() && ValidateNotExistKey_NV() && MessageBox.Show("Ban co muon sua khong ?", "Thong bao", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                {
                    NhanVien nv = new NhanVien();
                    nv = GetEmployee();
                    //insert to Thu
                    string queryToThu = "update NhanVien set tennhanvien =  N'" + nv.TenNhanVien + "'," +
                        "ngaysinh = N'" + nv.NgaySinh + "',"  +
                        "diachi = N'" + nv.DiaChi + "'," +
                        "dienthoai = N'" + nv.SoDienThoai + "'," +
                        "gioitinh = '" + nv.GioiTinh + "' where manhanvien = N'" + id + "'";
                    processDatabase.thucThiSQL(queryToThu);

                    GetListEmployee();
                    MessageBox.Show("Sửa thông tin thành công!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void GetListEmployee()
        {
            //Hien thi danh sach thu len datagridview
            DataTable dataTable = new DataTable();
            dataTable = processDatabase.docBang("select * from View_NhanVien");
            dgvNV.DataSource = dataTable;
        }
        private void FillDataToCombobox()
        {
            DataTable dataTable = new DataTable();
            //Fill dữ liệu từ DB vào combobox Mã Loài
            dataTable = processDatabase.docBang("Select MaChuong from Chuong");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbMaChuong.Items.Add(dataTable.Rows[i][0].ToString());
            }
        }
        private NhanVien GetEmployee()
        {
            NhanVien nhanVien = new NhanVien();
            nhanVien.MaNhanVien = txtMaNhanVien.Text;
            nhanVien.TenNhanVien = txtTenNhanVien.Text;
            nhanVien.SoDienThoai = txtSoDienThoai.Text;
            nhanVien.NgaySinh = Convert.ToDateTime(dtpNgaySinh.Value.ToString());
            if (rdbNam.Checked == true)
            {
                nhanVien.GioiTinh = 1; //nam
            }
            else if (rdbNu.Checked == true)
            {
                nhanVien.GioiTinh = 0; //nu
            }
            else {
                nhanVien.GioiTinh = 2;//khac
            }
            nhanVien.DiaChi = txtDiaChi.Text;  
            return nhanVien;
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //Lấy danh sách nhân viên hiển thị lên datagridview
            GetListEmployee();
            //Fill dữ liệu từ DB vào các combobox
            FillDataToCombobox();
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNhanVien.Text = dgvNV.CurrentRow.Cells[0].Value.ToString();
            txtTenNhanVien.Text = dgvNV.CurrentRow.Cells[1].Value.ToString();
            txtSoDienThoai.Text = dgvNV.CurrentRow.Cells[2].Value.ToString();
            dtpNgaySinh.Text = Convert.ToDateTime(dgvNV.CurrentRow.Cells[3].Value.ToString()).ToString();
            if (dgvNV.CurrentRow.Cells[4].Value.ToString() == "1")
            {
                rdbNam.Checked = true;
                rdbNu.Checked = false;
                rdbKhac.Checked = false;
            }
            else if (dgvNV.CurrentRow.Cells[4].Value.ToString() == "0")
            {
                rdbNam.Checked = false;
                rdbNu.Checked = true;
                rdbKhac.Checked = false;
            }
            else {
                rdbNam.Checked = false;
                rdbNu.Checked = false;
                rdbKhac.Checked = true;
            }
            txtDiaChi.Text = dgvNV.CurrentRow.Cells[5].Value.ToString();
            //cbbMaChuong.Text = dgvNV.CurrentRow.Cells[6].Value.ToString();
        }
        private bool ValidateDataNull_NV()
        {
            return true;
        }
        private bool ValidateDuplicateKey_NV()
        {
            return true;
        }
        private bool ValidateNotExistKey_NV()
        {
            try
            {
                DataTable dataTable = processDatabase.docBang("Select manhanvien from nhanvien where" + " manhanvien='" + txtMaNhanVien.Text + "'");
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Mã nhân viên này không tồn tại, bạn hãy nhập mã khác!");
                    txtMaNhanVien.Focus();
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
                if (ValidateDataNull_NV() && ValidateDuplicateKey_NV())
                {
                    NhanVien nv = new NhanVien();
                    nv = GetEmployee();
                    //insert to Thu
                    string queryToThu = "insert into NhanVien(manhanvien, tennhanvien, ngaysinh, diachi, dienthoai, gioitinh) " +
                        "values(N'" + nv.MaNhanVien + "', N'" + nv.TenNhanVien + "',N'" + nv.NgaySinh+ "', '" + nv.DiaChi + "','" + nv.SoDienThoai + "', " +
                        "N'" + nv.GioiTinh + "')";
                    processDatabase.thucThiSQL(queryToThu);

                    GetListEmployee();
                    MessageBox.Show("Thêm mới thành công!");
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
                exSheet.get_Range("B3:G4").Font.Bold = true;
                exSheet.get_Range("F3").Value = "Danh sách nhân viên";
                exSheet.get_Range("A4").Value = "STT";
                exSheet.get_Range("B4").Value = "Mã nhân viên";
                exSheet.get_Range("C4").Value = "Tên nhân viên";
                exSheet.get_Range("D4").Value = "Số điện thoại";
                exSheet.get_Range("E4").Value = "Ngày sinh";
                exSheet.get_Range("F4").Value = "Giới tính";
                exSheet.get_Range("G4").Value = "Địa chỉ";


                int n = dgvNV.Rows.Count;
                for (int i = 0; i < n; i++)
                {
                    exSheet.get_Range("A" + (i + 5).ToString()).Value = (i + 1).ToString();
                    exSheet.get_Range("B" + (i + 5).ToString()).Value = dgvNV.Rows[i].Cells[0].Value;
                    exSheet.get_Range("C" + (i + 5).ToString()).Value = dgvNV.Rows[i].Cells[1].Value;
                    exSheet.get_Range("D" + (i + 5).ToString()).Value = dgvNV.Rows[i].Cells[2].Value;
                    exSheet.get_Range("E" + (i + 5).ToString()).Value = dgvNV.Rows[i].Cells[3].Value;
                    exSheet.get_Range("F" + (i + 5).ToString()).Value = dgvNV.Rows[i].Cells[4].Value;
                    exSheet.get_Range("G" + (i + 5).ToString()).Value = dgvNV.Rows[i].Cells[5].Value;
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string id = dgvNV.CurrentRow.Cells[0].Value.ToString();
                if (ValidateNotExistKey_NV() && MessageBox.Show("Ban co muon xóa khong ?", "Thong bao", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                {
                    string query = "delete from NhanVien where manhanvien = N'" + id + "'";
                    processDatabase.thucThiSQL(query);
                    GetListEmployee();
                    MessageBox.Show("Xóa thông tin thành công!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
