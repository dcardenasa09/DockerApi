using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Lib.Helpers;

public class DateOnlyConverter: JsonConverter<DateOnly> {

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        return DateOnly.FromDateTime(reader.GetDateTime());
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) {
        var isoDate = value.ToString("O");
        writer.WriteStringValue(isoDate);
    }
}