using System.Text.Json;
using System.Text.Json.Serialization;

namespace SAMS.Services
{
    public class UnixDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                // MockAPI returns seconds since 1970
                var seconds = reader.GetInt64();
                return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                return DateTime.Parse(reader.GetString()!);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var unixSeconds = ((DateTimeOffset)value).ToUnixTimeSeconds();
            writer.WriteNumberValue(unixSeconds);
        }
    }
}
