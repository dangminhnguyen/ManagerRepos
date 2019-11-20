using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using Quanly.Handler;
using Quanly.Model;

namespace Quanly
{
    public partial class Xemluocsu : Form
    {
        
        int pk;
        Seri s = new Seri();

        public Xemluocsu()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);
           
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            getdsmay();
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            getdsmodel();
            comboBox2.SelectedIndexChanged += new EventHandler(comboBox2_SelectedIndexChanged);
            getdsseri();
            comboBox3.SelectedIndexChanged += new EventHandler(comboBox3_SelectedIndexChanged);
            comboBox3.SelectedIndex = 0;


        }
        public Xemluocsu(int x)
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);            
            
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";

            SeriHandler sh = new SeriHandler();
            Seri s = sh.laythongtin(x);

            getdsmay();
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);     
            comboBox1.SelectedValue = s.mamay;
            getdsmodel();
            comboBox2.SelectedIndexChanged += new EventHandler(comboBox2_SelectedIndexChanged);
            comboBox2.SelectedValue = s.mamodel;
            getdsseri();
            comboBox3.SelectedIndexChanged += new EventHandler(comboBox3_SelectedIndexChanged);
            comboBox3.SelectedValue = x.ToString();
            gettenkh(x);
            MessageBox.Show(pk.ToString());
            pk = x;
            tracuu();

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
            //comboBox2.DataSource = null;

            string mamay = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            string mamodel = ((KeyValuePair<string, string>)comboBox2.SelectedItem).Key;
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            SeriHandler lh = new SeriHandler();
            List<Model.Seri> ls = lh.layds(mamay,mamodel);
            if (ls.Count == 0)
            {
                combosource.Add("0", "null");
            }
            else
            {
                foreach (Model.Seri lm in ls)
                {
                    combosource.Add(lm.pk.ToString(), lm.ma + " - " + lm.ten);
                }
            }


            comboBox3.DataSource = new BindingSource(combosource, null);
            comboBox3.DisplayMember = "Value";
            comboBox3.ValueMember = "Key";
            comboBox3.SelectedIndex = 0;
        }

        public void gettenkh(int x){
            SeriHandler sh = new SeriHandler();
           
            s = sh.laythongtin(x);
            KhachHangHandler kh = new KhachHangHandler();
            KhachHang k = kh.laythongtin(s.khachhang, "");
            textBox1.Text = k.ten;
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maseri = ((KeyValuePair<string, string>)comboBox3.SelectedItem).Key;
            int x = 0;
            Int32.TryParse(maseri, out x);
            pk = x;
            gettenkh(x);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdsseri();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdsmodel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tracuu();

        }

        public void tracuu()
        {
            List<Record> list = new List<Record>();
            LichSuHandler lh = new LichSuHandler();
            
            list = lh.tracuu(pk, dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView1.Rows.Clear();
            foreach (Record r in list)
            {
                KySuHandler kh = new KySuHandler();
                KySu k = kh.laythongtin(r.phutrach, "");
               
                dataGridView1.Rows.Add(r.pk, r.hientrang, r.khacphuc, k.ten,
                    r.thoigian.ToString("dd/MM/yyyy hh:mm:ss"),r.ghichu );
            }
            
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                String value0 = row.Cells[0].Value.ToString();
                int x = 0;
                Int32.TryParse(value0, out x);
                SuaHienTrang frm2 = new SuaHienTrang(x);
                frm2.StartPosition = FormStartPosition.CenterParent;
                frm2.ShowDialog(this);

            }
            tracuu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Themhientrang frm2 = new Themhientrang(pk);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
            dateTimePicker2.Value = DateTime.Now;
            tracuu();
        }

        private void xuấtNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void xuấtRaFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }


                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Cells[1, 1] = "Hiện trạng";
                xlWorkSheet.Cells[1, 2] = "Khắc phục";
                xlWorkSheet.Cells[1, 3] = "Phụ trách";
                xlWorkSheet.Cells[1, 4] = "Thời gian";
                xlWorkSheet.Cells[1, 5] = "Mô tả";
                Excel.Range formatRange;
                formatRange = xlWorkSheet.get_Range("a1");
                formatRange.EntireRow.Font.Bold = true;
                

                int i =2;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    xlWorkSheet.Cells[i, 1] = row.Cells[1].Value;
                    xlWorkSheet.Cells[i, 2] = row.Cells[2].Value;
                    xlWorkSheet.Cells[i, 3] = row.Cells[3].Value;
                    xlWorkSheet.Cells[i, 4] = row.Cells[4].Value;
                    xlWorkSheet.Cells[i, 5] = row.Cells[5].Value;
                    i++;
                }
                string maseri = ((KeyValuePair<string, string>)comboBox3.SelectedItem).Value;
                string filepath = folderBrowserDialog1.SelectedPath.ToString() +"\\"+ maseri+ ".xlsx";
                xlWorkBook.SaveAs(filepath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                this.Close();


            }
        }

        private void nhậpTừFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XuatNhapForm.Nhaptheomay frm2 = new XuatNhapForm.Nhaptheomay(pk);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
            tracuu();

        }
    }
}
