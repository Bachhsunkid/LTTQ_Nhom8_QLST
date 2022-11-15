using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Nhom8_BTL_QLST
{
    public partial class PageThongKe : UserControl
    {
        public PageThongKe()
        {
            InitializeComponent();
        }
        ProcessDatabase db = new ProcessDatabase();
        int currentsize = 0;
        private void resetDS()
        {
            dataGridView1.DataSource = db.docBang("Select * from Thu");
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
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

        private void PageThongKe_Load(object sender, EventArgs e)
        {
            db.ketNoi();
            resetDS();
            db.dongKetNoi();
            addCombobox();
            currentsize = this.Width;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            db.ketNoi();
            if (comboBox1.SelectedIndex >= 0)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    dataGridView1.DataSource = db.docBang("select * from Timthutheoloai(N'" + comboBox1.SelectedItem.ToString() + "') and Mathu in (select Mathu from SachDo");
                }
                else
                {
                    dataGridView1.DataSource = db.docBang("select * from Timthutheoloai(N'" + comboBox1.SelectedItem.ToString() + "')");
                }
            }
            else if (comboBox2.SelectedIndex == 0)
            {
                dataGridView1.DataSource = db.docBang("select * from SachDo");
            }
            else if(comboBox2.SelectedIndex == 1)
            {
                dataGridView1.DataSource = db.docBang("select * from Thu where Mathu not in " + "(select Mathu from SachDo)");
            }
            else
            {
                MessageBox.Show("Bạn cần chọn thông tin cần lọc", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop);
            }
            if (dataGridView1.Rows.Count == 0)
            {
                if (comboBox1.SelectedIndex >= 0)
                {
                    if (comboBox2.SelectedIndex == 0)
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


        private void button4_Click_1(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            txtMathu.Text = "";
            db.ketNoi();
            resetDS();
            db.dongKetNoi();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            db.ketNoi();
            if (txtMathu.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập thông tin Mã thú !!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                txtMathu.Focus();
            }
            else if (db.docBang("select * from Thu where Mathu = N'" + txtMathu.Text.ToString().Trim() + "'").Rows.Count == 0)
            {
                MessageBox.Show("Không có mã thú này trong danh sách thú", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                txtMathu.Text = "";
                txtMathu.Focus(); resetDS();
            }
            else
            {
                dataGridView1.DataSource = db.docBang("select * from TimThu(N'" + txtMathu.Text.ToString().Trim() + "')");
            }
            db.dongKetNoi();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            db.ketNoi();
            dataGridView1.DataSource = db.docBang("select * from ThuBiOm");
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Không có thú nào bị ốm", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                resetDS();
            }
            db.dongKetNoi();
        }

        private void txtMathu1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PageThongKe_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width > currentsize)
            {
                panel2.Width = this.Width/4;
            }
            else
            {
                panel2.Width = currentsize/4;
            }
            listView1.Columns[0].Width = panel2.Width / 5;
            listView1.Columns[1].Width = panel2.Width / 4;
            listView1.Columns[2].Width = panel2.Width / 4;
            listView1.Columns[3].Width = (int)(panel2.Width / 3.7);
            listView1.Height = (int)(panel2.Height / 2);
            //currentsize = this.Width;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            db.ketNoi();
            if (listView1.Items.Count > 0)
            {
                listView1.Items.Clear();
                chart1.Series[0].Points.Clear();
                chart1.Titles.Clear();
            }
            if (txtMathu1.Text.ToString().Trim() == "")
            {

                DateTime a = dateTimePicker1.Value.Date;
                DateTime b = dateTimePicker2.Value.Date;

                int st;
                foreach (DataRow row in db.docBang("select * from Thu").Rows)
                {
                    st = 0;
                    while (DateTime.Compare(a.Date, b.Date) == -1)
                    {
                        if (db.docBang("select * from CP_MaThu(N'" + row["mathu"].ToString() + "','" + a.Year + "-" + a.Month + "-" + a.Day + "')").Rows.Count > 0)
                        {
                            foreach (DataRow r in db.docBang("select * from CP_MaThu(N'" + row["mathu"].ToString() + "','" + a.Year + "-" + a.Month + "-" + a.Day + "')").Rows)
                            {
                                string[] s = r["Tongtien"].ToString().Split(',');
                                st += int.Parse(s[0]);
                            }
                        }
                        else
                        {
                            st += 0;
                        }
                        a = a.AddDays(1);
                    }
                    a = dateTimePicker1.Value.Date;
                    ListViewItem item = new ListViewItem();
                    item.Text = row["Mathu"].ToString();
                    item.SubItems.Add(a.ToString());
                    item.SubItems.Add(b.ToString());
                    item.SubItems.Add(st.ToString());
                    listView1.Items.Add(item);
                    chart1.Series[0].Points.AddXY(row["mathu"], st);
                }
                chart1.Series[0].Name = "Mã thú";
                chart1.Series[0].ChartType = SeriesChartType.Pie;
                chart1.Titles.Add("Thống kê theo mã thú");
            }
            else if (txtMathu1.Text.ToString().Trim() != "")
            {
                int[] thang = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                DateTime a = dateTimePicker1.Value.Date;
                DateTime b = dateTimePicker2.Value.Date;
                int st = 0,kt=0;
                DateTime curD = a;
                while (a.Date != b.Date)
                {
                    if (db.docBang("select * from CP_MaThu(N'" + txtMathu1.Text.ToString().Trim() + "','" + a.Year + "-" + a.Month + "-" + a.Day + "')").Rows.Count > 0)
                    {
                        foreach (DataRow r in db.docBang("select * from CP_MaThu(N'" + txtMathu1.Text.ToString().Trim() + "','" + a.Year + "-" + a.Month + "-" + a.Day + "')").Rows)
                        {
                            string[] s = r["Tongtien"].ToString().Split(',');
                            st += int.Parse(s[0]);
                        }
                    }
                    else
                    {
                        st += 0;
                    }
                    thang[a.Month] += st;
                    a = a.AddDays(1);
                    if (a.Month != curD.Month)
                    {
                        ListViewItem itemt = new ListViewItem();
                        itemt.Text = txtMathu1.Text.ToString().Trim();
                        itemt.SubItems.Add(curD.ToString());
                        itemt.SubItems.Add(a.AddDays(-1).ToString());
                        itemt.SubItems.Add(thang[a.AddDays(-1).Month].ToString());
                        listView1.Items.Add(itemt);
                        curD = a;kt = 1;
                    }
                }
                //a = dateTimePicker1.Value.Date;
                ListViewItem item = new ListViewItem();
                item.Text = txtMathu1.Text.ToString().Trim();
                item.SubItems.Add(curD.ToString());
                item.SubItems.Add(b.ToString());
                item.SubItems.Add(thang[b.Month].ToString());
                listView1.Items.Add(item);
                
                for (int i = 1; i <= 12; i++)
                {
                    if (thang[i] != 0)
                    {
                        chart1.Series[0].Points.AddXY(i.ToString(), thang[i]);
                    }
                }
                chart1.Series[0].Name = "Tháng";
                chart1.Series[0].ChartType = SeriesChartType.Column;
                chart1.Titles.Add("Thống kê theo tháng");
            }
            db.dongKetNoi();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            txtMathu1.Text = "";
            listView1.Items.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Titles.Clear();

        }
    }
};