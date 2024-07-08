namespace softgen
{
    public partial class frmM_ItemType : Form
    {
        public frmM_ItemType()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmM_ItemType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.Text.Contains("<Add>"))
            {


                if (!DeTools.IsFieldUnique("m_item_type", "item_type_id", txtITypeId.Text.ToString().Trim()))
                {
                    MessageBox.Show("Id :" + txtITypeId.Text + " already Exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtITypeId.Text = null;
                    txtITypeId.Refresh();
                    txtITypeId.Focus();
                    // You can also clear the control or perform other actions
                }
            }
        }
    }
}
