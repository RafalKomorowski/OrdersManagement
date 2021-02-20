
namespace OrdersManagementProject.Module.Controllers.SalesOrders
{
    partial class SalesOrderListViewController
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
            this.MarkSalesOrderAsInvalidAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // MarkSalesOrderAsInvalidAction
            // 
            this.MarkSalesOrderAsInvalidAction.Caption = "Mark As Invalid";
            this.MarkSalesOrderAsInvalidAction.Category = "RecordEdit";
            this.MarkSalesOrderAsInvalidAction.ConfirmationMessage = "Are you sure you want to mark the selected order(s) as invalid and notify the adm" +
    "inistrator?";
            this.MarkSalesOrderAsInvalidAction.Id = "MarkSalesOrderAsInvalidAction";
            this.MarkSalesOrderAsInvalidAction.ImageName = "Actions_Bookmark";
            this.MarkSalesOrderAsInvalidAction.ToolTip = "This action will mark selected records as invalid and notify the administrator. I" +
    "nvalid orders cannot be modified until the administrator marks them as resolved." +
    "";
            this.MarkSalesOrderAsInvalidAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.MarkSalesOrderAsInvalidAction_Execute);
            // 
            // SalesOrderListViewController
            // 
            this.Actions.Add(this.MarkSalesOrderAsInvalidAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction MarkSalesOrderAsInvalidAction;
    }
}
