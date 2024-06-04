using static softgen.General;
using System.Data.Odbc;
using System.Data;
using Microsoft.Reporting.WinForms;

namespace softgen
{
    public partial class Sale_Return_Inv_Form : Form
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
        public string inv_no;

        public Sale_Return_Inv_Form()
        {
            InitializeComponent();
            LoadReport();
            this.sale_ret_reportViewer.RefreshReport();
        }
        public string BrandValue { get; set; }
        private void Sale_Return_Inv_Form_Load(object sender, EventArgs e)
        {

        }

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
                frmT_Sale_Return frmT_Sale_Return = new frmT_Sale_Return();

                string srhdr_data = "Select * from t_sr_hdr where sr_no = ?";
                long documentNumber = DocumentManager.DocumentNumber;
                string sr_no = documentNumber.ToString();
                OdbcParameter[] parametershdr = new OdbcParameter[1];
                parametershdr[0] = new OdbcParameter("sr_no", sr_no);
                using (OdbcDataReader reader = dbConnector.ExecuteReader(srhdr_data, parametershdr))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DateTime srDate = Convert.ToDateTime(reader["sr_dt"]);
                            //inv_dt = srDate.ToString("dd/MM/yy");
                            //DateTime invoiceDate = Convert.ToDateTime(reader["invoice_dt"]);
                            //inv_dt = invoiceDate.ToString("dd/MM/yy");
                            //inv_no = reader["invoice_no"].ToString();
                            //DateTime invoicetime = Convert.ToDateTime(reader["bill_time"]);
                            // bill_time = invoicetime.ToString("hh:mm:ss tt");
                            cashier = reader["ent_by"].ToString();
                            //cust_id = reader["cust_id"].ToString();
                            //cust_name = reader["custname"].ToString();
                            //cust_address = reader["custaddress"].ToString();
                            amttopay = reader["net_amt"].ToString();

                        }
                    }
                }

                string amttopaytxt = "(Rupees " + General.ConvertToWords(amttopay) + ")";


                // Set the path of the RDLC report file to the ReportViewer
                this.sale_ret_reportViewer.LocalReport.ReportPath = "Reports/sd_SaleReturn.rdlc";

                // Set report parameters
                ReportParameter[] hdrParameters = new ReportParameter[]
                {
                new ReportParameter("Brand", strBrand),
                new ReportParameter("strCompany", strCompany),
                new ReportParameter("tin", tin),
                new ReportParameter("cst", cst),
                new ReportParameter("lst", lst),
                new ReportParameter("address1", strAddress1),
                new ReportParameter("address2", strAddress2),
                new ReportParameter("address3", strAddress3),
                new ReportParameter("phoneno", phoneno),
                //new ReportParameter("note1", note1),
                //new ReportParameter("note2", note2),
                //new ReportParameter("note3", note3),
                //new ReportParameter("note4", note4),
                //new ReportParameter("branch", branch),
                //new ReportParameter("sr_no", sr_no),
               new ReportParameter("invoice_no", inv_no),
               // new ReportParameter("invoice_dt", inv_dt),
                //new ReportParameter("cust_id", cust_id),
                //new ReportParameter("cust_name", cust_name),
                //new ReportParameter("cust_add", cust_address),

                //new ReportParameter("bill_time", bill_time),
                //new ReportParameter("cashier", cashier),
                new ReportParameter("AmtToPaytxt",amttopaytxt)
                };
                this.sale_ret_reportViewer.LocalReport.SetParameters(hdrParameters);

                GenerateAndDisplayReport();
               

                // Refresh the ReportViewer to display the report
                this.sale_ret_reportViewer.RefreshReport();
            }
            //DbConnector dbConnector1 = new DbConnector();
            dbConnector.OpenConnection();
            if (dbConnector != null)
            {
                string delSQL = "Delete FROM sr";

                using (OdbcCommand delfrmsrtbl = new OdbcCommand(delSQL, dbConnector.connection))
                {
                    delfrmsrtbl.ExecuteNonQuery();
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
            string gstrSQL = "{ CALL sd_SalesReturns(?) }";
            long documentNumber = DocumentManager.DocumentNumber;
            string sr_no = documentNumber.ToString();

            // Create a command to execute the stored procedure
            using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters to the command
                command.Parameters.AddWithValue("@SRNo", sr_no);

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
            this.sale_ret_reportViewer.LocalReport.DataSources.Clear();
            this.sale_ret_reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Sr_Dataset", dataTable));


            // Refresh the report
            this.sale_ret_reportViewer.RefreshReport();
        }








    }
}
