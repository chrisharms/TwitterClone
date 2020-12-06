using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClassLibrary.Models;
using TwitterClassLibrary.Connection;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TwitterClassLibrary
{
    public class DBSerialize
    {
        public static int WriteSerializedPM(PrivateMessage pm)
        {
            DBConnect dbc = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_CreateSerializedPM";
            cmd.CommandType = CommandType.StoredProcedure;
            string reciever = pm.RecieverUsername;
            string sender = pm.SenderUsername;
            string serializedData = SerializePM(pm);
            
            SqlParameter inputParam = new SqlParameter(@"RecieverUsername", reciever);
            inputParam.Direction = ParameterDirection.Input;
            inputParam.SqlDbType = SqlDbType.VarChar;
            inputParam.Size = 24;
            cmd.Parameters.Add(inputParam);

            inputParam = new SqlParameter(@"SenderUsername", sender);
            inputParam.Direction = ParameterDirection.Input;
            inputParam.SqlDbType = SqlDbType.VarChar;
            inputParam.Size = 24;
            cmd.Parameters.Add(inputParam);

            inputParam = new SqlParameter(@"SerializedData", serializedData);
            inputParam.Direction = ParameterDirection.Input;
            inputParam.SqlDbType = SqlDbType.VarChar;
            cmd.Parameters.Add(inputParam);

            return dbc.DoUpdateUsingCmdObj(cmd);

        }

        public static List<PrivateMessage> ReadSerializedPM(string recieverUsername)
        {
            DBConnect dbc = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_GetUsersSerializedPMs";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter inputParam = new SqlParameter(@"Username", recieverUsername);
            inputParam.Direction = ParameterDirection.Input;
            inputParam.SqlDbType = SqlDbType.VarChar;
            inputParam.Size = 24;
            cmd.Parameters.Add(inputParam);

            List<DataRow> records = dbc.GetDataSetUsingCmdObj(cmd).Tables[0].AsEnumerable().ToList();
            List<PrivateMessage> pms = new List<PrivateMessage>();
            foreach (DataRow item in records)
            {
                object[] data = item.ItemArray;
                pms.Add(DeserializePM(data[2].ToString()));
            }

            return pms;
        }


        public static PrivateMessage DeserializePM(string serializedPM)
        {
            byte[] binaryData = Convert.FromBase64String(serializedPM);
            using (var ms = new MemoryStream(binaryData))
            {
                var binaryFormatter = new BinaryFormatter();
                ms.Seek(0, SeekOrigin.Begin);
                return (PrivateMessage)binaryFormatter.Deserialize(ms);
            }
        }

        public static string SerializePM(PrivateMessage pm)
        {
            using (var ms = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(ms, pm);
                ms.Flush();
                ms.Position = 0;
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
