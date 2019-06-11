using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.ApplicationBlocks.Data;

namespace Fixed_Dial_API.Controllers
{
    public class LoginAuthenticationController : ApiController
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["DfltConnection"].ToString());

        [Route("api/ClientLoginAuthentication")]
        [HttpGet,HttpPost]
        public bool Login([System.Web.Http.FromBody] MemberRegistration lgn)
        {
                    string query = "MemberAuthenticationPro";
           
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@MemeberEmail", lgn.MemeberEmail));
                    parameters.Add(new SqlParameter("@MemberPassword", lgn.MemberPassword));

                    DataSet ds = SqlHelper.ExecuteDataset(constr, CommandType.StoredProcedure, query, parameters.ToArray());

                    bool loginSuccessful = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
                    return loginSuccessful;

        }

    }
}
