using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClassLibrary.Models
{
    [Serializable]
    public class PrivateMessage
    {
        public int Id { get; set; }
        public string SenderUsername { get; set; }
        public string RecieverUsername { get; set; }
        public string DateSent { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }

        public PrivateMessage()
        {

        }

        public PrivateMessage(int id, string sender, string reciever, string date, string subject, string text)
        {
            Id = id;
            SenderUsername = sender;
            RecieverUsername = reciever;
            DateSent = date;
            Subject = subject;
            Text = text;
        }

    }
}
