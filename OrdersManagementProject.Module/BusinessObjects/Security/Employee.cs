using System;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;

namespace Domain.Security
{
    [DefaultProperty(nameof(FullName))]
    [ImageName("BO_User")]
    [MapInheritance(MapInheritanceType.ParentTable)]
    [NavigationItem("Security")]
    public class Employee : PermissionPolicyUser
    {
        public Employee(DevExpress.Xpo.Session session) : base(session)
        {
        }

        #region FirstName

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { SetPropertyValue<string>(nameof(FirstName), ref _firstName, value); }
        }

        #endregion

        #region LastName

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { SetPropertyValue<string>(nameof(LastName), ref _lastName, value); }
        }

        #endregion

        #region FullName

        [PersistentAlias("Trim(IsNull([FirstName],'') + ' ' + IsNull([LastName],''))")]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string FullName
        {
            get { return Convert.ToString(EvaluateAlias(nameof(FullName))); }
        }

        #endregion

        #region Email

        private string _email;

        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public string Email
        {
            get { return _email; }
            set { SetPropertyValue<string>(nameof(Email), ref _email, value); }
        }

        #endregion

        #region FindCurrentEmployee

        public static Employee FindCurrentEmployee(Session session, bool throwException = true)
        {
            Guard.ArgumentNotNull(session, nameof(session));

            Employee currentEmployee = null;
            object currentUserId = SecuritySystem.CurrentUserId;
            if (currentUserId != null)
                currentEmployee = session.GetObjectByKey<Employee>(currentUserId);
            if (currentEmployee == null && throwException)
                throw new UserFriendlyException("Cannot find current employee.");
            return currentEmployee;
        }

        #endregion
    }
}
