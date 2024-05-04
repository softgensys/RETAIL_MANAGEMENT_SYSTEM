//using Microsoft.Reporting.NETCore;
using Microsoft.Reporting.WinForms;
using System.Data;
using System.Data.Odbc;
using static softgen.General;

namespace softgen
{
    public partial class Form2 : Form
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
        public string hsn;
        //frmT_Invoice inv = new frmT_Invoice();


        public Form2()
        {
            InitializeComponent();
            LoadReport();
            this.reportViewer1.RefreshReport();
        }
        public string BrandValue { get; set; }

        // Method to set the path of the RDLC report file
        //private void LoadReport(string srno, string desc, string hsn, string disc, decimal qty, decimal mrp, decimal sp, decimal netamount)
        private void LoadReport()
        {
            string strBrand = DeTools.strBrand;
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

            DbConnector dbConnector = new DbConnector();
            dbConnector.OpenConnection();


            if (dbConnector.connection != null)
            {
                frmT_Invoice frmT_Invoice = new frmT_Invoice();

                string invhdr_data = "Select * from t_invoice_hdr where invoice_no = ?";
                long documentNumber = DocumentManager.DocumentNumber;
                string inv_no = documentNumber.ToString();
                OdbcParameter[] parametershdr = new OdbcParameter[1];
                parametershdr[0] = new OdbcParameter("invoice_no", inv_no);
                using (OdbcDataReader reader = dbConnector.ExecuteReader(invhdr_data, parametershdr))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DateTime invoiceDate = Convert.ToDateTime(reader["invoice_dt"]);
                            inv_dt = invoiceDate.ToString("dd/MM/yy");
                            DateTime invoicetime = Convert.ToDateTime(reader["bill_time"]);
                            bill_time = invoicetime.ToString("hh:mm tt");
                            cashier = reader["ent_by"].ToString();
                            cust_id = reader["cust_id"].ToString();
                            cust_name = reader["custname"].ToString();
                            cust_address = reader["custaddress"].ToString();

                        }
                    }
                }


                // Set the path of the RDLC report file to the ReportViewer
                this.reportViewer1.LocalReport.ReportPath = "InvReport.rdlc";

                // Set report parameters
                ReportParameter[] hdrParameters = new ReportParameter[]
                {
                new ReportParameter("Brand", strBrand),
                new ReportParameter("strCompany", strCompany),
                new ReportParameter("address1", strAddress1),
                new ReportParameter("address2", strAddress2),
                new ReportParameter("address3", strAddress3),
                new ReportParameter("phoneno", phoneno),
                new ReportParameter("note1", note1),
                new ReportParameter("note2", note2),
                new ReportParameter("note3", note3),
                new ReportParameter("note4", note4),
                new ReportParameter("lst", lst),
                new ReportParameter("cst", cst),
                new ReportParameter("tin", tin),
                new ReportParameter("branch", branch),
                new ReportParameter("invoice_no", inv_no),
                new ReportParameter("invoice_dt", inv_dt),
                new ReportParameter("cust_id", cust_id),
                new ReportParameter("cust_name", cust_name),
                new ReportParameter("cust_add", cust_address),

                new ReportParameter("bill_time", bill_time),
                new ReportParameter("cashier", cashier),
                };
                this.reportViewer1.LocalReport.SetParameters(hdrParameters);

                GenerateAndDisplayReport();


                // Refresh the ReportViewer to display the report
                this.reportViewer1.RefreshReport();
            }
            dbConnector.connection.Close();
        }

        private void GenerateAndDisplayReport()
        {
            // Create a DataTable to hold the data from the database
            DataTable dataTable = new DataTable();

            // Create an instance of your database connector
            DbConnector dbConnector = new DbConnector();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

            // Open the database connection
            dbConnector.connection.Open();

            // Define your SQL query
            string gstrSQL = "{ CALL sd_invoice(?) }";
            long documentNumber = DocumentManager.DocumentNumber;
            string inv_no = documentNumber.ToString();

            // Create a command to execute the stored procedure
            using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters to the command
                command.Parameters.AddWithValue("@InvoiceNo", inv_no);

                // Execute the command and read the data into the DataTable
                using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            // Close the database connection
            dbConnector.connection.Close();

            // Add a new column for serial numbers
            DataColumn serialColumn = new DataColumn("srno", typeof(int));
            dataTable.Columns.Add(serialColumn);

            // Populate the serial number column
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataTable.Rows[i]["srno"] = i + 1;
            }

            // Set the report's DataSources property to a new list containing your DataTable
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet_invoice", dataTable));


            // Refresh the report
            this.reportViewer1.RefreshReport();
        }


      
        //----------OLD CODE--------------------------//

        //private void LoadReport(List<string[]> itemData)
        //{
        //    string strBrand = DeTools.strBrand;
        //    string strCompany = DeTools.strCompany;
        //    string strAddress1 = DeTools.strAddress1;
        //    string strAddress2 = DeTools.strAddress2;
        //    string strAddress3 = DeTools.strCompany; // This looks like a mistake, should it be DeTools.strAddress3?
        //    string phoneno = DeTools.strPhone;
        //    string note1 = DeTools.strNote1;
        //    string note2 = DeTools.strNote2;
        //    string note3 = DeTools.strNote3;
        //    string note4 = DeTools.strNote4;
        //    string lst = DeTools.strLst;
        //    string cst = DeTools.strCst;
        //    string tin = DeTools.strTin;
        //    string branch = DeTools.strBranch;

        //    DbConnector dbConnector = new DbConnector();
        //    dbConnector.OpenConnection();

        //    if (dbConnector.connection != null)
        //    {
        //        string invhdr_data = "Select * from t_invoice_hdr where invoice_no = ?";
        //        long documentNumber = DocumentManager.DocumentNumber;
        //        string inv_no = documentNumber.ToString();
        //        OdbcParameter[] parametershdr = new OdbcParameter[1];
        //        parametershdr[0] = new OdbcParameter("invoice_no", inv_no);
        //        using (OdbcDataReader reader = dbConnector.ExecuteReader(invhdr_data, parametershdr))
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    DateTime invoiceDate = Convert.ToDateTime(reader["invoice_dt"]);
        //                    inv_dt = invoiceDate.ToString("dd/MM/yy");
        //                    DateTime invoicetime = Convert.ToDateTime(reader["bill_time"]);
        //                    bill_time = invoicetime.ToString("hh:mm tt");
        //                    cashier = reader["ent_by"].ToString();
        //                    cust_id = reader["cust_id"].ToString();
        //                    cust_name = reader["custname"].ToString();
        //                    cust_address = reader["custaddress"].ToString();
        //                }
        //            }
        //        }

        //        // Set the path of the RDLC report file to the ReportViewer
        //        this.reportViewer1.LocalReport.ReportPath = "InvReport.rdlc";

        //        // Set report parameters
        //        ReportParameter[] hdrParameters = new ReportParameter[]
        //        {
        //    new ReportParameter("Brand", strBrand),
        //    new ReportParameter("strCompany", strCompany),
        //    new ReportParameter("address1", strAddress1),
        //    new ReportParameter("address2", strAddress2),
        //    new ReportParameter("address3", strAddress3),
        //    new ReportParameter("phoneno", phoneno),
        //    new ReportParameter("note1", note1),
        //    new ReportParameter("note2", note2),
        //    new ReportParameter("note3", note3),
        //    new ReportParameter("note4", note4),
        //    new ReportParameter("lst", lst),
        //    new ReportParameter("cst", cst),
        //    new ReportParameter("tin", tin),
        //    new ReportParameter("branch", branch),
        //    new ReportParameter("invoice_no", inv_no),
        //    new ReportParameter("invoice_dt", inv_dt),
        //    new ReportParameter("cust_id", cust_id),
        //    new ReportParameter("cust_name", cust_name),
        //    new ReportParameter("cust_add", cust_address),
        //    new ReportParameter("bill_time", bill_time),
        //    new ReportParameter("cashier", cashier),
        //        };
        //        this.reportViewer1.LocalReport.SetParameters(hdrParameters);

        //        // Create a dataset for the current item and add it as a report data source
        //        DataSet1 dataSet = new DataSet1();
        //        foreach (string[] item in itemData)
        //        {
        //            srno = item[0];
        //            desc = item[1];
        //            hsn = item[2];
        //            disc = item[3];
        //            qty = Convert.ToDecimal(item[4]);
        //            mrp = Convert.ToDecimal(item[5]);
        //            sp = Convert.ToDecimal(item[6]);
        //            netamount = Convert.ToDecimal(item[7]);

        //            dataSet.ItemDataTable.Rows.Add(srno, desc, hsn, disc, qty, mrp, sp, netamount);

        //        }

        //        // Set report data source using the created DataSet1
        //        ReportDataSource reportDataSource = new ReportDataSource("DataSet1", (DataTable)dataSet.ItemDataTable);
        //        this.reportViewer1.RefreshReport();
        //        this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
        //        this.reportViewer1.RefreshReport();

        //        // Refresh the ReportViewer to display the report
        //    }
        //    //dbConnector.connection.Close();
        //}

        //public List<string[]> GetItemDataForPrint()
        //{
        //    List<string[]> itemList = new List<string[]>();

        //    try
        //    {
        //        DbConnector dbConnector = new DbConnector();
        //        dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
        //        dbConnector.connection.Open();

        //        string gstrSQL = "{ CALL sd_invoice(?) }";
        //        long documentNumber = DocumentManager.DocumentNumber;
        //        string inv_no = documentNumber.ToString();

        //        using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.AddWithValue("@InvoiceNo", inv_no);
        //            int rowno = 0;
        //            using (OdbcDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        string[] item = new string[8]; // Assuming there are 8 columns in your result set
        //                        item[0] = reader["pay_mode_desc"].ToString(); // Assuming the first column is Sr. No.
        //                        item[1] = reader["item_desc"].ToString(); // Assuming the second column is Item Name
        //                        item[2] = reader["hsn_code"].ToString();
        //                        item[3] = reader["disc_per"].ToString();
        //                        item[4] = reader["qty"].ToString(); // Assuming the third column is Qty
        //                        item[5] = reader["mrp"].ToString(); // Assuming the fourth column is Mrp
        //                        item[6] = reader["sale_price"].ToString(); // Assuming the fifth column is Sale Price
        //                        item[7] = reader["net_amt"].ToString(); // Assuming the sixth column is Net Amount

        //                        // Add the quantity to the totalQty variable
        //                        totqty += Convert.ToDecimal(reader["qty"]);

        //                        itemList.Add(item);

        //                        rowno++;
        //                    }

        //                }
        //                string formattedTotalQty = totqty.ToString("0.###");
        //                totqty = Convert.ToDecimal(formattedTotalQty);
        //            }
        //        }
        //        dbConnector.connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception, log it, or throw it as needed.
        //        Console.WriteLine("Error: " + ex.Message);
        //    }

        //    return itemList;
        //}

        //private void ItemListToVar()
        //{
        //    // Call GetItemDataForPrint to retrieve item data
        //    List<string[]> itemList = GetItemDataForPrint();

        //    // Call LoadReport to load the report with item data
        //    LoadReport(itemList);
        //}

        private void Form2_Load(object sender, EventArgs e)
        { // Call ItemListToVar to load the report with item data
           // ItemListToVar();
        }

    }


}
