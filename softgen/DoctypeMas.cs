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
