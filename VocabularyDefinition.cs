using System.Collections.Generic;
using Newtonsoft.Json;

namespace LookupTheVoc
{
    class VocDef
    {
        public meta_VocDef meta;
    }

    class meta_VocDef
    {
        public string id;
        public string uuid;
        [JsonProperty(PropertyName="app-shortdef")]
        public shortdef_meta_VocDef app_shortdef;
    }

    class shortdef_meta_VocDef
    {
        public string hw;
        public IList<string> def;
    }
}