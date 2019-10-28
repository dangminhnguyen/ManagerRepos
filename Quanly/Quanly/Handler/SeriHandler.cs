using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quanly.Model;

namespace Quanly.Handler
{
    class SeriHandler
    {
        String connString;
        public SeriHandler()
        {
            connString = new Property().ConnString;
        }
        public Boolean kiemtra(string ma, string ten, string mamay,string mamodel)
        {
            Boolean check = false;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from seri where (ma=@ma or @ma='') and (ten=@ten or @ten='')" +
                        " and (mamodel=@mamodel or @mamodel='') and (mamay=@mamay or @mamay='')";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);
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

        public List<Model.Seri> layds(string mamay,string mamodel)
        {
            List<Model.Seri> ls = new List<Model.Seri>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from seri where mamay=@mamay and mamodel=@mamodel";
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    cmd.Parameters.AddWithValue("mamodel", mamodel);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            int pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            String ma1 = reader.GetString(reader.GetOrdinal("ma"));
                            String ten1 = reader.GetString(reader.GetOrdinal("ten"));
                            String khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            DateTime? ngaylapdat = null;
                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                ngaylapdat = null;
                            }
                            else
                            {
                                ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                            Model.Seri m = new Model.Seri();
                            m.ma = ma1;
                            m.ten = ten1;
                            m.mamay = mamay;
                            m.mamodel = mamodel;
                            m.khachhang = khachhang;
                            m.pk = pk;
                            m.ngaylapdat = ngaylapdat;
                            ls.Add(m);
                        }
                }
            }
            return ls;
        }

        public List<Model.Seri> layds()
        {
            List<Model.Seri> ls = new List<Model.Seri>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from seri ";
                  
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            int pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            String ma1 = reader.GetString(reader.GetOrdinal("ma"));
                            String ten1 = reader.GetString(reader.GetOrdinal("ten"));
                            String khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            DateTime? ngaylapdat =null;
                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                ngaylapdat=null;
                            }
                            else
                            {
                                ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                            Model.Seri m = new Model.Seri();
                            m.ma = ma1;
                            m.ten = ten1;
                            m.khachhang = khachhang;
                            m.pk = pk;
                            m.ngaylapdat = ngaylapdat;
                            ls.Add(m);
                        }
                }
            }
            return ls;
        }

        public Model.Seri laythongtin(int pk)
        {
            Model.Seri s = new Model.Seri();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from seri where pk=@pk";
                    cmd.Parameters.AddWithValue("pk", @pk);
                   
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            s.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            s.ma = reader.GetString(reader.GetOrdinal("ma"));
                            s.ten = reader.GetString(reader.GetOrdinal("ten"));
                            s.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            s.mamodel = reader.GetString(reader.GetOrdinal("mamodel"));
                            s.mamay = reader.GetString(reader.GetOrdinal("mamay"));
                            s.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            
                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                           
                        }
                }
            }
            return s;
               
        }

        public Model.Seri laythongtinma(string ma)
        {
            Model.Seri s = new Model.Seri();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from seri where ma=@ma";
                    cmd.Parameters.AddWithValue("ma", @ma);

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            s.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            s.ma = reader.GetString(reader.GetOrdinal("ma"));
                            s.ten = reader.GetString(reader.GetOrdinal("ten"));
                            s.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            s.mamodel = reader.GetString(reader.GetOrdinal("mamodel"));
                            s.mamay = reader.GetString(reader.GetOrdinal("mamay"));
                            s.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));

                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }

                        }
                }
            }
            return s;

        }

        public List<Model.Seri> tracuu(string ten)
        {
            List < Model.Seri > list = new List<Model.Seri>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from seri where (ten=@ten or @ten='')";
                    cmd.Parameters.AddWithValue("ten", ten);                   
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            Model.Seri s = new Model.Seri();
                            s.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            s.ma = reader.GetString(reader.GetOrdinal("ma"));
                            s.ten = reader.GetString(reader.GetOrdinal("ten"));
                            s.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            s.mamodel = reader.GetString(reader.GetOrdinal("mamodel"));
                            s.mamay = reader.GetString(reader.GetOrdinal("mamay"));
                            s.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                            list.Add(s);
                        }
                }
            }
            return list;
        }

        public List<Model.Seri> tracuukho(string ten)
        {
            List<Model.Seri> list = new List<Model.Seri>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from seri where (ten=@ten or @ten='') and (khachhang='K2' or khachhang='K1')";
                    cmd.Parameters.AddWithValue("ten", ten);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            Model.Seri s = new Model.Seri();
                            s.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            s.ma = reader.GetString(reader.GetOrdinal("ma"));
                            s.ten = reader.GetString(reader.GetOrdinal("ten"));
                            s.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            s.mamodel = reader.GetString(reader.GetOrdinal("mamodel"));
                            s.mamay = reader.GetString(reader.GetOrdinal("mamay"));
                            s.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                            list.Add(s);
                        }
                }
            }
            return list;
        }

        public Model.Seri tracuuma(string ma)
        {
            Model.Seri s = new Model.Seri();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from seri where ma=@ma";
                    cmd.Parameters.AddWithValue("ma", ma);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            
                            s.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            s.ma = reader.GetString(reader.GetOrdinal("ma"));
                            s.ten = reader.GetString(reader.GetOrdinal("ten"));
                            s.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            s.mamodel = reader.GetString(reader.GetOrdinal("mamodel"));
                            s.mamay = reader.GetString(reader.GetOrdinal("mamay"));
                            s.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                            
                        }
                }
            }
            return s;
        }

        public void them(string ma, string ten, string mamay,string mamodel,string khachhang,DateTime ngaylapdat)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO seri (ma,ten,mamay,mamodel,khachhang,hientrang,ngaylapdat) VALUES (@ma,@ten,@mamay,@mamodel,@khachhang,'0',@ngaylapdat)";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    cmd.Parameters.AddWithValue("mamodel", mamodel);
                    cmd.Parameters.AddWithValue("khachhang", khachhang);
                    cmd.Parameters.AddWithValue("ngaylapdat", ngaylapdat);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void sua(string ma, string ten, string mamay ,string mamodel, string macu, string tencu,string khachhang,DateTime ngaylapdat)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE seri SET ma=@mamoi,ten=@tenmoi,khachhang=@khachhang,ngaylapdat=@ngaylapdat where ma=@macu and ten=@tencu " +
                        "and mamay=@mamay and mamodel=@mamodel";
                    cmd.Parameters.AddWithValue("mamoi", ma);
                    cmd.Parameters.AddWithValue("tenmoi", ten);
                    cmd.Parameters.AddWithValue("macu", macu);
                    cmd.Parameters.AddWithValue("tencu", tencu);
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    cmd.Parameters.AddWithValue("mamodel", mamodel);
                    cmd.Parameters.AddWithValue("khachhang", khachhang);
                    cmd.Parameters.AddWithValue("ngaylapdat", ngaylapdat);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void deleteseri(int pk)
        {
            using(var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using(var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM seri Where pk=@pk";
                    cmd.Parameters.AddWithValue("pk", pk);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void suaKH( string ma, int pk)
        {
            
            //using (var conn = new NpgsqlConnection(connString))
           // {
             //   conn.Open();
              //  using (var cmd = new NpgsqlCommand())
              //  {
               //     cmd.Connection = conn;
               //     cmd.CommandText = "UPDATE seri SET khachhang=@khachhang where pk=@pk";
               //     cmd.Parameters.AddWithValue("khachhang", ma);
               //     cmd.Parameters.AddWithValue("pk", pk);
               //     cmd.ExecuteNonQuery();
               // }
           // }
        }
    }

}
