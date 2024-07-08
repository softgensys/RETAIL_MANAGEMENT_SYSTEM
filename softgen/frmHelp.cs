using System.Data.Common;
using System.Data.Odbc;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace softgen
{
    public partial class frmHelp : Form
    {
        public DbConnector dbConnector;
        public static int selectedFieldIndex;
        public static string selectedDataType = "";

        private Dictionary<string, string> columnDataTypes;

        public static string selectedCaption;
        public static int likeIndex;

        public static bool flag_Enteryn = false;

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


        //private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboFields.SelectedItem != null)
        //    {
        //        selectedCaption = cboFields.SelectedItem.ToString().Trim();
        //        string selectedColumn = GetColumnForCaption(selectedCaption);

        //        if (!string.IsNullOrEmpty(selectedColumn))
        //        {
        //            string dataType = GetDataTypeForField(selectedColumn);
        //            if (!string.IsNullOrEmpty(dataType))
        //            {
        //                // Update Help.send_combo_box_value
        //                Help.send_combo_box_value = dataType;

        //                // Clear and populate cboRel based on data type
        //                cboRel.Items.Clear();

        //                if (dataType == "char" || dataType == "varchar")
        //                {
        //                    cboRel.Items.Add("=");
        //                    cboRel.Items.Add("LIKE");
        //                }
        //                else
        //                {
        //                    cboRel.Items.Add("=");
        //                    cboRel.Items.Add("<");
        //                    cboRel.Items.Add("<=");
        //                    cboRel.Items.Add(">");
        //                    cboRel.Items.Add(">=");
        //                }

        //                // Set the default selected item to "LIKE" if available
        //                int likeIndex = cboRel.Items.IndexOf("LIKE");
        //                cboRel.SelectedIndex = likeIndex != -1 ? likeIndex : 0;

        //                // Clear txtValue and set focus if cboFields.SelectedItem is not null or empty
        //                txtValue.Text = string.Empty;
        //                if (!string.IsNullOrEmpty(cboFields.SelectedItem.ToString()))
        //                {
        //                    txtValue.Focus();
        //                }

        //                // Set the selected index of cboDataType to match cboFields
        //                cboDataType.SelectedIndex = cboFields.SelectedIndex;

        //                // Get the selected item from cboDataType
        //                selectedDataType = cboDataType.SelectedItem.ToString();
        //                Console.WriteLine("Selected Data Type: " + selectedDataType); // Replace with your actual logic
        //            }
        //        }
        //        selectedFieldIndex = cboFields.SelectedIndex;
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
                        likeIndex = cboRel.Items.IndexOf("LIKE");
                        cboRel.SelectedIndex = likeIndex != -1 ? likeIndex : 0;
                        Help.selectedrel = likeIndex;

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

        public string GetColumnForCaption(string caption)
        {
            (string query, string[] captions) = GetQueryAndCaptionsFromHelpTable(Help.i_Help_id);
            if (captions == null || captions.Length == 0) return null;

            Dictionary<string, string> columnAliasMapping;
            List<string> columnNames = ExtractColumnNamesFromQuery(query, out columnAliasMapping);
            if (columnNames == null || columnNames.Count == 0) return null;

            for (int i = 0; i < captions.Length; i++)
            {
                if (captions[i].Trim() == caption && i < columnNames.Count)
                {
                    // If column name is in alias mapping, return the original column name
                    string columnName = columnNames[i];
                    if (columnAliasMapping.ContainsValue(columnName))
                    {
                        return columnAliasMapping.FirstOrDefault(x => x.Value == columnName).Key;
                    }
                    return columnName;
                }
            }
            return null;
        }



        private List<string> ExtractColumnNamesFromQuery(string query, out Dictionary<string, string> columnAliasMapping)
        {
            List<string> columnNames = new List<string>();
            columnAliasMapping = new Dictionary<string, string>();

            // Extract the part of the query between SELECT and FROM
            string columnsPart = query.Substring(query.IndexOf("SELECT", StringComparison.OrdinalIgnoreCase) + 6,
                                                 query.IndexOf("FROM", StringComparison.OrdinalIgnoreCase) -
                                                 (query.IndexOf("SELECT", StringComparison.OrdinalIgnoreCase) + 6));
            string[] columns = columnsPart.Split(',');

            foreach (string column in columns)
            {
                string cleanedColumn = column.Trim();

                // Handle column aliases (e.g., C.tax_per as tax_per1)
                int asIndex = cleanedColumn.IndexOf(" as ", StringComparison.OrdinalIgnoreCase);
                if (asIndex != -1)
                {
                    string originalColumn = cleanedColumn.Substring(0, asIndex).Trim();
                    string alias = cleanedColumn.Substring(asIndex + 4).Trim();
                    cleanedColumn = originalColumn;
                    columnAliasMapping[alias] = cleanedColumn;
                }

                // Remove table aliases (e.g., A., B., C.)
                int dotIndex = cleanedColumn.IndexOf('.');
                if (dotIndex != -1)
                {
                    cleanedColumn = cleanedColumn.Substring(dotIndex + 1);
                }

                // Add cleaned column name to the list
                columnNames.Add(cleanedColumn);
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

        public string GetDataTypeForField(string fieldName)
        {
            dbConnector = new DbConnector();
            using (dbConnector.connection = new OdbcConnection(dbConnector.connectionString))
            {
                dbConnector.OpenConnection();
                string tableName = ExtractTableNameFromHelpQuery(fieldName); // Pass the fieldName to the function
                if (string.IsNullOrEmpty(tableName)) return null;

                string sql = "SELECT DATA_TYPE FROM information_schema.COLUMNS WHERE TABLE_NAME = '" + tableName + "' AND COLUMN_NAME = '" + fieldName + "'";

                using (OdbcCommand Cmddatatype = new OdbcCommand(sql, dbConnector.connection))
                {
                    object result = Cmddatatype.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }


        //private void LoadQueryAndColumnDataTypes()
        //{
        //    columnDataTypes = new Dictionary<string, string>();

        //    string query = GetQueryFromHelpTable(Help.i_Help_id);
        //    if (string.IsNullOrEmpty(query)) return;

        //    // Extract the table name from the query
        //    string tableName = ExtractTableNameFromHelpQuery();
        //    if (string.IsNullOrEmpty(tableName)) return;

        //    // Extract column names from the query
        //    List<string> columnNames = ExtractColumnNamesFromQuery(query);

        //    // Fetch data types for each column and populate columnDataTypes dictionary
        //    foreach (string columnName in columnNames)
        //    {
        //        string dataType = GetDataTypeForField(columnName);
        //        if (!string.IsNullOrEmpty(dataType))
        //        {
        //            columnDataTypes[columnName] = dataType;
        //        }
        //    }

        //    PopulateCboDataType();
        //}

        private void LoadQueryAndColumnDataTypes()
        {
            columnDataTypes = new Dictionary<string, string>();

            string query = GetQueryFromHelpTable(Help.i_Help_id);
            if (string.IsNullOrEmpty(query)) return;

            // Extract column names from the query
            Dictionary<string, string> columnAliasMapping;
            List<string> columnNames = ExtractColumnNamesFromQuery(query, out columnAliasMapping);

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

        //private string ExtractTableNameFromHelpQuery()
        //{
        //    string query = GetQueryFromHelpTable(Help.i_Help_id);
        //    if (string.IsNullOrEmpty(query)) return null;

        //    int fromIndex = query.IndexOf("FROM", StringComparison.OrdinalIgnoreCase);
        //    if (fromIndex != -1)
        //    {
        //        string fromPart = query.Substring(fromIndex + 5);
        //        string[] parts = fromPart.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        //        return parts.Length > 0 ? parts[0] : string.Empty;
        //    }
        //    return null;
        //}

        private string ExtractTableNameFromHelpQuery(string columnName)
        {
            string query = GetQueryFromHelpTable(Help.i_Help_id);
            if (string.IsNullOrEmpty(query)) return null;

            Dictionary<string, string> tableAliasMapping = ExtractTableAliasMapping(query);
            Dictionary<string, string> columnAliasMapping;
            List<string> columnNames = ExtractColumnNamesFromQuery(query, out columnAliasMapping);

            // Check if the column name is an alias
            if (columnAliasMapping.ContainsKey(columnName))
            {
                columnName = columnAliasMapping[columnName];
            }

            // Iterate through table alias mapping to find the table name
            foreach (var entry in tableAliasMapping)
            {
                string alias = entry.Key;
                string tableName = entry.Value;

                // Check if the column belongs to this table alias
                if (query.IndexOf($"{alias}.{columnName}", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return tableName;
                }
            }

            // If no alias found, check for the column directly in the query
            foreach (var entry in tableAliasMapping)
            {
                string tableName = entry.Value;

                // Check if the column belongs to this table directly without alias
                if (query.IndexOf(columnName, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return tableName;
                }
            }

            // If no match found, return null
            return null;
        }


        private Dictionary<string, string> ExtractTableAliasMapping(string query)
        {
            Dictionary<string, string> tableAliasMapping = new Dictionary<string, string>();

            int fromIndex = query.IndexOf("FROM", StringComparison.OrdinalIgnoreCase);
            if (fromIndex != -1)
            {
                string fromPart = query.Substring(fromIndex + 5);
                string[] parts = fromPart.Split(',');

                foreach (string part in parts)
                {
                    string[] tableAlias = part.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tableAlias.Length == 2)
                    {
                        // Assuming format is "table_name alias"
                        tableAliasMapping[tableAlias[1].Trim()] = tableAlias[0].Trim();
                    }
                    else if (tableAlias.Length == 1)
                    {
                        // If no alias, use the table name itself
                        tableAliasMapping[tableAlias[0].Trim()] = tableAlias[0].Trim();
                    }
                }
            }

            return tableAliasMapping;
        }

        private string GetQueryFromHelpTable(int helpId)
        {
            string query = string.Empty;
            dbConnector = new DbConnector();
            // dbConnector.connectionString= new OdbcConnection();
            using (dbConnector.connection = new OdbcConnection(dbConnector.connectionString))
            {
                dbConnector.OpenConnection();
                string sql = "SELECT Query FROM help WHERE help_id = " + helpId + "";

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
                if (DeTools.gobjActiveForm.Name == "frmT_Invoice")
                {
                    Form form1 = DeTools.gobjActiveForm;
                    Help.TransferDataInv(form1);
                    frmT_Invoice frmT_Invoice = new frmT_Invoice();
                    //frmT_Invoice.Show(); // Show the form

                    //// Add a new row to the DataGridView
                    //frmT_Invoice.dbgItemDet.Rows.Add();

                    //// Refresh the form
                    //frmT_Invoice.Refresh();
                    //frmT_Invoice.dbgItemDet.Update();
                    //frmT_Invoice.dbgItemDet.Refresh();

                    //// Set the current cell
                    //if (frmT_Invoice.dbgItemDet.Rows.Count > 0)
                    //{
                    //    frmT_Invoice.dbgItemDet.CurrentCell = frmT_Invoice.dbgItemDet.Rows[frmT_Invoice.dbgItemDet.Rows.Count - 1].Cells[1];
                    //}
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
                txtValue.Text = "";
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
            txtValue.Text = "";
        }

        private void frmHelp_Load(object sender, EventArgs e)
        {
            // Assuming cboDataType should be populated with a list of data types
            //cboDataType.Items.Add("Integer");
            //cboDataType.Items.Add("Decimal");
            //cboDataType.Items.Add("Date");
            //cboDataType.Items.Add("String");
            cboFields.SelectedIndexChanged += cboFields_SelectedIndexChanged;
        }

        

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the beep sound on Enter key press

                // Simulate the visual click
                cmdOK.PerformClick();

                grdHelp.Focus();               
                grdHelp.CurrentCell = grdHelp.Rows[0].Cells[1];
            }
        }

        private void grdHelp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                flag_Enteryn= true;
                e.Handled = true; // Prevent the default behavior
                e.SuppressKeyPress = true; // Prevent the beep sound

                DataGridView dgv = (DataGridView)sender;
                int rowIndex = dgv.CurrentCell.RowIndex;
                int columnIndex = dgv.CurrentCell.ColumnIndex;

                // Trigger the CellDoubleClick event
                grdHelp_CellDoubleClick(dgv, new DataGridViewCellEventArgs(columnIndex, rowIndex));
                flag_Enteryn = false;
            }
        }
    }
}