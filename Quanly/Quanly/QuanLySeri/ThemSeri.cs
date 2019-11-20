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
            getdsks();
            getdshtr();
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
                string maks = ((KeyValuePair<string, string>)phutrachcombo.SelectedItem).Key;
                string mahtg = ((KeyValuePair<string, string>)hientrangcb.SelectedItem).Key;
                int mahtrang = 0;
                Int32.TryParse(mahtg, out mahtrang);

                int keyseri= lh.them(ma, ten, mamay,mamodel,makh,dateTimePicker1.Value.Date,maks,mahtrang);

                Model.Seri sr = lh.laythongtinma(ma);

                LichSuSeriHandler lssr = new LichSuSeriHandler();

                lssr.them(sr.pk, dateTimePicker1.Value.Date, mahtrang, maks, makh);
                
                this.Close();
            }
        }
    }
}
