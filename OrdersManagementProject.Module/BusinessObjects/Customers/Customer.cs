using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Domain.Base;

namespace Domain.Customers
{
    [NavigationItem("Customers")]
    public class Customer : PersonBase
    {
        public Customer(Session session) : base(session)
        {
        }
    }
}
