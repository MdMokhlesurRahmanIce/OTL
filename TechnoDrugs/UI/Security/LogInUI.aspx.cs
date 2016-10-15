using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using BaseModule;
using System.Data.SqlClient;
using System.Configuration;

namespace TechnoDrugs.UI.Security
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {            
            try
            {
                var userProvider = new UserProvider();
                UserProvider user = userProvider.GetByUserNameAndPassword(txtUserName.Text, txtPassword.Text);

                Session[SessionConstants.User] = user.UserID;
                Session["UserName"] = user.UserID;
                Session[SessionConstants.IsAdmin] = user.IsAdmin;
                Session["IsAdmin"] = user.IsAdmin;
                Session["ID"] = user.ID;
                Session["WarhouseID"] = user.WarehouseID;
                Session["WarehouseName"] = user.WarehouseName;
                Session["VatID"] = user.VatID;
                #region
                String MyIp = "-";
                var provider = new LogFileProvider();
                DateTime dt = DateTime.Now;
                provider.LogID = 0;
                provider.UserID = user.UserID;
                provider.LogInTime = dt;

                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());

                foreach (IPAddress ipAddress in ipHost.AddressList)
                {
                    if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                    {
                        MyIp = ipAddress.ToString();
                    }
                }
                provider.IPAddress = MyIp;
                provider.LogOutTime = dt;
                //long LogID = provider.Save();
                //Session[SessionConstants.LogID] = LogID.ToString();
                #endregion

                Response.Redirect("~/UI/Security/IndexUI.aspx", false);
            }
            catch (Exception)
            {
                var userProvider = new UserProvider();
                UserProvider user = userProvider.GetByUserNameAndPassword(txtUserName.Text,txtPassword.Text);

                if (user == null)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid credential')</script>");
                }
            }
        }

    }
}