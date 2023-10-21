using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace softgen
{
    public class Messages
    {
        public static string gstrMsg="";

            public static void ErrorMsg(string message)
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            public static void InfoMsg(string message)
            {
                MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            public static void WarningMsg(string message)
            {
                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            public static DialogResult ConfirmationMsg(string message)
            {
                return MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            public static void FKNotExistMsg(string FieldName,string FieldValue)
        {
            MessageBox.Show(FieldName.Trim()+" '"+FieldValue.Trim()+"' "+"Does not Exists in the Master", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        public void AuthorisedMsg()
        {
            General general = new General();
            //general.ClearStatusBarHelprMsg;
            gstrMsg += "\nUse inquire mode to see this information.";
            MessageBox.Show(gstrMsg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void VBError(Exception Err_no, string FormName, string ProcName, string strQuery = "")
        {
            HelpMsg(null);

            string gstrMsg = "Error Number\t: " + Err_no + "\n" +
                             "Form/Module\t: " + FormName + "\n" +
                             "Sub/Function\t: " + ProcName + "\n";

            if (!string.IsNullOrWhiteSpace(strQuery))
            {
                gstrMsg += "Query\t\t: " + strQuery + "\n";
            }

            MessageBox.Show(gstrMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void HelpMsg(object value)
        {
            // Implement HelpMsg logic if needed
            //MainForm.pnlHelp.Text = strHelp;
        }

        public static void IncorrectLoginMsg()
        {
            MessageBox.Show("Login Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static void IncorrectPasswordMsg() { }

        public static void HelpMsg(string msg)
        {
           MainForm.Instance.pnlHelp.Text = msg;
            MainForm.Instance.pnlHelp.ForeColor= System.Drawing.Color.FromArgb(0,100,0,0);
        }

        public static void PostedMsg()
        {
            MainForm.Instance.pnlHelp.Text = "Information Authorized!";
            MainForm.Instance.pnlHelp.ForeColor= System.Drawing.Color.FromArgb(0,100,0,0);

            InfoMsg("Information Authorized!");

        }

        public static void SavingMsg()
        {
            MainForm.Instance.pnlHelp.Text = "Information saving. Please wait...";
        }
        
        public static void SavedMsg()
        {
            MainForm.Instance.pnlHelp.Text = "Information Saved.!";
            InfoMsg("Information Saved Successfully!");
        }

        public static void UnsavedMsg(string id)
        {
            InfoMsg("This Was Unsaved pls Save or Clear: '"+id+"'");
        }



        //////////////////
    }
}
