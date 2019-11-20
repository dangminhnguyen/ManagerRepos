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

namespace Quanly.QuanlyDongMay
{
    public partial class ChinhSuaDongMay : Form
    {
        string macu;
        string tencu;
        string mamay;
        public ChinhSuaDongMay(string macu,string tencu,string mamay)
        {
            InitializeComponent();
            this.macu = macu;
            this.tencu = tencu;
            this.mamay = mamay;
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
            ModelHandler lh = new ModelHandler();
            Boolean checkma = lh.kiemtra(ma, "", mamay);
            if (checkma && macu!=ma)
            {
                MessageBox.Show("Mã nội bộ phải là duy nhất", "Lỗi khi sửa ",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else
            {
                lh.sua(ma, ten, mamay,macu,tencu);
                this.Close();
            }
        }
    }
}
