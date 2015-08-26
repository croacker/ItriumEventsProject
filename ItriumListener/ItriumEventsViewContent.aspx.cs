using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ItriumData.data;
using System.Data.Entity;

namespace ItriumListener
{
    public partial class ItriumEventsViewForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvItriumEventsData.DataSource = getData();
            gvItriumEventsData.DataBind();
        }

        private List<object> getData()
        {
            var events = new List<object>();
            using (var db = new ItriumDbContext())
            {
                var queEvents = (from e in db.ItriumEventsData select new {e.ID, e.dateTime, e.typeName, credentialHolder = e.credentialHolder.name, e.clockNumber }).ToList();
                events.AddRange(queEvents);
            }
            return events;
        }
    }
}