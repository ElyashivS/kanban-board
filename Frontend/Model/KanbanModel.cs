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
        public UserModel userModel;

        public void RemoveBoard(BoardModel boardModel)
        {
            Boards.Remove(boardModel);
        }
        public KanbanModel(BackendController backendController, ObservableCollection<BoardModel> Boards) : base(backendController)
        {

        }
    }
}
