using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    public class SetLimitToColumnVM : Notifiable
    {
        private BackendController backendController;
        private UserModel userModel;
        private BoardModel boardModel;
        private ColumnModel columnModel;
        private string setLimit;
        public SetLimitToColumnVM(UserModel userModel, BoardModel boardModel, ColumnModel columnModel)
        {
            this.backendController = userModel.backendController;
            this.userModel = userModel;
            this.boardModel = boardModel;
            this.columnModel = columnModel;
        }
        public string SetLimit
        {

            get => setLimit;
            set
            {
                setLimit = value;
                RaisePropertyChanged("SetLimit");
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
        internal bool SetLimitColumn()
        {
            Error = "";
            try
            {
                backendController.setLimitToColumn(userModel.Email, boardModel.Creator, boardModel.BoardName, columnModel.Id, Int32.Parse(SetLimit));
                columnModel.Limit = Int32.Parse(setLimit);
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
