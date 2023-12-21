using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Odbc;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;
using Org.BouncyCastle.Crypto;
using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using System.Globalization;
using Org.BouncyCastle.Utilities.Collections;

namespace softgen
{
 
    public class General
    {
        private string gstrSQl;
        private static string gstrSQl1;
        private DbConnector dbConnector;
        public string result;
       

        public bool IsValidCode(string strTable, string strField,string vntValue)
        {
            bool isValid=false;
            dbConnector = new DbConnector();
            
             dbConnector.OpenConnection();

            string sql = $"SELECT {strField} FROM {strTable} WHERE {strField} = @value AND status = 'A'";
            using (OdbcDataReader reader = dbConnector.CreateResultset(sql))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        isValid = true;
                    }
                    reader.Close(); // Close the reader if it's not null.
                }
            }

            return isValid;
        }

        public bool CheckDate(TextBox ctlText)
        {
            if (!string.IsNullOrWhiteSpace(ctlText.Text))
            {
                if (DateTime.TryParse(ctlText.Text,out DateTime date))
                {
                    if (date.Year<2000)
                    {
                        Messages.ErrorMsg("Year Cannot Be Less than 2000.");
                        ctlText.Text=date.ToString("dd/MM/yyyy");
                        return false;
                    }
                    else
                    {
                        ctlText.Text = date.ToString("dd/MM/yyyy");
                        return true;
                    }

                }
                else
                {
                Messages.ErrorMsg("Invalid date!");                    
                }

            }
            return false;

        }

        public string GetDesc(string strTable,string strId_Field,string strDesc_Field,string strType,Object vntId_Value)
        {
            DbConnector dbConnector = new DbConnector();
            string result = string.Empty;

            if (vntId_Value == null || string.IsNullOrWhiteSpace(vntId_Value.ToString()))
            {
                return string.Empty;
            }
            else
            {
                string sql = "SELECT "+strDesc_Field+" from "+strTable+" where "+strId_Field+"=";
                if (strType=="N")
                {
                    sql += vntId_Value;
                }
                else if(strType=="C")
                {
                    sql += "'" + vntId_Value + "'";
                }
                else if( strType=="D") 
                {
                    sql+= "'"+((DateTime)vntId_Value).ToString("yyyy-MM-dd")+"'";
                }

                using(OdbcDataReader reader = dbConnector.CreateResultset(sql)) 
                {
                    if (reader != null)
                    {
                        if (reader.Read())
                        {
                            result = reader[0].ToString().Trim();
                        }
                        else
                        {
                            result = "Value does not Exists in the Master.";
                            Messages.InfoMsg("Value does not Exists in the Master!");
                        }
                        reader.Close();
                    }
                }
            }

            return result;

        }

        public void ValidateColumn(DataGridView dataGridView, int rowIndex, int columnIndex)
        {
            decimal cpValue=0, mrpValue=0, spValue = 0, nrValue = 0;
            // Get the values of adjacent cells
            if (dataGridView.Rows[rowIndex].Cells[3].Value != null)
            {
            cpValue = Convert.ToDecimal(dataGridView.Rows[rowIndex].Cells[2].EditedFormattedValue);
            mrpValue = Convert.ToDecimal(dataGridView.Rows[rowIndex].Cells[3].EditedFormattedValue);
                
            }
            spValue = Convert.ToDecimal(dataGridView.Rows[rowIndex].Cells[4].EditedFormattedValue);
            nrValue = Convert.ToDecimal(dataGridView.Rows[rowIndex].Cells[5].EditedFormattedValue);

            switch (columnIndex)
            {
                case 3: // MRP column
                    if (Convert.ToDecimal(dataGridView.Rows[rowIndex].Cells[2].Value) == cpValue && Convert.ToDecimal(dataGridView.Rows[rowIndex].Cells[2].Value) == mrpValue)
                    {
                    ShowValidationError("mrp should be less than MRP, SP, and NR.", rowIndex, columnIndex);                        
                    }
                    break;

                case 4: // MRP column
                        // Validation logic for MRP column
                    break;
            }

            switch (columnIndex)
            {
                case 2: // CP column
                    break;

                case 3: // MRP column
                        // Validation logic for MRP column
                    break;

                case 4: // SP column
                        // Validation logic for SP column
                    break;

                case 5: // NR column
                        // Validation logic for NR column
                    break;

                    // Add more cases for other columns if needed
            }
        }

        private void ShowValidationError(string message, int rowIndex, int columnIndex)
        {
            frmM_Item m_Item = new frmM_Item();
            MessageBox.Show(message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Set a flag to indicate that validation has occurred for this cell
            m_Item.dbgBarDet.Rows[rowIndex].Cells[columnIndex].Tag = true;
        }

        public string GetuserName(string struserId)
        {
            //for getting unsaved data
            dbConnector = new DbConnector();
            // dbConnector.connectionString= new OdbcConnection();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);


            dbConnector.OpenConnection();
            string result = string.Empty;
            if (struserId.ToUpper().Trim()== "SA")
            {
                return "Super User";
            }
            else if (struserId.ToUpper().Trim() == "")
            {
                return "";
                
            }
            else
            {
                gstrSQl = "Select user_name from m_user where user_id ="+"'"+struserId+"'";

                using (OdbcDataReader reader = dbConnector.CreateResultset(gstrSQl))
                {
                    if (reader != null)
                    {
                        if (reader.Read())
                        {
                            result = reader[0].ToString().Trim();
                        }
                        else
                        {
                            result = "Invalid User.";
                            Messages.WarningMsg("Invalid User!");
                        }
                        reader.Close();
                    }
                }
            }
            return result;

        }

        public object IfNull(Control cntControl) 
        {
            if (string.IsNullOrWhiteSpace(cntControl.Text))
            {
                return null;
            }
            else
            {
                return cntControl.Text.Trim(); 
            }
        }

        public bool CheckId(string strCaption,string strFieldName,string strTableName,string strValue)
        {
            result=string.Empty;

            bool checkId = true;


            gstrSQl = "Select "+strFieldName+" from "+strTableName+" where "+strFieldName+" = "+"'"+strValue.Trim()+"'";

            using(OdbcDataReader reader= dbConnector.CreateResultset(gstrSQl))
            {
                if (!reader.HasRows)
                {
                    checkId= false;
                    // FKNotExistMsg(strCaption, strValue.Trim());
                }
                reader.Close();

            }
            return checkId;
        }
        public bool CheckFromMaster(string strCaption, string strFieldName, string strTableName, string strValue)
        {
            result= string.Empty;
            bool checkFromMaster = true;

            gstrSQl = "Select "+strFieldName.Trim()+" from "+strTableName.Trim()+" where "+strFieldName.Trim()+
                "= "+"'"+strValue.Trim()+"'";

            using(OdbcDataReader reader=dbConnector.CreateResultset(gstrSQl)) 
            { if (!reader.HasRows)
                {
                    checkFromMaster= false;
                    Messages.FKNotExistMsg(strCaption,strValue);
                }
            reader.Close();
            }
            return checkFromMaster;
        }
        
        public bool CheckFromMasterBln(string strCaption, string strFieldName, string strTableName, string strValue)
        {
            result= string.Empty;
            bool checkFromMasterbln = true;

            gstrSQl = "Select "+strFieldName.Trim()+" from "+strTableName.Trim()+" where "+strFieldName.Trim()+
                "= "+"'"+strValue.Trim()+"'";

            using(OdbcDataReader reader=dbConnector.CreateResultset(gstrSQl)) 
            { if (!reader.HasRows)
                {
                    checkFromMasterbln= false;
                    Messages.FKNotExistMsg(strCaption,strValue);
                }
            reader.Close();
            }
            return checkFromMasterbln;
        }

        public bool CheckNumeric(string strValue)
        {
            bool checkNumeric = true;
            int parsed;
            bool success = int.TryParse(strValue, out parsed);

            if (string.IsNullOrEmpty(strValue))
            {
                if (success)
                {
                    Messages.InfoMsg("Value is not numeric. Enter only numeric value.");
                    success= false;
                }
            }
            return success;
        }

        public bool CheckValidValue(ComboBox cmbCntl) 
        {
            bool validValue = false;
            if (!string.IsNullOrWhiteSpace(cmbCntl.Text))
            {
                for (int i = 0; i < cmbCntl.Items.Count; i++)
                {
                    if (string.Equals(cmbCntl.Text.Trim(), cmbCntl.Items[i].ToString().Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        validValue = true;
                        return validValue;
                    }
                }
            }

            Messages.InfoMsg("Select any value from the available list.");
            cmbCntl.Focus();

            return validValue;
        }

        public static string ConvertToWords(string strNumber)
        {
            string strRupees, strPaise, strDigit, strOutput, strValue;
            int intPaise, intDigit,intCount,intLength;

            intPaise = (int)(double.Parse(strNumber)- Math.Floor(double.Parse(strNumber)) * 100);

            if (intPaise > 0)
            {
                strPaise = ConvertTwo(intPaise);
                strPaise = "Paise" + strPaise.Trim();
            }

            else
            {
                strPaise= "";
            }
            strValue = "";
            strOutput = "";

            strRupees= double.Parse(strNumber).ToString();
            intLength= strRupees.Length;
            intCount = 2;

            while (strRupees != null)
            {
                if (intCount==10||intCount==3)
                {
                    strDigit = strRupees.Substring(strRupees.Length - 1);
                }
                else
                {
                    strDigit = strRupees.Substring(Math.Max(strRupees.Length-2,0));
                }

                intDigit = int.Parse(strDigit);
                if (intDigit != 0)
                {
                    switch (intCount)
                    {
                        case 2:
                            strValue = " ";
                            break;
                        case 3:
                        case 10:
                            strValue = " Hundred ";
                            break;
                        case 5:
                        case 12:
                            strValue = " Thousand ";
                            break;
                        case 7:
                        case 14:
                            strValue = " Lakh ";
                            break;
                        case 9:
                            strValue = " Crore ";
                            break;
                        default:
                            strValue = " ";
                            break;
                    }
                }
                else
                {
                    strValue = " ";
                }
                strOutput = ConvertTwo(intDigit) + strValue.Trim() + " " + strOutput.Trim();

                if (intLength > intCount)
                {
                    strRupees = strRupees.Substring(0, intLength - intCount);
                    if (intCount == 9 || intCount == 2)
                    {
                        intCount = intCount + 1;
                    }
                    else
                    {
                        intCount = intCount + 2;
                    }
                }
                else
                {
                    break;
                }
            }

            return "Rs. " + strOutput.Trim() + strPaise + " Only";



        }


        public static string ConvertTwo(int number)
        {
            if (number < 10)
            {
                // Handle single-digit numbers
                switch (number)
                {
                    case 1: return "One";
                    case 2: return "Two";
                    case 3: return "Three";
                    case 4: return "Four";
                    case 5: return "Five";
                    case 6: return "Six";
                    case 7: return "Seven";
                    case 8: return "Eight";
                    case 9: return "Nine";
                    default: return "";
                }
            }
            else if (number >= 10 && number <= 19)
            {
                // Handle numbers from 10 to 19
                switch (number)
                {
                    case 10: return "Ten";
                    case 11: return "Eleven";
                    case 12: return "Twelve";
                    case 13: return "Thirteen";
                    case 14: return "Fourteen";
                    case 15: return "Fifteen";
                    case 16: return "Sixteen";
                    case 17: return "Seventeen";
                    case 18: return "Eighteen";
                    case 19: return "Nineteen";
                    default: return "";
                }
            }
            else
            {
                // Handle numbers from 20 to 99
                int tens = number / 10;
                int ones = number % 10;
                string tensWords = "";
                string onesWords = "";

                switch (tens)
                {
                    case 2: tensWords = "Twenty"; break;
                    case 3: tensWords = "Thirty"; break;
                    case 4: tensWords = "Forty"; break;
                    case 5: tensWords = "Fifty"; break;
                    case 6: tensWords = "Sixty"; break;
                    case 7: tensWords = "Seventy"; break;
                    case 8: tensWords = "Eighty"; break;
                    case 9: tensWords = "Ninety"; break;
                   
                    default: tensWords = "";
                        break;
                }

                onesWords = ConvertTwo(ones); // Recursively convert the ones digit

                // Combine tens and ones words with a hyphen if ones digit is not zero
                return (string.IsNullOrEmpty(onesWords) ? tensWords : tensWords + "-" + onesWords);
            }
        }

        public bool DayClose(string dmDate)
        {
            gstrSQl = "Select * from r_closing where doc_type_id <= 'INV' and " +"'"+dmDate+"'" ;
            dbConnector = new DbConnector();

            dbConnector.OpenConnection();

            using (OdbcDataReader reader = dbConnector.CreateResultset(gstrSQl))
            {
                
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    reader.Close();
                
            }

            gstrSQl = "Insert into r_closing (doc_type_id,cls_dt) values ('INV', ?)";
            int rowafftected= dbConnector.ExecuteNonQuery(gstrSQl);

            return rowafftected > 0;
        }

        public void FillCombo(ComboBox combo, string fieldName, string tableName,  bool forReport, string criteria = "", string condition = "")
        {
            try
            {
                string sql;
                DbConnector dbConnector = new DbConnector();

                if (criteria=="")
                {
                sql = "SELECT DISTINCT " + fieldName + " FROM " + tableName;
                    
                }
                else
                {
                sql = "SELECT DISTINCT " + fieldName + " FROM " + tableName + " WHERE " + fieldName + " LIKE " + criteria;
                    
                }
                
                if (!string.IsNullOrEmpty(condition))
                {
                    sql += " AND " + condition;
                }
                sql += " ORDER BY " + fieldName;

                using (OdbcDataReader reader = dbConnector.CreateResultset(sql))
                {
                    string oldValue = Trim(combo.Text);
                    combo.Items.Clear();

                    if (!reader.HasRows && forReport)
                    {
                        combo.Items.Add("ALL");
                    }

                    while (reader.Read())
                    {
                        combo.Items.Add(Trim(reader[fieldName].ToString()));
                     
                    }

                    if (!forReport)
                    {
                        combo.Text = oldValue;
                    }
                        combo.MaxDropDownItems = 5;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or display an error message
                Console.WriteLine(ex.Message);
            }
        }

        private string Trim(string value)
        {
            return value?.Trim();
        }


        public void FillDate(DateTimePicker ctlFromDate, DateTimePicker ctlToDate)
        {
            try
            {
            dbConnector= new DbConnector();

            gstrSQl = "SELECT start_date, end_date FROM m_fin_year WHERE @CurrentDate >= start_date AND @CurrentDate < end_date";

            using (OdbcDataReader reader = dbConnector.CreateResultset(gstrSQl))
            {
                if (reader.Read())
                {
                    ctlFromDate.Value = (DateTime)reader["start_date"];
                    ctlToDate.Value = (DateTime)reader["end_date"];
                }
            }

            }

            catch (Exception ex)
            {
                // Handle the exception or display an error message
                Console.WriteLine(ex.Message);
                
            }
        }

        public void FillSSubGroup(ComboBox combo,string strGId)
        {
            try
            {

            dbConnector = new DbConnector();
            gstrSQl = "Select sub_sub_group_id from m_sub_sub_group where sub_group_id= "+"'"+strGId+"'";

            using(OdbcDataReader reader= dbConnector.CreateResultset(gstrSQl)) 
            { 
                combo.Items.Clear();
                
                if (reader.HasRows) 
                {
                    while (reader.Read())
                    {
                        combo.Items.Add(reader["sub_sub_group_id"].ToString());
                    }
                }
                    

                    reader.Close();
            }
            }
            
            catch (Exception ex)
            {

                // Handle the exception or display an error message
                Console.WriteLine(ex.Message);
            }

        }

        public void FillSubGroup(ComboBox combo,string strGId)
        {
            try
            {
            dbConnector = new DbConnector();
            gstrSQl = "SELECT sub_group_id FROM m_sub_group WHERE group_id = "+"'"+strGId+"'";

            using(OdbcDataReader reader= dbConnector.CreateResultset(gstrSQl)) 
            { 
                combo.Items.Clear();
                
                if (reader.HasRows) 
                {
                    while (reader.Read())
                    {
                        combo.Items.Add(reader["sub_group_id"].ToString());
                    }
                }
                
                reader.Close();
            }

            }
            
            catch (Exception ex)
            {

                // Handle the exception or display an error message
                Console.WriteLine(ex.Message);
            }

        }

        public void FillValues(string strSQl,Control objControl)
        {
            dbConnector = new DbConnector();

            using(OdbcDataReader reader= dbConnector.CreateResultset(strSQl))
            {
                   ComboBox combo1= objControl as ComboBox;
                if (combo1!= null)
                {

                combo1.Items.Clear();
                    
                }
                while (reader.Read())
                {
                    //combo1.Items.Add(reader[0].ToString());

                    combo1.Items.Add(reader[0].ToString());
                }
            }

        }

        public string FinYear(DateTime dtmDate)
        {
            dbConnector = new DbConnector();

            string Finyr = "";

            gstrSQl = "SELECT fin_year_id FROM m_fin_year WHERE start_date <='"+dtmDate+"' AND end_date >= '"+dtmDate+"' AND yr_clos = 'O' AND status = 'A'";
            
            using(OdbcDataReader reader= dbConnector.CreateResultset(gstrSQl))
            {
                if (reader.Read())
                {
                    Finyr= reader[0].ToString();
                }
            }

            return Finyr;
        }

        public static string FormatDate(object dateValue)
        {
            if (dateValue is DateTime)
            {
                return ((DateTime)dateValue).ToString("dd/MM/yyyy");
            }
            return string.Empty; // Return an empty string for invalid input
        }

        public static double GenDocno(string strDocTypeId,DateTime dtmDate)
        {
            int curDocNo = 0;
            int intFlag = 1;


            try
            {
                DbConnector dbConnector = new DbConnector();

                gstrSQl1 = "SELECT next_no FROM m_doc_series " +
                     "WHERE doc_type_id = @strDocTypeId " +
                     "AND from_date <= @dtmDate " +
                     "AND to_date >= @dtmDate " +
                     "AND end_no >= next_no AND status = 'A'";

                using (OdbcDataReader reader = dbConnector.CreateResultset(gstrSQl1))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        curDocNo = Convert.ToInt32(reader["next_no"]);

                        Console.WriteLine("Next No is:" + curDocNo);
                        intFlag = 2;

                        gstrSQl1 = "UPDATE m_doc_series SET next_no = next_no + 1 " +
                    "WHERE doc_type_id = @strDocTypeId " +
                    "AND from_date <= @dtmDate " +
                    "AND to_date >= @dtmDate " +
                    "AND end_no >= next_no AND status = 'A'";
                        dbConnector.ExecuteNonQuery(gstrSQl1);
                    }
                }
                if (intFlag == 2)
                {
                    // If the flag is 2, it means we opened the resultset, so we need to close it
                    dbConnector.CloseConnection();
                }
            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Messages.ErrorMsg("Error On GenDocNo.");
                
            }
            intFlag = 0;
            return curDocNo;
        }
        
        

        public string FormatCurrency(string s_Currency)
        {
            
                decimal currencyValue;
                if (decimal.TryParse(s_Currency, out currencyValue))
                {
                    // Format the decimal value with thousands separators and three decimal places
                    return currencyValue.ToString("#,##0.000");
                }
                else
                {
                    // Handle the case where s_Currency is not a valid decimal
                    return "Invalid currency format";
                }
            
        }

        public static double GenMDocno(string strDocTypeId)
        {
            int curDocNo = 0;
            int intFlag = 1;


            try
            {
                DbConnector dbConnector = new DbConnector();

                gstrSQl1 = "SELECT next_no FROM m_doc_series " +
                     "WHERE doc_type_id = '"+strDocTypeId.Trim()+"' AND status = 'A'";

                using (OdbcDataReader reader = dbConnector.CreateResultset(gstrSQl1))
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        curDocNo = Convert.ToInt32(reader["next_no"]);

                        Console.WriteLine("Next No is:" + curDocNo);
                        intFlag = 2;

                        gstrSQl1 = "UPDATE m_doc_series SET next_no = next_no + 1 " +
                                   "WHERE doc_type_id = '"+strDocTypeId.Trim()+"' AND status = 'A'";
                        dbConnector.ExecuteNonQuery(gstrSQl1);
                    }
                }
                if (intFlag == 2)
                {
                    // If the flag is 2, it means we opened the resultset, so we need to close it
                    dbConnector.CloseConnection();
                }
            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Messages.ErrorMsg("Error On GenMDocNo.");

            }
            intFlag = 0;
            return curDocNo;
        }

        public string GetPassword()
        {
            string decryptedPassword="";
            DbConnector dbConnector= new DbConnector();

            dbConnector.OpenConnection();

            gstrSQl = "SELECT Password FROM s_Login WHERE Login_id = ?";

            string login = DeTools.gstrloginId;

            OdbcParameter loginIdParam = new OdbcParameter("@LoginId", login);

            using (OdbcDataReader reader= dbConnector.ExecuteReader(gstrSQl,new OdbcParameter[] {loginIdParam})) 
            {
                if(reader.Read())
                {
                    string encryptedPassword = reader["Password"].ToString();
                    decryptedPassword = DeTools.Decrypt(encryptedPassword);
                }
                
            }
            return decryptedPassword;
        }

        public static void InitialiseSetup(string LoginId, string ServerName)
        {
            DbConnector dbConnector = new DbConnector();
            dbConnector.OpenConnection();

            DeTools.gstrsetup[0] = LoginId;
            DeTools.gstrsetup[1]= ServerName.Trim();

            DeTools.strBrand = "RED APRON PVT.LTD.";
            DeTools.strCompany = "";
            DeTools.strAddress1 = "Shop No.2,FF,ATS One Hamlet,";
            DeTools.strAddress2 = "Sector-104,NOIDA(U.P)";
            DeTools.strAddress3 = "";

            gstrSQl1 = "SELECT * FROM s_company";

            using(OdbcDataReader reader= dbConnector.CreateResultset(gstrSQl1))
            {
                if (reader.Read())
                {
                   DeTools.strBranch = reader["branch_id"].ToString();
                   DeTools.strTin = reader["tin_no"].ToString();
                   DeTools.strLst = reader["lst"].ToString();
                   DeTools.strCst = reader["cst"].ToString();
                    DeTools.strPhone = "Phone: " + reader["phone"].ToString();
                    DeTools.strNote1 = reader["note1"].ToString();
                    DeTools.strNote2 = reader["note2"].ToString();
                    DeTools.strNote3 = reader["note3"].ToString();
                    DeTools.strNote4 = reader["note4"].ToString();
                }
            }


        }

        public string MakeBookmark(int Index)
        {
            // Convert the numeric index to a string with a leading space for the sign
            return " " + Index.ToString();
        }

        public static void MasterParam(string strFieldName1,string strFieldName2,string strTableName,out string chkfld,out int chksn)
        {
            chkfld = string.Empty;
            chksn = 0;


            DbConnector dbConnector = new DbConnector();

            dbConnector.OpenConnection();

            gstrSQl1 = "SELECT " + strFieldName1 + ", " + strFieldName2 + " FROM " + strTableName;


            using (OdbcDataReader reader=dbConnector.CreateResultset(gstrSQl1))
            { if (reader.HasRows) 
                {
                    chkfld = reader[strFieldName1].ToString();
                    chksn = Convert.ToInt32(reader[strFieldName2]);
                }
            }

        }
        public static string NormalDate(string date)
        {
            
            DateTime datetime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return datetime.ToString();
        }

        public static string SQLDate(string date)
        {
            DateTime datetime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return datetime.ToString();
        }

        public static string MySQLDate(string date)
        {
            DateTime datetime = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return datetime.ToString();
        }

        public static int NumericChars(int KeyAscii) 
        {
            if ((KeyAscii < 48 || KeyAscii > 57) && KeyAscii != 46 && KeyAscii != 8)
            {
                return 0;
            }
            else
            {
                return KeyAscii;
            }
        }

        public void RequiredShadow(string strReqdFor, Control objControl, Panel shpControl)
        {
            // Define the shadow color based on the value of strReqdFor
            Color shadowColor = (strReqdFor == "S") ? Color.Red : Color.Blue;

            // Set the background and border colors of the panel (shadow)
            shpControl.BackColor = shadowColor;
            shpControl.BorderStyle = BorderStyle.FixedSingle; // To mimic the border effect

            // Set the dimensions and position of the shadow
            shpControl.Width = objControl.Width + 30;
            shpControl.Height = objControl.Height + 30;
            shpControl.Location = new Point(objControl.Left - 15, objControl.Top - 15);

            // Make the shadow visible
            shpControl.Visible = true;
        }

        //public double StocksPosition(string Item_id, string AsOn)
        //{
        //    double result = 0;

        //    DbConnector dbConnector = new DbConnector();
        //    dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
        //    dbConnector.connection.Open();

        //    //dbConnector.OpenConnection();

        //        DateTime AsOnDate;
        //        string gstrSQL = "{ Call GetItemStocks(?,?,?) }";

        //            OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection);
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.Add("RETURN_VALUE", OdbcType.Double).Direction = ParameterDirection.ReturnValue;
        //            command.Parameters.Add("@Item_id", OdbcType.NVarChar).Value = Item_id;
        //            command.Parameters.Add("@AsOn", OdbcType.NVarChar).Value = MySQLDate(AsOn);
        //            command.Parameters.Add("@Stocks", OdbcType.Double).Direction = ParameterDirection.Output;

        //            command.ExecuteNonQuery();

        //            if (command.Parameters["@Stocks"].Value != DBNull.Value)
        //            {
        //                result = Convert.ToDouble(command.Parameters["@Stocks"].Value);
        //            }


        //        dbConnector.CloseConnection();


        //        return result;

        //}

        public double StocksPosition(string Item_id, string AsOn)
        {
            double result = 0;
            double cl_bal = 0;

            DbConnector dbConnector = new DbConnector();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

            try
            {
                dbConnector.connection.Open();

                string gstrSQL = "{ CALL GetItemStocks(?, ?) }";

                using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Item_id", Item_id);
                    command.Parameters.AddWithValue("@AsOn", MySQLDate(AsOn));

                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Assuming 'cl_bal' is a column in your result set
                                cl_bal = Convert.ToDouble(reader["cl_bal"]);
                                // Process other columns as needed
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, or throw it as needed.
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }

            return result;
        }


        public void CenterForm(Form form)
        {
            if (form.MdiParent != null) // Check if the form is an MDI child
            {
                if (form.Width < form.MdiParent.ClientSize.Width)
                {
                    form.Left = (form.MdiParent.ClientSize.Width - form.Width) / 2;
                }
                else
                {
                    form.Left = 0;
                }

                if (form.Height < form.MdiParent.ClientSize.Height)
                {
                    form.Top = (form.MdiParent.ClientSize.Height - form.Height) / 2;
                }
                else
                {
                    form.Top = 0;
                }
            }
            else
            {
                form.Left = (Screen.PrimaryScreen.Bounds.Width - form.Width) / 2;
                form.Top = (Screen.PrimaryScreen.Bounds.Height - form.Height) / 2;
            }
        }
        /////////////////////////////////

    }
}
