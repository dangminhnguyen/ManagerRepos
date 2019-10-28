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

namespace Quanly.Quanlyvitri
{
    public partial class Suavitri : Form
    {
        string macu = "";
        string tencu = "";
        public Suavitri(string ma, string ten)
        {

            InitializeComponent();
            macu = ma;
            tencu = ten;
            matxtb.Text = macu;
            tentxtb.Text = tencu;
        }
       

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string ma = matxtb.Text;
            string ten = tentxtb.Text;
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
            VitriHandler lh = new VitriHandler();
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
