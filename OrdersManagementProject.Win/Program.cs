using System;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.AuditTrail;
using DevExpress.Persistent.Base;
using DevExpress.XtraEditors;
using OrdersManagementProject.Module.Services.AuditTrail;

namespace OrdersManagementProject.Win
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.ExpressApp.FrameworkSettings.DefaultSettingsCompatibilityMode = DevExpress.ExpressApp.FrameworkSettingsCompatibilityMode.Latest;
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            WindowsFormsSettings.LoadApplicationSettings();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.Utils.ToolTipController.DefaultController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            if (Tracing.GetFileLocationFromSettings() == DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder)
            {
                Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            }
            Tracing.Initialize();
            OrdersManagementProjectWindowsFormsApplication winApplication = new OrdersManagementProjectWindowsFormsApplication();
            winApplication.GetSecurityStrategy().RegisterXPOAdapterProviders();
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
            {
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                string automaticDebugUser = System.Configuration.ConfigurationManager.AppSettings["AutomaticDebugUser"];
                if (!string.IsNullOrEmpty(automaticDebugUser))
                {
                    SecurityStrategyComplex securityStrategyComplex = winApplication.Security as SecurityStrategyComplex;
                    if (securityStrategyComplex != null)
                        securityStrategyComplex.Authentication = new Domain.Security.AuthenticationStandardForDebug();
                }
            }
#endif

            // Audit Trail security improvement
            AuditTrailService.Instance.CustomizeAuditTrailSettings += new CustomizeAuditSettingsEventHandler(Instance_CustomizeAuditTrailSettings);

            try
            {
                winApplication.Setup();
                winApplication.Start();
            }
            catch (Exception e)
            {
                winApplication.StopSplash();
                winApplication.HandleException(e);
            }
        }

        /// <summary>
        /// Removes properties with the [ExcludeFromAuditTrail] attribute from the audit trail.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Instance_CustomizeAuditTrailSettings(object sender, CustomizeAuditTrailSettingsEventArgs e)
        {
            ExcludeFromAuditTrailService.Exclude(e.AuditTrailSettings);
        }
    }
}
