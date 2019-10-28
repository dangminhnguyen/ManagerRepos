using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quanly.Model;
using Npgsql;

namespace Quanly.Handler
{
    class VitriHandler
    {

        String connString;
        public VitriHandler()
        {
            connString = new Property().ConnString;
        }
        public List<Vitri> layds()
        {
            List<Vitri> ls = new List<Vitri>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM vitrikho", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        String ma = reader.GetString(reader.GetOrdinal("ma"));
                        String ten = reader.GetString(reader.GetOrdinal("ten"));
                        Vitri lm = new Vitri(ma, ten);
                        ls.Add(lm);
                    }
            }
            return ls;
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
                    cmd.CommandText = "SELECT * from vitrikho where (ma=@ma or @ma='') and (ten=@ten or @ten='')";
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
        public Vitri laythongtin(string ma, string ten)
        {
            if (ma == "" && ten == "")
            {
                return new Vitri();
            }
            Vitri lm = new Vitri();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from vitrikho where (ma=@ma or @ma='') and (ten=@ten or @ten='')";
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
                    cmd.CommandText = "INSERT INTO vitrikho (ma,ten) VALUES (@ma,@ten)";
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
                    cmd.CommandText = "UPDATE vitrikho set ten=@tenmoi,ma=@mamoi where ten=@tencu and ma=@macu";
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
