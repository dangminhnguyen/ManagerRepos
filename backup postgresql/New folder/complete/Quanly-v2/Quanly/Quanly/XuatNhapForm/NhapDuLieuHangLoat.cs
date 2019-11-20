using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Npgsql;
using System.Drawing;
using Quanly.Handler;
using Quanly.Model;
using System.ComponentModel;

namespace Quanly
{
    public partial class NhapDuLieuHangLoat : Form
    {
        String connString;
        String vaitro;
        String user;
        String path = "";

        public NhapDuLieuHangLoat(String user,String vaitro)
        {
            InitializeComponent();
            connString = new Property().ConnString;
            this.user = user;
            this.vaitro = vaitro;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = String.Format("{0} dòng dữ liệu được ghi nhận ", dataGridView2.Rows.Count);
            if (progressBar1.Value < progressBar1.Maximum)
            {
                label1.Text = "Dừng, " + label1.Text;
            }
            button1.Text = "Chọn...";
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
            {
                label1.Text = e.ProgressPercentage + "%";
                progressBar1.PerformStep();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            progressBar1.Invoke((Action)(() => progressBar1.Maximum = rowCount));

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 2; i <= rowCount; i++)
            {
                backgroundWorker1.ReportProgress(i / rowCount * 100);
                String check = "Dữ liệu chuẩn xác,nhập thành công";
                String maseri = xlRange.Cells[i, 1].Value2.ToString();
                String date = xlRange.Cells[i, 2].Value2.ToString();
                String hientrang = xlRange.Cells[i, 3].Value2.ToString();
                String makysu = xlRange.Cells[i, 4].Value2.ToString();
                String mota = xlRange.Cells[i, 5].Value2.ToString();
                String khacphuc = xlRange.Cells[i, 6].Value2.ToString();
                

                int serialdate = Int32.Parse(date);
                DateTime date1 = FromExcelSerialDate(serialdate);
                MessageBox.Show(date1.ToString());


                String[] mang = kiemTraSeri(maseri);
                String checkseri = mang[0];
                String tenseri = mang[1];
                String model = mang[2];
                String may = mang[3];
                if (checkseri.Equals("false"))
                {
                    check = "Không thể nhận dạng mã seri";
                    int row = dataGridView1.Rows.Add(maseri, tenseri, model, may,
                        date, hientrang, makysu, khacphuc, mota, check);

                    dataGridView1.Rows[row].Cells[0].Style.ForeColor = Color.Red;
                    continue;
                }

                DateTime? checkdate = kiemTraNgay(date1);
                DateTime mydate;
                if (checkdate == null)
                {
                    check = "Không thể nhận dạng thời gian";
                    int row = dataGridView1.Rows.Add(maseri, tenseri, model, may,
                         date, hientrang, makysu, khacphuc, mota, check);

                    dataGridView1.Rows[row].Cells[4].Style.ForeColor = Color.Red;
                    continue;
                }
                else
                {
                    //mydate = DateTime.Today;
                    mydate = checkdate ?? DateTime.Now;
                }

                Boolean checkHienTrang = kiemTraHienTrang(hientrang);
                if (checkHienTrang == false)
                {
                    check = "Không thể nhận dạng hiện trạng";
                    int row = dataGridView1.Rows.Add(maseri, tenseri, model, may,
                          date, hientrang, makysu, khacphuc, mota, check);
                    dataGridView1.Rows[row].Cells[5].Style.ForeColor = Color.Red;
                    continue;
                }

                Boolean checkKySu = kiemTraKySu(makysu);
                if (checkKySu == false)
                {
                    check = "Không thể nhận dạng mã kỹ sư";
                    int row = dataGridView1.Rows.Add(maseri, tenseri, model, may,
                          date, hientrang, makysu, khacphuc, mota, check);
                    dataGridView1.Rows[row].Cells[6].Style.ForeColor = Color.Red;
                    continue;
                }


                dataGridView2.Rows.Add(maseri, tenseri, model, may, mydate.ToString("dd/MM/yyyy HH:mm:ss"), hientrang, mota, check);
                SeriHandler seh = new SeriHandler();
                Model.Seri se = seh.tracuuma(maseri);

                LichSuHandler lih = new LichSuHandler();
                lih.them(se.pk, hientrang, khacphuc, makysu, mydate, mota);
               
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            backgroundWorker1.ReportProgress(100);
        }
        public static DateTime FromExcelSerialDate(int SerialDate)
        {
            if (SerialDate > 59) SerialDate -= 1; //Excel/Lotus 2/29/1900 bug   
            return new DateTime(1899, 12, 31).AddDays(SerialDate);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Chọn file excel";
            openFileDialog1.Filter = "Excel file|*.xlsx;*.xls";
            openFileDialog1.InitialDirectory = @"C:\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path=openFileDialog1.FileName.ToString();
                if (backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.CancelAsync();
                }
                else
                {
                    progressBar1.Value = progressBar1.Minimum;
                    button1.Text = "Stop";
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();
                    backgroundWorker1.RunWorkerAsync();
                }


            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        public String[] kiemTraSeri(String maseri)
        {
            String[] mang = new String[4];
            //kiem tra maseri
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Select * from seri where ma=@ma";
                    cmd.Parameters.AddWithValue("ma", maseri);
                    using (var reader = cmd.ExecuteReader())
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                mang[0] = "true";
                                mang[1] = reader.GetString(reader.GetOrdinal("ten"));
                                String mamodel = reader.GetString(reader.GetOrdinal("mamodel"));
                                String mamay = reader.GetString(reader.GetOrdinal("mamay"));

                                ModelHandler moh = new ModelHandler();
                                Model.Model mo = moh.laythongtin(mamodel, "", "");
                                mang[2] = mo.ten;

                                LoaiMayHandler mah = new LoaiMayHandler();
                                Model.LoaiMay ma = mah.laythongtin(mamay, "");
                                mang[3] = ma.ten;             
                            }
                        }
                        else
                        {
                            mang[0] = "false";
                            mang[1] = "";
                            mang[2] = "";
                            mang[3] = "";
                        }
                }
            }
            return mang;
        }

        public DateTime? kiemTraNgay(DateTime date) 
        {
           

            


            DateTime? mydate = new DateTime?();
            try
            {

                /*int day = Int32.Parse(date.Substring(0, 2));
                int month = Int32.Parse(date.Substring(2, 2));
                int years = 2000 + Int32.Parse(date.Substring(4, 2));
                MessageBox.Show(years.ToString());
               // int hour = Int32.Parse(date.Substring(8, 2));
                //int minute = Int32.Parse(date.Substring(10, 2));
                //int second = Int32.Parse(date.Substring(12, 2));
                mydate = new DateTime(2019, 10, 10, 00, 00, 00);*/
                mydate = date;
               
                

            }
            catch (Exception ev)
            {
                mydate = null;
            }

            /*if (date.Length > 14)
            {
                mydate = null;
            }*/

            return mydate;
        }

        public void xuLyFile()
        {
            
        }

        public Boolean kiemTraHienTrang(String hientrang)
        {
            HienTrangHandler hit = new HienTrangHandler();
            Boolean check = hit.kiemtra(hientrang);
            return check;
        }

        public Boolean kiemTraKySu(String makysu)
        {
            KySuHandler kit = new KySuHandler();
            Boolean check = kit.kiemtra(makysu,"");
            return check;
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
