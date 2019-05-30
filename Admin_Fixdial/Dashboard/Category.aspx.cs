using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fixed_Dial_BLL;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Fixed_Dial_BLL;


namespace Admin_Fixdial.Dashboard
{
    public partial class Category : System.Web.UI.Page
    {
        categoryManagementBLL categoryManagementBLL = new categoryManagementBLL();
        mediaManagementBLL mediaManagementBLL = new mediaManagementBLL();
        adminIDBLL adminIDBLL = new adminIDBLL();

        public int adminID;
        public int currentMediaID;

        string connectionString = @"server=JACK-PC\SQLEXPRESS;User ID=sa;Password=1234;Database = indiantr_fixeddial";

        protected void Page_Load(object sender, EventArgs e)
        {
            adminInfoPL();

            if (!IsPostBack)
            {
                PopulateGridview();
            }

            //insertCategory();
        }

       

        void PopulateGridview()
        {
            
            
            DataTable dtbl = new DataTable();
            dtbl = categoryManagementBLL.PopulateGridViewMethod();
           
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

        //Getting Admin ID
        void adminInfoPL()
        {
            int aID = adminIDBLL.adminMail(Session["Login"].ToString());
            Cache["adminID"] = aID;
        }
        //Inserting Media with Admin ID
        void mediaUpload()
        {
            FileUpload fileUpload = (FileUpload)categoryGridview.FooterRow.FindControl("FileUploadFooter");
            string fName = fileUpload.FileName;
            string oName = fileUpload.FileName;
            string fExt = System.IO.Path.GetExtension(fileUpload.FileName);
            int fSize = Convert.ToInt16(Math.Round(((decimal)fileUpload.PostedFile.ContentLength / (decimal)1024), 2));


            string directory = Server.MapPath("~/Dashboard/Images/Category/");

            string changeName = DateTime.Now.ToString("ddmmmmyyyyhhmmssffffff") + fExt;

            fileUpload.SaveAs(Path.Combine(directory, changeName));

             adminID = Convert.ToInt16(Cache["adminID"]);
             currentMediaID = mediaManagementBLL.mediaManagement(changeName, oName, fExt, fSize, DateTime.Now, adminID);
        }

        protected void categoryGridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            mediaUpload();

            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO CategoryMaster (categoryName,pageTitle,metaKeyword,metaDescription,IsActive,IsSelected,createDate,updatedDate,createdBy,updatedBy,mediaId)" +
                            " VALUES (@categoryName,@pageTitle,@metaKeyword,@description,1,1,'"+DateTime.Now+"','"+DateTime.Now+"','"+ adminID + "','" + adminID + "','"+ currentMediaID + "')";
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
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
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
