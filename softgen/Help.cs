using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace softgen
{
    public static class Help
    {
        private static string s_Mode="";
        private static int i_Help_id;
        private static int i_NoOfRecords;
        private static Form o_form;
        private static Control o_control;  
        public static Dictionary<Control, string> controlToHelpTopicMapping = new Dictionary<Control, string>();
        public static frmHelp frmHelp= new frmHelp();
        public static DbConnector dbConnector;

        /////////////////////////////////---start-----///////////////////////////////////

        public static void ClickHelp(Form form, Control control)
        {
            o_form = form;
            o_control = control;

            i_NoOfRecords = 0;

            s_Mode = DeTools.GetMode(form);

            if (controlToHelpTopicMapping.TryGetValue(control, out var helpTopic))
            {
                i_Help_id = Int32.Parse(helpTopic);

                switch (i_Help_id)
                {
                    case 0:
                        // Currently Help is not available
                        frmHelp.pnlText.Text = "Currently Help is not available.";
                        frmHelp.pnlText.Visible = true;
                        break;

                    case var _ when i_Help_id < 3000:
                        // In add mode, don't display the grid help.
                        if (s_Mode == DeTools.ADDMODE)
                        {
                            PrepareGridHelp();
                        }
                        else
                        {
                            PrepareGridHelp();
                        }
                        break;

                    case var _ when i_Help_id > 9000:
                        // These helps are for a substitute for combo help.
                        PrepareGridHelp();
                        break;

                    case var _ when i_Help_id > 3000:
                        PrepareTextHelp();
                        break;
                }
            }

            frmHelp.pnlToptxt.Text = DeTools.RestoreCaption(form) + " (" + i_NoOfRecords.ToString().Trim() + ")";
            frmHelp.pnlToptxt.Tag = DeTools.RestoreCaption(form);
            FillFields(frmHelp.cboFields, frmHelp.cboFieldsId);
            FillFields(frmHelp.cboOrder, frmHelp.cboOrderId);
            frmHelp.Show();
        }



        public static void DisplayHelp(Form form, int keyCode, Control control)
        {
            if (keyCode == (int)Keys.F1)
            {
                o_form = form;
                o_control = control;

                i_NoOfRecords = 0;

                s_Mode = DeTools.GetMode(form);
                //i_Help_id = control.HelpContextID;

                if (controlToHelpTopicMapping.TryGetValue(control, out var helpTopic))
                {
                   i_Help_id = Int32.Parse(helpTopic);


                    switch (i_Help_id)
                    {
                        case 0:
                            // Currently Help is not available
                            frmHelp.pnlText.Text = "Currently Help is not available.";
                            frmHelp.pnlText.Visible = true;
                            break;

                        case var _ when i_Help_id < 3000:
                            // In add mode, don't display the grid help.
                            if (s_Mode == DeTools.ADDMODE)
                            {
                                PrepareGridHelp();
                            }
                            else
                            {
                                PrepareGridHelp();
                            }
                            break;

                        case var _ when i_Help_id > 9000:
                            // These helps are for a substitute for combo help.
                            PrepareGridHelp();
                            break;

                        case var _ when i_Help_id > 3000:
                            PrepareTextHelp();
                            break;
                    }
                }

                frmHelp.pnlToptxt.Text = DeTools.RestoreCaption(form) + " (" + i_NoOfRecords.ToString().Trim() + ")";
                frmHelp.pnlToptxt.Tag = DeTools.RestoreCaption(form);
                FillFields(frmHelp.cboFields, frmHelp.cboFieldsId);
                FillFields(frmHelp.cboOrder, frmHelp.cboOrderId);
                frmHelp.Show();

                switch (i_Help_id)
                {
                    case 9001:
                        frmHelp.cboDataType.Items.Add(1);
                        frmHelp.cboFields.Items.Add("Item Description");
                        break;
                    case 9002:
                        frmHelp.cboDataType.Items.Add(1);
                        frmHelp.cboFields.Items.Add("Customer Name");
                        break;
                    case 9005:
                        frmHelp.cboDataType.Items.Add(1);
                        frmHelp.cboFields.Items.Add("Supplier Name");
                        break;
                    case 9010:
                        frmHelp.cboDataType.Items.Add(1);
                        frmHelp.cboFields.Items.Add("Item Description");
                        break;
                    case 9011:
                        frmHelp.cboDataType.Items.Add(1);
                        frmHelp.cboFields.Items.Add("Item Description");
                        break;
                    case 1005:
                        frmHelp.cboDataType.Items.Add(1);
                        frmHelp.cboFields.Items.Add("Customer Name");
                        break;
                    case 1006:
                        frmHelp.cboDataType.Items.Add(1);
                        frmHelp.cboFields.Items.Add("Item Name");
                        break;
                    case 1013:
                        frmHelp.cboDataType.Items.Add(1);
                        frmHelp.cboFields.Items.Add("Supplier Name");
                        break;
                    case 9014:
                        frmHelp.cboDataType.Items.Add(1);
                        frmHelp.cboFields.Items.Add("Description");
                        break;
                    case 9018:
                        frmHelp.cboDataType.Items.Add(1);
                        frmHelp.cboFields.Items.Add("Description");
                        break;
                    case 9006:
                        frmHelp.cboDataType.Items.Add(1);
                        frmHelp.cboFields.Items.Add("Description");
                        break;
                }
            }
        }

        public static void FieldHelp(string strReqdFor, Button objControl)
        {
 //--todo-           Messages.HelpMsg(objControl.Tooltiptext);

            if (strReqdFor == "S" || strReqdFor == "A")
            {
                // Display shadow only if required for saving or authorization.
                objControl.FlatStyle = FlatStyle.Flat;
                objControl.FlatAppearance.BorderSize = 1;
                objControl.FlatAppearance.BorderColor = Color.DarkGray;
            }
        }


        public static void FillFields(ComboBox cboField, ComboBox cboFieldId)
        {
            string strFieldName, strFormType, strQuery;
            int i_Cols;
            string[] strField;

            strFormType = o_form.Name.Trim().Substring(3, 1).ToUpper();

            dbConnector = new DbConnector();

            dbConnector.OpenConnection();

            DeTools.gstrSQL = "Select Query, No_of_columns, Caption1, Caption2, Caption3, Caption4, Caption5, " +
                   "Caption6, Caption7, Caption8, Caption9, Caption10 " +
                   "from Help where Help_id = " + "'"+i_Help_id+"'";

            using (OdbcDataReader reader = dbConnector.CreateResultset(DeTools.gstrSQL))
            {
                if (reader.HasRows)
                {
                    cboField.Items.Clear();
                    cboFieldId.Items.Clear();
                    cboField.SelectedIndex = -1;

                    i_Cols = (int)reader["No_of_columns"];

                    for (int J = 0; J < i_Cols; J++)
                    {
                        strFieldName = "Caption" + (J + 1).ToString().Trim();
                        cboField.Items.Add(reader[strFieldName].ToString().Trim());
                    }
                }
                else
                {
                    cboField.Items.Clear();
                    cboFieldId.Items.Clear();
                    cboField.SelectedIndex = -1;
                }
            }
        }

        public static void PrepareGridHelp()
        {
            int i, j, i_Rows, i_Cols, Count;
            string strFieldName, strFormType, strOrderBy;
            string strFields;
            string strCondition;
            object strValue = null;
            string strheading="";
            string strMode = new string(' ', 1);


            strFormType = o_form.Name.Trim().Substring(3, 1).ToUpper();
            frmHelp.pnlText.Visible = false;

            dbConnector = new DbConnector();
            dbConnector.OpenConnection();

            // Select the query from the Help table
            DeTools.gstrSQL = "Select Query, A_Mode_Cond, M_Mode_Cond, D_Mode_Cond, I_Mode_Cond, " +
                "P_Mode_Cond, Order_by, No_of_columns, Caption1, Caption2, " +
                "Caption3, Caption4, Caption5, Caption6, Caption7, " +
                "Caption8, Caption9, Caption10 " +
                "from Help where Help_id = " + "'"+i_Help_id+"';";
            //rs_Query = CreateResultset(gstrSQL);
            using (OdbcDataReader rs_Query = dbConnector.CreateResultset(DeTools.gstrSQL))
            {
                if (rs_Query.HasRows)
                {
                    frmHelp.pnlText.Visible = true;
                    frmHelp.pnlText.Text = "Record not available.";
                    return;
                }

                DeTools.gstrSQL = (rs_Query["Query"] + " ").Trim();
                if (string.IsNullOrEmpty(DeTools.gstrSQL))
                {
                    frmHelp.pnlText.Text = "Query not present in Help table.";
                    frmHelp.pnlInstructiontxt.Text = "Press Escape to exit from help.";
                    frmHelp.pnlText.Visible = true;
                    return;
                }

                switch (s_Mode)
                {
                    case DeTools.ADDMODE:
                        DeTools.gstrSQL += (rs_Query["A_Mode_Cond"] + " ").Trim();
                        break;

                    case DeTools.MODIFYMODE:
                        DeTools.gstrSQL += (rs_Query["M_Mode_Cond"] + " ").Trim();
                        break;

                    case DeTools.DELETEMODE:
                        DeTools.gstrSQL += (" " + (rs_Query["D_Mode_Cond"] + " ").Trim());
                        break;

                    case DeTools.INQUIREMODE:
                        DeTools.gstrSQL += (" " + (rs_Query["I_Mode_Cond"] + " ").Trim());
                        break;

                    case DeTools.POSTMODE:
                        DeTools.gstrSQL += (" " + (rs_Query["P_Mode_Cond"] + " ").Trim());
                        break;

                    default:
                        DeTools.gstrSQL += string.Empty;
                        break;
                }

                if (frmHelp.cboFields.SelectedItem == null)
                {
                    strFields = string.Empty;  // Assign the selected item to strFields
                }
                else
                {
                    string selectedField = frmHelp.cboFields.SelectedItem.ToString().Trim();
                    strFields = selectedField;  // Assign the selected item to strField
                }

                if (frmHelp.cboRel.SelectedItem == null)
                {
                    strCondition = string.Empty;
                }
                else
                {
                    string selectedField = frmHelp.cboRel.SelectedItem.ToString().Trim();
                    // Assign the selected item
                    strCondition = selectedField;
                }

                if (string.IsNullOrEmpty(frmHelp.txtValue.Text.Trim()))
                {
                    strValue = string.Empty;
                }
                else
                {
                    if (frmHelp.cboDataType.SelectedItem.ToString().Trim() == "1")
                    {
                        if (frmHelp.cboRel.SelectedItem.ToString().Trim() == "LIKE")
                        {
                            strValue = $"'%{frmHelp.txtValue.Text.Trim()}%'";
                        }
                        else if (frmHelp.cboRel.SelectedItem.ToString().Trim() == "=")
                        {
                            strValue = $"'{frmHelp.txtValue.Text.Trim()}'";
                        }
                    }
                    else if (frmHelp.cboDataType.SelectedItem.ToString().Trim() == "2")
                    {
                        strValue = double.Parse(frmHelp.txtValue.Text.Trim());
                    }
                    else if (frmHelp.cboDataType.SelectedItem.ToString().Trim() == "11")
                    {
                        if (!DateTime.TryParse(frmHelp.txtValue.Text.Trim(), out DateTime dateValue))
                        {
                            strValue = $"'{frmHelp.txtValue.Text.Trim().Substring(0, 4)}'";
                        }
                        else
                        {
                            strValue = $"'{frmHelp.txtValue.Text.Trim()}'";
                        }
                    }
                }

                if (frmHelp.cboOrder.SelectedItem == null)
                {
                    strOrderBy = (rs_Query["order_by"] + " ").Trim();
                }
                else
                {
                    strOrderBy = "ORDER BY " + frmHelp.cboOrderId.SelectedItem;
                }

                if (string.IsNullOrEmpty(strFields))
                {
                    strCondition = string.Empty;
                    strValue = string.Empty;
                }
                else if (string.IsNullOrEmpty(strCondition))
                {
                    strFields = string.Empty;
                    strValue = string.Empty;
                }
                else if (string.IsNullOrEmpty(strValue.ToString()))
                {
                    strFields = string.Empty;
                    strCondition = string.Empty;
                }

                if (!string.IsNullOrEmpty(frmHelp.txtValue.Text.Trim()) && frmHelp.cboDataType.SelectedItem == "11")
                {
                    if (!DateTime.TryParse(frmHelp.txtValue.Text.Trim(), out DateTime dateValue))
                    {
                        strCondition = "=";
                        strValue = "'01-JAN-1900'   ";
                    }
                }

                if (s_Mode == DeTools.INQUIREMODE)
                {
                    if (string.IsNullOrEmpty(rs_Query["I_Mode_Cond"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(strFields))
                        {
                            strFields = " WHERE " + strFields;
                        }
                        DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(strFields))
                        {
                            strFields = " WHERE " + strFields;
                        }
                        DeTools.gstrSQL = DeTools.gstrSQL;
                    }
                }
                if (s_Mode == DeTools.MODIFYMODE)
                {
                    if (!string.IsNullOrEmpty(strFields))
                    {
                        strFields = " AND " + strFields;
                    }
                    DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                }

                if (s_Mode == DeTools.ADDMODE)
                {
                    if (!string.IsNullOrEmpty(strFields))
                    {
                        strFields = " AND " + strFields;
                    }
                    DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                }

                if (s_Mode == DeTools.POSTMODE)
                {
                    if (!string.IsNullOrEmpty(strFields))
                    {
                        strFields = " AND " + strFields;
                    }
                    DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                }

                if (s_Mode == DeTools.DELETEMODE)
                {
                    if (!string.IsNullOrEmpty(strFields))
                    {
                        strFields = " AND " + strFields;
                    }
                    DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                }

                if (string.IsNullOrEmpty(s_Mode))
                {
                    if (!string.IsNullOrEmpty(strFields))
                    {
                        strFields = " WHERE " + strFields;
                    }
                    DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                }

                // Adjust the SQL query to limit the number of records
                DeTools.gstrSQL = DeTools.gstrSQL.Replace("SELECT", "SELECT TOP 2000");

                // Execute the query and retrieve results
                using (OdbcDataReader rs_Result = dbConnector.CreateResultset(DeTools.gstrSQL))
                {
                    if (!rs_Result.HasRows)
                    {
                        if (s_Mode == DeTools.MODIFYMODE || s_Mode == DeTools.DELETEMODE || s_Mode == DeTools.POSTMODE)
                        {
                            if (i_Help_id == 1101)
                            {
                                frmHelp.pnlText.Text = "No authorized record available in Employee Master.";
                            }
                            else
                            {
                                frmHelp.pnlText.Text = "No unauthorized record available.";
                            }
                        }
                        else
                        {
                            frmHelp.pnlText.Text = "No record available.";
                        }

                        for (Count = 0; Count < frmHelp.pnlToptxt.Text.Length; Count++)
                        {
                            if (frmHelp.pnlToptxt.Text[Count] == '(')
                            {
                                strheading = frmHelp.pnlToptxt.Text.Substring(0, Count);
                                break;
                            }
                        }

                        frmHelp.pnlToptxt.Text = strheading + " (0)";
                        frmHelp.pnlInstructiontxt.Text = "Press Escape to exit from help.";
                        frmHelp.pnlText.Visible = true;
                    }
                    else
                    {
                        // Get the number of rows (subtract one for column headings)
                        i_Rows = 1;
                        while (rs_Result.Read())
                        {
                            i_Rows++;
                        }
                        rs_Result.Close();

                        i_NoOfRecords = i_Rows - 1;

                        for (Count = 0; Count < frmHelp.pnlToptxt.Text.Length; Count++)
                        {
                            if (frmHelp.pnlToptxt.Text[Count] == '(')
                            {
                                strheading = frmHelp.pnlToptxt.Text.Substring(0, Count);
                                break;
                            }
                        }

                        // Get the number of columns from Help table
                        i_Cols = rs_Query.GetInt32("No_of_columns");

                        frmHelp.grdHelp.Rows.Clear(); // Clear any existing rows
                        for (int r = 0; r < i_Rows; r++)
                        {
                            frmHelp.grdHelp.Rows.Add(); // Add a new row
                        }

                        frmHelp.grdHelp.Columns.Clear(); // Clear any existing columns
                        for (int c = 0; c < i_Cols; c++)
                        {
                            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                            frmHelp.grdHelp.Columns.Add(column);
                        }

                        //// Prepare the header row
                        //frmHelp.grdHelp.Row = 0;
                        //frmHelp.grdHelp.RowHeight[0] = 450;
                        //for (j = 0; j < i_Cols; j++)
                        //{
                        //    frmHelp.grdHelp.Col = j;
                        //    strFieldName = "Caption" + (j + 1);
                        //    frmHelp.grdHelp.Text = rs_Query[strFieldName].ToString();
                        //    frmHelp.grdHelp.ColWidth[j] = 1200;
                        //    frmHelp.grdHelp.ColWidth[j] = rs_Result.GetFieldValue(j).ToString().Length * 90;
                        //}

                        // Prepare the header row
                        for (j = 0; j < i_Cols; j++)
                        {
                            // Assuming frmHelp.grdHelp is a DataGridView
                            DataGridViewColumn column = new DataGridViewColumn();
                            column.HeaderText = "Caption" + (j + 1);
                            column.Width = 1200; // Set your desired width here

                            frmHelp.grdHelp.Columns.Add(column);
                        }

                        // After adding columns, you can set the header row's height
                        frmHelp.grdHelp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                        frmHelp.grdHelp.ColumnHeadersHeight = 450;
                        frmHelp.grdHelp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;


                        frmHelp.pgbStatus.Minimum = 0;
                        frmHelp.pgbStatus.Maximum = i_Rows - 1;
                        frmHelp.Show();

                        frmHelp.pnlToptxt.Text = strheading + " (0)";

                        for (i = 1; i < i_Rows; i++)
                        {
                            frmHelp.pgbStatus.Value = i;
                            if (i % 50 == 0)
                            {
                                frmHelp.pnlToptxt.Text = strheading + " (" + i + ")";
                            }
                            // Add a new row to the DataGridView and populate its cells
                            frmHelp.grdHelp.Rows.Add();
                            for (j = 0; j < i_Cols; j++)
                            {
                                // frmHelp.grdHelp.Rows[i - 1].Cells[j].Value = rs_Result.GetFieldValue(j).ToString();
                                frmHelp.grdHelp.Rows[i - 1].Cells[j].Value = rs_Result.GetFieldValue<string>(j);
                            }
                        }

                        frmHelp.pnlToptxt.Text = strheading + " (" + i_NoOfRecords + ")";
                        //frmHelp.grdHelp.Col = 0;
                        //frmHelp.grdHelp.Row = 1;
                        frmHelp.grdHelp.CurrentCell = frmHelp.grdHelp[0, 1];
                        frmHelp.grdHelp.Visible = true;

                        // Enable filter menus based on the number of rows
                        if (frmHelp.grdHelp.RowCount > 101)
                        {
                            //frmHelp.mnuLast_100.Enabled = true;
                            //frmHelp.mnuLast_50.Enabled = true;
                            //frmHelp.mnuLast_15.Enabled = true;
                        }
                        else if (frmHelp.grdHelp.RowCount > 51)
                        {
                            //frmHelp.mnuLast_50.Enabled = true;
                            //frmHelp.mnuLast_15.Enabled = true;
                        }
                        else if (frmHelp.grdHelp.RowCount > 16)
                        {
                          //  frmHelp.mnuLast_15.Enabled = true;
                        }

                        frmHelp.pnlInstructiontxt.Text = "Double click or press Enter to select.";
                    }
                }
            }
        }

        public static void PrepareTextHelp()
        {
            dbConnector= new DbConnector();

            dbConnector.OpenConnection();

            ///Select the help string from the Help table
            DeTools.gstrSQL = "Select Text_help, Text_help_caption from Help ";
            DeTools.gstrSQL = DeTools.gstrSQL + "where Help_id = " + "'"+i_Help_id+"';";
            using(OdbcDataReader reader= dbConnector.CreateResultset(DeTools.gstrSQL))
            {
                if (!reader.HasRows)
                {
                    frmHelp.pnlText.Text = "No entry available in the table.";
                }
                else
                {
                    string textHelp = reader["Text_help"].ToString();
                    frmHelp.pnlText.Text = string.IsNullOrEmpty(textHelp) ? "No entry available in the table." : textHelp;
                }
            }
            frmHelp.pnlInstructiontxt.Text = "Press Escape to exit from help.";
            frmHelp.pnlText.Visible = true;

        }



    }///////////////////////END////////


}
