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
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    cbbMaChuong.Items.Add(dataTable.Rows[i][0].ToString());
            //}
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

            txtMaNhanVien.Focus();
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNhanVien.Text = dgvNV.CurrentRow.Cells[0].Value.ToString();
            txtTenNhanVien.Text = dgvNV.CurrentRow.Cells[1].Value.ToString();
            txtSoDienThoai.Text = dgvNV.CurrentRow.Cells[2].Value.ToString();
            dtpNgaySinh.Text = Convert.ToDateTime(dgvNV.CurrentRow.Cells[3].Value.ToString()).ToString();
            if (dgvNV.CurrentRow.Cells[4].Value.ToString().Equals("Nam"))
            {
                rdbNam.Checked = true;
                rdbNu.Checked = false;
                rdbKhac.Checked = false;
            }
            else if (dgvNV.CurrentRow.Cells[4].Value.ToString().Equals("Nữ"))
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            
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
                exSheet.get_Range("F3").Value = "Danh nhân viên";
                exSheet.get_Range("A4").Value = "STT";
                exSheet.get_Range("B4").Value = "Mã nhân viên";
                exSheet.get_Range("C4").Value = "Tên nhân viên";
                exSheet.get_Range("D4").Value = "Điện thoại";
                exSheet.get_Range("E4").Value = "Ngày sinh";
                exSheet.get_Range("F4").Value = "Giới tính";
                exSheet.get_Range("G4").Value = "Địa chỉ";



                int n = dgvNhanVien.Rows.Count;
                for (int i = 0; i < n; i++)
                {
                    exSheet.get_Range("A" + (i + 5).ToString()).Value = (i + 1).ToString();
                    exSheet.get_Range("B" + (i + 5).ToString()).Value = dgvNhanVien.Rows[i].Cells[0].Value;
                    exSheet.get_Range("C" + (i + 5).ToString()).Value = dgvNhanVien.Rows[i].Cells[1].Value;
                    exSheet.get_Range("D" + (i + 5).ToString()).Value = dgvNhanVien.Rows[i].Cells[2].Value;
                    exSheet.get_Range("E" + (i + 5).ToString()).Value = dgvNhanVien.Rows[i].Cells[3].Value;
                    exSheet.get_Range("F" + (i + 5).ToString()).Value = dgvNhanVien.Rows[i].Cells[4].Value;
                    exSheet.get_Range("G" + (i + 5).ToString()).Value = dgvNhanVien.Rows[i].Cells[5].Value;
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
