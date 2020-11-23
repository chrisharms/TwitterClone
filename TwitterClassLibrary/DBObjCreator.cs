using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;


namespace TwitterClassLibrary   
{
    public class DBObjCreator
    {
        public static T CreateObj<T>(object[] rowData, Type t)
        {
            try
            {
                List<Type> constructorParams = new List<Type>();
                rowData.ToList().ForEach(o => constructorParams.Add(o.GetType()));
                ConstructorInfo ci = t.GetConstructor(constructorParams.ToArray());
                object obj = ci.Invoke(rowData);
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch(Exception ex)
            {
                return (T)Convert.ChangeType(new object(), typeof(T));
            }
        }
        /// <summary>
        /// Run a DB Command to retrieve data from the database. 
        /// Made for use with DBObjCreator.CreatObj() method
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="key">Optional key, this is the field to use in a where statement</param>
        /// <param name="value">Optional value, this is the value to use for the field in the where statement. If you have a key, you must have a value.</param>
        /// <returns>Returns an object array of the first row that matches</returns>
        public static object[] ReadDBObj(string commandName, ref Exception ex, string key = null, string value = null, List<Type> filterType = null)
        {
            try
            {
                if (filterType != null && filterType.Count != 1)
                {
                    return null;
                }
                else if(filterType != null)
                {
                    Convert.ChangeType(value, filterType[0]);
                }

                DBConnect dbc = new DBConnect();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = commandName;
                //Could use a dictionary<string, string> and iterate over the key collection if we wanted to allow multiple filters(input params)
                if (key != null && value != null)
                {
                    SqlParameter inputParam = new SqlParameter($@"{key}", value)
                    {
                        Direction = ParameterDirection.Input
                    };
                    cmd.Parameters.Add(inputParam);
                }
                else if ((key != null && value == null) || (key == null && value != null))
                {
                    return null;
                }

                DataTable dt = dbc.GetDataSetUsingCmdObj(cmd).Tables[0];
                return dt.Rows[0].ItemArray;
            }
            catch (Exception e)
            {
                ex = e;
                return null;
            }
        }


        public static List<object[]> ReadDBObjs(string commandName, ref Exception ex, string key = null, string value = null, List<Type> filterType = null)
        {
            List<object[]> records = new List<object[]>();
            try
            {
                if (filterType != null && filterType.Count != 1)
                {
                    return null;
                }
                else if (filterType != null)
                {
                    Convert.ChangeType(value, filterType[0]);
                }

                DBConnect dbc = new DBConnect();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = commandName;
                //Could use a dictionary<string, string> and iterate over the key collection if we wanted to allow multiple filters(input params)
                if (key != null && value != null)
                {
                    SqlParameter inputParam = new SqlParameter($@"{key}", value)
                    {
                        Direction = ParameterDirection.Input
                    };
                    cmd.Parameters.Add(inputParam);
                }
                else if ((key != null && value == null) || (key == null && value != null))
                {
                    return null;
                }

                DataTable dt = dbc.GetDataSetUsingCmdObj(cmd).Tables[0];
                foreach (DataRow row in dt.AsEnumerable())
                {
                    records.Add(row.ItemArray);
                }

                return records;
            }
            catch (Exception e)
            {
                ex = e;
                return null;
            }
        }
    }
}
