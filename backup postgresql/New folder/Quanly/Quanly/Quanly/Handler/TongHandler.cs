using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quanly.Model;

namespace Quanly.Handler
{
    class TongHandler
    {
        String connString;
        public TongHandler()
        {
            connString = new Property().ConnString;
        }
        public Boolean kiemtra(string seri,  string mamay, string mamodel)
        {
            Boolean check = false;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from tong where (seri=@seri or @seri='') and (mamodel=@mamodel or @mamodel='') and (mamay=@mamay or @mamay='')";
                    cmd.Parameters.AddWithValue("seri", seri);
                    cmd.Parameters.AddWithValue("mamodel", mamodel);
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read())
                        {
                            check = true;
                        }
                }
            }
            return check;
        }


        public List<Model.tong> layds(string mamay, string mamodel)
        {
            List<Model.tong> ls = new List<Model.tong>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from tong where mamay=@mamay and mamodel=@mamodel";
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    cmd.Parameters.AddWithValue("mamodel", mamodel);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            int pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            String seri = reader.GetString(reader.GetOrdinal("seri"));
                            String mamay1 = reader.GetString(reader.GetOrdinal("mamay"));
                            String loaimay1 = reader.GetString(reader.GetOrdinal("loaimay"));
                            String mamodel1 = reader.GetString(reader.GetOrdinal("mamodel"));
                            String model1 = reader.GetString(reader.GetOrdinal("model"));
                            String khachhang1 = reader.GetString(reader.GetOrdinal("khachhang"));
                            String makhachhang1 = reader.GetString(reader.GetOrdinal("makhachhang"));
                            String phutrach1 = reader.GetString(reader.GetOrdinal("phutrach"));
                            String hientrang1 = reader.GetString(reader.GetOrdinal("hientrang"));
                            String lichsu1 = reader.GetString(reader.GetOrdinal("lichsu"));
                            DateTime? thoigian1 = null;
                            if (reader.IsDBNull(reader.GetOrdinal("thoigian")))
                            {
                                thoigian1 = null;
                            }
                            else
                            {
                                thoigian1 = reader.GetDateTime(reader.GetOrdinal("thoigian"));
                            }
                            Model.tong m = new Model.tong();

                            m.pk = pk;
                            m.mamodel = mamodel1;
                            m.model = model1;
                            m.mamay = mamay1;
                            m.loaimay = loaimay1;
                            m.makhachhang = makhachhang1;
                            m.khachhang = khachhang1;
                            m.phutrach = phutrach1;
                            m.hientrang = hientrang1;
                            m.thoigian = thoigian1;
                            m.lichsu = lichsu1;
                            ls.Add(m);
                        }
                }
                conn.Close();
            }
            return ls;
        }


        public List<Model.tong> layds()
        {
            List<Model.tong> ls = new List<Model.tong>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from tong";
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            int pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            String seri = reader.GetString(reader.GetOrdinal("seri"));
                            String mamay1 = reader.GetString(reader.GetOrdinal("mamay"));
                            String loaimay1 = reader.GetString(reader.GetOrdinal("loaimay"));
                            String mamodel1 = reader.GetString(reader.GetOrdinal("mamodel"));
                            String model1 = reader.GetString(reader.GetOrdinal("model"));
                            String khachhang1 = reader.GetString(reader.GetOrdinal("khachhang"));
                            String makhachhang1 = reader.GetString(reader.GetOrdinal("makhachhang"));
                            String phutrach1 = reader.GetString(reader.GetOrdinal("phutrach"));
                            String hientrang1 = reader.GetString(reader.GetOrdinal("hientrang"));
                            String lichsu1 = reader.GetString(reader.GetOrdinal("lichsu"));
                            DateTime? thoigian1 = null;
                            if (reader.IsDBNull(reader.GetOrdinal("thoigian")))
                            {
                                thoigian1 = null;
                            }
                            else
                            {
                                thoigian1 = reader.GetDateTime(reader.GetOrdinal("thoigian"));
                            }
                            Model.tong m = new Model.tong();

                            m.pk = pk;
                            m.mamodel = mamodel1;
                            m.model = model1;
                            m.mamay = mamay1;
                            m.loaimay = loaimay1;
                            m.makhachhang = makhachhang1;
                            m.khachhang = khachhang1;
                            m.phutrach = phutrach1;
                            m.hientrang = hientrang1;
                            m.thoigian = thoigian1;
                            m.lichsu = lichsu1;
                            ls.Add(m);
                        }
                }
                conn.Close();
            }
            return ls;
        }

        public Model.tong laythongtin(int pk)
        {
            Model.tong s = new Model.tong();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from tong where pk=@pk";
                    cmd.Parameters.AddWithValue("pk", @pk);

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            s.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            s.seri = reader.GetString(reader.GetOrdinal("seri"));
                            s.mamay = reader.GetString(reader.GetOrdinal("mamay"));
                            s.loaimay = reader.GetString(reader.GetOrdinal("loaimay"));
                            s.mamodel = reader.GetString(reader.GetOrdinal("mamodel"));
                            s.model = reader.GetString(reader.GetOrdinal("model"));
                            s.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            s.makhachhang = reader.GetString(reader.GetOrdinal("makhachhang"));
                            s.phutrach = reader.GetString(reader.GetOrdinal("phutrach"));
                            s.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            s.lichsu = reader.GetString(reader.GetOrdinal("lichsu"));

                            if (reader.IsDBNull(reader.GetOrdinal("thoigian")))
                            {
                                s.thoigian = null;
                            }
                            else
                            {
                                s.thoigian = reader.GetDateTime(reader.GetOrdinal("thoigian"));
                            }

                        }
                }
            }
            return s;

        }

        public List<Model.tong> tracuu(string seri)
        {
            List<Model.tong> ls = new List<Model.tong>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from tong where (seri=@seri or @seri='')";
                    cmd.Parameters.AddWithValue("seri", seri);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            Model.tong s = new Model.tong();
                            s.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            s.seri = reader.GetString(reader.GetOrdinal("seri"));
                            s.mamay = reader.GetString(reader.GetOrdinal("mamay"));
                            s.loaimay = reader.GetString(reader.GetOrdinal("loaimay"));
                            s.mamodel = reader.GetString(reader.GetOrdinal("mamodel"));
                            s.model = reader.GetString(reader.GetOrdinal("model"));
                            s.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            s.makhachhang = reader.GetString(reader.GetOrdinal("makhachhang"));
                            s.phutrach = reader.GetString(reader.GetOrdinal("phutrach"));
                            s.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            s.lichsu = reader.GetString(reader.GetOrdinal("lichsu"));

                            if (reader.IsDBNull(reader.GetOrdinal("thoigian")))
                            {
                                s.thoigian = null;
                            }
                            else
                            {
                                s.thoigian = reader.GetDateTime(reader.GetOrdinal("thoigian"));
                            }
                            ls.Add(s);
                        }
                }
                conn.Close();
            }
            return ls;
        }

        public void them(int pk, string seri, string model,
            string mamodel, string mamay, string loaimay, string makhachhang, string khachhang, string hientrang, string phutrach, string lichsu, DateTime? thoigian)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO tong (pk,seri,mamay,loaimay,mamodel,model,makhachhang,khachhang,phutrach,hientrang,thoigian,lichsu) VALUES (@pk,@seri,@mamay,@loaimay,@mamodel,@model,@makhachhang,@khachhang,@phutrach,@hientrang,@thoigian,@lichsu)";
                    cmd.Parameters.AddWithValue("pk", pk);
                    cmd.Parameters.AddWithValue("seri", seri);
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    cmd.Parameters.AddWithValue("mamay", loaimay);
                    cmd.Parameters.AddWithValue("mamodel", mamodel);
                    cmd.Parameters.AddWithValue("model", model);
                    cmd.Parameters.AddWithValue("makhachhang", makhachhang);
                    cmd.Parameters.AddWithValue("khachhang", khachhang);
                    cmd.Parameters.AddWithValue("hientrang", hientrang);
                    cmd.Parameters.AddWithValue("phutrach", phutrach);
                    cmd.Parameters.AddWithValue("thoigian", thoigian);
                    cmd.Parameters.AddWithValue("lichsu", lichsu);
                    cmd.ExecuteNonQuery();
                }
            }
        }

       /* public void sua(int pk, string seri, string model,
            string mamodel, string mamay, string loaimay, string makhachhang, string khachhang, string hientrang, string phutrach, string lichsu, DateTime? thoigian)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE tong SET pk=@pk,seri=@seri,mamay=@khac,ngaylapdat=@ngaylapdat where ma=@macu and ten=@tencu " +
                        "and mamay=@mamay and mamodel=@mamodel";
                    cmd.Parameters.AddWithValue("pk", pk);
                    cmd.Parameters.AddWithValue("seri", seri);
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    cmd.Parameters.AddWithValue("mamay", loaimay);
                    cmd.Parameters.AddWithValue("mamodel", mamodel);
                    cmd.Parameters.AddWithValue("model", model);
                    cmd.Parameters.AddWithValue("makhachhang", makhachhang);
                    cmd.Parameters.AddWithValue("khachhang", khachhang);
                    cmd.Parameters.AddWithValue("hientrang", hientrang);
                    cmd.Parameters.AddWithValue("phutrach", phutrach);
                    cmd.Parameters.AddWithValue("thoigian", thoigian);
                    cmd.Parameters.AddWithValue("lichsu", lichsu);
                    cmd.ExecuteNonQuery();
                }
            }
        }*/

    }
}
