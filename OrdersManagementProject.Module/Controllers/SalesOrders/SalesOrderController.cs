using System.Linq;
using DevExpress.ExpressApp;
using Domain.SalesOrders;

namespace OrdersManagementProject.Module.Controllers.SalesOrders
{
    /// <summary>
    /// A controller for the SalesOrder list views and detail views
    /// </summary>
    public partial class SalesOrderController : ViewController
    {
        #region Ctor(s)

        public SalesOrderController()
        {
            InitializeComponent();
            this.TargetObjectType = typeof(SalesOrder);
            this.TargetViewType = ViewType.Any;

        }

        #endregion

        private void SalesOrderMarkAsResolvedAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            // Set Resolved flag on selected records.
            foreach (SalesOrder salesOrder in View.SelectedObjects.OfType<SalesOrder>())
            {
                salesOrder.Resolved = true;
            }

            // Save Changes only if the view is List View. On Detail View, we want to let user decide if changes should be saved or reverted. 
            if (this.View is ListView)
                this.ObjectSpace.CommitChanges();
        }
    }
}
