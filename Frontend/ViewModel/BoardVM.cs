using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Frontend.ViewModel
{
    class BoardVM : Notifiable
    {
        private BackendController backendController;
        private UserModel userModel;
        public KanbanModel kanbanModel { get; private set; }
        private BoardModel selectedBoard;

        public BoardVM(UserModel userModel)
        {
            this.backendController = userModel.backendController;
            this.userModel = userModel;
            kanbanModel = userModel.GetKanban();
        }
        public BoardModel SelectedBoard
        {
            get
            {
                return selectedBoard;
            }
            set
            {
                selectedBoard = value;
                RaisePropertyChanged("SelectedBoard");
                Forward = value != null;
            }

        }
        private BoardModel selectedBoard2;
        public BoardModel SelectedBoard2
        {
            get
            {
                return selectedBoard2;
            }
            set
            {
                selectedBoard2 = value;
                Forward2 = value != null;
            }

        }
        private bool forward = false;
        public bool Forward
        {
            get => forward;
            private set
            {
                forward = value;
                RaisePropertyChanged("Forward");
            }
        }
        private bool forward2 = false;
        public bool Forward2
        {
            get => forward2;
            private set
            {
                forward2 = value;
                RaisePropertyChanged("Forward2");
            }
        }
        internal void AddBoard(AddBoardVM addBoardvm)
        {
            addBoardvm.AddBoardAction = (BoardModel boardModel) =>
            {
                kanbanModel.Boards.Add(boardModel);
            };
        }

        internal void JoinBoard()
        {
            try
            {
                kanbanModel.JoinBoard(SelectedBoard2);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        internal void RemoveBoard()
        {
            try
            {
                if (selectedBoard.Creator.Equals(userModel.Email))
                    kanbanModel.RemoveBoard(SelectedBoard);
                else
                    MessageBox.Show("You can't remove this board");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        internal void Logout()
        {
            backendController.Logout(userModel.Email);
        }
    }
}
