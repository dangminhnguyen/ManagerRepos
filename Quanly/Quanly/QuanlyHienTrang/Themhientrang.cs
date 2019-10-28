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
using Quanly.Handler;
using Quanly.Model;
using System.IO;

namespace Quanly
{
  
    public partial class Themhientrang : Form
    {
        String connString;
        int pk;
        
        public Themhientrang(int key )
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            connString = new Property().ConnString;
            this.pk = key;
            getdsks();
            getdsht();
            getkh();
            getvitris(pk);
        }

        public void getdsks()
        {
            comboBox1.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            KySuHandler lh = new KySuHandler();
            List<Model.KySu> ls = lh.layds();
            foreach (KySu lm in ls)
            {
                combosource.Add(lm.ma, lm.ma + " - " + lm.ten);
            }
            comboBox1.DataSource = new BindingSource(combosource, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            comboBox1.SelectedIndex = 0;
        }

       
        public void getkh()
        {
            khachhangCB.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            KhachHangHandler kh = new KhachHangHandler();
            List<Model.KhachHang> khl = kh.layds();
            foreach (KhachHang kh1 in khl)
            {
                combosource.Add(kh1.ma, kh1.ten);
            }
            khachhangCB.DataSource = new BindingSource(combosource, null);
            khachhangCB.DisplayMember = "Value";
            khachhangCB.ValueMember = "Key";
            khachhangCB.SelectedIndex = 0;
        }

        public void getvitris(int pk)
        {
            //vitricb.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            if(!combosource.ContainsKey(""))
            {
                combosource.Add("", "");
            }
            
            string makhachhang = ((KeyValuePair<string, string>)khachhangCB.SelectedItem).Key;
            
            if (makhachhang == "K2")
            {
                VitriHandler vth = new VitriHandler();
                List<Vitri> vtl = vth.layds();
                foreach (Vitri vt in vtl)
                {
                    if(!combosource.ContainsKey(vt.ma))
                    {
                        combosource.Add(vt.ma, vt.ten);
                    }
                    
                }
            }
            
            vitricb.DataSource = new BindingSource(combosource, null);
            vitricb.DisplayMember = "Value";
            vitricb.ValueMember = "Key";
            vitricb.SelectedIndex = 0;

            LichSuHandler ls = new LichSuHandler();
            Record r = ls.traCuuGanNhat(pk);
            if(r.vitri !=null)
            {
                vitricb.SelectedValue = r.vitri;
            }

        }
        public void getdsht()
        {
            comboBox2.Items.Clear();
            Dictionary<int, string> combosource = new Dictionary<int, string>();
            HienTrangHandler lh = new HienTrangHandler();
            List<Model.HienTrang> ls = lh.layds();
            foreach (HienTrang lm in ls)
            {
                combosource.Add(lm.ma, lm.ten);
            }
            comboBox2.DataSource = new BindingSource(combosource, null);
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";
            comboBox2.SelectedIndex = 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string makysu = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            string hientrang = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Value;

            string khachhang = ((KeyValuePair<string, string>)khachhangCB.SelectedItem).Key;
            string mavitri = "";
            if (khachhang == "K2")
            {
                mavitri = ((KeyValuePair<string, string>)vitricb.SelectedItem).Key;
            }
           
           
            // string makhachhang = ((KeyValuePair<string, string>)khachhangCB.SelectedItem).Key;
            SeriHandler s = new SeriHandler();
            s.suaKH(khachhang, pk);
            //Them vao lich su
            
            LichSuHandler ls = new LichSuHandler();

            String path = textBox1.Text;
            int keylichsu=ls.them(pk, hientrang, richTextBox2.Text, makysu, dateTimePicker1.Value, richTextBox1.Text, khachhang,mavitri);
            if (!textBox1.Text.Equals("") && textBox1.Text != null)
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("Đường dẫn  file ko tồn tại, bản ghi sẽ được ghi mà ko có đường dẫn file", "Lỗi",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (keylichsu == 0)
                    {
                        MessageBox.Show("Không thể lấy mã của bản ghi vừa thêm, liên hệ lập trình viên để sửa lỗi", "Lỗi",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String filename = Path.GetFileName(path);
                        String folder=System.IO.Path.GetDirectoryName(Application.ExecutablePath)  + "\\FileStorage\\" 
                            +pk+ "\\"+keylichsu;
                        String newpath = folder + "\\" + filename;
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                        File.Copy(path, newpath,true);
                        if (File.Exists(newpath))
                        {
                            ls.capNhatFile(keylichsu, "\\FileStorage\\"+ pk + "\\" + keylichsu + "\\" + filename);
                        }
                        else
                        {
                            MessageBox.Show("Lỗi khi copy file", "Lỗi",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                       
                    }
                }
            }

            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Chọn file để lưu";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String path = openFileDialog1.FileName.ToString();
                textBox1.Text = path;
            }
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void KhachhangCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            getvitris(pk);
        }
    }
}
