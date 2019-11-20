using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quanly.Handler;
using Quanly.Model;

namespace Quanly.QuanLyKhachHang
{
    public partial class ChinhSuaKhachHang : Form
    {
        string macu = "";
        string tencu = "";
        public ChinhSuaKhachHang(string ma,string ten,string thongtin)
        {
            InitializeComponent();
            macu = ma;
            tencu = ten;
            textBox1.Text = macu;
            textBox2.Text = tencu;
            richTextBox1.Text = thongtin;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ma = textBox1.Text;
            string ten = textBox2.Text;
            string thongtin = richTextBox1.Text;
            if (ma == null || ma.Equals(""))
            {
                MessageBox.Show("Không chấp nhận mã thay đổi là khoảng trống", "Lỗi khi sửa ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ten == null || ten.Equals(""))
            {
                MessageBox.Show("Không chấp nhận tên thay đổi là khoảng trống", "Lỗi khi sửa ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            KhachHangHandler lh = new KhachHangHandler();
            Boolean checkma = lh.kiemtra(ma, "");
            if (checkma && macu != ma)
            {
                MessageBox.Show("Mã nội bộ phải là duy nhất", "Lỗi khi sửa ",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else
            {
                lh.sua(ten, tencu, ma, macu,thongtin);
                this.Close();
            }
        }
    }
}
