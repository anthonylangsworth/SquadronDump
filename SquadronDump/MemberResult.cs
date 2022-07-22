using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SquadronDump
{
    internal class MemberResult
    {
        [JsonPropertyName("members")]
        public Member[] Members { get; set; } = new Member[0];
    }
}
