using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Npgsql;
using Quanly.Handler;
using Quanly.Model;

namespace Quanly.XuatNhapForm
{
    public partial class Nhaptheomay : Form
    {
        int pk;
        public Nhaptheomay(int keyseri)
        {
            InitializeComponent();
            button2.Enabled = false;
            pk = keyseri;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Chọn file excel";
            openFileDialog1.Filter = "Excel file|*.xlsx;*xls";
            openFileDialog1.InitialDirectory = @"C:\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String path = openFileDialog1.FileName.ToString();
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;
                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                //iterate over the rows and columns and print to the console as it appears in the file
                //excel is not zero based!!
                for (int i = 1; i <= rowCount; i++)
                {
                    String check = "true";
                    String hientrang = xlRange.Cells[i, 1].Value2.ToString();
                    String khacphuc = xlRange.Cells[i, 2].Value2.ToString();
                    String phutrach = xlRange.Cells[i, 3].Value2.ToString();
                    String ghichu = xlRange.Cells[i, 5].Value2.ToString();
                    String thoigian= xlRange.Cells[i, 4].Value2.ToString();
                    DateTime date;
                    try
                    {
                        int day = Int32.Parse(thoigian.Substring(0, 2));
                        int month = Int32.Parse(thoigian.Substring(3, 2));
                        int year = Int32.Parse(thoigian.Substring(6, 4));
                        int hour = Int32.Parse(thoigian.Substring(11, 2));
                        int min = Int32.Parse(thoigian.Substring(14, 2));
                        int second = Int32.Parse(thoigian.Substring(17, 2));
                        date = new DateTime(year, month, day, hour, min, second);
                    }
                    catch(Exception ex)
                    {
                        check = "Ko nhận dạng được thời gian";
                        int row = dataGridView1.Rows.Add(hientrang, khacphuc, phutrach, 
                            xlRange.Cells[i, 4].Value2, ghichu, check);
                        for (int i2 = 0; i2 < 6; i2++)
                        {
                            dataGridView1.Rows[row].Cells[i2].Style.ForeColor = Color.Red;
                        }
                        continue;
                    }                 
                    

                    KySuHandler kh = new KySuHandler();
                    KySu k = kh.laythongtin("", phutrach);
                    if (k.ma == null)
                    {
                        check = "Ko tìm được tên phụ trách";
                        int row = dataGridView1.Rows.Add(hientrang, khacphuc, phutrach, thoigian.ToString(), ghichu, check);
                        for (int i2 = 0; i2 < 6; i2++)
                        {
                            dataGridView1.Rows[row].Cells[i2].Style.ForeColor = Color.Red;
                        }
                        continue;
                    }

                   

                    HienTrangHandler hh = new HienTrangHandler();
                    HienTrang h = hh.laythongtin(0, hientrang);
                    if (h.ma == 0)
                    {
                        check = "Ko tìm được tên hiện trạng";
                        int row = dataGridView1.Rows.Add(hientrang, khacphuc, phutrach, thoigian.ToString(), ghichu, check);
                        for (int i2 = 0; i2 < 6; i2++)
                        {
                            dataGridView1.Rows[row].Cells[i2].Style.ForeColor = Color.Red;
                        }
                        continue;
                    }
                    dataGridView1.Rows.Add(hientrang, khacphuc, phutrach, thoigian, ghichu, check);                   
                }
                xlWorkbook.Close(false);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorksheet);
                Marshal.ReleaseComObject(xlWorkbook);
                Marshal.ReleaseComObject(xlApp);


                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[5].Value.ToString().Contains("true"))
                {
                    try
                    {
                        String hientrang = row.Cells[0].ToString();
                        String khacphuc = row.Cells[1].ToString();
                        String kysu = row.Cells[2].ToString();
                        String thoigian = row.Cells[3].ToString();
                        String ghichu = row.Cells[4].ToString();

                        DateTime date;
                        int day = Int32.Parse(thoigian.Substring(0, 2));
                        int month = Int32.Parse(thoigian.Substring(3, 5));
                        int year = Int32.Parse(thoigian.Substring(6, 10));
                        int hour = Int32.Parse(thoigian.Substring(11, 13));
                        int min = Int32.Parse(thoigian.Substring(14, 16));
                        int second = Int32.Parse(thoigian.Substring(17, 19));
                        date = new DateTime(year, month, day, hour, min, second);


                        LichSuHandler ls = new LichSuHandler();
                        ls.them(pk, hientrang, khacphuc, kysu, date, ghichu);
                        count++;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }  
                }
            }
            MessageBox.Show("Nhập " + count.ToString()+ " hàng vào lược sử","Thông tin");
            this.Close();
        }
    }
}
