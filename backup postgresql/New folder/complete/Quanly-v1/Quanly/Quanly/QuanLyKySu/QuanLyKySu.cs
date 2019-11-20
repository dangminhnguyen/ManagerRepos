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

namespace Quanly.QuanLyKySu
{
    public partial class QuanLyKySu : Form
    {
        string connString;
        public QuanLyKySu()
        {
            InitializeComponent();
            connString = new Property().ConnString;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);
            getds();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        public void getds()
        {
            dataGridView1.Rows.Clear();
            KySuHandler lh = new KySuHandler();
            List<KySu> ls = lh.layds();
            foreach (KySu lm in ls)
            {
                dataGridView1.Rows.Add(lm.ma, lm.ten);
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string ma = row.Cells[0].Value.ToString();
                string ten = row.Cells[1].Value.ToString();
                ChinhSuaKySu frm2 = new ChinhSuaKySu(ma, ten)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                frm2.ShowDialog(this);
                getds();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThemKySu frm2 = new ThemKySu();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
            getds();
        }
    }
}
