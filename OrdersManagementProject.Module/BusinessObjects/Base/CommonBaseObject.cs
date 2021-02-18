using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace Domain.Base
{
    [NonPersistent]
    public class CommonBaseObject : BaseObject
    {
        public CommonBaseObject(Session session) : base(session)
        {
        }

        [Delayed(true)]
        [MemberDesignTimeVisibility(false)]
        public bool IsNewObject
        {
            get { return this.Session.IsNewObject(this); }
        }
    }
}
