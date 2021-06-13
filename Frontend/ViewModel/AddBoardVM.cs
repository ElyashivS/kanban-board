using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                boardName = value;
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
