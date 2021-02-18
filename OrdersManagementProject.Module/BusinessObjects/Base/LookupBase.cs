using System.Linq;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace Domain.Base
{
    [System.ComponentModel.DefaultProperty(nameof(Name))]
    [NonPersistent]
    public abstract partial class LookupBase : TrackedBaseObject
    {
        public LookupBase(Session session) : base(session) { }

        #region Name

        string _name;

        [RuleRequiredField("LookupBase_Name_Required", DefaultContexts.Save, CustomMessageTemplate = "Name is required", TargetCriteria = "IsNameRequired = true", SkipNullOrEmptyValues = false)]
        [VisibleInListView(true)]
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue<string>(nameof(Name), ref _name, value); }
        }

        #endregion

        #region LookupCode

        string _lookupCode;

        [RuleRequiredField("LookupBase_LookupCode_Required", DefaultContexts.Save, CustomMessageTemplate = "Lookup Code is required", TargetCriteria = "IsLookupCodeRequired = true", SkipNullOrEmptyValues = false)]
        [Size(50)]
        [VisibleInListView(true)]
        public string LookupCode
        {
            get { return _lookupCode; }
            set { SetPropertyValue<string>(nameof(LookupCode), ref _lookupCode, value); }
        }

        #endregion

        #region IsNameRequired

        [MemberDesignTimeVisibility(false)]
        public bool IsNameRequired
        {
            get { return GetIsNameRequired(); }

        }

        public virtual bool GetIsNameRequired()
        {
            return false;
        }

        #endregion

        #region IsLookupCodeRequired

        [MemberDesignTimeVisibility(false)]
        public bool IsLookupCodeRequired
        {
            get { return GetIsLookupCodeRequired(); }

        }

        public virtual bool GetIsLookupCodeRequired()
        {
            return false;
        }

        #endregion

        // Functionality

        public static T FindByName<T>(Session session, string name) where T : LookupBase
        {
            Guard.ArgumentNotNull(session, nameof(session));

            if (string.IsNullOrEmpty(name))
                return null;

            return session.Query<T>().Where(s => s.Name == name).FirstOrDefault();
        }

        public static T FindByLookupCode<T>(Session session, string lookupCode) where T : LookupBase
        {
            Guard.ArgumentNotNull(session, nameof(session));

            if (string.IsNullOrEmpty(lookupCode))
                return null;

            return session.Query<T>().Where(s => s.LookupCode == lookupCode).FirstOrDefault();
        }
    }
}
