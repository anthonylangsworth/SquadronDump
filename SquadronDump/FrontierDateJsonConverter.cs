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
    /// Handle "0000-00-00 00:00:00" to mean no date.
    /// </summary>
    internal class FrontierDateJsonConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            DateTime? result;
            if(value is null || "0000-00-00 00:00:00".Equals(value))
            {
                result = null;
            }
            else
            {
                result = DateTime.Parse(value);
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
