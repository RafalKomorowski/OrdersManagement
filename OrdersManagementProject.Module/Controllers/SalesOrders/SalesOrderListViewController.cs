using System.Linq;
using DevExpress.ExpressApp;
using Domain.SalesOrders;

namespace OrdersManagementProject.Module.Controllers.SalesOrders
{
    /// <summary>
    /// A controller for the SalesOrder list views
    /// </summary>
    public partial class SalesOrderListViewController : ViewController
    {
        #region Ctor(s)

        public SalesOrderListViewController()
        {
            InitializeComponent();
            this.TargetObjectType = typeof(SalesOrder);
            this.TargetViewType = ViewType.ListView;
        }

        #endregion

        #region MarkSalesOrderAsInvalidAction_Execute

        private void MarkSalesOrderAsInvalidAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            // Set Invalid flag on selected records.
            foreach (SalesOrder salesOrder in View.SelectedObjects.OfType<SalesOrder>())
            {
                salesOrder.Invalid = true;
            }

            // Save Changes
            this.ObjectSpace.CommitChanges();
        }

        #endregion
    }
}
