using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    class AssignTaskVM : Notifiable
    {
        private BackendController backendController;
        private UserModel userModel;
        private BoardModel boardModel;
        private ColumnModel columnModel;
        private TaskModel taskModel;
        private int columnID;
        public AssignTaskVM(UserModel userModel, BoardModel boardModel, ColumnModel columnModel, TaskModel taskModel)
        {
            backendController = userModel.backendController;
            this.userModel = userModel;
            this.boardModel = boardModel;
        }
        private string assignName;
        public string AssignName
        {
            get => assignName;
            set
            {
                assignName = value;
                RaisePropertyChanged("AssignName");
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

        internal bool AddAssign()
        {
            Error = "";
            try
            {
                backendController.AddAssign(userModel.Email, boardModel.Creator, boardModel.BoardName, columnID, taskModel.Id, AssignName);
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
