using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fixed_Dial_DAL;

namespace Fixed_Dial_BLL
{
    public class adminLoginBl
    {
        

        adminLogin adminLoginDAL = new adminLogin();

        public bool adminLoginBLL (string aMail, string aPassword)
        {
            bool loginCheck = adminLoginDAL.adminLoginDetails(aMail,aPassword);
           

            return loginCheck;
        }
    }
}