using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quanly.Model;
using Npgsql;
using System.Windows.Forms;


namespace Quanly.Handler
{
    class LinhKienHandle
    {
        String connString;

        public LinhKienHandle()
        {
            connString = new Property().ConnString;
        }
        public Boolean kiemtra(string model,string ma, string ten)
        {

            Boolean check = false;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from linhkien where (model=@model or @model='') and (ma=@ma or @ma='') and (ten=@ten or @ten='')";
                    cmd.Parameters.AddWithValue("model", model);
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
        public List<linhkien> layds()
        {
            List<linhkien> ls = new List<linhkien>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM linhkien", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        String model = reader.GetString(reader.GetOrdinal("model"));
                        String ma = reader.GetString(reader.GetOrdinal("ma"));
                        String ten = reader.GetString(reader.GetOrdinal("ten"));
                        linhkien lk = new linhkien(model, ma, ten);
                        ls.Add(lk);
                    }
            }
            return ls;
        }
        public List<linhkien> layds(string model)
        {
            List<linhkien> ls = new List<linhkien>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from linhkien where (model=@model or @model='')";
                    cmd.Parameters.AddWithValue("model", model);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String ma1 = reader.GetString(reader.GetOrdinal("ma"));
                            String ten1 = reader.GetString(reader.GetOrdinal("ten"));
                            string model1 = reader.GetString(reader.GetOrdinal("model"));
                            linhkien lm = new linhkien();
                            lm.ma = ma1;
                            lm.ten = ten1;
                            lm.model = model1;
                            ls.Add(lm);
                        }
                    }
                        
                }
            }
            return ls;
        }
        public linhkien laythongtin(string model,string ma, string ten)
        {
            if (ma == "" && ten == "")
            {
                return new linhkien();
            }
            linhkien lm = new linhkien();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from linhkien where (ma=@ma or @ma='') and (ten=@ten or @ten='') and (model=@model or @model='')";
                    cmd.Parameters.AddWithValue("model", model);
                    cmd.Parameters.AddWithValue("ma", ma);
                    cmd.Parameters.AddWithValue("ten", ten);


                    using (var reader = cmd.ExecuteReader())
                        if (reader.Read())
                        {
                            String ma1 = reader.GetString(reader.GetOrdinal("ma"));
                            String ten1 = reader.GetString(reader.GetOrdinal("ten"));
                            string model1 = reader.GetString(reader.GetOrdinal("model"));
                            lm.ma = ma1;
                            lm.ten = ten1;
                            lm.model = model1;
                        }
                }
            }
            return lm;
        }
        public void them(string model, string ma, string ten)
        {
            bool check = kiemtra(model, ma, "");
            if(check)
            {
                MessageBox.Show("ma linh kien nay da ton tai vui longf nhap ma khac");
            }
            else
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO linhkien (model,ma,ten) VALUES (@model,@ma,@ten)";
                        cmd.Parameters.AddWithValue("model", model);
                        cmd.Parameters.AddWithValue("ma", ma);
                        cmd.Parameters.AddWithValue("ten", ten);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            
        }

        public void sua(string modelmoi, string modelcu,string tenmoi, string tencu, string mamoi, string macu)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE linhkien set model=@modelmoi, ten=@tenmoi,ma=@mamoi where model=@modelcu and ten=@tencu and ma=@macu";
                    cmd.Parameters.AddWithValue("modelmoi", modelmoi);
                    cmd.Parameters.AddWithValue("modelcu", modelcu);
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
