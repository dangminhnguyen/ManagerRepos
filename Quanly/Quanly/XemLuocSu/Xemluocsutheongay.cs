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
    public partial class Xemluocsutheongay : Form
    {
        
        int pk;
        Seri s = new Seri();

        public Xemluocsutheongay()
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
          
            

        }
        
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

       
        public void tracuu()
        {
            dataGridView1.Rows.Clear();
            
            LoaiMayHandler lmh = new LoaiMayHandler();
            List<LoaiMay> llm = lmh.layds(); ;
            foreach(LoaiMay may in llm)
            {
                ModelHandler mh = new ModelHandler();
                List<Model.Model> lm = mh.layds(may.ma);
                foreach (Model.Model m in lm)
                {
                    SeriHandler sh = new SeriHandler();
                    List<Model.Seri> ls = sh.layds(may.ma, m.ma);
                    foreach (Model.Seri s in ls)
                    {
                        KhachHangHandler khh = new KhachHangHandler();
                        KhachHang kh = khh.laythongtin(s.khachhang, "");

                        List<Record> list = new List<Record>();
                        LichSuHandler lh = new LichSuHandler();

                        list = lh.tracuu(s.pk, dateTimePicker1.Value, dateTimePicker2.Value,"");

                        foreach (Record r in list)
                        {
                            KySuHandler ksh = new KySuHandler();
                            KySu ks = ksh.laythongtin(r.phutrach, "");

                            dataGridView1.Rows.Add(r.pk,may.ten, m.ten, s.ten, kh.ten, r.hientrang,
                                r.khacphuc, ks.ten, r.thoigian.ToString("dd/MM/yyyy hh:mm:ss"), r.ghichu);
                        }

                    }
                }
            }     
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

                xlWorkSheet.Cells[1, 1] = "Loại máy";
                xlWorkSheet.Cells[1, 2] = "Model";
                xlWorkSheet.Cells[1, 3] = "Seri";
                xlWorkSheet.Cells[1, 4] = "Khách Hàng";
                xlWorkSheet.Cells[1, 5] = "Hiện trạng";
                xlWorkSheet.Cells[1, 6] = "Khắc phục";
                xlWorkSheet.Cells[1, 7] = "Phụ trách";
                xlWorkSheet.Cells[1, 8] = "Thời gian";
                xlWorkSheet.Cells[1, 9] = "Mô tả";
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
                    xlWorkSheet.Cells[i, 6] = row.Cells[6].Value;
                    xlWorkSheet.Cells[i, 7] = row.Cells[7].Value;
                    xlWorkSheet.Cells[i, 8] = row.Cells[8].Value;
                    xlWorkSheet.Cells[i, 9] = row.Cells[9].Value;
                    i++;
                }
                
                string filepath = folderBrowserDialog1.SelectedPath.ToString() +"\\"+ "danhsach"+ ".xlsx";
                xlWorkBook.SaveAs(filepath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                this.Close();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tracuu();
        }
    }
}
