using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure
{
    public class RestHttpClient:IRestHttpClient
    {
        private string mediaTypeRequestHeader;

        public async Task<T> Get<T>(string baseUrl, string url, string accessToken = null)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue($"application/{mediaTypeRequestHeader}json"));

                    /*if (!string.IsNullOrEmpty(accessToken))
                    {
                        client.SetBearerToken(accessToken);
                    }*/

                    //LoggerManager.LogInfo($"Get - {baseUrl} + {url}");
                    var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                    //LoggerManager.LogInfo($"Get - {baseUrl} + {url} - with response.IsSuccessStatusCode: {response.IsSuccessStatusCode}");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(data);
                    }
                    //LoggerManager.LogError($"{baseUrl}+{url} - {response.StatusCode} - {response}");
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.LogError($"{baseUrl}+{url} - {ex.StackTrace}");
                return default(T);
            }
        }

        public async Task<TOut> Post<TIn, TOut>(string baseUrl, string url, TIn data, string accessToken = null, string contentType = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue($"application/{mediaTypeRequestHeader}json"));

                    /*if (!string.IsNullOrEmpty(accessToken))
                    {
                        client.SetBearerToken(accessToken);
                    }*/

                    HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, contentType ?? "application/json");
                    //LoggerManager.LogInfo($"Post -{baseUrl} + {url}");
                    var response = await client.PostAsync(url, contentPost);

                    if (response.IsSuccessStatusCode)
                    {
                        var dataOut = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<TOut>(dataOut);
                    }
                    //LoggerManager.LogError($"{baseUrl}+{url} - {response.StatusCode} - {response}");
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.LogError($"{baseUrl}+{url}- {ex.StackTrace}");
            }

            return default(TOut);
        }

        public async Task<bool> Put<T>(string baseUrl, string url, T data)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, contentPost).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                   // LoggerManager.LogError($"{baseUrl}+{url} - {response.StatusCode} - {response}");
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.LogError($"{baseUrl}+{url}- {ex.StackTrace}");
            }
            return false;
        }

        public void SetMediaTypeRequestHeader(string mediaTypeRequestHeader)
        {
            this.mediaTypeRequestHeader = mediaTypeRequestHeader;
        }
    }
}
