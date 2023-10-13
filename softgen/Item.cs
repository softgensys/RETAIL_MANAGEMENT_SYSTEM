//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using System.Windows.Forms.VisualStyles;



namespace softgen
{
    public partial class Item : Form
    {
        public DbConnector dbConnector;



        public Item()
        {
            InitializeComponent();

            dbConnector = new DbConnector();
            ComboBoxDataLoader.SetDbConnector(dbConnector);

            this.Load += Item_Load;

        }

        public void Item_Load(object sender, EventArgs e)
        {
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            dataGridView1.AllowUserToAddRows = true;
            Dataonhelpitemid();
        }

        //////Set Text On Form ///////////////////////
        public void UpdateFormTitle(string newTitle)
        {
            this.Text = newTitle;
        }
        ///////////////////////

        public void PopulateDataGridView(DataGridView dataGridView, DataTable dataTable)
        {
            dataGridView.Rows.Clear(); // Clear existing rows

            // Populate data into existing columns
            foreach (DataRow row in dataTable.Rows)
            {
                List<object> rowData = new List<object>();

                // Extract data for each column
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName == "Active") // Assuming the column name is "Active"
                    {
                        // Add checkbox value based on Active value
                        bool isActive = row[column].ToString().ToUpper() == "Y";
                        DataGridViewCheckBoxCell checkboxCell = new DataGridViewCheckBoxCell();
                        checkboxCell.Value = isActive;
                        rowData.Add(checkboxCell);
                    }
                    else
                    {
                        rowData.Add(row[column]);
                    }
                }

                // Add row to the DataGridView
                dataGridView.Rows.Add(rowData.ToArray());
            }
        }

        public void SetDataGridViewReadOnly(DataGridView dataGridView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.ReadOnly = true;
            }

        }

        public void Dataonhelpitemid()
        {

            // Retrieve the data from the query using the DbConnector
            DataTable dataTable = dbConnector.ExecuteQuery("SELECT plu, bar_code,cost_price, mrp, sale_price, net_rate, active_yn FROM softgen_db.m_item_det WHERE item_id = '102501'");

            // Populate the DataGridView with the retrieved data
            PopulateDataGridView(dataGridView1, dataTable);

            // Make the fields read-only
            SetDataGridViewReadOnly(dataGridView1);

        }

        public void grpcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string tableName = "m_group";
            string columnName = "group_desc";
            string ClauseValue = grpcomb.SelectedValue.ToString();
            whereClauseVariable.Add("group_id");
            whereClauseValue.Add(ClauseValue);
            int op = 0;

            ComboBoxDataLoader.GetcomboValue_in_txt(tableName, columnName, whereClauseVariable, whereClauseValue, op, lblgrp);

            if (ClauseValue != "")
            {
                subgrpcomb.Enabled = true;
            }
            else { subgrpcomb.Enabled = false; }

        }

        public void grpcomb_MouseDown(object sender, MouseEventArgs e)
        {

            //this will give the name of the combo box coz sender is the object.//
            ComboBox comboBox = (ComboBox)sender; // Cast the sender object to ComboBox 

            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();

            String tableName = "m_group";
            whereClauseVariable.Add("");
            whereClauseValue.Add("");
            String selectColumn = "group_id";
            ComboBoxDataLoader.LoadDataIntoComboBox(tableName, whereClauseVariable, whereClauseValue, 0, selectColumn, comboBox);

        }

        public void comboBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender; // Cast the sender object to ComboBox 

            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();

            String tableName = "m_manuf";
            whereClauseVariable.Add("");
            whereClauseValue.Add("");
            String selectColumn = "manuf_id";
            ComboBoxDataLoader.LoadDataIntoComboBox(tableName, whereClauseVariable, whereClauseValue, 0, selectColumn, comboBox);

        }

        public void manufcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string tableName = "m_manuf";
            string columnName = "manuf_name";
            string ClauseValue1 = manufcomb.SelectedValue.ToString();
            whereClauseVariable.Add("manuf_id");
            whereClauseValue.Add(ClauseValue1);
            int op = 0;


            ComboBoxDataLoader.GetcomboValue_in_txt(tableName, columnName, whereClauseVariable, whereClauseValue, op, lblmanuf);
        }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void grptxt_TextChanged(object sender, EventArgs e)
        {

        }

        public void lblgrp_Click(object sender, EventArgs e)
        {

        }



        public void typcomb_MouseDown(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender; // Cast the sender object to ComboBox 

            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            String tableName = "m_item_type";
            whereClauseVariable.Add("");
            whereClauseValue.Add("");
            //  int op = 0;
            String selectColumn = "item_type_id";
            ComboBoxDataLoader.LoadDataIntoComboBox(tableName, whereClauseVariable, whereClauseValue, 0, selectColumn, comboBox);

            comboBox.SelectedIndex = -1;
            lbltype.Text = "";
        }

        public void typcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedIndex != -1)
            {
                List<string> whereClauseVariable = new List<string>();
                List<string> whereClauseValue = new List<string>();

                string tableName = "m_item_type";
                string columnName = "item_type_desc";
                string ClauseValue1 = typcomb.SelectedValue.ToString();
                whereClauseVariable.Add("item_type_id");
                whereClauseValue.Add(ClauseValue1);
                int op = 0;


                ComboBoxDataLoader.GetcomboValue_in_txt(tableName, columnName, whereClauseVariable, whereClauseValue, op, lbltype);
            }

        }

        public void subgrpcomb_MouseDown(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender; // Cast the sender object to ComboBox 

            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string ClauseValue = grpcomb.SelectedValue.ToString();
            String tableName = "m_sub_group";
            whereClauseVariable.Add("group_id");
            whereClauseValue.Add(ClauseValue);
            String selectColumn = "sub_group_id";
            ComboBoxDataLoader.LoadDataIntoComboBox(tableName, whereClauseVariable, whereClauseValue, 0, selectColumn, comboBox);
        }

        public void subgrpcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string tableName = "m_sub_group";
            string columnName = "sub_group_desc";
            string ClauseValue = subgrpcomb.SelectedValue.ToString();
            whereClauseVariable.Add("sub_group_id");
            whereClauseValue.Add(ClauseValue);
            int op = 0;

            ComboBoxDataLoader.GetcomboValue_in_txt(tableName, columnName, whereClauseVariable, whereClauseValue, op, lblsubgrp);

            if (ClauseValue != "")
            {
                ssgrpcomb.Enabled = true;
            }
            else
            {
                ssgrpcomb.Enabled = false;
            }
        }

        public void ssgrpcomb_MouseDown(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender; // Cast the sender object to ComboBox 

            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string ClauseValue1 = grpcomb.SelectedValue.ToString();
            string ClauseValue2 = subgrpcomb.SelectedValue.ToString();
            String tableName = "m_sub_sub_group";
            whereClauseVariable.Add("group_id");
            whereClauseValue.Add(ClauseValue1);

            whereClauseVariable.Add("sub_group_id");
            whereClauseValue.Add(ClauseValue2);

            String selectColumn = "sub_sub_group_id";
            ComboBoxDataLoader.LoadDataIntoComboBox(tableName, whereClauseVariable, whereClauseValue, 1, selectColumn, comboBox);

        }

        public void ssgrpcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string tableName = "m_sub_sub_group";
            string columnName = "sub_sub_group_desc";
            string ClauseValue = ssgrpcomb.SelectedValue.ToString();
            whereClauseVariable.Add("sub_sub_group_id");
            whereClauseValue.Add(ClauseValue);
            int op = 0;

            ComboBoxDataLoader.GetcomboValue_in_txt(tableName, columnName, whereClauseVariable, whereClauseValue, op, lblssgrp);
        }

        public void colidcomb_MouseDown(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender; // Cast the sender object to ComboBox 

            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            //string ClauseValue = grpcomb.SelectedValue.ToString();
            String tableName = "m_color";
            whereClauseVariable.Add("");
            whereClauseValue.Add("");
            String selectColumn = "color_id";
            ComboBoxDataLoader.LoadDataIntoComboBox(tableName, whereClauseVariable, whereClauseValue, 0, selectColumn, comboBox);
        }

        public void colidcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string tableName = "m_color";
            string columnName = "color_desc";
            string ClauseValue = colidcomb.SelectedValue.ToString();
            whereClauseVariable.Add("color_id");
            whereClauseValue.Add(ClauseValue);
            int op = 0;

            ComboBoxDataLoader.GetcomboValue_in_txt(tableName, columnName, whereClauseVariable, whereClauseValue, op, lblcolid);

        }

        public void sizecomb_MouseDown(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender; // Cast the sender object to ComboBox 

            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            //string ClauseValue = grpcomb.SelectedValue.ToString();
            String tableName = "m_size";
            whereClauseVariable.Add("");
            whereClauseValue.Add("");
            String selectColumn = "size_id";
            ComboBoxDataLoader.LoadDataIntoComboBox(tableName, whereClauseVariable, whereClauseValue, 0, selectColumn, comboBox);
        }

        public void sizecomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string tableName = "m_size";
            string columnName = "color_desc";
            string ClauseValue = sizecomb.SelectedValue.ToString();
            whereClauseVariable.Add("size_id");
            whereClauseValue.Add(ClauseValue);
            int op = 0;

            ComboBoxDataLoader.GetcomboValue_in_txt(tableName, columnName, whereClauseVariable, whereClauseValue, op, lblsizeid);

        }

        public void purunitcomb_MouseDown(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender; // Cast the sender object to ComboBox 

            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            //string ClauseValue = grpcomb.SelectedValue.ToString();
            String tableName = "m_unit";
            whereClauseVariable.Add("");
            whereClauseValue.Add("");
            String selectColumn = "unit_id";
            ComboBoxDataLoader.LoadDataIntoComboBox(tableName, whereClauseVariable, whereClauseValue, 0, selectColumn, comboBox);
        }

        public void purunitcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string tableName = "m_unit";
            string columnName = "unit_desc";
            string ClauseValue = purunitcomb.SelectedValue.ToString();
            whereClauseVariable.Add("unit_id");
            whereClauseValue.Add(ClauseValue);
            int op = 0;

            ComboBoxDataLoader.GetcomboValue_in_txt(tableName, columnName, whereClauseVariable, whereClauseValue, op, lblpurunit);
        }

        public void salunitcomb_MouseDown(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender; // Cast the sender object to ComboBox 

            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            //string ClauseValue = grpcomb.SelectedValue.ToString();
            String tableName = "m_unit";
            whereClauseVariable.Add("");
            whereClauseValue.Add("");
            String selectColumn = "unit_id";
            ComboBoxDataLoader.LoadDataIntoComboBox(tableName, whereClauseVariable, whereClauseValue, 0, selectColumn, comboBox);
        }

        public void salunitcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string tableName = "m_unit";
            string columnName = "unit_desc";
            string ClauseValue = salunitcomb.SelectedValue.ToString();
            whereClauseVariable.Add("unit_id");
            whereClauseValue.Add(ClauseValue);
            int op = 0;

            ComboBoxDataLoader.GetcomboValue_in_txt(tableName, columnName, whereClauseVariable, whereClauseValue, op, lblsalunit);
        }

        public void gstperccomb_MouseDown(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender; // Cast the sender object to ComboBox 

            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            //string ClauseValue = grpcomb.SelectedValue.ToString();
            String tableName = "m_tax_type";
            whereClauseVariable.Add("");
            whereClauseValue.Add("");
            String selectColumn = "tax_type_id";
            ComboBoxDataLoader.LoadDataIntoComboBox(tableName, whereClauseVariable, whereClauseValue, 0, selectColumn, comboBox);
        }

        public void gstperccomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> whereClauseVariable = new List<string>();
            List<string> whereClauseValue = new List<string>();
            string tableName = "m_tax_type";
            string columnName = "tax_type_desc";
            string ClauseValue = gstperccomb.SelectedValue.ToString();
            whereClauseVariable.Add("tax_type_id");
            whereClauseValue.Add(ClauseValue);
            int op = 0;

            ComboBoxDataLoader.GetcomboValue_in_txt(tableName, columnName, whereClauseVariable, whereClauseValue, op, lblgstperc);
        }

        public void Item_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide(); // Hide the child form when it is minimized
                MdiParent.Refresh(); // Refresh the MDI parent form to update the layout
            }
        }

        public void Item_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm parentform = (MainForm)this.MdiParent;

            parentform.dashpanel.Visible = true;
            parentform.mainpanel.Visible = false;
            parentform.formpanel.Visible = false;
        }

        public void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {


        }
        public void ValidateColumnValues(int rowIndex, int column2Index, int column3Index, int column4Index)
        {
            //DataGridViewCell column2cell = dataGridView1.Rows[rowIndex].Cells[column2Index];
            //DataGridViewCell column3cell = dataGridView1.Rows[rowIndex].Cells[column3Index];
            //DataGridViewCell MRPcell = dataGridView1.Rows[rowIndex].Cells[column3Index];
            //DataGridViewCell SPcell = dataGridView1.Rows[rowIndex].Cells[column4Index];
            //DataGridViewCell CPcell = dataGridView1.Rows[rowIndex].Cells[column2Index];

            //if (column2cell.Value != null && column3cell.Value != null)
            //{
            //    decimal col2val = Convert.ToDecimal(column2cell.Value);
            //    decimal col3val;

            //    if (!decimal.TryParse(column3cell.Value.ToString(), out col3val))
            //    {
            //        dataGridView1.Rows[rowIndex].ErrorText = "Invalid Numeric Value";
            //        return;
            //    }
            //    if (col3val < col2val)
            //    {
            //        MessageBox.Show("MRP cannot be Smaller Than CP!");
            //        dataGridView1.Rows[rowIndex].Cells[column3Index].Value = 0;
            //        return;
            //    }
            //}

            //if (SPcell.Value != null && MRPcell.Value != null)
            //{
            //    decimal MRPval = Convert.ToDecimal(MRPcell.Value);
            //    decimal SPval;

            //    if (!decimal.TryParse(SPcell.Value.ToString(), out SPval))
            //    {
            //        dataGridView1.Rows[rowIndex].ErrorText = "Invalid Numeric Value";
            //        return;
            //    }
            //    if (SPval > MRPval)
            //    {
            //        MessageBox.Show("SP cannot be Greater Than MRP!");
            //        dataGridView1.Rows[rowIndex].Cells[column4Index].Value = 0;
            //        return;
            //    }
            //}

            //if (CPcell.Value != null && MRPcell.Value != null)
            //{
            //    decimal MRPval = Convert.ToDecimal(MRPcell.Value);
            //    decimal CPval;

            //    if (!decimal.TryParse(CPcell.Value.ToString(), out CPval))
            //    {
            //        dataGridView1.Rows[rowIndex].ErrorText = "Invalid Numeric Value";
            //        return;
            //    }
            //    if (CPval > MRPval)
            //    {
            //        MessageBox.Show("CP cannot be Greater Than MRP! or SP!");
            //        dataGridView1.Rows[rowIndex].Cells[column2Index].Value = 0;
            //        return;
            //    }
            //}
        }

        public void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int columnStartIndex = 1; // Starting column index to apply the restriction
            int columnEndIndex = 3; // Ending column index to apply the restriction

            if (dataGridView1.CurrentCell.ColumnIndex >= columnStartIndex && dataGridView1.CurrentCell.ColumnIndex <= columnEndIndex) // Assuming the column index is 1 for the barcode column
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.KeyPress -= NumericOnlyKeyPress;
                    textBox.KeyPress += NumericOnlyKeyPress;

                    // Perform validation when the editing control is shown
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    int column2Index = 2;
                    int column3Index = 3;
                    int column4Index = 4;
                    ValidateColumnValues(rowIndex, column2Index, column3Index, column4Index);
                }
            }

        }
        public void NumericOnlyKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        public void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // Perform validation when the editing control is shown
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int column2Index = 2;
                int column3Index = 3;
                int column4Index = 4;
                ValidateColumnValues(rowIndex, column2Index, column3Index, column4Index);

            }
        }

        public void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }


        public void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        public string GetPLUValue(int counter)
        {
            // Customize the PLU format here based on the counter
            if (counter <= 9)
            {
                return "-00" + counter.ToString();
            }
            else if (counter <= 99)
            {
                return "-0" + counter.ToString();
            }
            else
            {
                return "-" + counter.ToString();
            }
        }
        public int pluCounter = 0; // Counter to track the auto-generated PLU number

        public void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int barcodeColumnIndex = 1; // Assuming the barcode column is the 2nd column (index 1)

            // Check if the edited cell is in the barcode column
            if (e.ColumnIndex == barcodeColumnIndex && e.RowIndex >= 0)
            {
                string barcodeValue = dataGridView1.Rows[e.RowIndex].Cells[barcodeColumnIndex].Value.ToString().Trim();

                if (!string.IsNullOrEmpty(barcodeValue))
                {
                    // Increment the PLU counter and generate the PLU value based on the counter
                    pluCounter++;

                    string pluValue = GetPLUValue(pluCounter); // Function to get the PLU value in the desired format

                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = pluValue; // Update the PLU column value
                }
            }
            ////////////////////////////////////////////
            ///   // Your additional validation and functionality
            int rowIndex = e.RowIndex;
            int column2Index = 2; // Index of column for CP values
            int column3Index = 3; // Index of column for MRP values
            int column4Index = 4; // Index of column for SP values

            DataGridViewCell column2cell = dataGridView1.Rows[rowIndex].Cells[column2Index];
            DataGridViewCell column3cell = dataGridView1.Rows[rowIndex].Cells[column3Index];
            DataGridViewCell MRPcell = dataGridView1.Rows[rowIndex].Cells[column3Index];
            DataGridViewCell SPcell = dataGridView1.Rows[rowIndex].Cells[column4Index];
            DataGridViewCell CPcell = dataGridView1.Rows[rowIndex].Cells[column2Index];


            // Rest of your validation code for column values
            if (column2cell.Value != null && column3cell.Value != null)
            {
                decimal col2val = Convert.ToDecimal(column2cell.Value);
                decimal col3val;

                if (!decimal.TryParse(column3cell.Value.ToString(), out col3val))
                {
                    dataGridView1.Rows[rowIndex].ErrorText = "Invalid Numeric Value";
                    return;
                }
                if (col3val < col2val)
                {
                    MessageBox.Show("MRP cannot be Smaller Than CP!");
                    dataGridView1.Rows[rowIndex].Cells[column3Index].Value = col3val;
                    return;
                }
                else if (col3val <= 0 || col2val <= 0)
                {
                    MessageBox.Show("MRP OR CP CANNOT BE 0 OR SMALLER THAN 0!");
                    dataGridView1.Rows[rowIndex].Cells[column3Index].Value = 0;
                    return;

                }
            }

            if (SPcell.Value != null && MRPcell.Value != null)
            {
                decimal MRPval = Convert.ToDecimal(MRPcell.Value);
                decimal CPval = Convert.ToDecimal(CPcell.Value);

                decimal SPval;

                if (!decimal.TryParse(SPcell.Value.ToString(), out SPval))
                {
                    dataGridView1.Rows[rowIndex].ErrorText = "Invalid Numeric Value";
                    return;
                }
                if (SPval > MRPval)
                {
                    MessageBox.Show("SP cannot be Greater Than MRP!");
                    dataGridView1.Rows[rowIndex].Cells[column4Index].Value = SPval;
                    return;
                }
                if (SPval < CPval)
                {
                    MessageBox.Show("SP cannot be SMALLER Than CP!");
                    dataGridView1.Rows[rowIndex].Cells[column4Index].Value = SPval;
                    return;
                }
                else if (SPval <= 0)
                {
                    MessageBox.Show("SP CANNOT BE 0 OR SMALLER THAN 0!");
                    dataGridView1.Rows[rowIndex].Cells[column3Index].Value = 0;
                    return;

                }
            }

            if (CPcell.Value != null && MRPcell.Value != null)
            {
                decimal MRPval = Convert.ToDecimal(MRPcell.Value);
                decimal CPval;

                if (!decimal.TryParse(CPcell.Value.ToString(), out CPval))
                {
                    dataGridView1.Rows[rowIndex].ErrorText = "Invalid Numeric Value";
                    return;
                }
                if (CPval > MRPval)
                {
                    MessageBox.Show("CP cannot be Greater Than MRP! or SP!");
                    dataGridView1.Rows[rowIndex].Cells[column2Index].Value = CPval;
                    return;
                }
            }


        }

        private void itemidtxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

