using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quanly.Model;
using System.Windows.Forms;

namespace Quanly.Handler
{
    class HienTrangHandler
    {
        string connString;
        public HienTrangHandler()
        {
            connString = new Property().ConnString;
        }
        public Boolean kiemtra( string ten)
        {
            Boolean check = false;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from hientrang where ten=@ten";
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

        public List<HienTrang> layds()
        {
            List<HienTrang> ls = new List<HienTrang>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM hientrang", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        int ma = reader.GetInt32(reader.GetOrdinal("ma"));
                        String ten = reader.GetString(reader.GetOrdinal("ten"));
                        HienTrang lm = new HienTrang(ma, ten);
                        ls.Add(lm);
                    }
            }
            return ls;
        }

        public HienTrang laythongtin(int ma, string ten)
        {
            HienTrang lm = new HienTrang();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from hientrang where (ma=@ma or @ma=1) and (ten=@ten or @ten='')";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            int ma1 = reader.GetInt32(reader.GetOrdinal("ma"));
                            String ten1 = reader.GetString(reader.GetOrdinal("ten"));
                            lm.ma = ma1;
                            lm.ten = ten1;
                               
                        }
                }
            }
            return lm;
        }

        public void them(string ten)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO hientrang (ten) VALUES (@ten)";
                    
                    cmd.Parameters.AddWithValue("ten", ten);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void sua(string tenmoi, string tencu)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE hientrang set ten=@tenmoi where ten=@tencu";
                    cmd.Parameters.AddWithValue("tenmoi", tenmoi);
                    cmd.Parameters.AddWithValue("tencu", tencu);
                   
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void xoa(string ten)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE from hientrang where ten=@ten";
                    cmd.Parameters.AddWithValue("ten", ten);
                   
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
