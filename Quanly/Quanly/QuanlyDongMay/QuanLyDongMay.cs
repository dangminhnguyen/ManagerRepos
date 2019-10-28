using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Quanly.Handler;
using Quanly.Model;

namespace Quanly.QuanlyDongMay
{
    public partial class QuanlyDongMay : Form
    {
        String connString;
        public QuanlyDongMay()
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
            foreach(LoaiMay lm in ls)
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
            dataGridView1.Rows.Clear();
            string mamay = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            ModelHandler mh = new ModelHandler();
            List<Model.Model> ls = mh.layds(mamay);
            foreach (Model.Model lm in ls)
            {
                dataGridView1.Rows.Add(lm.ma, lm.ten);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdsmodel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mamay = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            ThemDongMay frm2 = new ThemDongMay(mamay);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
            getdsmodel();

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string mamay = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string ma = row.Cells[0].Value.ToString();
                string ten = row.Cells[1].Value.ToString();
                ChinhSuaDongMay frm2 = new ChinhSuaDongMay(ma, ten,mamay)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                frm2.ShowDialog(this);
                getdsmodel();
            }
        }
    }
   

}
