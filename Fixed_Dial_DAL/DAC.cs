using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace Fixed_Dial_DAL
{
    public class DAC

    {

        // Execute the stored procedure

        public static DataSet Execute(string StoredProcName)

        {

            // Generic collection of my DacParameter object

            List<Parameters.DacParameter> ParameterPairs = Parameters.MyParameters;

            DataSet ds;



            // Validation for Parameter collection

            try

            {

                // if you want to force the user to give parameters, use this code

                if (ParameterPairs.Count <= 0)

                {

                    //throw new Exception("Invalid parameter supply. Check your BL Method");

                }

            }



            catch (Exception Ex)

            {

                throw (Ex);

            }



            // Get connection String - I've created this for web app. Hence it is reading from web.config.

            // You can modify this as you wish.

            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

            string ConnString = "";



            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)

            {

                ConnString = rootWebConfig.ConnectionStrings.ConnectionStrings["DfltConnection"].ToString();

            }



            // Using a Transaction Here: To ensure data integrity

            SqlTransaction Trans;

            using (SqlConnection Conn = new SqlConnection(ConnString))

            {

                Conn.Open();

                Trans = Conn.BeginTransaction();

                try

                {

                    // If there is any parameters then iterate through the collection

                    // and bind it

                    if (ParameterPairs.Count > 0)

                    {

                        SqlParameter[] arparams = new SqlParameter[ParameterPairs.Count];



                        int Count = 0;

                        foreach (Parameters.DacParameter pObject in ParameterPairs)

                        {

                            arparams[Count] = new SqlParameter(pObject.Name, pObject.Type);

                            arparams[Count].Value = pObject.Value;



                            //Increment counter

                            Count++;

                        }



                        // Execute stored procedure

                        ds = SqlHelper.ExecuteDataset(ConnString, CommandType.StoredProcedure, StoredProcName, arparams);

                    }



                    // else I just need to execute it Eg: Procedures that have only SELECT queries

                    else

                    {

                        ds = SqlHelper.ExecuteDataset(ConnString, CommandType.StoredProcedure, StoredProcName);

                    }



                    // Yes, go ahead with Committing the transaction

                    Trans.Commit();

                }



                catch (Exception Ex)

                {

                    Trans.Rollback();

                    throw (Ex);

                }



                finally

                {

                    if (Conn.State == ConnectionState.Open)

                    {

                        Conn.Close();

                        Conn.Dispose();

                    }

                    Parameters.MyParameters.Clear();

                }



                return ds;

            }

        }



        // My Parameters class

        public class Parameters

        {

            protected internal static List<DacParameter> MyParameters = new List<DacParameter>();

            public static void Add(string Name, SqlDbType dbtype, object value)

            {

                DacParameter KeyValuePair = new DacParameter();

                KeyValuePair.Name = Name;

                KeyValuePair.Type = dbtype;

                KeyValuePair.Value = value;

                MyParameters.Add(KeyValuePair);

            }



            // class param, type , value class : Aggregation

            public class DacParameter

            {

                private string _ParameterName;

                public string Name

                {

                    get

                    {

                        return _ParameterName;

                    }

                    set

                    {

                        _ParameterName = value;

                    }

                }



                private System.Data.SqlDbType _Type;

                public System.Data.SqlDbType Type

                {

                    get

                    {

                        return _Type;

                    }

                    set

                    {

                        _Type = value;

                    }

                }



                private object _value;

                public object Value

                {

                    get

                    {

                        return _value;

                    }

                    set

                    {

                        _value = value;

                    }

                }

            }

        }

    }
}