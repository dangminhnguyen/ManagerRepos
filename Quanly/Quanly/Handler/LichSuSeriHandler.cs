using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quanly.Handler;
using Npgsql;
using Quanly.Model;

namespace Quanly.Handler
{
  
    class LichSuSeriHandler
    {
        String connString;
        public LichSuSeriHandler()
        {
            connString = new Property().ConnString;
        }

        public void them(int keyseri, DateTime ngaylapdat, int tinhtrang, string phutrach, string khachhang)
        {

            int pk = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO lichsuseri (keyseri,khachhang,kysuphutrach,ngaylapdat,tinhtrang) " +
                      "VALUES (@keyseri,@khachhang,@kysuphutrach,@ngaylapdat,@tinhtrang) RETURNING pk";

                    cmd.Parameters.AddWithValue("keyseri", keyseri);
                    cmd.Parameters.AddWithValue("khachhang", khachhang);
                    cmd.Parameters.AddWithValue("ngaylapdat", ngaylapdat);
                    cmd.Parameters.AddWithValue("tinhtrang", tinhtrang);
                    cmd.Parameters.AddWithValue("kysuphutrach", phutrach);
                    cmd.ExecuteNonQuery();
                }
            }
            
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
            string kysu, DateTime thoigian, string ghichu, string khachhang, string vitri)
        {
            int keyseri = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Update lichsu set hientrang=@hientrang," +
                        "kysu=@kysu,khacphuc=@khacphuc,ghichu=@ghichu,khachhang=@khachhang,vitri=@vitri,thoigian=@thoigian" +
                        " where pk=@pk RETURNING keyseri";
                    cmd.Parameters.AddWithValue("pk", pk);
                    cmd.Parameters.AddWithValue("thoigian", thoigian);
                    cmd.Parameters.AddWithValue("khacphuc", khacphuc);
                    cmd.Parameters.AddWithValue("hientrang", hientrang);
                    cmd.Parameters.AddWithValue("ghichu", ghichu);
                    cmd.Parameters.AddWithValue("kysu", kysu);
                    cmd.Parameters.AddWithValue("khachhang", khachhang);
                    cmd.Parameters.AddWithValue("vitri", vitri);
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
        public void xoafromseri(int keyseri)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Delete from  lichsu where keyseri=@keyseri";
                    cmd.Parameters.AddWithValue("keyseri", keyseri);

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
                            if (reader.IsDBNull(reader.GetOrdinal("khachhang")))
                            {
                                r.khachhang = null;
                            }
                            else
                            {
                                r.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            }

                            //if (reader.IsDBNull(reader.GetOrdinal("filepath")))
                            //{
                            r.filepath = "";
                            // }
                            // else
                            // {
                            //r.filepath = reader.GetString(reader.GetOrdinal("filepath"));
                            // }
                            if (reader.IsDBNull(reader.GetOrdinal("vitri")))
                            {
                                r.vitri = null;
                            }
                            else
                            {
                                r.vitri = reader.GetString(reader.GetOrdinal("vitri"));
                            }

                        }
                }
            }
            return r;
        }

        public List<Record> tracuu(int pk, DateTime from, DateTime to, String khachhang)
        {
            List<Record> list = new List<Record>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from lichsu where keyseri=@pk and (khachhang=@khachhang or @khachhang='')" +
                        "and (thoigian BETWEEN @from and @to) order by thoigian desc ";
                    cmd.Parameters.AddWithValue("pk", pk);
                    cmd.Parameters.AddWithValue("from", from);
                    cmd.Parameters.AddWithValue("to", to);
                    cmd.Parameters.AddWithValue("khachhang", khachhang);

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
                            if (reader.IsDBNull(reader.GetOrdinal("khachhang")))
                            {
                                r.khachhang = null;
                            }
                            else
                            {
                                r.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));

                            }
                            if (reader.IsDBNull(reader.GetOrdinal("vitri")))
                            {
                                r.vitri = null;
                            }
                            else
                            {
                                r.vitri = reader.GetString(reader.GetOrdinal("vitri"));

                            }
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

                            if (reader.IsDBNull(reader.GetOrdinal("khachhang")))
                            {
                                r.khachhang = null;
                            }
                            else
                            {
                                r.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));

                            }
                            if (reader.IsDBNull(reader.GetOrdinal("vitri")))
                            {
                                r.vitri = null;
                            }
                            else
                            {
                                r.vitri = reader.GetString(reader.GetOrdinal("vitri"));

                            }

                        }
                }
            }
            return r;
        }

    }
}
