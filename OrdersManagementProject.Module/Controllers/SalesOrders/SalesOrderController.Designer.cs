
namespace OrdersManagementProject.Module.Controllers.SalesOrders
{
    partial class SalesOrderController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SalesOrderMarkAsResolvedAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // SalesOrderMarkAsResolvedAction
            // 
            this.SalesOrderMarkAsResolvedAction.Caption = "Mark As Resolved";
            this.SalesOrderMarkAsResolvedAction.Category = "RecordEdit";
            this.SalesOrderMarkAsResolvedAction.ConfirmationMessage = "Do you want to mark selected record(s) as resolved?";
            this.SalesOrderMarkAsResolvedAction.Id = "SalesOrderMarkAsResolvedAction";
            this.SalesOrderMarkAsResolvedAction.ImageName = "TrackingChanges_Accept";
            this.SalesOrderMarkAsResolvedAction.TargetObjectsCriteria = "IsCurrentUserInRole(\'Administrators\')";
            this.SalesOrderMarkAsResolvedAction.TargetObjectType = typeof(Domain.SalesOrders.SalesOrder);
            this.SalesOrderMarkAsResolvedAction.ToolTip = null;
            this.SalesOrderMarkAsResolvedAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.SalesOrderMarkAsResolvedAction_Execute);
            // 
            // SalesOrderController
            // 
            this.Actions.Add(this.SalesOrderMarkAsResolvedAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction SalesOrderMarkAsResolvedAction;
    }
}
