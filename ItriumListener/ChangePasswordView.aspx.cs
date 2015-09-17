using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ItriumListener
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnChangePassword.Attributes.Add("onclick", "return CallServerFunction();");
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            
        }

        [WebMethod]
        public static string ChangePassword(string message)
        {
            return message;
        }
    }
}