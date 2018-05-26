using ServicesDemo.Infrastructure;
using System;

namespace ServicesDemo
{
    public class Service//:IService
    {
        private readonly IRestHttpClient restHttpClient;
        private readonly string phoenixApiBaseUrl;

        public void Salut()
        {
            Console.WriteLine();
        }
    }
}
