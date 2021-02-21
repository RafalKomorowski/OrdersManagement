namespace Services
{
    #region IShowMessageStrategy

    public partial interface IShowMessageStrategy
    {
        /// <summary>
        /// Shows a dialog box. Returns DialogBox result.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        CustomDialogResult ShowDialog(string message, string title, CustomMessageBoxButtons buttons, CutomMessageBoxIcon icon);
    }

    #endregion

    #region ShowMessageService

    public static partial class ShowMessageService
    {
        #region Strategy

        private static IShowMessageStrategy _strategy = null;

        /// <summary>
        /// Returns the DefaultStrategy when no strategy has been set.
        /// </summary>
        public static IShowMessageStrategy Strategy
        {
            [System.Diagnostics.DebuggerStepThrough()]
            get
            {
                if (_strategy == null)
                    _strategy = new DefaultShowMessageStrategy();
                return _strategy;
            }
            set { _strategy = value; }
        }

        #endregion
    }

    #endregion

    #region ShowMessageStrategyBase

    /// <summary>
    /// A strategy that allows us to show dialog box from the base project without references to the WinForms DLLs.
    /// </summary>
    public abstract partial class ShowMessageStrategyBase : IShowMessageStrategy
    {
        #region ShowDialog

        /// <summary>
        /// Do not call this base method in the descendant method. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public virtual CustomDialogResult ShowDialog(string message, string title, CustomMessageBoxButtons buttons, CutomMessageBoxIcon icon)
        {
            // Return default value - logic need to be implement in the Win project.
            return CustomDialogResult.None;
        }

        #endregion

        #region IShowMessageStrategy Members

        CustomDialogResult IShowMessageStrategy.ShowDialog(string message, string title, CustomMessageBoxButtons buttons, CutomMessageBoxIcon icon)
        {
            return ShowDialog(message, title, buttons, icon);
        }

        #endregion
    }

    #endregion

    #region DefaultShowMessageStrategy

    public class DefaultShowMessageStrategy : ShowMessageStrategyBase
    {
    }

    #endregion

    #region CustomDialogResult

    public enum CustomDialogResult
    {
        None = 0,
        OK = 1,
        Cancel = 2,
        Abort = 3,
        Retry = 4,
        Ignore = 5,
        Yes = 6,
        No = 7
    }

    #endregion

    #region CustomMessageBoxButtons

    public enum CustomMessageBoxButtons
    {
        OK = 0,
        OKCancel = 1,
        AbortRetryIgnore = 2,
        YesNoCancel = 3,
        YesNo = 4,
        RetryCancel = 5
    }

    #endregion

    #region CutomMessageBoxIcon

    public enum CutomMessageBoxIcon
    {
        None = 0,
        Hand = 1,
        Stop = 2,
        Error = 3,
        Question = 4,
        Exclamation = 5,
        Warning = 6,
        Asterisk = 7,
        Information = 8
    }

    #endregion
}
