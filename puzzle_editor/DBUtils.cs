using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace puzzle_editor
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "127.0.0.1";
            int port = 3306;
            string database = "model";
            string username = "root";
            string password = "abc8734";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}
