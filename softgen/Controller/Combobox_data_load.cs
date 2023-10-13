using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.Odbc;

namespace softgen
{

    public static class ComboBoxDataLoader
    {
        private static DbConnector dbConnector;

        public static void SetDbConnector(DbConnector connector)
        {
            dbConnector = connector;
        }



        public static void LoadDataIntoComboBox(string tableName, List<string> whereClauseVariable, List<string> whereClauseValue, int op, string selectColumn, ComboBox comboBox)
        {
            string query="";

            if (dbConnector == null)
            {
                throw new InvalidOperationException("DbConnector is not set. Call SetDbConnector method to set the DbConnector object.");
            }

            else
            {

                if (whereClauseVariable.Count != whereClauseValue.Count)
                {
                    throw new ArgumentException("No. of whereClauseVariable and whereClauseValue Not Matched!");

                }

                if (whereClauseVariable[0] == "")
                {
                    query = $"SELECT {selectColumn} FROM {tableName}";

                   

                    DataTable result = dbConnector.ExecuteQuery(query);

                    comboBox.DisplayMember = selectColumn;
                    comboBox.ValueMember = selectColumn;
                    comboBox.DataSource = result;

                }
                else if (whereClauseVariable.Count > 0)
                {
                    query = $"SELECT {selectColumn} FROM {tableName} where ";

                    for (int i = 0; i < whereClauseVariable.Count; i++)
                    {
                        query += $"{whereClauseVariable[i]} = '{whereClauseValue[i]}'";

                        if (i < whereClauseVariable.Count - 1)
                        {
                            if (op == 1)
                            {
                                query += " AND ";
                            }
                            else if (op == 2)
                            {
                                query += " OR ";
                            }


                        }

                    }

                    DataTable result = dbConnector.ExecuteQuery(query);

                    comboBox.DisplayMember = selectColumn;
                    comboBox.ValueMember = selectColumn;
                    comboBox.DataSource = result;

                }
            }

          
        }

        public static string GetcomboValue_in_txt(string tableName, string columnName, List<string> whereClauseVariable, List<string> whereClauseValue,int op,Label label)
        {
            string query = "";
            if (dbConnector == null)
            {
                throw new InvalidOperationException("DbConnector is not set. Call SetDbConnector method to set the DbConnector object.");
            }
            else
            {
                if (whereClauseVariable.Count != whereClauseValue.Count)
                {
                    throw new ArgumentException("No. of whereClauseVariable and whereClauseValue Not Matched!");
                    
                }

                query = $"SELECT {columnName} FROM {tableName} WHERE ";

                for (int i = 0; i < whereClauseVariable.Count; i++)
                {
                    query += $"{whereClauseVariable[i]} = '{whereClauseValue[i]}'";

                    if (i < whereClauseVariable.Count-1)
                    {
                        if (op==1)
                        {
                            query += " AND ";
                        }
                        else if (op==2) 
                        {
                            query += " OR ";
                        }
                        

                    }
                }

                    DataTable result = dbConnector.ExecuteQuery(query);

                if (result.Rows.Count > 0)
                {
                    string value = result.Rows[0][columnName].ToString();
                    label.Text = value;
                    return value;
                }

                label.Text = string.Empty;
                return string.Empty;
            }
        }

    }
}
