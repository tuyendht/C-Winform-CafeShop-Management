using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeShop_ASM.DAO
{
    public class DataAccess
    {
        private string connectionStr = @"Data Source=SE140046\SQLEXPRESS;Initial Catalog=CafeShop_Managerment;Integrated Security=True";

        private static DataAccess instance;
        
        public static DataAccess Instance {
            get { if (instance == null) instance = new DataAccess(); return DataAccess.instance; }
            private set { DataAccess.instance = value; }
        }
        private DataAccess() { }

        public DataTable ExecuteQuery(string sql)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionStr)){

                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    adapter.Fill(data);
                }
                catch (SqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return data;
        }
        public int ExecuteNonQuery(string sql)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    data = command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return data;
        }
    }
}

