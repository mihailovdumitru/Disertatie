using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure
{
    public interface IRestHttpClient
    {
        Task<T> Get<T>(string baseUrl, string url, string accessToken = null);
        Task<TOut> Post<TIn, TOut>(string baseUrl, string url, TIn data, string accessToken = null, string contentType = null);
        Task<bool> Put<T>(string baseUrl, string url, T data);
        void SetMediaTypeRequestHeader(string mediaTypeRequestHeader);
    }
}
