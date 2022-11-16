using Microsoft.Office.Interop.Excel;
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
using DataTable = System.Data.DataTable;
using TextBox = System.Windows.Forms.TextBox;

namespace Nhom8_BTL_QLST
{
    public partial class PageNhapLieu : UserControl
    {
        ProcessDatabase processDatabase = new ProcessDatabase();
        public PageNhapLieu()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            DataTable dataTable = new DataTable();
            //Fill dữ liệu từ DB vào combobox Mã Loài
            dataTable = processDatabase.docBang("Select mathucan from thucan");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                cbbMaTA.Items.Add(dataTable.Rows[i][0].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Hien thi danh sach thu len datagridview
                DataTable dataTable = new DataTable();
                if (comboBox1.SelectedIndex == 0)
                {
                    dataTable = processDatabase.docBang("select * from loai");
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    dataTable = processDatabase.docBang("select * from nguongoc");
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    dataTable = processDatabase.docBang("select * from que");
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    dataTable = processDatabase.docBang("select * from thucan");
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    dataTable = processDatabase.docBang("select * from thucan_gia");
                }
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private bool ValidateNotExistKey(string key, string table, TextBox textBox)
        {
            try
            {
                DataTable dataTable = processDatabase.docBang("Select "+ key + " from "+ table + " where "+ key + " = '" + textBox.Text + "'");
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Mã này không tồn tại, bạn hãy nhập mã khác!");
                    textBox.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return true;
        }
        private bool ValidateDuplicateKey(string key, string table, TextBox textBox)
        {
            try
            {
                DataTable dataTable = processDatabase.docBang("Select "+ key + " from "+ table + " where "+ key + "='" + textBox.Text + "'");
                if (dataTable.Rows.Count > 0)
                {
                    MessageBox.Show("Mã này đã tồn tại, bạn hãy nhập mã khác!");
                    textBox.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return true;
        }
        private bool ValidateDataNull_Loai()
        {
            try
            {
                if (txtMaLoai.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập mã loài");
                    txtMaLoai.Focus();
                    return false;
                }
                if (txtTenLoai.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập tên loài");
                    txtMaLoai.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return true;
        }
        private bool ValidateDataNull_NG()
        {
            try
            {
                if (txtMaNG.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập mã nguồn gốc");
                    txtMaNG.Focus();
                    return false;
                }
                if (txtTenNG.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập tên nguồn gốc");
                    txtTenNG.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return true;
        }
        private bool ValidateDataNull_Que()
        {
            try
            {
                if (txtMaQue.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập mã quê");
                    txtMaQue.Focus();
                    return false;
                }
                if (txtTenQue.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập tên quê");
                    txtTenQue.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return true;
        }
        private bool ValidateDataNull_TA()
        {
            try
            {
                if (txtMaTA.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập mã thức ăn");
                    txtMaTA.Focus();
                    return false;
                }
                if (txtTenTA.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập tên thức ăn");
                    txtTenTA.Focus();
                    return false;
                }
                if (txtDonVi.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập đơn vị");
                    txtDonVi.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return true;
        }
        private bool ValidateDataNull_GTA()
        {
            try
            {
                if (txtMaGia.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập mã giá");
                    txtMaGia.Focus();
                    return false;
                }
                if (cbbMaTA.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập mã thức ăn");
                    txtTenTA.Focus();
                    return false;
                }
                if (txtDonGia.Text.Equals(""))
                {
                    MessageBox.Show("Bạn cần phải nhập đơn giá");
                    txtDonGia.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckChooseCombobox())
                {
                    MessageBox.Show("Bạn cần phải chọn danh mục!");
                    return;
                }

                DataTable dataTable = new DataTable();
                if (comboBox1.SelectedIndex == 0)
                {
                    try
                    {
                        if (ValidateDataNull_Loai() && ValidateDuplicateKey("maloai", "loai", txtMaLoai))
                        {
                            //insert to chuong
                            string query = "insert into Loai values(N'" + txtMaLoai.Text + "', N'" + txtTenLoai.Text + "',N'" + txtGhiChu1.Text + "')";
                            processDatabase.thucThiSQL(query);
                            dataTable = processDatabase.docBang("select * from loai");
                            MessageBox.Show("Thêm mới thành công!");
                            RefreshLoai();
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    try
                    {
                        if (ValidateDataNull_NG() && ValidateDuplicateKey("manguongoc", "nguongoc", txtMaNG))
                        {
                            //insert to chuong
                            string query = "insert into nguongoc values(N'" + txtMaNG.Text + "', N'" + txtTenNG.Text + "',N'" + txtGhiChu2.Text + "')";
                            processDatabase.thucThiSQL(query);
                            dataTable = processDatabase.docBang("select * from nguongoc");
                            MessageBox.Show("Thêm mới thành công!");
                            RefreshNG();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    try
                    {
                        if (ValidateDataNull_Que() && ValidateDuplicateKey("maque", "que", txtMaQue))
                        {
                            //insert to chuong
                            string query = "insert into que values(N'" + txtMaQue.Text + "', N'" + txtTenQue.Text + "')";
                            processDatabase.thucThiSQL(query);
                            dataTable = processDatabase.docBang("select * from que");
                            MessageBox.Show("Thêm mới thành công!");
                            RefreshQue();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    try
                    {
                        if (ValidateDataNull_TA() && ValidateDuplicateKey("mathucan", "thucan", txtMaTA))
                        {
                            //insert to chuong
                            string query = "insert into thucan values(N'" + txtMaTA.Text + "', N'" + txtTenTA.Text + ", 'N'" + txtCongDung.Text + "', N'" + txtDonVi.Text + "')";
                            processDatabase.thucThiSQL(query);
                            dataTable = processDatabase.docBang("select * from thucan");
                            MessageBox.Show("Thêm mới thành công!");
                            RefreshTA();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    try
                    {
                        if (ValidateDataNull_GTA() && ValidateDuplicateKey("magia", "thucan_gia", txtMaGia))
                        {
                            //insert to chuong
                            string query = "insert into thucan_gia values(N'" + txtMaGia.Text + "', N'" + cbbMaTA.Text + "',N'" + txtDonGia.Text + "', N'" + Convert.ToDateTime(dtpNgayApDung.Value.ToString()) + "')";
                            processDatabase.thucThiSQL(query);
                            dataTable = processDatabase.docBang("select * from thucan_gia");
                            MessageBox.Show("Thêm mới thành công!");
                            RefreshGTA();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = new DataTable();
                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                if (comboBox1.SelectedIndex == 0)
                {
                    if (ValidateDataNull_Loai() && ValidateNotExistKey("maloai", "loai", txtMaLoai) && MessageBox.Show("ban co muon sua khong ?", "thong bao", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                    {
                        //insert to loai
                        string query = "update loai set tenloai = N'" + txtTenLoai.Text + "', " +
                            "ghichu = N'" + txtGhiChu1.Text + "'" +
                            " where maloai = N'" + id + "'";
                        processDatabase.thucThiSQL(query);
                        dataTable = processDatabase.docBang("Select * from loai");
                        MessageBox.Show("sửa thông tin thành công!");
                        RefreshLoai();
                    }
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    if (ValidateDataNull_NG() && ValidateNotExistKey("manguongoc", "nguongoc", txtMaNG) && MessageBox.Show("ban co muon sua khong ?", "thong bao", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                    {
                        //insert to nguongoc
                        string query = "update nguongoc set tennguongoc = N'" + txtTenNG.Text + "', " +
                            "ghichu = N'" + txtGhiChu2.Text + "'" +
                            " where manguongoc = N'" + id + "'";
                        processDatabase.thucThiSQL(query);
                        dataTable = processDatabase.docBang("Select * from NguonGoc");
                        MessageBox.Show("sửa thông tin thành công!");
                        RefreshNG();
                    }
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    if (ValidateDataNull_Que() && ValidateNotExistKey("maque", "que", txtMaQue) && MessageBox.Show("ban co muon sua khong ?", "thong bao", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                    {
                        //insert to loai
                        string query = "update que set tenque = '" + txtTenNG.Text + "', " +
                            " where maque = N'" + id + "'";
                        processDatabase.thucThiSQL(query);
                        dataTable = processDatabase.docBang("Select * from que");
                        MessageBox.Show("sửa thông tin thành công!");
                        RefreshQue();
                    }
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    if (ValidateDataNull_TA() && ValidateNotExistKey("mathucan", "thucan", txtMaTA) && MessageBox.Show("ban co muon sua khong ?", "thong bao", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                    {
                        //insert to loai
                        string query = "update thucan set tenthucan = N'" + txtTenTA.Text + "', " +
                            "congdung = N'" + txtCongDung.Text + "'" +
                            "madonvi = N'" + txtDonVi.Text + "'" +
                            " where mathucan = N'" + id + "'";
                        processDatabase.thucThiSQL(query);
                        dataTable = processDatabase.docBang("Select * from thucan");
                        MessageBox.Show("sửa thông tin thành công!");
                        RefreshTA();
                    }
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    if (ValidateDataNull_GTA() && ValidateNotExistKey("magia", "thucan_gia", txtMaGia) && MessageBox.Show("ban co muon sua khong ?", "thong bao", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                    {
                        //insert to loai
                        string query = "update thucan_gia set mathucan = N'" + cbbMaTA.Text + "', " +
                            "dongia = n'" + txtDonGia.Text + "'" +
                            "Thang_NamApDung = N'" + Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value.ToString()).ToString() + "'" +
                            " where magia = N'" + id + "'";
                        processDatabase.thucThiSQL(query);
                        dataTable = processDatabase.docBang("Select * from thucan_gia");
                        MessageBox.Show("sửa thông tin thành công!");
                        RefreshGTA();
                    }
                }
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void DeteleByID(string key, string table, TextBox textBox)
        {
            try
            {
                DataTable dataTable = new DataTable();
                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                if (ValidateNotExistKey(key, table, textBox) && MessageBox.Show("Ban có muốn xóa khong ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes && !id.Equals(""))
                {
                    string query = "delete from " + table + " where " + key + " = N'" + id + "'";
                    processDatabase.thucThiSQL(query);
                    dataTable = processDatabase.docBang("select * from " + table + "");
                    MessageBox.Show("Xóa thông tin thành công!");
                }
                dataGridView1.DataSource = dataTable;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                
                
                if (comboBox1.SelectedIndex == 0)
                {
                    DeteleByID("MaLoai", "Loai", txtMaLoai);
                    RefreshLoai();

                }
                if (comboBox1.SelectedIndex == 1)
                {
                    DeteleByID("MaNguonGoc", "nguongoc", txtMaNG);
                    RefreshNG();
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    DeteleByID("MaQue", "Que", txtMaQue);
                    RefreshQue();
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    DeteleByID("MaThucAn", "ThucAn", txtMaTA);
                    RefreshTA();

                }
                if (comboBox1.SelectedIndex == 4)
                {
                    DeteleByID("MaGia", "ThucAn_Gia",txtMaGia);
                    RefreshGTA();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    txtMaLoai.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtTenLoai.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtGhiChu1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    txtMaNG.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtTenNG.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtGhiChu2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    txtMaQue.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtTenQue.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    txtMaTA.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtTenTA.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtCongDung.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtDonVi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    txtMaGia.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    cbbMaTA.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtDonGia.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    dtpNgayApDung.Text = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value.ToString()).ToString();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void RefreshLoai()
        {
            txtMaLoai.Text = "";
            txtTenLoai.Text = "";
            txtGhiChu1.Text = "";
        }
        private void RefreshNG()
        {
            txtMaNG.Text = "";
            txtTenNG.Text = "";
            txtGhiChu2.Text = "";
        }
        private void RefreshQue()
        {
            txtMaQue.Text = "";
            txtTenQue.Text = "";
        }
        private void RefreshTA()
        {
            txtMaTA.Text = "";
            txtTenTA.Text = "";
            txtCongDung.Text = "";
            txtDonVi.Text = "";
        }
        private void RefreshGTA()
        {
            txtMaGia.Text = "";
            cbbMaTA.SelectedIndex = -1;
            txtDonGia.Text = "";
            dtpNgayApDung.Value = DateTime.Now;
        }
        private bool CheckChooseCombobox()
        {
            if (comboBox1.Text == "")
            {
                return false;
            }
            return true;
        }
    }
}
