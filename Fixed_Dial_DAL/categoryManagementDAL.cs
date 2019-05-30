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
        SqlConnection constr = new SqlConnection(@"server=JACK-PC\SQLEXPRESS;User ID=sa;Password=1234;Database = indiantr_fixeddial");

       public void insertCategoryDALmethod(
           string categoryName,string pageTitle,string metaKeyword,string metaDescription,int mediaID,int adminID)
        {
            
            string query = "categoryMasterPro";
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@categoryName", categoryName));
            parameters.Add(new SqlParameter("@IsActive", 1));
            parameters.Add(new SqlParameter("@IsSelected", 1));
            parameters.Add(new SqlParameter("@mediaId", mediaID));
            parameters.Add(new SqlParameter("@pageTitle", pageTitle));
            parameters.Add(new SqlParameter("@metaKeyword", metaKeyword));
            parameters.Add(new SqlParameter("@metaDescription", metaDescription));
            parameters.Add(new SqlParameter("@createDate", DateTime.Now));
            parameters.Add(new SqlParameter("@createdBy", adminID));
            parameters.Add(new SqlParameter("@updatedDate", DateTime.Now));
            parameters.Add(new SqlParameter("@updatedBy", adminID));

            int rowsAffected = SqlHelper.ExecuteNonQuery(constr, CommandType.StoredProcedure, query, parameters.ToArray());
        }

        public void deleteCategoryDAL(int id)
        {
            
            string query = "DELETE FROM CategoryMaster WHERE categoryId = @id";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id", id));
            int rowsAffected = SqlHelper.ExecuteNonQuery(constr, CommandType.Text, query, parameters.ToArray());
        }

        public DataTable PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(@"server=JACK-PC\SQLEXPRESS;User ID=sa;Password=1234;Database = indiantr_fixeddial"))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM CategoryMaster", sqlCon);
                sqlDa.Fill(dtbl);

                return dtbl;
            }
        }

        public void updateCategoryDAL(
           int id, string categoryName, string pageTitle, string metaKeyword, string metaDescription, int mediaID, int adminID)
        {
            string query = "updateCategoryPro";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id", id));
            parameters.Add(new SqlParameter("@categoryName", categoryName));
            parameters.Add(new SqlParameter("@IsActive", 1));
            parameters.Add(new SqlParameter("@IsSelected", 1));
            parameters.Add(new SqlParameter("@mediaId", mediaID));
            parameters.Add(new SqlParameter("@pageTitle", pageTitle));
            parameters.Add(new SqlParameter("@metaKeyword", metaKeyword));
            parameters.Add(new SqlParameter("@metaDescription", metaDescription));
            parameters.Add(new SqlParameter("@createDate", DateTime.Now));
            parameters.Add(new SqlParameter("@createdBy", adminID));
            parameters.Add(new SqlParameter("@updatedDate", DateTime.Now));
            parameters.Add(new SqlParameter("@updatedBy", adminID));

            int rowsAffected = SqlHelper.ExecuteNonQuery(constr, CommandType.StoredProcedure, query, parameters.ToArray());
        }

        
    }
}