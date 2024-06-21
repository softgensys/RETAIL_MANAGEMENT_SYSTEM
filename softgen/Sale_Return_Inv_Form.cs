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

            string amttopay = string.Empty;
            string cashier = string.Empty;

            long documentNumber = DocumentManager.DocumentNumber;
            string sr_no = documentNumber.ToString();
            DbConnector dbConnector = new DbConnector();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

            try
            {
                dbConnector.OpenConnection();

                if (dbConnector.connection.State == ConnectionState.Open)
                {
                    string srhdr_data = "SELECT * FROM t_sr_hdr WHERE sr_no = ?";
                    OdbcParameter[] parametershdr = { new OdbcParameter("sr_no", sr_no) };

                    using (OdbcCommand command = new OdbcCommand(srhdr_data, dbConnector.connection))
                    {
                        command.Parameters.AddRange(parametershdr);

                        using (OdbcDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DateTime srDate = Convert.ToDateTime(reader["sr_dt"]);
                                    cashier = reader["ent_by"].ToString();
                                    amttopay = reader["net_amt"].ToString();
                                }
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
                new ReportParameter("AmtToPaytxt", amttopaytxt)
                    };

                    this.sale_ret_reportViewer.LocalReport.SetParameters(hdrParameters);

                    GenerateAndDisplayReport(sr_no, dbConnector);

                    // Refresh the ReportViewer to display the report
                    this.sale_ret_reportViewer.RefreshReport();

                    // Deleting data from sr table
                    string delSQL = "DELETE FROM sr";
                    using (OdbcCommand delfrmsrtbl = new OdbcCommand(delSQL, dbConnector.connection))
                    {
                        delfrmsrtbl.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (dbConnector.connection.State == ConnectionState.Open)
                {
                    dbConnector.connection.Close();
                }
            }
        }

        private void GenerateAndDisplayReport(string sr_no, DbConnector dbConnector)
        {
            try
            {
                DataTable dataTable = new DataTable();

                if (dbConnector.connection.State == ConnectionState.Closed)
                {
                    dbConnector.OpenConnection();
                }

                string gstrSQL = "{ CALL sd_SalesReturns(?) }";

                using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SRNo", sr_no);

                    using (OdbcDataAdapter adapter = new OdbcDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                DataColumn serialColumn = new DataColumn("srno", typeof(int));
                dataTable.Columns.Add(serialColumn);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    dataTable.Rows[i]["srno"] = i + 1;
                }

                this.sale_ret_reportViewer.LocalReport.DataSources.Clear();
                this.sale_ret_reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Sr_Dataset", dataTable));

                this.sale_ret_reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
