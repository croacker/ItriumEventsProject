using ItriumData.data;
using System;
using System.Collections.Generic;
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
        private const int DELAY_MSEC = 100;
        private const int DELAY_COUNT = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            //btnChangePassword.Attributes.Add("onclick", "return CallServerFunction();");
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
            var newPassword = ((Dictionary<string, object>)messageData)["newPassword"];
            var confirmPassword = ((Dictionary<string, object>)messageData)["confirmPassword"];

            bool result = changePasswordRun((string)newPassword);

            ChangePasswordResult changePasswordResult = new ChangePasswordResult();
            changePasswordResult.result = result ? "OK":"ERROR";
            changePasswordResult.message = result ? "Password changed" : "Password NOT changed";

            var json = serializer.Serialize(changePasswordResult);

            return json;
        }

        /// <summary>
        /// Изменить пароль пользователя
        /// </summary>
        /// <param name="newPassword"></param>
        private static bool changePasswordRun(string newPassword)
        {
            bool result = false;
            EventData eventData = waitItriumEvent(DateTime.Now);
            if(eventData != null)
            {

            }
            return result;
        }

        /// <summary>
        /// Ждать события от Итриум
        /// </summary>
        /// <param name="changeMoment"></param>
        /// <returns></returns>
        private static EventData waitItriumEvent(DateTime changeMoment)
        {
            EventData eventData = null;
            using (var db = new ItriumDbContext())
            {
                    for(int i = 0; i < DELAY_COUNT; i++)
                    {
                        eventData = db.EventData.Where(ed => ed.dateTime > changeMoment).First();
                        if(eventData != null)
                        {
                        break;
                        }
                    System.Threading.Thread.Sleep(DELAY_MSEC);
                }
            }
            return eventData;
        }
    }


    public class ChangePasswordResult
    {
        public string result;
        public string message;
    }
}