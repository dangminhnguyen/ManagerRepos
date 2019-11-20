using Npgsql;
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
using Quanly.QuanLyKhachHang;


namespace Quanly
{
    public partial class QuanlyKH : Form
    {
        String connString;
        public QuanlyKH()
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
            KhachHangHandler lh = new KhachHangHandler();
            List<KhachHang> ls = lh.layds();
            foreach (KhachHang lm in ls)
            {
                dataGridView1.Rows.Add(lm.ma, lm.ten,lm.thongtin);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ThemKhachHang frm2 = new ThemKhachHang();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
            getds();
        }

        private void dataGridView1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string ma = row.Cells[0].Value.ToString();
                string ten = row.Cells[1].Value.ToString();
                string thongtin= row.Cells[2].Value.ToString();
                ChinhSuaKhachHang frm2 = new ChinhSuaKhachHang(ma, ten,thongtin)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                frm2.ShowDialog(this);
                getds();
            }
        }
    }
}
