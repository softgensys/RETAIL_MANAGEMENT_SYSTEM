using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softgen.Controller
{
    public static class FormUtility
    {
        public static void SetFormMode(Form form, string mode, Action<Form> modeAction)
        {
            if (form != null)
            {
                switch (mode)
                {
                    case "Add":
                        form.Text = $"{form.Name} < Add>";
                        // Perform actions specific to Add mode
                        modeAction?.Invoke(form); // Invoke the provided action for the specified mode
                        break;
                    case "Modify":
                        form.Text = $"{form.Name} < Modify>";
                        // Perform actions specific to Modify mode
                        modeAction?.Invoke(form); // Invoke the provided action for the specified mode
                        break;
                    case "Delete":
                        form.Text = $"{form.Name} < Delete>";
                        // Perform actions specific to Modify mode
                        modeAction?.Invoke(form); // Invoke the provided action for the specified mode
                        break;
                    default:
                        break;
                }
            }
        }
    }

}
