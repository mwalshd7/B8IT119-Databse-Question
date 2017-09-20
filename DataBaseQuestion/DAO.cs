using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//New Using
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DataBaseQuestion
{
    class DAO
    {
        SqlConnection con;

        public DAO()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);
        }

        public SqlConnection OpenCon()
        {
            if (con.State==ConnectionState.Closed || con.State==ConnectionState.Broken)
            {
                con.Open();
            }
            return con;
        }

        public SqlConnection CloseCon()
        {
            if (con.State==ConnectionState.Open)
            {
                con.Close();
            }
            return con;
        }
    }
}
