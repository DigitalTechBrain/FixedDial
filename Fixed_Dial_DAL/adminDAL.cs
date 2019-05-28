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
    

    public class adminDAL
    {
        
        public int currentMediaID;
        public int currentAdminID;

        //SqlConnection con = new SqlConnection(WebConfigurationManager.AppSettings["AdminConnection"].ToString());
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

        public int adminInfo(string aMail)
        {
            using (SqlCommand sqlCommand = new SqlCommand("getAdminID", con))
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    using (DataTable dataTable = new DataTable())
                    {
                        con.Open();

                        sqlCommand.Parameters.AddWithValue("@aMail", SqlDbType.VarChar).Value = aMail;
                        
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlDataAdapter.SelectCommand = sqlCommand;


                        currentAdminID = Convert.ToInt32(sqlCommand.ExecuteScalar());

                        con.Close();
                    }
                }


            }

            return currentAdminID;
        }

        public void insertMediaDetails(string mediafileName, string mediaOriginalName, string mediaExtn,int mediaSize, DateTime CreatedDate,int adminID)
        {
            
            using (SqlCommand sqlCommand = new SqlCommand("mediaDetails", con))
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    using (DataTable dataTable = new DataTable())
                    {
                        con.Open();

                        sqlCommand.Parameters.AddWithValue("@mediafileName", SqlDbType.VarChar).Value = mediafileName;
                        sqlCommand.Parameters.AddWithValue("@mediaOriginalName", SqlDbType.VarChar).Value = mediaOriginalName;
                        sqlCommand.Parameters.AddWithValue("@mediaExtn", SqlDbType.VarChar).Value = mediaExtn;
                        sqlCommand.Parameters.AddWithValue("@mediaSize", SqlDbType.Int).Value = mediaSize;
                        sqlCommand.Parameters.AddWithValue("@CreatedDate", SqlDbType.DateTime).Value = CreatedDate;
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", SqlDbType.Int).Value = adminID;
                        sqlCommand.Parameters.AddWithValue("UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                        sqlCommand.Parameters.AddWithValue("@UpdatedBy", SqlDbType.Int).Value = adminID;

                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlDataAdapter.SelectCommand = sqlCommand;
                     

                        currentMediaID = Convert.ToInt32(sqlCommand.ExecuteScalar());

                        con.Close();
                    }
                }


            }
        }

        public void insertAdminData(string aName,string aPhone,string aMail,string aAddress,string aCity,string aGender,string aPassword,string aRoll, DateTime registerDate,int aStatus)
        {
            using (SqlCommand sqlCommand = new SqlCommand("insertAdminData",con))
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    using (DataTable dataTable = new DataTable())
                    {
                        con.Open();

                        sqlCommand.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = aName;
                        sqlCommand.Parameters.AddWithValue("@phone", SqlDbType.VarChar).Value = aPhone;
                        sqlCommand.Parameters.AddWithValue("@mail", SqlDbType.VarChar).Value = aMail;
                        sqlCommand.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = aAddress;
                        sqlCommand.Parameters.AddWithValue("@city", SqlDbType.VarChar).Value = aCity;
                        sqlCommand.Parameters.AddWithValue("@Gender", SqlDbType.VarChar).Value = aGender;
                        sqlCommand.Parameters.AddWithValue("@password",SqlDbType.VarChar).Value = Encrypt(aPassword);
                        sqlCommand.Parameters.AddWithValue("@aRoll", SqlDbType.VarChar).Value = aRoll;
                        sqlCommand.Parameters.AddWithValue("@mediaID", SqlDbType.Int).Value = currentMediaID;
                        sqlCommand.Parameters.AddWithValue("@registerDate", SqlDbType.DateTime).Value = registerDate;
                        sqlCommand.Parameters.AddWithValue("@aStatus", SqlDbType.Bit).Value = aStatus;

                        
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        sqlDataAdapter.Fill(dataTable);

                      

                        con.Close();
                    }
                }
                    

            }
        }
    }


}