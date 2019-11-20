using Npgsql;
using Quanly.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Quanly.Handler
{
    class LoaiMayHandler
    {
        String connString;
        public LoaiMayHandler()
        {
            connString = new Property().ConnString;            
        }

        public Boolean kiemtra(string ma,string ten)
        {
            Boolean check = false;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from LoaiMay where (ma=@ma or @ma='') and (ten=@ten or @ten='')";
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

        public List<LoaiMay>  layds()
        {
            List<LoaiMay> ls = new List<LoaiMay>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open(); 
                using (var cmd = new NpgsqlCommand("SELECT * FROM LoaiMay", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        String ma = reader.GetString(reader.GetOrdinal("ma"));
                        String ten= reader.GetString(reader.GetOrdinal("ten"));
                        LoaiMay lm = new LoaiMay(ma, ten);
                        ls.Add(lm);
                    }
            }
            return ls;
        }

        public LoaiMay laythongtin(string ma,string ten)
        {
            LoaiMay lm = new LoaiMay();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from LoaiMay where (ma=@ma or @ma='') and (ten=@ten or @ten='')";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);

                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read())
                        {
                            String ma1 = reader.GetString(reader.GetOrdinal("ma"));
                            String ten1 = reader.GetString(reader.GetOrdinal("ten"));
                            lm.ma = ma1;
                            lm.ten = ten1;
                        }
                }
                conn.Close();
            }
            return lm;
        }

        public List<Model.LoaiMay> tracuu(string ten)
        {
            List<Model.LoaiMay> list = new List<Model.LoaiMay>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from loaimay where (ten=@ten or @ten='')";
                    cmd.Parameters.AddWithValue("ten", ten);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            Model.LoaiMay s = new Model.LoaiMay();
                            s.ma = reader.GetString(reader.GetOrdinal("ma"));
                            s.ten = reader.GetString(reader.GetOrdinal("ten"));
                            list.Add(s);
                        }
                }
                conn.Close();
            }
            return list;
        }

        public void them(string ma,string ten)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO LoaiMay (ma,ten) VALUES (@ma,@ten)";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void sua(string tenmoi,string tencu,string mamoi,string macu)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE LoaiMay set ten=@tenmoi,ma=@mamoi where ten=@tencu and ma=@macu";
                    cmd.Parameters.AddWithValue("tenmoi", tenmoi);
                    cmd.Parameters.AddWithValue("tencu", tencu);
                    cmd.Parameters.AddWithValue("mamoi", mamoi);
                    cmd.Parameters.AddWithValue("macu", macu);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
