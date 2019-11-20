using Quanly.Handler;
using Quanly.Model;
using Quanly.QuanLyLoaiMay;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Quanly
{
    public partial class Quanlymay : Form
    {
        String connString;
        public Quanlymay()
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
            dataGridView1.Rows.Clear();
            LoaiMayHandler lh = new LoaiMayHandler();
            List<LoaiMay> ls= lh.layds();
            foreach(LoaiMay lm in ls)
            {
                dataGridView1.Rows.Add(lm.ma, lm.ten);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThemLoaiMay frm2 = new ThemLoaiMay();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
            getdsmay();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string ma = row.Cells[0].Value.ToString();
                string ten = row.Cells[1].Value.ToString();
                ChinhSuaLoaiMay frm2 = new ChinhSuaLoaiMay(ma, ten)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                frm2.ShowDialog(this);
                getdsmay();
            }
        }
    }
}
