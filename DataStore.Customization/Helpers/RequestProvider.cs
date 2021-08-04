using DataStore.Customization.Paths;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using DataStore.Customization.Responses.Login;
using DataStore.Customization.Requests.ClassRoom;

namespace DataStore.Customization.Helpers
{
    public static class RequestProvider
    {
        private static CancellationTokenSource _cancellationTokenSource;

        private static CancellationToken GetCancellationToken()
        {
            _cancellationTokenSource = new CancellationTokenSource(20000);
            return _cancellationTokenSource.Token;
        }

        private static void DisposeCancellationToken() =>
            _cancellationTokenSource?.Dispose();

        public static async Task<TResult> GetAsync<TResult>(string uri, string authenticationToken = null)
        {
            TResult result = default;
            try
            {
                var client = new HttpClient();
                if (!string.IsNullOrWhiteSpace(authenticationToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TResult>(content);
                    return result;
                }
                else
                    return result;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { DisposeCancellationToken(); }
        }

        public static async Task<TResult> PostAsync<TRequest, TResult>(string uri, List<KeyValuePair<string, string>> data, string authenticationToken = null)
        {
            TResult result = default;
            try
            {

                HttpContent httpContent = new FormUrlEncodedContent(data);
                //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var client = new HttpClient();
                if (!string.IsNullOrWhiteSpace(authenticationToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
                var response = await client.PostAsync(uri, httpContent, GetCancellationToken());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TResult>(content);
                    return result;
                }
                else if (!response.IsSuccessStatusCode)
                {
                    var ErrMsg = JsonConvert.DeserializeObject<HttpErrorResponse>(response.Content.ReadAsStringAsync().Result);
                    Application.Current.MainPage.DisplayAlert("Error", ErrMsg.ERROR, "OK");
                    return result;
                }
                else
                    return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DisposeCancellationToken();
            }
        }

        public static async Task PostToken()
        {
            var fcmtoken = CrossSecureStorage.Current.GetValue("FCMToken", "");
            var formVariables = new List<KeyValuePair<string, string>>();
            formVariables.Add(new KeyValuePair<string, string>("token", fcmtoken));
            HttpContent httpContent = new FormUrlEncodedContent(formVariables);
            var uri = UriPathBuilder.GetPath(ApiResources.FCMToken).ToString();
            //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(CrossSecureStorage.Current.GetValue("clientToken", "")))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CrossSecureStorage.Current.GetValue("clientToken", ""));
            var response = await client.PostAsync(uri, httpContent, GetCancellationToken());
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                CrossSecureStorage.Current.SetValue("TokenSent", "toekn");
            }
        }

        public static async Task<TResult> MediaPostAsync<TRequest, TResult>(string uri, List<KeyValuePair<string, string>> data, List<FileWrapper> files, string authenticationToken = null)
        {
            TResult result = default;
            try
            {

                var form = new MultipartFormDataContent();

                if (files != null)
                {

                    foreach (var item in files)
                    {
                        var fileContent = new ByteArrayContent(item.FileData);
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                        form.Add(fileContent, "submission_file[]", Path.GetFileName(item.Path));
                    }

                }
                else
                {
                    form.Add(new StringContent(""), "submission_file");
                }

                foreach (KeyValuePair<string, string> item in data)
                {
                    form.Add(new StringContent(item.Value), item.Key);
                }

                var client = new HttpClient();
                if (!string.IsNullOrWhiteSpace(authenticationToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
                var response = await client.PostAsync(uri, form, GetCancellationToken());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TResult>(content);
                    return result;
                }
                else if (!response.IsSuccessStatusCode)
                {
                    var ErrMsg = JsonConvert.DeserializeObject<HttpErrorResponse>(response.Content.ReadAsStringAsync().Result);
                    await Application.Current.MainPage.DisplayAlert("Error", ErrMsg.ERROR, "OK");
                    return result;
                }
                else
                    return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DisposeCancellationToken();
            }
        }

        public static async Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data, string authenticationToken = null)
        {
            TResult result = default;
            try
            {
                var json = JsonConvert.SerializeObject(data);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var client = new HttpClient();
                if (!string.IsNullOrWhiteSpace(authenticationToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
                var response = await client.PutAsync(uri, httpContent, GetCancellationToken());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TResult>(content);
                    return result;
                }
                else
                    return result;
            }
            catch (Exception ex) { throw ex; }
            finally { DisposeCancellationToken(); }
        }

        public static async Task<TResult> DeleteAsync<TResult>(string uri, string authenticationToken = null)
        {
            TResult result = default;
            try
            {
                var client = new HttpClient();
                if (!string.IsNullOrWhiteSpace(authenticationToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
                var response = await client.DeleteAsync(uri, GetCancellationToken());
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TResult>(content);
                    return result;
                }
                else
                    return result;
            }
            catch (Exception ex) { throw ex; }
            finally { DisposeCancellationToken(); }
        }
    }
}
