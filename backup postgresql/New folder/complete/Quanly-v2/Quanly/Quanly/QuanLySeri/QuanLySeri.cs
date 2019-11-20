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

namespace Quanly.QuanLySeri
{
    public partial class QuanLySeri : Form
    {
        String connString;
        public QuanLySeri()
        {
            InitializeComponent();
            connString = new Property().ConnString;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);
            getdsmay();
            
            
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        public void getdsmay()
        {
            comboBox1.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            LoaiMayHandler lh = new LoaiMayHandler();
            List<LoaiMay> ls = lh.layds();
            foreach (LoaiMay lm in ls)
            {
                combosource.Add(lm.ma, lm.ma + " - " + lm.ten);
            }
            comboBox1.DataSource = new BindingSource(combosource, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            comboBox1.SelectedIndex = 0;
        }

        public void getdsmodel()
        {
            //comboBox2.DataSource = null;
           
            string mamay = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            ModelHandler lh = new ModelHandler();
            List<Model.Model> ls = lh.layds(mamay);
            if (ls.Count == 0)
            {
                combosource.Add("null", "null");
            }
            else
            {
                foreach (Model.Model lm in ls)
                {
                    combosource.Add(lm.ma, lm.ma + " - " + lm.ten);
                }
            }
        
            
            comboBox2.DataSource = new BindingSource(combosource, null);
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";
            comboBox2.SelectedIndex = 0;
        }

        public void getdsseri()
        {
            dataGridView1.Rows.Clear();
            string mamay = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            
            string mamodel = ((KeyValuePair<string, string>)comboBox2.SelectedItem).Key;
            if (mamodel.Equals("null"))
            {
                MessageBox.Show("Chua có model máy ở loại máy này", "Lỗi khi thêm ",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SeriHandler mh = new SeriHandler();
            List<Model.Seri> ls = mh.layds(mamay,mamodel);
            foreach (Model.Seri lm in ls)
            {
                KhachHangHandler kh = new KhachHangHandler();
                KhachHang k = kh.laythongtin(lm.khachhang, "");
                if (lm.ngaylapdat == null)
                {
                    dataGridView1.Rows.Add(lm.ma, lm.ten, lm.khachhang, k.ten, "");
                }
                else
                {
                    DateTime Ut = lm.ngaylapdat ?? DateTime.Now;
                    dataGridView1.Rows.Add(lm.ma, lm.ten, lm.khachhang, k.ten, Ut.ToString("dd/MM/yyyy"));
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mamay = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            string mamodel = ((KeyValuePair<string, string>)comboBox2.SelectedItem).Key;
            string mamodel1 = ((KeyValuePair<string, string>)comboBox2.SelectedItem).Value;
            MessageBox.Show(mamodel);
            if (mamodel.Equals("null"))
            {
                MessageBox.Show("Chua có model máy ở loại máy này", "Lỗi khi thêm ",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ThemSeri frm2 = new ThemSeri(mamay, mamodel);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
            getdsseri();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdsmodel();
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           getdsseri();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string mamay = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            string mamodel = ((KeyValuePair<string, string>)comboBox2.SelectedItem).Key;
            if (mamodel.Equals("null"))
            {
                MessageBox.Show("Chua có model máy ở loại máy này", "Lỗi khi thêm ",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string ma = row.Cells[0].Value.ToString();
                string ten = row.Cells[1].Value.ToString();
                string makh= row.Cells[2].Value.ToString();
                SeriHandler sh = new SeriHandler();
                Model.Seri s = new Model.Seri();
                s = sh.tracuuma(ma);
                DateTime UpdatedTime = s.ngaylapdat?? DateTime.Now;
                ChinhSuaSeri frm2 = new ChinhSuaSeri(ma, ten, mamay,mamodel,makh,UpdatedTime)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                frm2.ShowDialog(this);
                getdsseri();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
