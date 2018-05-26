using Services.Infrastructure;
using System;

namespace Services
{
    public class Service//:IService
    {
        private readonly IRestHttpClient restHttpClient;
        private readonly string phoenixApiBaseUrl;
        public string verif;

        public void Salut()
        {
            verif = "Dima";
            Console.WriteLine("Salut");
        }
    }
}
