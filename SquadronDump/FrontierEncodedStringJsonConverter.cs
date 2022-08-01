using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SquadronDump
{
    /// <summary>
    /// Decode the hex ASCII encoded text.
    /// </summary>
    internal class FrontierEncodedStringJsonConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Encoding.UTF8.GetString(Convert.FromHexString(reader.GetString() ?? string.Empty));
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
