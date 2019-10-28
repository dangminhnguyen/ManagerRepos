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
using Quanly.QuanLyKySu;
using Quanly.Handler;
using Quanly.Model;
using Quanly.Quanlylinhkien;
using Quanly.managerhistory;
using Quanly.QuanLyNoiLayPhTung;


namespace Quanly
{
    public partial class Main : Form
    {
        String connString;
        String vaitro;
        String user;
        public Main()

        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            connString = new Property().ConnString;
            //dangnhap();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);
            layds();
            laydsKho();

        }
       
        public void dangnhap()
        {
            Dangnhap frm2 = new Dangnhap();
            frm2.StartPosition = FormStartPosition.CenterParent;
            var result = frm2.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string val = frm2.ReturnValue1;            //values preserved after close
                if (val.Equals("Close")) this.Shown += new EventHandler(MyForm_CloseOnStart);
                else if (val.Equals("Open"))
                {
                    user = frm2.ReturnValue2;
                    vaitro = frm2.ReturnValue3;
                    if (!vaitro.Equals("admin"))
                    {
                        quảnLýToolStripMenuItem.Visible = false;
                    }
                }


            }
        }

        private void MyForm_CloseOnStart(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void layds()
        {
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col1 = new AutoCompleteStringCollection();
            LoaiMayHandler handler1 = new LoaiMayHandler();
            List<LoaiMay> list1 = handler1.layds();
            foreach (LoaiMay ob in list1)
            {
                col1.Add(ob.ten);
            }
            textBox1.AutoCompleteCustomSource = col1;

            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col2 = new AutoCompleteStringCollection();
            ModelHandler handler2 = new ModelHandler();
            List<Model.Model> list2 = handler2.layds();
            foreach (Model.Model ob in list2)
            {
                col2.Add(ob.ten);
            }
            textBox2.AutoCompleteCustomSource = col2;

            textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col3 = new AutoCompleteStringCollection();
            SeriHandler handler3 = new SeriHandler();
            List<Model.Seri> list3 = handler3.layds();
            foreach (Model.Seri ob in list3)
            {
                col3.Add(ob.ten);
            }
            textBox3.AutoCompleteCustomSource = col3;

            textBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col4 = new AutoCompleteStringCollection();
            KhachHangHandler handler4 = new KhachHangHandler();
            List<Model.KhachHang> list4 = handler4.layds();
            foreach (Model.KhachHang ob in list4)
            {
                col4.Add(ob.ten);
            }
            textBox4.AutoCompleteCustomSource = col4;
        }



        private void thêmMãMáyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quanlymay frm2 = new Quanlymay();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void thêmKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanlyKH frm2 = new QuanlyKH();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);

        }

        private void thêmModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanlyDongMay.QuanlyDongMay frm2 = new QuanlyDongMay.QuanlyDongMay();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);

        }

        private void quảnLýPhụTráchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quanly.QuanLyKySu.QuanLyKySu frm2 = new Quanly.QuanLyKySu.QuanLyKySu();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void thêmSeriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLySeri.QuanLySeri frm2 = new QuanLySeri.QuanLySeri();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);

        }




        private void themHienTrang_Click(Object sender, System.EventArgs e)
        {
            themHienTrang();
        }

        private void xemLichSu_Click(Object sender, System.EventArgs e)
        {
            xemLichSu();
        }

        private void themHienTrang()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                String value1 = row.Cells[0].Value.ToString();
                int x = 0;
                Int32.TryParse(value1, out x);
                Themhientrang frm2 = new Themhientrang(x);
                frm2.StartPosition = FormStartPosition.CenterParent;
                frm2.ShowDialog(this);

            }
        }

        private void xemLichSu()
        {

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                String value0 = row.Cells[0].Value.ToString();
                int x = 0;
                Int32.TryParse(value0, out x);
                inputhistory frm2 = new inputhistory(x);
                //Xemluocsu frm2 = new Xemluocsu(x);
                frm2.StartPosition = FormStartPosition.CenterParent;
                frm2.ShowDialog(this);

            } 
        }
        private void xemLichSuKho()
        {

            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                String value0 = row.Cells[0].Value.ToString();
                int x = 0;
                Int32.TryParse(value0, out x);
                Xemluocsu frm2 = new Xemluocsu(x);
                frm2.StartPosition = FormStartPosition.CenterParent;
                frm2.ShowDialog(this);

            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyTaiKhoan.QuanLyTaiKhoan frm2 = new QuanLyTaiKhoan.QuanLyTaiKhoan(user, vaitro);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Doimatkhau frm2 = new Doimatkhau(user);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void đổiTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dangnhap();

        }

        private void nhậpDữLiệuHàngLoạtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhapDuLieuHangLoat frm2 = new NhapDuLieuHangLoat(user, vaitro);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        private string format(string s)
        {
            if (s == null) s = "";
            return s;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            xemLichSu();
        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýHiệnTrạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanlyHienTrang.QuanLyHientrang frm2 = new QuanlyHienTrang.QuanLyHientrang();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void traCứuLịchSửToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Xemluocsu frm2 = new Xemluocsu();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void traCứuTheoMáyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void theoMáyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Xemluocsutheomay frm2 = new Xemluocsutheomay();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void theoNgàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Xemluocsutheongay frm2 = new Xemluocsutheongay();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void theoSeriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Xemluocsu frm2 = new Xemluocsu();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void nhậpDữLiệuHàngLoạtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NhapDuLieuHangLoat frm2 = new NhapDuLieuHangLoat(user, vaitro);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void càiĐặtChungToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            SeriHandler sh = new SeriHandler();
            List<Seri> list = sh.tracuu(format(textBox3.Text));
            foreach (Seri s in list)
            {
                LoaiMayHandler lh = new LoaiMayHandler();
                LoaiMay m = lh.laythongtin(s.mamay, "");
                if (m.ten.Equals(textBox1.Text) || textBox1.Text.Equals(""))
                {
                    ModelHandler mh = new ModelHandler();
                    Model.Model mo = mh.laythongtin(s.mamodel, "", "");
                    //if(mo.ten.Equals(format(textBox2.Text)) || textBox2.Text.Equals(format("")))
                    if (mo.ten == format(textBox2.Text) || textBox2.Text == "")
                    {
                        LichSuHandler lih = new LichSuHandler();
                        Record r = lih.traCuuGanNhat(s.pk);

                        if (r.khachhang != null && r.khachhang != "")
                        {
                            KhachHangHandler kh = new KhachHangHandler();
                            KhachHang k = kh.laythongtin(r.khachhang, "");
                            if (k.ten.Equals(textBox4.Text) || textBox4.Text.Equals(""))
                            {
                                if (r.pk == 0)
                                {
                                    dataGridView1.Rows.Add(s.pk, s.ma, s.ten, mo.ten, m.ten,k.ten
                                        , "", "", "", "");
                                }
                                else
                                {
                                    KySuHandler kih = new KySuHandler();
                                    KySu kysu = kih.laythongtin(r.phutrach, "");

                                    dataGridView1.Rows.Add(s.pk, s.ma, s.ten, mo.ten, m.ten,k.ten, kysu.ten, r.thoigian.ToString("dd/MM/yyyy hh:mm:ss"),
                                             r.hientrang, r.ghichu);
                                }
                            }
                        }
                        else
                        {
                            if (r.pk == 0)
                            {
                                dataGridView1.Rows.Add(s.pk, s.ma, s.ten, mo.ten, m.ten,""
                                    , "", "", "", "");
                            }
                            else
                            {
                                KySuHandler kih = new KySuHandler();
                                KySu kysu = kih.laythongtin(r.phutrach, "");

                                dataGridView1.Rows.Add(s.pk, s.ma, s.ten, mo.ten, m.ten,"", kysu.ten, r.thoigian.ToString("dd/MM/yyyy hh:mm:ss"),
                                         r.hientrang, r.ghichu);
                            }
                        }
                    }
                }

            }

        }
        // kho
        private void Button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            SeriHandler sh = new SeriHandler();
            List<Seri> list = sh.tracuukho(format(seriKhoTB.Text));
            foreach (Seri s in list)
            {
                LoaiMayHandler lh = new LoaiMayHandler();
                LoaiMay m = lh.laythongtin(s.mamay, "");
                if (m.ten.Equals(loaimayKhoTB.Text) || loaimayKhoTB.Text.Equals(""))
                {
                    ModelHandler mh = new ModelHandler();
                    Model.Model mo = mh.laythongtin(s.mamodel, "", "");
                    if (mo.ten == format(modelKhoTB.Text) || modelKhoTB.Text == "")
                    {
                        KhachHangHandler kh = new KhachHangHandler();
                        KhachHang k = kh.laythongtin(s.khachhang, "");

                        LichSuHandler lih = new LichSuHandler();
                        Record r = lih.traCuuGanNhat(s.pk);
                        if (r.pk == 0)
                        {
                            dataGridView2.Rows.Add(s.pk, s.ma, s.ten, mo.ten, m.ten,
                                k.ten, "", "", "", "");
                        }
                        else
                        {
                            KySuHandler kih = new KySuHandler();
                            KySu kysu = kih.laythongtin(r.phutrach, "");

                            dataGridView2.Rows.Add(s.pk, s.ma, s.ten, mo.ten, m.ten,
                                k.ten, kysu.ten, r.thoigian.ToString("dd/MM/yyyy hh:mm:ss"),
                                r.hientrang, r.ghichu);
                        }

                    }

                }

            }
        }
        private void laydsKho()
        {
            loaimayKhoTB.AutoCompleteMode = AutoCompleteMode.Suggest;
            loaimayKhoTB.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col1 = new AutoCompleteStringCollection();
            LoaiMayHandler handler1 = new LoaiMayHandler();
            List<LoaiMay> list1 = handler1.layds();
            foreach (LoaiMay ob in list1)
            {
                col1.Add(ob.ten);
            }
            loaimayKhoTB.AutoCompleteCustomSource = col1;

            modelKhoTB.AutoCompleteMode = AutoCompleteMode.Suggest;
            modelKhoTB.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col2 = new AutoCompleteStringCollection();
            ModelHandler handler2 = new ModelHandler();
            List<Model.Model> list2 = handler2.layds();
            foreach (Model.Model ob in list2)
            {
                col2.Add(ob.ten);
            }
            modelKhoTB.AutoCompleteCustomSource = col2;

            seriKhoTB.AutoCompleteMode = AutoCompleteMode.Suggest;
            seriKhoTB.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col3 = new AutoCompleteStringCollection();
            SeriHandler handler3 = new SeriHandler();
            List<Model.Seri> list3 = handler3.layds();
            foreach (Model.Seri ob in list3)
            {
                col3.Add(ob.ten);
            }
            seriKhoTB.AutoCompleteCustomSource = col3;

        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Xemluocsu frm2 = new Xemluocsu();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void DataGridView1_CellMouseDown_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Thêm hiện trạng", themHienTrang_Click));
                m.MenuItems.Add(new MenuItem("Xem lịch sử", xemLichSu_Click));
                m.Show(dataGridView1, dataGridView1.
                    PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)));

            }
        }

        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            xemLichSu();
        }

        private void DataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            xemLichSuKho();
        }

        private void QuanrLyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vitrikho frm = new vitrikho();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog(this);
        }

        private void Xoasrbt_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                
                String value0 = row.Cells[0].Value.ToString();
                int x = 0;
                string serit = row.Cells[1].Value.ToString();
                string modelt = row.Cells[3].Value.ToString();
                string text = "bạn có chắc muỗn xóa seri: " + serit +"model: "+ modelt;
                Int32.TryParse(value0, out x);
                
                DialogResult dlr = MessageBox.Show(text,"Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if(dlr == DialogResult.OK)
                {
                    SeriHandler srh = new SeriHandler();
                    srh.deleteseri(x);
                    LichSuHandler lsh = new LichSuHandler();
                    lsh.xoafromseri(x);
                }
                
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            using(var conn = new NpgsqlConnection(connString))
            {
                using(var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Select * fom lichsu";
                    
                }
            }
        }

        

        private void QuảnLýLinhKiệnMáyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Linhkien frm = new Linhkien();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog(this);
            
        }

        private void quanToolStripMenuItem_Click(object sender, EventArgs e)
        {
             QuanLyNoiLay frm = new QuanLyNoiLay();

            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog(this);
        }
    }
}
