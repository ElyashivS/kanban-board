using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    public class AddColumnVM : Notifiable
    {
        private BackendController backendController;
        private UserModel userModel;
        private BoardModel boardModel;
        public AddColumnVM(UserModel userModel, BoardModel boardModel)
        {
            this.backendController = userModel.backendController;
            this.userModel = userModel;
            this.boardModel = boardModel;
        }
        private string columnName;

        public string ColumnName
        {
            get => columnName;
            set
            {
                columnName = value;
                RaisePropertyChanged("ColumnName");
            }
        }
        private string columnID;

        public string ColumnID
        {
            get => columnID;
            set
            {
                columnID = value;
                RaisePropertyChanged("ColumnID");
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
        public bool AddColumn()
        {
            Error = "";
            try
            {
                backendController.AddColumn(userModel.Email, boardModel.Creator, boardModel.BoardName, Int32.Parse(ColumnID), ColumnName);
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
