using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebCoreTest.Domain.Helpers;


namespace WebCoreTest.Domain.DB.Repositories
{
    public class TableRepository
    {

        public void GetSelectedMysqlTableMetaData(string mySqlConnectionString, string selectedTable)
        {

            //            To get column information with SqlClient or other providers you do: 
            //DataTable schema = conn.GetSchema("Columns", new string[4] { conn.Database, null, "products", null });

            //            With MySQL Connector / NET is different: 
            //DataTable schema = conn.GetSchema("Columns", new string[4] { null, conn.Database, "products", null });

            //            Note that the first 2 items in the array are swapped. 

            var con = new MySqlConnection(mySqlConnectionString);
            con.Open();

            string[] objArrRestrict;
            var tParts = selectedTable.Split(".".ToCharArray());
            objArrRestrict = new string[] {null,
                con.Database,
                tParts[2],
                null
                 };
            DataTable tbl = con.GetSchema("Columns", objArrRestrict);


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
                    //String NUMERIC_PRECISION = rowTable.Table.Columns.Contains("NUMERIC_PRECISION") ? rowTable["NUMERIC_PRECISION "].ToStr() : "";
                    //String NUMERIC_SCALE = rowTable["NUMERIC_SCALE"].ToStr();
                    //String CHARACTER_SET_NAME = rowTable["CHARACTER_SET_NAME"].ToStr();
                    //String COLLATION_NAME = rowTable["COLLATION_NAME"].ToStr();
                    //String COLUMN_TYPE = rowTable["COLUMN_TYPE"].ToStr();
                    String COLUMN_KEY = DataTableHelper.GetValue(rowTable, "COLUMN_KEY").ToStr();
                    //String EXTRA = rowTable["EXTRA"].ToStr();
                    //String PRIVILEGES = rowTable["PRIVILEGES"].ToStr();
                    //String COLUMN_COMMENT = rowTable["COLUMN_COMMENT"].ToStr();


                    var k = new TableRowMetaData();
                    k.ID = i++;

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
                    k.PrimaryKey = COLUMN_KEY.Equals("PRI", StringComparison.InvariantCultureIgnoreCase);

                    TableRowMetaDataList.Add(k);

            }
            con.Close();



        }

        public void GetAllMySqlTables(String connectionString)
        {
            MySqlConnection con = new MySqlConnection(connectionString);

            try
            {

                con.Open();

                DataTable tblDatabases =
                                con.GetSchema("Tables");

                var list = new List<TableMetaData>();

                foreach (DataRow rowTable in tblDatabases.Rows)
                {
                    var i = new TableMetaData();
                    String TABLE_CATALOG = rowTable["TABLE_CATALOG"].ToStr();
                    String TABLE_SCHEMA = rowTable["TABLE_SCHEMA"].ToStr();
                    String TABLE_NAME = rowTable["TABLE_NAME"].ToStr();
                    String TABLE_TYPE = rowTable["TABLE_TYPE"].ToStr();
                    String ENGINE = rowTable["ENGINE"].ToStr();
                    String VERSION = rowTable["VERSION"].ToStr();
                    String ROW_FORMAT = rowTable["ROW_FORMAT"].ToStr();
                    String TABLE_ROWS = rowTable["TABLE_ROWS"].ToStr();
                    String AVG_ROW_LENGTH = rowTable["AVG_ROW_LENGTH"].ToStr();
                    String DATA_LENGTH = rowTable["DATA_LENGTH"].ToStr();
                    String MAX_DATA_LENGTH = rowTable["MAX_DATA_LENGTH"].ToStr();
                    String INDEX_LENGTH = rowTable["INDEX_LENGTH"].ToStr();
                    String DATA_FREE = rowTable["DATA_FREE"].ToStr();
                    String AUTO_INCREMENT = rowTable["AUTO_INCREMENT"].ToStr();
                    String CREATE_TIME = rowTable["CREATE_TIME"].ToStr();
                    String UPDATE_TIME = rowTable["UPDATE_TIME"].ToStr();
                    String CHECK_TIME = rowTable["CHECK_TIME"].ToStr();
                    String TABLE_COLLATION = rowTable["TABLE_COLLATION"].ToStr();
                    String CHECKSUM = rowTable["CHECKSUM"].ToStr();
                    String CREATE_OPTIONS = rowTable["CREATE_OPTIONS"].ToStr();
                    String TABLE_COMMENT = rowTable["TABLE_COMMENT"].ToStr();

                    i.TableCatalog = TABLE_CATALOG;
                    i.TableSchema = TABLE_SCHEMA;
                    i.TableName = TABLE_NAME;
                    i.TableType = TABLE_TYPE;
                    list.Add(i);
                }

                con.Close();

            }
            catch (Exception e)
            {

            }

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
        public void GetSelectedTableMetaData(string connectionString, string selectedTable)
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
