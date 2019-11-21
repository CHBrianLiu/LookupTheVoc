using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace LookupVoc
{
    class Program
    {
        static void Main(string[] args)
        {
            // try find key locally in credential.local file.
            if (!File.Exists("./credential.local")) {
                // not found. read from user input and store in credential.local file.
                Console.WriteLine("Cannot find a valid key. Please enter your key: ");
                string userKeyInput = Console.ReadLine();
                File.WriteAllText("./credential.local", userKeyInput);
            }

            // read key from credential.local file and create an Profile object to store key.
            LookupVoc.Profile profileObj = new LookupVoc.Profile(File.ReadAllText("./credential.local"));
            if (!profileObj.isCorrectKey()) {
                Console.WriteLine("Incorrect key detected. Please modify the key in file 'credential.local'");
                Environment.Exit(0);
            }

            // specify read file
            List<string> inputFilesList = new List<string>();
            if (args.Length == 0) {
                // read file not specified in arguments
                Console.WriteLine("Enter the file name that contains vocabularies: ");
                inputFilesList.Add(Console.ReadLine());
            } else {
                foreach (string filename in args) {
                    inputFilesList.Add(filename);
                }
            }

            // process each file in the list
            foreach (string file in inputFilesList) {
                // check if file exists
                if (!File.Exists(file)) {
                    Console.WriteLine(file + " not found.");
                    continue;
                }
                // read all text from the file and store in a list
                var vocs = File.ReadAllLines(file);
                // loop the list
                foreach (string voc in vocs) {
                    // look up the vocabulary and process the result
                    HttpClient client = new HttpClient();
                    string url = "https://www.dictionaryapi.com/api/v3/references/learners/json/" + voc + "?key=" + profileObj.getKey;
                    try {
                        var response = client.GetStringAsync(url);
                        response.Wait();
                    } catch {
                        Console.WriteLine(voc + " not found.");
                    }
                    LookupVoc.Definition def = JsonConvert.DeserializeObject<LookupVoc.Definition>(response.Result);
                    // process the output format and save in file
                    
                }
            }

        }
    
        // static public bool lookup (string key, string keyword) {
        //     string url = "https://www.dictionaryapi.com/api/v3/references/learners/json/" + keyword + "?key=" + key;
        //     HttpClient client = new HttpClient();
        //     Task<string> response;
        //     try {
        //         response = client.GetStringAsync(url);
        //         response.Wait();
        //     } catch {
        //         return false;
        //     }
        //     parse(response.Result);
        //     return true;
        // }
        // static private void parse(string jsonStr) {
        //     var result = JsonConvert.DeserializeObject<Definition>(jsonStr);
        //     this.id = result.id;
        //     this.functionalLabel = result.functionalLabel;
        //     this.shortDef = result.shortDef;
        // }
    }
}