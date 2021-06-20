using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BSS_Shopping.Extension
{
    public static class HttpClientExtension
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpClient.PutAsync(url, content);
        }

        public static Task<HttpResponseMessage> GetAsJsonAsync(this HttpClient httpClient, string url)
        {
            //var dataAsString = JsonConvert.SerializeObject(data);

            return httpClient.GetAsync(url);
        }

        public static Task<HttpResponseMessage> DeleteAsJsonAsync(this HttpClient httpClient, string url)
        {
            //var dataAsString = JsonConvert.SerializeObject(data);

            return httpClient.DeleteAsync(url);
        }
    }
}
