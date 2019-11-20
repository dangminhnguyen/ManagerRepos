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
        String connString ;
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
            layds();
           // capnhattong();

        }

        private void capnhattong()
        {
            SeriHandler srh = new SeriHandler();
            List<Model.Seri> list = srh.tracuu("");

            foreach (Model.Seri s in list)
            {

                LichSuHandler lih = new LichSuHandler();
                Record r = lih.traCuuGanNhat(s.pk);
                if (r.pk != 0)
                {
                    KySuHandler kih = new KySuHandler();
                    KySu kysu = kih.laythongtin(r.phutrach, "");

                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand())
                        {
                            cmd.Connection = conn;
                            // cmd.CommandText = "INSERT INTO tong (loaimay) VALUES (@loaimay) where @mamay=mamay";
                            cmd.CommandText = "UPDATE tong SET hientrang=@hientrang, phutrach=@phutrach,lichsu=@lichsu,thoigian=@thoigian where pk=@pk ";
                            cmd.Parameters.AddWithValue("hientrang", r.hientrang);
                            cmd.Parameters.AddWithValue("phutrach", kysu.ten);
                            cmd.Parameters.AddWithValue("lichsu", r.ghichu);
                            cmd.Parameters.AddWithValue("thoigian", r.thoigian);

                            cmd.Parameters.AddWithValue("pk", r.pk);
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();

                    }
                    //dataGridView1.Rows.Add(s.pk, s.ma, s.ten, mo.ten, m.ten,
                    // k.ten, kysu.ten, r.thoigian.ToString("dd/MM/yyyy hh:mm:ss"),
                    // r.hientrang, r.ghichu);


                }
               
                    

                    
               

                
                    
                       
                       /* using (var conn = new NpgsqlConnection(connString))
                        {
                            conn.Open();
                            using (var cmd = new NpgsqlCommand())
                            {
                                cmd.Connection = conn;
                               // cmd.CommandText = "INSERT INTO tong (loaimay) VALUES (@loaimay) where @mamay=mamay";
                                cmd.CommandText = "UPDATE tong SET khachhang=@khachhang where makhachhang=@makhachhang ";
                                cmd.Parameters.AddWithValue("khachhang", m.ten);
                            
                                cmd.Parameters.AddWithValue("makhachhang", s.khachhang);
                                cmd.ExecuteNonQuery();
                            }
                            conn.Close();

                        }*/
                    


                    
              


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

            
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {           
                dataGridView1.Rows[e.RowIndex].Selected = true;
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Thêm hiện trạng" ,themHienTrang_Click));
                m.MenuItems.Add(new MenuItem("Xem lịch sử",xemLichSu_Click));
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
                //MessageBox.Show(value0);
                Xemluocsu frm2 = new Xemluocsu(x);
                //frm2.StartPosition = FormStartPosition.CenterParent;
                //frm2.ShowDialog(this);

            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyTaiKhoan.QuanLyTaiKhoan frm2 = new QuanLyTaiKhoan.QuanLyTaiKhoan(user,vaitro);
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
                    cmd.CommandText = "SELECT * from tong where (seri=@seri or @seri='') and (mamay=@mamay or @mamay='') and (mamodel=@mamodel or @mamodel='') and (makhachhang=@makhachhang or @makhachhang='')";

                    cmd.Parameters.AddWithValue("seri", format(textBox3.Text));
                    cmd.Parameters.AddWithValue("mamay", format(textBox1.Text));
                    cmd.Parameters.AddWithValue("mamodel", format(textBox2.Text));
                    cmd.Parameters.AddWithValue("makhachhang", format(textBox4.Text));
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            Model.tong s = new Model.tong();
                            s.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            s.seri = reader.GetString(reader.GetOrdinal("seri"));
                            s.mamay = reader.GetString(reader.GetOrdinal("mamay"));
                            s.loaimay = reader.GetString(reader.GetOrdinal("loaimay"));
                            s.mamodel = reader.GetString(reader.GetOrdinal("mamodel"));
                            s.model = reader.GetString(reader.GetOrdinal("model"));
                            s.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            s.makhachhang = reader.GetString(reader.GetOrdinal("makhachhang"));
                            
                            
                            /*if (reader.IsDBNull(reader.GetOrdinal("lichsu")))
                            {
                                s.lichsu = null;
                            }
                            else
                            {
                                s.lichsu = reader.GetString(reader.GetOrdinal("lichsu"));
                            }

                            if (reader.IsDBNull(reader.GetOrdinal("phutrach")))
                            {
                                s.phutrach = null;
                            }
                            else
                            {
                                s.phutrach = reader.GetString(reader.GetOrdinal("phutrach"));
                            }

                            if (reader.IsDBNull(reader.GetOrdinal("hientrang")))
                            {
                                s.hientrang = null;
                            }
                            else
                            {
                                s.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            }
                            if (reader.IsDBNull(reader.GetOrdinal("thoigian")))
                            {
                                s.thoigian = null;
                            }
                            else
                            {
                                s.thoigian = reader.GetDateTime(reader.GetOrdinal("thoigian"));
                            }
                            */



                           dataGridView1.Rows.Add(s.pk, s.seri, s.seri, s.model, s.loaimay, s.khachhang, "", "","", "");


                            // dataGridView1.Rows.Add(s.pk, s.ma, s.ten, s.tenmodel, s.tenmay,
                            //  s.tenkhachhang, "", s.ngaylapdat,
                            // s.tenhientrang, "");


                        }
                }
                conn.Close();
            }
            // return list;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            search();
           /* SeriHandler sh = new SeriHandler();
            List<Seri> list = sh.tracuu(format(textBox3.Text));
            foreach (Seri s in list)
            {
                LoaiMayHandler lh = new LoaiMayHandler();
                LoaiMay m = lh.laythongtin(s.mamay, "");
                if (m.ten.Equals(textBox1.Text) || textBox1.Text.Equals("")){
                    ModelHandler mh = new ModelHandler();
                    Model.Model mo = mh.laythongtin(s.mamodel, "", "");
                    if(mo.ten.Equals(textBox2.Text) || textBox2.Text.Equals(""))
                    {
                        KhachHangHandler kh = new KhachHangHandler();
                        KhachHang k = kh.laythongtin(s.khachhang, "");
                        if (k.ten.Equals(textBox4.Text) || textBox4.Text.Equals(""))
                        {
                            LichSuHandler lih = new LichSuHandler();
                            Record r= lih.traCuuGanNhat(s.pk);
                            if (r.pk == 0)
                            {
                                dataGridView1.Rows.Add(s.pk, s.ma, s.ten, mo.ten, m.ten,
                                    k.ten, "","","","");
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
            frm2.StartPosition= FormStartPosition.CenterParent;
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
            NhapDuLieuHangLoat frm2 = new NhapDuLieuHangLoat(user,vaitro);
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
        }

        private void càiĐặtChungToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
