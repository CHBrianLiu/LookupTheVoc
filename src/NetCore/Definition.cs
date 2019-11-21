using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LookupVoc {
    public class Definition {
        // metea.id
        public string id { get; set; }
        // fl
        [JsonProperty("fl")]
        public string  functionalLabel { get; set; }
        // shortdef
        public List<string> shortDef { get; set; }
    }
}