using System.Text.Json.Serialization;

namespace XkcdOde
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(XkcdResponse))]
    internal partial class SourceGenerationContext : JsonSerializerContext { }
}
