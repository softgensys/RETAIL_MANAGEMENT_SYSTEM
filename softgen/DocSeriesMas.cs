namespace softgen
{
    public partial class DocSeriesMas : Form
    {
        private DbConnector dbConnector;

        public DocSeriesMas()
        {
            InitializeComponent();

            dbConnector = new DbConnector();
            ComboBoxDataLoader.SetDbConnector(dbConnector);

            fromdt.Format = DateTimePickerFormat.Short;
            todt.Format = DateTimePickerFormat.Short;

            fromdt.Format = DateTimePickerFormat.Custom;
            fromdt.CustomFormat = "dd/MM/yyyy";

            todt.Format = DateTimePickerFormat.Custom;
            todt.CustomFormat = "dd/MM/yyyy";

        }

        private void doctypeidcomb_MouseDown(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender; // Cast the sender object to ComboBox 

            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            //string ClauseValue = grpcomb.SelectedValue.ToString();
            String tableName = "m_doc_type";
            whereClauseVariable.Add("");
            whereClauseValue.Add("");
            String selectColumn = "doc_type_id";
            ComboBoxDataLoader.LoadDataIntoComboBox(tableName, whereClauseVariable, whereClauseValue, 0, selectColumn, comboBox);
        }

        private void doctypeidcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string tableName = "m_doc_type";
            string columnName = "doc_type_name";
            string ClauseValue = doctypeidcomb.SelectedValue.ToString();
            whereClauseVariable.Add("doc_type_id");
            whereClauseValue.Add(ClauseValue);
            int op = 0;

            ComboBoxDataLoader.GetcomboValue_in_txt(tableName, columnName, whereClauseVariable, whereClauseValue, op, lbldoctype);

        }

        private void fromdt_Leave(object sender, EventArgs e)
        {
            //get the selected fromdt
            DateTime from_dt = fromdt.Value;

            //calculating to date for financial yr.
            DateTime to_dt = from_dt.AddYears(1).AddDays(-1);

            todt.Value = to_dt;
        }

        private void txtstartno_TextChanged(object sender, EventArgs e)
        {
            string strtno = txtstartno.Text;

            lblnextno.Text = strtno;
        }

        private void DocSeriesMas_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm parentform = (MainForm)this.MdiParent;

            parentform.dashpanel.Visible = true;
            parentform.mainpanel.Visible = false;
            parentform.formpanel.Visible = false;
        }
    }
}
