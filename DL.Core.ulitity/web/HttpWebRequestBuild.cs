using DL.Core.ulitity.tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DL.Core.ulitity.web
{
    public class HttpWebRequestBuild
    {
        public static HttpWebRequestBuild Build => new HttpWebRequestBuild();
        public string Post(string url,Dictionary<string,object> dic,Dictionary<string,string> header=null,string type= HttpType.JsonType)=> PostMethod(url, dic, header, type);
 
        public T Post<T>(string url, Dictionary<string, object> dic, Dictionary<string, string> header = null, string type = HttpType.JsonType)
        {
            var data = PostMethod(url, dic, header, type);
            var obj = data.FromJson<T>();
            return obj;
        }

        public T Get<T>(string url, Dictionary<string, string> dic, Dictionary<string, string> header = null, string type = HttpType.FromType)
        {
            var data = GetMethod(url, dic, header, type);
            var obj = data.FromJson<T>();
            return obj;
        }
        public string Get(string url, Dictionary<string, string> dic, Dictionary<string, string> header = null, string type = HttpType.FromType) => GetMethod(url,dic,header,type);
        private string PostMethod(string url, Dictionary<string, object> dic, Dictionary<string, string> header = null, string type=null)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            request.ContentType = type;
            var jsondata = dic.ToJson();
            byte[] bytes = Encoding.UTF8.GetBytes(jsondata);
            request.ContentLength = bytes.Length;
            if (header != null && header.Count > 0)
            {
                foreach (var item in header.Keys)
                {
                    var value = header[item];
                    request.Headers.Add(item, value);
                }
            }
            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            var response = request.GetResponse();
            var resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            var result = reader.ReadToEnd();
            return result;
        }
        private string GetMethod(string url, Dictionary<string, string> dic, Dictionary<string, string> header = null,string type =null)
        {
            StringBuilder sb = new StringBuilder();
            if (dic != null && dic.Count > 0)
            {
                foreach (var item in dic.Keys)
                {
                    sb.Append($"{item}={dic[item]}&");
                }
            }
            var requestUrl = url + sb.ToString().ExpenSubstr();
            HttpWebRequest request = WebRequest.CreateHttp(requestUrl);
            request.Method = "GET";
            request.ContentType = type;
            var jsondata = dic.ToJson();
            byte[] bytes = Encoding.UTF8.GetBytes(jsondata);
            request.ContentLength = bytes.Length;
            if (header != null && header.Count > 0)
            {
                foreach (var item in header.Keys)
                {
                    var value = header[item];
                    request.Headers.Add(item, value);
                }
            }
            var response = request.GetResponse();
            var resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            var result = reader.ReadToEnd();
            return result;
        }
    }
}
