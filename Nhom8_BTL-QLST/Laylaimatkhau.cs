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
    public partial class Laylaimatkhau : Form
    {
        public Laylaimatkhau()
        {
            InitializeComponent();
        }
        ProcessDatabase db = new ProcessDatabase();
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtemail.Text.ToString().Trim() == "")
            {
                label1.Text = "Email không được để trống !!";
            }
            else if (txttaikhoan.Text.ToString().Trim() == "")
            {
                label2.Text = "Tên tài khoản không được để trống !!";
            }
            else
            {
                db.ketNoi();
                if(db.docBang("select * from Taikhoan where username ='"+txttaikhoan.Text.ToString().Trim()+"' and email = '"+txtemail.Text.ToString().Trim()+"'").Rows.Count == 0)
                {
                    label4.Text = "Tài khoản không tồn tại !!";
                }
                else
                {
                    foreach(DataRow row in db.docBang("select * from Taikhoan where username ='" + txttaikhoan.Text.ToString().Trim() + "' and email = '" + txtemail.Text.ToString().Trim() + "'").Rows)
                    {
                        label4.Text = "Mật khẩu của bạn là :" + row["pass"].ToString();
                    }
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát ??", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap t = new DangNhap();
            t.ShowDialog();
            this.Close();
        }

        private void Laylaimatkhau_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtemail;
        }
    }
}
