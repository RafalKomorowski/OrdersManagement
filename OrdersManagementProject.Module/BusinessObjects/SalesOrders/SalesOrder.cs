using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Domain.Base;

namespace Domain.SalesOrders
{
    [DefaultProperty(nameof(OrderCode))]
    [NavigationItem("SalesOrders")]
    public class SalesOrder : TrackedBaseObject
    {
        public SalesOrder(Session session) : base(session)
        {
        }

        #region OrderCode

        private string _orderCode;

        [Size(30)]
        public string OrderCode
        {
            get { return _orderCode; }
            set { SetPropertyValue<string>(nameof(OrderCode), ref _orderCode, value); }
        }

        #endregion
    }
}
