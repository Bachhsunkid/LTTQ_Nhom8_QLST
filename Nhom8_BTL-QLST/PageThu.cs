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
    public partial class PageThu : UserControl
    {
        ProcessDatabase processDatabase = new ProcessDatabase();
        public PageThu()
        {
            InitializeComponent();
        }
        //Kiem tra ma thu nhap vao da ton tai hay chua
        public bool validateExistKey()
        {
            try
            {
                string maThu = txtMaThu.Text;

                DataTable dataTable = processDatabase.docBang("Select MaThu from Thu where" + " MaThu='" + maThu + "'");
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Mã khách hàng này không tồn tại, bạn hãy nhập mã khác!");
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
