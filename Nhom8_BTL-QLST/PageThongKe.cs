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
    public partial class PageThongKe : UserControl
    {
        public PageThongKe()
        {
            InitializeComponent();
        }
        ProcessDatabase db = new ProcessDatabase();
        private void button1_Click(object sender, EventArgs e)
        {
            db.ketNoi();
            if(comboBox1.SelectedIndex >= 0 )
            {
                if(comboBox2.SelectedIndex == 0)
                {
                    dataGridView1.DataSource = db.docBang("select * from Timthutheoloai(N'" + comboBox1.SelectedItem.ToString() + "') and Mathu in (select Mathu from SachDo");
                }
                else
                {
                    dataGridView1.DataSource = db.docBang("select * from Timthutheoloai(N'" + comboBox1.SelectedItem.ToString() + "')");
                }
            }
            else if(comboBox2.SelectedIndex == 0)
            {
                dataGridView1.DataSource = db.docBang("select * from SachDo");
            }
            else
            {
                dataGridView1.DataSource = db.docBang("select * from Thu where Mathu not in " + "(select Mathu from SachDo)");
            }
            if(dataGridView1.Rows.Count == 0)
            {
                if(comboBox1.SelectedIndex >= 0 )
                {
                    if(comboBox2.SelectedIndex == 0)
                    {
                        MessageBox.Show("Không có thú loài " + comboBox1.Text + " thuộc danh sách đỏ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        MessageBox.Show("Không có thú loài " + comboBox1.Text, "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
                    }
                    comboBox1.SelectedIndex = -1;
                    comboBox2.SelectedIndex = -1;
                    resetDS();
                }
            }
            db.dongKetNoi();
        }
        private void resetDS()
        {
            dataGridView1.DataSource = db.docBang("Select * from Thu");
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            db.ketNoi();
            resetDS();
            db.dongKetNoi();
            addCombobox();
        }

        private void addCombobox()
        {
            db.ketNoi();
            foreach(DataRow row in db.docBang("select TenLoai from Loai").Rows)
            {
                comboBox1.Items.Add(row["TenLoai"]);
            }
            db.dongKetNoi();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            db.ketNoi();
            dataGridView1.DataSource = db.docBang("select * from ThuBiOm");
            if(dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Không có thú nào bị ốm", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                resetDS();
            }
            db.dongKetNoi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            txtMathu.Text = "";
            db.ketNoi();
            resetDS();
            db.dongKetNoi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.ketNoi();
            if (txtMathu.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập thông tin Mã thú !!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                txtMathu.Focus();
            }
            else if (db.docBang("select * from Thu where Mathu = N'" + txtMathu.Text.ToString().Trim() + "'").Rows.Count == 0)
            {
                MessageBox.Show("Không có mã thú này trong danh sách thú","Thông báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
                txtMathu.Text = "";
                txtMathu.Focus();resetDS();
            }
            else
            {
                dataGridView1.DataSource = db.docBang("select * from TimThu(N'" + txtMathu.Text.ToString().Trim() + "')");
            }
            db.dongKetNoi();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (txtMathu1.Text.ToString().Trim() == "")
            {
                
            }
        }
    }
};