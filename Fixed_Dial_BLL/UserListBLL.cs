using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Fixed_Dial_DAL;

namespace Fixed_Dial_BLL
{
    public class UserListBLL
    {
        UserListDAL userListDAL = new UserListDAL();

        public DataSet userLIstBLL_Method()
        {
            DataSet ds = userListDAL.bindUserDAL();
            return ds;
        }

        public void suspendAdminBLL_Method(string mail)
        {
            userListDAL.suspendAdminDAL(mail);
        }

        public void activateAdminBLL_Method(string mail)
        {
            userListDAL.activateAdminDAL(mail);
        }
    }
}