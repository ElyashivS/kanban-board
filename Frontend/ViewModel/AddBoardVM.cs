using Frontend.Model;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBoard = IntroSE.Kanban.Backend.ServiceLayer.Objects.Board;

namespace Frontend.ViewModel
{
    public class AddBoardVM : Notifiable
    {
        private BackendController backendController;
        private UserModel userModel;
        private string boardName;

        public string BoardName
        {
            get => boardName;
            set
            {
                boardName = value != null ? value : boardName;
                RaisePropertyChanged("BoardName");
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
        public AddBoardVM(UserModel userModel)
        {
            backendController = userModel.backendController;
            this.userModel = userModel;
        }
        public bool AddBoard()
        {
            Error = "";
            try
            {
                backendController.AddBoard(userModel.Email, BoardName);
                this.AddBoardAction.Invoke(backendController.GetBoard(userModel.Email, BoardName));
                return true;
            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }
        }
        public Action<BoardModel> AddBoardAction;
    }
}
