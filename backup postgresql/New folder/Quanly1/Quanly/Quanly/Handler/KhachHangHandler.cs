using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quanly.Model;



namespace Quanly.Handler
{
    class KhachHangHandler
    {
        String connString;
        public KhachHangHandler()
        {
            connString = new Property().ConnString;            
        }
        public Boolean kiemtra(string ma, string ten)
        {
            Boolean check = false;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from khachhang where (ma=@ma or @ma='') and (ten=@ten or @ten='')";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);

                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read())
                        {
                            check = true;
                        }
                }
            }
            return check;
        }

        public List<KhachHang> layds()
        {
            List<KhachHang> ls = new List<KhachHang>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM khachhang", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        String ma = reader.GetString(reader.GetOrdinal("ma"));
                        String ten = reader.GetString(reader.GetOrdinal("ten"));
                        String tt;
                        if (reader.IsDBNull(reader.GetOrdinal("thongtin")))
                        {
                            tt = "";
                        }
                        else
                        {
                            tt = reader.GetString(reader.GetOrdinal("thongtin"));
                        }
                        
                        KhachHang lm = new KhachHang(ma, ten,tt);
                        ls.Add(lm);
                    }
            }
            return ls;
        }

        public KhachHang laythongtin(string ma, string ten)
        {
            KhachHang lm = new KhachHang();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from khachhang where (ma=@ma or @ma='') and (ten=@ten or @ten='')";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);

                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read())
                        {
                            String ma1 = reader.GetString(reader.GetOrdinal("ma"));
                            String ten1 = reader.GetString(reader.GetOrdinal("ten"));
                            String tt;
                            if (reader.IsDBNull(reader.GetOrdinal("thongtin")))
                            {
                                tt = "";
                            }
                            else
                            {
                                tt = reader.GetString(reader.GetOrdinal("thongtin"));
                            }

                            lm.ma = ma1;
                            lm.ten = ten1;
                            lm.thongtin = tt;
                        }
                }
            }
            return lm;
        }

        public void them(string ma, string ten,string thongtin)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO khachhang (ma,ten,thongtin) VALUES (@ma,@ten,@thongtin)";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);
                    cmd.Parameters.AddWithValue("thongtin", thongtin);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void sua(string tenmoi, string tencu, string mamoi, string macu ,string thongtin)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE khachhang set ten=@tenmoi,ma=@mamoi,thongtin=@thongtin where ten=@tencu and ma=@macu";
                    cmd.Parameters.AddWithValue("tenmoi", tenmoi);
                    cmd.Parameters.AddWithValue("tencu", tencu);
                    cmd.Parameters.AddWithValue("mamoi", mamoi);
                    cmd.Parameters.AddWithValue("macu", macu);
                    cmd.Parameters.AddWithValue("thongtin", thongtin);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
