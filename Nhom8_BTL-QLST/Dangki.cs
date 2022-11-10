using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom8_BTL_QLST
{
    public partial class Dangki : Form
    {
        public Dangki()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        ProcessDatabase database = new ProcessDatabase();
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát ??", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (pictureBox5.Image != pictureBox5.ErrorImage)
            {
                Image t = pictureBox5.Image;
                pictureBox5.Image = pictureBox5.ErrorImage;
                pictureBox5.ErrorImage = t;
            }
            if (txtpassword.UseSystemPasswordChar == true)
            {
                txtpassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtpassword.UseSystemPasswordChar = true;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap dangNhap = new DangNhap();
            dangNhap.ShowDialog();
            this.Close();
        }

        private bool check()
        {
            bool kt = true;
            string pattern = @"[^@]{2,64}@[^.]{2,253}\.[0-9a-z-.]{2,63}";
            string regusername = @"^[a-z0-9_-]{6,15}$";
            if (txtusername.Text.ToString() == "")
            {
                lbtaikhoan.Text = "Tên tài khoản không được để trống !!";
                return false;
            }

            if (txtusername.Text.ToString().Trim().Length < 6 || txtusername.Text.ToString().Trim().Length > 15)
            {
                lbtaikhoan.Text = "Tên tài khoản chứa từ 6 đến 15 kí tự";
                return false;
            }
            if (Regex.IsMatch(txtusername.Text.ToString().Trim(),regusername)==false)
            {
                lbtaikhoan.Text = "Tên tài khoản chỉ chứa chữ cái, số và kí tự '_'";
                return false;
            }
            database.ketNoi();
            if(database.docBang("select * from TaiKhoan where username = '" + txtusername.Text.Trim()+"'").Rows.Count > 0)
            {
                lbtaikhoan.Text = "Tên tài khoản đã tồn tại";
                database.dongKetNoi();
                return false;
            }
            if(txtpassword.Text.ToString() == "")
            {
                lbpassword.Text = "Mật khẩu không được để trống !!";
                return false;
            }
            if(txtpassword.Text.ToString().Length <8 || txtpassword.Text.ToString().Trim().Length > 15)
            {
                lbpassword.Text = "Mật khẩu phải chứa từ 8 đến 15 kí tự";
            }
            if (txtemail.Text.ToString() == "")
            {
                lbemail.Text = "Email không được để trống !!";
                return false;
            }
            Regex ch = new Regex(pattern);
            if (ch.IsMatch(txtemail.Text.ToString().Trim())==false)
            {
                lbemail.Text = "Email phải đúng định dạng";
                return false;
            }
            return kt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (check())
            {
                database.ketNoi();
                database.thucThiSQL("insert into Taikhoan values('" + txtusername.Text.ToString().Trim() + "','" + txtpassword.Text.ToString().Trim() + "','" + txtemail.Text.ToString().Trim() + "')");
                database.dongKetNoi();
                MessageBox.Show("Bạn đã đăng ký tài khoản thành công !!", "Chúc mừng", MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
                txtusername.Text = "";
                txtpassword.Text = "";
                txtemail.Text = "";
            }
        }

        private void txtusername_MouseClick(object sender, MouseEventArgs e)
        {
            lbtaikhoan.Text = "";
        }

        private void txtpassword_MouseClick(object sender, MouseEventArgs e)
        {
            lbpassword.Text = "";
        }

        private void txtemail_MouseClick(object sender, MouseEventArgs e)
        {
            lbemail.Text = "";
        }

        private void Dangki_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtusername;
        }
    }
}
