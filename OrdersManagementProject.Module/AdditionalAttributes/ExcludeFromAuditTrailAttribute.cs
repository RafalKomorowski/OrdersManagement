using System;

namespace OrdersManagementProject.Module.AdditionalAttributes
{
    /// <summary>
    /// Attribute to exclude the property from the Audit Trail
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class ExcludeFromAuditTrailAttribute : Attribute { }
}
