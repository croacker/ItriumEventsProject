using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ItriumData.data;

namespace ItriumListener
{
    public partial class ItriumEventsViewForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvItriumEventsData.DataSource = getData();
            gvItriumEventsData.DataBind();
        }

        private List<ItriumEventData> getData()
        {
            using (ItriumDbContext db = new ItriumDbContext())
            {
                return db.ItriumEventsData.OrderBy(errorData => errorData.ID).ToList();
            }
        }
    }
}