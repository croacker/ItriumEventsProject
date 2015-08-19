using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using ItriumCls;
using log4net;

namespace ItriumListener
{
    public class ItriumEventHttpListener : IHttpHandler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof (ItriumEventHttpListener));

        private readonly WriteDataService writeDataService;
        private readonly PersistService persistService;

        public ItriumEventHttpListener()
        {
            writeDataService = new WriteDataService();
            persistService = new PersistService();
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            log.Info("Process Itrium request....");
            HttpRequest Request = context.Request;

            string requestData;
            using (var streamReader = new StreamReader(Request.InputStream))
            {
                requestData = streamReader.ReadToEnd();
            }

            writeOriginal(requestData);
            writeData(getData(requestData));
        }

        private void writeOriginal(string requestData)
        {
            persistService.persistEventOriginal(requestData);
        }

        private Dictionary<string, string> getData(string eventData)
        {
            var data = new Dictionary<string, string>();
            EventProcessor eventProcessor = new EventProcessor();
            try
            {
                data = eventProcessor.getDataEvent(eventData);
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                persistError("Error GetData from SOAP request", e);
            }

            return data;
        }

        private void persistError(string title, Exception exception)
        {
            persistService.persistError(title, exception);
        }

        private void writeData(Dictionary<string, string> data)
        {
            writeDataService.writeData(data);
            persistService.persistEvent(data);
        }
    }
}