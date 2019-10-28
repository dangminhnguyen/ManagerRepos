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

namespace Quanly.QuanLyNoiLayPhTung
{
    public partial class QuanLyNoiLay : Form
    {
        string connString;
        public QuanLyNoiLay()
        {
            InitializeComponent();
            connString = new Property().ConnString;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            
            getds();
        }
        public void getds()
        {
            dataGridView1.Rows.Clear();
            NoiLayPTHandler lh = new NoiLayPTHandler();
            List<NoiLayPT> ls = lh.layds();
            foreach (NoiLayPT lm in ls)
            {
                dataGridView1.Rows.Add(lm.ma, lm.ten);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThemNoiLay frm2 = new ThemNoiLay();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
            getds();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string ma = row.Cells[0].Value.ToString();
                string ten = row.Cells[1].Value.ToString();
                SuaNoiLay frm2 = new SuaNoiLay(ma, ten)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                frm2.ShowDialog(this);
                getds();
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }
    }
}
