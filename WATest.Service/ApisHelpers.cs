using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace WATest.Service
{

    public class ApisHelpers
    {
        private string _apiAdress;

        public ApisHelpers() => _apiAdress = WebConfigurationManager.AppSettings["ApiAdress"];

        public async Task<HttpResponseMessage> GetApi(string apiName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiAdress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(apiName);
                return Res;
            }
        }

        public async Task<HttpResponseMessage> PostApi(string apiName, string parameters) 
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiAdress);
                client.DefaultRequestHeaders.Clear();
                HttpContent content = new StringContent(parameters, Encoding.UTF8, "application/json");
                HttpResponseMessage Res = await client.PostAsync(apiName, content);
                return Res;
            }
        }
    }
}
