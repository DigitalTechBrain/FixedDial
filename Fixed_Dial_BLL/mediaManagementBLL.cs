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
        public int mediaManagement(string mediafileName, string mediaOriginalName, string mediaExtn, int mediaSize, DateTime CreatedDate, int adminID)
        {
          int mediaID =  mediaManagementDAL.insertMediaDetails(mediafileName, mediaOriginalName, mediaExtn, mediaSize, CreatedDate, adminID);

            return mediaID;

        }
    }
}