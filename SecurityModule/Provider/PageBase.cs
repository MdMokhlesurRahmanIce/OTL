using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using SecurityModule.DataAccess;
using BaseModule;

namespace SecurityModule.Provider
{
    public class PageBase : Page
    {
        public Boolean RequiresAuthorization = false;
        protected internal FormAccessRights accessRights
        {
            get
            {
                if (ViewState["accessRights"].IsNull())
                    return new FormAccessRights();
                else
                    return (FormAccessRights)ViewState["accessRights"];
            }
            set
            {
                ViewState["accessRights"] = value;
            }
        }
        protected override void OnPreLoad(EventArgs e)
        {
            if (IsPostBack.IsFalse() && RequiresAuthorization)
            {
                GetFormAccess();
            }
            base.OnPreLoad(e);
        }
        protected internal void GetFormAccess()
        {
            //SecurityManager manager = new SecurityManager();
            FormAccessRights obj = new FormAccessRights();

            String formName = string.Empty;

        #if DEBUG
            {
                formName = Request.Url.AbsolutePath;
            }
        #else
            {
                formName = Request.Url.AbsolutePath.Replace(@"/ERP","");
            }
        #endif
            try
            {
                if ((bool)HttpContext.Current.Session[SessionConstants.IsAdmin] == true)
                {
                    accessRights = new FormAccessRights
                    {
                        CanSelect = true,
                        CanInsert = true,
                        CanUpdate = true,
                        CanDelete = true,
                        CanSend = true,
                        CanCheck = true,
                        CanApprove = true,
                        CanPreview = true,
                        CanReceive = true
                    };
                }
                else
                {
                    this.accessRights = obj.GetFormAccessRightsByUserCodeAndFormName(HttpContext.Current.Session[SessionConstants.User].ToString(), formName);
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }
        protected internal string CheckUserAuthentication(string mode)
        {
            try
            {
                if (accessRights.CanInsert.IsFalse() && mode == "Save")
                {
                    //throw new System.InvalidOperationException("You have no permission to add any information from this page.");

                    return "You have no permission to do this operation.";
                }
                
                else if (accessRights.CanUpdate.IsFalse() && mode == "Update")
                {
                   // throw new System.InvalidOperationException("You have no permission to update any information in this page.");
                    return "You have no permission to do this operation.";
                }
                else if (accessRights.CanDelete.IsFalse() && mode == "Delete")
                {
                    //throw new System.InvalidOperationException("You have no permission to delete any information in this page.");
                    return "You have no permission to do this operation.";
                }
                if (accessRights.CanSend.IsFalse() && mode == "Send")
                {
                    //throw new System.InvalidOperationException("You have no permission to add any information from this page.");

                    return "You have no permission to do this operation.";
                }
                if (accessRights.CanCheck.IsFalse() && mode == "Check")
                {
                    //throw new System.InvalidOperationException("You have no permission to add any information from this page.");

                    return "You have no permission to do this operation.";
                }
                if (accessRights.CanApprove.IsFalse() && mode == "Approve")
                {
                    //throw new System.InvalidOperationException("You have no permission to add any information from this page.");

                    return "You have no permission to do this operation.";
                }
                else if (accessRights.CanPreview.IsFalse() && mode == "Preview")
                {
                    //throw new System.InvalidOperationException("You have no permission to preview any information in this page.");
                    return "You have no permission to do this operation.";
                }
                else if (accessRights.CanReceive.IsFalse() && mode == "Received")
                {
                    //throw new System.InvalidOperationException("You have no permission to print any information in this page.");
                    return "You have no permission to do this operation.";
                }
                return "";
            }
            catch (Exception ex)
            {                
                return ex.ToString();
            }            
        }
    }
}
