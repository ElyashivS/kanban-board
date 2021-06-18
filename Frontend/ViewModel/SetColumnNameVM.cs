using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    class SetColumnNameVM : Notifiable
    {
        private ColumnModel columnModel;
        private BoardModel boardModel;
        private BackendController backendController;
        private UserModel userModel;
        private string setColumnName2;

        public string SetColumnName2
        {
            get => setColumnName2;
            set
            {
                setColumnName2 = value;
                RaisePropertyChanged("SetColumnName2");
            }
        }

        private string error;
        public string Error
        {
            get => error;
            set
            {
                error = value;
                RaisePropertyChanged("Error");
            }
        }
        internal bool SetColumnName()
        {
            Error = "";
            try
            {
                backendController.SetColumnName(userModel.Email, boardModel.Creator, boardModel.BoardName, columnModel.Id, SetColumnName2);
                columnModel.ColumnName = setColumnName2;
                return true;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }
        }
    }
}
