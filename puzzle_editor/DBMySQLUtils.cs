﻿using MySql.Data.MySqlClient;

namespace puzzle_editor
{
    class DBMySQLUtils
    {
        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            string connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }

    }
}
