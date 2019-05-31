using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fixed_Dial_BLL;

namespace Admin_Fixdial.Dashboard
{
    public partial class SubCategory : System.Web.UI.Page
    {
        subCategoryBLL subCategoryBLL = new subCategoryBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            bindCategory();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            subCategoryInsert();
        }

        void bindCategory()
        {
          
                DataSet ds = subCategoryBLL.bindCategryBLL();
               
            ddwnCategoryDropDown.DataSource = ds.Tables[0];
            ddwnCategoryDropDown.DataTextField = "categoryName";
            ddwnCategoryDropDown.DataValueField = "categoryId";
            ddwnCategoryDropDown.DataBind();

            //string str = ddwnCategoryDropDown.SelectedValue.ToString();


        }

        void subCategoryInsert()
        {
            int catID = Convert.ToInt32(ddwnCategoryDropDown.SelectedValue.ToString());
            subCategoryBLL.insertSubCategoryBLL(catID,txtSubCategoryName.Text,txtPageTitle.Text,txtMetaKeyword.Text,txtMetaDescription.Text,1,DateTime.Now,1);
        }
    }
}