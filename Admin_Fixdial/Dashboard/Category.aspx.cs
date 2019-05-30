using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fixed_Dial_BLL;
using System.Data.SqlClient;
using System.Data;

namespace Admin_Fixdial.Dashboard
{
    public partial class Category : System.Web.UI.Page
    {
        categoryManagementBLL categoryManagementBLL = new categoryManagementBLL();

        string connectionString = @"server=JACK-PC\SQLEXPRESS;User ID=sa;Password=1234;Database = indiantr_fixeddial";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
            }

            //insertCategory();
        }

        void PopulateGridview()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM CategoryMaster", sqlCon);
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                categoryGridview.DataSource = dtbl;
                categoryGridview.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                categoryGridview.DataSource = dtbl;
                categoryGridview.DataBind();
                categoryGridview.Rows[0].Cells.Clear();
                categoryGridview.Rows[0].Cells.Add(new TableCell());
                categoryGridview.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                categoryGridview.Rows[0].Cells[0].Text = "No Data Found ..!";
                categoryGridview.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void categoryGridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           // try
           // {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO CategoryMaster (categoryName,pageTitle,metaKeyword,metaDescription) VALUES (@categoryName,@pageTitle,@metaKeyword,@description)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@categoryName", (categoryGridview.FooterRow.FindControl("txtCategoryNameFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@pageTitle", (categoryGridview.FooterRow.FindControl("txtPageTitleFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@metaKeyword", (categoryGridview.FooterRow.FindControl("txtMetaKeywordFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@description", (categoryGridview.FooterRow.FindControl("txtMetaDescriptionFooter") as TextBox).Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        PopulateGridview();
                        lblSuccessMessage.Text = "New Record Added";
                        lblErrorMessage.Text = "";
                    }
                }
           // }
            //catch (Exception ex)
            //{
            //    lblSuccessMessage.Text = "";
            //    lblErrorMessage.Text = ex.Message;
            //}
        }

        protected void categoryGridview_RowEditing(object sender, GridViewEditEventArgs e)
        {
            categoryGridview.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        protected void categoryGridview_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            categoryGridview.EditIndex = -1;
            PopulateGridview();
        }

        protected void categoryGridview_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE CategoryMaster SET categoryName=@categoryName,pageTitle=@pageTitle,metaKeyword=@metaKeyword,metaDescription=@metaDescription WHERE categoryId = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@categoryName", (categoryGridview.Rows[e.RowIndex].FindControl("txtCategoryName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@pageTitle", (categoryGridview.Rows[e.RowIndex].FindControl("txtPageTitle") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@metaKeyword", (categoryGridview.Rows[e.RowIndex].FindControl("txtMetaKeyword") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@metaDescription", (categoryGridview.Rows[e.RowIndex].FindControl("txtMetaDescription") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(categoryGridview.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    categoryGridview.EditIndex = -1;
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void categoryGridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM CategoryMaster WHERE categoryId = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(categoryGridview.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview();
                    lblSuccessMessage.Text = "Selected Record Deleted";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
    }


        //void insertCategory()
        //{
        //    categoryManagementBLL.categoryMagementBlMethod("Test",1,1,1,"Page Title","Law","Added",DateTime.Now,1,DateTime.Now,1);
        //}


    }
