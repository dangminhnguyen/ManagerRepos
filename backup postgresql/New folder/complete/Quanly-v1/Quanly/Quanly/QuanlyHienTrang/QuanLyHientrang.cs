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

namespace Quanly.QuanlyHienTrang
{
    public partial class QuanLyHientrang : Form
    {
        string connString;
        public QuanLyHientrang()
        {
            InitializeComponent();
            connString = new Property().ConnString;
            layds();
        }

        public void layds()
        {
            listBox1.Items.Clear();
            HienTrangHandler hh = new HienTrangHandler();
            List<HienTrang> list = hh.layds();
            foreach(HienTrang h in list)
            {
                listBox1.Items.Add(h.ten);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ten = textBox1.Text;
            string tencu = listBox1.Text;
            if (ten == null || ten.Equals(""))
            {
                MessageBox.Show("Không chấp nhận tên thay đổi là khoảng trống", "Lỗi khi sửa ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HienTrangHandler lh = new HienTrangHandler();
            Boolean check = lh.kiemtra(ten);
            if (check && tencu!=ten)
            {
                MessageBox.Show("Tên phải là duy nhất", "Lỗi khi sửa ",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else
            {
                lh.sua(ten, tencu);
                layds();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ten = listBox1.Text;
            HienTrangHandler lh = new HienTrangHandler();
            lh.xoa(ten);
            layds();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string ten = textBox1.Text;
            if (ten == null || ten.Equals(""))
            {
                MessageBox.Show("Không chấp nhận tên thay đổi là khoảng trống", "Lỗi khi sửa ",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HienTrangHandler lh = new HienTrangHandler();
            lh.them(ten);
            layds();
        }
    }
}
