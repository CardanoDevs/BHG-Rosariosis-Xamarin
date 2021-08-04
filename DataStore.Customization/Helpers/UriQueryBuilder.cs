using System.Collections.Generic;
using System.Linq;

namespace DataStore.Customization.Helpers
{
    public class UriQueryBuilder
    {
        List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

        public UriQueryBuilder()
        {
        }

        public UriQueryBuilder AddParam(string key, object value)
        {
            parameters.Add(new KeyValuePair<string, object>(key, value));
            return this;
        }

        public string Build()
        {
            return string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}"));
        }

        public override string ToString()
        {
            return Build();
        }
    }
}
