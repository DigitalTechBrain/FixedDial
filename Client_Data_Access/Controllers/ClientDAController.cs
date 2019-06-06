using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Client_Data_Access.Controllers
{
    public class ClientDAController : ApiController
    {
        public IEnumerable<MemberRegistration> Get()
        {
            using (Client_DA_Entities client_DA_Entities = new Client_DA_Entities())
            {
                return client_DA_Entities.MemberRegistrations.ToList();
            }
        }
    }
}
