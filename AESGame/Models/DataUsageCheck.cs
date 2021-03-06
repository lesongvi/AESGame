﻿using Newtonsoft.Json;
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
        string pcIp = getMyIp();
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

        public BlogDetail BlogNotification()
        {
            return JsonConvert.DeserializeObject<BlogDetail>(requestMe("blog", null));
        }

        public static string getMyIp()
        {
            try
            {
                return new System.Net.WebClient().DownloadString("https://api.ipify.org");
            }
            catch (Exception e)
            {
                return "0.0.0.0";
            }
        }

        public string requestMe(string _method, string _action)
        {
            var usageData = new Data();
            usageData.ip = pcIp;
            usageData.action = _action;

            if(usageData.ip == "0.0.0.0")
            {
                return "{\"success\": " + false + ", \"message\": {\"code\":" + 503 + ", \"detail\": \"Vui lòng kiểm tra kết nối mạng của bạn!\"}}"; ;
            }

            var json = JsonConvert.SerializeObject(usageData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.rqn9.com/data/1.0/dapp/_/182230003154961/usage/");

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
