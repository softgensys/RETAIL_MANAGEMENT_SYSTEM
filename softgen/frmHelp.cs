using System.Data.Common;
using System.Data.Odbc;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace softgen
{
    public partial class frmHelp : Form
    {
         public DbConnector dbConnector;
        public static int selectedFieldIndex;
        public static string selectedDataType="";

        private Dictionary<string, string> columnDataTypes;

        public static string selectedCaption;

        public frmHelp()
        {
            InitializeComponent();
            LoadQueryAndColumnDataTypes();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Help.PrepareGridHelp();

        }

        private void cboFields_SelectionChangeCommitted(object sender, EventArgs e)
        {



        }

        private void cboFields_MouseClick(object sender, MouseEventArgs e)
        {


        }

        //private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboFields.Items.Count > 0 && cboDataType.Items.Count > 0)
        //    {
        //        cboFieldsId.SelectedIndex = cboFields.SelectedIndex;

        //        // Update Help.send_combo_box_value based on cboDataType selection
        //        if (cboDataType.SelectedIndex >= 0 && cboDataType.SelectedIndex < Help.data_type_index_copy.Items.Count)
        //        {
        //            string copyComboBoxItemValue = Help.data_type_index_copy.Items[cboDataType.SelectedIndex].ToString().Trim();
        //            Help.send_combo_box_value = copyComboBoxItemValue.ToString();
        //        }

        //        // Set cboDataType.SelectedIndex based on cboFields.SelectedIndex
        //        cboDataType.SelectedIndex = cboFields.SelectedIndex;

        //        // Clear and populate cboRel based on selected data type
        //        cboRel.Items.Clear();
        //        string selectedDataType = cboDataType.SelectedItem.ToString().Trim();

        //        if (selectedDataType == "String")
        //        {
        //            cboRel.Items.Add("=");
        //            cboRel.Items.Add("LIKE");
        //        }
        //        else
        //        {
        //            cboRel.Items.Add("=");
        //            cboRel.Items.Add("<");
        //            cboRel.Items.Add("<=");
        //            cboRel.Items.Add(">");
        //            cboRel.Items.Add(">=");
        //        }

        //        // Set the default selected item to "LIKE" if available
        //        int likeIndex = cboRel.Items.IndexOf("LIKE");
        //        cboRel.SelectedIndex = likeIndex != -1 ? likeIndex : 0;

        //        // Clear txtValue and set focus if cboFields.SelectedItem is not null or empty
        //        txtValue.Text = string.Empty;
        //        if (cboFields.SelectedItem != null && cboFields.SelectedItem.ToString() != "")
        //        {
        //            txtValue.Focus();
        //        }
        //    }
        //}


        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFields.SelectedItem != null)
            {
                selectedCaption = cboFields.SelectedItem.ToString().Trim();
                string selectedColumn = GetColumnForCaption(selectedCaption);

                if (!string.IsNullOrEmpty(selectedColumn))
                {
                    string dataType = GetDataTypeForField(selectedColumn);
                    if (!string.IsNullOrEmpty(dataType))
                    {
                        // Update Help.send_combo_box_value
                        Help.send_combo_box_value = dataType;

                        // Clear and populate cboRel based on data type
                        cboRel.Items.Clear();

                        if (dataType == "char" || dataType == "varchar")
                        {
                            cboRel.Items.Add("=");
                            cboRel.Items.Add("LIKE");
                        }
                        else
                        {
                            cboRel.Items.Add("=");
                            cboRel.Items.Add("<");
                            cboRel.Items.Add("<=");
                            cboRel.Items.Add(">");
                            cboRel.Items.Add(">=");
                        }

                        // Set the default selected item to "LIKE" if available
                        int likeIndex = cboRel.Items.IndexOf("LIKE");
                        cboRel.SelectedIndex = likeIndex != -1 ? likeIndex : 0;

                        // Clear txtValue and set focus if cboFields.SelectedItem is not null or empty
                        txtValue.Text = string.Empty;
                        if (!string.IsNullOrEmpty(cboFields.SelectedItem.ToString()))
                        {
                            txtValue.Focus();
                        }

                        // Set the selected index of cboDataType to match cboFields
                        cboDataType.SelectedIndex = cboFields.SelectedIndex;

                        // Get the selected item from cboDataType
                        selectedDataType = cboDataType.SelectedItem.ToString();
                        Console.WriteLine("Selected Data Type: " + selectedDataType); // Replace with your actual logic
                    }
                }
                selectedFieldIndex = cboFields.SelectedIndex;
            }
        }

        private string GetColumnForCaption(string caption)
        {
            (string query, string[] captions) = GetQueryAndCaptionsFromHelpTable(Help.i_Help_id);
            if (captions == null || captions.Length == 0) return null;

            List<string> columnNames = ExtractColumnNamesFromQuery(query);
            if (columnNames == null || columnNames.Count == 0) return null;

            for (int i = 0; i < captions.Length; i++)
            {
                if (captions[i].Trim() == caption && i < columnNames.Count)
                {
                    return columnNames[i];
                }
            }
            return null;
        }


        private List<string> ExtractColumnNamesFromQuery(string query)
        {
            List<string> columnNames = new List<string>();
            string columnsPart = query.Substring(query.IndexOf("SELECT", StringComparison.OrdinalIgnoreCase) + 6, query.IndexOf("FROM", StringComparison.OrdinalIgnoreCase) - 6);
            string[] columns = columnsPart.Split(',');

            foreach (string column in columns)
            {
                columnNames.Add(column.Trim());
            }

            return columnNames;
        }


        private (string, string[]) GetQueryAndCaptionsFromHelpTable(int helpId)
        {
            string query = string.Empty;
            string[] captions = new string[4];

            dbConnector = new DbConnector();
            // dbConnector.connectionString= new OdbcConnection();
            using (dbConnector.connection = new OdbcConnection(dbConnector.connectionString))
            {
                dbConnector.OpenConnection();
                string sql = "SELECT Query, Caption1, Caption2, Caption3, Caption4 FROM help WHERE help_id = ?";
                OdbcParameter[] parameters = new OdbcParameter[1];

                parameters[0] = new OdbcParameter("help_id", Help.i_Help_id);

                using (OdbcDataReader reader = dbConnector.ExecuteReader(sql, parameters))
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            query = reader["Query"].ToString();
                            captions[0] = reader["Caption1"].ToString();
                            captions[1] = reader["Caption2"].ToString();
                            captions[2] = reader["Caption3"].ToString();
                            captions[3] = reader["Caption4"].ToString();
                        }
                    }
                    reader.Close();
                }

            }
            return (query, captions);
            
            
        }

        private string GetDataTypeForField( string fieldName)
        {
            dbConnector = new DbConnector();
            // dbConnector.connectionString= new OdbcConnection();
            using (dbConnector.connection = new OdbcConnection(dbConnector.connectionString))
            {
                dbConnector.OpenConnection();
                string tableName = ExtractTableNameFromHelpQuery();
                if (string.IsNullOrEmpty(tableName)) return null;

                string sql = "SELECT DATA_TYPE FROM information_schema.COLUMNS WHERE TABLE_NAME = '"+tableName+"' AND COLUMN_NAME = '"+ fieldName + "'";

                
                using (OdbcCommand Cmddatatype = new OdbcCommand(sql, dbConnector.connection))
                {
                   
                    object result = Cmddatatype.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }



        private void LoadQueryAndColumnDataTypes()
        {
            columnDataTypes = new Dictionary<string, string>();

            string query = GetQueryFromHelpTable(Help.i_Help_id);
            if (string.IsNullOrEmpty(query)) return;

            // Extract the table name from the query
            string tableName = ExtractTableNameFromHelpQuery();
            if (string.IsNullOrEmpty(tableName)) return;

            // Extract column names from the query
            List<string> columnNames = ExtractColumnNamesFromQuery(query);

            // Fetch data types for each column and populate columnDataTypes dictionary
            foreach (string columnName in columnNames)
            {
                string dataType = GetDataTypeForField(columnName);
                if (!string.IsNullOrEmpty(dataType))
                {
                    columnDataTypes[columnName] = dataType;
                }
            }

            PopulateCboDataType();
        }

        private void PopulateCboDataType()
        {
            cboDataType.Items.Clear();
            HashSet<string> uniqueDataTypes = new HashSet<string>();

            foreach (var dataType in columnDataTypes.Values)
            {
                if (!uniqueDataTypes.Contains(dataType))
                {
                    uniqueDataTypes.Add(dataType);
                    cboDataType.Items.Add(dataType);
                }
            }
        }

        private string ExtractTableNameFromHelpQuery()
        {
            string query = GetQueryFromHelpTable(Help.i_Help_id);
            if (string.IsNullOrEmpty(query)) return null;

            int fromIndex = query.IndexOf("FROM", StringComparison.OrdinalIgnoreCase);
            if (fromIndex != -1)
            {
                string fromPart = query.Substring(fromIndex + 5);
                string[] parts = fromPart.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return parts.Length > 0 ? parts[0] : string.Empty;
            }
            return null;
        }

        private string GetQueryFromHelpTable(int helpId)
        {
            string query = string.Empty;
            dbConnector = new DbConnector();
            // dbConnector.connectionString= new OdbcConnection();
            using (dbConnector.connection = new OdbcConnection(dbConnector.connectionString))
            {
                dbConnector.OpenConnection();
                string sql = "SELECT Query FROM help WHERE help_id = "+helpId+"";

                using (OdbcCommand Cmdquery = new OdbcCommand(sql, dbConnector.connection))
                {                    
                    object result = Cmdquery.ExecuteScalar();
                    if (result != null)
                    {
                        query = result.ToString();
                    }
                }
            }

            return query;
        }
    



    //private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    cboRel.Items.Clear();

    //    // Add items based on your condition
    //    if (cboFields.SelectedIndex != -1)
    //    {
    //        if (cboDataType.Text.Trim() == cboDataType.Text.Trim())  // Adjust condition based on your logic
    //        {
    //            cboRel.Items.Add("=");
    //            cboRel.Items.Add("LIKE");

    //            // Set selected index, ensuring it does not exceed the number of items in cboRel
    //            if (cboRel.Items.Count > 1)  // Ensure at least two items are present
    //            {
    //                cboRel.SelectedIndex = 1; // Select "LIKE"
    //            }
    //            else if (cboRel.Items.Count > 0)
    //            {
    //                cboRel.SelectedIndex = 0; // Fallback to the first item if only one item is present
    //            }
    //            // If cboRel.Items.Count is 0, leave SelectedIndex unset as there are no items to select

    //            if (cboRel.SelectedIndex != -1)
    //            {
    //                string copyComboBoxItemValue = Help.data_type_index_copy.Items[cboDataType.SelectedIndex].ToString().Trim();
    //                Help.send_combo_box_value = copyComboBoxItemValue;
    //            }
    //        }
    //        else
    //        {
    //            cboRel.Items.Add("=");
    //            cboRel.Items.Add("<");
    //            cboRel.Items.Add("<=");
    //            cboRel.Items.Add(">");
    //            cboRel.Items.Add(">=");

    //            // Set selected index, ensuring it does not exceed the number of items in cboRel
    //            if (cboRel.Items.Count > 0)
    //            {
    //                cboRel.SelectedIndex = 0; // Select "=" as default
    //            }
    //            // If cboRel.Items.Count is 0, leave SelectedIndex unset as there are no items to select

    //            if (cboRel.SelectedIndex != -1)
    //            {
    //                string copyComboBoxItemValue = Help.data_type_index_copy.Items[cboDataType.SelectedIndex].ToString().Trim();
    //                Help.send_combo_box_value = copyComboBoxItemValue;
    //            }
    //        }
    //    }
    //}


    private void grdHelp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a valid cell was double-clicked
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Transfer data to another form
                if (DeTools.gobjActiveForm.Name == "frmT_Invoice")
                {
                    Form form1 = DeTools.gobjActiveForm;
                    Help.TransferDataInv(form1);
                }

                else if (DeTools.gobjActiveForm.Name == "frmT_Sale_Return")
                {
                    Form form1 = DeTools.gobjActiveForm;
                    Help.TransferDataSr(form1);
                }

                else
                {

                    Help.TransferData();
                }

                this.Hide();
                this.Refresh();

            }
        }

        private void grdHelp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Prevent the form from closing
            e.Cancel = true;

            // Hide the form instead
            this.Hide();
            this.Refresh();
        }

        private void frmHelp_Load(object sender, EventArgs e)
        {
            // Assuming cboDataType should be populated with a list of data types
            //cboDataType.Items.Add("Integer");
            //cboDataType.Items.Add("Decimal");
            //cboDataType.Items.Add("Date");
            //cboDataType.Items.Add("String");
        }
    }
}