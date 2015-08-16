using System.IO;
using System.Web;
using log4net;

namespace ItriumListener
{
    public class DispatcherHttpListener : IHttpHandler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DispatcherHttpListener));

        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            log.Info("Process Itrium request....");
            HttpRequest Request = context.Request;

            string requestData;
            using (var streamReader = new StreamReader(Request.InputStream))
            {
                requestData = streamReader.ReadToEnd();
            }
            log.Info("requestData=[" + requestData + "]");
        }

        
    }
}