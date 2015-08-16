﻿using System;
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

            writeData(getData(requestData));
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
            }

            return data;
        }

        private void writeData(Dictionary<string, string> data)
        {
            writeDataService.writeData(data);
            persistService.persistEvent(data);
        }
    }
}