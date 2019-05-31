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
    public class subCategoryDAL
    {
        SqlConnection constr = new SqlConnection(@"server=JACK-PC\SQLEXPRESS;User ID=sa;Password=1234;Database = indiantr_fixeddial");

        public void insertCategory(int catID,string catName,string pageTitle,string metaKeyword,string metaDescription,int adminID,DateTime dateTime,int updateadminID)
        {
            
          
            string query = "subCategoryPro";
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@Cat_ID", catID));
            parameters.Add(new SqlParameter("@SubCatName", catName));
            parameters.Add(new SqlParameter("@PageTitle", pageTitle));
            parameters.Add(new SqlParameter("@MetaKeywork", metaKeyword));
            parameters.Add(new SqlParameter("@MedaDescription", metaDescription));
            parameters.Add(new SqlParameter("@CreateDate", DateTime.Now));
            parameters.Add(new SqlParameter("@CreateBy", adminID));
            parameters.Add(new SqlParameter("@UpdatedDate", dateTime));
            parameters.Add(new SqlParameter("@UpdatedBy", updateadminID));
            
            int rowsAffected = SqlHelper.ExecuteNonQuery(constr, CommandType.StoredProcedure, query, parameters.ToArray());
        }

        public DataSet bindCategory()
        {
            string query = "SELECT categoryName,categoryId FROM CategoryMaster";
            DataSet ds = SqlHelper.ExecuteDataset(constr, CommandType.Text, query);
            return ds;
        }
    }
}