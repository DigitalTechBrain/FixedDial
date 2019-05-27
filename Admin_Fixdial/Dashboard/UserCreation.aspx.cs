using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fixed_Dial_BLL;

namespace Admin_Fixdial.Dashboard
{
    public partial class UserCreation : System.Web.UI.Page
    {
        Admin_BLL admin_BLL = new Admin_BLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            insertMedia();
            Admin_Creation();
            lblNotification.Text = "Admin Created Successfully.....!!!";

            txtName.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtPhone.Text = string.Empty;
            
        }

        void Admin_Creation()
        {
            admin_BLL.UserCreation(txtName.Text, txtPhone.Text, txtMail.Text,txtAddress.Text, txtCity.Text, dropdownGender.Text, txtConfirmPassword.Text, dropdownRole.Text, DateTime.Now, 1);

        }

        void insertMedia()
        {

            string fName = fileuploadImage.FileName;
            string oName = fileuploadImage.FileName;
            string fExt = System.IO.Path.GetExtension(fileuploadImage.FileName);
            int fSize = Convert.ToInt16(Math.Round(((decimal)fileuploadImage.PostedFile.ContentLength / (decimal)1024), 2));


            string directory = Server.MapPath("~/Dashboard/Images/AdminImages/");

            string changeName = DateTime.Now.ToString("ddmmmmyyyyhhmmssffffff") + fExt;

            this.fileuploadImage.SaveAs(Path.Combine(directory, changeName));

            admin_BLL.mediaManagement(changeName, oName, fExt, fSize, DateTime.Now);
        }
    }
}