﻿namespace softgen
{
    public class Interface_for_Common_methods
    {
        public interface ISearchableForm
        {
            void SetSearchVar(bool StartVal);

            bool GetDEStatus();

            void SaveForm();

            void SearchForm();

            void UnsavedData();

            void check_temp_login_sytemname();//to check comp name and login id for showing to particualr login id

            void ClearForm();

            void PrintForm();

            void ResetControls(Control.ControlCollection controls);

        }

    }


}
