//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;



namespace softgen
{
    public partial class frmM_Item : Form, Interface_for_Common_methods.ISearchableForm
    {
        public DbConnector dbConnector;
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn, chkItemid;
        public bool mblnSearch, mblnDataEntered;
        private OdbcTransaction transaction;
        public string strMode;
        private int chkItemsn;
        Messages msg = new Messages();
        General general = new General();
        // Define the list of where clause variables and values
        private Dictionary<int, string> pluValues = new Dictionary<int, string>();
        public int pluCounter = 0; // Counter to track the auto-generated PLU number
        bool saveflag=true;
        // Assuming existingData is a DataTable declared at the class level
        public DataTable existingData;
        public frmM_Item()
        {
            InitializeComponent();

            dbConnector = new DbConnector();
            ComboBoxDataLoader.SetDbConnector(dbConnector);


            this.Activated += MyForm_Activated;
            dbgBarDet.CellValidating += dbgBarDet_CellValidating;
            dbgBarDet.KeyPress += dbgBarDet_KeyPress;

        }

        private void MyForm_Activated(object sender, EventArgs e)
        {
            DeTools.ClearStatusBarHelp();
            DeTools.ActiveFileMenu(this);
            DeTools.CreatedBy(mstrEntBy, mstrEntOn);
            DeTools.PostedBy(mstrAuthBy, mstrAuthOn);



        }

        public bool GetDEStatus()
        {
            return mblnDataEntered == true ? true : false;
        }

        public void ClearForm()
        {
            int i, j;
            strMode = DeTools.GetMode(this);
            //mblnDataEntered = false;
            //mstrEntBy = null;
            //mstrEntOn = null;
            //mstrAuthBy = null;
            //mstrAuthOn = null;
            //ClearItemGrid();

            if (strMode == DeTools.ADDMODE)
            {
                if (chkItemid == "Y")
                {
                    txtItemId.Enabled = false;
                    txtItemDesc.Enabled = true;
                    txtItemDesc.Focus();
                }
                else
                {
                    txtItemId.Enabled = true;
                    txtItemId.Focus();
                }
            }
            else
            {
                txtItemId.Enabled = true;
                txtItemId.Focus();
            }


        }

        private void ClearItemGrid()
        {
            try
            {
                int I, J;
                dbgBarDet.EndEdit();
                dbgBarDet.Update();
                //dbgBarDet.Rows.Clear();
                //dbgBarDet.Columns.Clear();

                dbgBarDet.DataSource = null;
                dbgBarDet.Rows.Clear();
            }
            catch (Exception ex)
            {
                // Handle exception if necessary
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckMandatoryFields()
        {
            try
            {
                if (txtItemId.Text.Length == 0)
                {
                    Messages.InfoMsg("Item Id");
                    txtItemId.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Messages msg = new Messages();
                msg.VBError(ex, this.Name, "SaveForm", "CheckMandatoryFields");
                return false;
            }
        }

        public void SaveForm()
        {
            try
            {

         
                dbConnector = new DbConnector();
            // dbConnector.connectionString= new OdbcConnection();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

            saveflag = true;

            bool blnItem_H, blnItem_D;
            int J;

            dbgBarDet.Update();
            //transaction = dbConnector.connection.BeginTransaction();

           
            if (mblnSearch == true)
            {
                if (!CheckMandatoryFields())
                {
                    saveflag = false;
                }

                else
                {
        
                    blnItem_H = true;
                    DeTools.gstrSQL = "select a.*,b.* from m_item_det a join m_item_hdr b on a.item_id=b.item_id and  a.item_id = '" + txtItemId.Text.Trim() + "' and b.item_id ='" + txtItemId.Text.Trim() + "'  limit 1;  ";
                    OdbcCommand cmd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);
                    dbConnector.connection.Open();
                    
                        OdbcDataReader reader = cmd.ExecuteReader();

                        // Check if the record with the specified Group_id exists
                        if (DeTools.GetMode(this) != DeTools.ADDMODE)
                        {
                            if (reader.HasRows)
                            {

                                if (DeTools.CheckTemporaryTableExists("m_item_hdr") != null)
                                {
                                    if (DeTools.CheckTemporaryTableExists("m_item_det") != null)
                                    {

                                        // The record exists, so update it
                                        reader.Close();
                                        Cursor.Current = Cursors.WaitCursor;

                                        string gstrSQL1 = "INSERT INTO temp_m_item_hdr (item_id, item_desc, short_desc, item_type_id, group_id, sub_group_id, sub_sub_group_id,size_id, color_id, style, manuf_id, manuf_name, pur_unit_id, sale_unit_id, conv_pur_sale, op_bal_unit, min_level, re_order_level, max_level, qty_decimal_yn, decimal_upto, sale_tax_paid, cost_price, mrp,     sale_price, bar_yn, active_yn, status, ent_on, ent_by, Trans_status, loc_id, lt, net_rate, disc_per," +
                                                 "HSN_CODE, cess_perc, excis_perc, local_rate_yn, bar_code, disc_yn, mod_date, mod_by,closed_yn,comp_name) SELECT item_id, item_desc, short_desc, item_type_id, group_id, sub_group_id, sub_sub_group_id,size_id, color_id, style, manuf_id, manuf_name, pur_unit_id, sale_unit_id, conv_pur_sale, op_bal_unit, min_level, re_order_level, max_level, qty_decimal_yn, decimal_upto, sale_tax_paid, cost_price, mrp, sale_price, bar_yn, active_yn, status, ent_on, ent_by, Trans_status, loc_id, lt, net_rate, disc_per," +
                                                 "HSN_CODE, cess_perc, excis_perc, local_rate_yn, bar_code, disc_yn, mod_date, mod_by, 'Y' AS closed_yn,  '" + DeTools.fOSMachineName.GetMachineName() + "' AS comp_name FROM m_item_hdr WHERE item_id = '" + txtItemId.Text.Trim() + "'; ";

                                        using (OdbcCommand insertintemp1 = new OdbcCommand(gstrSQL1, dbConnector.connection))
                                        {
                                            insertintemp1.ExecuteNonQuery();
                                        }

                                        string gstrSQL2 = "Select * from temp_m_item_hdr where item_id='" + txtItemId.Text.Trim() + "' and closed_yn='Y'";
                                        OdbcCommand selectintemp1 = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);

                                        OdbcDataReader selectread = selectintemp1.ExecuteReader();

                                        if (selectread.HasRows)
                                        {
                                            string delSQL = "Delete FROM m_item_hdr WHERE item_id = '" + txtItemId.Text.Trim() + "'; ";

                                            using (OdbcCommand delfrmhdr1 = new OdbcCommand(delSQL, dbConnector.connection))
                                            {
                                                delfrmhdr1.ExecuteNonQuery();
                                            }


                                            DeTools.gstrSQL = "UPDATE temp_m_item_hdr SET item_desc = ?, short_desc = ?, item_type_id = ?, group_id = ?, sub_group_id = ?, sub_sub_group_id = ?, size_id = ?, color_id = ?, style = ?, manuf_id = ?, manuf_name = ?," +
                                                " pur_unit_id = ?, sale_unit_id = ?, conv_pur_sale = ?, op_bal_unit = ?, min_level = ?, re_order_level = ?, max_level = ?, qty_decimal_yn = ?, decimal_upto = ?, sale_tax_paid = ?, cost_price = ?, mrp = ?," +
                                                " sale_price = ?, bar_yn = ?, active_yn = ?, status = ?, Trans_status = ?, loc_id = ?, lt = ?, net_rate = ?, disc_per = ?, HSN_CODE = ?, cess_perc = ?, excis_perc = ?, local_rate_yn = ?," +
                                                " bar_code = ?, disc_yn = ?, mod_date = ?, mod_by = ?  WHERE item_id = '" + txtItemId.Text.Trim() + "'; ";

                                            cmd.CommandText = DeTools.gstrSQL;

                                            cmd.Parameters.Add(new OdbcParameter("item_desc", txtItemDesc.Text.Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("short_desc", txtShDesc.Text.Trim()));
                                            string type = cboType.SelectedItem != null ? cboType.SelectedItem.ToString().Trim() : string.Empty;
                                            cmd.Parameters.Add(new OdbcParameter("item_type_id", type));

                                            string grp = cboGroup.SelectedItem != null ? cboGroup.SelectedItem.ToString().Trim() : string.Empty;
                                            cmd.Parameters.Add(new OdbcParameter("group_id", grp));

                                            string sgrp = cboSGroup.SelectedItem != null ? cboSGroup.SelectedItem.ToString().Trim() : string.Empty;
                                            cmd.Parameters.Add(new OdbcParameter("sub_group_id", sgrp));

                                            string ssgrp = cboSSGroupDesc.SelectedItem != null ? cboSSGroupDesc.SelectedItem.ToString().Trim() : string.Empty;
                                            cmd.Parameters.Add(new OdbcParameter("sub_sub_group_id", ssgrp));

                                            string size = cboSizeDesc.SelectedItem != null ? cboSizeDesc.SelectedItem.ToString().Trim() : string.Empty;
                                            cmd.Parameters.Add(new OdbcParameter("size_id", size));

                                            string color = cboColorDesc.SelectedItem != null ? cboColorDesc.SelectedItem.ToString().Trim() : string.Empty;
                                            cmd.Parameters.Add(new OdbcParameter("color_id", color));

                                            cmd.Parameters.Add(new OdbcParameter("style", txtStyle.Text.ToString().Trim()));

                                            string manuf = cboManuf.SelectedItem != null ? cboManuf.SelectedItem.ToString().Trim() : string.Empty;
                                            cmd.Parameters.Add(new OdbcParameter("manuf_id", manuf));
                                            cmd.Parameters.Add(new OdbcParameter("manuf_name", rotManuf.Text.ToString().Trim()));

                                            string purunit = cboPurUnit.SelectedItem != null ? cboPurUnit.SelectedItem.ToString().Trim() : string.Empty;
                                            cmd.Parameters.Add(new OdbcParameter("pur_unit_id", purunit));

                                            string salunit = cboSaleUnit.SelectedItem != null ? cboSaleUnit.SelectedItem.ToString().Trim() : string.Empty;
                                            cmd.Parameters.Add(new OdbcParameter("sale_unit_id", salunit));

                                            cmd.Parameters.Add(new OdbcParameter("conv_pur_sale", txtConv.Text.ToString().Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("op_bal_unit", txtOpBal.Text.ToString().Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("min_level", txtMinLevel.Text.ToString().Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("re_order_level", txtReOLevel.Text.ToString().Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("max_level", txtMaxLevel.Text.ToString().Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("qty_decimal_yn", chkDecimal.Checked ? "Y" : "N"));
                                            cmd.Parameters.Add(new OdbcParameter("decimal_upto", txtDecimalupto.Text.ToString().Trim()));

                                            string sltax = cboSaleTax.SelectedItem != null ? cboSaleTax.SelectedItem.ToString().Trim() : string.Empty;
                                            cmd.Parameters.Add(new OdbcParameter("sale_tax_paid", sltax));

                                            cmd.Parameters.Add(new OdbcParameter("cost_price", dbgBarDet.Rows[0].Cells[2].Value.ToString().Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("mrp", dbgBarDet.Rows[0].Cells[3].Value.ToString().Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("sale_price", dbgBarDet.Rows[0].Cells[4].Value.ToString().Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("bar_yn", chkBarYN.Checked ? "Y" : "N"));
                                            cmd.Parameters.Add(new OdbcParameter("active_yn", chkAct.Checked ? "Y" : "N"));
                                            cmd.Parameters.Add(new OdbcParameter("status", "V"));
                                            cmd.Parameters.Add(new OdbcParameter("Trans_status", "N"));

                                            string loc = cboLoc.SelectedItem != null ? cboLoc.SelectedItem.ToString().Trim() : string.Empty;
                                            cmd.Parameters.Add(new OdbcParameter("loc_id", loc));

                                            cmd.Parameters.Add(new OdbcParameter("lt", txtLT.ToString().Trim()));// Required frequency
                                            cmd.Parameters.Add(new OdbcParameter("net_rate", dbgBarDet.Rows[0].Cells[5].Value.ToString().Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("disc_per", txtDisc.Text.ToString().Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("HSN_CODE", txtHSN.Text.ToString().Trim()));

                                            string cessValue = string.IsNullOrEmpty(txtCess.Text) ? "0.00" : txtCess.Text.ToString().Trim();
                                            string excisperc = string.IsNullOrEmpty(txtexisper.Text) ? "0.00" : txtexisper.Text.ToString().Trim();

                                            cmd.Parameters.Add(new OdbcParameter("cess_perc", cessValue));
                                            cmd.Parameters.Add(new OdbcParameter("excis_perc", excisperc));
                                            cmd.Parameters.Add(new OdbcParameter("local_rate_yn", "N")); //todo -its for rate can't be change when there will be transfer in.
                                            cmd.Parameters.Add(new OdbcParameter("bar_code", dbgBarDet.Rows[0].Cells[1].Value.ToString().Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("disc_yn", chkNodisc.Checked ? "N" : "Y"));
                                            
                                            cmd.Parameters.Add(new OdbcParameter("mod_date", OdbcType.DateTime)).Value = DateTime.Now;
                                            cmd.Parameters.Add(new OdbcParameter("mod_by", DeTools.gstrloginId));


                                            cmd.ExecuteNonQuery();

                                            Cursor.Current = Cursors.Default;

                                            Messages.SavingMsg();


                                            //DeTools.SelectDataFromTemporaryTable("m_group");
                                            reader.Close();


                                            string insertQuery = "INSERT INTO m_item_hdr (item_id, item_desc, short_desc, item_type_id, group_id, sub_group_id, sub_sub_group_id,size_id, color_id, style, manuf_id, manuf_name, pur_unit_id, sale_unit_id, conv_pur_sale, op_bal_unit, min_level, re_order_level, max_level, qty_decimal_yn, decimal_upto, sale_tax_paid, cost_price, mrp,     sale_price, bar_yn, active_yn, status, ent_on, ent_by, Trans_status, loc_id, lt, net_rate, disc_per," +
                                                "HSN_CODE, cess_perc, excis_perc, local_rate_yn, bar_code, disc_yn, mod_date, mod_by) SELECT item_id, item_desc, short_desc, item_type_id, group_id, sub_group_id, sub_sub_group_id,size_id, color_id, style, manuf_id, manuf_name, pur_unit_id, sale_unit_id, conv_pur_sale, op_bal_unit, min_level, re_order_level, max_level, qty_decimal_yn, decimal_upto, sale_tax_paid, cost_price, mrp,     sale_price, bar_yn, active_yn, status, ent_on, ent_by, Trans_status, loc_id, lt, net_rate, disc_per," +
                                                "HSN_CODE, cess_perc, excis_perc, local_rate_yn, bar_code, disc_yn, mod_date, mod_by FROM temp_m_item_hdr WHERE item_id = ?";


                                            using (OdbcCommand insertCmd = new OdbcCommand(insertQuery, dbConnector.connection))
                                            {
                                                insertCmd.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.ToString().Trim()));
                                                insertCmd.ExecuteNonQuery();


                                            }
                                            string querupdN1 = "update temp_m_item_hdr set closed_yn='N' where item_id=? order by ent_on desc ";

                                            using (OdbcCommand querupdNCmd = new OdbcCommand(querupdN1, dbConnector.connection))
                                            {
                                                querupdNCmd.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.ToString().Trim()));
                                                querupdNCmd.ExecuteNonQuery();


                                            }

                                            string querdel1 = "delete from temp_m_item_hdr where item_id=? order by ent_on desc ";

                                            using (OdbcCommand querdelCmd = new OdbcCommand(querdel1, dbConnector.connection))
                                            {
                                                querdelCmd.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.ToString().Trim()));
                                                querdelCmd.ExecuteNonQuery();


                                            }



                                        }
                                        Cursor.Current = Cursors.WaitCursor;

                                        //-----------------------updation for det starts----------------//

                                        // Step 1: Select specific columns for the given item_id
                                        string selectQuery = "SELECT plu, bar_code, cost_price, mrp, net_rate FROM m_item_det WHERE item_id = ?";
                                        using (OdbcCommand selectCmd = new OdbcCommand(selectQuery, dbConnector.connection))
                                        {
                                            selectCmd.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.Trim()));

                                            using (OdbcDataReader readerdbg = selectCmd.ExecuteReader())
                                            {
                                                existingData = new DataTable();
                                                existingData.Columns.Add("plu", typeof(string));
                                                existingData.Columns.Add("bar_code", typeof(string));
                                                existingData.Columns.Add("cost_price", typeof(decimal));
                                                existingData.Columns.Add("mrp", typeof(decimal));
                                                existingData.Columns.Add("net_rate", typeof(decimal));

                                                while (readerdbg.Read())
                                                {
                                                    DataRow row = existingData.NewRow();
                                                    row["plu"] = readerdbg["plu"].ToString();
                                                    row["bar_code"] = readerdbg["bar_code"].ToString();
                                                    row["cost_price"] = readerdbg["cost_price"] is DBNull ? 0 : Convert.ToDecimal(readerdbg["cost_price"]);
                                                    row["mrp"] = readerdbg["mrp"] is DBNull ? 0 : Convert.ToDecimal(readerdbg["mrp"]);
                                                    row["net_rate"] = readerdbg["net_rate"] is DBNull ? 0 : Convert.ToDecimal(readerdbg["net_rate"]);
                                                    existingData.Rows.Add(row);
                                                }
                                            }
                                        }

                                        // Step 2: Compare existing data with the DataGridView (dbgBarDet)
                                        foreach (DataGridViewRow row in dbgBarDet.Rows)
                                        {
                                            if (row.Cells[0].Value != null && row.Cells[2].Value != null)
                                            {

                                            
                                                string plu = row.Cells[0].Value.ToString();
                                                string barCode = row.Cells[1].Value.ToString();
                                                decimal costPrice = Convert.ToDecimal(row.Cells[2].Value);
                                                decimal mrp = Convert.ToDecimal(row.Cells[3].Value);
                                                decimal netRate = Convert.ToDecimal(row.Cells[4].Value);

                                                DataRow[] existingRows = existingData.Select($"plu = '{plu}'");

                                                if (existingRows.Length > 0)
                                                {
                                                    DataRow existingRow = existingRows[0];

                                                    if (existingRow["bar_code"].ToString() != barCode ||
                                                        (decimal)existingRow["cost_price"] != costPrice ||
                                                        (decimal)existingRow["mrp"] != mrp ||
                                                        (decimal)existingRow["net_rate"] != netRate)
                                                    {
                                                        existingRow["bar_code"] = barCode;
                                                        existingRow["cost_price"] = costPrice;
                                                        existingRow["mrp"] = mrp;
                                                        existingRow["net_rate"] = netRate;

                                                        // Perform the update in the database using your UPDATE query
                                                        // Perform the update in the database using your UPDATE query

                                                        //--------------------- then insert into temp det table----------//

                                                        string insertQuerytempdet = "INSERT INTO temp_m_item_det (item_id, item_desc, bar_code, plu, cost_price, mrp,sale_price, active_yn, Trans_status, net_rate, local_rate_yn, closed_yn, comp_name) SELECT item_id, item_desc, bar_code,plu, cost_price, mrp, sale_price, active_yn, Trans_status, net_rate, local_rate_yn, 'Y' AS closed_yn, '" + DeTools.fOSMachineName.GetMachineName() + "' AS comp_name FROM m_item_det WHERE item_id = ?";

                                                        using (OdbcCommand insertQuerytempdetCmd1 = new OdbcCommand(insertQuerytempdet, dbConnector.connection))
                                                        {
                                                            insertQuerytempdetCmd1.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.ToString().Trim()));
                                                            insertQuerytempdetCmd1.ExecuteNonQuery();
                                                        }

                                                        //--------for a check that 
                                                        string selfrmtempdet = "Select * from temp_m_item_det where item_id='" + txtItemId.Text.Trim() + "' and closed_yn='Y'";
                                                        OdbcCommand selfrmtempdetcmd = new OdbcCommand(selfrmtempdet, dbConnector.connection);

                                                        OdbcDataReader selfrmtempdetread = selfrmtempdetcmd.ExecuteReader();

                                                        if (selfrmtempdetread.HasRows)
                                                        {


                                                            string updateQuery = "UPDATE temp_m_item_det " +
                                                                                 "SET bar_code = ?, cost_price = ?, mrp = ?, net_rate = ? " +
                                                                                 "WHERE item_id = '" + txtItemId.Text.Trim() + "'";

                                                            using (OdbcCommand updateCmd = new OdbcCommand(updateQuery, dbConnector.connection))
                                                            {
                                                                updateCmd.Parameters.Add(new OdbcParameter("bar_code", barCode));
                                                                updateCmd.Parameters.Add(new OdbcParameter("cost_price", costPrice));
                                                                updateCmd.Parameters.Add(new OdbcParameter("mrp", mrp));
                                                                updateCmd.Parameters.Add(new OdbcParameter("net_rate", netRate));
                                                                updateCmd.Parameters.Add(new OdbcParameter("plu", plu));

                                                                updateCmd.ExecuteNonQuery();
                                                            }

                                                            selfrmtempdetread.Close();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // Step 3: Add new rows
                                                        DataRow newRow = existingData.NewRow();
                                                        newRow["plu"] = plu;
                                                        newRow["bar_code"] = barCode;
                                                        newRow["cost_price"] = costPrice;
                                                        newRow["mrp"] = mrp;
                                                        newRow["net_rate"] = netRate;

                                                        existingData.Rows.Add(newRow);

                                                        // Execute your INSERT query for new rows
                                                        foreach (DataRow row1 in existingData.Rows)
                                                        {

                                                            string insert = "Insert into temp_m_item_det (plu,bar_code,cost_price,mrp,net_rate) values (?,?,?,?,?) where item_id='" + txtItemId.Text.Trim() + "'";
                                                            using (OdbcCommand insertCmd = new OdbcCommand(insert, dbConnector.connection))
                                                            {
                                                                // Add parameters for your INSERT query
                                                                insertCmd.Parameters.Add(new OdbcParameter("plu", row1["plu"].ToString()));
                                                                insertCmd.Parameters.Add(new OdbcParameter("bar_code", row1["bar_code"].ToString()));
                                                                insertCmd.Parameters.Add(new OdbcParameter("cost_price", row1["cost_price"]));
                                                                insertCmd.Parameters.Add(new OdbcParameter("mrp", row1["mrp"]));
                                                                insertCmd.Parameters.Add(new OdbcParameter("net_rate", row1["net_rate"]));

                                                                // Execute the INSERT query
                                                                insertCmd.ExecuteNonQuery();



                                                            }

                                                        }

                                                    }
                                                }

                                            }

                                        }


                                        string selfrmtempdet2 = "Select * from temp_m_item_det where item_id='" + txtItemId.Text.Trim() + "' and closed_yn='Y'";
                                        OdbcCommand selfrmtempdetcmd2 = new OdbcCommand(selfrmtempdet2, dbConnector.connection);

                                        OdbcDataReader selfrmtempdetread2 = selfrmtempdetcmd2.ExecuteReader();

                                        //-------------updating rest columns other than dbgbardet--//


                                        if (selfrmtempdetread2.HasRows)
                                        {
                                        selfrmtempdetread2.Close();
                                            
                                            DeTools.gstrSQL = "UPDATE temp_m_item_det SET item_desc = ?, active_yn = ?, Trans_status = ?, local_rate_yn = ?, mod_date = ?, mod_by = ? WHERE item_id = '" + txtItemId.Text.Trim() + "'; ";

                                            selfrmtempdetcmd2.CommandText = DeTools.gstrSQL;

                                            selfrmtempdetcmd2.Parameters.Add(new OdbcParameter("item_desc", txtItemDesc.Text.Trim()));

                                            selfrmtempdetcmd2.Parameters.Add(new OdbcParameter("active_yn", chkAct.Checked ? "Y" : "N"));
                                            selfrmtempdetcmd2.Parameters.Add(new OdbcParameter("Trans_status", "N"));
                                            selfrmtempdetcmd2.Parameters.Add(new OdbcParameter("local_rate_yn", "N")); //todo -its for rate can't be change when there will be transfer in.
                                            selfrmtempdetcmd2.Parameters.Add(new OdbcParameter("mod_date", OdbcType.DateTime)).Value = DateTime.Now;
                                            selfrmtempdetcmd2.Parameters.Add(new OdbcParameter("mod_by", DeTools.gstrloginId));


                                            selfrmtempdetcmd2.ExecuteNonQuery();
                                        }

                                        //-------------After updation complete insert into Main det tbl-------------//


                                        string delSQLdet = "Delete FROM m_item_det WHERE item_id = '" + txtItemId.Text.Trim() + "'; ";

                                        using (OdbcCommand delfrmdet1 = new OdbcCommand(delSQLdet, dbConnector.connection))
                                        {
                                            delfrmdet1.ExecuteNonQuery();
                                        }

                                        string insertQuerydet1 = "INSERT INTO m_item_det (item_id, item_desc, bar_code, plu, cost_price, mrp,sale_price, active_yn, Trans_status, net_rate, local_rate_yn, mod_date, mod_by) SELECT item_id, item_desc, bar_code,plu, cost_price, mrp, sale_price, active_yn, Trans_status, net_rate, local_rate_yn, mod_date, mod_by FROM temp_m_item_det WHERE item_id = ?";

                                        using (OdbcCommand insertCmd2 = new OdbcCommand(insertQuerydet1, dbConnector.connection))
                                        {
                                            insertCmd2.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.ToString().Trim()));
                                            insertCmd2.ExecuteNonQuery();
                                        }

                                        //---------for update N in temp det-----------------------------//

                                        string UPDATEQuerydet1 = "Update temp_m_item_det set closed_yn='N' WHERE item_id = ?";

                                        using (OdbcCommand UPDATEQuerydetcmd1 = new OdbcCommand(UPDATEQuerydet1, dbConnector.connection))
                                        {
                                            UPDATEQuerydetcmd1.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.ToString().Trim()));
                                            UPDATEQuerydetcmd1.ExecuteNonQuery();
                                        }

                                        //-------- for delete data from the temp tbl after insert in det----------------//
                                        string DelQuerydet1 = "delete from temp_m_item_det WHERE item_id = ? and closed_yn='N'";

                                        using (OdbcCommand DelQuerydetcmd1 = new OdbcCommand(DelQuerydet1, dbConnector.connection))
                                        {
                                            DelQuerydetcmd1.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.ToString().Trim()));
                                            DelQuerydetcmd1.ExecuteNonQuery();
                                        }



                                    }
                                }

                            }
                        }//--------------over code for updation now add//

                        else
                        {
                            // The record does not exist, so insert a new one
                            reader.Close();
                            if (chkItemid == "Y" && strMode != string.Empty && saveflag == true)
                            {
                                txtItemId.Text = General.GenMDocno("ITSN").ToString().Trim();
                                if (txtItemId.Text.Length == 0)
                                {
                                    txtItemId.Text = "";
                                    string gstrMsg = "Document series for Item Serial Generation. exhausted or not available. Item cannot be saved.";
                                    Messages.ErrorMsg(gstrMsg);
                                    saveflag = false;
                                }

                            }
                            if (DeTools.CheckTemporaryTableExists("m_item_hdr") != null)
                            {
                                if (DeTools.CheckTemporaryTableExists("m_item_det") != null)
                                {
                                    Cursor.Current = Cursors.WaitCursor;

                                    string insert = "INSERT INTO temp_m_item_hdr (item_id, item_desc, short_desc, item_type_id, group_id, sub_group_id, sub_sub_group_id,size_id, color_id, style, manuf_id, manuf_name, pur_unit_id, sale_unit_id, conv_pur_sale, op_bal_unit, min_level, re_order_level, max_level, qty_decimal_yn, decimal_upto, sale_tax_paid, cost_price, mrp,     sale_price, bar_yn, active_yn, status, ent_on, ent_by, Trans_status, loc_id, lt, net_rate, disc_per,    HSN_CODE, cess_perc, excis_perc, local_rate_yn, bar_code, disc_yn, closed_yn, comp_name) VALUES" +
                                            " (?, ?, ?, ?, ?, ?, ?, ?, ?, ?," +
                                            " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?," +
                                            " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                                    OdbcCommand cmdd = new OdbcCommand(insert, dbConnector.connection);
                                    // cmd.Transaction = transaction;



                                    cmdd.CommandText = insert;

                                    cmdd.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("item_desc", txtItemDesc.Text.Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("short_desc", txtShDesc.Text.Trim()));
                                    string type = cboType.SelectedItem != null ? cboType.SelectedItem.ToString().Trim() : string.Empty;
                                    cmdd.Parameters.Add(new OdbcParameter("item_type_id", type));

                                    string grp = cboGroup.SelectedItem != null ? cboGroup.SelectedItem.ToString().Trim() : string.Empty;
                                    cmdd.Parameters.Add(new OdbcParameter("group_id", grp));

                                    string sgrp = cboSGroup.SelectedItem != null ? cboSGroup.SelectedItem.ToString().Trim() : string.Empty;
                                    cmdd.Parameters.Add(new OdbcParameter("sub_group_id", sgrp));

                                    string ssgrp = cboSSGroupDesc.SelectedItem != null ? cboSSGroupDesc.SelectedItem.ToString().Trim() : string.Empty;
                                    cmdd.Parameters.Add(new OdbcParameter("sub_sub_group_id", ssgrp));

                                    string size = cboSizeDesc.SelectedItem != null ? cboSizeDesc.SelectedItem.ToString().Trim() : string.Empty;
                                    cmdd.Parameters.Add(new OdbcParameter("size_id", size));

                                    string color = cboColorDesc.SelectedItem != null ? cboColorDesc.SelectedItem.ToString().Trim() : string.Empty;
                                    cmdd.Parameters.Add(new OdbcParameter("color_id", color));

                                    cmdd.Parameters.Add(new OdbcParameter("style", txtStyle.Text.ToString().Trim()));

                                    string manuf = cboManuf.SelectedItem != null ? cboManuf.SelectedItem.ToString().Trim() : string.Empty;
                                    cmdd.Parameters.Add(new OdbcParameter("manuf_id", manuf));
                                    cmdd.Parameters.Add(new OdbcParameter("manuf_name", rotManuf.Text.ToString().Trim()));

                                    string purunit = cboPurUnit.SelectedItem != null ? cboPurUnit.SelectedItem.ToString().Trim() : string.Empty;
                                    cmdd.Parameters.Add(new OdbcParameter("pur_unit_id", purunit));

                                    string salunit = cboSaleUnit.SelectedItem != null ? cboSaleUnit.SelectedItem.ToString().Trim() : string.Empty;
                                    cmdd.Parameters.Add(new OdbcParameter("sale_unit_id", salunit));

                                    cmdd.Parameters.Add(new OdbcParameter("conv_pur_sale", txtConv.Text.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("op_bal_unit", txtOpBal.Text.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("min_level", txtMinLevel.Text.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("re_order_level", txtReOLevel.Text.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("max_level", txtMaxLevel.Text.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("qty_decimal_yn", chkDecimal.Checked ? "Y" : "N"));
                                    cmdd.Parameters.Add(new OdbcParameter("decimal_upto", txtDecimalupto.Text.ToString().Trim()));

                                    string sltax = cboSaleTax.SelectedItem != null ? cboSaleTax.SelectedItem.ToString().Trim() : string.Empty;
                                    cmdd.Parameters.Add(new OdbcParameter("sale_tax_paid", sltax));

                                    cmdd.Parameters.Add(new OdbcParameter("cost_price", dbgBarDet.Rows[0].Cells[2].Value.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("mrp", dbgBarDet.Rows[0].Cells[3].Value.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("sale_price", dbgBarDet.Rows[0].Cells[4].Value.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("bar_yn", chkBarYN.Checked ? "Y" : "N"));
                                    cmdd.Parameters.Add(new OdbcParameter("active_yn", chkAct.Checked ? "Y" : "N"));
                                    cmdd.Parameters.Add(new OdbcParameter("status", "V"));
                                    cmdd.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;
                                    cmdd.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                                    cmdd.Parameters.Add(new OdbcParameter("Trans_status", "N"));

                                    string loc = cboLoc.SelectedItem != null ? cboLoc.SelectedItem.ToString().Trim() : string.Empty;
                                    cmdd.Parameters.Add(new OdbcParameter("loc_id", loc));

                                    cmdd.Parameters.Add(new OdbcParameter("lt", txtLT.ToString().Trim()));// Required frequency
                                    cmdd.Parameters.Add(new OdbcParameter("net_rate", dbgBarDet.Rows[0].Cells[5].Value.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("disc_per", txtDisc.Text.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("HSN_CODE", txtHSN.Text.ToString().Trim()));

                                    string cessValue = string.IsNullOrEmpty(txtCess.Text) ? "0.00" : txtCess.Text.ToString().Trim();
                                    string excisperc = string.IsNullOrEmpty(txtexisper.Text) ? "0.00" : txtexisper.Text.ToString().Trim();

                                    cmdd.Parameters.Add(new OdbcParameter("cess_perc", cessValue));
                                    cmdd.Parameters.Add(new OdbcParameter("excis_perc", excisperc));
                                    cmdd.Parameters.Add(new OdbcParameter("local_rate_yn", "N")); //todo -its for rate can't be change when there will be transfer in.
                                    cmdd.Parameters.Add(new OdbcParameter("bar_code", dbgBarDet.Rows[0].Cells[1].Value.ToString().Trim()));
                                    cmdd.Parameters.Add(new OdbcParameter("disc_yn", chkNodisc.Checked ? "N" : "Y"));
                                    cmdd.Parameters.Add(new OdbcParameter("closed_yn", "Y"));
                                    cmdd.Parameters.Add(new OdbcParameter("comp_name", DeTools.fOSMachineName.GetMachineName()));


                                    cmdd.ExecuteNonQuery();
                                    //transaction.Commit();

                                    //DeTools.SelectDataFromTemporaryTable("m_group");
                                    reader.Close();

                                    // Specify the columns explicitly in the SELECT statement
                                    string insertQuery = "INSERT INTO m_item_hdr (item_id, item_desc, short_desc, item_type_id, group_id, sub_group_id, sub_sub_group_id,size_id, color_id, style, manuf_id, manuf_name, pur_unit_id, sale_unit_id, conv_pur_sale, op_bal_unit, min_level, re_order_level, max_level, qty_decimal_yn, decimal_upto, sale_tax_paid, cost_price, mrp,     sale_price, bar_yn, active_yn, status, ent_on, ent_by, Trans_status, loc_id, lt, net_rate, disc_per," +
                                        "HSN_CODE, cess_perc, excis_perc, local_rate_yn, bar_code, disc_yn) SELECT item_id, item_desc, short_desc, item_type_id, group_id, sub_group_id, sub_sub_group_id,size_id, color_id, style, manuf_id, manuf_name, pur_unit_id, sale_unit_id, conv_pur_sale, op_bal_unit, min_level, re_order_level, max_level, qty_decimal_yn, decimal_upto, sale_tax_paid, cost_price, mrp,     sale_price, bar_yn, active_yn, status, ent_on, ent_by, Trans_status, loc_id, lt, net_rate, disc_per," +
                                        "HSN_CODE, cess_perc, excis_perc, local_rate_yn, bar_code, disc_yn FROM temp_m_item_hdr WHERE item_id = ?";


                                    using (OdbcCommand insertCmd = new OdbcCommand(insertQuery, dbConnector.connection))
                                    {
                                        insertCmd.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.ToString().Trim()));
                                        insertCmd.ExecuteNonQuery();

                                        
                                    }
                                    
                                    //-------------------------------Hdr end done-----------------------//


                                    string inserttempdet = "INSERT INTO temp_m_item_det (item_id, item_desc, bar_code, plu, cost_price, mrp, sale_price, active_yn, Trans_status, net_rate, local_rate_yn, closed_yn, comp_name) VALUES" +
                                       " (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                                    OdbcCommand cmddet = new OdbcCommand(inserttempdet, dbConnector.connection);

                                    // Uncomment the following line if you want to use a transaction
                                    // cmd.Transaction = transaction;

                                    cmddet.CommandText = inserttempdet;
                                    cmddet.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.Trim()));
                                    cmddet.Parameters.Add(new OdbcParameter("item_desc", txtItemDesc.Text.Trim()));
                                    cmddet.Parameters.Add(new OdbcParameter("bar_code", dbgBarDet.Rows[0].Cells[1].Value.ToString().Trim()));
                                    cmddet.Parameters.Add(new OdbcParameter("plu", txtItemId.Text.Trim() + dbgBarDet.Rows[0].Cells[0].Value.ToString().Trim()));
                                    cmddet.Parameters.Add(new OdbcParameter("cost_price", dbgBarDet.Rows[0].Cells[2].Value.ToString().Trim()));
                                    cmddet.Parameters.Add(new OdbcParameter("mrp", dbgBarDet.Rows[0].Cells[3].Value.ToString().Trim()));
                                    cmddet.Parameters.Add(new OdbcParameter("sale_price", dbgBarDet.Rows[0].Cells[4].Value.ToString().Trim()));
                                    cmddet.Parameters.Add(new OdbcParameter("active_yn", chkAct.Checked ? "Y" : "N"));
                                    cmddet.Parameters.Add(new OdbcParameter("Trans_status", "N"));
                                    cmddet.Parameters.Add(new OdbcParameter("net_rate", dbgBarDet.Rows[0].Cells[5].Value.ToString().Trim()));
                                    cmddet.Parameters.Add(new OdbcParameter("local_rate_yn", "N")); //todo -its for rate can't be change when there will be transfer in.
                                    cmddet.Parameters.Add(new OdbcParameter("closed_yn", "Y"));
                                    cmddet.Parameters.Add(new OdbcParameter("comp_name", DeTools.fOSMachineName.GetMachineName()));

                                    //cmd.Parameters.Add(new OdbcParameter("status", "V"));

                                    cmddet.ExecuteNonQuery();
                                    // transaction.Commit();

                                    //DeTools.SelectDataFromTemporaryTable("m_group");
                                    reader.Close();

                                    // Specify the columns explicitly in the SELECT statement
                                    string insertQuerydet = "INSERT INTO m_item_det (item_id, item_desc, bar_code, plu, cost_price, mrp,sale_price, active_yn, Trans_status, net_rate, local_rate_yn) SELECT item_id, item_desc, bar_code,plu, cost_price, mrp, sale_price, active_yn, Trans_status, net_rate, local_rate_yn FROM temp_m_item_det WHERE item_id = ?";

                                    using (OdbcCommand insertCmd1 = new OdbcCommand(insertQuerydet, dbConnector.connection))
                                    {
                                        insertCmd1.Parameters.Add(new OdbcParameter("item_id", txtItemId.Text.ToString().Trim()));
                                        insertCmd1.ExecuteNonQuery();
                                    }
                                    Messages.SavingMsg();
                                    Cursor.Current = Cursors.Default;

                                    string quer1 = "update temp_m_item_hdr set closed_yn='N' where item_id='"+ txtItemId.Text.ToString().Trim() + "' order by ent_on desc ";
                                    using (OdbcCommand qurCmd = new OdbcCommand(quer1, dbConnector.connection))
                                    {
                                        qurCmd.ExecuteNonQuery();
                                    }
                                    string quer2 = "update temp_m_item_det set closed_yn='N' where item_id='" + txtItemId.Text.ToString().Trim() + "'";
                                    using (OdbcCommand qur2Cmd = new OdbcCommand(quer2, dbConnector.connection))
                                    {
                                        qur2Cmd.ExecuteNonQuery();
                                    }
                                }//--------end of CheckTemporaryTableExists("m_item_det")--//


                            }//-----end of CheckTemporaryTableExists("m_item_hdr")----//
                        }
                      

                        Messages.SavedMsg();
                        dbConnector.connection.Close();
                        ClearForm();
                    }

            }

            }
            catch (Exception ex)
            {
              
                Messages msg= new Messages();
                msg.VBError(ex,this.Name, "Save Form", null);
            }
            

        }
        public void SearchForm()
        {
            try
            {

                DbConnector dbConnector = new DbConnector();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                if (!string.IsNullOrWhiteSpace(txtItemId.Text) && !mblnSearch)
                {
                    msg.HelpMsg("Information retrieving. Please wait...");

                    DeTools.gstrSQL = "SELECT * FROM m_item_hdr WHERE item_id = '" + txtItemId.Text.Trim() + "'";

                    OdbcCommand hdrcmd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);
                    
                    OdbcDataReader hdrread = hdrcmd.ExecuteReader();

                    if (hdrread.HasRows)
                    {
                        hdrcmd.CommandText = DeTools.gstrSQL;
                    
                        txtShDesc.Text = hdrread["short_desc"].ToString().Trim();
                        txtItemDesc.Text = hdrread["item_desc"].ToString().Trim();

                        //-------------------------Item Type combo----------------------------//
                        general.FillCombo(cboType, "item_type_id", "m_item_type", false);
                        string typIDFromDatabase = hdrread["item_type_id"].ToString().Trim();

                        if (typIDFromDatabase != "")
                        {
                            // Find the item in the ComboBox's items collection
                            object selectedItem = cboType.Items.Cast<object>().FirstOrDefault(item => item.ToString() == typIDFromDatabase);

                            // Set the selected item if found
                            if (selectedItem != null)
                            {
                                cboType.SelectedItem = selectedItem;
                                rotType.Text = general.GetDesc("m_item_type", "item_type_id", "item_type_desc", "C", cboType.SelectedItem.ToString().Trim());
                            }


                        }

                        //------------------------Group combo----------------------------//
                        general.FillCombo(cboGroup, "group_id", "m_group", false);
                        string groupIDFromDatabase = hdrread["group_id"].ToString().Trim();

                        if (groupIDFromDatabase != "")
                        {
                            // Find the item in the ComboBox's items collection
                            object selectedItem1 = cboGroup.Items.Cast<object>().FirstOrDefault(item => item.ToString() == groupIDFromDatabase);

                            // Set the selected item if found
                            if (selectedItem1 != null)
                            {
                                cboGroup.SelectedItem = selectedItem1;
                                rotGroup.Text = general.GetDesc("m_group", "group_id", "group_desc", "C", cboGroup.SelectedItem.ToString().Trim());
                            }

                            //------------------------Sub Group combo----------------------------//
                            general.FillSubGroup(cboSGroup, selectedItem1.ToString().Trim());
                            string SgroupIDFromDatabase = hdrread["sub_group_id"].ToString().Trim();
                            if (SgroupIDFromDatabase != "")
                            {
                                // Find the item in the ComboBox's items collection
                                object selectedItem2 = cboSGroup.Items.Cast<object>().FirstOrDefault(item => item.ToString() == SgroupIDFromDatabase);

                                // Set the selected item if found
                                if (selectedItem2 != null)
                                {
                                    cboSGroup.SelectedItem = selectedItem2;
                                    rotSGroup.Text = general.GetDesc("m_sub_group", "sub_group_id", "sub_group_desc", "C", cboSGroup.SelectedItem.ToString().Trim());
                                }

                                //------------------------Sub Sub Group combo----------------------------//
                                general.FillSSubGroup(cboSSGroupDesc, selectedItem2.ToString().Trim());

                                string SSgroupIDFromDatabase = hdrread["sub_sub_group_id"].ToString().Trim();
                                if (SSgroupIDFromDatabase != "")
                                {
                                    // Find the item in the ComboBox's items collection
                                    object selectedItem3 = cboSSGroupDesc.Items.Cast<object>().FirstOrDefault(item => item.ToString() == SSgroupIDFromDatabase);

                                    // Set the selected item if found
                                    if (selectedItem3 != null)
                                    {
                                        cboSSGroupDesc.SelectedItem = selectedItem3;

                                        rotSSGroupDesc.Text = general.GetDesc("m_sub_sub_group", "sub_sub_group_id", "sub_sub_group_desc", "C", cboSSGroupDesc.SelectedItem.ToString().Trim());
                                    }

                                }

                            }

                        }

                        //------------------------Size combo----------------------------//
                        general.FillCombo(cboSizeDesc, "size_id", "m_size", false);
                        string sizeFromDatabase = hdrread["size_id"].ToString().Trim();
                        if (sizeFromDatabase != "")
                        {
                            // Find the item in the ComboBox's items collection
                            object selectedItem4 = cboSizeDesc.Items.Cast<object>().FirstOrDefault(item => item.ToString() == sizeFromDatabase);

                            // Set the selected item if found
                            if (selectedItem4 != null)
                            {
                                cboSizeDesc.SelectedItem = selectedItem4;
                                rotSizeDesc.Text = general.GetDesc("m_size", "size_id", "size_desc", "C", cboSizeDesc.SelectedItem.ToString().Trim());
                            }

                        }
                        //------------------------Color combo----------------------------//
                        general.FillCombo(cboColorDesc, "color_id", "m_color", false);
                        string colorFromDatabase = hdrread["color_id"].ToString().Trim();
                        if (colorFromDatabase != "")
                        {
                            // Find the item in the ComboBox's items collection
                            object selectedItem5 = cboColorDesc.Items.Cast<object>().FirstOrDefault(item => item.ToString() == colorFromDatabase);

                            // Set the selected item if found
                            if (selectedItem5 != null)
                            {
                                cboSizeDesc.SelectedItem = selectedItem5;
                                rotColorDesc.Text = general.GetDesc("m_color", "colot_id", "color_desc", "C", cboColorDesc.SelectedItem.ToString().Trim());
                            }

                        }

                        txtStyle.Text = hdrread["style"].ToString().Trim();

                        //------------------------Manuf combo----------------------------//
                        general.FillCombo(cboManuf, "manuf_id", "m_manuf", false);
                        string manufFromDatabase = hdrread["manuf_id"].ToString().Trim();
                        if (manufFromDatabase != "")
                        {
                            // Find the item in the ComboBox's items collection
                            object selectedItem6 = cboManuf.Items.Cast<object>().FirstOrDefault(item => item.ToString() == manufFromDatabase);

                            // Set the selected item if found
                            if (selectedItem6 != null)
                            {
                                cboManuf.SelectedItem = selectedItem6;
                                rotManuf.Text = general.GetDesc("m_manuf", "manuf_id", "manuf_name", "C", cboManuf.SelectedItem.ToString().Trim());
                            }

                        }

                        //------------------------Pur Unit combo----------------------------//
                        general.FillCombo(cboPurUnit, "unit_id", "m_unit", false);
                        string purunitFromDatabase = hdrread["pur_unit_id"].ToString().Trim();
                        if (purunitFromDatabase != "")
                        {
                            // Find the item in the ComboBox's items collection
                            object selectedItem7 = cboPurUnit.Items.Cast<object>().FirstOrDefault(item => item.ToString() == purunitFromDatabase);

                            // Set the selected item if found
                            if (selectedItem7 != null)
                            {
                                cboPurUnit.SelectedItem = selectedItem7;
                                rotPurUnit.Text = general.GetDesc("m_unit", "unit_id", "unit_desc", "C", cboPurUnit.SelectedItem.ToString().Trim());
                            }

                        }

                        //------------------------Sale Unit combo----------------------------//
                        general.FillCombo(cboSaleUnit, "unit_id", "m_unit", false);
                        string saleunitFromDatabase = hdrread["sale_unit_id"].ToString().Trim();
                        if (saleunitFromDatabase != "")
                        {
                            // Find the item in the ComboBox's items collection
                            object selectedItem8 = cboSaleUnit.Items.Cast<object>().FirstOrDefault(item => item.ToString() == saleunitFromDatabase);

                            // Set the selected item if found
                            if (selectedItem8 != null)
                            {
                                cboSaleUnit.SelectedItem = selectedItem8;
                                rotSaleUnit.Text = general.GetDesc("m_unit", "unit_id", "unit_desc", "C", cboSaleUnit.SelectedItem.ToString().Trim());
                            }

                        }


                        txtConv.Text = hdrread["conv_pur_sale"].ToString().Trim();
                        txtOpBal.Text = hdrread["op_bal_unit"].ToString().Trim();
                        txtMinLevel.Text = hdrread["min_level"].ToString().Trim();
                        txtReOLevel.Text = hdrread["re_order_level"].ToString().Trim();
                        txtMaxLevel.Text = hdrread["max_level"].ToString().Trim();
                        if (hdrread["qty_decimal_yn"].ToString() == "Y")
                        {
                            chkDecimal.Checked = true;
                        }
                        else
                        {
                            chkDecimal.Checked = false;
                        }

                        //------------------------Sale Tax combo----------------------------//
                        general.FillCombo(cboSaleTax, "tax_type_id", "m_tax_type", false);
                        string saletaxFromDatabase = hdrread["sale_tax_paid"].ToString().Trim();
                        if (saletaxFromDatabase != "")
                        {
                            // Find the item in the ComboBox's items collection
                            object selectedItem9 = cboSaleTax.Items.Cast<object>().FirstOrDefault(item => item.ToString() == saletaxFromDatabase);

                            // Set the selected item if found
                            if (selectedItem9 != null)
                            {
                                cboSaleTax.SelectedItem = selectedItem9;
                                rotSaleTax.Text = general.GetDesc("m_tax_type", "tax_type_id", "tax_type_desc", "C", cboSaleTax.SelectedItem.ToString().Trim());
                            }

                        }


                        if (hdrread["bar_yn"].ToString() == "Y")
                        {
                            chkBarYN.Checked = true;
                        }
                        else
                        {
                            chkBarYN.Checked = false;
                        }
                        if (hdrread["active_yn"].ToString() == "Y")
                        {
                            chkAct.Checked = true;
                        }
                        else
                        {
                            chkAct.Checked = false;
                        }

                        rotNetRate.Text = hdrread["net_rate"].ToString().Trim();
                        txtLT.Text = hdrread["lt"].ToString().Trim();
                        txtDisc.Text = hdrread["disc_per"].ToString().Trim();
                        txtHSN.Text = hdrread["HSN_CODE"].ToString().Trim();
                        txtCess.Text = hdrread["cess_perc"].ToString().Trim();
                        txtexisper.Text = hdrread["excis_perc"].ToString().Trim();

                        hdrread.Close();

                        //------------HDR DATA LOAD CLOSED---------------------//

                        // Create a DataTable to store the results
                        DataTable dataTable = new DataTable();

                        // Your SQL query
                        DeTools.gstrSQL = "SELECT plu, bar_code, cost_price, mrp, sale_price, net_rate FROM m_item_det WHERE item_id = '" + txtItemId.Text.Trim() + "' order by plu ";

                        // Create OdbcCommand
                        OdbcCommand detcmd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);

                        // Execute the reader
                        OdbcDataReader detread = detcmd.ExecuteReader();

                        if (detread.HasRows)
                        {
                            detcmd.CommandText = DeTools.gstrSQL;
                            // Load the results into the DataTable
                            dataTable.Load(detread);

                            // Close the reader
                            detread.Close();

                            // Clear existing rows in the DataGridView
                            dbgBarDet.Rows.Clear();

                            // Add new rows to the DataGridView from the DataTable
                            foreach (DataRow row in dataTable.Rows)
                            {
                                // Add a new row to the DataGridView
                                int rowIndex = dbgBarDet.Rows.Add();

                                // Populate the cells in the DataGridView with data from the DataTable
                                dbgBarDet.Rows[rowIndex].Cells[0].Value = row["plu"];
                                dbgBarDet.Rows[rowIndex].Cells[1].Value = row["bar_code"];
                                dbgBarDet.Rows[rowIndex].Cells[2].Value = row["cost_price"];
                                dbgBarDet.Rows[rowIndex].Cells[3].Value = row["mrp"];
                                dbgBarDet.Rows[rowIndex].Cells[4].Value = row["sale_price"];
                                dbgBarDet.Rows[rowIndex].Cells[5].Value = row["net_rate"];
                            }
                                                  
                        }
                        //------------DATAGRID DET DATA LOAD CLOSED---------------------//

                        // Your SQL query
                        DeTools.gstrSQL = "SELECT active_yn FROM m_item_det WHERE item_id = '" + txtItemId.Text.Trim() + "'";

                        // Create OdbcCommand
                        OdbcCommand detcmd2 = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);

                        // Execute the reader
                        OdbcDataReader detread2 = detcmd2.ExecuteReader();

                        if (detread2.HasRows)
                        {
                            detcmd2.CommandText = DeTools.gstrSQL;
                            if (detread2["active_yn"] == "Y")
                            {
                                chkAct.Checked = true;
                            }
                            else
                            {
                                chkAct.Checked = false;
                            }
                        

                            detread2.Close();
                            mblnSearch = true;
                        }


                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        

        public void UnsavedData()
        {
            DbConnector dbConnector = new DbConnector();


            try
            {
                string compname = DeTools.fOSMachineName.GetMachineName();
                string user = MainForm.Instance.pnlUserName.Text.Trim();

                if (DeTools.CheckTemporaryTableExists("m_item_hdr") != null)
                {
                    if (DeTools.CheckTemporaryTableExists("m_item_det") != null)
                    {
                        dbConnector.OpenConnection();

                        saveflag = true;

                      

                        string query = "SELECT a.* FROM temp_m_item_det a,temp_m_item_hdr b  WHERE a.closed_yn='Y' and b.ent_by='" + user.Trim() + "' and a.comp_name='" + compname.Trim() + "' order by ent_on desc limit 1;";

                        OdbcParameter[] parameters = new OdbcParameter[0];


                        using (OdbcDataReader reader = dbConnector.ExecuteReader(query, parameters))
                        {
                            if (reader.Read())
                            {
                                // Populate other text fields similarly
                                Messages.UnsavedMsg(null);
                                //txtItemId.Text = reader["item_id"].ToString().Trim();
                                txtItemDesc.Text= reader["item_desc"].ToString().Trim();
                                dbgBarDet.Rows[0].Cells[1].Value = reader["bar_code"].ToString().Trim();
                                string plu = reader["plu"].ToString().Trim();
                               
                                    dbgBarDet.Rows[0].Cells[0].Value = plu;

                                dbgBarDet.Rows[0].Cells[2].Value = reader["cost_price"].ToString().Trim();
                                dbgBarDet.Rows[0].Cells[3].Value = reader["mrp"].ToString().Trim();
                                dbgBarDet.Rows[0].Cells[4].Value = reader["sale_price"].ToString().Trim();
                                if (reader["active_yn"] == "Y")
                                {
                                    chkAct.Checked = true;
                                }
                                else
                                {
                                    chkAct.Checked = false;
                                }
                                dbgBarDet.Rows[0].Cells[5].Value = reader["net_rate"].ToString().Trim();

                                reader.Close();

                            }
                               
                        }
                    }

                    string query1 = "SELECT * FROM temp_m_item_hdr WHERE closed_yn='Y' and ent_by='" + user.Trim() + "' and comp_name='" + compname.Trim() + "' order by ent_on desc limit 1;";

                    OdbcParameter[] parameters1 = new OdbcParameter[0];


                    using (OdbcDataReader reader = dbConnector.ExecuteReader(query1, parameters1))
                    {
                        if (reader.Read())
                        {
                            txtShDesc.Text = reader["short_desc"].ToString().Trim();

                            //-------------------------Item Type combo----------------------------//
                            general.FillCombo(cboType, "item_type_id", "m_item_type", false);
                            string typIDFromDatabase = reader["item_type_id"].ToString().Trim();

                            if (typIDFromDatabase !="")
                            {
                                // Find the item in the ComboBox's items collection
                                object selectedItem = cboType.Items.Cast<object>().FirstOrDefault(item => item.ToString() == typIDFromDatabase);

                                // Set the selected item if found
                                if (selectedItem != null)
                                {
                                    cboType.SelectedItem = selectedItem;
                                    rotType.Text = general.GetDesc("m_item_type", "item_type_id", "item_type_desc", "C", cboType.SelectedItem.ToString().Trim());                                
                                }
                            
                                
                            }

                            //------------------------Group combo----------------------------//
                            general.FillCombo(cboGroup, "group_id", "m_group", false);
                            string groupIDFromDatabase = reader["group_id"].ToString().Trim();

                            if (groupIDFromDatabase != "")
                            {
                                // Find the item in the ComboBox's items collection
                                object selectedItem1 = cboGroup.Items.Cast<object>().FirstOrDefault(item => item.ToString() == groupIDFromDatabase);

                                // Set the selected item if found
                                if (selectedItem1 != null)
                                {
                                    cboGroup.SelectedItem = selectedItem1;
                                    rotGroup.Text = general.GetDesc("m_group", "group_id", "group_desc", "C", cboGroup.SelectedItem.ToString().Trim());
                                }

                                //------------------------Sub Group combo----------------------------//
                                general.FillSubGroup(cboSGroup, selectedItem1.ToString().Trim());
                                string SgroupIDFromDatabase = reader["sub_group_id"].ToString().Trim();
                                if (SgroupIDFromDatabase != "")
                                {
                                    // Find the item in the ComboBox's items collection
                                    object selectedItem2 = cboSGroup.Items.Cast<object>().FirstOrDefault(item => item.ToString() == SgroupIDFromDatabase);

                                    // Set the selected item if found
                                    if (selectedItem2 != null)
                                    {
                                        cboSGroup.SelectedItem = selectedItem2;                                    
                                        rotSGroup.Text = general.GetDesc("m_sub_group", "sub_group_id", "sub_group_desc", "C", cboSGroup.SelectedItem.ToString().Trim());
                                    }

                                    //------------------------Sub Sub Group combo----------------------------//
                                    general.FillSSubGroup(cboSSGroupDesc, selectedItem2.ToString().Trim());

                                    string SSgroupIDFromDatabase = reader["sub_sub_group_id"].ToString().Trim();
                                    if (SSgroupIDFromDatabase != "")
                                    {
                                        // Find the item in the ComboBox's items collection
                                        object selectedItem3 = cboSSGroupDesc.Items.Cast<object>().FirstOrDefault(item => item.ToString() == SSgroupIDFromDatabase);

                                        // Set the selected item if found
                                        if (selectedItem3 != null)
                                        {
                                            cboSSGroupDesc.SelectedItem = selectedItem3;
                                    
                                            rotSSGroupDesc.Text = general.GetDesc("m_sub_sub_group", "sub_sub_group_id", "sub_sub_group_desc", "C", cboSSGroupDesc.SelectedItem.ToString().Trim());
                                        }

                                    }

                                }

                            }

                            //------------------------Size combo----------------------------//
                             general.FillCombo(cboSizeDesc, "size_id", "m_size", false);
                             string sizeFromDatabase = reader["size_id"].ToString().Trim();
                             if (sizeFromDatabase != "")
                             {
                                // Find the item in the ComboBox's items collection
                                object selectedItem4 = cboSizeDesc.Items.Cast<object>().FirstOrDefault(item => item.ToString() == sizeFromDatabase);

                                // Set the selected item if found
                                   if (selectedItem4 != null)
                                   {
                                      cboSizeDesc.SelectedItem = selectedItem4;                                                                               
                                      rotSizeDesc.Text = general.GetDesc("m_size", "size_id", "size_desc", "C", cboSizeDesc.SelectedItem.ToString().Trim());
                                    }

                             }
                            //------------------------Color combo----------------------------//
                             general.FillCombo(cboColorDesc, "color_id", "m_color", false);
                             string colorFromDatabase = reader["color_id"].ToString().Trim();
                             if (colorFromDatabase != "")
                             {
                                // Find the item in the ComboBox's items collection
                                object selectedItem5 = cboColorDesc.Items.Cast<object>().FirstOrDefault(item => item.ToString() == colorFromDatabase);

                                // Set the selected item if found
                                   if (selectedItem5 != null)
                                   {
                                      cboSizeDesc.SelectedItem = selectedItem5;                                                                                                                     
                                      rotColorDesc.Text = general.GetDesc("m_color", "colot_id", "color_desc", "C", cboColorDesc.SelectedItem.ToString().Trim());
                                    }

                             }

                            txtStyle.Text = reader["style"].ToString().Trim();

                            //------------------------Manuf combo----------------------------//
                            general.FillCombo(cboManuf, "manuf_id", "m_manuf", false);
                            string manufFromDatabase = reader["manuf_id"].ToString().Trim();
                            if (manufFromDatabase != "")
                            {
                                // Find the item in the ComboBox's items collection
                                object selectedItem6 = cboManuf.Items.Cast<object>().FirstOrDefault(item => item.ToString() == manufFromDatabase);

                                // Set the selected item if found
                                if (selectedItem6 != null)
                                {
                                    cboManuf.SelectedItem = selectedItem6;                                    
                                    rotManuf.Text = general.GetDesc("m_manuf", "manuf_id", "manuf_name", "C", cboManuf.SelectedItem.ToString().Trim());
                                }

                            }

                            //------------------------Pur Unit combo----------------------------//
                            general.FillCombo(cboPurUnit, "unit_id", "m_unit", false);
                            string purunitFromDatabase = reader["pur_unit_id"].ToString().Trim();
                            if (purunitFromDatabase != "")
                            {
                                // Find the item in the ComboBox's items collection
                                object selectedItem7 = cboPurUnit.Items.Cast<object>().FirstOrDefault(item => item.ToString() == purunitFromDatabase);

                                // Set the selected item if found
                                if (selectedItem7 != null)
                                {
                                    cboPurUnit.SelectedItem = selectedItem7;                                                                        
                                    rotPurUnit.Text = general.GetDesc("m_unit", "unit_id", "unit_desc", "C", cboPurUnit.SelectedItem.ToString().Trim());
                                }

                            }

                            //------------------------Sale Unit combo----------------------------//
                            general.FillCombo(cboSaleUnit, "unit_id", "m_unit", false);
                            string saleunitFromDatabase = reader["sale_unit_id"].ToString().Trim();
                            if (saleunitFromDatabase != "")
                            {
                                // Find the item in the ComboBox's items collection
                                object selectedItem8 = cboSaleUnit.Items.Cast<object>().FirstOrDefault(item => item.ToString() == saleunitFromDatabase);

                                // Set the selected item if found
                                if (selectedItem8 != null)
                                {
                                    cboSaleUnit.SelectedItem = selectedItem8;                                                                                                        
                                    rotSaleUnit.Text = general.GetDesc("m_unit", "unit_id", "unit_desc", "C", cboSaleUnit.SelectedItem.ToString().Trim());
                                }

                            }


                            txtConv.Text = reader["conv_pur_sale"].ToString().Trim();
                            txtOpBal.Text = reader["op_bal_unit"].ToString().Trim();
                            txtMinLevel.Text = reader["min_level"].ToString().Trim();
                            txtReOLevel.Text = reader["re_order_level"].ToString().Trim();
                            txtMaxLevel.Text = reader["max_level"].ToString().Trim();
                            if (reader["qty_decimal_yn"].ToString() == "Y")
                            {
                                chkDecimal.Checked = true;
                            }
                            else
                            {
                                chkDecimal.Checked = false;
                            }

                            //------------------------Sale Tax combo----------------------------//
                            general.FillCombo(cboSaleTax, "tax_type_id", "m_tax_type", false);
                            string saletaxFromDatabase = reader["sale_tax_paid"].ToString().Trim();
                            if (saletaxFromDatabase != "")
                            {
                                // Find the item in the ComboBox's items collection
                                object selectedItem9 = cboSaleTax.Items.Cast<object>().FirstOrDefault(item => item.ToString() == saletaxFromDatabase);

                                // Set the selected item if found
                                if (selectedItem9 != null)
                                {
                                    cboSaleTax.SelectedItem = selectedItem9;                                    
                                    rotSaleTax.Text = general.GetDesc("m_tax_type", "tax_type_id", "tax_type_desc", "C", cboSaleTax.SelectedItem.ToString().Trim());
                                }

                            }


                            if (reader["bar_yn"].ToString() == "Y")
                            {
                                chkBarYN.Checked = true;
                            }
                            else
                            {
                                chkBarYN.Checked = false;
                            }
                              if (reader["active_yn"].ToString() == "Y")
                            {
                                chkAct.Checked = true;
                            }
                            else
                            {
                                chkAct.Checked = false;
                            }

                            rotNetRate.Text = reader["net_rate"].ToString().Trim();
                            txtLT.Text = reader["lt"].ToString().Trim();
                            txtDisc.Text = reader["disc_per"].ToString().Trim();
                            txtHSN.Text = reader["HSN_CODE"].ToString().Trim();
                            txtCess.Text = reader["cess_perc"].ToString().Trim();
                            txtexisper.Text = reader["excis_perc"].ToString().Trim();

                            reader.Close();

                        }

                    }


                  
                }

             
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the database operation
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }
        }

        public void check_temp_login_sytemname()
        {
            string getent_by = "";
            string getcomp_name = "";
            DbConnector dbConnector = new DbConnector();

            dbConnector.OpenConnection();

            bool check = false;
            string id = "item_id";
            string tblname = "m_item_hdr";

            DeTools.gstrSQL = "SELECT " + id + ",ent_by,comp_name FROM temp_" + tblname + " order by ent_on desc limit 1;";

            using (OdbcDataReader reader = dbConnector.CreateResultset(DeTools.gstrSQL))
            {
                if (reader.HasRows)
                {
                    getent_by = reader["ent_by"].ToString();
                    getcomp_name = reader["comp_name"].ToString();


                }
                string pnlusername = MainForm.Instance.pnlUserName.Text.Trim();
                string machine_name = DeTools.fOSMachineName.GetMachineName();

                if (getent_by.ToLower().Trim() == pnlusername.ToLower().Trim() && getcomp_name.Trim() == machine_name.Trim())
                {

                    UnsavedData();
                }
            }
        }

        public void SetSearchVar(bool StartVal)
        {
            // Implementation of SetSearchVar
            // You can define the behavior of SetSearchVar here

            mblnSearch = StartVal;
        }

        public void Item_Load(object sender, EventArgs e)
        {
            DeTools.DisplayForm(this, 485, 740);
            this.Location = new Point(320, 50);
            ToolTip toolTip = new ToolTip();
            dbgBarDet.KeyDown += dataGridView1_KeyDown;
            dbgBarDet.AllowUserToAddRows = true;

            toolTip.SetToolTip(dbgBarDet, "Enter the details in this table.");
            General.MasterParam("item_id_am", "item_id_sn", "m_mast_param", out chkItemid, out chkItemsn);

            Help.controlToHelpTopicMapping.Add(txtItemId, "1006"); /////For Help ContextId///IMP...

            DeTools.CheckTemporaryTableExists("m_item_hdr");
            DeTools.CheckTemporaryTableExists("m_item_det");
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


        public void grpcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGroup.SelectedItem != null)
            {
                string desc = general.GetDesc("m_group", "group_id", "group_desc", "C", cboGroup.SelectedItem.ToString().Trim());
                rotGroup.Text = desc;
            }
        }

        public void grpcomb_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void comboBox2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void manufcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboManuf.SelectedItem != null)
            {
                string desc = general.GetDesc("m_manuf", "manuf_id", "manuf_name", "C", cboManuf.SelectedItem.ToString().Trim());
                rotManuf.Text = desc;
            }
        }

        public void grptxt_TextChanged(object sender, EventArgs e)
        {

        }

        public void lblgrp_Click(object sender, EventArgs e)
        {

        }



        public void typcomb_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void typcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedItem != null)
            {
                string desc = general.GetDesc("m_item_type", "item_type_id", "item_type_desc", "C", cboType.SelectedItem.ToString().Trim());
                rotType.Text = desc;
            }
        }

        public void subgrpcomb_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void subgrpcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSGroup.SelectedItem != null)
            {
                string desc = general.GetDesc("m_sub_group", "sub_group_id", "sub_group_desc", "C", cboSGroup.SelectedItem.ToString().Trim());
                rotSGroup.Text = desc;
            }
        }

        public void ssgrpcomb_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void ssgrpcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSSGroupDesc.SelectedItem != null)
            {
                string desc = general.GetDesc("m_sub_sub_group", "sub_sub_group_id", "sub_sub_group_desc", "C", cboSSGroupDesc.SelectedItem.ToString().Trim());
                rotSSGroupDesc.Text = desc;
            }
        }

        public void colidcomb_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void colidcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboColorDesc.SelectedItem != null)
            {
                string desc = general.GetDesc("m_color", "colot_id", "color_desc", "C", cboColorDesc.SelectedItem.ToString().Trim());
                rotColorDesc.Text = desc;
            }
        }

        public void sizecomb_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void sizecomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSizeDesc.SelectedItem != null)
            {
                string desc = general.GetDesc("m_size", "size_id", "size_desc", "C", cboSizeDesc.SelectedItem.ToString().Trim());
                rotSizeDesc.Text = desc;
            }
        }

        public void purunitcomb_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void purunitcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPurUnit.SelectedItem != null)
            {
                string desc = general.GetDesc("m_unit", "unit_id", "unit_desc", "C", cboPurUnit.SelectedItem.ToString().Trim());
                rotPurUnit.Text = desc;
            }
        }

        public void salunitcomb_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void salunitcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSaleUnit.SelectedItem != null)
            {
                string desc = general.GetDesc("m_unit", "unit_id", "unit_desc", "C", cboSaleUnit.SelectedItem.ToString().Trim());
                rotSaleUnit.Text = desc;
            }
        }

        public void gstperccomb_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void cboSaleTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSaleTax.SelectedItem != null)
            {
                string desc = general.GetDesc("m_tax_type", "tax_type_id", "tax_type_desc", "C", cboSaleTax.SelectedItem.ToString().Trim());
                rotSaleTax.Text = desc;
            }
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
            DeTools.DestroyToolbar(this);
            MainForm.Instance.MItemmenu.Enabled = true;

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
            // Check if the current cell belongs to the 3rd, 4th, or 5th column
            if (dbgBarDet.CurrentCell != null &&
                (dbgBarDet.CurrentCell.ColumnIndex == 2 || dbgBarDet.CurrentCell.ColumnIndex == 3 || dbgBarDet.CurrentCell.ColumnIndex == 4))
            {
                // Allow digits, backspace, and the decimal point
                if (!char.IsControl((char)e.KeyCode) && !char.IsDigit((char)e.KeyCode) && (char)e.KeyCode != '.')
                {
                    e.Handled = true; // Ignore the key press
                    MessageBox.Show("Please enter numeric values only.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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


        public void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the edited cell is in the second column and the row is not a new row
            if ((e.ColumnIndex == 1) && e.RowIndex != dbgBarDet.NewRowIndex)
            {
                DataGridViewRow editedRow = dbgBarDet.Rows[e.RowIndex];
                DataGridViewCell editedCell = editedRow.Cells[e.ColumnIndex];

                // Increment the PLU counter
                pluCounter++;

                // Get the PLU value based on the counter
                string pluValue = GetPLUValue(pluCounter);

                // Store the PLU value for the current row index
                pluValues[e.RowIndex] = pluValue;

                // Set the PLU value in the first column
                int columnIndex = 0;
                editedRow.Cells[columnIndex].Value = pluValue;

                // Set default values in the 3rd, 4th, and 5th columns
                editedRow.Cells[2].Value = "0.00"; // 3rd column
                editedRow.Cells[3].Value = "0.00"; // 4th column
                editedRow.Cells[4].Value = "0.00"; // 5th column

                // Update PLU value in the first column of the previous row
                if (e.RowIndex > 0)
                {
                    DataGridViewRow previousRow = dbgBarDet.Rows[e.RowIndex - 1];

                    // Check if the PLU value for the previous row index exists in the dictionary
                    if (pluValues.ContainsKey(e.RowIndex - 1))
                    {
                        previousRow.Cells[columnIndex].Value = pluValues[e.RowIndex - 1];
                    }
                }
            }

            // Check if the current cell belongs to the 3rd, 4th, or 5th column
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                DataGridViewCell cell = dbgBarDet.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Validate numeric input
                if (!decimal.TryParse(cell.Value?.ToString(), out decimal result))
                {
                    MessageBox.Show("You Cannot use Alphabets.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Optionally, you can reset the cell value to a default or previous value
                    // cell.Value = GetPreviousValue(); 
                    cell.Value = "";
                    return;
                }

                // Format the input to display two decimal places
                cell.Value = result.ToString("0.00");
            }
        }

        private void ValidateCellValues(int rowIndex, int columnIndex)
        {

        }



        private void ShowValidationError(string message, int rowIndex, int columnIndex)
        {
            MessageBox.Show(message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Set a flag to indicate that validation has occurred for this cell
            dbgBarDet.Rows[rowIndex].Cells[columnIndex].Tag = true;
        }



        private void itemidtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboGroup_DropDown(object sender, EventArgs e)
        {
            general.FillCombo(cboGroup, "group_id", "m_group", false);
            // cboGroup.MaxDropDownItems = 8;
        }

        private void cboSGroup_DropDown(object sender, EventArgs e)
        {
            if (cboGroup.SelectedItem == null)
            {
                Messages.InfoMsg("Pls First Select In Group!");

            }
            else
            {
                general.FillSubGroup(cboSGroup, cboGroup.SelectedItem.ToString().Trim());
            }
        }

        private void cboSSGroupDesc_DropDown(object sender, EventArgs e)
        {
            if (cboSGroup.SelectedItem == null)
            {
                Messages.InfoMsg("Pls First Select In Sub Group!");
            }
            else
            {
                general.FillSSubGroup(cboSSGroupDesc, cboSGroup.SelectedItem.ToString().Trim());
            }
        }

        private void cboSizeDesc_DropDown(object sender, EventArgs e)
        {
            general.FillCombo(cboSizeDesc, "size_id", "m_size", false);
            if (cboSizeDesc.Items == null)
            {
                Messages.WarningMsg("No Data Found, Pls Add First!");
            }
        }

        private void cboColorDesc_DropDown(object sender, EventArgs e)
        {
            general.FillCombo(cboColorDesc, "color_id", "m_color", false);
        }

        private void cboManuf_DropDown(object sender, EventArgs e)
        {
            general.FillCombo(cboManuf, "manuf_id", "m_manuf", false);
        }

        private void cboPurUnit_DropDown(object sender, EventArgs e)
        {
            general.FillCombo(cboPurUnit, "unit_id", "m_unit", false);
        }

        private void cboSaleUnit_DropDown(object sender, EventArgs e)
        {
            general.FillCombo(cboSaleUnit, "unit_id", "m_unit", false);
        }

        private void cboType_DropDown(object sender, EventArgs e)
        {
            general.FillCombo(cboType, "item_type_id", "m_item_type", false);
        }

        private void cboSaleTax_DropDown(object sender, EventArgs e)
        {
            general.FillCombo(cboSaleTax, "tax_type_id", "m_tax_type", false);
        }

        private void cboType_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dbgBarDet_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void dbgBarDet_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private decimal GetCellValue(int rowIndex, int columnIndex)
        {
            if (dbgBarDet.EditingControl is DataGridViewTextBoxEditingControl editingControl)
            {
                // Use EditingControlFormattedValue to get the current, possibly formatted, value
                object editingControlValue = editingControl.EditingControlFormattedValue;

                return editingControlValue == null || !decimal.TryParse(editingControlValue.ToString(), out decimal result) ? 0 : result;
            }

            // If the editing control is not a text box, you can still use the Value property
            object cellValue = dbgBarDet.Rows[rowIndex].Cells[columnIndex].Value;
            return cellValue == null || !decimal.TryParse(cellValue.ToString(), out decimal numericValue) ? 0 : numericValue;
        }

        private void ErrorMsg(string message)
        {
            MessageBox.Show(message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void dbgBarDet_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //if (e.ColumnIndex >= 2 && e.ColumnIndex <= 4)
            //{
            //    decimal numericValue = GetCellValue(e.RowIndex, e.ColumnIndex);

            //    if (numericValue < 0)
            //    {
            //        ErrorMsg("Enter a positive numeric value.");
            //        e.Cancel = true;
            //        return;
            //    }

            //    if (e.ColumnIndex == 2)
            //    {
            //        if (numericValue > 0)
            //        {
            //            decimal mrpValue = GetCellValue(e.RowIndex, 3);
            //            decimal salePriceValue = GetCellValue(e.RowIndex, 4);

            //            if ((mrpValue > 0 && mrpValue < numericValue) || (salePriceValue > 0 && salePriceValue < numericValue))
            //            {
            //                ErrorMsg("Cost Price can not be greater than M.R.P. or Sale Price");
            //                e.Cancel = true;
            //            }
            //        }
            //    }
            //    else if (e.ColumnIndex == 3)
            //    {
            //        if (numericValue > 0)
            //        {
            //            decimal costPriceValue = GetCellValue(e.RowIndex, 2);
            //            decimal salePriceValue = GetCellValue(e.RowIndex, 4);

            //            if ((costPriceValue > 0 && numericValue < costPriceValue) || (salePriceValue > 0 && numericValue < salePriceValue))
            //            {
            //                ErrorMsg("M.R.P can not be less than Cost Price or Sale Price");
            //                e.Cancel = true;
            //            }
            //        }
            //    }
            //    else if (e.ColumnIndex == 4)
            //    {
            //        if (numericValue > 0)
            //        {
            //            decimal mrpValue = GetCellValue(e.RowIndex, 3);
            //            decimal costPriceValue = GetCellValue(e.RowIndex, 2);

            //            if ((mrpValue > 0 && mrpValue < numericValue) || (costPriceValue > 0 && costPriceValue > numericValue))
            //            {
            //                ErrorMsg("Sale Price can not be greater than M.R.P. or less than Cost Price");
            //                e.Cancel = true;
            //            }
            //        }
            //    }
            //}
        }

        private decimal GetDecimalCellValue(int rowIndex, int columnIndex)
        {
            object value = dbgBarDet.Rows[rowIndex].Cells[columnIndex].Value;
            return decimal.TryParse(value?.ToString(), out decimal result) ? result : 0;
        }

        private void dbgBarDet_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the current cell belongs to the 2nd, 3rd, 4th, or 5th column
            if (e.ColumnIndex >= 2 && e.ColumnIndex <= 5)
            {
                ValidateCellValues(e.RowIndex, e.ColumnIndex);
            }

        }

        private void dbgBarDet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 2 && e.ColumnIndex <= 4 && e.RowIndex >= 0)
            {
                decimal numericValue;

                if (!decimal.TryParse(dbgBarDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString(), out numericValue) || numericValue < 0)
                {
                    ErrorMsg("Enter a positive numeric value.");
                    dbgBarDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value; // Reset the cell value
                    return;
                }

                if (e.ColumnIndex == 2)
                {
                    if (numericValue > 0)
                    {
                        decimal mrpValue = GetCellValue(e.RowIndex, 3);
                        decimal salePriceValue = GetCellValue(e.RowIndex, 4);

                        if ((mrpValue > 0 && mrpValue < numericValue) || (salePriceValue > 0 && salePriceValue < numericValue))
                        {
                            ErrorMsg("Cost Price can not be greater than M.R.P. or Sale Price");
                            dbgBarDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value; // Reset the cell value
                            // Prevent moving to the next cell

                            dbgBarDet.CurrentCell = dbgBarDet.Rows[e.RowIndex].Cells[e.ColumnIndex - 1];

                        }
                    }
                }
                else if (e.ColumnIndex == 3)
                {
                    if (numericValue > 0)
                    {
                        decimal costPriceValue = GetCellValue(e.RowIndex, 2);
                        decimal salePriceValue = GetCellValue(e.RowIndex, 4);

                        if ((costPriceValue > 0 && numericValue < costPriceValue) || (salePriceValue > 0 && numericValue < salePriceValue))
                        {
                            ErrorMsg("M.R.P can not be less than Cost Price or Sale Price");
                            dbgBarDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0.00"; // Reset the cell value
                            // Prevent moving to the next cell
                            dbgBarDet.CurrentCell = dbgBarDet.Rows[e.RowIndex].Cells[e.ColumnIndex - 1];
                        }
                    }
                }
                else if (e.ColumnIndex == 4)
                {
                    if (numericValue > 0)
                    {
                        decimal mrpValue = GetCellValue(e.RowIndex, 3);
                        decimal costPriceValue = GetCellValue(e.RowIndex, 2);

                        if ((mrpValue > 0 && mrpValue < numericValue) || (costPriceValue > 0 && costPriceValue > numericValue))
                        {
                            ErrorMsg("Sale Price can not be greater than M.R.P. or less than Cost Price");
                            dbgBarDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value; // Reset the cell value
                            // Prevent moving to the next cell
                            dbgBarDet.CurrentCell = dbgBarDet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        }
                    }
                }
            }
        }

        private void frmM_Item_FormClosing(object sender, FormClosingEventArgs e)
        {
            
              

            if ( txtItemDesc.Text != "" && dbgBarDet.Rows[0].Cells[3].Value != "0.00")
            {
                dbConnector = new DbConnector();
                // dbConnector.connectionString= new OdbcConnection();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();
                int i = 0;
                //transaction = dbConnector.connection.BeginTransaction();

                string selectid_toenter = "select item_desc from m_item_hdr where item_desc='" + txtItemDesc.Text.Trim() + "'";
                using (OdbcDataReader reader = dbConnector.CreateResultset(selectid_toenter))
                {
                    if (!reader.HasRows)
                    {
                        using (OdbcCommand selectexistdata = new OdbcCommand("SELECT * FROM temp_m_item_hdr WHERE item_desc = ?", dbConnector.connection))
                        {
                            selectexistdata.Parameters.Add(new OdbcParameter("item_desc", txtItemDesc.Text.Trim()));

                            using (OdbcDataReader readerdata = selectexistdata.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                              

                                    string insert = "INSERT INTO temp_m_item_hdr (item_id, item_desc, short_desc, item_type_id, group_id, sub_group_id, sub_sub_group_id,size_id, color_id, style, manuf_id, manuf_name, pur_unit_id, sale_unit_id, conv_pur_sale, op_bal_unit, min_level, re_order_level, max_level, qty_decimal_yn, decimal_upto, sale_tax_paid, cost_price, mrp,     sale_price, bar_yn, active_yn, status, ent_on, ent_by, Trans_status, loc_id, lt, net_rate, disc_per,    HSN_CODE, cess_perc, excis_perc, local_rate_yn, bar_code, disc_yn, closed_yn, comp_name) VALUES" +
                                             " (?, ?, ?, ?, ?, ?, ?, ?, ?, ?," +
                                             " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?," +
                                             " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                                    OdbcCommand cmd = new OdbcCommand(insert, dbConnector.connection);
                                   // cmd.Transaction = transaction;

                        

                                    cmd.CommandText = insert;
                
                                    cmd.Parameters.Add(new OdbcParameter("item_id", i.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("item_desc", txtItemDesc.Text.Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("short_desc", txtShDesc.Text.Trim()));
                                    string type = cboType.SelectedItem != null ? cboType.SelectedItem.ToString().Trim() : string.Empty;
                                    cmd.Parameters.Add(new OdbcParameter("item_type_id", type));

                                    string grp = cboGroup.SelectedItem != null ? cboGroup.SelectedItem.ToString().Trim() : string.Empty;                        
                                    cmd.Parameters.Add(new OdbcParameter("group_id", grp));

                                    string sgrp = cboSGroup.SelectedItem != null ? cboSGroup.SelectedItem.ToString().Trim() : string.Empty;
                                    cmd.Parameters.Add(new OdbcParameter("sub_group_id", sgrp));

                                    string ssgrp = cboSSGroupDesc.SelectedItem != null ? cboSSGroupDesc.SelectedItem.ToString().Trim() : string.Empty;
                                    cmd.Parameters.Add(new OdbcParameter("sub_sub_group_id", ssgrp));

                                    string size = cboSizeDesc.SelectedItem != null? cboSizeDesc.SelectedItem.ToString().Trim() : string.Empty;
                                    cmd.Parameters.Add(new OdbcParameter("size_id", size));

                                    string color = cboColorDesc.SelectedItem != null ? cboColorDesc.SelectedItem.ToString().Trim() : string.Empty;
                                    cmd.Parameters.Add(new OdbcParameter("color_id", color));

                                    cmd.Parameters.Add(new OdbcParameter("style", txtStyle.Text.ToString().Trim()));

                                    string manuf = cboManuf.SelectedItem != null ? cboManuf.SelectedItem.ToString().Trim() : string.Empty;
                                    cmd.Parameters.Add(new OdbcParameter("manuf_id", manuf));
                                    cmd.Parameters.Add(new OdbcParameter("manuf_name", rotManuf.Text.ToString().Trim()));

                                    string purunit = cboPurUnit.SelectedItem != null ? cboPurUnit.SelectedItem.ToString().Trim() : string.Empty;
                                    cmd.Parameters.Add(new OdbcParameter("pur_unit_id", purunit));

                                    string salunit = cboSaleUnit.SelectedItem != null ? cboSaleUnit.SelectedItem.ToString().Trim() : string.Empty;
                                    cmd.Parameters.Add(new OdbcParameter("sale_unit_id", salunit));

                                    cmd.Parameters.Add(new OdbcParameter("conv_pur_sale", txtConv.Text.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("op_bal_unit", txtOpBal.Text.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("min_level", txtMinLevel.Text.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("re_order_level", txtReOLevel.Text.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("max_level", txtMaxLevel.Text.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("qty_decimal_yn", chkDecimal.Checked ? "Y" : "N"));
                                    cmd.Parameters.Add(new OdbcParameter("decimal_upto", txtDecimalupto.Text.ToString().Trim()));

                                    string sltax = cboSaleTax.SelectedItem != null ? cboSaleTax.SelectedItem.ToString().Trim() : string.Empty;
                                    cmd.Parameters.Add(new OdbcParameter("sale_tax_paid", sltax));

                                    cmd.Parameters.Add(new OdbcParameter("cost_price", dbgBarDet.Rows[0].Cells[2].Value.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("mrp", dbgBarDet.Rows[0].Cells[3].Value.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("sale_price", dbgBarDet.Rows[0].Cells[4].Value.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("bar_yn", chkBarYN.Checked ? "Y" : "N"));
                                    cmd.Parameters.Add(new OdbcParameter("active_yn", chkAct.Checked ? "Y" : "N"));
                                    cmd.Parameters.Add(new OdbcParameter("status", "V"));
                                    cmd.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;
                                    cmd.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                                    cmd.Parameters.Add(new OdbcParameter("Trans_status", "N"));

                                    string loc = cboLoc.SelectedItem != null ? cboLoc.SelectedItem.ToString().Trim() : string.Empty;
                                    cmd.Parameters.Add(new OdbcParameter("loc_id", loc));

                                    cmd.Parameters.Add(new OdbcParameter("lt", txtLT.ToString().Trim()));// Required frequency
                                    cmd.Parameters.Add(new OdbcParameter("net_rate", dbgBarDet.Rows[0].Cells[5].Value.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("disc_per", txtDisc.Text.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("HSN_CODE", txtHSN.Text.ToString().Trim()));
                        
                                    string cessValue = string.IsNullOrEmpty(txtCess.Text) ? "0.00" : txtCess.Text.ToString().Trim();
                                    string excisperc = string.IsNullOrEmpty(txtexisper.Text) ? "0.00" : txtexisper.Text.ToString().Trim();

                                    cmd.Parameters.Add(new OdbcParameter("cess_perc", cessValue));
                                    cmd.Parameters.Add(new OdbcParameter("excis_perc", excisperc));
                                    cmd.Parameters.Add(new OdbcParameter("local_rate_yn", "N")); //todo -its for rate can't be change when there will be transfer in.
                                    cmd.Parameters.Add(new OdbcParameter("bar_code", dbgBarDet.Rows[0].Cells[1].Value.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("disc_yn", chkNodisc.Checked? "N":"Y"));
                                    cmd.Parameters.Add(new OdbcParameter("closed_yn", "Y"));
                                    cmd.Parameters.Add(new OdbcParameter("comp_name", DeTools.fOSMachineName.GetMachineName()));
                        

                                    cmd.ExecuteNonQuery();
                                    //transaction.Commit();

                                    //DeTools.SelectDataFromTemporaryTable("m_group");
                                    reader.Close();
                                }
                            }
                        }

                    }
                }

                 string selectid_toenterdet = "select item_desc from m_item_det where item_desc='" + txtItemDesc.Text.Trim() + "'";
                using (OdbcDataReader reader = dbConnector.CreateResultset(selectid_toenterdet))
                {
                    if (!reader.HasRows)
                    {
                        using (OdbcCommand selectexistdata = new OdbcCommand("SELECT * FROM temp_m_item_det WHERE item_desc = ?", dbConnector.connection))
                        {
                            selectexistdata.Parameters.Add(new OdbcParameter("item_desc", txtItemDesc.Text.Trim()));

                            using (OdbcDataReader readerdata = selectexistdata.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {

                                    string insert = "INSERT INTO temp_m_item_det (item_id, item_desc, bar_code, plu, cost_price, mrp, sale_price, active_yn, Trans_status, net_rate, local_rate_yn, closed_yn, comp_name) VALUES" +
                                        " (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                                    OdbcCommand cmd = new OdbcCommand(insert, dbConnector.connection);

                                    // Uncomment the following line if you want to use a transaction
                                    // cmd.Transaction = transaction;

                                    cmd.CommandText = insert;
                                    cmd.Parameters.Add(new OdbcParameter("item_id", i));
                                    cmd.Parameters.Add(new OdbcParameter("item_desc", txtItemDesc.Text.Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("bar_code", dbgBarDet.Rows[0].Cells[1].Value.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("plu", txtItemId.Text.Trim() + dbgBarDet.Rows[0].Cells[0].Value.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("cost_price", dbgBarDet.Rows[0].Cells[2].Value.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("mrp", dbgBarDet.Rows[0].Cells[3].Value.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("sale_price", dbgBarDet.Rows[0].Cells[4].Value.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("active_yn", chkAct.Checked ? "Y" : "N"));
                                    cmd.Parameters.Add(new OdbcParameter("Trans_status", "N"));
                                    cmd.Parameters.Add(new OdbcParameter("net_rate", dbgBarDet.Rows[0].Cells[5].Value.ToString().Trim()));
                                    cmd.Parameters.Add(new OdbcParameter("local_rate_yn", "N")); //todo -its for rate can't be change when there will be transfer in.
                                    cmd.Parameters.Add(new OdbcParameter("closed_yn", "Y"));
                                    cmd.Parameters.Add(new OdbcParameter("comp_name", DeTools.fOSMachineName.GetMachineName()));

                                    //cmd.Parameters.Add(new OdbcParameter("status", "V"));

                                    cmd.ExecuteNonQuery();
                                    // transaction.Commit();

                                    //DeTools.SelectDataFromTemporaryTable("m_group");
                                    reader.Close();
                                }
                            }
                        }
                    }
                }


                i++;
                dbConnector.connection.Close();
            }

        }
    }///////////////////////End

}

