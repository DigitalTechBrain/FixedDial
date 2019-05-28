using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fixed_Dial_BLL;

namespace Admin_Fixdial.Dashboard
{
    public partial class Category : System.Web.UI.Page
    {
        categoryManagementBLL categoryManagementBLL = new categoryManagementBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            insertCategory();
        }

        void insertCategory()
        {
            categoryManagementBLL.categoryMagementBlMethod("Test",1,1,1,"Page Title","Law","Added",DateTime.Now,1,DateTime.Now,1);
        }
    }
}