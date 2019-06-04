using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Linq;
using System.Web;

namespace Fixed_Dial_DAL
{
    public class microCategoryManagementDAL
    {
        SqlConnection constr = new SqlConnection(@"server=JACK-PC\SQLEXPRESS;User ID=sa;Password=1234;Database = indiantr_fixeddial");

        public void insertCategory(int catID, string catName, string pageTitle, string metaKeyword, string metaDescription, int adminID, DateTime dateTime, int updateadminID)
        {


            string query = "microCategoryInsertPro";
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@SubCat_ID", catID));
            parameters.Add(new SqlParameter("@MicroCatName", catName));
            parameters.Add(new SqlParameter("@PageTitle", pageTitle));
            parameters.Add(new SqlParameter("@MetaKeyword", metaKeyword));
            parameters.Add(new SqlParameter("@MetaDescription", metaDescription));
            parameters.Add(new SqlParameter("@CreateDate", DateTime.Now));
            parameters.Add(new SqlParameter("@CreateBy", adminID));
            parameters.Add(new SqlParameter("@UpdatedDate", dateTime));
            parameters.Add(new SqlParameter("@UpdatedBy", updateadminID));

            int rowsAffected = SqlHelper.ExecuteNonQuery(constr, CommandType.StoredProcedure, query, parameters.ToArray());
        }

        public DataSet bindCategory()
        {
            string query = "SELECT SubCat_ID,SubCatName FROM SubCategoryManager";
            DataSet ds = SqlHelper.ExecuteDataset(constr, CommandType.Text, query);
            return ds;
        }

        public DataSet populateMicroCategory()
        {
            string query = "SELECT * FROM MicroCategory";
            DataSet ds = SqlHelper.ExecuteDataset(constr, CommandType.Text, query);
            return ds;
        }

        public void deleteSubCategoryDAL(int id)
        {

            string query = "DELETE FROM MicroCategory WHERE MicroCat_ID = @id";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id", id));
            int rowsAffected = SqlHelper.ExecuteNonQuery(constr, CommandType.Text, query, parameters.ToArray());
        }

        public void updateCategoryDAL(
           int id, string categoryName, string pageTitle, string metaKeyword, string metaDescription, int adminID)
        {
            string query = "microCategoryUpdationPro";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id", id));
            parameters.Add(new SqlParameter("@MicroCatName", categoryName));
            parameters.Add(new SqlParameter("@PageTitle", pageTitle));
            parameters.Add(new SqlParameter("@MetaKeyword", metaKeyword));
            parameters.Add(new SqlParameter("@MetaDescription", metaDescription));
            parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now));
            parameters.Add(new SqlParameter("@UpdatedBy",  adminID));

            int rowsAffected = SqlHelper.ExecuteNonQuery(constr, CommandType.StoredProcedure, query, parameters.ToArray());
        }


    }
}
