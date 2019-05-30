using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fixed_Dial_DAL;
using System.Data;

namespace Fixed_Dial_BLL
{
  
    public class categoryManagementBLL
    {
        categoryManagementDAL cmanagDAL = new categoryManagementDAL();

        public DataTable PopulateGridViewMethod()
        {
            DataTable dataTable =  cmanagDAL.PopulateGridview();

            return dataTable;
        }

        public void insertCategoryBLLmethod(string categoryName, string pageTitle, string metaKeyword, string metaDescription,int mediaID,int adminID)
        {
            cmanagDAL.insertCategoryDALmethod(categoryName, pageTitle, metaKeyword, metaDescription,mediaID, adminID);
        }

        public void deleteCategoryBLLmethod(int id)
        {
            cmanagDAL.deleteCategoryDAL(id);
        }

        public void updateCategoryBLLmethod(int id,string categoryName, string pageTitle, string metaKeyword, string metaDescription,int mediaID,int adminID)
        {
            cmanagDAL.updateCategoryDAL(id,categoryName, pageTitle, metaKeyword, metaDescription, mediaID, adminID);
        }
    }
}