using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SquadronDump
{
    internal class Squadron
    {
        [JsonPropertyName("id")]
        public uint Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("tag")]
        public string? Tag { get; set; }
        [JsonPropertyName("platform")]
        public string? Platform { get; set; }
    }
}
