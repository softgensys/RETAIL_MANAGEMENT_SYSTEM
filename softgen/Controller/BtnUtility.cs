using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace softgen.Utility
{
        public static class FormUtility
        {
            public static void ShowAddFormPanel(
                Panel parentPanel, Panel formPanel, Panel mainPanel, Panel dashPanel,
                Button saveButton, Button deleteButton, Button button10)
            {
                // Your code to show the form panel and configure controls
                FlagManager.Flag = "Add";
                dashPanel.Visible = false;
                formPanel.Visible = true;

                // Add the FormPanel to the parentPanel's Controls collection
                parentPanel.Controls.Add(formPanel);

                // Position the FormPanel within the parentPanel
                formPanel.Location = new Point(0, 1); // Adjust as needed

                // Bring the FormPanel to the front
                formPanel.BringToFront();

                mainPanel.Visible = false;
                button10.Visible = false;
                deleteButton.Visible = false;
                saveButton.Visible = true;
            }
        }
}
