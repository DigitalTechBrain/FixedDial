using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fixed_Dial_DAL
{
    public class adminIdDAL
    {
        SqlConnection con = new SqlConnection(@"server=JACK-PC\SQLEXPRESS;User ID=sa;Password=1234;Database = indiantr_fixeddial");

        public int currentAdminID;

        public int adminInfo(string aMail)
        {
            using (SqlCommand sqlCommand = new SqlCommand("getAdminID", con))
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    using (DataTable dataTable = new DataTable())
                    {
                        con.Open();

                        sqlCommand.Parameters.AddWithValue("@aMail", SqlDbType.VarChar).Value = aMail;

                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlDataAdapter.SelectCommand = sqlCommand;


                        currentAdminID = Convert.ToInt32(sqlCommand.ExecuteScalar());

                        con.Close();
                    }
                }


            }

            return currentAdminID;
        }


    
    }
}