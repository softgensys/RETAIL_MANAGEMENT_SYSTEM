using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace softgen
{
    public partial class frmM_Group : Form
    {
        private DbConnector dbConnector;
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn;
        public bool mblnSearch, mblnDataEntered;

        public frmM_Group()
        {
            InitializeComponent();
            dbConnector = new DbConnector();

            this.Activated += MyForm_Activated;
        }


        private void MyForm_Activated(object sender, EventArgs e)
        {
            DeTools.ClearStatusBarHelp();
            DeTools.ActiveFileMenu(this);
            DeTools.CreatedBy(mstrEntBy, mstrEntOn);
            DeTools.PostedBy(mstrAuthBy, mstrAuthOn);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string query = QueryManager.getgrp_data;
                DataTable result = dbConnector.ExecuteQuery(query);
                DataRow row = result.Rows[0];
                string group_id = row["group_id"].ToString(); // Replace Column1 with your actual column name
                string group_desc = row["group_desc"].ToString(); // Replace Column2 with your actual column name
                string active_yn = row["active_yn"].ToString(); // Replace Column2 with your actual column name

                // Assign the values to the desired fields
                txtGrpId.Text = group_id;
                txtGrpDesc.Text = group_desc;

                if (active_yn == "Y")
                {
                    chkStatus.Checked = true;

                }
                else
                {
                    chkStatus.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("In getbtn", ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtGrpId.Text = String.Empty;
            txtGrpDesc.Text = String.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {

                String grpid = txtGrpId.Text ?? string.Empty; ;
                String grpdesc = txtGrpDesc.Text ?? string.Empty; ;
                bool active = chkStatus.Checked;

                DateTime enteredOn = DateTime.Now;
                String ent_by = "TEST";
                String status = "V";
                String trans_status = "N";

                if (string.IsNullOrWhiteSpace(grpid) || string.IsNullOrWhiteSpace(grpdesc))
                {
                    MessageBox.Show("Please Enter Group Id or Group Desc");
                    return;
                }

                if (!active)
                {
                    MessageBox.Show("Please Check Active");
                    return;
                }
                String isactive = active ? "Y" : "N";



                //       string query = $"INSERT INTO m_group (group_id, group_desc, active_yn,status,ent_on,ent_by,trans_status)" +
                //         $" VALUES ('{grpid}', '{grpdesc}', '{isactive}', '{status}','{enteredOn.ToString("yyyy-MM-dd HH:mm:ss")}','{ent_by}','{trans_status}');";

                string query = QueryManager.Addgrp_data;
                query += $" VALUES ('{grpid}', '{grpdesc}', '{isactive}', '{status}', '{enteredOn.ToString("yyyy-MM-dd HH:mm:ss")}', '{ent_by}', '{trans_status}')";

                int rowsAffected = dbConnector.ExecuteNonQuery(query);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data Inserted Successfully!");
                    txtGrpId.Text = string.Empty;
                    txtGrpDesc.Text = string.Empty;

                }
                else
                {
                    MessageBox.Show("Failed to Add Data.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("In Grp Add", ex.Message);
            }
        }

        private void Group_Load(object sender, EventArgs e)
        {
            DeTools.DisplayForm(this, 400, 220);
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(txtGrpId, "Enter Group Id.");
            toolTip.SetToolTip(txtGrpDesc, "Enter Group Description.");

        }

        private void Group_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm parentform = (MainForm)this.MdiParent;

            parentform.dashpanel.Visible = true;
            parentform.mainpanel.Visible = false;
            parentform.formpanel.Visible = false;
        }

        private void frmM_Group_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (DeTools.GetMode(this))
            {
                case DeTools.ADDMODE:
                case DeTools.MODIFYMODE:
                    mblnDataEntered = true;
                    break;
            }
        }
    }
}
