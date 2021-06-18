using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Frontend.ViewModel
{
    class KanbanMenuVM : Notifiable
    {
        private UserModel userModel;
        private BackendController backendController;
        private KanbanModel kanbanModel;
        private BoardModel _selectedBoard;
        public KanbanMenuVM(UserModel userModel)
        {
            backendController = userModel.backendController;
            this.userModel = userModel;
            SelectedBoard = new BoardModel(backendController, -1, "", "", "");
            this.kanban = new KanbanModel(backendController, userModel, SelectedBoard);
        }
        public KanbanModel kanban
        {
            get => kanbanModel;
            set
            {
                kanbanModel = value;
                RaisePropertyChanged("kanban");
            }
        }
        public BoardModel SelectedBoard
        {
            get => _selectedBoard;
            set
            {
                _selectedBoard = value != null ? value : _selectedBoard;
                RaisePropertyChanged("SelectedBoard");
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
    }
}
