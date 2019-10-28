using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace Quanly
{
    public partial class NhapLieuHangLoat : Form
    {
        String path = "";
        public NhapLieuHangLoat()
        {
            InitializeComponent();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Chọn file excel";
            openFileDialog1.Filter = "excel file (*.xlsx)|*.xlsx";
            openFileDialog1.CheckFileExists = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range range;

                string str;
                int rCnt;
                int cCnt;
                int rw = 0;
                int cl = 0;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;


                for (rCnt = 1; rCnt <= rw; rCnt++)
                {
                    for (cCnt = 1; cCnt <= cl; cCnt++)
                    {
                        str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                        MessageBox.Show(str);
                    }
                }

                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
            }

        }
    }
}
