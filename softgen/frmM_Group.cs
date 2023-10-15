using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace softgen
{
    public partial class frmM_Group : Form, Interface_for_Common_methods.ISearchableForm
    {
        private DbConnector dbConnector;
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn;
        public bool mblnSearch, mblnDataEntered;

        public frmM_Group()
        {
            InitializeComponent();
            dbConnector = new DbConnector();

            this.Activated += MyForm_Activated;
        }


        private void MyForm_Activated(object sender, EventArgs e)
        {
            DeTools.ClearStatusBarHelp();
            DeTools.ActiveFileMenu(this);
            DeTools.CreatedBy(mstrEntBy, mstrEntOn);
            DeTools.PostedBy(mstrAuthBy, mstrAuthOn);



        }

        private void Group_Load(object sender, EventArgs e)
        {
            DeTools.DisplayForm(this, 300, 420);
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(txtGrpId, "Enter Group Id.");
            toolTip.SetToolTip(txtGrpDesc, "Enter Group Description.");

        }

        //private void Group_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    MainForm parentform = (MainForm)this.MdiParent;

        //    parentform.dashpanel.Visible = true;
        //    parentform.mainpanel.Visible = false;
        //    parentform.formpanel.Visible = false;
        //}

        private void frmM_Group_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (DeTools.GetMode(this))
            {
                case DeTools.ADDMODE:
                case DeTools.MODIFYMODE:
                    mblnDataEntered = true;
                    break;
            }
        }

        public bool GetDEStatus()
        {
            return mblnDataEntered == true ? true : false;
        }


        public void SetSearchVar(bool StartVal)
        {
            // Implementation of SetSearchVar
            // You can define the behavior of SetSearchVar here

            mblnSearch = StartVal;
        }

        private void frmM_Group_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeTools.DestroyToolbar(this);
            MainForm.Instance.Mgroupmenu.Enabled = true;
        }
    }
}
