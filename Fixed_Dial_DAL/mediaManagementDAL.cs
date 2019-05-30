using Microsoft.ApplicationBlocks.Data;
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


        public  int getMediaID(int id)
        {

            string query = "select mediaID from CategoryMaster where categoryID = @Name;SELECT SCOPE_IDENTITY();";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Name", id));
           
            int mediaID = Convert.ToInt32(SqlHelper.ExecuteScalar(con, CommandType.Text, query, parameters.ToArray()));

            return mediaID;
        }

        public void deleteMediaFile(int mID)
        {
            string query = "delete  from MediaMaster where mediaID = @mediaID";
           
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@mediaID", mID));
            
            int rowsAffected = SqlHelper.ExecuteNonQuery(con, CommandType.Text, query, parameters.ToArray());


        }

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

        public void updateMediaDetails(int id,string mediafileName, string mediaOriginalName, string mediaExtn, int mediaSize, int adminID)
        {

            string query = "updateMediaDetails";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id", id));
            parameters.Add(new SqlParameter("@mediafileName", mediafileName));
            parameters.Add(new SqlParameter("@mediaOriginalName", mediaOriginalName));
            parameters.Add(new SqlParameter("@mediaExtn", mediaExtn));
            parameters.Add(new SqlParameter("@mediaSize", mediaSize));
            parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            parameters.Add(new SqlParameter("@CreatedBy", adminID));
            parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
            parameters.Add(new SqlParameter("@UpdatedBy", adminID));


            int rowsAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, query, parameters.ToArray());

           
        }
    }
}