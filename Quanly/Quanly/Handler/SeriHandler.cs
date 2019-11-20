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
                            String phutrach = null;

                            int hientrang = 0;
                            if (reader.IsDBNull(reader.GetOrdinal("hientrang")))
                            {
                                hientrang = 0;
                            }
                            else
                            {
                                hientrang = reader.GetInt32(reader.GetOrdinal("hientrang"));
                            }


                            if (reader.IsDBNull(reader.GetOrdinal("phutrach")))
                            {
                                phutrach = null;
                            }
                            else
                            {
                                phutrach = reader.GetString(reader.GetOrdinal("phutrach"));
                            }
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
                            m.phutrach = phutrach;
                            m.hientrang = hientrang;
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
                            String phutrach = null;

                            if (reader.IsDBNull(reader.GetOrdinal("phutrach")))
                            {
                                phutrach = null;
                            }
                            else
                            {
                                phutrach = reader.GetString(reader.GetOrdinal("phutrach"));
                            }

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
                            m.khachhang = khachhang;
                            m.pk = pk;
                            m.ngaylapdat = ngaylapdat;
                            m.phutrach = phutrach;
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
                            s.hientrang = reader.GetInt32(reader.GetOrdinal("hientrang"));
                            
                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                          

                            if (reader.IsDBNull(reader.GetOrdinal("phutrach")))
                            {
                                s.phutrach = null;
                            }
                            else
                            {
                                s.phutrach = reader.GetString(reader.GetOrdinal("phutrach"));
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
                            s.hientrang = reader.GetInt32(reader.GetOrdinal("hientrang"));

                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                            if (reader.IsDBNull(reader.GetOrdinal("phutrach")))
                            {
                                s.phutrach = null;
                            }
                            else
                            {
                                s.phutrach = reader.GetString(reader.GetOrdinal("phutrach"));
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
                            s.hientrang = reader.GetInt32(reader.GetOrdinal("hientrang"));
                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                            if (reader.IsDBNull(reader.GetOrdinal("phutrach")))
                            {
                                s.phutrach = null;
                            }
                            else
                            {
                                s.phutrach = reader.GetString(reader.GetOrdinal("phutrach"));
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
                            s.hientrang = reader.GetInt32(reader.GetOrdinal("hientrang"));
                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                            if (reader.IsDBNull(reader.GetOrdinal("phutrach")))
                            {
                                s.phutrach = null;
                            }
                            else
                            {
                                s.phutrach = reader.GetString(reader.GetOrdinal("phutrach"));
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
                            s.hientrang = reader.GetInt32(reader.GetOrdinal("hientrang"));
                            if (reader.IsDBNull(reader.GetOrdinal("ngaylapdat")))
                            {
                                s.ngaylapdat = null;
                            }
                            else
                            {
                                s.ngaylapdat = reader.GetDateTime(reader.GetOrdinal("ngaylapdat"));
                            }
                            if (reader.IsDBNull(reader.GetOrdinal("phutrach")))
                            {
                                s.phutrach = null;
                            }
                            else
                            {
                                s.phutrach = reader.GetString(reader.GetOrdinal("phutrach"));
                            }

                        }
                }
            }
            return s;
        }

        public int them(string ma, string ten, string mamay,string mamodel,string khachhang,DateTime ngaylapdat,string phutrach,int mahientrang)
        {
            int pk = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO seri (ma,ten,mamay,mamodel,khachhang,hientrang,ngaylapdat,phutrach) VALUES (@ma,@ten,@mamay,@mamodel,@khachhang,@hientrang,@ngaylapdat,@phutrach)";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    cmd.Parameters.AddWithValue("mamodel", mamodel);
                    cmd.Parameters.AddWithValue("khachhang", khachhang);
                    cmd.Parameters.AddWithValue("ngaylapdat", ngaylapdat);
                    cmd.Parameters.AddWithValue("phutrach", phutrach);
                    cmd.Parameters.AddWithValue("hientrang", mahientrang);

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            pk = reader.GetInt32(reader.GetOrdinal("pk"));
                        }
                }
            }
            
            return pk;
        }

        public void sua(string ma, string ten, string mamay ,string mamodel, string macu, string tencu, string phutrach, int hientrang,string khachhang,DateTime ngaylapdat)
        {
            MessageBox.Show(phutrach);
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE seri SET ma=@mamoi,ten=@tenmoi,khachhang=@khachhang,phutrach=@phutrach,hientrang=@hientrang,ngaylapdat=@ngaylapdat where ma=@macu and ten=@tencu " +
                        "and mamay=@mamay and mamodel=@mamodel";
                    cmd.Parameters.AddWithValue("mamoi", ma);
                    cmd.Parameters.AddWithValue("tenmoi", ten);
                    cmd.Parameters.AddWithValue("macu", macu);
                    cmd.Parameters.AddWithValue("tencu", tencu);
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    cmd.Parameters.AddWithValue("mamodel", mamodel);
                    cmd.Parameters.AddWithValue("khachhang", khachhang);
                    cmd.Parameters.AddWithValue("ngaylapdat", ngaylapdat);
                    cmd.Parameters.AddWithValue("phutrach", phutrach);
                    cmd.Parameters.AddWithValue("hientrang", hientrang);
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
