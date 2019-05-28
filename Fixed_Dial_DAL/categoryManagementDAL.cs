using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

namespace Fixed_Dial_DAL
{
    public class categoryManagementDAL
    {
       public void insertCategoriesDAL(string categoryName,int IsActive,int IsSelected,int mediaId,string pageTitle,string metaKeyword,string metaDescription,DateTime createDate,int createdBy,DateTime updatedDate,int updatedBy)
        {
           // string constr = ConfigurationManager.ConnectionStrings["DfltConnection"].ConnectionString;

            SqlConnection constr = new SqlConnection(@"server=JACK-PC\SQLEXPRESS;User ID=sa;Password=1234;Database = indiantr_fixeddial");

            string query = "categoryMasterPro";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@categoryName", categoryName));
            parameters.Add(new SqlParameter("@IsActive", IsActive));
            parameters.Add(new SqlParameter("@IsSelected", IsSelected));
            parameters.Add(new SqlParameter("@mediaId", mediaId));
            parameters.Add(new SqlParameter("@pageTitle", pageTitle));
            parameters.Add(new SqlParameter("@metaKeyword", metaKeyword));
            parameters.Add(new SqlParameter("@metaDescription", metaDescription));
            parameters.Add(new SqlParameter("@createDate", createDate));
            parameters.Add(new SqlParameter("@createdBy", createdBy));
            parameters.Add(new SqlParameter("@updatedDate", updatedDate));
            parameters.Add(new SqlParameter("@updatedBy", updatedBy));

            int rowsAffected = SqlHelper.ExecuteNonQuery(constr, CommandType.StoredProcedure, query, parameters.ToArray());
        }

    }
}