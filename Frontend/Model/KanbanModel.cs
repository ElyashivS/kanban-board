using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{

    public class KanbanModel : NotifiableModelObject
    {
        public ObservableCollection<BoardModel> Boards { get; set; }
        public ObservableCollection<BoardModel> Boards2 { get; set; }

        public UserModel userModel;
        public BoardModel boardModel;

        public KanbanModel(BackendController backendController, UserModel userModel) : base(backendController)
        {
            this.userModel = userModel;
            Boards = new ObservableCollection<BoardModel>(backendController.GetAllBoardsIds(userModel.Email).
                Select((c, i) => new BoardModel(backendController, boardModel.Id, boardModel.BoardName, boardModel.Creator, userModel.Email)));
            Boards.CollectionChanged += HandleChange;
        }
        public KanbanModel(BackendController backendController, UserModel userModel, BoardModel boardModel) : base(backendController)
        {
            this.userModel = userModel;
            this.boardModel = boardModel;
            Boards = new ObservableCollection<BoardModel>(backendController.GetAllBoardsIds(userModel.Email).
                Select(i => new BoardModel(backendController, backendController.getBoard(i))));
            Boards.CollectionChanged += HandleChange;
        }
        public void RemoveBoard(BoardModel boardModel)
        {
            Boards.Remove(boardModel);
        }

        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            //read more here: https://stackoverflow.com/questions/4279185/what-is-the-use-of-observablecollection-in-net/4279274#4279274
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (BoardModel y in e.OldItems)
                {
                    backendController.RemoveBoard(userModel.Email, y.Creator, y.BoardName);
                }
            }
        }

        internal void JoinBoard(BoardModel selectedBoard2)
        {
            Boards2.Remove(selectedBoard2);
            Boards.Add(selectedBoard2);
        }
    }
}
