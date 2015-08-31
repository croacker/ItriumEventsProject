using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ItriumData.data;

namespace ItriumListener
{
    public partial class CredentialHoldersViewForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            gvCredentialHoldersData.DataSource = getData();
            gvCredentialHoldersData.DataBind();
        }

        private List<CredentialHolder> getData()
        {
            using (var db = new ItriumDbContext())
            {
                return db.CredentialHolder.OrderBy(credentialHolder => credentialHolder.name).ToList();
            }
        }

        protected void gvCredentialHoldersData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCredentialHoldersData.PageIndex = e.NewPageIndex;
            loadData();
        }
    }
}