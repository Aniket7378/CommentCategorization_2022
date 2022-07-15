using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLibrary.Database
{
    public interface ISqlDbConnection
    {
        SqlConnection SqlConnectionToDb { get; set; }

        void Open();

        void Close();
    }
}
