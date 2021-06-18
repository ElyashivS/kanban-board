using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Frontend.ViewModel
{
    class ColumnMenuVM : Notifiable
    {
        private BackendController backendController;
        private UserModel userModel;
        public ColumnMenuModel Columns { get; private set; }
        public BoardModel boardModel;
        private ColumnModel selectedColumn;
        public ColumnModel SelectedColumn
        {
            get
            {
                return selectedColumn;
            }
            set
            {
                selectedColumn = value;
                Forward = value != null;
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

        internal void AddColumn(AddColumnVM addColumnvm)
        {
            //TODO
        }

        private string error;

        internal void MoveColumn(MoveColumnVM moveColumnvm)
        {
            //TODO
        }

        public string Error
        {
            get => error;
            set
            {
                error = value;
                RaisePropertyChanged("Error");
            }
        }
        internal void AddColumn()
        {
            //TODO
        }
        public void RemoveColumn()
        {
            try
            {
                Columns.RemoveColumn(SelectedColumn);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public ColumnMenuVM (UserModel userModel, BoardModel boardModel)
        {
            this.backendController = userModel.backendController;
            this.userModel = userModel;
            this.boardModel = boardModel;
            Columns = new ColumnMenuModel(backendController, userModel, boardModel);
        }
    }
}
