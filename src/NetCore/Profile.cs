using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace LookupVoc {
    public class Profile {
        public Profile(string key) {
            this.key = key;
        }
        private string key; 
        public string getKey() => this.key;
        public bool isCorrectKey() {
            HttpClient testAgent = new HttpClient();
            try {
                var respose = testAgent.GetStringAsync("https://www.dictionaryapi.com/api/v3/references/learners/json/test?key=" + this.key);
            } catch (HttpRequestException e) {
                // wrong key
                return false;
            }
            return true;
        }
    }
}