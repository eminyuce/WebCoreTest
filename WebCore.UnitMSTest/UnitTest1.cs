using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebCoreTest.Domain.Helpers;

namespace WebCore.UnitMSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
.SetBasePath(AppContext.BaseDirectory)
.AddJsonFile("appsettings.Development.json")
.Build();

     

            Console.WriteLine(configuration.GetConnectionString("DefaultConnection"));
            GetAllTables(configuration.GetConnectionString("DefaultConnection"));
            GetSelectedTableMetaData(configuration.GetConnectionString("DefaultConnection"), "TestEY.dbo.Products");
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
        public void GetSelectedTableMetaData(string connectionString,string selectedTable)
        {


            var builder = new SqlConnectionStringBuilder(connectionString);
            var con = new SqlConnection(builder.ConnectionString);
            con.Open();

            string[] objArrRestrict;
            var tParts = selectedTable.Split(".".ToCharArray());
            objArrRestrict = new string[] {
                tParts[0],
                tParts[1],
                tParts[2],
                null };
            DataTable tbl = con.GetSchema(SqlClientMetaDataCollectionNames.Columns, objArrRestrict);

            SqlDataAdapter da = new SqlDataAdapter();

            #region Get Primary Key
            String primaryKey = "";
            DataTable ttt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from " + selectedTable);
            cmd.Connection = con;
            SqlDataAdapter daa = new SqlDataAdapter();
            daa.SelectCommand = cmd;
            //da.Fill(tl);
            daa.FillSchema(ttt, SchemaType.Mapped);
            primaryKey = DataTableHelper.GetPrimaryKeys(ttt);

            #endregion

            List<TableRowMetaData> TableRowMetaDataList = new List<TableRowMetaData>();

                int i = 0;

                foreach (DataRow rowTable in tbl.Rows)
                {
                    String TABLE_CATALOG = rowTable["TABLE_CATALOG"].ToStr();
                    String TABLE_SCHEMA = rowTable["TABLE_SCHEMA"].ToStr();
                    String TABLE_NAME = rowTable["TABLE_NAME"].ToStr();
                    String COLUMN_NAME = rowTable["COLUMN_NAME"].ToStr();
                    String ORDINAL_POSITION = rowTable["ORDINAL_POSITION"].ToStr();
                    String COLUMN_DEFAULT = rowTable["COLUMN_DEFAULT"].ToStr();
                    String IS_NULLABLE = rowTable["IS_NULLABLE"].ToStr();
                    String DATA_TYPE = rowTable["DATA_TYPE"].ToStr();
                    String CHARACTER_MAXIMUM_LENGTH = rowTable["CHARACTER_MAXIMUM_LENGTH"].ToStr();
                    String CHARACTER_OCTET_LENGTH = rowTable["CHARACTER_OCTET_LENGTH"].ToStr();
                    String NUMERIC_PRECISION = rowTable["NUMERIC_PRECISION"].ToStr();
                    String NUMERIC_PRECISION_RADIX = rowTable["NUMERIC_PRECISION_RADIX"].ToStr();
                    String NUMERIC_SCALE = rowTable["NUMERIC_SCALE"].ToStr();
                    String DATETIME_PRECISION = rowTable["DATETIME_PRECISION"].ToStr();
                    String CHARACTER_SET_CATALOG = rowTable["CHARACTER_SET_CATALOG"].ToStr();
                    String CHARACTER_SET_SCHEMA = rowTable["CHARACTER_SET_SCHEMA"].ToStr();
                    String CHARACTER_SET_NAME = rowTable["CHARACTER_SET_NAME"].ToStr();
                    String COLLATION_CATALOG = rowTable["COLLATION_CATALOG"].ToStr();
                    String IS_SPARSE = rowTable["IS_SPARSE"].ToStr();
                    String IS_COLUMN_SET = rowTable["IS_COLUMN_SET"].ToStr();
                    String IS_FILESTREAM = rowTable["IS_FILESTREAM"].ToStr();


                    var k = new TableRowMetaData();
                    k.ColumnName = COLUMN_NAME;
                    k.DataType = DATA_TYPE;
                    k.IsNull = IS_NULLABLE;
                    k.MaxChar = CHARACTER_MAXIMUM_LENGTH;
                    k.DataTypeMaxChar = k.DataType;
                    if (k.DataType.Contains("varchar"))
                    {
                        k.MaxChar = CHARACTER_MAXIMUM_LENGTH.Equals("-1") ? "4000" : CHARACTER_MAXIMUM_LENGTH;
                        k.DataTypeMaxChar = k.DataType + "(" + k.MaxChar + ")";
                    }
                    k.Order = ORDINAL_POSITION.ToInt();
                    k.ID = ++i;
                    k.PrimaryKey = COLUMN_NAME == primaryKey;
                    TableRowMetaDataList.Add(k);
                }
            con.Close();



        }
        public class TableRowMetaData
        {
            public String ColumnName { set; get; }
            public String ColumnNameInput { get { return String.Format("p_{0}", ColumnName); } }
            public String IsNull { set; get; }
            public String DataType { set; get; }
            public String MaxChar { set; get; }
            public String DataTypeMaxChar { set; get; }
            public String CssClass { set; get; }
            public int Order { set; get; }
            public int ID { set; get; }
            public bool PrimaryKey { set; get; }
            public String ControlID { set; get; }
            public bool ForeignKey { get; set; }
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
