using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace cinema
{
    public class RESTAPI
    {
        public static void rest() 
        {
            WebRequest request = WebRequest.Create("https://api.kinopoisk.dev/movie?token=1989K4Y-FWG4M0T-H3G2DEJ-9F30HK2&search=326&field=id");
            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse response = (HttpWebResponse)request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                JObject sad = JObject.Parse(reader.ReadToEnd());
                var sa2 = sad["poster"]["url"].ToString();
            }
        } 
    }
}
