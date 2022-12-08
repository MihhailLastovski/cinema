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
using System.Windows.Forms;
using System.Xml.Linq;

namespace cinema
{
    public class RESTAPI
    {
        public static MyObject rest(int filmID) 
        {
            WebRequest request = WebRequest.Create($"https://api.kinopoisk.dev/movie?token=1989K4Y-FWG4M0T-H3G2DEJ-9F30HK2&search={filmID}&field=id");
            request.Method = "GET";
            request.ContentType = "application/json";
            try
            {
                WebResponse response = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    JObject movieinfo = JObject.Parse(reader.ReadToEnd());
                    var poster = movieinfo["poster"]["url"].ToString();
                    var name = movieinfo["name"].ToString();
                    var ratingimdb = (float)movieinfo["rating"]["imdb"];
                    var movieLength = (int)movieinfo["movieLength"];
                    var description = movieinfo["description"].ToString();
                    var zanr = movieinfo["genres"][0]["name"].ToString();
                    var year = (int)movieinfo["year"];
                    MyObject getinfo = new MyObject();
                    getinfo.Poster = poster;
                    getinfo.Name = name;
                    getinfo.Zanr = zanr;
                    getinfo.MovieLength = movieLength;
                    getinfo.Description = description;
                    getinfo.Year = year;
                    getinfo.Rating = ratingimdb;
                    return getinfo;
                }
            }
            catch { return null; }
            
        }
        public class MyObject
        {
            public string Poster { get; set; }
            public string Name { get; set; }
            public float Rating { get; set; }
            public int MovieLength { get; set; }
            public string Description { get; set; }
            public string Zanr { get; set; }
            public int Year { get; set; }
        }
    }
}
