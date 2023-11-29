using System.Text.Json;

namespace Blazor.Blog.Tests.HelperClasses
{
    public class ObjectToJsonSerializer
    {
        public static JsonDocument SerializeToJson(object objectToSerialize)
        {
            return JsonDocument.Parse(JsonSerializer.Serialize(objectToSerialize));
        }
    }
}
