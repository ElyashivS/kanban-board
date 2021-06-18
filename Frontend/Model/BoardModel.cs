using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBoard = IntroSE.Kanban.Backend.ServiceLayer.Objects.Board;


namespace Frontend.Model
{
    public class BoardModel : NotifiableModelObject
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }
        private string creator;
        public string Creator
        {
            get => creator;
            set
            {
                creator = value;
                RaisePropertyChanged("CreatorEmail");
            }
        }
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
        private string userEmail;
        private SBoard board;

        public string UserEmail
        {
            get => userEmail;
            set
            {
                userEmail = value;
                RaisePropertyChanged("UserEmail");
            }
        }
        // private string UserEmail;
        public BoardModel(BackendController backendController, int id, string boardName, string creator, string userEmail) : base(backendController)
        {
            this.Id = id;
            this.BoardName = boardName;
            this.Creator = creator;
            this.UserEmail = userEmail;
        }

        //public BoardModel(BackendController backendController, SBoard board1, string boardName, BoardModel board) : base(backendController)
        //{
        //    this.Id = board.id;
        //    this.BoardName = board.BoardName;
        //    this.Creator = board.Creator;
        //    this.UserEmail = board.userEmail;
        //}

        public BoardModel(BackendController controller, SBoard board) : base(controller)
        {
            this.board = board;
        }


        //public BoardModel(BackendController backendController, SBoard sBoard)
        //{
        //    Id = sBoard.BoardId;
        //}
    }
}
