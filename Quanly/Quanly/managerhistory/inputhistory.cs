﻿using System;
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
namespace Quanly.managerhistory
{
    public partial class inputhistory : Form
    {
        public int keyseri { get; set;}
        public int pk { get; set; }
        public inputhistory()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            if (tralaicheckBox.Checked)
            {
                groupBox5.Enabled = false;
                button4.Enabled = false;
                groupBox4.Enabled = false;
            }
            else
            {
                groupBox5.Enabled = true;
                button4.Enabled = true;
                groupBox4.Enabled = true;
            }

            if(fullmaycheckBox.Checked) malkCB.Enabled = false;
            else malkCB.Enabled = true;

            datelay.Format = DateTimePickerFormat.Custom;
            datelay.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            datelay.Value = DateTimePicker.MinimumDateTime;

            datePH.Format = DateTimePickerFormat.Custom;
            datePH.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            datePH.Value = DateTimePicker.MinimumDateTime;

            //getModel();
           
        }
        public inputhistory(int x)
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;

            this.keyseri = x;
            if (tralaicheckBox.Checked)
            {
                groupBox5.Enabled = false;
                button4.Enabled = false;
                groupBox4.Enabled = false;
            }
            else
            {
                groupBox5.Enabled = true;
                button4.Enabled = true;
                groupBox4.Enabled = true;
            }

            if (fullmaycheckBox.Checked) malkCB.Enabled = false;
            else malkCB.Enabled = true;
            datelay.Format = DateTimePickerFormat.Custom;
            datelay.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            datelay.Value = DateTimePicker.MinimumDateTime;

            datePH.Format = DateTimePickerFormat.Custom;
            datePH.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            datePH.Value = DateTimePicker.MinimumDateTime;

            dateFrom.Format = DateTimePickerFormat.Custom;
            dateFrom.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            dateFrom.Value = DateTimePicker.MinimumDateTime;

            dateTo.Format = DateTimePickerFormat.Custom;
            dateTo.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            dateTo.Value = DateTime.Now;



            SeriHandler sh = new SeriHandler();
            Seri s = sh.laythongtin(x);
            getdsmay();
            loaimayCB.SelectedValue = s.mamay;
            getdsmodel();
            modelCB.SelectedValue = s.mamodel;
            getdsseri();
            seriCB.SelectedValue = x.ToString();
            TraCuu();
            getKysulay();
            getKysuph();
            getHientrang();
            getKhachhang();
            getNoiLay();
            getInitDataGridView(0);
            tracuulinhkien();
        }
        
        private void getNoiLay()
        {
            vitrilaymCB.Items.Clear();
            vtlapphCB.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            NoiLayPTHandler nlh = new NoiLayPTHandler();
            List<NoiLayPT> lnl = nlh.layds();
            combosource.Add("",null);
            foreach(NoiLayPT nl in lnl)
            {
                combosource.Add(nl.ma,nl.ten);
            }
            vitrilaymCB.DataSource = new BindingSource(combosource, null);
            vitrilaymCB.DisplayMember = "Value";
            vitrilaymCB.ValueMember = "Key";
            vitrilaymCB.SelectedIndex = 0;

            vtlapphCB.DataSource = new BindingSource(combosource, null);
            vtlapphCB.DisplayMember = "Value";
            vtlapphCB.ValueMember = "Key";
            vtlapphCB.SelectedIndex = 0;


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
          
            string mamay = ((KeyValuePair<string, string>)loaimayCB.SelectedItem).Key;
            string mamodel = ((KeyValuePair<string, string>)modelCB.SelectedItem).Key;
            Dictionary<string, string> combosource = new Dictionary<string, string>();

            SeriHandler lh = new SeriHandler();
            List<Model.Seri> ls = lh.layds(mamay, mamodel);
            combosource.Add("", null);
            foreach (Model.Seri lm in ls)
            {
                combosource.Add(lm.pk.ToString(),lm.ten);
            }
            seriCB.DataSource = new BindingSource(combosource, null);
            seriCB.DisplayMember = "Value";
            seriCB.ValueMember = "Key";
            seriCB.SelectedIndex = 0;

            serilapCB.DataSource = new BindingSource(combosource, null);
            serilapCB.DisplayMember = "Value";
            serilapCB.ValueMember = "Key";
            serilapCB.SelectedIndex = 0;


            srlayphCB.DataSource = new BindingSource(combosource, null);
            srlayphCB.DisplayMember = "Value";
            srlayphCB.ValueMember = "Key";
            srlayphCB.SelectedIndex = 0;
        }
        private void getKhachhang()
        {
            khachhangCB.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            KhachHangHandler khhd = new KhachHangHandler();
            List<Model.KhachHang> khl = new List<Model.KhachHang>();
            khl = khhd.layds();
            combosource.Add("", null);
            foreach (Model.KhachHang kh in khl)
            {
                combosource.Add(kh.ma, kh.ten);
            }
            khachhangCB.DataSource = new BindingSource(combosource, null);
            khachhangCB.DisplayMember = "Value";
            khachhangCB.ValueMember = "Key";
            khachhangCB.SelectedIndex = 0;
        }

        private void getKysulay()
        {
            kslayCB.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            KySuHandler ksh = new KySuHandler();
            List<Model.KySu> lks = new List<KySu>();
            lks = ksh.layds();
            combosource.Add("", null);
            foreach (Model.KySu ks in lks)
            {
                combosource.Add(ks.ma, ks.ten);
            }
            kslayCB.DataSource = new BindingSource(combosource, null);
            kslayCB.DisplayMember = "Value";
            kslayCB.ValueMember = "Key";
            kslayCB.SelectedIndex = 0;
        }
        private void getKysuph()
        {
            ksphCB.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            KySuHandler ksh = new KySuHandler();
            List<Model.KySu> lks = new List<KySu>();
            lks = ksh.layds();
            combosource.Add("", null);
            foreach (Model.KySu ks in lks)
            {
                combosource.Add(ks.ma, ks.ten);
            }
            ksphCB.DataSource = new BindingSource(combosource, null);
            ksphCB.DisplayMember = "Value";
            ksphCB.ValueMember = "Key";
            ksphCB.SelectedIndex = 0;
        }

        private void getLinhkien(string model)
        {
            //malkCB.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            LinhKienHandle lkh = new LinhKienHandle();
            List<Model.linhkien> llk = new List<linhkien>();
            llk = lkh.layds(model);
            combosource.Add("", null);
            
            foreach (Model.linhkien lk in llk)
            {
               combosource.Add(lk.ma, lk.ten);
            }
            malkCB.DataSource = new BindingSource(combosource, null);
            malkCB.DisplayMember = "Value";
            malkCB.ValueMember = "Key";
            malkCB.SelectedIndex = 0;
        }
        private void Inputhistory_Load(object sender, EventArgs e)
        {

        }

        private void TralaicheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (tralaicheckBox.Checked)
            {
                
                button4.Enabled = false;
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;
                vitrilaymCB.SelectedIndex = 0;
                srlayphCB.SelectedIndex = 0;
                tinhtranglapCB.SelectedIndex = 0;
                vtlapphCB.SelectedIndex = 0;
                serilapCB.SelectedIndex = 0;
                ghichulaptxt.Text = "";
            }
            else
            {
                groupBox5.Enabled = true;
                button4.Enabled = true;
                groupBox4.Enabled = true;
            }
        }

        private void FullmaycheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fullmaycheckBox.Checked)
            {
                malkCB.Enabled = false;
                malkCB.SelectedIndex = 0;
            } 
            else malkCB.Enabled = true;
        }

        private void khachhangCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void modelCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdsseri();
            string model = ((KeyValuePair<string, string>)modelCB.SelectedItem).Key;
            getLinhkien(model);
        }

        private void seriCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void loaimayCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdsmodel();
        }

        private void vitrilaymCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vitrilay = vitrilaymCB.Text;
            if(vitrilay == "Giám đốc")
            {
                groupBox6.Enabled = false;
            }
            else
            {
                groupBox6.Enabled = true;
            }

        }

        private void vtlapphCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vitriph = vtlapphCB.Text;
            if (vitriph == "Giám đốc")
            {
                groupBox7.Enabled = false;
            }else
            {
                groupBox7.Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void getHientrang()
        {
            HienTrangHandler hth = new HienTrangHandler();
            List<Model.HienTrang> lht = new List<Model.HienTrang>();
            lht = hth.layds();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            combosource.Add("",null);
            foreach(Model.HienTrang ht in lht)
            {
                string ma = ht.ma.ToString();
                combosource.Add(ma, ht.ten);
            }
            tinhtranglayphCB.DataSource = new BindingSource(combosource, null);
            tinhtranglayphCB.DisplayMember = "Value";
            tinhtranglayphCB.ValueMember = "Key";
            tinhtranglayphCB.SelectedIndex = 0;
            //////////
            tinhtranglapCB.DataSource = new BindingSource(combosource, null);
            tinhtranglapCB.DisplayMember = "Value";
            tinhtranglapCB.ValueMember = "Key";
            tinhtranglapCB.SelectedIndex = 0;
            ////
            tinhtranglayCB.DataSource = new BindingSource(combosource, null);
            tinhtranglayCB.DisplayMember = "Value";
            tinhtranglayCB.ValueMember = "Key";
            tinhtranglayCB.SelectedIndex = 0;



        }

        private void ADD_Click(object sender, EventArgs e)
        {
            int keyseri = this.keyseri;

            string khachhang = ((KeyValuePair<string, string>)khachhangCB.SelectedItem).Key;

            string lklay = null;
            if (fullmaycheckBox.Checked) lklay = "";
            else lklay = ((KeyValuePair<string, string>)malkCB.SelectedItem).Key;

            string kslay = ((KeyValuePair<string, string>)kslayCB.SelectedItem).Key;
            string lydolay = lydolaytxt.Text;
            DateTime ngaylay = datelay.Value;
            string ttrlay = ((KeyValuePair<string, string>)tinhtranglayCB.SelectedItem).Key;
            DateTime ngayPh = datePH.Value;
            string ksPh = ((KeyValuePair<string, string>)ksphCB.SelectedItem).Key;
            string ttrPhLay = ((KeyValuePair<string, string>)tinhtranglayphCB.SelectedItem).Key; 
            string ghichuphlay = ghichutxt.Text;

            string srmayphlap = "";
            string vtrmoiPhlay = "";///
            
            string ttrphlap = "";///
            string srmayphlay = "";
            string xuatsu = "";
            string ghichuphlap = "";
            if (!tralaicheckBox.Checked)
            {
                vtrmoiPhlay = ((KeyValuePair<string, string>)vitrilaymCB.SelectedItem).Key;
                if (vtrmoiPhlay == "GĐ") srmayphlap = "";
                else srmayphlap = ((KeyValuePair<string, string>)srlayphCB.SelectedItem).Key;

                xuatsu = ((KeyValuePair<string, string>)vtlapphCB.SelectedItem).Key;
                if (xuatsu == "GĐ") srmayphlay = "";
                else srmayphlay = ((KeyValuePair<string, string>)serilapCB.SelectedItem).Key;

                ttrphlap = ((KeyValuePair<string, string>)tinhtranglapCB.SelectedItem).Key; 
                ghichuphlap = ghichulaptxt.Text;
            }
            
            LichsumoiHandler lsm = new LichsumoiHandler();
            lsm.them(this.keyseri, khachhang, lklay, kslay, lydolay, ngaylay, ttrlay, ngayPh, ksPh, ttrPhLay,
               srmayphlap, vtrmoiPhlay, ghichuphlay, ttrphlap, srmayphlay, xuatsu, ghichuphlap);
            TraCuu();
            if(lklay != null && lklay != "")
            {
                ThongkeLKHandler tkh = new ThongkeLKHandler();
                bool check = tkh.kiemtra(this.keyseri, lklay);
                if (check)
                {
                    tkh.sua(this.keyseri, lklay, ttrPhLay, srmayphlap, vtrmoiPhlay, srmayphlay, xuatsu, ttrphlap);
                }
                else
                {
                    tkh.them(this.keyseri, lklay, ttrPhLay, srmayphlap, vtrmoiPhlay, srmayphlay, xuatsu, ttrphlap);
                }
            }

            
            


        }


        private void capnhatlinhkiencuamay()
        {
            ThongkeLKHandler tkh = new ThongkeLKHandler();

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            
        }
        private void TraCuu()
        {
            List<RecordNew> list = new List<RecordNew>();
            LichsumoiHandler lh = new LichsumoiHandler();

            list = lh.tracuu(this.keyseri, dateFrom.Value, dateTo.Value);
            if(list.Count != 0)
            {
                
            }

            KhachHangHandler khhd = new KhachHangHandler();
            LinhKienHandle lkhd = new LinhKienHandle();
            KySuHandler kshd = new KySuHandler();
            HienTrangHandler htrhd = new HienTrangHandler();
            NoiLayPTHandler nlhd = new NoiLayPTHandler();
            SeriHandler srhd = new SeriHandler();
            dataGridView1.Rows.Clear();
            foreach (RecordNew r in list)
            {
                KhachHang khhg = khhd.laythongtin(r.khachhang, "");
                string model = ((KeyValuePair<string, string>)modelCB.SelectedItem).Key;
                linhkien lk = lkhd.laythongtin(model, r.lklay, "");
                KySu ksl1 = kshd.laythongtin(r.kslay, "");

                int xttrlay = 0;
                Int32.TryParse(r.ttrlay, out xttrlay);
                HienTrang ttrlay1 = htrhd.laythongtin(xttrlay,"");

                KySu ksPH1 = kshd.laythongtin(r.ksPh, "");
                int xttrPhLay = 0;
                Int32.TryParse(r.ttrPhLay, out xttrPhLay);
                HienTrang ttrPhLay1 = htrhd.laythongtin(xttrPhLay, "");

                NoiLayPT vtrmoiPhlay1 = nlhd.laythongtin(r.vtrmoiPhlay, "");

                int xttrphlap = 0;
                Int32.TryParse(r.ttrphlap, out xttrphlap);
                HienTrang ttrphlap1 = htrhd.laythongtin(xttrphlap, "");

                NoiLayPT xuatsu1 = nlhd.laythongtin(r.xuatsu, "");
                int xsrmayphlap = 0;
                Int32.TryParse(r.srmayphlap, out xsrmayphlap);
                Seri sr1 = srhd.laythongtin(xsrmayphlap);
                int xsrmayphlay = 0;
                Int32.TryParse(r.srmayphlay, out xsrmayphlay);
                Seri sr2 = srhd.laythongtin(xsrmayphlay);

                dataGridView1.Rows.Add(r.pk,r.keyseri, khhg.ten, ksl1.ten, lk.ten, r.ngaylay.ToString("dd/MM/yyyy hh:mm:ss"), r.lydolay,
                        ttrlay1.ten, ksPH1.ten, r.ngayPh.ToString("dd/MM/yyyy hh:mm:ss")
                       ,  ttrPhLay1.ten, sr1.ten, vtrmoiPhlay1.ten, r.ghichuphlay, xuatsu1.ten, sr2.ten
                       , ttrphlap1.ten, r.ghichuphlap); 
          
            }
            dataGridView1.Rows.Add();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void getInitDataGridView(int index)
        {
            

            if(dataGridView1.Rows[index].Cells[0].Value != null)
            {
                string pkstr = dataGridView1.Rows[index].Cells[0].Value.ToString();
                int pk = 0;
                Int32.TryParse(pkstr, out pk);
                this.pk = pk;
                
            }
            
            if(dataGridView1.Rows[index].Cells[1].Value != null)
            {
                string keyseristr = dataGridView1.Rows[index].Cells[0].Value.ToString();
                int keyseri = 0;
                Int32.TryParse(keyseristr, out keyseri);
            }

            //-------
            KhachHangHandler khhd = new KhachHangHandler();
            if (dataGridView1.Rows[index].Cells[2].Value != null)
            {
                string khachhang = dataGridView1.Rows[index].Cells[2].Value.ToString();

                KhachHang kh = khhd.laythongtin("", khachhang);
                khachhangCB.SelectedValue = kh.ma;
            }
            KySuHandler khs = new KySuHandler();
            if (dataGridView1.Rows[index].Cells[3].Value != null)
            {
                string kslay = dataGridView1.Rows[index].Cells[3].Value.ToString();

                KySu ks = khs.laythongtin("", kslay);
                kslayCB.SelectedValue = ks.ma;
            }
            else kslayCB.SelectedIndex = 0;
            //-------
            if (dataGridView1.Rows[index].Cells[4].Value != null)
            {
                string lklay = dataGridView1.Rows[index].Cells[4].Value.ToString();

                LinhKienHandle lkhd = new LinhKienHandle();
                string model = ((KeyValuePair<string, string>)modelCB.SelectedItem).Key;
                linhkien lk = lkhd.laythongtin(model, "", lklay);
                malkCB.SelectedValue = lk.ma;
                fullmaycheckBox.Checked = false;
            }
            else
            {
                malkCB.SelectedIndex = 0;
                fullmaycheckBox.Checked = true;
            }

            ///

            if (dataGridView1.Rows[index].Cells[5].Value != null)
            {
                string ngaylaystr = dataGridView1.Rows[index].Cells[5].Value.ToString();
                DateTime ngaylay = DateTime.ParseExact(ngaylaystr, "dd/MM/yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                datelay.Value = ngaylay;
            }
            else datelay.Value = DateTimePicker.MinimumDateTime;
            ////
            if (dataGridView1.Rows[index].Cells[6].Value != null)
            {
                string lydolay = dataGridView1.Rows[index].Cells[6].Value.ToString();
                lydolaytxt.Text = lydolay;
            }
            else
            {
                lydolaytxt.Text = "";
            }

            
            
            HienTrangHandler hthd = new HienTrangHandler();
            if (dataGridView1.Rows[index].Cells[7].Value != null)
            {
                string ttrlay = dataGridView1.Rows[index].Cells[7].Value.ToString();

                HienTrang ht = hthd.laythongtin(1, ttrlay);
                string mastr = ht.ma.ToString();
                tinhtranglayCB.SelectedValue = mastr;
            }
            else tinhtranglayCB.SelectedIndex = 0;

            if (dataGridView1.Rows[index].Cells[8].Value != null)
            {
                string ksPh = dataGridView1.Rows[index].Cells[8].Value.ToString();
                KySu ks1 = khs.laythongtin("", ksPh);
                ksphCB.SelectedValue = ks1.ma;
            }
            else ksphCB.SelectedIndex = 0;

            if (dataGridView1.Rows[index].Cells[9].Value != null)
            {
                string ngayPhstr = dataGridView1.Rows[index].Cells[9].Value.ToString();
                DateTime ngayPh = DateTime.ParseExact(ngayPhstr, "dd/MM/yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                datePH.Value = ngayPh;
            }
            else datePH.Value = DateTimePicker.MinimumDateTime;
            

            
            

            if (dataGridView1.Rows[index].Cells[10].Value != null)
            {
                string ttrPhLay = dataGridView1.Rows[index].Cells[10].Value.ToString();
                HienTrang ht1 = hthd.laythongtin(1, ttrPhLay);
                string ht1m = ht1.ma.ToString();
                tinhtranglayphCB.SelectedValue = ht1m;
            }
            else tinhtranglayphCB.SelectedIndex = 0;

            
            SeriHandler srhd = new SeriHandler();
            if (dataGridView1.Rows[index].Cells[11].Value != null)
            {
                string srmayphlap = dataGridView1.Rows[index].Cells[11].Value.ToString();

                Seri sr = srhd.laythongtinma(srmayphlap);
                string srm = sr.pk.ToString();
                srlayphCB.SelectedValue = srm;
                tralaicheckBox.Checked = false;
            }
            else
            {
                srlayphCB.SelectedIndex = 0;
                tralaicheckBox.Checked = true;
            }

            NoiLayPTHandler vtrhd = new NoiLayPTHandler();
            if (dataGridView1.Rows[index].Cells[12].Value != null)
            {
                string vtrmoiPhlay = dataGridView1.Rows[index].Cells[12].Value.ToString();

                NoiLayPT nl = vtrhd.laythongtin("", vtrmoiPhlay);
                vitrilaymCB.SelectedValue = nl.ma;

            }
            else
            {
                vitrilaymCB.SelectedIndex = 0;
            }

            if (dataGridView1.Rows[index].Cells[13].Value != null)
            {
                string ghichuphlay = dataGridView1.Rows[index].Cells[13].Value.ToString();
                ghichutxt.Text = ghichuphlay;
            }
            else ghichutxt.Text = "";

            if (dataGridView1.Rows[index].Cells[14].Value != null)
            {
                string xuatsu = dataGridView1.Rows[index].Cells[14].Value.ToString();
                NoiLayPT nl1 = vtrhd.laythongtin("", xuatsu);
                vtlapphCB.SelectedValue = nl1.ma;
            }
            else vtlapphCB.SelectedIndex = 0;

            if (dataGridView1.Rows[index].Cells[15].Value != null)
            {
                string srmayphlay = dataGridView1.Rows[index].Cells[15].Value.ToString();
                Seri sr1 = srhd.laythongtinma(srmayphlay);
                string sr1m = sr1.pk.ToString();
                serilapCB.SelectedValue = sr1m;
            }
            else serilapCB.SelectedIndex = 0;

            if (dataGridView1.Rows[index].Cells[16].Value != null)
            {
                string ttrphlap = dataGridView1.Rows[index].Cells[16].Value.ToString();
                HienTrang ht2 = hthd.laythongtin(1, ttrphlap);
                string ht2m = ht2.ma.ToString();
                tinhtranglapCB.SelectedValue = ht2m;
            }
            else tinhtranglapCB.SelectedIndex = 0;

            
            if(dataGridView1.Rows[index].Cells[17].Value !=  null)
            {
                string ghichuphlap = dataGridView1.Rows[index].Cells[17].Value.ToString();
                ghichulaptxt.Text = ghichuphlap;
            }else ghichulaptxt.Text = "";
            
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { return; }
            getInitDataGridView(e.RowIndex);

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
           
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TraCuu();
        }

        private void EDIT_Click(object sender, EventArgs e)
        {
            int keyseri = this.keyseri;

            string khachhang = ((KeyValuePair<string, string>)khachhangCB.SelectedItem).Key;

            string lklay = null;
            if (fullmaycheckBox.Checked) lklay = "";
            else lklay = ((KeyValuePair<string, string>)malkCB.SelectedItem).Key;

            string kslay = ((KeyValuePair<string, string>)kslayCB.SelectedItem).Key;
            string lydolay = lydolaytxt.Text;
            DateTime ngaylay = datelay.Value;
            string ttrlay = ((KeyValuePair<string, string>)tinhtranglayCB.SelectedItem).Key;
            DateTime ngayPh = datePH.Value;
            MessageBox.Show(ngayPh.ToString());
            string ksPh = ((KeyValuePair<string, string>)ksphCB.SelectedItem).Key;
            string ttrPhLay = ((KeyValuePair<string, string>)tinhtranglayphCB.SelectedItem).Key;
            string ghichuphlay = ghichutxt.Text;

            string srmayphlap = "";
            string vtrmoiPhlay = "";///

            string ttrphlap = "";///
            string srmayphlay = "";
            string xuatsu = "";
            string ghichuphlap = "";
            if (!tralaicheckBox.Checked)
            {
                vtrmoiPhlay = ((KeyValuePair<string, string>)vitrilaymCB.SelectedItem).Key;
                if (vtrmoiPhlay == "GĐ") srmayphlap = "";
                else srmayphlap = ((KeyValuePair<string, string>)srlayphCB.SelectedItem).Key;

                xuatsu = ((KeyValuePair<string, string>)vtlapphCB.SelectedItem).Key;
                if (xuatsu == "GĐ") srmayphlay = "";
                else srmayphlay = ((KeyValuePair<string, string>)serilapCB.SelectedItem).Key;

                ttrphlap = ((KeyValuePair<string, string>)tinhtranglapCB.SelectedItem).Key;
                ghichuphlap = ghichulaptxt.Text;
            }

            LichsumoiHandler lsm = new LichsumoiHandler();
            lsm.sua(this.pk, khachhang, lklay, kslay, lydolay, ngaylay, ttrlay, ngayPh, ksPh, ttrPhLay,
               srmayphlap, vtrmoiPhlay, ghichuphlay, ttrphlap, srmayphlay, xuatsu, ghichuphlap);
            TraCuu();
        
        }

        private void CLOSE_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void malkCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lklay = ((KeyValuePair<string, string>)malkCB.SelectedItem).Key;
            ThongkeLKHandler tkl = new ThongkeLKHandler();
            thongkelinhkien tk = new thongkelinhkien();
            if(lklay == "")
            {
                vitrilkLB.Text = "";
            }
            else
            {
                tk = tkl.laythongtin(this.keyseri, lklay);
                NoiLayPTHandler nlpt = new NoiLayPTHandler();
                NoiLayPT nl = nlpt.laythongtin(tk.xuatsu, "");
                vitrilkLB.Text = nl.ten;
            }
            
        }

        private void dataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

        }
        private void tracuulinhkien()
        {
            ThongkeLKHandler tkh = new ThongkeLKHandler();
            List<thongkelinhkien> llk = tkh.layds();
            foreach(thongkelinhkien tk in llk)
            {
                LinhKienHandle lkh = new LinhKienHandle();
                string model = ((KeyValuePair<string, string>)modelCB.SelectedItem).Key;
                linhkien lk = lkh.laythongtin(model, tk.ma, "");
                HienTrangHandler hth = new HienTrangHandler();

                int tinhtrang = 0;
                Int32.TryParse(tk.tinhtrang, out tinhtrang);
                string hientrang = "";
                if(tinhtrang != 0)
                {
                    HienTrang ht = hth.laythongtin(tinhtrang, "");
                    hientrang = ht.ten;

                }
                
                int tinhtrang2 = 0;
                Int32.TryParse(tk.ttrangmayve, out tinhtrang2);
                string hientrang2 = "";
                if(tinhtrang2 != 0)
                {
                    HienTrang ht2 = hth.laythongtin(tinhtrang2, "");
                    hientrang2 = ht2.ten;
                }
                


                NoiLayPTHandler nlpt = new NoiLayPTHandler();
                NoiLayPT nl1 = nlpt.laythongtin(tk.vitrimoi, "");
                NoiLayPT nl2 = nlpt.laythongtin(tk.xuatsu, "");


                SeriHandler serihd = new SeriHandler();
                int sr1 = 0;
                Int32.TryParse(tk.maymoi, out sr1);
                Seri srmoi= serihd.laythongtin(sr1);
                int sr2 = 0;
                Int32.TryParse(tk.maymoi, out sr2);
                Seri srve = serihd.laythongtin(sr2);

                dataGridView2.Rows.Add(tk.keyseri,tk.ma, lk.ten, hientrang, nl1.ten, srmoi.ten, srve.ten, nl2.ten, hientrang2);
            }

        }

    }
   
}
