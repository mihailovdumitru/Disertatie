using Model.Test;
using Services.Infrastructure;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Services.Endpoints;

namespace Services
{
    public class Service: IService
    {
        private readonly IRestHttpClient restHttpClient;
        private readonly string testApiUrl;

        public Service(IRestHttpClient restHttpClient)
        {
            this.restHttpClient = restHttpClient;
            testApiUrl = ConfigurationManager.AppSettings.Get("TestApiUrl");
        }


        public async Task<ActionResult> AddTest(Test test)
        {
            return await restHttpClient.Post<Test, ActionResult>(testApiUrl, $"{TestEndpoint.AddTest}", test);
        }
    }
}
