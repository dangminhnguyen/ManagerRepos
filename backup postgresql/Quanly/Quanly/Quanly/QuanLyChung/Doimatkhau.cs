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

namespace Quanly
{
    public partial class Doimatkhau : Form
    {
        String connString;
        String user;
        public Doimatkhau(String user)
        {
            InitializeComponent();           
            connString = new Property().ConnString;
            this.user = user;
            this.ActiveControl = textBox1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==null || textBox1.Text.Equals(""))
            {
                MessageBox.Show("Không chấp nhận mật khẩu cũ là khoảng trống", "Lỗi",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = textBox1;
                return;
            }
            if (textBox2.Text == null || textBox2.Text.Equals(""))
            {
                MessageBox.Show("Không chấp nhận mật khẩu mới là khoảng trống", "Lỗi",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = textBox2;
                return;
            }
            if (textBox2.Text.Equals(textBox1.Text))
            {
                MessageBox.Show("Mật khẩu trùng với mật khẩu cũ", "Lỗi",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                this.ActiveControl = textBox2;
                return;
            }
            if (!textBox3.Text.Equals(textBox2.Text))
            {
                MessageBox.Show("Mật khẩu nhập lại không trùng khớp", "Lỗi",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                this.ActiveControl = textBox3;
                return;
            }
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Select * from taikhoan where taikhoan=@tk";
                    cmd.Parameters.AddWithValue("tk", user);
                    Boolean check = false;
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            if (reader.GetString(reader.GetOrdinal("matkhau")).Equals(textBox1.Text)){
                                check = true;
                            }                        
                        }
                        if (check == true)
                        {
                            var cmd1 = new NpgsqlCommand();
                            cmd1.Connection = conn;
                            cmd1.CommandText = "Update taikhoan set matkhau=@mk where taikhoan=@tk";
                            cmd1.Parameters.AddWithValue("mk", textBox2.Text);
                            cmd1.Parameters.AddWithValue("tk", user);
                            int column = cmd1.ExecuteNonQuery();
                            if (column !=0 )
                            {
                                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Đổi mật khẩu không thành công", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mật khẩu cũ không chính xác", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.ActiveControl = textBox1;
                            return;
                        }
                }

            }
        }
    }
}
