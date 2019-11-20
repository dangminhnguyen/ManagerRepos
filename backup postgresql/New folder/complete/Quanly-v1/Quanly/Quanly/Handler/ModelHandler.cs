using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quanly.Model;
using Npgsql;

namespace Quanly.Handler
{
    class ModelHandler
    {
        String connString;
        public ModelHandler()
        {
            connString = new Property().ConnString;
        }

        public Boolean kiemtra(string ma, string ten,string mamay)
        {
            Boolean check = false;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from model where (ma=@ma or @ma='') and (ten=@ten or @ten='')" +
                        " and (mamay=@mamay or @mamay='')";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);
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

        public List<Model.Model> layds(string mamay)
        {
            List<Model.Model> ls = new List<Model.Model>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from model where mamay=@mamay";
                    cmd.Parameters.AddWithValue("mamay", mamay);

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            String ma1 = reader.GetString(reader.GetOrdinal("ma"));
                            String ten1 = reader.GetString(reader.GetOrdinal("ten"));
                            Model.Model m = new Model.Model();
                            m.ma = ma1;
                            m.ten = ten1;
                            m.mamay = mamay;
                            ls.Add(m);
                        }
                }
            }
            return ls;
        }

        public List<Model.Model> layds()
        {
            List<Model.Model> ls = new List<Model.Model>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from model";
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            String ma1 = reader.GetString(reader.GetOrdinal("ma"));
                            String ten1 = reader.GetString(reader.GetOrdinal("ten"));
                            Model.Model m = new Model.Model();
                            m.ma = ma1;
                            m.ten = ten1;
                            ls.Add(m);
                        }
                }
            }
            return ls;
        }

        public Model.Model laythongtin(string ma,string ten,string mamay)
        {
            Model.Model lm = new Model.Model();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from Model where (ma=@ma or @ma='') and (ten=@ten or @ten='')" +
                        "and (mamay=@mamay or @mamay='') ";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);
                    cmd.Parameters.AddWithValue("mamay", mamay);

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

        public void them(string ma, string ten,string mamay)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO model (ma,ten,mamay) VALUES (@ma,@ten,@mamay)";
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void sua(string ma, string ten, string mamay,string macu,string tencu)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE model SET ma=@mamoi,ten=@tenmoi where ma=@macu and ten=@tencu and mamay=@mamay";
                    cmd.Parameters.AddWithValue("mamoi", ma);
                    cmd.Parameters.AddWithValue("tenmoi", ten);
                    cmd.Parameters.AddWithValue("macu", macu);
                    cmd.Parameters.AddWithValue("tencu", tencu);
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
