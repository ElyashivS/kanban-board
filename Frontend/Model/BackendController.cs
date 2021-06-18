using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.ServiceLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class BackendController
    {
        private Service service;
        public BackendController(Service service)
        {
            this.service = service;
        }
        public BackendController()
        {
            this.service = new Service();
            service.LoadData();
        }
        public UserModel Login(string email, string password)
        {
            Response<User> userResponse = service.Login(email, password);
            if (userResponse.ErrorOccured)
            {
                throw new Exception(userResponse.ErrorMessage);
            }
            return new UserModel(this, email, password);
        }

        internal void Logout(string email)
        {
            Response logoutResponse = service.Logout(email);
            if (logoutResponse.ErrorOccured)
            {
                throw new Exception(logoutResponse.ErrorMessage);
            }
        }

        internal void SetColumnName(string email, string creator, string boardName, int id, string columnName)
        {
            Response a = service.RenameColumn(email, creator, boardName, id, columnName);
            if (a.ErrorOccured)
            {
                throw new Exception(a.ErrorMessage);
            }
        }

        internal void setLimitToColumn(string email, string creator, string boardName, int id, int v)
        {
            Response a = service.LimitColumn(email, creator, boardName, id, v);
            if (a.ErrorOccured)
                throw new Exception(a.ErrorMessage);
            throw new Exception(a.ErrorMessage);
        }

        internal void Register(string email, string password)
        {
            Response registerResponse = service.Register(email, password);
            if (registerResponse.ErrorOccured)
            {
                throw new Exception(registerResponse.ErrorMessage);
            }
        }

        internal void AddAssign(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            Response response = service.AssignTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId, emailAssignee);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }

        internal void AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            Response response = service.AddTask(userEmail, creatorEmail, boardName, title, description, dueDate);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }

        //public Response<Board> AddBoard(string email, string boardName)
        //{
        //    Response<Board> response = (Response<Board>)service.AddBoard(email, boardName);
        //    if (response.ErrorOccured)
        //        throw new Exception(response.ErrorMessage);
        //    return response;
        //}
        public void AddBoard(string email, string boardName)
        {
            Response response = service.AddBoard(email, boardName);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        public BoardModel GetBoard(string email, string boardName)
        {
            Response<Board> response = service.GetBoard(email, boardName);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
            else
            {
                Board board = response.Value;
                BoardModel boardModel = new BoardModel(this, board.BoardId, board.BoardName, board.Creator, email);
                return boardModel;
            }
        }
        public void AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {
            Response response = service.AddColumn(userEmail, creatorEmail, boardName, columnOrdinal, columnName);
            if (response.ErrorOccured)
                throw new Exception(response.ErrorMessage);
        }
        internal List<BoardModel> GetAllBoards(UserModel user)
        {
            List<Board> sBoards = (List<Board>)service.GetBoardNames(user.Email).Value;
            if (sBoards == null)
                return new List<BoardModel>();
            List<BoardModel> boards = new List<BoardModel>();
            foreach (Board board in sBoards)
            {
                boards.Add(new BoardModel(this, board.BoardId, board.BoardName, board.Creator, user.Email));
            }
            return boards;
        }
        internal void RemoveBoard(string email, string creatorEmail, string boardName)
        {
            Response response = service.RemoveBoard(email, creatorEmail, boardName);
            if (response.ErrorOccured)
            {
                throw new Exception(response.ErrorMessage);
            }
        }
        internal List<int> GetAllBoardsIds(string email)
        {
            Response<IList<Board>> a = service.GetAllBoards(email);
            List<int> c = new List<int>();
            foreach (Board b in a.Value)
            {
                c.Add(b.BoardId);
            }
            return c;
        }
        internal Board getBoard(int i)
        {
            Response<Board> a = service.getBoard(i); // Create function that get ID and return object of Board
            if (a.ErrorOccured)
                throw new Exception(a.ErrorMessage);
            return a.Value;
        }
    }
}
