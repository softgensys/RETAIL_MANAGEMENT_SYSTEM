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
    public partial class DoctypeMas : Form
    {
        public DoctypeMas()
        {
            InitializeComponent();
        }

        private void DoctypeMas_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm parentform = (MainForm)this.MdiParent;

            parentform.dashpanel.Visible = true;
            parentform.mainpanel.Visible = false;
            parentform.formpanel.Visible = false;
        }
    }
}
