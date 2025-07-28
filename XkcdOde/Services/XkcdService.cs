using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace XkcdOde
{
    internal class XkcdService
    {
        private static readonly CompositeFormat _xkcdUrlTemplate = CompositeFormat.Parse("http://xkcd.com{0}/info.0.json");
       
        public static XkcdResponse? GetXkcdResponse(int? num)
        {
            string url;

            if (num == null)
            {
                url = string.Format(CultureInfo.InvariantCulture, _xkcdUrlTemplate, string.Empty);
            }
            else
            {
                url = string.Format(CultureInfo.InvariantCulture, _xkcdUrlTemplate, $"/{num}");
            }

            using var http = new HttpClient();
            var response = http.GetAsync(url).Result;
            var contentString = response.Content.ReadAsStringAsync().Result;
            
            return JsonSerializer.Deserialize(contentString, SourceGenerationContext.Default.XkcdResponse);
        }
    }
}
