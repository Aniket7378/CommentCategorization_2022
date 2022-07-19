using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GenericLibrary.Database
{
    public class SqlDbConnection : ISqlDbConnection
    {
        private string _sqlConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=CommentDB;Integrated Security=SSPI;MultipleActiveResultSets=true;";
        public SqlConnection SqlConnectionToDb { get; set; }

        public SqlDbConnection()
        {
            SqlConnectionToDb = new SqlConnection(_sqlConnectionString);
        }

        public void Open()
        {
            if (SqlConnectionToDb != null && SqlConnectionToDb.State == ConnectionState.Closed)
            {
                SqlConnectionToDb.Open();
            }
        }

        public void Close()
        {
            if (SqlConnectionToDb != null && SqlConnectionToDb.State == ConnectionState.Open)
            {
                SqlConnectionToDb.Close();
            }
        }
    }
}
