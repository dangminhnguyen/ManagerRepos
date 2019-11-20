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
    public partial class Dangnhap : Form
    {
        public string ReturnValue1 { get; set; }
        public string ReturnValue2 { get; set; }
        public string ReturnValue3 { get; set; }
        String connString;
        public Dangnhap()
        {
            InitializeComponent();
            connString = new Property().ConnString;
            this.ActiveControl = textBox1;
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String user = textBox1.Text;
            String pass = textBox2.Text;
            if(user.Equals("") || user == null)
            {
                MessageBox.Show("Không chấp nhận tài khoản là khoảng trống", "Lỗi",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = textBox1;
                return;
               
            }
            if (pass.Equals("") || pass == null)
            {
                MessageBox.Show("Không chấp nhận mật khẩu là khoảng trống", "Lỗi",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = textBox2;
                return;
            }
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Select * from taikhoan where taikhoan=@ten";
                    cmd.Parameters.AddWithValue("ten", user);
                    Boolean flag = false;
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            if (reader.GetString(reader.GetOrdinal("matkhau")).Equals(pass))
                            {
                                flag = true;
                                this.ReturnValue1 = "Open";
                                this.ReturnValue2 = reader.GetString(reader.GetOrdinal("taikhoan"));
                                this.ReturnValue3 = reader.GetString(reader.GetOrdinal("vaitro"));

                            }
                        }
                        if (flag)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập thất bại", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                             
                            textBox2.Text = "";
                            this.ActiveControl = textBox2;

                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ReturnValue1 = "Close";
            this.ReturnValue2 = "Close";
            this.ReturnValue3 = "Close";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
