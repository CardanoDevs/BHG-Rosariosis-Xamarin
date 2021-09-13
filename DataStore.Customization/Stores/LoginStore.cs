using DataStore.Customization.Helpers;
using DataStore.Customization.Paths;
using DataStore.Customization.Requests.Login;
using DataStore.Customization.Responses.Login;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataStore.Customization.Stores
{
    public class LoginStore
    {
        public async Task<LoginResponse> PostLoginRequest(LoginRequest item)
        {
            try
            {
                var uri = UriPathBuilder.GetPath(ApiResources.Login).ToString();
                var formVariables = new List<KeyValuePair<string, string>>();
                formVariables.Add(new KeyValuePair<string, string>("username", item.UserName));
                formVariables.Add(new KeyValuePair<string, string>("password", item.Password));
                var result = await RequestProvider.PostAsync<LoginRequest, LoginResponse>(uri, formVariables);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ClientToken> CheckTokenLife()
        {
            try
            {
                var uri = UriPathBuilder.GetPath(ApiResources.VerifyToken).ToString();// "https://stratusarchives.com/assignment/api/VerifyJWT.php";
                var token = CrossSecureStorage.Current.GetValue("clientToken", "");
                var result = await RequestProvider.GetAsync<ClientToken>(uri, token);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<SchoolResponse> GetSchools()
        {
            try
            {
                //var uri = "https://globalsismobile.gs4ed.com/api/Schools.php?key=12345";
                var uri = "http://10.10.12.188/api/Schools.php?key=12345";
                
                var result = await RequestProvider.GetAsync<SchoolResponse>(uri);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
