using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace Fixed_Dial_DAL
{
    public class UserListDAL
    {
        SqlConnection con = new SqlConnection(@"server=JACK-PC\SQLEXPRESS;User ID=sa;Password=1234;Database = indiantr_fixeddial");

        public DataSet bindUserDAL()
        {
          
            string query = "select * from AdminAssistant";
            DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, query);

            return ds;
           
        }

        public void suspendAdminDAL(string mail)
        {
            string query2 = "Update AdminAssistant set aStatus = '" + false + "' where  aMail = @aMail";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@aMail", mail));
            int rowsAffected = SqlHelper.ExecuteNonQuery(con, CommandType.Text, query2, parameters.ToArray());
        }

        public void activateAdminDAL(string mail)
        {
            string query2 = "Update AdminAssistant set aStatus = '" + true + "' where  aMail = @aMail";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@aMail", mail));
            int rowsAffected = SqlHelper.ExecuteNonQuery(con, CommandType.Text, query2, parameters.ToArray());
        }

    }
}