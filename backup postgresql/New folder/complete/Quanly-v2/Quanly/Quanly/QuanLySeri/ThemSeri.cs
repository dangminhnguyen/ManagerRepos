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

namespace Quanly.QuanLySeri
{
    public partial class ThemSeri : Form
    {
        string mamay;
        string mamodel;
        public ThemSeri(string mamay,string mamodel)
        {
            InitializeComponent();
            this.mamay = mamay;
            this.mamodel = mamodel;
            getdskh();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        public void getdskh()
        {
            comboBox2.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            KhachHangHandler lh = new KhachHangHandler();
            List<Model.KhachHang> ls = lh.layds();
            foreach (Model.KhachHang lm in ls)
            {
                combosource.Add(lm.ma, lm.ma + " - " + lm.ten);
            }
            comboBox2.DataSource = new BindingSource(combosource, null);
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";
            comboBox2.SelectedIndex = 0;
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
            SeriHandler lh = new SeriHandler();
            TongHandler th = new TongHandler();
            Boolean checkma = lh.kiemtra(ma, "", "","");
            if (checkma)
            {
                MessageBox.Show("Mã nội bộ phải là duy nhất", "Lỗi khi sửa ",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else
            {
                string makh = ((KeyValuePair<string, string>)comboBox2.SelectedItem).Key;
                int k =  lh.them(ma, ten, mamay,mamodel,makh,dateTimePicker1.Value.Date);
                th.them(k, ma, mamay, mamodel, makh);
                this.Close();
            }
        }
    }
}
