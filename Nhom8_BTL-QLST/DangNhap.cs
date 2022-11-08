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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        ProcessDatabase database = new ProcessDatabase("Data Source=DESKTOP-9ATFPMT\\SQLEXPRESS;Initial Catalog=Taikhoan;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn thoát ??","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
              this.Close();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if(pictureBox5.Image!= pictureBox5.ErrorImage)
            {
                Image t = pictureBox5.Image;
                pictureBox5.Image = pictureBox5.ErrorImage;
                pictureBox5.ErrorImage = t;
            }
            if (txtmatkhau.UseSystemPasswordChar == true)
            {
                txtmatkhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtmatkhau.UseSystemPasswordChar = true;
            }
        }
        private bool checktk()
        {
            database.ketNoi();
            bool kt = true;
            if (txttaikhoan.Text.Trim() == "")
            {
                lbtkhoan.Text = "Bạn chưa nhập tài khoản";
                return false;
            }
            else
            {
                lbtkhoan.Text = "";
            }
            if (txtmatkhau.Text.Trim() == "")
            {
                lbmkhau.Text = "Bạn chưa nhập mật khẩu";
                return false;
            }
            else
            {
                lbmkhau.Text = "";
            }
            if (kt == true)
            {
                foreach(DataRow row in database.docBang("select * from Taikhoan where username='"+txttaikhoan.Text.Trim()+"'").Rows){
                    if (row["pass"].ToString().Trim() == txtmatkhau.Text.Trim())
                    {
                        return true;
                    }
                }
                lbmkhau.Text = "Tài khoản hoặc mật khẩu không đúng !!";
                return false;
            }
            return kt;
        }
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (checktk())
            {
                this.Hide();
                TrangChu main = new TrangChu();
                main.ShowDialog();
                this.Close();
            }
        }

        private void txttaikhoan_MouseClick(object sender, MouseEventArgs e)
        {
            lbtkhoan.Text = "";
        }

        private void txtmatkhau_MouseClick(object sender, MouseEventArgs e)
        {
            lbmkhau.Text = "";
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txttaikhoan;
        }

        private void DangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dangki dangki = new Dangki();
            dangki.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Laylaimatkhau t = new Laylaimatkhau();
            t.ShowDialog();
            this.Close();
        }
    }
}
