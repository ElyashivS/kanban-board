using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    class AddTaskVM : Notifiable
    {
        private BackendController backendController;
        private UserModel userModel;
        private BoardModel boardModel;
        public AddTaskVM(UserModel userModel, BoardModel boardModel)
        {
            backendController = userModel.backendController;
            this.userModel = userModel;
            this.boardModel = boardModel;
        }
        private string taskName;
        public string TaskName
        {
            get => taskName;
            set
            {
                taskName = value;
                RaisePropertyChanged("TaskName");
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                RaisePropertyChanged("Description");
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
        private DateTime dueDate;
        public DateTime DueDate
        {
            get => dueDate;
            set
            {
                dueDate = value;
                RaisePropertyChanged("DueDate");
            }
        }
        public bool AddTask()
        {
            Error = "";
            try
            {
                backendController.AddTask(userModel.Email, boardModel.Creator, boardModel.BoardName, TaskName, Description, DueDate);
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
