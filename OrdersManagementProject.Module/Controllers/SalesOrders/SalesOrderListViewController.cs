using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using Domain.Options;
using Domain.SalesOrders;
using Domain.Security;
using Helpers;
using MimeKit;

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
            int selectedRecordsCount = View.SelectedObjects.Count;
            if (selectedRecordsCount > 0)
            {
                // Get current employee
                Employee employee = SecuritySystem.CurrentUser as Employee;

                // Create a new builder to create message
                StringBuilder builder = new StringBuilder();
                if (selectedRecordsCount == 1)
                    builder.AppendLine($"Following Sales Order record has been marked as Invalid by the '{employee}' employee:");
                else
                    builder.AppendLine($"Following Sales Order records have been marked as Invalid by the '{employee}' employee:");

                // Set Invalid flag on selected records.
                foreach (SalesOrder salesOrder in View.SelectedObjects.OfType<SalesOrder>())
                {
                    salesOrder.Invalid = true;
                    builder.AppendLine($"- {salesOrder.OrderCode}");
                }


                // Send email message to the employee set in the Program Options as SalesOrder notification receiver.
                ProgramOptions programOptions = ProgramOptions.GetInstance(this.ObjectSpace.Session());
                if (programOptions != null)
                {
                    try
                    {
                        if (programOptions.SalesOrderNotificationAdministrator == null)
                            throw new Exception("Sales Order Notification Administrator is not set in the Program Options.\r\nPlease contact the administrator.");

                        if (string.IsNullOrEmpty(programOptions.SenderEmailAddress) || string.IsNullOrEmpty(programOptions.SenderEmailPassword) || programOptions.SMTPPort == 0 || string.IsNullOrEmpty(programOptions.SmtpClientName))
                            throw new Exception("Email configuration in the Program Options is missing. Please contact the administrator.");

                        MimeMessage message = new MimeMessage();
                        message.From.Add(new MailboxAddress($"{Application.ApplicationName} - Notification Service", programOptions.SenderEmailAddress));
                        message.To.Add(new MailboxAddress(programOptions.SalesOrderNotificationAdministrator.FullName, programOptions.SalesOrderNotificationAdministrator.Email));
                        message.Subject = $"{Application.ApplicationName} - Invalid Sales Orders";
                        message.Body = new TextPart("plain")
                        {
                            Text = builder.ToString()
                        };

                        // Send the email
                        if (EmailHelper.SendEmail(this.ObjectSpace.Session(), message, out string errorMessage))
                        {
                            //TODO: Show confirmation message
                        }
                        else
                            //Show error information. Logging has been done in the SendEmail method.
                            throw new Exception($"An error occurred while sending the Email message to the '{programOptions.SalesOrderNotificationAdministrator.Email}':\n{errorMessage}");
                    }
                    catch (Exception ex)
                    {
                        this.ObjectSpace.Rollback(false);
                        throw;
                    }
                }

                // Save Changes
                this.ObjectSpace.CommitChanges();
            }
        }

        #endregion
    }
}
