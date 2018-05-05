using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebCoreTest.Domain.Entities;
using WebCoreTest.Domain.Helpers;


namespace WebCoreTest.Domain.DB.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private MyAppSettings MyAppSettings { get; }

        public ProductRepository(MyAppSettings myAppSettings)
        {
            MyAppSettings = myAppSettings;
        }

        private string ConnectionString
        {
            get
            {
                 return MyAppSettings.ConnectionString;
            }
        }
    
        public int SaveOrUpdateNwmProduct(NwmProduct item)
        {
            String commandText = @"SaveOrUpdateNwmProduct";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.StoredProcedure;
            parameterList.Add(DatabaseUtility.GetSqlParameter("id", item.Id, SqlDbType.Int));
            parameterList.Add(DatabaseUtility.GetSqlParameter("storeid", item.StoreId, SqlDbType.Int));
            parameterList.Add(DatabaseUtility.GetSqlParameter("productcategoryid", item.ProductCategoryId, SqlDbType.Int));
            parameterList.Add(DatabaseUtility.GetSqlParameter("brandid", item.BrandId, SqlDbType.Int));
            parameterList.Add(DatabaseUtility.GetSqlParameter("retailerid", item.RetailerId, SqlDbType.Int));
            parameterList.Add(DatabaseUtility.GetSqlParameter("productcode", item.ProductCode.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("name", item.Name.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("description", item.Description.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("type", item.Type.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("mainpage", item.MainPage, SqlDbType.Bit));
            parameterList.Add(DatabaseUtility.GetSqlParameter("state", item.State, SqlDbType.Bit));
            parameterList.Add(DatabaseUtility.GetSqlParameter("ordering", item.Ordering, SqlDbType.Int));
            parameterList.Add(DatabaseUtility.GetSqlParameter("createddate", item.CreatedDate, SqlDbType.DateTime));
            parameterList.Add(DatabaseUtility.GetSqlParameter("imagestate", item.ImageState, SqlDbType.Bit));
            parameterList.Add(DatabaseUtility.GetSqlParameter("updateddate", item.UpdatedDate, SqlDbType.DateTime));
            parameterList.Add(DatabaseUtility.GetSqlParameter("price", item.Price, SqlDbType.Float));
            parameterList.Add(DatabaseUtility.GetSqlParameter("discount", item.Discount, SqlDbType.Float));
            parameterList.Add(DatabaseUtility.GetSqlParameter("unitsinstock", item.UnitsInStock, SqlDbType.Int));
            parameterList.Add(DatabaseUtility.GetSqlParameter("totalrating", item.TotalRating, SqlDbType.Int));
            parameterList.Add(DatabaseUtility.GetSqlParameter("videourl", item.VideoUrl.ToStr(), SqlDbType.NVarChar));
            int id = DatabaseUtility.ExecuteScalar(new SqlConnection(ConnectionString), commandText, commandType, parameterList.ToArray()).ToInt();
            return id;
        }

        public NwmProduct GetNwmProduct(int id)
        {
            String commandText = @"SELECT * FROM dbo.Products WHERE id=@id";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.Text;
            parameterList.Add(DatabaseUtility.GetSqlParameter("id", id, SqlDbType.Int));
            DataSet dataSet = DatabaseUtility.ExecuteDataSet(new SqlConnection(ConnectionString), commandText, commandType, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                using (DataTable dt = dataSet.Tables[0])
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var e = GetNwmProductFromDataRow(dr);
                        return e;
                    }
                }
            }
            return null;
        }

        public List<NwmProduct> GetNwmProducts()
        {
            var list = new List<NwmProduct>();
            String commandText = @"SELECT * FROM dbo.Products ORDER BY Id DESC";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.Text;
            DataSet dataSet = DatabaseUtility.ExecuteDataSet(new SqlConnection(ConnectionString), commandText, commandType, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                using (DataTable dt = dataSet.Tables[0])
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var e = GetNwmProductFromDataRow(dr);
                        list.Add(e);
                    }
                }
            }
            return list;
        }
        public  void DeleteNwmProduct(int id)
        {
  
            String commandText = @"DELETE FROM dbo.Products WHERE Id=@Id";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.Text;
            parameterList.Add(DatabaseUtility.GetSqlParameter("Id", id, SqlDbType.Int));
            DatabaseUtility.ExecuteNonQuery(new SqlConnection(ConnectionString), commandText, commandType, parameterList.ToArray());
        }

        private  NwmProduct GetNwmProductFromDataRow(DataRow dr)
        {
            var item = new NwmProduct();

            item.Id = dr["Id"].ToInt();
            item.StoreId = dr["StoreId"].ToInt();
            item.ProductCategoryId = dr["ProductCategoryId"].ToInt();
            item.BrandId = dr["BrandId"].ToInt();
            item.RetailerId = dr["RetailerId"].ToInt();
            item.ProductCode = dr["ProductCode"].ToStr();
            item.Name = dr["Name"].ToStr();
            item.Description = dr["Description"].ToStr();
            item.Type = dr["Type"].ToStr();
            item.MainPage = dr["MainPage"].ToBool();
            item.State = dr["State"].ToBool();
            item.Ordering = dr["Ordering"].ToInt();
            item.CreatedDate = dr["CreatedDate"].ToDateTime();
            item.ImageState = dr["ImageState"].ToBool();
            item.UpdatedDate = dr["UpdatedDate"].ToDateTime();
            item.Price = dr["Price"].ToFloat();
            item.Discount = dr["Discount"].ToFloat();
            item.UnitsInStock = dr["UnitsInStock"].ToInt();
            item.TotalRating = dr["TotalRating"].ToInt();
            item.VideoUrl = dr["VideoUrl"].ToStr();
            return item;
        }


    }
}
