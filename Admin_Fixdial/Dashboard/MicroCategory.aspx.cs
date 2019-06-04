using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fixed_Dial_BLL;

namespace Admin_Fixdial.Dashboard
{
    public partial class MicroCategory : System.Web.UI.Page
    {
        microCategoryManagementBLL microCategoryManagementBLL = new microCategoryManagementBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindCategory();
                populateGrid();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Update")
            {
                microCategoryManagementBLL.updateSubCategoryBLL(Convert.ToInt32(Cache["MicroCatID"].ToString()), txtSubCategoryName.Text, txtPageTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, Convert.ToInt32(Cache["adminID"].ToString()));
                txtSubCategoryName.Text = string.Empty;
                txtPageTitle.Text = string.Empty;
                txtMetaKeyword.Text = string.Empty;
                txtMetaDescription.Text = string.Empty;

                bindCategory();
                populateGrid();

                lblNotification.Text = "Micro Category Updated Successfully...!!!";
            }

            else
            {
                subCategoryInsert();
                bindCategory();
                populateGrid();

                lblNotification.Text = "Micro Category Submitted Successfully...!!!";

                txtSubCategoryName.Text = string.Empty;
                txtPageTitle.Text = string.Empty;
                txtMetaKeyword.Text = string.Empty;
                txtMetaDescription.Text = string.Empty;
            }
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

                Cache["MicroCatID"] = id.ToString();

                btnSubmit.Text = "Update";

            }
            if (e.CommandName == "deleteSubCategory")
            {
                microCategoryManagementBLL.deleteSubCategoryBLL(id);
                populateGrid();
            }
        }

        void bindCategory()
        {

            DataSet dss = microCategoryManagementBLL.bindCategryBLL();

            ddwnCategoryDropDown.DataSource = dss.Tables[0];
            ddwnCategoryDropDown.DataTextField = "SubCatName";
            ddwnCategoryDropDown.DataValueField = "SubCat_ID";
            ddwnCategoryDropDown.DataBind();




        }

        void subCategoryInsert()
        {
            int catID = Convert.ToInt32(ddwnCategoryDropDown.SelectedValue.ToString());
            microCategoryManagementBLL.insertSubCategoryBLL(catID, txtSubCategoryName.Text, txtPageTitle.Text, txtMetaKeyword.Text, txtMetaDescription.Text, Convert.ToInt32(Cache["adminID"].ToString()), DateTime.Now, Convert.ToInt32(Cache["adminID"].ToString()));
        }

        void populateGrid()
        {
            DataSet ds = microCategoryManagementBLL.populateMicroCat();
            populateSubCategoryGrid.DataSource = ds.Tables[0];
            populateSubCategoryGrid.DataBind();
        }

    }
}