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


namespace Quanly
{
    public partial class Main : Form
    {
        String connString;
        String vaitro;
        String user;
        String valueLoaimayCB;
        Boolean initForm;
        List<Seri> listseribegin;
        List<Model.Model> listmodelbegin;
        List<Model.KhachHang> listkhachhangbegin;
        List<Model.LoaiMay> listloaimaybegin;
        public Main()

        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            connString = new Property().ConnString;
            // dangnhap();
            initForm = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);
            //setupLoaimay();
            //setupModel();
            //setupKhachhang();

            //updateseri();



        }

        private void updateseri()
        {
            
            var listseri = new List<Model.serinew>();
            var listRecore = new List<Model.Record>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select * from seri";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Model.serinew s = new Model.serinew();
                            s.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            s.ma = reader.GetString(reader.GetOrdinal("ma"));
                            s.ten = reader.GetString(reader.GetOrdinal("ten"));
                            s.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            s.mamodel = reader.GetString(reader.GetOrdinal("mamodel"));
                            s.mamay = reader.GetString(reader.GetOrdinal("mamay"));
                            s.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                            listseri.Add(s);
                            
                        }
                    }
                }
                conn.Close();
                
            }



            foreach (Model.serinew s in listseri)
            {

                /* foreach (Model.Model md in listmodel)
                 {


                     if (format(s.mamodel) == format(md.ma))
                     {
                         //s.tenmodel = md.ten;
                         //MessageBox.Show(s.mamodel, md.ten);

                         using (var conn = new NpgsqlConnection(connString))
                         {
                             conn.Open();
                             using (var cmd = new NpgsqlCommand())
                             {
                                 cmd.Connection = conn;
                                 cmd.CommandText = "UPDATE seri set tenmodel=@tenmodel where mamodel=@mamodel" ;
                                 cmd.Parameters.AddWithValue("tenmodel", md.ten);
                                 cmd.Parameters.AddWithValue("mamodel", s.mamodel);
                                 cmd.ExecuteNonQuery();
                                // MessageBox.Show(md.ten);
                             }
                             conn.Close();
                         }
                     }
                 }*/



                using (var conn = new NpgsqlConnection(connString))
                {
                    
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * from lichsu where keyseri=@pk and thoigian=(Select max(thoigian) from lichsu where keyseri=@pk) ";

                        cmd.Parameters.AddWithValue("pk", s.pk);


                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var r = new Model.Record();
                                r.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                                //r.keyseri = reader.GetInt32(reader.GetOrdinal("keyseri"));
                                //r.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                                //r.khacphuc = reader.GetString(reader.GetOrdinal("khacphuc"));
                                // r.phutrach = reader.GetString(reader.GetOrdinal("kysu"));
                                // r.ghichu = reader.GetString(reader.GetOrdinal("ghichu"));

                                /*if (reader.IsDBNull(reader.GetOrdinal("thoigian")))
                                {
                                    r.thoigian = null;
                                }
                                else
                                {
                                    r.thoigian = reader.GetDateTime(reader.GetOrdinal("thoigian"));
                                }*/
                                //r.thoigian = reader.GetDateTime(reader.GetOrdinal("thoigian"));


                                r.thoigian = reader.GetDateTime(reader.GetOrdinal("thoigian"));
                                //MessageBox.Show(r.thoigian.ToString("dd/MM/yyyy hh:mm:ss"));
                                listRecore.Add(r);
                            }
                        }
                    }
                    conn.Close();
                }

            }
            foreach (Model.serinew s in listseri)
            {
                
                foreach (Model.Record re in listRecore)
                 {
                   

                    if (s.pk == re.pk)
                     {
                        //MessageBox.Show(re.pk.ToString(), s.pk.ToString());
                        //s.tenmodel = md.ten;


                        using (var conn = new NpgsqlConnection(connString))
                        {
                            conn.Open();
                            using (var cmd = new NpgsqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandText = "UPDATE seri set currentTime=@currentTime where pk=@pk" ;
                                cmd.Parameters.AddWithValue("currentTime", re.thoigian);
                                cmd.Parameters.AddWithValue("pk", s.pk);
                                cmd.ExecuteNonQuery();
                               // MessageBox.Show(md.ten);
                            }
                            conn.Close();
                        }
                    }
                 }
            }

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

        private void setupLoaimay() {
            var loaimayCBList = new List<string>();
            loaimayCBList.Add("");

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from loaimay";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ten = reader.GetString(reader.GetOrdinal("ma"));
                            loaimayCBList.Add(ten);
                        }
                    }
                }
                LoaimayCB.DataSource = loaimayCBList;
                LoaimayCB.SelectedIndex = 0;
                conn.Close();
            }
        }


        private void setupModel()
        {
            var modelCBList = new List<string>();
            modelCBList.Add("");

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from model";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ten = reader.GetString(reader.GetOrdinal("ma"));
                            modelCBList.Add(ten);
                        }
                    }
                }
                modelCB.DataSource = modelCBList;
                modelCB.SelectedIndex = 0;
                conn.Close();
            }
        }

        private void setupKhachhang()
        {
            var khachhangCBList = new List<string>();
            khachhangCBList.Add("");

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from khachhang";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ten = reader.GetString(reader.GetOrdinal("ma"));
                            khachhangCBList.Add(ten);
                        }
                    }
                }
                khachhangCB.DataSource = khachhangCBList;
                khachhangCB.SelectedIndex = 0;
                conn.Close();
            }
        }

        private void reloadComboBox(string model, string khachhang, string mamay)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                var serilist = new List<string>();
                serilist.Add("");
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    var loaimayList = new List<string>();
                    cmd.Connection = conn;
                    cmd.CommandText = "select * from seri where (mamodel=@mamodel or @mamodel='') " +
                        "and (khachhang=@khachhang or @khachhang='') and (mamay=@mamay or @mamay='')";
                
                    cmd.Parameters.AddWithValue("mamodel", model);
                   
                    cmd.Parameters.AddWithValue("khachhang", khachhang);
                    
                    cmd.Parameters.AddWithValue("mamay", mamay);
                   // }
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ma = reader.GetString(reader.GetOrdinal("ma"));
                            serilist.Add(ma);
                            
                        }
                    }
                }
                
                seriCB.DataSource = serilist;
                if (serilist.Count == 0)
                {
                    seriCB.Text = "";

                }
                conn.Close();
                somay.Text = (serilist.Count-1).ToString();
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


        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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
                Xemluocsu frm2 = new Xemluocsu(x);
                frm2.StartPosition = FormStartPosition.CenterParent;
                frm2.ShowDialog(this);

            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //quang ly tai khoang
        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyTaiKhoan.QuanLyTaiKhoan frm2 = new QuanLyTaiKhoan.QuanLyTaiKhoan(user, vaitro);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }
        //đổi mật khẩu
        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Doimatkhau frm2 = new Doimatkhau(user);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }
        // đổi tài khoảng
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


        private void search()
        {
            //List<Model.serinew> list = new List<Model.serinew>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from seri where (ten=@ten or @ten='') and (mamay=@mamay or @mamay='') and (mamodel=@mamodel or @mamodel='') and (khachhang=@khachhang or @khachhang='')";

                    cmd.Parameters.AddWithValue("ten", format(seriCB.Text));
                    cmd.Parameters.AddWithValue("mamay", format(LoaimayCB.Text));
                    cmd.Parameters.AddWithValue("mamodel", format(modelCB.Text));
                    cmd.Parameters.AddWithValue("khachhang", format(khachhangCB.Text));
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            Model.serinew s = new Model.serinew();
                            s.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            s.ma = reader.GetString(reader.GetOrdinal("ma"));
                            s.ten = reader.GetString(reader.GetOrdinal("ten"));
                            s.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            s.tenmodel = reader.GetString(reader.GetOrdinal("tenmodel"));
                            s.tenmay = reader.GetString(reader.GetOrdinal("tenmay"));
                            s.tenkhachhang = reader.GetString(reader.GetOrdinal("tenkhachhang"));
                            s.ghichu = reader.GetString(reader.GetOrdinal("ghichu"));

                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                            if (reader.IsDBNull(reader.GetOrdinal("tenhientrang")))
                            {
                                s.tenhientrang = null;
                            }
                            else
                            {
                                s.tenhientrang = reader.GetString(reader.GetOrdinal("tenhientrang"));
                            }
                            if (reader.IsDBNull(reader.GetOrdinal("currentTime")))
                            {
                                s.currentTime = null;
                            }
                            else
                            {
                                s.currentTime = reader.GetDateTime(reader.GetOrdinal("currentTime"));
                            }


                            

                                dataGridView1.Rows.Add(s.pk, s.ma, s.ten, s.tenmodel, s.tenmay,
                                    s.tenkhachhang, "", s.currentTime,
                                    "", s.ghichu);
                            

                           // dataGridView1.Rows.Add(s.pk, s.ma, s.ten, s.tenmodel, s.tenmay,
                               //  s.tenkhachhang, "", s.ngaylapdat,
                               // s.tenhientrang, "");

                          
                        }
                }
                conn.Close();
            }
           // return list;
        }


        // nút tra cứu
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            search();
            /*SeriHandler sh = new SeriHandler();
            List<Seri> list = sh.tracuu(format(seriCB.Text));


            foreach (Seri s in list)
            {
                LoaiMayHandler lh = new LoaiMayHandler();
                LoaiMay m = lh.laythongtin(s.mamay, "");

               if (m.ten.Equals(LoaimayCB.Text) || LoaimayCB.Text.Equals(""))
                {
                    ModelHandler mh = new ModelHandler();
                    Model.Model mo = mh.laythongtin(s.mamodel, "", "");


                    if (format(mo.ma) == format(modelCB.Text) || modelCB.Text.Equals(""))
                    {
                        KhachHangHandler kh = new KhachHangHandler();
                        KhachHang k = kh.laythongtin(s.khachhang, "");

                       // MessageBox.Show(k.ten);
                        if (format(k.ma) == format(khachhangCB.Text) || khachhangCB.Text.Equals(""))
                        {
                            LichSuHandler lih = new LichSuHandler();
                            Record r = lih.traCuuGanNhat(s.pk);
                            if (r.pk == 0)
                            {
                                dataGridView1.Rows.Add(s.pk, s.ma, s.ten, mo.ten, m.ten,
                                    k.ten, "", "", "", "");
                            }
                            else
                            {
                                KySuHandler kih = new KySuHandler();
                                KySu kysu = kih.laythongtin(r.phutrach, "");

                                dataGridView1.Rows.Add(s.pk, s.ma, s.ten, mo.ten, m.ten,
                                    k.ten, kysu.ten, r.thoigian.ToString("dd/MM/yyyy hh:mm:ss"),
                                    r.hientrang, r.ghichu);
                            }

                        }
                    }
                }
            }*/




        }
        private string format(string s)
        {
            if (s == null) s = "";
            return s;
        }
        //nhấp đúp vào table cell
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            xemLichSu();
        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //quảng lý hiện trạng
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
        //xuất dữ liệu theo máy
        private void theoMáyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Xemluocsutheomay frm2 = new Xemluocsutheomay();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }
        //xuất dữ liệu theo ngày
        private void theoNgàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Xemluocsutheongay frm2 = new Xemluocsutheongay();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }
        //xuất dữ liệu theo seri
        private void theoSeriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Xemluocsu frm2 = new Xemluocsu();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }
        // nhập dữ liệu hàng loạt
        private void nhậpDữLiệuHàngLoạtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NhapDuLieuHangLoat frm2 = new NhapDuLieuHangLoat(user, vaitro);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void càiĐặtChungToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void LoaimayCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoaimayCB_SelectedValueChanged(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select * from loaimay where ma=@ma";
                    string loaimayCBstring = LoaimayCB.Text;
                    cmd.Parameters.AddWithValue("ma", loaimayCBstring);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string loaimaystring = reader.GetString(reader.GetOrdinal("ten"));

                            loaimayLB.Text = loaimaystring;

                        }
                    }
                }
                conn.Close();
            }
           reloadComboBox(modelCB.Text, khachhangCB.Text, LoaimayCB.Text);

        }

        
       

        

        private void ModelCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ModelCB_SelectedValueChanged(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select * from model where ma=@ma";
                    string modelstring = modelCB.Text;
                    cmd.Parameters.AddWithValue("ma", modelstring);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string modelten = reader.GetString(reader.GetOrdinal("ten"));
                            modelLB.Text = modelten;
                        }
                    }

                }
                conn.Close();
            }
            reloadComboBox(modelCB.Text, khachhangCB.Text, LoaimayCB.Text);
        }

        private void KhachhangCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void KhachhangCB_SelectedValueChanged(object sender, EventArgs e)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand() )
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select * from khachhang where ma=@ma";
                    string mastring = khachhangCB.Text;
                    cmd.Parameters.AddWithValue("ma", mastring);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           string tenmay = reader.GetString(reader.GetOrdinal("ten"));
                            khachhangLB.Text = tenmay;
                        }
                    }


                }
                
                conn.Close();
            }
            

            
                reloadComboBox(modelCB.Text, khachhangCB.Text, LoaimayCB.Text);

            

            //
        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void SeriCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SeriCB_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void FlowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PhutrachCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PhutrachCB_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}
