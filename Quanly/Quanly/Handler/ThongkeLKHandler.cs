using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Quanly.Model;

namespace Quanly.Handler
{
    class ThongkeLKHandler
    {
        String connString;
        public ThongkeLKHandler()
        {
            connString = new Property().ConnString;
        }
        public Boolean kiemtra(int keyseri, string ma)
        {

            Boolean check = false;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from thongkelinhkien where (keyseri=@keyseri or @keyseri=0) and (ma=@ma or @ma='')";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("keyseri", keyseri);

                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read())
                        {
                            check = true;
                        }
                }
            }
            return check;
        }

        public List<thongkelinhkien> layds()
        {
            List<thongkelinhkien> ls = new List<thongkelinhkien>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM thongkelinhkien", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        int keyseri = reader.GetInt32(reader.GetOrdinal("keyseri"));
                        string ma = reader.GetString(reader.GetOrdinal("ma"));
                        string tinhtrang = reader.GetString(reader.GetOrdinal("tinhtrang"));
                        string maymoi = reader.GetString(reader.GetOrdinal("mayhientai"));
                        string mayve = reader.GetString(reader.GetOrdinal("maythaythe"));
                        string ttrangmayve = reader.GetString(reader.GetOrdinal("tinhtrangthaythe"));
                        string vitrimoi = reader.GetString(reader.GetOrdinal("vitrimoi"));
                        string xuatsu = reader.GetString(reader.GetOrdinal("xuatsu"));


                        thongkelinhkien tk = new thongkelinhkien(keyseri, ma,tinhtrang, maymoi,vitrimoi, mayve,xuatsu,ttrangmayve);
                        ls.Add(tk);
                    }
            }
            return ls;
        }

        public thongkelinhkien laythongtin(int keyseri, string ma)
        {
            if (ma == "" && keyseri == 0)
            {
                return new thongkelinhkien();
            }
            thongkelinhkien lm = new thongkelinhkien();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from thongkelinhkien where (keyseri=@keyseri or @keyseri=0) and (ma=@ma or @ma='')";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("keyseri", keyseri);

                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read())
                        {
                            string ma1 = reader.GetString(reader.GetOrdinal("ma"));
                            int keyseri1 = reader.GetInt32(reader.GetOrdinal("keyseri"));
                            string tinhtrang = reader.GetString(reader.GetOrdinal("tinhtrang"));
                            string maymoi = reader.GetString(reader.GetOrdinal("mayhientai"));
                            string mayve = reader.GetString(reader.GetOrdinal("maythaythe"));
                            string ttrmayve = reader.GetString(reader.GetOrdinal("tinhtrangthaythe"));
                            string vitrimoi = reader.GetString(reader.GetOrdinal("vitrimoi"));
                            string xuatsu = reader.GetString(reader.GetOrdinal("xuatsu"));
                            lm.ma = ma1;
                            lm.keyseri = keyseri1;
                            lm.tinhtrang = tinhtrang;
                            lm.maymoi = maymoi;
                            lm.mayve = mayve;
                            lm.ttrangmayve = ttrmayve;
                            lm.vitrimoi = vitrimoi;
                            lm.xuatsu = xuatsu;
                        }
                }
            }
            return lm;
        }



        public void them(int keyseri, string ma, string tinhtrang, string maymoi,string vitrimoi, string mayve,string xuatsu, string ttrangmayve)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO thongkelinhkien (keyseri,ma,tinhtrang,mayhientai,vitrimoi,maythaythe,xuatsu,tinhtrangthaythe) VALUES (@keyseri,@ma,@tinhtrang,@mayhientai,@vitrimoi,@maythaythe,@xuatsu,@tinhtrangthaythe)";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("keyseri", keyseri);
                    cmd.Parameters.AddWithValue("tinhtrang", tinhtrang);
                    cmd.Parameters.AddWithValue("mayhientai", maymoi);
                    cmd.Parameters.AddWithValue("maythaythe", mayve);
                    cmd.Parameters.AddWithValue("vitrimoi", vitrimoi);
                    cmd.Parameters.AddWithValue("xuatsu", xuatsu);
                    cmd.Parameters.AddWithValue("tinhtrangthaythe", ttrangmayve);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void sua(int keyseri,string macu,string tinhtrangmoi,  string maymoimoi,string vitrimoi, string mayvemoi,string xuatsu, string tinhtrangthaythemoi)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE thongkelinhkien set tinhtrang=@tinhtrangmoi,mayhientai=@maymoimoi,vitrimoi=@vitrimoi" +
                        ",maythaythe=@mayvemoi,xuatsu=@xuatsu, tinhtrangthaythe=@tinhtrangthaythemoi where keyseri=@keyseri and ma=@macu";
                    cmd.Parameters.AddWithValue("keyseri", keyseri);
                    cmd.Parameters.AddWithValue("macu", macu);
                    cmd.Parameters.AddWithValue("tinhtrangmoi", tinhtrangmoi);
                    cmd.Parameters.AddWithValue("maymoimoi", maymoimoi);
                    cmd.Parameters.AddWithValue("mayvemoi", mayvemoi);
                    cmd.Parameters.AddWithValue("vitrimoi", vitrimoi);
                    cmd.Parameters.AddWithValue("xuatsu", xuatsu);
                    cmd.Parameters.AddWithValue("tinhtrangthaythemoi", tinhtrangthaythemoi);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
