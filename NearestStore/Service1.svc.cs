using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NearestStore
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string findNearestStore(string zipcode, string storeName)
        {
            List<string> list = new List<string>();
            string url = "http://maps.googleapis.com/maps/api/geocode/json?components=postal_code:" + zipcode + "&sensor=false"; // invokes service to obtain lat and long of given zipcode
            LatLonRootObject latLonobj = new LatLonRootObject(); 
            StoreObject storeobj = new StoreObject(); 
            string location; 
            string finalData = "";

            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(url); 

                latLonobj = JsonConvert.DeserializeObject<LatLonRootObject>(json); 
                if (latLonobj.status == "OK") 
                {
                    location = latLonobj.results[0].geometry.bounds.northeast.lat.ToString() + ","
                                + latLonobj.results[0].geometry.bounds.northeast.lng.ToString();

                    // Builds url to invoke the Google Places API nearbysearch(...)
                    url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="
                        + location + "&radius=30000&keyword=" + storeName + "&key=AIzaSyClPTJJI-iY1Ha0Lprt-ecYaLP_wpSSu5o";

                    json = webClient.DownloadString(url);

                    storeobj = JsonConvert.DeserializeObject<StoreObject>(json); 

                    if (storeobj.status == "OK") 
                    {
                        list.Add(storeobj.results[0].icon); 
                        list.Add(storeobj.results[0].name); 
                        list.Add(storeobj.results[0].rating.ToString()); 
                        list.Add(storeobj.results[0].vicinity); 
                    }
                    else
                    {
                        list.Add(storeName + " does not exist in the area."); 
                    }
                }
                else
                    list.Add("Not a valid location"); 
            }

            
            foreach (string str in list)
            {
                finalData += str + "|"; 
            }

            finalData = finalData.TrimEnd('|'); 
            return finalData;
        }
    }

    
    public class LatLonRootObject
    {
        public Result[] results { get; set; }
        public string status { get; set; }

        public class Result
        {
            public Geometry geometry { get; set; }
            public class Geometry
            {
                public Bounds bounds { get; set; }
                public class Bounds
                {
                    public Northeast northeast { get; set; }
                    public class Northeast
                    {
                        public float lat { get; set; }
                        public float lng { get; set; }
                    }
                }
            }
        }
    }

    
    public class StoreObject
    {
        public Result[] results { get; set; }
        public string status { get; set; }

        public class Result
        {
            public string icon { get; set; }
            public string name { get; set; }
            public float rating { get; set; }
            public string vicinity { get; set; }
        }
    }
}
