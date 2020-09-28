using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Models
{
    public class DataUsageCheck
    {
        string pcIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
        public int AESStringUsage 
        {
            set;
            private get;
        }
        public int AESFileUsage
        {
            set;
            private get;
        }

        public DataUsageCheck()
        {
            AESStringUsage = 0;
            AESFileUsage = 0;
        }

        public UsageDetail initData()
        {
            return JsonConvert.DeserializeObject<UsageDetail>(requestMe("summary", null));
        }

        public SessionMk hashMe()
        {
            return JsonConvert.DeserializeObject<SessionMk>(requestMe("sessionMk", null));
        }

        public UsageDetail AESStringDone()
        {
            return JsonConvert.DeserializeObject<UsageDetail>(requestMe("action", "_str"));
        }
        public UsageDetail AESDeStringDone()
        {
            return JsonConvert.DeserializeObject<UsageDetail>(requestMe("action", "_deStr"));
        }

        public UsageDetail AESFileDone()
        {
            return JsonConvert.DeserializeObject<UsageDetail>(requestMe("action", "_file"));
        }

        public UsageDetail AESDeFileDone()
        {
            return JsonConvert.DeserializeObject<UsageDetail>(requestMe("action", "_deFile"));
        }

        public string requestMe(string _method, string _action)
        {
            var usageData = new Data();
            usageData.ip = pcIp;
            usageData.action = _action;

            var json = JsonConvert.SerializeObject(usageData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://api.rqn9.com/data/1.0/dapp/_/182230003154961/usage/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsync(_method, data).Result;

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    //response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    return responseBody;

                }
                else
                {
                    return "{\"success\": " + false + ", \"message\": {\"code\":" + response.StatusCode + ", \"detail\":" + response.ReasonPhrase + "}}";
                }
            }
            catch (HttpRequestException e)
            {
                return "{\"success\": " + false + ", \"message\": {\"code\":" + 500 + ", \"detail\":" + e.Message + "}}";
            }
        }
    }
}
