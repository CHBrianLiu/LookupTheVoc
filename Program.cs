using System;
using LookupTheVoc;
// additional package
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace LookupTheVoc
{


    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            string vocToLook = Console.ReadLine();
            string vocJStirng = lookup(vocToLook).Result;
            var primary = parseJson(vocJStirng);
            printResult(primary);
        }

        private static async Task<string> lookup(string voc)
        {
            client.DefaultRequestHeaders.Clear();

            // construct url 
            // https://dictionaryapi.com/api/v3/references/learners/json/<voc>?key=<key>
            // https://dictionaryapi.com/api/v3/references/learners/json/test?<key>
            string dicURL = Info.hostURL + voc + "?key=" + Info.key;

            // Get Json String from URL
            var vocJString = await client.GetStringAsync(dicURL);
            return vocJString;
        }

        private static VocDef parseJson(string Jstring)
        {
            // Parse Json string
            var resultList = JsonConvert.DeserializeObject<List<VocDef>>(Jstring);

            return resultList[0];
        }

        private static void printResult(VocDef voc)
        {
            int count = 1;
            Console.WriteLine($"id: {voc.meta.id}");
            foreach (string def in voc.meta.app_shortdef.def)
            {
                Console.WriteLine($"def {count++}: {def}");
            }
        }
    }
}
