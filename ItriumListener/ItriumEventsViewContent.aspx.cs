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
            loadData();
        }

        private void loadData()
        {
            gvItriumEventsData.DataSource = getData();
            gvItriumEventsData.DataBind();
        }

        private List<object> getData()
        {
            var events = new List<object>();
            using (var db = new ItriumDbContext())
            {
                var queEvents = (from e in db.EventData
                                 select new {e.ID,
                                     e.dateTime,
                                     credentialHolder = e.credentialHolder.name,
                                     e.сard,
                                     e.clockNumber,
                                     accessPoint = e.eventSource.accessPointName,
                                     e.headline,
                                     e.credentialToken
                                     }).OrderByDescending(e => e.dateTime).ToList();
                events.AddRange(queEvents);
            }
            return events;
        }

        protected void gvItriumEventsData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItriumEventsData.PageIndex = e.NewPageIndex;
            loadData();
        }
    }
}