using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LookupVoc {
    public class Definition {
        public Definition(string id, string functionalLabel, List<string> shortDefList) {
            this.id = id;
            this.functionalLabel = functionalLabel;
            foreach (string def in shortDefList) {
                this.shortDef.Add(def);
            }
        }
        public Definition(string id, string functionalLabel, JToken shortDefArray) {
            this.id = id;
            this.functionalLabel = functionalLabel;
            this.shortDef = new List<string>();
            foreach (string def in shortDefArray) {
                this.shortDef.Add(def);
            }
        }
        // metea.id
        public string id { get; set; }
        // fl
        [JsonProperty("fl")]
        public string  functionalLabel { get; set; }
        // shortdef
        public List<string> shortDef { get; set; }
    }
}