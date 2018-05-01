using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WebCoreTest.Domain.Helpers;

namespace WebCoreTest.Domain.DB.Repositories
{
    public class MySqlRepository
    {
        public List<NwmHaberler> GetNwmHaberlers(string connectionString)
        {
            var list = new List<NwmHaberler>();
            String commandText = @"SELECT * FROM db_kodyazan.haberler ORDER BY id DESC";
            var parameterList = new List<MySqlParameter>();
            DataSet dataSet = MySqlHelper.ExecuteDataset(connectionString, commandText, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                list = dataSet.Tables[0].ToList<NwmHaberler>();
            }
            return list;
        }
        public class NwmHaberler
        {
            public int id { get; set; }
            public int sira { get; set; }
            public DateTime tarih { get; set; }
            public int durum { get; set; }
            public string seo { get; set; }
            public string link { get; set; }
            public string baslik_tr { get; set; }
            public string keywords_tr { get; set; }
            public string ozet_tr { get; set; }
            public string detay_tr { get; set; }
            public string baslik_en { get; set; }
            public string keywords_en { get; set; }
            public string ozet_en { get; set; }
            public string detay_en { get; set; }
            public DateTime haberTarihi { get; set; }
            public string baslik_de { get; set; }
            public string keywords_de { get; set; }
            public string ozet_de { get; set; }
            public string detay_de { get; set; }
            public int tip { get; set; }
            public string baslik_ar { get; set; }
            public string keywords_ar { get; set; }
            public string ozet_ar { get; set; }
            public string detay_ar { get; set; }
            public string yorum_tr { get; set; }
            public string yorum_en { get; set; }
        }
    }
}
