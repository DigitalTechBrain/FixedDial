using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fixed_Dial_DAL;

namespace Fixed_Dial_BLL
{
    public class Admin_BLL
    {
        adminDAL adminDataAccess = new adminDAL();

        public void UserCreation(string aName, string aPhone, string aMail, string aAddress, string aCity, string aGender, string aPassword, string aRoll,DateTime registerDate, int aStatus)
        { 
            adminDataAccess.insertAdminData(aName, aPhone, aMail, aAddress, aCity, aGender, aPassword, aRoll,registerDate, aStatus);

        }

        public void mediaManagement(string mediafileName, string mediaOriginalName, string mediaExtn, int mediaSize, DateTime CreatedDate)
        {
            adminDataAccess.insertMediaDetails(mediafileName, mediaOriginalName, mediaExtn, mediaSize, CreatedDate);

        }
    }
}