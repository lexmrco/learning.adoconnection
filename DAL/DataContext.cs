using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Learning.Ado.DAL
{
    class DataContext
    {
        public string _ConnectionString = "Data Source=192.168.0.15;Initial Catalog=learning;User ID=sa;Password=p@ssw0rd";

        public void ExecuteQuery(Action<SqlDataReader> action, string commandText, CommandType commandType, IDictionary<string, object> parameters)
        {
            SqlConnection conn = null;

            // 1. Instantiate the connection
            //SqlConnection conn = this._SqlConnection;
            conn = new SqlConnection(_ConnectionString);


            // 3. Pass the connection to a command object
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }

            SqlDataReader rdr = null;
            try
            {
                // 2. Open the connection
                conn.Open();

                //3. Set the connection to the sql command
                cmd.Connection = conn;

                // get query results
                rdr = cmd.ExecuteReader();


                // print the CustomerID of each record
                while (rdr.Read())
                {
                    action.Invoke(rdr);

                }

            }
            catch (Exception e)
            {
                Type type = e.GetType();
                if (type.Name.Contains("Sql"))
                    throw e;
                throw new Exception(string.Format("{0}", e.Message));
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void ExecuteNonQuery(Action<int> action, string commandText, CommandType commandType, IDictionary<string, object> parameters)
        {
            SqlConnection conn = null;
            int result = 0;
            // 1. Instantiate the connection
            //SqlConnection conn = this._SqlConnection;
            conn = new SqlConnection(_ConnectionString);


            // 3. Pass the connection to a command object
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }

            try
            {
                // 2. Open the connection
                conn.Open();

                //3. Set the connection to the sql command
                cmd.Connection = conn;

                // get query results
                result = cmd.ExecuteNonQuery();

                action.Invoke(result);

            }
            catch (Exception e)
            {

                throw new Exception(string.Format("{0}", e.Message));
            }
            finally
            {

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
