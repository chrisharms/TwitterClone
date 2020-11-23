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
        /// <summary>
        /// Create an object of any type as long as the Objects attributes
        /// match up to its DB representation(Names of fields/Attributes must match exactly)
        /// </summary>
        /// <typeparam name="T">The type of the object you want to convert a record to</typeparam>
        /// <param name="rowData">The object array representing the DB row</param>
        /// <param name="t">The type of the object to convert to</param>
        /// <returns>An object of Type t</returns>
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
        /// Retrieve all records returned by a given SQL stored procedure command
        /// </summary>
        /// <param name="commandName">The name of the command to run</param>
        /// <param name="ex">An exception reference for error handling</param>
        /// <returns>A list of records in the form of object arrays</returns>
        public static List<object[]> ReadDBObjs(string commandName, ref Exception ex)
        {
            List<object[]> records = new List<object[]>();
            try
            {
                DBConnect dbc = new DBConnect();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = commandName;

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

        /// <summary>
        /// Retrieve all records returned by a given SQL stored procedure command that uses a where clause
        /// </summary>
        /// <param name="commandName">The name of the command to run</param>
        /// <param name="ex">An exception reference for error handling</param>
        /// <param name="filters">A list of tuples containing the field to use for a where clause, the vaue to use for the field, and the type of given value/field.</param>
        /// <returns>Returns a list of records from the db in the form of an object array.</returns>
        public static List<object[]> ReadDBObjsWithWhere(string commandName, ref Exception ex, List<(string field, dynamic value, Type type)> filters)
        {
            List<object[]> records = new List<object[]>();
            try
            {
                DBConnect dbc = new DBConnect();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = commandName;

                foreach ((string field, dynamic value, Type type) filter in filters)
                {
                    if(string.IsNullOrEmpty(filter.field) || filter.value.GetType() != filter.type)
                    {
                        return null;
                    }

                    SqlParameter inputParam = new SqlParameter($@"{filter.field}", filter.value)
                    {
                        Direction = ParameterDirection.Input
                    };
                    cmd.Parameters.Add(inputParam);
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


        public static (string field, dynamic value, Type type) CreateFilter(string field, dynamic value, Type t)
        {
            return (field, value, t);
        }
    }
}
