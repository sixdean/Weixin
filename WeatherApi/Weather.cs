
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace WeatherApi
{
    public static class Weather
    {
        public static WeatherModel.HeWeatherResult GetWeatherResult(string city, string cityid = null,
            string cityip = null)
        {
            var apiUrl = ConfigurationManager.AppSettings["WeatherApiWork"];
            var weatherReplaceString = ConfigurationManager.AppSettings["weatherReplaceString"];
            var param = "";
            if (!string.IsNullOrEmpty(city))
            {
                param = "city=" + city;
            }
            else
            {
                if (!string.IsNullOrEmpty(cityid))
                {
                    param = "cityid=" + cityid;
                }
                else
                {
                    if (!string.IsNullOrEmpty(cityip))
                    {
                        param = "cityip=" + cityip;
                    }
                }
            }
            var url = apiUrl + '?' + param;
            var str = GetWeather(url).Replace(weatherReplaceString, "HeWeatherList");
            var result = JsonConvert.DeserializeObject(str, typeof(WeatherModel.HeWeatherResult)) as WeatherModel.HeWeatherResult;
            return result;
        }

        public static string GetWeather(string url)
        {
            var apiKey = ConfigurationManager.AppSettings["apikey"];
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("apikey", apiKey);
            var response = (HttpWebResponse)request.GetResponse();
            var s = response.GetResponseStream();
            var strDate = "";
            var strValue = "";
            if (s == null) return strValue;
            var reader = new StreamReader(s, Encoding.UTF8);
            while ((strDate = reader.ReadLine()) != null)
            {
                strValue += strDate + "\r\n";
            }
            return strValue;
        }
    }
}