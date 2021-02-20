using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace Domain.Options
{
    [CreatableItem(false)]
    [DefaultClassOptions]
    [NavigationItem("Security")]
    [VisibleInDashboards(false), VisibleInReports(false)]
    public class ProgramOptions : BaseObject
    {
        public ProgramOptions(Session session) : base(session)
        {
        }

        public static ProgramOptions GetInstance(IObjectSpace space)
        {
            return GetInstance(((XPObjectSpace)space).Session);
        }

        public static ProgramOptions GetInstance(Session session)
        {
            ProgramOptions options = session.FindObject<ProgramOptions>(PersistentCriteriaEvaluationBehavior.InTransaction, null);
            if (options == null)
                options = new ProgramOptions(session);

            return options;
        }

        #region SenderEmailAddress

        private string _senderEmailAddress;

        public string SenderEmailAddress
        {
            get { return _senderEmailAddress; }
            set { SetPropertyValue<string>(nameof(SenderEmailAddress), ref _senderEmailAddress, value); }
        }

        #endregion

        #region SenderEmailPassword

        private string _senderEmailPassword;

        [System.ComponentModel.PasswordPropertyText(true)]
        public string SenderEmailPassword
        {
            get { return _senderEmailPassword; }
            set { SetPropertyValue<string>(nameof(SenderEmailPassword), ref _senderEmailPassword, value); }
        }

        #endregion

        #region SmtpClientName

        private string _smtpClientName;

        public string SmtpClientName
        {
            get { return _smtpClientName; }
            set { SetPropertyValue<string>(nameof(SmtpClientName), ref _smtpClientName, value); }
        }

        #endregion

        #region SMTPPort

        private int _smtpPort;

        [DevExpress.ExpressApp.DC.XafDisplayName("SMTP Port")]
        public int SMTPPort
        {
            get { return _smtpPort; }
            set { SetPropertyValue<int>(nameof(SMTPPort), ref _smtpPort, value); }
        }

        #endregion

        #region EnableSSL

        private bool _enableSSL;

        public bool EnableSSL
        {
            get { return _enableSSL; }
            set { SetPropertyValue<bool>(nameof(EnableSSL), ref _enableSSL, value); }
        }

        #endregion
    }
}
