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

        internal void Register(string email, string password)
        {
            Response registerResponse = service.Register(email, password);
            if (registerResponse.ErrorOccured)
            {
                throw new Exception(registerResponse.ErrorMessage);
            }
        }
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
                BoardModel boardModel = new BoardModel();
                return boardModel;
            }
        }
    }
}
