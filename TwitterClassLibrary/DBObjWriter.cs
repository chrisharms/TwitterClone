using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using TwitterClassLibrary.Connection;

namespace TwitterClassLibrary.DBObjWriter
{
public class DBObjWriter
{

        private static Dictionary<Type, SqlDbType> typeMap = new Dictionary<Type, SqlDbType>() { {typeof(int), SqlDbType.Int }, { typeof(double), SqlDbType.Float },
                                                                                                { typeof(decimal), SqlDbType.Float }, {typeof(string), SqlDbType.VarChar},
                                                                                                { typeof(bool), SqlDbType.Bit}
                                                                                                };
        /// <summary>
        /// Generic type method to write an object to the Microsoft SQL DB. Requires the property names of the object
        /// to match the property names of the fields in the database)
        /// </summary>
        /// <param name="obj">The object you want to write the database</param>
        /// <param name="commandName">The name of the stored procedure you want to use</param>
        /// <param name="ex">A list of tuples(bool, int, Exception) for keeping track of what works/didn't work. The last index will contain DB result int. 1 = Success, 0 = didn't complete, -1 = error</param>
        /// <returns>Return true if no conversion errors or runtime errors, not if the DB command ran succesfully.</returns>
        public static bool GenericWriteToDB<T>(T obj, string commandName, ref List<(bool, int, Exception)> ex, List<string> filter = null)
        {
            DBConnect dbc = new DBConnect();
            int count = 0;
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandName
                };
                List<PropertyInfo> properties = obj.GetType().GetProperties().ToList();
                if(filter != null && filter.Count < properties.Count)
                {
                    properties = properties.Where(p => !filter.Contains(p.Name)).ToList();
                }
                else if( filter != null && filter.Count >= properties.Count)
                {
                    ex.Add((false, -1, new IndexOutOfRangeException("Filter size greater than number of fields")));
                    return false;
                }

                foreach (PropertyInfo property in properties)
                {
                    Type t = property.PropertyType;
                    SqlParameter inputParam = new SqlParameter($@"{property.Name}", property.GetValue(obj));
                    inputParam.Direction = ParameterDirection.Input;
                    if (typeMap.ContainsKey(t))
                    {
                        inputParam.SqlDbType = typeMap[t];
                    }
                    else
                    {
                        ex.Add((false, count, new InvalidCastException($"Couldn't convert property type to SQLData Type, map is missing for type {t}")));
                        return false;
                    }
                    cmd.Parameters.Add(inputParam);
                    ex.Add((true, count, null));
                    count++;
                }

                int dbResult = dbc.DoUpdateUsingCmdObj(cmd);
                if (dbResult < 0)
                {
                    ex.Add((false, dbResult, null));
                    return false;
                }
                else
                {
                    ex.Add((true, dbResult, null));
                    return true;
                }
            }
            catch (Exception e)
            {
                ex.Add((false, count, e));
                return false;
            }
        }


        public static bool DeleteWithWhere(string commandName, ref Exception ex, List<(string field, dynamic value, Type type)> filters)
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
                    if (string.IsNullOrEmpty(filter.field) || filter.value.GetType() != filter.type)
                    {
                        ex = new FormatException($"{filter.field} value {filter.value} did not match the specified type ({filter.type})");
                        return false;
                    }

                    SqlParameter inputParam = new SqlParameter($@"{filter.field}", filter.value)
                    {
                        Direction = ParameterDirection.Input
                    };
                    cmd.Parameters.Add(inputParam);
                }

                dbc.DoUpdateUsingCmdObj(cmd);

                return true;
            }
            catch (Exception e)
            {
                ex = e;
                return false;
            }
        }
    }
}
