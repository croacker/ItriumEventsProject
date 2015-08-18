using System;
using System.Web;
using log4net;

namespace ItriumListener
{
    public class Global : HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Global));

        protected void Application_Start(object sender, EventArgs e)
        {
            ItriumListenerEnvironment.getInstance().start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Сконфигурировать логгер
        /// </summary>
        private void configLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Application start...");
        }
    }
}