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
        // internal Action<BoardModel> AddBoardAction;

        public string BoardName
        {
            get => boardName;
            set
            {
                this.boardName = value;
                RaisePropertyChanged("BoardName");
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                this.password = value;
                RaisePropertyChanged("Password");
            }
        }
        private string error;
        public string Error
        {
            get => error;
            set
            {
                this.error = value;
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
                if (!userModel.Password.Equals(Password))
                {
                    Error = "Wrong password";
                    return false;
                }
                else
                {
                    backendController.AddBoard(userModel.Email, BoardName);
                    // this.AddBoardAction.Invoke(backendController.GetBoard(userModel.Email, BoardName));
                    return true;
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }
        }
    }
}
