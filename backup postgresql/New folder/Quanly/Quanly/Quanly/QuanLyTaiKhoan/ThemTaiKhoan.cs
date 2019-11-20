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

namespace Quanly.QuanLyTaiKhoan
{
    public partial class ThemTaiKhoan : Form
    {
        String connString;
        public ThemTaiKhoan()
        {
            InitializeComponent();
            connString = new Property().ConnString;
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==null || textBox1.Text.Equals(""))
            {
                MessageBox.Show("Không chấp nhận nhập ký tự trắng", "Lỗi khi thêm",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox2.Text == null || textBox2.Text.Equals(""))
            {
                MessageBox.Show("Không chấp nhận nhập ký tự trắng", "Lỗi khi thêm",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Boolean flag = true;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Select * from taikhoan where taikhoan=@ten";
                    cmd.Parameters.AddWithValue("ten", textBox1.Text);
                    using (var reader = cmd.ExecuteReader())
                        if (reader.HasRows) flag = false;
                }
                if (flag)
                {
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "Insert into taikhoan(taikhoan,matkhau,vaitro) values(@tk,@mk,@vt)";
                        cmd.Parameters.AddWithValue("tk", textBox1.Text);
                        cmd.Parameters.AddWithValue("mk", textBox2.Text);
                        cmd.Parameters.AddWithValue("vt", comboBox1.Text);
                        cmd.ExecuteNonQuery();
                        this.Close();
                    }
                }else
                {
                    MessageBox.Show("Tài khoản đã tồn tại", "Lỗi khi thêm",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
