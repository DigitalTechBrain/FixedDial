using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fixed_Dial_API.Controllers
{
    public class ClientAccessController : ApiController
    {
       

        public IEnumerable<MemberRegistration> Get()
        {
            using (indiantr_fixeddialEntities indiantr_FixeddialEntities = new indiantr_fixeddialEntities ())
            {
                {
                    List<MemberRegistration> listOfUsers = new List<MemberRegistration>();
                    foreach (var user in indiantr_FixeddialEntities.MemberRegistrations)
                    {
                        MemberRegistration userModel = new MemberRegistration();
                        userModel.MemberName = user.MemberName;
                        userModel.MemeberEmail = user.MemeberEmail;
                        userModel.MemberPassword = user.MemberPassword;
                        listOfUsers.Add(userModel);
                    }
                    IEnumerable<MemberRegistration> users = listOfUsers; return users;
                }

                return indiantr_FixeddialEntities.MemberRegistrations.ToList();
            }
        }
    }
}
