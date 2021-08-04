using DataStore.Customization.Responses.ClassRoom;
using Plugin.SecureStorage;
using System;

namespace DataStore.Customization.Helpers
{
    public class UriPathBuilder
    {
        private static UriBuilder _uriBuilder;

        public UriPathBuilder() { }

        public UriPathBuilder(string endPoint) => _uriBuilder = new UriBuilder(endPoint);

        public static string _baseUrl = CrossSecureStorage.Current.GetValue("BaseUrl", "");

        public static UriBuilder GetPath(string path, UriQueryBuilder queryBuilder = null)
        {
            path = _baseUrl + path;
            _uriBuilder = new UriBuilder(path);
            //_uriBuilder.Path = path;
            _uriBuilder.Query = queryBuilder?.Build();
            return _uriBuilder;
        }
    }
}
