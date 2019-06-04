using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Fixed_Dial_DAL;

namespace Fixed_Dial_BLL
{
    public class microCategoryManagementBLL
    {
        microCategoryManagementDAL microCategoryManagementDAL = new microCategoryManagementDAL();

        public void insertSubCategoryBLL(int catID, string catName, string pageTitle, string metaKeyword, string metaDescription, int adminID, DateTime dateTime, int updateadminID)
        {
            microCategoryManagementDAL.insertCategory(catID, catName, pageTitle, metaKeyword, metaDescription, adminID, dateTime, updateadminID);
        }

        public DataSet bindCategryBLL()
        {
            DataSet ds = microCategoryManagementDAL.bindCategory();
            return ds;
        }

        public DataSet populateMicroCat()
        {
            DataSet ds = microCategoryManagementDAL.populateMicroCategory();
            return ds;
        }

        public void deleteSubCategoryBLL(int id)
        {
            microCategoryManagementDAL.deleteSubCategoryDAL(id);
        }

        public void updateSubCategoryBLL(int id, string categoryName, string pageTitle, string metaKeyword, string metaDescription, int adminID)
        {
            microCategoryManagementDAL.updateCategoryDAL(id, categoryName, pageTitle, metaKeyword, metaDescription, adminID);
        }
    }
}
