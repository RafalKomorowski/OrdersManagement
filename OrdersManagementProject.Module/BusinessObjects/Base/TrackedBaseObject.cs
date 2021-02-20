using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Domain.Security;

namespace Domain.Base
{
    [NonPersistent]
    public class TrackedBaseObject : CommonBaseObject
    {
        public TrackedBaseObject(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.CreatedOn = DateTime.Now;
            this.CreatedBy = Employee.FindCurrentEmployee(this.Session);
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            this.ModifiedBy = Employee.FindCurrentEmployee(this.Session);
        }

        private DateTime _createdOn;
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:g}"), ModelDefault("EditMask", "g")]
        [NonCloneable]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { SetPropertyValue<DateTime>(nameof(CreatedOn), ref _createdOn, value); }
        }

        private Employee _createdBy;
        [ModelDefault("AllowEdit", "false")]
        [NonCloneable]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public Employee CreatedBy
        {
            get { return _createdBy; }
            set { SetPropertyValue<Employee>(nameof(CreatedBy), ref _createdBy, value); }
        }

        private DateTime _modifiedOn;
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:g}"), ModelDefault("EditMask", "g")]
        [NonCloneable]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime ModifiedOn
        {
            get { return _modifiedOn; }
            set { SetPropertyValue<DateTime>(nameof(ModifiedOn), ref _modifiedOn, value); }
        }

        private Employee _modifiedBy;
        [ModelDefault("AllowEdit", "false")]
        [NonCloneable]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public Employee ModifiedBy
        {
            get { return _modifiedBy; }
            set { SetPropertyValue<Employee>(nameof(ModifiedBy), ref _modifiedBy, value); }
        }

    }
}
