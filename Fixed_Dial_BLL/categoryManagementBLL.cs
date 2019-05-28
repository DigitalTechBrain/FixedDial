using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fixed_Dial_DAL;

namespace Fixed_Dial_BLL
{
  
    public class categoryManagementBLL
    {
        categoryManagementDAL cmanagDAL = new categoryManagementDAL();

        public void categoryMagementBlMethod(string categoryName, int IsActive, int IsSelected, int mediaId, string pageTitle, string metaKeyword, string metaDescription, DateTime createDate, int createdBy, DateTime updatedDate, int updatedBy)
        {
            cmanagDAL.insertCategoriesDAL(categoryName, IsActive, IsSelected, mediaId, pageTitle, metaKeyword, metaDescription, createDate, createdBy, updatedDate, updatedBy);
        }
    }
}