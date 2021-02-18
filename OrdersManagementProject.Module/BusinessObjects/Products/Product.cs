using System.Linq;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Domain.Base;
using Domain.SalesOrders;

namespace Domain.Products
{
    public class Product : TrackedBaseObject
    {
        public Product(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            // Auto-generate first available product code.
            XPQuery<Product> pQ = new XPQuery<Product>(this.Session);
            int maxProductCode = pQ.Max(p => p.Code);
            this.Code = maxProductCode + 1;
        }

        #region Code

        private int _code;

        [Indexed]
        [RuleUniqueValue]
        [RuleRequiredField]
        public int Code
        {
            get { return _code; }
            set { SetPropertyValue<int>(nameof(Code), ref _code, value); }
        }

        #endregion

        #region Price

        private decimal _price;

        [RuleRequiredField]
        [RuleRange(0, 100000000000, "Price need to be in the 0-100000000000 range")]
        public decimal Price
        {
            get { return _price; }
            set { SetPropertyValue<decimal>(nameof(Price), ref _price, value); }
        }

        #endregion

        #region Name

        private string _name;

        [RuleRequiredField]
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue<string>(nameof(Name), ref _name, value); }
        }

        #endregion

        #region Description

        private string _description;

        [Size(SizeAttribute.Unlimited)]
        public string Description
        {
            get { return _description; }
            set { SetPropertyValue<string>(nameof(Description), ref _description, value); }
        }

        #endregion

        #region SalesOrders

        [Association]
        public XPCollection<SalesOrder> SalesOrders
        {
            get { return GetCollection<SalesOrder>(nameof(SalesOrders)); }
        }

        #endregion
    }
}
