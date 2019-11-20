using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quanly.Handler;
using Quanly.Model;

namespace Quanly
{
    public partial class SuaHienTrang : Form
    {
        String connString;
        int pk;
        
        public SuaHienTrang(int key)
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            connString = new Property().ConnString;
            this.pk = key;            
            getdsks();
            getdsht();
            getthongtin(pk);
        }

        public void getthongtin(int pk)
        {
            LichSuHandler lh = new LichSuHandler();
            Record r = lh.laythongtin(pk);           

            KySuHandler kh = new KySuHandler();
            KySu k = kh.laythongtin(r.phutrach, "");

            HienTrangHandler hh = new HienTrangHandler();
            HienTrang h = hh.laythongtin(0, r.hientrang);

            comboBox1.SelectedValue = k.ma;
            comboBox2.SelectedValue=h.ma;
            dateTimePicker1.Value = r.thoigian;
            richTextBox1.Text = r.ghichu;
            richTextBox2.Text = r.khacphuc;
            if (r.filepath.Equals(""))
            {
                textBox1.Text = "";
            }
            else
            {
                textBox1.Text = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + r.filepath;
            }
            
        }

        public void getdsks()
        {
            comboBox1.Items.Clear();
            Dictionary<string, string> combosource = new Dictionary<string, string>();
            KySuHandler lh = new KySuHandler();
            List<Model.KySu> ls = lh.layds();
            foreach (KySu lm in ls)
            {
                combosource.Add(lm.ma,lm.ten);
            }
            comboBox1.DataSource = new BindingSource(combosource, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            comboBox1.SelectedIndex = 0;
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string makysu = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            string hientrang = ((KeyValuePair<int, string>)comboBox2.SelectedItem).Value;
            //Them vao lich su
            LichSuHandler ls = new LichSuHandler();
            int keyseri=ls.sua(pk, hientrang, richTextBox2.Text, makysu, dateTimePicker1.Value, richTextBox1.Text);
            String path = textBox1.Text;
            if (!textBox1.Text.Equals("") && textBox1.Text != null)
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("Đường dẫn  file ko tồn tại, bản ghi sẽ được ghi mà ko có đường dẫn file", "Lỗi",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (pk == 0)
                    {
                        MessageBox.Show("Không thể lấy mã của bản ghi vừa thêm, liên hệ lập trình viên để sửa lỗi", "Lỗi",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        String filename = Path.GetFileName(path);
                        String folder = System.IO.Path.GetDirectoryName(Application.ExecutablePath) +"\\FileStorage\\"
                             + keyseri + "\\" + pk;
                        String newpath = folder + "\\"+ filename;
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                        File.Copy(path, newpath, true);
                        if (File.Exists(newpath))
                        {
                            ls.capNhatFile(pk, "\\FileStorage\\"+keyseri + "\\" + pk + "\\" + filename);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa bản ghi hiện trạng này?", "Xác nhận", MessageBoxButtons.YesNo,
        MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                LichSuHandler ls = new LichSuHandler();
                ls.xoa(pk);
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Chọn file để lưu";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String path = openFileDialog1.FileName.ToString();
                textBox1.Text = "";
                textBox1.Text = path;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                String path = Path.GetDirectoryName(textBox1.Text);
                Process.Start(path);
            }
            else
            {
                MessageBox.Show("File ko tồn tại trong hệ thống", "Lỗi",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
