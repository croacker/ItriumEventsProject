using ItriumData.data;
using log4net;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ItriumListener
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(WebForm1));

        private const int DELAY_MSEC = 2000;
        private const int DELAY_COUNT = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Принимает запрос на изменение пароля
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ChangePassword(string message)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var messageData = serializer.DeserializeObject(message);
            var employeeNumber = ((Dictionary<string, object>)messageData)["employeeNumber"];
            var newPassword = ((Dictionary<string, object>)messageData)["newPassword"];
            var confirmPassword = ((Dictionary<string, object>)messageData)["confirmPassword"];

            ChangePasswordResult changePasswordResult = changePasswordRun((string)employeeNumber, (string)newPassword);
            var json = serializer.Serialize(changePasswordResult);

            return json;
        }

        /// <summary>
        /// Изменить пароль пользователя
        /// </summary>
        /// <param name="newPassword"></param>
        private static ChangePasswordResult changePasswordRun(string employeeNumber, string newPassword)
        {
            ChangePasswordResult changePasswordResult = null;
            EventData eventData = waitItriumEvent(employeeNumber, DateTime.Now);
            if(eventData != null)
            {
                changePasswordResult = changeNtUserPassword(employeeNumber, newPassword);
            }
            else
            {
                changePasswordResult = new ChangePasswordResult();
                changePasswordResult.result = "ERROR";
                changePasswordResult.message ="Время ожидания истекло!";
            }
            return changePasswordResult;
        }

        /// <summary>
        /// Ждать события от Итриум
        /// </summary>
        /// <param name="changeMoment"></param>
        /// <returns></returns>
        private static EventData waitItriumEvent(string employeeNumber, DateTime changeMoment)
        {
            EventData eventData = null;
            using (var db = new ItriumDbContext())
            {
                    for(int i = 0; i < DELAY_COUNT; i++)
                    {
                    List<EventData> events = db.EventData.Where(ed => ed.dateTime > changeMoment
                    && ed.clockNumber.Equals(employeeNumber)).ToList();
                    if(events.Count != 0)
                    {
                        eventData = events[0];
                    }
                        
                        if(eventData != null)
                        {
                        break;
                        }
                    System.Threading.Thread.Sleep(DELAY_MSEC);
                }
            }
            return eventData;
        }

        private static ChangePasswordResult changeNtUserPassword(string employeeNumber, string newPassword)
        {
            ChangePasswordResult result = new ChangePasswordResult();

            try { 
                DirectoryEntry uEntry = new DirectoryEntry(employeeNumber); 
                uEntry.Invoke("SetPassword", new object[] { newPassword });
                uEntry.Properties["LockOutTime"].Value = 0;
                uEntry.Close();

                result.result = "OK";
                result.message = "Пароль успешно изменен";
            }
            catch(System.Runtime.InteropServices.COMException e)
            {
                log.Error(e.Message, e);
                result.result = "ERROR";
                result.message = e.Message;
            }

            return result;
        }
    }   

    public class ChangePasswordResult
    {
        public string result;
        public string message;
    }
}