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
        Dictionary<string, string> modelsource = new Dictionary<string, string>();
        Dictionary<string, string> loaimaysource = new Dictionary<string, string>();
        Dictionary<string, string> khachhangsource = new Dictionary<string, string>();
        Dictionary<string, string> kysusource = new Dictionary<string, string>();
        String connString;
        public TongHandler()
        {
            connString = new Property().ConnString;
            setup();
        }

        private void setup()
        {
            LoaiMayHandler lh = new LoaiMayHandler();
            List<LoaiMay> ls = lh.layds();
            foreach (Model.LoaiMay lm in ls)
            {
                loaimaysource.Add(lm.ma, lm.ten);
                //MessageBox.Show(loaimaysource[lm.ma]);
            }

            ModelHandler mo = new ModelHandler();
            List<Model.Model> mo1 = mo.layds();
            foreach (Model.Model m in mo1)
            {
                modelsource.Add(m.ma, m.ten);
            }

            KhachHangHandler kh = new KhachHangHandler();
            List<Model.KhachHang> khl = kh.layds();
            foreach (Model.KhachHang k in khl)
            {
                khachhangsource.Add(k.ma, k.ten);
            }
            KySuHandler ks = new KySuHandler();
            List<Model.KySu> ksl = ks.layds();
            foreach (Model.KySu k in ksl)
            {
                kysusource.Add(k.ma, k.ten);
            }

        }
        public List<Model.tong> layds()
        {
            List<Model.tong> ls = new List<Model.tong>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from tong";
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            int pk = reader.GetInt32(reader.GetOrdinal("keyseri"));
                            Model.tong m = new Model.tong();
                            m.keyseri = pk;
                            ls.Add(m);
                        }
                }
                conn.Close();
            }
            return ls;
        }

        public void them(int serikey, string seri, string mamay, string mamodel, string makhachhang)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO tong (keyseri,seri,mamay,loaimay,mamodel,model,makhachhang,khachhang)" +
                        " VALUES (@keyseri,@seri,@mamay,@loaimay,@mamodel,@model,@makhachhang,@khachhang)";
                    cmd.Parameters.AddWithValue("keyseri", serikey);
                    cmd.Parameters.AddWithValue("seri", seri);
                    cmd.Parameters.AddWithValue("mamay", mamay);
                    cmd.Parameters.AddWithValue("loaimay",loaimaysource[mamay]);
                    cmd.Parameters.AddWithValue("mamodel", mamodel);
                    cmd.Parameters.AddWithValue("model", modelsource[mamodel]);
                    cmd.Parameters.AddWithValue("makhachhang", makhachhang);
                    cmd.Parameters.AddWithValue("khachhang", khachhangsource[makhachhang]);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void sua( string seri,string sericu, string mamay, string mamodel, string makhachhang)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                  cmd.Connection = conn;
                    cmd.CommandText = "UPDATE tong SET seri=@seri,khachhang=@khachhang where seri=@sericu " +
                        "and loaimay=@loaimay and model=@model";
                    cmd.Parameters.AddWithValue("seri", seri);
                    cmd.Parameters.AddWithValue("sericu", sericu);
                    cmd.Parameters.AddWithValue("loaimay",loaimaysource[mamay]);
                    cmd.Parameters.AddWithValue("model",modelsource[mamodel]);
                    cmd.Parameters.AddWithValue("khachhang", khachhangsource[makhachhang]);
                    cmd.ExecuteNonQuery();
                    
                }
            }
        }

        public void updateHientrang(int keyseri,string hientrang, string lichsu, DateTime thoigian, string kysu)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE tong SET hientrang=@hientrang,phutrach=@phutrach, thoigian=@thoigian,lichsu=@lichsu where keyseri=@keyseri";
                    cmd.Parameters.AddWithValue("hientrang", hientrang);
                    cmd.Parameters.AddWithValue("keyseri", keyseri);
                    cmd.Parameters.AddWithValue("phutrach", kysusource[kysu]);
                    cmd.Parameters.AddWithValue("thoigian", thoigian);
                    cmd.Parameters.AddWithValue("lichsu", lichsu);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        private List<Model.Record> getlichsu(int serikey)
        {
            
            LichSuHandler ls = new LichSuHandler();
            List<Model.Record> r = new List<Model.Record>();
            
            Model.Record re = ls.traCuuGanNhat(serikey);
            r.Add(re);

            KySuHandler kih = new KySuHandler();
            KySu kysu = kih.laythongtin(re.phutrach, "");
            return r;
        }



    }
   
}
