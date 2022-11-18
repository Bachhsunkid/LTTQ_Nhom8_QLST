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

        }
    }
}
