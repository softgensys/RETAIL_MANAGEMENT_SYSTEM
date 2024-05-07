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
        //public string amttopay;
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
                            bill_time = invoicetime.ToString("hh:mm:ss tt");
                            cashier = reader["ent_by"].ToString();
                            cust_id = reader["cust_id"].ToString();
                            cust_name = reader["custname"].ToString();
                            cust_address = reader["custaddress"].ToString();
                            amttopay = reader["net_amt_after_disc"].ToString();

                        }
                    }
                }

                string amttopaytxt="(Rupees "+General.ConvertToWords(amttopay)+")";


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
                new ReportParameter("AmtToPaytxt",amttopaytxt)
                };
                this.reportViewer1.LocalReport.SetParameters(hdrParameters);

                GenerateAndDisplayReport();

                GenerateAndDisplayGSTReport();

                GenerateAndDisplayCessReport();

                // Refresh the ReportViewer to display the report
                this.reportViewer1.RefreshReport();
            }
            //DbConnector dbConnector1 = new DbConnector();
            dbConnector.OpenConnection();
            if (dbConnector != null)
            {
                string delSQL = "Delete FROM invoice";

                using (OdbcCommand delfrminvtbl = new OdbcCommand(delSQL, dbConnector.connection))
                {
                    delfrminvtbl.ExecuteNonQuery();
                }
            dbConnector.connection.Close();
            }
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

        private void GenerateAndDisplayGSTReport()
        {
            // Create a DataTable to hold the data from the database
            DataTable dataTable1 = new DataTable();

            // Create an instance of your database connector
            DbConnector dbConnector = new DbConnector();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

            // Open the database connection
            dbConnector.connection.Open();

            // Define your SQL query
            string gstrSQL = "{ CALL sd_inv_VAT(?) }";
            long documentNumber = DocumentManager.DocumentNumber;
            string inv_no = documentNumber.ToString();

            // Create a command to execute the stored procedure
            using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters to the command
                command.Parameters.AddWithValue("@INVOICENo", inv_no);

                // Execute the command and read the data into the DataTable
                using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                {
                    adapter.Fill(dataTable1);
                }
            }

            // Close the database connection
            dbConnector.connection.Close();
          
            // Set the report's DataSources property to a new list containing your DataTable
            //this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet_Tax", dataTable1));


            // Refresh the report
            this.reportViewer1.RefreshReport();
        }

         private void GenerateAndDisplayCessReport()
        {
            // Create a DataTable to hold the data from the database
            DataTable dataTable2 = new DataTable();

            // Create an instance of your database connector
            DbConnector dbConnector = new DbConnector();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

            // Open the database connection
            dbConnector.connection.Open();

            // Define your SQL query
            string gstrSQL = "{ CALL sd_inv_cess(?) }";
            long documentNumber = DocumentManager.DocumentNumber;
            string inv_no = documentNumber.ToString();

            // Create a command to execute the stored procedure
            using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters to the command
                command.Parameters.AddWithValue("@INVOICENo", inv_no);

                // Execute the command and read the data into the DataTable
                using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                {
                    adapter.Fill(dataTable2);
                }
            }

            // Close the database connection
            dbConnector.connection.Close();
          
            // Set the report's DataSources property to a new list containing your DataTable
            //this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet_Cess", dataTable2));


            // Refresh the report
            this.reportViewer1.RefreshReport();
        }


      
        //----------OLD CODE--------------------------//

        
        private void Form2_Load(object sender, EventArgs e)
        { // Call ItemListToVar to load the report with item data
           // ItemListToVar();
        }

    }


}
