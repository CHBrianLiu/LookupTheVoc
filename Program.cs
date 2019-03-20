using System;
// additional package
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace LookupTheVoc
{
    // contains credentials
    class Info 
    {
        public static string key = "39c1ff57-b341-4cb3-a10a-183a19c641c3";
        // https://dictionaryapi.com/api/v3/references/learners/json/<voc>?key=<key>
        public static string hostURL = "https://dictionaryapi.com/api/v3/references/learners/json/"; 
    }
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            string voc = Console.ReadLine();
            Console.WriteLine(lookup(voc).Result);
        }

        private static async Task<string> lookup(string voc)
        {
            client.DefaultRequestHeaders.Clear();

            // construct url 
            // https://dictionaryapi.com/api/v3/references/learners/json/<voc>?key=<key>
            // https://dictionaryapi.com/api/v3/references/learners/json/test?key=39c1ff57-b341-4cb3-a10a-183a19c641c3
            string dicURL = Info.hostURL + voc + "?key=" + Info.key;

            // Get Json String from URL
            var vocJString = await client.GetStringAsync(dicURL);
            
            // Parse Json string
            JObject vocJObj = JObject.Parse(vocJString);
            
            return vocJString;
        }
    }
}
