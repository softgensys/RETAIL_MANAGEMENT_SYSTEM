using Microsoft.Reporting.WinForms;
using System.Data;
using System.Data.Odbc;
using static softgen.General;

namespace softgen
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            GenerateAndDisplayReport();

            // Set the path of the RDLC report file to the ReportViewer
            this.reportViewer1.LocalReport.ReportPath = "Report2.rdlc";
            // Refresh the report to ensure it displays the latest changes
            this.reportViewer1.RefreshReport();

        }

        private void Form3_Load(object sender, EventArgs e)
        {

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

        private void reportViewer2_Load(object sender, EventArgs e)
        {

        }
    }
}
