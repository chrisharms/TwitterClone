using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace TwitterClone
{
    public class WebAssetSerializer
    {
        public static string SerializeImage(string path)
        {
            byte[] imageArray = File.ReadAllBytes(@path);
            return Convert.ToBase64String(imageArray);
        }

        public static void DeserializeImage(string base64String, Image img)
        {
            img.ImageUrl = "data:image/png;base64," + base64String;
        }
    }
}
