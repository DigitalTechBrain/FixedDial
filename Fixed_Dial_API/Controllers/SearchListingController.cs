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
    public class SearchListingController : ApiController
    {
        [Route("api/SearchData")]
        [HttpGet, HttpPost]
      //  [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DfltConnection"].ToString());

            string query = String.Format("select categoryName from CategoryMaster like'{0}%'", prefixText);
            
            SqlDataAdapter da = new SqlDataAdapter(query,con);
            DataSet ds = new DataSet();
            da.Fill(ds, "CategoryMaster");
            int rcount, size;
            rcount = ds.Tables[0].Rows.Count;
            if (rcount >= count)
                size = count;
            else
                size = rcount;

            string[] pnames = new string[size];
            for (int i = 0; i < size; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];
                pnames[i] = row["categoryName"].ToString();
            }
            return pnames;

        }
    }
}
