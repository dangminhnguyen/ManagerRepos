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
    class LichSuHandler
    {
        String connString;
        public LichSuHandler()
        {
            connString = new Property().ConnString;
        }

        public int  them( int keyseri, string hientrang,string khacphuc,
            string kysu,DateTime thoigian,string ghichu)
        {
            
            int pk=0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO lichsu (keyseri,hientrang,khacphuc,kysu,thoigian,ghichu) " +
                        "VALUES (@keyseri,@hientrang,@khacphuc,@kysu,@thoigian,@ghichu) RETURNING pk";
                    cmd.Parameters.AddWithValue("keyseri", keyseri);
                    cmd.Parameters.AddWithValue("thoigian", thoigian);
                    cmd.Parameters.AddWithValue("khacphuc", khacphuc);
                    cmd.Parameters.AddWithValue("hientrang", hientrang);
                    cmd.Parameters.AddWithValue("ghichu", ghichu);
                    cmd.Parameters.AddWithValue("kysu", kysu);
                    
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            pk = reader.GetInt32(reader.GetOrdinal("pk"));
                        }
                }
            }
            return pk;
        }

        public void capNhatFile(int pk, string filepath)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Update lichsu set filepath=@filepath" +
                        " where pk=@pk";
                    cmd.Parameters.AddWithValue("pk", pk);
                    cmd.Parameters.AddWithValue(" filepath", filepath);            
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int sua(int pk, string hientrang, string khacphuc,
            string kysu, DateTime thoigian, string ghichu)
        {
            int keyseri = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = "Update lichsu set hientrang=@hientrang," +
                        "kysu=@kysu,khacphuc=@khacphuc,ghichu=@ghichu,thoigian=@thoigian" +
                        " where pk=@pk RETURNING keyseri";
                    cmd.Parameters.AddWithValue("pk", pk);
                    cmd.Parameters.AddWithValue("thoigian", thoigian);
                    cmd.Parameters.AddWithValue("khacphuc", khacphuc);
                    cmd.Parameters.AddWithValue("hientrang", hientrang);
                    cmd.Parameters.AddWithValue("ghichu", ghichu);
                    cmd.Parameters.AddWithValue("kysu", kysu);

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            keyseri = reader.GetInt32(reader.GetOrdinal("keyseri"));
                        }
                }
            }
            return keyseri;
        }

        public void xoa(int pk)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Delete from  lichsu where pk=@pk ";
                    cmd.Parameters.AddWithValue("pk", pk);
                   
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Record laythongtin(int pk)
        {
            Record r = new Record();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from lichsu where pk=@pk";
                    cmd.Parameters.AddWithValue("pk", pk);
                    

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            r.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            r.keyseri = reader.GetInt32(reader.GetOrdinal("keyseri"));
                            r.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            r.khacphuc = reader.GetString(reader.GetOrdinal("khacphuc"));
                            r.phutrach = reader.GetString(reader.GetOrdinal("kysu"));
                            r.ghichu = reader.GetString(reader.GetOrdinal("ghichu"));
                            r.thoigian = reader.GetDateTime(reader.GetOrdinal("thoigian"));


                            /*if (reader.IsDBNull(reader.GetOrdinal("filepath")))
                            {
                                r.filepath = "";
                            }
                            else
                            {
                                r.filepath = reader.GetString(reader.GetOrdinal("filepath"));
                            }
                            */
                            r.filepath = "nguyen";
                            

                        }
                }
            }
            return r;
        }

        public List<Record> tracuu(int pk,DateTime from,DateTime to)
        {
            List<Record> list = new List<Record>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from lichsu where keyseri=@pk " +
                        "and (thoigian BETWEEN @from and @to) order by thoigian desc ";
                    cmd.Parameters.AddWithValue("pk", pk);
                    cmd.Parameters.AddWithValue("from", from);
                    cmd.Parameters.AddWithValue("to", to);

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {

                            Record r = new Record();
                            r.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            r.keyseri= reader.GetInt32(reader.GetOrdinal("keyseri"));
                            r.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            r.khacphuc= reader.GetString(reader.GetOrdinal("khacphuc"));
                            r.phutrach = reader.GetString(reader.GetOrdinal("kysu"));
                            
                            r.ghichu = reader.GetString(reader.GetOrdinal("ghichu"));
                            r.thoigian = reader.GetDateTime(reader.GetOrdinal("thoigian"));
                            list.Add(r);
                        }
                }
            }
            return list;
        }

        public Record traCuuGanNhat(int pk)
        {
            Record r = new Record();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from lichsu where keyseri=@pk " +
                        "and thoigian=(Select max(thoigian) from lichsu where keyseri=@pk) ";
                    cmd.Parameters.AddWithValue("pk", pk);
              

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {                            
                            r.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            r.keyseri = reader.GetInt32(reader.GetOrdinal("keyseri"));
                            r.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            r.khacphuc = reader.GetString(reader.GetOrdinal("khacphuc"));
                            r.phutrach = reader.GetString(reader.GetOrdinal("kysu"));
                            r.ghichu = reader.GetString(reader.GetOrdinal("ghichu"));
                            r.thoigian = reader.GetDateTime(reader.GetOrdinal("thoigian"));
                            
                        }
                }
            }
            return r;
        }

        public List<Model.Record> traCuuGanNhatList(int pk)
        {
            List<Model.Record> list = new List<Model.Record>();
            
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from lichsu where keyseri=@pk " +
                        "and thoigian=(Select max(thoigian) from lichsu where keyseri=@pk) ";
                    cmd.Parameters.AddWithValue("pk", pk);


                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            Record r = new Record();
                            r.pk = reader.GetInt32(reader.GetOrdinal("pk"));
                            r.keyseri = reader.GetInt32(reader.GetOrdinal("keyseri"));
                            r.hientrang = reader.GetString(reader.GetOrdinal("hientrang"));
                            r.khacphuc = reader.GetString(reader.GetOrdinal("khacphuc"));
                            r.phutrach = reader.GetString(reader.GetOrdinal("kysu"));
                            r.ghichu = reader.GetString(reader.GetOrdinal("ghichu"));
                            r.thoigian = reader.GetDateTime(reader.GetOrdinal("thoigian"));
                            list.Add(r);
                        }
                }
            }
            return list;
        }

    }
}
