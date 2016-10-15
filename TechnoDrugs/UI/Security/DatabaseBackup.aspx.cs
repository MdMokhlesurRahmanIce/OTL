using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.DataAccess;
using TechnoDrugs.Helper;
using SecurityModule.Provider;

namespace TechnoDrugs.UI.Security
{
    public partial class DatabaseBackup : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {            
            try
            {
                LogFileProvider logFileProvider = new LogFileProvider();
                bool value = logFileProvider.DatabaseBackup();
                if (value)
                {
                    lblsuccessfull.Text = "Database backup has been completed successfully";
                }
            }
            catch (Exception ex)
            {
                lblsuccessfull.Text = string.Empty;
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
        }
    }
}