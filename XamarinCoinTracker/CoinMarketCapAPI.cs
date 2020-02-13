using Nancy.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Web;

namespace XamarinCoinTracker
{
    public class CoinMarketCapAPI
    {
        private static string API_KEY = "abfae75d-ac8c-4516-9834-851de52f3ab5";

  
       public static string makeAPICall()
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "125";
            queryString["convert"] = "USD";


            URL.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(URL.ToString());

        }
    }

}