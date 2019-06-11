using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fixed_Dial_API.Controllers
{
    public class ClientAccessController : ApiController
    {

        [Route("api/deliveryitems")]
        [HttpGet]
        public IEnumerable<MemberRegistration> Delivery()
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
                    IEnumerable<MemberRegistration> users = listOfUsers;

                    return users;
                }

                
            }
        }

        //Post Method with Location of Inserted Record
        //Accept: application/json
        //Content-Type: application/json
        //{"MemberName":"Mack","MemeberEmail":"mack@mack.com","MemberPassword":"111"}
        [Route("api/clientRegistration")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] MemberRegistration apiObject)
        {
            try
            {

                using (indiantr_fixeddialEntities indiantr_FixeddialEntities = new indiantr_fixeddialEntities())
                {
                    indiantr_FixeddialEntities.MemberRegistrations.Add(apiObject);
                    indiantr_FixeddialEntities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, apiObject);
                    message.Headers.Location = new Uri(Request.RequestUri + apiObject.memberRegistration_ID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        

    }
}
