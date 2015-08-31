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
            loadData();
        }

        private void loadData()
        {
            gvErrorData.DataSource = getData();
            gvErrorData.DataBind();
        }

        private List<ErrorData> getData()
        {
            using (ItriumDbContext db = new ItriumDbContext())
            {
                return db.ErrorData.OrderByDescending(errorData => errorData.errorDate).ToList();
            }
        }

        protected void gvErrorData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvErrorData.PageIndex = e.NewPageIndex;
            loadData();
        }
    }
}