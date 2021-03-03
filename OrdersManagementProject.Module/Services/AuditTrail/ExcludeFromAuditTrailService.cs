using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.AuditTrail;
using OrdersManagementProject.Module.AdditionalAttributes;

namespace OrdersManagementProject.Module.Services.AuditTrail
{
    /// <summary>
    /// Removes properties with the [ExcludeFromAuditTrail] attribute from the audit trail
    /// </summary>
    public static class ExcludeFromAuditTrailService
    {
        public static void Exclude(AuditTrailSettings audiTrailSettings)
        {
            foreach (ITypeInfo typeInfo in XafTypesInfo.Instance.PersistentTypes)
            {
                foreach (IMemberInfo member in typeInfo.Members)
                {
                    ExcludeFromAuditTrailAttribute attribute = member.FindAttribute<ExcludeFromAuditTrailAttribute>();
                    if (attribute != null)
                    {
                        Type type = member.Owner?.Type;
                        if (type != null && !string.IsNullOrEmpty(member.Name))
                            audiTrailSettings.RemoveProperties(type, member.Name);
                    }
                }
            }
        }
    }
}
