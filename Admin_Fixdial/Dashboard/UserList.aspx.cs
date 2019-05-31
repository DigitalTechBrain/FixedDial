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
    public partial class UserList : System.Web.UI.Page
    {
        UserListBLL userListBLL = new UserListBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            bindAdmin();
        }

        void bindAdmin()
        {
            DataSet ds = userListBLL.userLIstBLL_Method();


            userListGrid.DataSource = ds.Tables[0];
            userListGrid.DataBind();
        }

        protected void userListGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "suspendAdmin")
            {
                int crow;
                crow = Convert.ToInt32(e.CommandArgument.ToString());
                string v = userListGrid.Rows[crow].Cells[3].Text;

                userListBLL.suspendAdminBLL_Method(v);
            }

            if (e.CommandName == "activateAdmin")
            {
                int crow;
                crow = Convert.ToInt32(e.CommandArgument.ToString());
                string v = userListGrid.Rows[crow].Cells[3].Text;

                userListBLL.activateAdminBLL_Method(v);
            }
            
            bindAdmin();
        }

        protected void userListGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}