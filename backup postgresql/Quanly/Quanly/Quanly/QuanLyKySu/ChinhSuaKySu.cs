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

namespace Quanly.QuanLyKySu
{
    public partial class ChinhSuaKySu : Form
    {
        string macu = "";
        string tencu = "";
        public ChinhSuaKySu(string ma,string ten)
        {
            InitializeComponent();
            macu = ma;
            tencu = ten;
            textBox1.Text = macu;
            textBox2.Text = tencu;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ma = textBox1.Text;
            string ten = textBox2.Text;
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
            KySuHandler lh = new KySuHandler();
            Boolean checkma = lh.kiemtra(ma, "");
            if (checkma && macu != ma)
            {
                MessageBox.Show("Mã nội bộ phải là duy nhất", "Lỗi khi sửa ",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else
            {
                lh.sua(ten, tencu, ma, macu);
                this.Close();
            }
        }
    }
}
