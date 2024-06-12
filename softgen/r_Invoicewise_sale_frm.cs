using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.Odbc;
using static softgen.General;
using System.Data.Common;

namespace softgen
{
    public partial class r_Invoicewise_sale_frm : Form
    {
        private string Brand;
        public string form2inv_no;
        public string inv_dt;
        public string bill_time;
        public string cust_name;
        public string cust_id;
        public string cust_address;
        public decimal mrptot;
        public string totamt;
        public string yousavers;
        public string amttopay;
        public string cashier;
        //public string srno;
        public string desc;
        public decimal qty;
        public decimal totqty;
        public decimal mrp;
        public decimal sp;
        public decimal netamount;
        public string gst;
        public string disc;
        public string discamt;
        public string gstamt;
        public string fromDt;
        public string Todt;

        public r_Invoicewise_sale_frm()
        {
            InitializeComponent();
            // LoadReport();
            GenerateAndDisplayReport();
            
            this.invwisesalerpt.RefreshReport();
        }

        //private void LoadReport()
        //{
        //    string strBrand = DeTools.strBrand;
        //    string strCompany = DeTools.strCompany;
        //    string strAddress1 = DeTools.strAddress1;
        //    string strAddress2 = DeTools.strAddress2;
        //    string strAddress3 = DeTools.strCompany;
        //    string phoneno = DeTools.strPhone;
        //    string note1 = DeTools.strNote1;
        //    string note2 = DeTools.strNote2;
        //    string note3 = DeTools.strNote3;
        //    string note4 = DeTools.strNote4;
        //    string lst = DeTools.strLst;
        //    string cst = DeTools.strCst;
        //    string tin = DeTools.strTin;
        //    string branch = DeTools.strBranch;

           

        //        // Set the path of the RDLC report file to the ReportViewer
        //        this.invwisesalerpt.LocalReport.ReportPath = "Reports/Ledgers/InvReport.rdlc";

        //        // Set report parameters
        //       ReportParameter[] Parameters = new ReportParameter[]
        //       {
        //            new ReportParameter("Brand", strBrand),
        //            new ReportParameter("strCompany", strCompany),
        //            new ReportParameter("address1", strAddress1),
        //            new ReportParameter("address2", strAddress2),
        //            new ReportParameter("address3", strAddress3),
        //            new ReportParameter("phoneno", phoneno),
        //            new ReportParameter("note1", note1),
        //            new ReportParameter("note2", note2),
        //            new ReportParameter("note3", note3),
        //            new ReportParameter("note4", note4),
        //            new ReportParameter("lst", lst),
        //            new ReportParameter("cst", cst),
        //            new ReportParameter("tin", tin),
        //            new ReportParameter("branch", branch),
                   
        //        };
        //        this.invwisesalerpt.LocalReport.SetParameters(Parameters);

        //        GenerateAndDisplayReport();


        //        // Refresh the ReportViewer to display the report
        //        this.invwisesalerpt.RefreshReport();
            
           
        //}

        public void GenerateAndDisplayReport()
        {
            // Create a DataTable to hold the data from the database
            DataTable dataTable = new DataTable();
            frmR_invoice_wise_sale_rpt invoice_Wise_Sale_Rpt = new frmR_invoice_wise_sale_rpt();

            //// Create an instance of your database connector
            //DbConnector dbConnector = new DbConnector();
            //dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

            try
            {
                //// Open the database connection
                //dbConnector.connection.Open();                
                //string formattedFDate = invoice_Wise_Sale_Rpt.GetFormattedFDate();
                //string formattedTDate = invoice_Wise_Sale_Rpt.GetFormattedTDate();
                //// Define your SQL query (stored procedure)
                //string gstrSQL = "{ CALL r_invoice_sale(?,?,?) }";

                //// Create a command to execute the stored procedure
                //using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
                //{
                //    command.CommandType = CommandType.StoredProcedure;

                //    // Add parameters to the stored procedure
                //    command.Parameters.Add(new OdbcParameter("F_Date", OdbcType.VarChar)).Value = formattedFDate;
                //    command.Parameters.Add(new OdbcParameter("T_Date", OdbcType.VarChar)).Value = formattedTDate;
                //    if (frmR_invoice_wise_sale_rpt.Paymchecked_yn == "Y")
                //    {
                //        command.Parameters.Add(new OdbcParameter("PayMode", OdbcType.VarChar)).Value = invoice_Wise_Sale_Rpt.selectedPaymItem.Trim();

                //    }
                //    else if (frmR_invoice_wise_sale_rpt.Paymchecked_yn == "N")
                //    {
                //        command.Parameters.Add(new OdbcParameter("PayMode", OdbcType.VarChar)).Value = "";
                //    }

                //    // Execute the command and fill the DataTable
                //    using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                //    {
                //        adapter.Fill(dataTable);
                //    }
                //}

                //// Set the processing mode for the ReportViewer to Local
                //invwisesalerpt.ProcessingMode = ProcessingMode.Local;

                // Construct the path of the rdlc file dynamically
                if (frmR_invoice_wise_sale_rpt.Paymchecked_yn == "Y" || !string.IsNullOrEmpty(frmR_invoice_wise_sale_rpt.selectedPaymItem.ToString().Trim()))
                {



                    this.invwisesalerpt.LocalReport.ReportPath = "Reports/Ledgers/r_invoicewise_sale.rdlc";



                    string Brand = DeTools.strBrand;
                    string strCompany = DeTools.strCompany;
                    string strAddress1 = DeTools.strAddress1;
                    string strAddress2 = DeTools.strAddress2;
                    string strAddress3 = DeTools.strCompany;
                    string phoneno = DeTools.strPhone;
                    string note1 = DeTools.strNote1;
                    string note2 = DeTools.strNote2;
                    string note3 = DeTools.strNote3;
                    string note4 = DeTools.strNote4;
                    string lst = DeTools.strLst;
                    string cst = DeTools.strCst;
                    string tin = DeTools.strTin;
                    string branch = DeTools.strBranch;
                    string fromdate = frmR_invoice_wise_sale_rpt.FromDt;
                    string todate = frmR_invoice_wise_sale_rpt.Todt;
                    string rundate = DateTime.Now.ToString("dd/MM/yyyy");
                    // Add the report parameters
                    ReportParameter[] reportParameters = new ReportParameter[]
                    {
                    new ReportParameter("Brand", Brand),
                    new ReportParameter("strCompany", strCompany),
                    new ReportParameter("address1", strAddress1),
                    new ReportParameter("address2", strAddress2),
                    new ReportParameter("address3", strAddress3),
                    new ReportParameter("phoneno", phoneno),
                    new ReportParameter("lst", lst),
                    new ReportParameter("cst", cst),
                    new ReportParameter("tin", tin),
                    new ReportParameter("branch", branch),
                    new ReportParameter("fromdate", fromdate),
                    new ReportParameter("todate", todate),
                    new ReportParameter("rundate", rundate)


                    };
                    invwisesalerpt.LocalReport.SetParameters(reportParameters);
                    GenerateAndDisplayReportr_invoice_sale();

                    // Refresh the ReportViewer to display the report
                    invwisesalerpt.RefreshReport();

                    DbConnector dbConnector = new DbConnector();
                    dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                    dbConnector.OpenConnection();
                    if (dbConnector != null)
                    {
                        string delSQL = "Delete FROM Sale";

                        using (OdbcCommand delfrminvtbl = new OdbcCommand(delSQL, dbConnector.connection))
                        {
                            delfrminvtbl.ExecuteNonQuery();
                        }
                        dbConnector.connection.Close();
                    }
                }

                else if (frmR_invoice_wise_sale_rpt.Gstdetchecked_yn=="Y")
                {


                    this.invwisesalerpt.LocalReport.ReportPath = "Reports/Ledgers/r_vat_sale.rdlc";



                    string Brand = DeTools.strBrand;
                    string strCompany = DeTools.strCompany;
                    string strAddress1 = DeTools.strAddress1;
                    string strAddress2 = DeTools.strAddress2;
                    string strAddress3 = DeTools.strCompany;
                    string phoneno = DeTools.strPhone;
                    string note1 = DeTools.strNote1;
                    string note2 = DeTools.strNote2;
                    string note3 = DeTools.strNote3;
                    string note4 = DeTools.strNote4;
                    string lst = DeTools.strLst;
                    string cst = DeTools.strCst;
                    string tin = DeTools.strTin;
                    string branch = DeTools.strBranch;
                    string fromdate = frmR_invoice_wise_sale_rpt.FromDt;
                    string todate = frmR_invoice_wise_sale_rpt.Todt;
                    string rundate = DateTime.Now.ToString("dd/MM/yyyy");
                    // Add the report parameters
                    ReportParameter[] reportParameters1 = new ReportParameter[]
                    {
                    new ReportParameter("Brand", Brand),
                    new ReportParameter("strCompany", strCompany),
                    new ReportParameter("address1", strAddress1),
                    new ReportParameter("address2", strAddress2),
                    new ReportParameter("address3", strAddress3),
                    new ReportParameter("phoneno", phoneno),
                    new ReportParameter("lst", lst),
                    new ReportParameter("cst", cst),
                    new ReportParameter("tin", tin),
                    new ReportParameter("branch", branch),
                    new ReportParameter("fromdate", fromdate),
                    new ReportParameter("todate", todate),
                    new ReportParameter("rundate", rundate)


                    };

                    invwisesalerpt.LocalReport.SetParameters(reportParameters1);
                    GenerateAndDisplayReportr_vat_sale();

                    // Refresh the ReportViewer to display the report
                    invwisesalerpt.RefreshReport();

                    DbConnector dbConnector = new DbConnector();
                    dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                    dbConnector.OpenConnection();
                    if (dbConnector != null)
                    {
                        string delSQL = "Delete FROM vatsale";

                        using (OdbcCommand delfrminvtbl = new OdbcCommand(delSQL, dbConnector.connection))
                        {
                            delfrminvtbl.ExecuteNonQuery();
                        }
                        dbConnector.connection.Close();
                    }
                }


                else if (frmR_invoice_wise_sale_rpt.ItemGstdetchecked_yn=="Y")
                {


                    this.invwisesalerpt.LocalReport.ReportPath = "Reports/Ledgers/r_vat_item_sale.rdlc";



                    string Brand = DeTools.strBrand;
                    string strCompany = DeTools.strCompany;
                    string strAddress1 = DeTools.strAddress1;
                    string strAddress2 = DeTools.strAddress2;
                    string strAddress3 = DeTools.strCompany;
                    string phoneno = DeTools.strPhone;
                    string note1 = DeTools.strNote1;
                    string note2 = DeTools.strNote2;
                    string note3 = DeTools.strNote3;
                    string note4 = DeTools.strNote4;
                    string lst = DeTools.strLst;
                    string cst = DeTools.strCst;
                    string tin = DeTools.strTin;
                    string branch = DeTools.strBranch;
                    string fromdate = frmR_invoice_wise_sale_rpt.FromDt;
                    string todate = frmR_invoice_wise_sale_rpt.Todt;
                    string rundate = DateTime.Now.ToString("dd/MM/yyyy");
                    // Add the report parameters
                    ReportParameter[] reportParameters2 = new ReportParameter[]
                    {
                    new ReportParameter("Brand", Brand),
                    new ReportParameter("strCompany", strCompany),
                    new ReportParameter("address1", strAddress1),
                    new ReportParameter("address2", strAddress2),
                    new ReportParameter("address3", strAddress3),
                    new ReportParameter("phoneno", phoneno),
                    new ReportParameter("lst", lst),
                    new ReportParameter("cst", cst),
                    new ReportParameter("tin", tin),
                    new ReportParameter("branch", branch),
                    new ReportParameter("fromdate", fromdate),
                    new ReportParameter("todate", todate),
                    new ReportParameter("rundate", rundate)


                    };

                    invwisesalerpt.LocalReport.SetParameters(reportParameters2);
                    GenerateAndDisplayReportr_vat_item_sale();

                    // Refresh the ReportViewer to display the report
                    invwisesalerpt.RefreshReport();

                    DbConnector dbConnector = new DbConnector();
                    dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                    dbConnector.OpenConnection();
                    if (dbConnector != null)
                    {
                        string delSQL = "Delete FROM vat_itemsale";

                        using (OdbcCommand delfrminvtbl = new OdbcCommand(delSQL, dbConnector.connection))
                        {
                            delfrminvtbl.ExecuteNonQuery();
                        }
                        dbConnector.connection.Close();
                    }
                }
                
                else if (frmR_invoice_wise_sale_rpt.InvGstdetchecked_yn=="Y")
                {


                    this.invwisesalerpt.LocalReport.ReportPath = "Reports/Ledgers/r_inv_gst_sale.rdlc";



                    string Brand = DeTools.strBrand;
                    string strCompany = DeTools.strCompany;
                    string strAddress1 = DeTools.strAddress1;
                    string strAddress2 = DeTools.strAddress2;
                    string strAddress3 = DeTools.strCompany;
                    string phoneno = DeTools.strPhone;
                    string note1 = DeTools.strNote1;
                    string note2 = DeTools.strNote2;
                    string note3 = DeTools.strNote3;
                    string note4 = DeTools.strNote4;
                    string lst = DeTools.strLst;
                    string cst = DeTools.strCst;
                    string tin = DeTools.strTin;
                    string branch = DeTools.strBranch;
                    string fromdate = frmR_invoice_wise_sale_rpt.FromDt;
                    string todate = frmR_invoice_wise_sale_rpt.Todt;
                    string rundate = DateTime.Now.ToString("dd/MM/yyyy");
                    // Add the report parameters
                    ReportParameter[] reportParameters3 = new ReportParameter[]
                    {
                    new ReportParameter("Brand", Brand),
                    new ReportParameter("strCompany", strCompany),
                    new ReportParameter("address1", strAddress1),
                    new ReportParameter("address2", strAddress2),
                    new ReportParameter("address3", strAddress3),
                    new ReportParameter("phoneno", phoneno),
                    new ReportParameter("lst", lst),
                    new ReportParameter("cst", cst),
                    new ReportParameter("tin", tin),
                    new ReportParameter("branch", branch),
                    new ReportParameter("fromdate", fromdate),
                    new ReportParameter("todate", todate),
                    new ReportParameter("rundate", rundate)


                    };

                    invwisesalerpt.LocalReport.SetParameters(reportParameters3);
                    GenerateAndDisplayReportr_inv_gst_sale();

                    // Refresh the ReportViewer to display the report
                    invwisesalerpt.RefreshReport();

                    DbConnector dbConnector = new DbConnector();
                    dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                    dbConnector.OpenConnection();
                    if (dbConnector != null)
                    {
                        string delSQL = "Delete FROM r_inv_gst_sale";

                        using (OdbcCommand delfrminvtbl = new OdbcCommand(delSQL, dbConnector.connection))
                        {
                            delfrminvtbl.ExecuteNonQuery();
                        }
                        dbConnector.connection.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
           
        }

        //private void GenerateAndDisplayReportr_invoice_sale()
        //{
        //    // Create a DataTable to hold the data from the database
        //    DataTable dataTable = new DataTable();

        //    // Create an instance of your database connector
        //    DbConnector dbConnector = new DbConnector();
        //    dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

        //    // Open the database connection
        //    dbConnector.connection.Open();
        //    frmR_invoice_wise_sale_rpt invoice_Wise_Sale_Rpt = new frmR_invoice_wise_sale_rpt();
        //    //string formattedFDate = invoice_Wise_Sale_Rpt.GetFormattedFDate();
        //    //string formattedTDate = invoice_Wise_Sale_Rpt.GetFormattedTDate();

        //    // Define your SQL query
        //    string gstrSQL = "{ CALL r_invoice_sale(?,?,?) }";
        //    //long documentNumber = DocumentManager.DocumentNumber;
        //    //string inv_no = documentNumber.ToString();

        //    // Create a command to execute the stored procedure
        //    using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;

        //        // Add parameters to the command
        //        command.Parameters.AddWithValue("@F_Date", frmR_invoice_wise_sale_rpt.FromDt_mysql);
        //        command.Parameters.AddWithValue("@T_Date", frmR_invoice_wise_sale_rpt.Todt_mysql);
        //        if (frmR_invoice_wise_sale_rpt.Paymchecked_yn == "Y")
        //        {
        //            command.Parameters.AddWithValue("@PayMode", invoice_Wise_Sale_Rpt.selectedPaymItem.Trim());

        //        }
        //        else if (frmR_invoice_wise_sale_rpt.Paymchecked_yn == "N")
        //        {
        //            command.Parameters.AddWithValue("@PayMode", "");
        //        }


        //        // Execute the command and read the data into the DataTable
        //        using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
        //        {
        //            adapter.Fill(dataTable);
        //        }
        //    }

        //    // Close the database connection
        //    dbConnector.connection.Close();          

        //    // Set the report's DataSources property to a new list containing your DataTable
        //    this.invwisesalerpt.LocalReport.DataSources.Clear();
        //    this.invwisesalerpt.LocalReport.DataSources.Add(new ReportDataSource("invoiceSalePaym", dataTable));


        //    // Refresh the report
        //    this.invwisesalerpt.RefreshReport();
        //}


        private void GenerateAndDisplayReportr_invoice_sale()
        {
            try
            {
                // Create a DataTable to hold the data from the database
                DataTable dataTable = new DataTable();

                // Create an instance of your database connector
                DbConnector dbConnector = new DbConnector();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

                // Open the database connection
                dbConnector.connection.Open();

                frmR_invoice_wise_sale_rpt invoice_Wise_Sale_Rpt = new frmR_invoice_wise_sale_rpt();

                // Define your SQL query
                string gstrSQL = "{ CALL r_invoice_sale(?,?,?) }";

                // Create a command to execute the stored procedure
                using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@F_Date", frmR_invoice_wise_sale_rpt.FromDt_mysql);
                    command.Parameters.AddWithValue("@T_Date", frmR_invoice_wise_sale_rpt.Todt_mysql);

                    if (frmR_invoice_wise_sale_rpt.Paymchecked_yn == "Y")
                    {
                        command.Parameters.AddWithValue("@PayMode", "");
                    }
                   
                    else if (frmR_invoice_wise_sale_rpt.Paymchecked_yn == "N" && !string.IsNullOrEmpty(frmR_invoice_wise_sale_rpt.selectedPaymItem.ToString().Trim()))
                    {
                        command.Parameters.AddWithValue("@PayMode", frmR_invoice_wise_sale_rpt.selectedPaymItem);
                    }
                   
                    // Execute the command and read the data into the DataTable
                    using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                // Close the database connection
                dbConnector.connection.Close();

                // Check if the DataTable contains data
                if (dataTable.Rows.Count > 0)
                {
                    // Set the report's DataSources property to a new list containing your DataTable
                    //this.invwisesalerpt.LocalReport.DataSources.Clear();
                    invwisesalerpt.LocalReport.DataSources.Clear();
                    this.invwisesalerpt.LocalReport.DataSources.Add(new ReportDataSource("invoiceSalePaym", dataTable));

                    // Refresh the report
                    this.invwisesalerpt.RefreshReport();
                }
                //else
                //{
                //    MessageBox.Show("No data found for the given criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


         private void GenerateAndDisplayReportr_vat_sale()
         {
            try
            {
                // Create a DataTable to hold the data from the database
                DataTable dataTable1 = new DataTable();

                // Create an instance of your database connector
                DbConnector dbConnector = new DbConnector();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

                // Open the database connection
                dbConnector.connection.Open();

                frmR_invoice_wise_sale_rpt invoice_Wise_Sale_Rpt = new frmR_invoice_wise_sale_rpt();

                // Define your SQL query
                string gstrSQL = "{ CALL r_vat_sale(?,?) }";

                // Create a command to execute the stored procedure
                using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@F_Date", frmR_invoice_wise_sale_rpt.FromDt_mysql);
                    command.Parameters.AddWithValue("@T_Date", frmR_invoice_wise_sale_rpt.Todt_mysql);                    
                   
                    // Execute the command and read the data into the DataTable
                    using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                    {
                        adapter.Fill(dataTable1);
                    }
                }

                // Close the database connection
                dbConnector.connection.Close();

                // Check if the DataTable contains data
                if (dataTable1.Rows.Count > 0)
                {
                    // Set the report's DataSources property to a new list containing your DataTable
                    //this.invwisesalerpt.LocalReport.DataSources.Clear();
                    invwisesalerpt.LocalReport.DataSources.Clear();
                    this.invwisesalerpt.LocalReport.DataSources.Add(new ReportDataSource("vatsaleDataset", dataTable1));

                    // Refresh the report
                    this.invwisesalerpt.RefreshReport();
                }
                //else
                //{
                //    MessageBox.Show("No data found for the given criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }

         private void GenerateAndDisplayReportr_vat_item_sale()
         {
            try
            {
                // Create a DataTable to hold the data from the database
                DataTable dataTable2 = new DataTable();

                // Create an instance of your database connector
                DbConnector dbConnector = new DbConnector();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

                // Open the database connection
                dbConnector.connection.Open();

                frmR_invoice_wise_sale_rpt invoice_Wise_Sale_Rpt = new frmR_invoice_wise_sale_rpt();

                // Define your SQL query
                string gstrSQL = "{ CALL r_vat_item_sale(?,?) }";

                // Create a command to execute the stored procedure
                using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@F_Date", frmR_invoice_wise_sale_rpt.FromDt_mysql);
                    command.Parameters.AddWithValue("@T_Date", frmR_invoice_wise_sale_rpt.Todt_mysql);                    
                   
                    // Execute the command and read the data into the DataTable
                    using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                    {
                        adapter.Fill(dataTable2);
                    }
                }

                // Close the database connection
                dbConnector.connection.Close();

                // Check if the DataTable contains data
                if (dataTable2.Rows.Count > 0)
                {
                    // Set the report's DataSources property to a new list containing your DataTable
                    //this.invwisesalerpt.LocalReport.DataSources.Clear();
                    invwisesalerpt.LocalReport.DataSources.Clear();
                    this.invwisesalerpt.LocalReport.DataSources.Add(new ReportDataSource("vatitemsaleDataset", dataTable2));

                    // Refresh the report
                    this.invwisesalerpt.RefreshReport();
                }
                //else
                //{
                //    MessageBox.Show("No data found for the given criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         }

         private void GenerateAndDisplayReportr_inv_gst_sale()
         {
            try
            {
                // Create a DataTable to hold the data from the database
                DataTable dataTable3 = new DataTable();

                // Create an instance of your database connector
                DbConnector dbConnector = new DbConnector();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

                // Open the database connection
                dbConnector.connection.Open();

                frmR_invoice_wise_sale_rpt invoice_Wise_Sale_Rpt = new frmR_invoice_wise_sale_rpt();

                // Define your SQL query
                string gstrSQL = "{ CALL r_inv_gst_sale(?,?) }";

                // Create a command to execute the stored procedure
                using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@F_Date", frmR_invoice_wise_sale_rpt.FromDt_mysql);
                    command.Parameters.AddWithValue("@T_Date", frmR_invoice_wise_sale_rpt.Todt_mysql);                    
                   
                    // Execute the command and read the data into the DataTable
                    using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                    {
                        adapter.Fill(dataTable3);
                    }
                }

                // Close the database connection
                dbConnector.connection.Close();

                // Add a new column for serial numbers
                DataColumn serialColumn = new DataColumn("srno", typeof(int));
                dataTable3.Columns.Add(serialColumn);

                // Populate the serial number column
                for (int i = 0; i < dataTable3.Rows.Count; i++)
                {
                    dataTable3.Rows[i]["srno"] = i + 1;
                }

                // Check if the DataTable contains data
                if (dataTable3.Rows.Count > 0)
                {
                    // Set the report's DataSources property to a new list containing your DataTable
                    //this.invwisesalerpt.LocalReport.DataSources.Clear();
                    invwisesalerpt.LocalReport.DataSources.Clear();
                    this.invwisesalerpt.LocalReport.DataSources.Add(new ReportDataSource("rinvgstsaleDataset", dataTable3));

                    // Refresh the report
                    this.invwisesalerpt.RefreshReport();
                }
                //else
                //{
                //    MessageBox.Show("No data found for the given criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void r_Invoicewise_sale_frm_Load(object sender, EventArgs e)
        {
            //GenerateAndDisplayReport();
        }
    }
}
