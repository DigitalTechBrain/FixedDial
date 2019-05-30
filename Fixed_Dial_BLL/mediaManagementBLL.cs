using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fixed_Dial_DAL;

namespace Fixed_Dial_BLL
{
    public class mediaManagementBLL
    {
        mediaManagementDAL mediaManagementDAL = new mediaManagementDAL();
        public void mediaManagement(string mediafileName, string mediaOriginalName, string mediaExtn, int mediaSize, DateTime CreatedDate, int adminID)
        {
            mediaManagementDAL.insertMediaDetails(mediafileName, mediaOriginalName, mediaExtn, mediaSize, CreatedDate, adminID);

        }
    }
}