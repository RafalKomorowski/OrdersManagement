using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Services
{
    public class WinShowMessageStrategy : DefaultShowMessageStrategy
    {
        #region ShowDialog

        public override CustomDialogResult ShowDialog(string message, string title, CustomMessageBoxButtons buttons, CutomMessageBoxIcon icon)
        {
            // Try to parse CustomMessageBoxButtons into the MessageBoxButtons
            MessageBoxButtons? messageBoxButtons;
            if (Enum.TryParse<MessageBoxButtons>(buttons.ToString(), out MessageBoxButtons tempMessageBoxButtons))
                messageBoxButtons = tempMessageBoxButtons;
            else
                throw new Exception($"Could not parse '{buttons}' to the MessageBoxButtons enum value!");

            // Try to parse CutomMessageBoxIcon into the MessageBoxIcon
            MessageBoxIcon? messageBoxIcon;
            if (Enum.TryParse<MessageBoxIcon>(icon.ToString(), out MessageBoxIcon tempMessageBoxIcon))
                messageBoxIcon = tempMessageBoxIcon;
            else
                throw new Exception($"Could not parse '{icon}' to the MessageBoxButtons enum value!");

            DialogResult result = XtraMessageBox.Show(message, title, messageBoxButtons.Value, messageBoxIcon.Value);


            // Try to parse DialogResult into the CustomDialogResult
            CustomDialogResult? customDialogResult;
            if (Enum.TryParse<CustomDialogResult>(result.ToString(), out CustomDialogResult tempCustomDialogResult))
                customDialogResult = tempCustomDialogResult;
            else
                throw new Exception($"Could not parse '{result}' to the CustomDialogResult enum value!");

            return customDialogResult.Value;
        }

        #endregion
    }
}
