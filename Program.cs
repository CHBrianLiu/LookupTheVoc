using System;
// additional package
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace LookupTheVoc
{
    // contains credentials
    class Info 
    {
        public static string key = "***REMOVED***";
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
            string dicURL = Info.hostURL + voc + "?key=" + Info.key;

            var resultString = await client.GetStringAsync(dicURL);
            return resultString;
        }
    }
}
