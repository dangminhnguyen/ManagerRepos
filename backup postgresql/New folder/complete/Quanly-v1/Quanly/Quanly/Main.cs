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
        Seri s = new Seri();
        int pk;
        public Dictionary<string, string> modelsource = new Dictionary<string, string>();
        public Dictionary<string , string> loaimaysource = new Dictionary<string, string>();
        public Dictionary<string, string> khachhangsource = new Dictionary<string, string>();
        
        public Main()
        
        {
            InitializeComponent();           
            this.WindowState = FormWindowState.Maximized;
            connString = new Property().ConnString;
           // dangnhap();
            
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);
            layds();
            getdsmay();
            getdsmodel();
            getdsseri();
            //updatetong();
            setup();
        }

        private void setup()
        {
            LoaiMayHandler lh = new LoaiMayHandler();
            List<LoaiMay> ls = lh.layds();
            foreach (Model.LoaiMay lm in ls)
            {
                loaimaysource.Add(lm.ma, lm.ten);
                //MessageBox.Show(loaimaysource[lm.ma]);
            }

            ModelHandler mo = new ModelHandler();
            List<Model.Model> mo1 = mo.layds();
            foreach (Model.Model m in mo1)
            {
                modelsource.Add(m.ma, m.ten);
            }


        }



        public void getdsmay()
        {
            loaimayCB.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            LoaiMayHandler lh = new LoaiMayHandler();
            List<LoaiMay> ls = lh.layds();
            foreach (LoaiMay lm in ls)
            {
                combosource.Add(lm.ma, lm.ma + " - " + lm.ten);
            }
            loaimayCB.DataSource = new BindingSource(combosource, null);
            loaimayCB.DisplayMember = "Value";
            loaimayCB.ValueMember = "Key";
            loaimayCB.SelectedIndex = 0;
        }

        public void getdsmodel()
        {
            string mamay = ((KeyValuePair<string, string>)loaimayCB.SelectedItem).Key;
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
            modelCB.DataSource = new BindingSource(combosource, null);
            modelCB.DisplayMember = "Value";
            modelCB.ValueMember = "Key";
            modelCB.SelectedIndex = 0;
        }

        public void getdsseri()
        {
            //comboBox2.DataSource = null;

            string mamay = ((KeyValuePair<string, string>)loaimayCB.SelectedItem).Key;
            string mamodel = ((KeyValuePair<string, string>)modelCB.SelectedItem).Key;
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            SeriHandler lh = new SeriHandler();
            List<Model.Seri> ls = lh.layds(mamay, mamodel);
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


            seriCB.DataSource = new BindingSource(combosource, null);
            seriCB.DisplayMember = "Value";
            seriCB.ValueMember = "Key";
            seriCB.SelectedIndex = 0;
        }


        public void gettenkh(int x)
        {
            SeriHandler sh = new SeriHandler();

            s = sh.laythongtin(x);
            KhachHangHandler kh = new KhachHangHandler();
            KhachHang k = kh.laythongtin(s.khachhang, "");
            //text.Text = k.ten;
            List<string> l = new List<string>();
            l.Add(k.ten);
            khachhangCB.DataSource = l;
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
                //MessageBox.Show(x.ToString());
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

        private List<Model.Record> getlichsu()
        {
            SeriHandler sh = new SeriHandler();
            List<Seri> list = sh.tracuu("");
            LichSuHandler ls = new LichSuHandler();
            List<Model.Record> r = new List<Model.Record>();
            foreach (Seri s in list)
            {
                Model.Record re = ls.traCuuGanNhat(s.pk);
                r.Add(re);
            }
            return r;
        }

        private void updatetong()
        {
            List<Model.Record> r = new List<Model.Record>();
            r = getlichsu();
            TongHandler tong = new TongHandler();
            List<tong> tlist = tong.layds();

            foreach (Model.Record re in r)
            {
                if (re.keyseri != 0)
                {
                    foreach (Model.tong to in tlist)
                    {
                        if (re.keyseri == to.keyseri)
                        {
                            
                            //MessageBox.Show(re.keyseri.ToString(), re.ghichu);
                            KySuHandler kih = new KySuHandler();
                            KySu kysu = kih.laythongtin(re.phutrach, "");

                            //dataGridView1.Rows.Add(s.pk, s.ma, s.ten, mo.ten, m.ten,
                            //k.ten, kysu.ten, r.thoigian.ToString("dd/MM/yyyy hh:mm:ss"),
                            //r.hientrang, r.ghichu);

                            using (var conn = new NpgsqlConnection(connString))
                            {
                                conn.Open();
                                using (var cmd = new NpgsqlCommand())
                                {
                                    cmd.Connection = conn;
                                    cmd.CommandText = "Update tong set phutrach=@phutrach,thoigian=@thoigian,lichsu=@lichsu,hientrang=@hientrang where keyseri=@keyseri";
                                    cmd.Parameters.AddWithValue("phutrach", kysu.ten);
                                    cmd.Parameters.AddWithValue("thoigian", re.thoigian);
                                    cmd.Parameters.AddWithValue("lichsu", re.ghichu);
                                    cmd.Parameters.AddWithValue("hientrang", re.hientrang);
                                    cmd.Parameters.AddWithValue("keyseri", re.keyseri);
                                    cmd.ExecuteNonQuery();

                                }
                                conn.Close();
                            }

                        }

                    }

                }

            }
            
        }



        private void copytotong()
        {
            SeriHandler sh = new SeriHandler();
            List<Seri> list = sh.tracuu("");
            MessageBox.Show(list.Count().ToString());
            foreach (Seri s in list)
            {
                //MessageBox.Show(s.pk.ToString());
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO tong (keyseri,seri,mamodel,mamay,makhachhang) VALUES (@keyseri,@seri,@mamodel,@mamay,@makhachhang)";
                        cmd.Parameters.AddWithValue("keyseri", s.pk);
                        cmd.Parameters.AddWithValue("mamodel", s.mamodel);
                        cmd.Parameters.AddWithValue("seri", s.ma);
                        cmd.Parameters.AddWithValue("mamay", s.mamay);
                        cmd.Parameters.AddWithValue("makhachhang", s.khachhang);

                        cmd.ExecuteNonQuery();

                    }
                    conn.Close();
                }

            }



        }

        private void getseri()
        {
            SeriHandler sh = new SeriHandler();
            List<Seri> list = sh.tracuu("");
            ModelHandler mo = new ModelHandler();
            List < Model.Model > listmo = mo.layds();
            LoaiMayHandler lm = new LoaiMayHandler();
            List<Model.LoaiMay> listlm = lm.layds();
            KhachHangHandler kh = new KhachHangHandler();
            List < Model.KhachHang > listkh = kh.layds();

            string tenmodel;
            string tenmay;
            string tenkhachhang;
            foreach (Seri s in list)
            {
                /*foreach (Model.Model m in listmo)
                {
                    if (s.mamodel == m.ma)
                    {
                        
                        tenmodel = m.ten;

                        using (var conn = new NpgsqlConnection(connString))
                        {
                            conn.Open();
                            using (var cmd = new NpgsqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandText = "Update tong set model=@model where mamodel=@mamodel";
                                cmd.Parameters.AddWithValue("model", tenmodel);
                                cmd.Parameters.AddWithValue("mamodel", s.mamodel);
                                cmd.ExecuteNonQuery();

                            }
                            conn.Close();
                        }

                    }
                }
                foreach (Model.LoaiMay l in listlm)
                {
                    if (s.mamay == l.ma)
                    {
                        tenmay = l.ten;

                        using (var conn = new NpgsqlConnection(connString))
                        {
                            conn.Open();
                            using (var cmd = new NpgsqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandText = "Update tong set loaimay=@loaimay where mamay=@mamay";
                                cmd.Parameters.AddWithValue("loaimay", tenmay);
                                cmd.Parameters.AddWithValue("mamay", s.mamay);
                                cmd.ExecuteNonQuery();

                            }
                            conn.Close();
                        }
                    }
                }*/
                foreach (Model.KhachHang k in listkh)
                {
                    if (s.khachhang == k.ma)
                    {
                        tenkhachhang = k.ten;
                        using (var conn = new NpgsqlConnection(connString))
                        {
                            conn.Open();
                            using (var cmd = new NpgsqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandText = "Update tong set khachhang=@khachhang where makhachhang=@makhachhang";
                                cmd.Parameters.AddWithValue("khachhang", tenkhachhang);
                                cmd.Parameters.AddWithValue("makhachhang", s.khachhang);
                                cmd.ExecuteNonQuery();

                            }
                            conn.Close();
                        }

                    }
                }

                



            }




        }
        private void search()
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from tong where (seri=@seri or @seri='')" +
                        " and (model=@model or @model='') and (loaimay=@loaimay or @loaimay='') and (khachhang=@khachhang or @khachhang='')";
                    cmd.Parameters.AddWithValue("seri", format(textBox3.Text));
                    cmd.Parameters.AddWithValue("khachhang", format(textBox4.Text));
                    cmd.Parameters.AddWithValue("model", format(textBox2.Text));
                    cmd.Parameters.AddWithValue("loaimay", format(textBox1.Text));
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int pk = reader.GetInt32(reader.GetOrdinal("keyseri"));
                            String seri1 = reader.GetString(reader.GetOrdinal("seri"));
                            String mamodel1 = null;
                            if (reader.IsDBNull(reader.GetOrdinal("model")))
                            {
                                mamodel1 = null;
                            }
                            else
                            {
                                mamodel1 = reader.GetString(reader.GetOrdinal("model"));
                            }

                           
                            String mamay1 = reader.GetString(reader.GetOrdinal("loaimay"));
                            String khachhang = reader.GetString(reader.GetOrdinal("khachhang"));

                            string phutrach1 = null;
                            if (reader.IsDBNull(reader.GetOrdinal("phutrach")))
                            {
                                phutrach1 = null;
                            }
                            else
                            {
                                phutrach1 = reader.GetString(reader.GetOrdinal("phutrach"));
                            }

                            string hientrang1 = null;
                            if (reader.IsDBNull(reader.GetOrdinal("hientrang")))
                            {
                                hientrang1 = null;
                            }
                            else
                            {
                                hientrang1 = reader.GetString(reader.GetOrdinal("hientrang"));
                            }
                            string lichsu1 = null;
                            if (reader.IsDBNull(reader.GetOrdinal("lichsu")))
                            {
                                lichsu1 = null;
                            }
                            else
                            {
                                lichsu1 = reader.GetString(reader.GetOrdinal("lichsu"));
                            }
                            DateTime? thoigian1 = null;
                            if (reader.IsDBNull(reader.GetOrdinal("thoigian")))
                            {
                                thoigian1 = null;
                            }
                            else
                            {
                                thoigian1 = reader.GetDateTime(reader.GetOrdinal("thoigian"));
                            }


                            dataGridView1.Rows.Add(pk, seri1, seri1, mamodel1, mamay1,
                                     khachhang, phutrach1, thoigian1.ToString(), hientrang1, lichsu1);
                        }
                    }

                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            search();
            /*SeriHandler sh = new SeriHandler();
            List<Seri> list = sh.tracuu(format(textBox3.Text));
            foreach (Seri s in list)
            {
                LoaiMayHandler lh = new LoaiMayHandler();
                LoaiMay m = lh.laythongtin(s.mamay, "");
                if ( format(m.ten)== format(textBox1.Text) || format(textBox1.Text) == ""){
                    ModelHandler mh = new ModelHandler();
                    Model.Model mo = mh.laythongtin(s.mamodel, "", "");
                    if(format(mo.ten) ==  format(textBox2.Text) || format(textBox2.Text) == "")
                    {
                        KhachHangHandler kh = new KhachHangHandler();
                        KhachHang k = kh.laythongtin(s.khachhang, "");
                        if (format(m.ten)==format(textBox4.Text) || format(textBox4.Text) == "")
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

        private void KhachhangCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void KhachhangCB_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void LoaimayCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoaimayCB_SelectedValueChanged(object sender, EventArgs e)
        {
            getdsmodel();
        }

        private void ModelCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ModelCB_SelectedValueChanged(object sender, EventArgs e)
        {
            getdsseri();
        }

        private void SeriCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SeriCB_SelectedValueChanged(object sender, EventArgs e)
        {
            string maseri = ((KeyValuePair<string, string>)seriCB.SelectedItem).Key;
            int x = 0;
            Int32.TryParse(maseri, out x);
            pk = x;
            gettenkh(x);
        }
    }
}
