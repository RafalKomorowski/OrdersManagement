using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;

namespace Helpers
{
    public static class XpoHelper
    {
        #region Session

        /// <summary>
        /// Extension method that provides access to Session.
        /// We assume that XPO is used as an ORM  (not EF, etc).
        /// </summary>
        public static Session Session(this IObjectSpace objectSpace)
        {
            Session result = null;

            if (objectSpace != null)
            {
                if (objectSpace is XPObjectSpace)
                    result = ((XPObjectSpace)objectSpace).Session;
                else if (objectSpace is NonPersistentObjectSpace)
                {
                    // We should not require Session from within Non Persistent Object Space. Every time it happens we need to check in the call stack why it happened 
                    // and if some scenario is done correctly.
                    System.Diagnostics.Debugger.Break();
                    return null;
                }
            }

            return result;
        }

        #endregion    
    }
}
