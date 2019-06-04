using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Fixed_Dial_DAL;

namespace Fixed_Dial_BLL
{
    public class subCategoryBLL
    {
        subCategoryDAL subCategoryDAL = new subCategoryDAL();

        public void insertSubCategoryBLL(int catID, string catName, string pageTitle, string metaKeyword, string metaDescription, int adminID, DateTime dateTime, int updateadminID)
        {
            subCategoryDAL.insertCategory(catID, catName, pageTitle, metaKeyword, metaDescription, adminID, dateTime, updateadminID);
        }

        public DataSet bindCategryBLL()
        {
            DataSet ds = subCategoryDAL.bindCategory();
            return ds;
        }

        public DataSet populateSubCat()
        {
            DataSet ds = subCategoryDAL.populateSubCategory();
            return ds;
        }

        public void deleteSubCategoryBLL(int id)
        {
            subCategoryDAL.deleteSubCategoryDAL(id);
        }

        public void updateSubCategoryBLL(int id, string categoryName, string pageTitle, string metaKeyword, string metaDescription, int adminID)
        {
            subCategoryDAL.updateCategoryDAL( id,  categoryName,  pageTitle, metaKeyword,  metaDescription,  adminID);
        }
    }
}