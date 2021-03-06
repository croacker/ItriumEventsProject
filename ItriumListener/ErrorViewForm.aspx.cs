﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using ItriumData.data;

namespace ItriumListener
{
    public partial class ErrorViewForm : Page
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