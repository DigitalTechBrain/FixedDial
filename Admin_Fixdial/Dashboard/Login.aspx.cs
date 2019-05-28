using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fixed_Dial_BLL;

namespace Admin_Fixdial.Dashboard
{
    public partial class Login : System.Web.UI.Page
    {
        adminLoginBl adminLoginBl = new adminLoginBl();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            adminLoginCheck();
        }

        void adminLoginCheck()
        {
            bool loginResult =adminLoginBl.adminLoginBLL(txtMail.Text, txtPassword.Text);

            if(loginResult)
            {
                Session["Login"] = txtMail.Text;

                Response.Redirect("Home.aspx");
            }
            else
            {
                lblNotification.Text = "Wrong Credentials...!!!";
            }
        }

       
    }
}