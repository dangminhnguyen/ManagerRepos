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
    public partial class QuanLyTaiKhoan : Form
    {
        String connString;
        String vaitro;
        String user;
        public QuanLyTaiKhoan(String user,String vaitro)
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridView1_RowPrePaint);

            connString = new Property().ConnString;
            this.user = user;
            this.vaitro = vaitro;
            
            DsTaiKhoan();
        }

        private void MyForm_CloseOnStart(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
        }

        private void DsTaiKhoan()
        {
            dataGridView1.Rows.Clear();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand("SELECT * FROM taikhoan", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        String taikhoan = reader.GetString(reader.GetOrdinal("taikhoan"));
                        String vaitro = reader.GetString(reader.GetOrdinal("vaitro"));
                        dataGridView1.Rows.Add(taikhoan, vaitro);
                    }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThemTaiKhoan frm2 = new ThemTaiKhoan();
            frm2.StartPosition = FormStartPosition.CenterParent;
            frm2.ShowDialog(this);
            DsTaiKhoan();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Khởi tạo lại mật khẩu", ResetPass_Click));
                m.MenuItems.Add(new MenuItem("Xóa tài khoản", EraseUser_Click));
                m.Show(dataGridView1, dataGridView1.
                    PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y)));

            }
        }

        private void ResetPass_Click(Object sender, System.EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Khởi tạo mật khẩu tài khoản này?", "Xác nhận", MessageBoxButtons.YesNoCancel,
                 MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                String value1="";
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    value1 = row.Cells[0].Value.ToString();
                }
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "Update taikhoan set matkhau='1' where taikhoan=@ten";
                        cmd.Parameters.AddWithValue("ten", value1);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Mật khẩu đã được khởi tạo lại thành 1", "Thông tin",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void EraseUser_Click(Object sender, System.EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNoCancel,
                 MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                String value1 = "";
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    value1 = row.Cells[0].Value.ToString();
                }
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "Delete from taikhoan  where taikhoan=@ten";
                        cmd.Parameters.AddWithValue("ten", value1);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa thành công", "Thông tin",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DsTaiKhoan();
                    }
                }
            }
        }
    }
}
