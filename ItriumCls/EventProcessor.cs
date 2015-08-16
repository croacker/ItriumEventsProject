using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using log4net;

namespace ItriumCls
{
    public class EventProcessor
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EventProcessor));
        
        /// <summary>
        /// Получить данные из ответа на запрос Подписка
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        public Dictionary<string, string> getDataSubscribe(string subscribeData)
        {
            try
            {
                return getDataFromSubscribe(subscribeData);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        ///  Получить данные из ответа на запрос Обновление подписки
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        public Dictionary<string, string> getDataRenew(string renewData)
        {
            try
            {
                return getDataFromRenew(renewData);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        ///  Получить данные из события от Итриум
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        public Dictionary<string, string> getDataEvent(string eventData)
        {
            try
            {
                return getDataFromEvent(eventData);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        private Dictionary<string, string> getDataFromSubscribe(string subscribeData)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(subscribeData);

            string address = xmlDocument.ChildNodes.Item(1).ChildNodes.Item(1).FirstChild.FirstChild.FirstChild.FirstChild.Value;
            string terminationTime = xmlDocument.ChildNodes.Item(1).ChildNodes.Item(1).FirstChild.ChildNodes.Item(1).FirstChild.Value;

            result.Add("address", address);
            result.Add("terminationTime", terminationTime);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        private Dictionary<string, string> getDataFromRenew(string renewData)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(renewData);

            string terminationTime = xmlDocument.ChildNodes.Item(1).ChildNodes.Item(1).FirstChild.FirstChild.FirstChild.Value;

            result.Add("terminationTime", terminationTime);

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        private Dictionary<string, string> getDataFromEvent(string eventData)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(eventData);

            log.Info("getDataFromEvent: xmlDocument.Value:" + xmlDocument.Value);

            XmlNode notoficationMessageNode = getNotoficationMessageNode(xmlDocument);

            XmlNode messageNode = getMessageNode(notoficationMessageNode);

            XmlNode dataNode = getDataNode(messageNode);

            string credentialHolderName = string.Empty;
            string clockNumber = string.Empty;
            if (dataNode != null)
            {
                foreach (XmlNode xmlNode in dataNode.ChildNodes)
                {
                    if (xmlNode.Attributes["Name"].Value.Equals("CredentialHolderName"))
                    {
                        credentialHolderName = xmlNode.Attributes["Value"].Value;
                    }

                    if (xmlNode.Attributes["Name"].Value.Contains("ClockNumber"))
                    {
                        clockNumber = xmlNode.Attributes["Value"].Value;
                    }
                }
            }

            log.Info("credentialHolderName:" + credentialHolderName);
            Console.WriteLine(credentialHolderName);
            log.Info("clockNumber:" + clockNumber);
            Console.WriteLine(clockNumber);

            result.Add("credentialHolderName", credentialHolderName);
            result.Add("clockNumber", clockNumber);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        private XmlNode getNotoficationMessageNode(XmlDocument xmlDocument)
        {
            return xmlDocument.ChildNodes.Item(0).ChildNodes.Item(1).ChildNodes.Item(0).ChildNodes.Item(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notoficationMessageNode"></param>
        /// <returns></returns>
        private static XmlNode getMessageNode(XmlNode notoficationMessageNode)
        {
            XmlNode messageNode = null;
            foreach (XmlNode xmlNode in notoficationMessageNode.ChildNodes)
            {
                if (xmlNode.Name.Equals("wsn:Message"))
                {
                    messageNode = xmlNode;
                    break;
                }
            }
            return messageNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notoficationMessageNode"></param>
        /// <returns></returns>
        private XmlNode getDataNode(XmlNode messageNode)
        {
            XmlNode dataNode = null;
            if (messageNode != null)
            {
                foreach (XmlNode xmlNode in messageNode.ChildNodes.Item(0).ChildNodes)
                {
                    if (xmlNode.Name.Equals("onvif:Data"))
                    {
                        dataNode = xmlNode;
                        break;
                    }
                }
            }
            return dataNode;
        }

        /// <summary>
        /// Для примера
        /// </summary>
        /// <param name="args"></param>
        void example(string[] args)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader("c:\\tmp\\2\\renew.xml"))
                {
                    Dictionary<string, string> data = getDataFromRenew(streamReader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }
    }
}
