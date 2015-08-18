using System.Net;

namespace ItriumCls
{
    static class ItriumWebRequestBuilder
    {
        public static HttpWebRequest newWebRequest(string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AppProperties.ItriumWsUrl);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
    }
}
