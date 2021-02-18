using System;
using System.ComponentModel;
using System.Configuration;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;


namespace Domain.Security
{
#if DEBUG

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class AuthenticationStandardForDebug : AuthenticationStandard
    {
        public override bool AskLogonParametersViaUI
        {
            get
            {
                string automaticDebugUser = ConfigurationManager.AppSettings["AutomaticDebugUser"];
                if (!string.IsNullOrEmpty(automaticDebugUser))
                    return false;
                else
                    return base.AskLogonParametersViaUI;
            }
        }

        public override object Authenticate(IObjectSpace objectSpace)
        {
            string debugUserName = ConfigurationManager.AppSettings["AutomaticDebugUser"];
            if (!string.IsNullOrEmpty(debugUserName))
            {
                CriteriaOperator debugUserCriteria = new BinaryOperator(nameof(Domain.Security.Employee.UserName), debugUserName);

                object debugUser = objectSpace.FindObject(UserType, debugUserCriteria);
                if (debugUser == null)
                    throw new Exception("AutomaticDebugUser has been specified but it was not found. Please check the config file or contact your administrator.");

                return debugUser;
            }
            else
                return base.Authenticate(objectSpace);
        }
    }

#endif
}
