using Conquerorhub.Repository;
using Conquerorhub.Request.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Conquerorhub.Request.Repository
{
    public class RepositoryBase
    {
        private readonly string _apiUrlBase;

        protected RepositoryBase()
        {
            _apiUrlBase = Settings.ApiUrlBase;
        }

        private string MakeRequest(string sessionToken, HttpVerb method, string parameters, string postData)
        {
            var requestedUrl = _apiUrlBase + parameters;
            var webRequest = (HttpWebRequest)WebRequest.Create(requestedUrl);
            webRequest.Method = method.ToString();
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = 0;
            webRequest.Accept = "application/json; charset=utf-8";

            if (!string.IsNullOrEmpty(sessionToken))
                webRequest.Headers.Add("X-SessionToken", sessionToken);

            var encoding = Encoding.GetEncoding("ISO-8859-1");

            webRequest.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(encoding.GetBytes(Settings.ApiUserName + ":" + Settings.ApiPassword)));

            if (!string.IsNullOrEmpty(postData) && method == HttpVerb.POST)
            {
                var encodedData = WebUtility.UrlEncode(postData);

                Console.Write(encodedData);

                if (encodedData != null)
                {
                    var bytes = encoding.GetBytes(encodedData);

                    webRequest.ContentLength = bytes.Length;

                    using (var writeStream = webRequest.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }

            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (Exception ex)
            {
                return null;
            }

            if (response.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException($"Request failed. Received HTTP {response.StatusCode}");

            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        var respone = reader.ReadToEnd();

                        Console.Write(respone);

                        return respone;
                    }
                }
            }

            return "";
        }

        private string MakeResponse(string sessionToken, HttpVerb method, string parameters, string postData)
        {
            string _apiUrlBase = Settings.ApiUrlBase;
            var requestedUrl = _apiUrlBase + parameters;
            var webRequest = (HttpWebRequest)WebRequest.Create(requestedUrl);
            webRequest.Method = method.ToString();
            webRequest.ContentType = "application/json";
            // webRequest.ContentLength = 0;
            webRequest.Accept = "application/json; charset=utf-8";
            if (!string.IsNullOrEmpty(sessionToken))
                webRequest.Headers.Add("X-SessionToken", sessionToken);

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (Exception ex)
            {
                return null;
            }

            if (response.StatusCode != HttpStatusCode.OK)
                throw new ApplicationException($"Request failed. Received HTTP {response.StatusCode}");

            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        var respone = reader.ReadToEnd();

                        Console.Write(respone);

                        return respone;
                    }
                }
            }

            return "";
        }

        protected T GetAndParseData<T>(string sessionToken, string parameters)
        {
            var data = MakeRequest(sessionToken, HttpVerb.GET, parameters, null);
            if (data != null)
            {
                return (T)JsonConvert.DeserializeObject(data, typeof(T));
            }
            return default(T);
        }

        protected T PostAndGetData<T>(string sessionToken, string parameters, string postData)
        {
            var data = MakeResponse(sessionToken, HttpVerb.POST, parameters, postData);

            return (T)JsonConvert.DeserializeObject(data, typeof(T));
        }

        protected RequestResult<T> GetAndParseRequestResult<T>(string sessionToken, string parameters) where T : class
        {
            try
            {
                var data = MakeRequest(sessionToken, HttpVerb.GET, parameters, null);

                var responseObj = (T)JsonConvert.DeserializeObject(data, typeof(T));

                return responseObj as RequestResult<T>;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return RequestResult<T>.NewUnexpectedError(null);
            }
        }
    }
}
