using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Fixed_Dial_DAL
{
    public class mediaManagementDAL
    {
        SqlConnection con = new SqlConnection(@"server=JACK-PC\SQLEXPRESS;User ID=sa;Password=1234;Database = indiantr_fixeddial");

        public int currentMediaID;

        public int insertMediaDetails(string mediafileName, string mediaOriginalName, string mediaExtn, int mediaSize, DateTime CreatedDate, int adminID)
        {

            using (SqlCommand sqlCommand = new SqlCommand("mediaDetails", con))
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    using (DataTable dataTable = new DataTable())
                    {
                        con.Open();

                        sqlCommand.Parameters.AddWithValue("@mediafileName", SqlDbType.VarChar).Value = mediafileName;
                        sqlCommand.Parameters.AddWithValue("@mediaOriginalName", SqlDbType.VarChar).Value = mediaOriginalName;
                        sqlCommand.Parameters.AddWithValue("@mediaExtn", SqlDbType.VarChar).Value = mediaExtn;
                        sqlCommand.Parameters.AddWithValue("@mediaSize", SqlDbType.Int).Value = mediaSize;
                        sqlCommand.Parameters.AddWithValue("@CreatedDate", SqlDbType.DateTime).Value = CreatedDate;
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", SqlDbType.Int).Value = adminID;
                        sqlCommand.Parameters.AddWithValue("UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                        sqlCommand.Parameters.AddWithValue("@UpdatedBy", SqlDbType.Int).Value = adminID;

                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlDataAdapter.SelectCommand = sqlCommand;


                        currentMediaID = Convert.ToInt32(sqlCommand.ExecuteScalar());

                        con.Close();

                        return currentMediaID;
                    }
                }


            }
        }
    }
}