using System;
using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Domain.Base;
using Domain.Customers;
using Domain.Products;
using Domain.Security;

namespace Domain.SalesOrders
{
    [DefaultProperty(nameof(OrderCode))]
    [NavigationItem("SalesOrders")]
    public class SalesOrder : TrackedBaseObject
    {
        public SalesOrder(Session session) : base(session)
        {
            this.Products.ListChanged += Products_ListChanged;
        }

        #region AfterConstruction

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            // Auto-generate order code.
            long timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            this.OrderCode = $"SO-{DateTime.Now:yyyyMMddHHmmssffff}";

            // Set AssignedTo after object creation to the current user
            this.AssignedTo = SecuritySystem.CurrentUser as Employee;
        }

        #endregion

        #region OrderCode

        private string _orderCode;

        [Indexed]
        [RuleRequiredField]
        [RuleUniqueValue]
        [Size(30)]
        public string OrderCode
        {
            get { return _orderCode; }
            set { SetPropertyValue<string>(nameof(OrderCode), ref _orderCode, value); }
        }

        #endregion

        #region Customer

        private Customer _customer;

        [RuleRequiredField]
        public Customer Customer
        {
            get { return _customer; }
            set { SetPropertyValue<Customer>(nameof(Customer), ref _customer, value); }
        }

        #endregion

        #region OrderPrice

        private decimal _orderPrice;

        [ModelDefault("AllowEdit", "false")]
        public decimal OrderPrice
        {
            get { return _orderPrice; }
            set { SetPropertyValue<decimal>(nameof(OrderPrice), ref _orderPrice, value); }
        }

        #endregion

        #region Status

        private OrderStatus _status;

        public OrderStatus Status
        {
            get { return _status; }
            set { SetPropertyValue<OrderStatus>(nameof(Status), ref _status, value); }
        }

        #endregion

        #region AssignedTo

        private Employee _employee;

        public Employee AssignedTo
        {
            get { return _employee; }
            set { SetPropertyValue<Employee>(nameof(AssignedTo), ref _employee, value); }
        }

        #endregion


        #region Products

        [Association]
        public XPCollection<Product> Products
        {
            get { return GetCollection<Product>(nameof(Products)); }
        }

        #endregion

        // Functionality

        #region Products_ListChanged

        /// <summary>
        /// Updates the order price when products list changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.OrderPrice = this.Products.Sum(p => p.Price);
        }

        #endregion
    }

    public enum OrderStatus
    {
        NewOrder = 0,
        Paid = 1,
        Packed = 2,
        ReadyToShip = 3,
        Invoiced = 4,
        Shipped = 5,
        OnHold = 6,
        Delivered = 7,
        Cancelled = 8
    }
}
