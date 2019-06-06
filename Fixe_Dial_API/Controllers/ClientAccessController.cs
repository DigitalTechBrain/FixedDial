using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fixe_Dial_API.Controllers
{
    public class ClientAccessController : ApiController
    {
        public IEnumerable<MembersMaster> Get()
        {
            using (Fixed_Dial_Entities_Con fixed_Dial_Entities = new Fixed_Dial_Entities_Con())
            {
                return fixed_Dial_Entities.MembersMasters.ToList();
            }
        }

        //Post Method with Location of Inserted Record
        public HttpResponseMessage Post([FromBody] MembersMaster apiObject)
        {
            //try
            //{

                using (Fixed_Dial_Entities_Con fixed_Dial_Entities = new Fixed_Dial_Entities_Con())
                {
                    fixed_Dial_Entities.MembersMasters.Add(apiObject);
                    fixed_Dial_Entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, apiObject);
                message.Headers.Location = new Uri(Request.RequestUri + apiObject.Member_ID.ToString());

                return message;
            }
            //}
            //catch (Exception ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            //}
        }
    }
}
