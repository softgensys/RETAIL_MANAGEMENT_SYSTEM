﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Odbc;
using MySql.Data.MySqlClient;
using System.Data.Common;


namespace softgen
{
     public class DbConnector
       {
           private OdbcConnection connection;
           public string connectionString;

           public DbConnector()
           {
               connectionString = "Dsn=softgen_db_my;uid=root";
           }

           public bool OpenConnection()
           {
               try
               {
                   connection = new OdbcConnection(connectionString);
                   connection.Open();
                   return true;
               }
               catch (Exception ex)
               {
                   // Handle the exception or display an error message
                   Console.WriteLine(ex.Message);
                   return false;
               }
           }


        public bool ConnectSGS_db()
        {
            try
            {
               
               connectionString = "Server=localhost;Database=softgen_db;Uid=root";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                // You are now connected to the MySQL database.

                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the connection.
                // You can also log the error message for debugging purposes.
                //VBError(ex, "DBObjects", "ConnectSGS_db");
                return false;
            }
        }

        public OdbcConnection GetConnection()
        {
            return new OdbcConnection(connectionString);
        }

        public void CloseConnection()
           {
               connection.Close();
           }

           public DataTable ExecuteQuery(string query)
           {
               DataTable dataTable = new DataTable();
               try
               {
                   if (OpenConnection())
                   {
                       OdbcCommand cmd = new OdbcCommand(query, connection);
                       OdbcDataAdapter adapter = new OdbcDataAdapter(cmd);
                       adapter.Fill(dataTable);
                       CloseConnection();
                   }
               }
               catch (Exception ex)
               {
                   // Handle the exception or display an error message
                   Console.WriteLine(ex.Message);
               }
               return dataTable;
           }

        public OdbcDataReader CreateResultset(string query)
        {
            try
            {
                if (OpenConnection())
                {
                    OdbcCommand cmd = new OdbcCommand(query,connection);
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {

                // Handle the exception or display an error message
                Console.WriteLine(ex.Message);

            }
            return null;

        }


           public int ExecuteNonQuery(string query)
           {
               int rowsAffected = 0;
               try
               {
                   if (OpenConnection())
                   {
                       OdbcCommand cmd = new OdbcCommand(query, connection);
                       rowsAffected = cmd.ExecuteNonQuery();
                       CloseConnection();
                   }
               }
               catch (Exception ex)
               {
                   // Handle the exception or display an error message
                   Console.WriteLine(ex.Message);
               }
               return rowsAffected;
           }

        public OdbcDataReader ExecuteReader(string query, OdbcParameter[] parameters)
        {
            OdbcDataReader reader = null;

            try
            {
                if (OpenConnection())
                {
                    OdbcCommand cmd = new OdbcCommand(query, connection);

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // You can handle exceptions as needed
            }

            return reader;
        }


        /////////////////

    }

}

