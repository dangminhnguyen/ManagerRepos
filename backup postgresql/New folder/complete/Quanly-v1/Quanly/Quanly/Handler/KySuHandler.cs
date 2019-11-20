using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quanly.Model;



namespace Quanly.Handler
{
    class KySuHandler
    {
        String connString;
        public KySuHandler()
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
                    cmd.CommandText = "SELECT * from kysu where (ma=@ma or @ma='') and (ten=@ten or @ten='')";
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

        public List<KySu> layds()
        {
            List<KySu> ls = new List<KySu>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM kysu", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        String ma = reader.GetString(reader.GetOrdinal("ma"));
                        String ten = reader.GetString(reader.GetOrdinal("ten"));
                        KySu lm = new KySu(ma, ten);
                        ls.Add(lm);
                    }
            }
            return ls;
        }

        public KySu laythongtin(string ma, string ten)
        {
            if (ma == "" && ten == "")
            {
                return new KySu();
            }
            KySu lm = new KySu();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from kysu where (ma=@ma or @ma='') and (ten=@ten or @ten='')";
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
            }
            return lm;
        }

        public void them(string ma, string ten)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO kysu (ma,ten) VALUES (@ma,@ten)";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void sua(string tenmoi, string tencu, string mamoi, string macu)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE kysu set ten=@tenmoi,ma=@mamoi where ten=@tencu and ma=@macu";
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
