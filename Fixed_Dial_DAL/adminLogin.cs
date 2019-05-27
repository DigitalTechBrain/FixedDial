using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Fixed_Dial_DAL
{
    public class adminLogin
    {
        SqlConnection con = new SqlConnection(@"server=JACK-PC\SQLEXPRESS;User ID=sa;Password=1234;Database = indiantr_fixeddial");

        static string Encrypt(string value) // Encryption of Password
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

        public bool adminLoginDetails(string aMail, string aPassword)
        {
            using (SqlCommand sqlCommand = new SqlCommand("adminLogin", con))
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    using (DataTable dataTable = new DataTable())
                    {
                        using (DataSet dataSet = new DataSet())
                      {
                            
                            con.Open();

                            sqlCommand.Parameters.AddWithValue("@aMail", SqlDbType.NVarChar).Value = aMail;
                            sqlCommand.Parameters.AddWithValue("@aPassword", SqlDbType.NVarChar).Value = Encrypt(aPassword);

                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlDataAdapter.SelectCommand = sqlCommand;
                            sqlDataAdapter.Fill(dataTable);
                            sqlDataAdapter.Fill(dataSet);

                            con.Close();

                            bool loginSuccessful = ((dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0));

                            return loginSuccessful;

                           
                        }
                    }
                }


            }

            
        }

    }
}