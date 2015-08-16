using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using log4net;

namespace ItriumCls
{
    class SubscribeService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SubscribeService));

        public bool subscribed = false;

        string renewSubscribeUrl = string.Empty;
        string terminationTime = string.Empty;

        /// <summary>
        /// Подписаться
        /// </summary>
        public void beginSubscribe()
        {
            log.Info("Begin subscribe, create HttpWebRequest");
            HttpWebRequest webRequest = CreateWebRequest(AppProperties.WS_URL, AppProperties.WS_ACTION_SUBSCRIBE);
            webRequest.CookieContainer = new ItriumAuthCookie();
            log.Info("Add to HttpWebRequest SOAP Envelope");
            insertSoapEnvelopeIntoWebRequest(createSoapEnvelopeSubscribe(), webRequest);

            RequestState requestState = new RequestState();
            log.Info("Send Subscribe request...");
            string soapResult = sendRequest(webRequest);

            EventProcessor eventProcessor = new EventProcessor();
            Dictionary<string, string> data = eventProcessor.getDataSubscribe(soapResult);
            log.Info("Result Subscribe data:[" + data + "]");
            renewSubscribeUrl = data["address"];
            log.Info("Subscribe address:[" + data["address"] + "]");
            terminationTime = data["terminationTime"];
            log.Info("Subscribe termination time:[" + data["terminationTime"] + "]");
            subscribed = true;
        }

        /// <summary>
        /// Обновить подписку
        /// </summary>

        public void renewSubscribe()
        {
            log.Info("Begin Renew subscribe, create HttpWebRequest");
            HttpWebRequest webRequest = CreateWebRequest(renewSubscribeUrl, renewSubscribeUrl + "/" + AppProperties.WS_ACTION_RENEW);
            webRequest.CookieContainer = new ItriumAuthCookie();
            insertSoapEnvelopeIntoWebRequest(createSoapEnvelopeSubscribe(), webRequest);

            RequestState requestState = new RequestState();
            string soapResult = sendRequest(webRequest);

            EventProcessor eventProcessor = new EventProcessor();
            Dictionary<string, string> data = eventProcessor.getDataRenew(soapResult);
            log.Info("Result RenewSubscribe data:[" + data + "]");
            terminationTime = data["terminationTime"];
            log.Info("RenewSubscribe termination time:[" + data["terminationTime"] + "]");
            
        }

        /// <summary>
        /// Отписаться        
        /// </summary>
        public void unsubscribe()
        {
            log.Info("Begin Unsubscribe, create HttpWebRequest");
            HttpWebRequest webRequest = CreateWebRequest(AppProperties.WS_URL, AppProperties.WS_ACTION_UNSUBSCRIBE);
            webRequest.CookieContainer = new ItriumAuthCookie();
            insertSoapEnvelopeIntoWebRequest(createSoapEnvelopeUnsubscribe(), webRequest);

            RequestState requestState = new RequestState();
            string soapResult = sendRequest(webRequest);
            log.Info("Unsubscribe data:[" + soapResult + "]");
        }

        /// <summary>
        /// Создать веб-запрос
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns> 
        private HttpWebRequest CreateWebRequest(string url, string action)
        {
            return ItriumWebRequestBuilder.newWebRequest(action);
        }

        /// <summary>
        /// SOAP запрос подписка
        /// </summary>
        /// <returns></returns> 
        private XmlDocument createSoapEnvelopeSubscribe()
        {
            return SoapEnvelopeBuilder.newSubscribe();
        }

        /// <summary>
        /// SOAP запрос продление
        /// </summary>
        /// <returns></returns> 
        private XmlDocument createSoapEnvelopeRenew()
        {
            return SoapEnvelopeBuilder.newRenew(terminationTime);
        }

        /// <summary>
        /// SOAP запрос отписка
        /// </summary>
        /// <returns></returns>
        private XmlDocument createSoapEnvelopeUnsubscribe()
        {
            return SoapEnvelopeBuilder.newUnsubscribe();
        }

        // 
        private void insertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        /// <summary>
        /// Отправить запрос
        /// </summary>
        /// <param name="webRequest"></param>
        /// <returns></returns>
        private string sendRequest(HttpWebRequest webRequest)
        {
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            return readResult(webRequest, asyncResult);
        }

        /// <summary>
        /// Прочитать результат http запроса
        /// </summary>
        /// <param name="webRequest"></param>
        /// <returns></returns>
        private string readResult(HttpWebRequest webRequest, IAsyncResult asyncResult)
        {
            string soapResult = string.Empty;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    log.Info("Process Http response...");
                    soapResult = rd.ReadToEnd();
                    log.Info("Http response data:[" + soapResult + "]");
                }
            }
            return soapResult;
        }

        private DateTime extractReconnectTime(string soapResult)
        {
            return DateTime.Now;
        }

    }
}
