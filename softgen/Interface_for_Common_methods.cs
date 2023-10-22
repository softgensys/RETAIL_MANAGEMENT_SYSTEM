using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softgen
{
    public class Interface_for_Common_methods
    {
        public interface ISearchableForm
        {
            void SetSearchVar(bool StartVal);

            bool GetDEStatus();

            void SaveForm();

            void UnsavedData();

            void check_temp_login_sytemname();//to check comp name and login id for showing to particualr login id

        }

    }

   
}
