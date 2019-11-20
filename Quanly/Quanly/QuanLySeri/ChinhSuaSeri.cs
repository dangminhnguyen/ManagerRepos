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
    public partial class ChinhSuaSeri : Form
    {
        string macu;
        string tencu;
        string mamay;
        string mamodel;
        public ChinhSuaSeri(string macu,string tencu, string mamay,string mamodel,string makh,DateTime ngaylapdat, int hientrang,string phutrach)
        {
            InitializeComponent();
            this.macu = macu;
            this.tencu = tencu;
            this.mamay = mamay;
            this.mamodel = mamodel;
            textBox1.Text = macu;
            textBox2.Text = tencu;
            getdskh();
            getdsks();

            getdshtr();
            comboBox2.SelectedValue = makh;
            hientrangcb.SelectedValue = hientrang.ToString();
            phutrachcombo.SelectedValue = phutrach;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Value = ngaylapdat;
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
        public void getdshtr()
        {
            hientrangcb.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            HienTrangHandler lh = new HienTrangHandler();
            List<Model.HienTrang> ls = lh.layds();
            foreach (Model.HienTrang lm in ls)
            {
                combosource.Add(lm.ma.ToString(), lm.ma + " - " + lm.ten);
            }
            hientrangcb.DataSource = new BindingSource(combosource, null);
            hientrangcb.DisplayMember = "Value";
            hientrangcb.ValueMember = "Key";
            hientrangcb.SelectedIndex = 0;
        }
        public void getdsks()
        {
            phutrachcombo.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            KySuHandler lh = new KySuHandler();
            List<Model.KySu> ls = lh.layds();
            foreach (Model.KySu lm in ls)
            {
                combosource.Add(lm.ma, lm.ma + " - " + lm.ten);
            }
            phutrachcombo.DataSource = new BindingSource(combosource, null);
            phutrachcombo.DisplayMember = "Value";
            phutrachcombo.ValueMember = "Key";
            phutrachcombo.SelectedIndex = 0;
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
            Boolean checkma = lh.kiemtra(ma, "", mamay, mamodel);
            if (checkma &&macu!=ma)
            {
                MessageBox.Show("Mã nội bộ phải là duy nhất", "Lỗi khi sửa ",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else
            {
                string makh = ((KeyValuePair<string, string>)comboBox2.SelectedItem).Key;
                string maks = ((KeyValuePair<string, string>)phutrachcombo.SelectedItem).Key;
                string mahtr = ((KeyValuePair<string, string>)hientrangcb.SelectedItem).Key;
                int mahtrang = 0;
                Int32.TryParse(mahtr, out mahtrang);
                // sua(string ma, string ten, string mamay, string mamodel, string macu, string phutrach, int hientrang, string tencu, string khachhang, DateTime ngaylapdat)
                

                lh.sua(ma, ten, mamay, mamodel,macu,tencu, maks ,mahtrang, makh,dateTimePicker1.Value.Date);
                Model.Seri sr = lh.laythongtinma(ma);
                LichSuSeriHandler lssr = new LichSuSeriHandler();

                lssr.them(sr.pk, dateTimePicker1.Value.Date, mahtrang, maks, makh);

                this.Close();
            }
        }
    }
}
