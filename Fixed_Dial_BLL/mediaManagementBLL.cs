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
          int mediaID = mediaManagementDAL.insertMediaDetails(mediafileName, mediaOriginalName, mediaExtn, mediaSize, CreatedDate, adminID);

            return mediaID;

        }


        public void updateMediaManagementBLLMethod(int ID,string mediafileName, string mediaOriginalName, string mediaExtn, int mediaSize,  int adminID)
        {
            mediaManagementDAL.updateMediaDetails(ID, mediafileName, mediaOriginalName, mediaExtn, mediaSize, adminID);
        }

        public int getMediaIDBLL(int id)
        {
          int iId =  mediaManagementDAL.getMediaID(id);
          return iId;
        }

        public void deleteMediaFileBLL(int mediaID)
        {
            mediaManagementDAL.deleteMediaFile(mediaID);
        }
    }
}