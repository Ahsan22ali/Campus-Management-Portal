using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CommonFn
    {
        public class Commonfnx
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SchoolCS"].ConnectionString);

            public void Query(string query)
            {
                if(conn.State==ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.ExecuteNonQuery();
                conn.Close();

             }
            public DataTable Fetch(string query)
            {
                if(conn.State==ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd= new SqlCommand(query,conn);
                SqlDataAdapter sda= new SqlDataAdapter(cmd);
                DataTable dt= new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }
    }
}