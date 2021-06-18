using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    class MoveColumnVM : Notifiable
    {
        private BackendController backendController;
        private UserModel userModel;
        private BoardModel boardModel;
        private ColumnModel columnModel;
        private string columnMove;
        public string ColumnMove
        {
            get => columnMove;
            set
            {
                this.columnMove = value;
                RaisePropertyChanged("ColumnMove");
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
        public MoveColumnVM(UserModel userModel, BoardModel boardModel, ColumnModel columnModel)
        {
            this.backendController = userModel.backendController;
            this.userModel = userModel;
            this.boardModel = boardModel;
            this.columnModel = columnModel;
        }
        public bool MoveColumn()
        {
            //TODO
            return false;
        }
    }
}
