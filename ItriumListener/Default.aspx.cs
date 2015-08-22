using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ItriumData.data;

namespace ItriumListener
{
    public partial class DefaultWebFormIL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvErrorData.DataSource = getData();
            gvErrorData.DataBind();
        }

        private List<ErrorData> getData()
        {
            using (ItriumDbContext db = new ItriumDbContext())
            {
                return db.ErrorData.OrderBy(errorData => errorData.errorDate).ToList();
            }
        }
    }
}