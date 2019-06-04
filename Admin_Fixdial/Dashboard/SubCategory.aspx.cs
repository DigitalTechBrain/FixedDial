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
            if(!IsPostBack)
            { 
            bindCategory();
            populateGrid();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(btnSubmit.Text == "Update")
            {
                subCategoryBLL.updateSubCategoryBLL(Convert.ToInt32(Cache["SubCatID"].ToString()), txtSubCategoryName.Text, txtPageTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, Convert.ToInt32(Cache["adminID"].ToString()));
                txtSubCategoryName.Text = string.Empty;
                txtPageTitle.Text = string.Empty;
                txtMetaKeyword.Text = string.Empty;
                txtMetaDescription.Text = string.Empty;

                bindCategory();
                populateGrid();

                lblNotification.Text = "Sub Category Updated Successfully...!!!";
            }

            else
            {
                subCategoryInsert();
                bindCategory();
                populateGrid();

                lblNotification.Text = "Sub Category Submitted Successfully...!!!";

                txtSubCategoryName.Text = string.Empty;
                txtPageTitle.Text = string.Empty;
                txtMetaKeyword.Text = string.Empty;
                txtMetaDescription.Text = string.Empty;
            }
            
        }

        

        void bindCategory()
        {
          
                DataSet ds = subCategoryBLL.bindCategryBLL();
               
            ddwnCategoryDropDown.DataSource = ds.Tables[0];
            ddwnCategoryDropDown.DataTextField = "categoryName";
            ddwnCategoryDropDown.DataValueField = "categoryId";
            ddwnCategoryDropDown.DataBind();

            


        }

        void subCategoryInsert()
        {
            int catID = Convert.ToInt32(ddwnCategoryDropDown.SelectedValue.ToString());
            subCategoryBLL.insertSubCategoryBLL(catID,txtSubCategoryName.Text,txtPageTitle.Text,txtMetaKeyword.Text,txtMetaDescription.Text,Convert.ToInt32(Cache["adminID"].ToString()),DateTime.Now, Convert.ToInt32(Cache["adminID"].ToString()));
        }

        void populateGrid()
        {
            DataSet ds = subCategoryBLL.populateSubCat();
            populateSubCategoryGrid.DataSource = ds.Tables[0];
            populateSubCategoryGrid.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(populateSubCategoryGrid.DataKeys[0].Value.ToString());

            if (e.CommandName == "editSubCategory")
            {
                int crow;
                crow = Convert.ToInt32(e.CommandArgument.ToString());
                txtSubCategoryName.Text = populateSubCategoryGrid.Rows[crow].Cells[0].Text;
                txtPageTitle.Text = populateSubCategoryGrid.Rows[crow].Cells[1].Text;
                txtMetaKeyword.Text = populateSubCategoryGrid.Rows[crow].Cells[2].Text;
                txtMetaDescription.Text = populateSubCategoryGrid.Rows[crow].Cells[3].Text;

                Cache["SubCatID"] = id.ToString();

                btnSubmit.Text = "Update";

            }
            if (e.CommandName == "deleteSubCategory")
            {
                subCategoryBLL.deleteSubCategoryBLL(id);
                populateGrid();
            }
        }

        
    }
}