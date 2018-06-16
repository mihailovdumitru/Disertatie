using log4net;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure
{
    public class RestHttpClient : IRestHttpClient
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

                    var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(data);
                    }

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Rest Get error: ", ex);

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

                    HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, contentType ?? "application/json");
                    var response = await client.PostAsync(url, contentPost);

                    if (response.IsSuccessStatusCode)
                    {
                        var dataOut = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<TOut>(dataOut);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Rest Post error: ", ex);
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
                }
            }
            catch (Exception ex)
            {
                _log.Error("Rest Put error: ", ex);
            }

            return false;
        }

        public async Task<bool> Delete(string baseUrl, string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.DeleteAsync(url).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Rest Delete error: ", ex);
            }

            return false;
        }

        public void SetMediaTypeRequestHeader(string mediaTypeRequestHeader)
        {
            this.mediaTypeRequestHeader = mediaTypeRequestHeader;
        }
    }
}