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
using Quanly.Quanlyvitri;

namespace Quanly
{
    public partial class vitrikho : Form
    {
        string connString;
        public vitrikho()
        {
            InitializeComponent();
            connString = new Property().ConnString;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(DataGridView1_RowPrePaint);
            getds();
        }
        public void getds()
        {
            dataGridView1.Rows.Clear();
            VitriHandler lh = new VitriHandler();
            List<Vitri> ls = lh.layds();
            foreach (Vitri lm in ls)
            {
                dataGridView1.Rows.Add(lm.ma, lm.ten);
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            themvitri frm = new themvitri();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog(this);
        }

        private void DataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string ma = row.Cells[0].Value.ToString();
                string ten = row.Cells[1].Value.ToString();
                Suavitri frm2 = new Suavitri(ma, ten)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                frm2.ShowDialog(this);
                getds();
            }
        }
    }
}
