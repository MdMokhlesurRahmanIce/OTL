using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseModule;
using SecurityModule.Provider;

namespace TechnoDrugs
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }
            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Session["ID"]== null
                if (!HasAllSession())
                {
                    Response.Redirect("~/UI/Security/LogInUI.aspx", false);
                    return;
                }
                var userID = (bool)Session["IsAdmin"];
                var provider = new MenuProvider();
                var UserID = (string)HttpContext.Current.Session[SessionConstants.User];
                DataTable menuTable = userID ? provider.GetAll() : provider.GetAllByUserGroupID(UserID);
                menuDiv.InnerHtml = MenuHelper.GetHtml(menuTable, "ID", "ParentId", "Caption", "Location", "myMenu");
            }
        }

        private bool HasAllSession()
        {
            if (string.IsNullOrEmpty(Session[SessionConstants.User] as string))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Session["UserName"] as string))
            {
                return false;
            }

            if (0 > (int)Session["VatID"])
            {
                return false;
            }
            return 0 <= (int)Session["WarhouseID"];
        }
        protected void btnLogOut_Click(Object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Write("<script>javascript: parent.opener=''; " + "parent.close();</script>");
            Response.RedirectPermanent("~/UI/Security/LogInUI.aspx");
            Response.End();
        }
    }
}