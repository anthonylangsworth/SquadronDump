using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SquadronDump
{
    internal class SquadronList
    {
        [JsonPropertyName("squadron")]
        public Squadron Squadron { get; set; } = new Squadron();
    }
}
