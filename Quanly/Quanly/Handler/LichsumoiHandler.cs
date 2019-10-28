using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Quanly.Model;
namespace Quanly.Handler
{
    class LichsumoiHandler
    {
        String connString;
        public LichsumoiHandler()
        {
            connString = new Property().ConnString;
        }

        public int them(int keyseri, string khachhang,string lklay,string kslay,string lydolay,DateTime ngaylay,string ttrlay
            ,DateTime ngayPh,string ksPh,string ttrPhLay,string srmayphlap,string vtrmoiPhlay,string ghichuphlay,string ttrphlap,
            string srmayphlay,string xuatsu,string ghichuphlap)
        {

            int pk = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO lichsum (keyseri,khachhang,linhkienlay,kysulay,lydolay,ngaylay" +
                        ",tinhtranglay,ngayphanhoi,kysuphanhoi,tinhtrangphlay,serimayphlap,vitrimoiphlay,ghichuphlay," +
                        "tinhtrangphlap,serimayphlay,xuatsu,ghichuphlap) VALUES (@keyseri,@khachhang,@linhkienlay,@kysulay" +
                        ",@lydolay,@ngaylay" +
                        ",@tinhtranglay,@ngayphanhoi,@kysuphanhoi,@tinhtrangphlay,@serimayphlap,@vitrimoiphlay,@ghichuphlay" +
                        "," +
                        "@tinhtrangphlap,@serimayphlay,@xuatsu,@ghichuphlap)";

                    cmd.Parameters.AddWithValue("keyseri", keyseri);
                    cmd.Parameters.AddWithValue("khachhang", khachhang);
                    cmd.Parameters.AddWithValue("linhkienlay", lklay);
                    cmd.Parameters.AddWithValue("kysulay", kslay);
                    cmd.Parameters.AddWithValue("lydolay", lydolay);
                    cmd.Parameters.AddWithValue("ngaylay", ngaylay);
                    cmd.Parameters.AddWithValue("tinhtranglay", ttrlay);
                    cmd.Parameters.AddWithValue("ngayphanhoi", ngayPh);
                    cmd.Parameters.AddWithValue("kysuphanhoi", ksPh);
                    cmd.Parameters.AddWithValue("tinhtrangphlay", ttrPhLay);
                    cmd.Parameters.AddWithValue("serimayphlap", srmayphlap);
                    cmd.Parameters.AddWithValue("vitrimoiphlay", vtrmoiPhlay);
                    cmd.Parameters.AddWithValue("ghichuphlay", ghichuphlay);
                    cmd.Parameters.AddWithValue("tinhtrangphlap", ttrphlap);
                    cmd.Parameters.AddWithValue("serimayphlay", srmayphlay);
                    cmd.Parameters.AddWithValue("xuatsu", xuatsu);
                    cmd.Parameters.AddWithValue("ghichuphlap", ghichuphlap);
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

        public List<RecordNew> tracuu(int pk, DateTime from, DateTime to)
        {
            List<RecordNew> list = new List<RecordNew>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from lichsum where keyseri=@pk and (ngaylay BETWEEN @from and @to) order by ngaylay desc ";
                    cmd.Parameters.AddWithValue("pk", pk);
                    cmd.Parameters.AddWithValue("from", from);
                    cmd.Parameters.AddWithValue("to", to);

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {

                            RecordNew r = new RecordNew();
                            r.pk = reader.GetInt32(reader.GetOrdinal("pk"));


                            r.khachhang = reader.GetString(reader.GetOrdinal("khachhang"));
                            r.lklay = reader.GetString(reader.GetOrdinal("linhkienlay"));
                            r.kslay = reader.GetString(reader.GetOrdinal("kysulay"));
                            r.lydolay = reader.GetString(reader.GetOrdinal("lydolay"));
                            r.ngaylay = reader.GetDateTime(reader.GetOrdinal("ngaylay"));
                            r.ttrlay = reader.GetString(reader.GetOrdinal("tinhtranglay"));
                            r.ngayPh = reader.GetDateTime(reader.GetOrdinal("ngayphanhoi"));
                            r.ksPh = reader.GetString(reader.GetOrdinal("kysuphanhoi"));
                            r.ttrPhLay = reader.GetString(reader.GetOrdinal("tinhtrangphlay"));
                            r.srmayphlap = reader.GetString(reader.GetOrdinal("serimayphlap"));
                            r.vtrmoiPhlay = reader.GetString(reader.GetOrdinal("vitrimoiphlay"));
                            r.ghichuphlay = reader.GetString(reader.GetOrdinal("ghichuphlay"));
                            r.ttrphlap = reader.GetString(reader.GetOrdinal("tinhtrangphlap"));
                            r.srmayphlay = reader.GetString(reader.GetOrdinal("serimayphlay"));
                            r.xuatsu = reader.GetString(reader.GetOrdinal("xuatsu"));
                            r.ghichuphlap = reader.GetString(reader.GetOrdinal("ghichuphlap"));


                            if (reader.IsDBNull(reader.GetOrdinal("khachhang")))
                            {

                            }
                            else
                            {


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
