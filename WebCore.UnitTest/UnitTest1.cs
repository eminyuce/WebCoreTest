using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace WebCore.UnitTest
{
    public class UnitTest1
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder();

            return builder.Build();
        }
        int Add(int x, int y)
        {
            return x + y;
        }
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        [Fact]
        public void Test1()
        {
            Console.WriteLine("Test");
            var config = GetConfig();
            var con = config.GetConnectionString("DefaultConnection");
            Console.WriteLine(con);

            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppContext.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

            Console.WriteLine(configuration.GetConnectionString("ConnectionStrings"));

            //GetAllTables(con);
        }

        public void GetAllTables(String connectionString)
        {
            SqlConnection con =
                          new SqlConnection(connectionString);

            try
            {

                con.Open();


                DataTable tblDatabases =
                                con.GetSchema(
                                           SqlClientMetaDataCollectionNames.Tables);


                var list = new List<TableMetaData>();
                foreach (DataRow rowDatabase in tblDatabases.Rows)
                {
                    var i = new TableMetaData();
                    i.TableCatalog = rowDatabase["table_catalog"].ToString();
                    i.TableSchema = rowDatabase["table_schema"].ToString();
                    i.TableName = rowDatabase["table_name"].ToString();
                    i.TableType = rowDatabase["table_type"].ToString();
                    list.Add(i);
                }

                con.Close();

            }
            catch (Exception e)
            {

            }
        }
        public class TableMetaData
        {
            public string TableCatalog { get; set; }
            public string TableName { get; set; }
            public string TableType { get; set; }
            public string TableSchema { get; set; }

            public string DatabaseTableName
            {
                get
                {
                    return String.Format("{0}.{1}.{2}", TableCatalog, TableSchema, TableName);
                }
            }
            public string TableNameWithSchema
            {
                get
                {
                    return String.Format("{0}.{1}", TableSchema, TableName);
                }
            }
        }

    }
}
