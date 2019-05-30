using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fixed_Dial_DAL;

namespace Fixed_Dial_BLL
{
    public class adminIDBLL
    {
        adminIdDAL adminIdDAL = new adminIdDAL();

        public int adminMail(string aMail)
        {
            int aID = adminIdDAL.adminInfo(aMail);

            return aID;
        }
    }
}