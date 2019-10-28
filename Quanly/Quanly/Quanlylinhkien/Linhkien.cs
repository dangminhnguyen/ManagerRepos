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

namespace Quanly.Quanlylinhkien
{
    public partial class Linhkien : Form
    {
        public Linhkien()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);
            getModel();
            tracuu();
        }
        void getModel()
        {
            modellk.Items.Clear();
            ModelHandler mdh = new ModelHandler();
            List<Model.Model> mdl = new List<Model.Model>();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            
            mdl = mdh.layds();
            foreach(Model.Model md in mdl)
            {
                combosource.Add(md.ma, md.ten);

            }
            modellk.DataSource = new BindingSource(combosource, null);
            modellk.DisplayMember = "Value";
            modellk.ValueMember = "Key";
            modellk.SelectedIndex = 0;
        }
        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void DataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string ma = malk.Text;
            string ten = tenlk.Text;
            string model = ((KeyValuePair<string, string>)modellk.SelectedItem).Key;
            
            if(ma == "" || model=="" || ten=="")
            {
                
                MessageBox.Show("không được để trống bất cứ trường nào!!!!");
            }
            else
            {
               
                LinhKienHandle lkh = new LinhKienHandle();
                lkh.them(model, ma, ten);
            }
            tracuu();
        }
        private void tracuu()
        {
            dataGridView1.Rows.Clear();
            LinhKienHandle lkh = new LinhKienHandle();
            List<linhkien> llk = lkh.layds();
            foreach(linhkien lk in llk)
            {
                dataGridView1.Rows.Add(lk.model, lk.ma, lk.ten);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
