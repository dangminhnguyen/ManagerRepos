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
        String vaitro;
        String user;
        public ThemTaiKhoan()
        {
            InitializeComponent();
            dangnhap();
            connString = new Property().ConnString;
            comboBox1.SelectedIndex = 0;
        }
        public void dangnhap()
        {
            Dangnhap frm2 = new Dangnhap();
            frm2.StartPosition = FormStartPosition.CenterParent;
            var result = frm2.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string val = frm2.ReturnValue1;
                string val1 = frm2.ReturnValue2;   //values preserved after close
                if (val.Equals("Close")) this.Shown += new EventHandler(MyForm_CloseOnStart);
                if (val1.Equals("root") == false)
                {
                    MessageBox.Show("Sorry! you not root", "Lỗi",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Shown += new EventHandler(MyForm_CloseOnStart);
                }

                else if (val.Equals("Open"))
                {
                    user = frm2.ReturnValue2;
                    vaitro = frm2.ReturnValue3;
                    if (!vaitro.Equals("admin"))
                    {
                        //quảnLýToolStripMenuItem.Visible = false;
                    }
                }


            }
        }

        private void MyForm_CloseOnStart(object sender, EventArgs e)
        {

            this.Close();
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
