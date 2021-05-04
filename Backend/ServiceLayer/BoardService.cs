using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BuisnessLayer;
using log4net;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class BoardService
    {
        BoardController boardController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public BoardService()
        {
            boardController = new BoardController();
        }


        public void Register(string email)
        {
            boardController.Register(email);
        }
        public Response LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                boardController.LimitColumn(userEmail, creatorEmail, boardName, columnOrdinal, limit); ;
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response<int> GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {

            try
            {
                int c = boardController.GetcolumnLimit(userEmail, creatorEmail, boardName, columnOrdinal);
                return Response<int>.FromValue(c);
            }
            catch (Exception e)
            {
                return Response<int>.FromError(e.Message);
            }
        }


        public Response<string> GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                string c = boardController.GetColumnName(userEmail, creatorEmail, boardName, columnOrdinal);
                return Response<string>.FromValue(c);
            }
            catch (Exception e)
            {
                return Response<string>.FromError(e.Message);
            }
        }


        public Response<Task> AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            try
            {
                BuisnessLayer.Task c = boardController.AddTask(userEmail, creatorEmail, boardName, title, description, dueDate);
                log.Info("Task has been added");
                return Response<Task>.FromValue(new Task(c.GetId(), c.GetCreationTime(), c.GetTitle(), c.GetDescription(), c.GetDueDate(), c.GetAssignee()));
            }
            catch (Exception e)
            {
                log.Warn("Failed to add task");
                return Response<Task>.FromError(e.Message);
            }
        }

        public Response UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                boardController.ChangeDueDate(userEmail, creatorEmail, boardName, columnOrdinal, taskId, dueDate);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            try
            {
                boardController.ChangeTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, title);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            try
            {
                boardController.ChangeDescription(userEmail, creatorEmail, boardName, columnOrdinal, taskId, description);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                boardController.MoveTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response<IList<Task>> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                List<BuisnessLayer.Task> c = boardController.GetColunm(userEmail, creatorEmail, boardName, columnOrdinal);
                List<Task> d = new List<Task>();
                foreach (BuisnessLayer.Task a in c)
                {
                    d.Add(new Task(a.id, DateTime.Now, a.GetTitle(), a.GetDescription(), a.GetDueDate(), a.GetAssignee()));

                }
                return Response<IList<Task>>.FromValue(d);
            }
            catch (Exception e)
            {
                return Response<IList<Task>>.FromError(e.Message);
            }
        }

        public Response AddBoard(string email, string name)
        {
            try
            {
                log.Info("Board has been added");
                boardController.AddBoard(email, name);
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn("Failed to add board");
                return new Response(e.Message);
            }
        }

        public Response RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                boardController.RemoveBoard(userEmail, creatorEmail, boardName);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        public Response<IList<Task>> InProgressTasks(string email)
        {
            try
            {
                List<BuisnessLayer.Task> c = boardController.InProgressTasks(email);
                List<Task> d = new List<Task>();
                foreach (BuisnessLayer.Task a in c)
                {
                    d.Add(new Task(a.GetId(), a.GetCreationTime(), a.GetTitle(), a.GetDescription(), a.GetDueDate(), a.GetAssignee()));
                }
                return Response<IList<Task>>.FromValue(d);
            }
            catch (Exception e)
            {
                return Response<IList<Task>>.FromError(e.Message);
            }
        }
        public Response JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                boardController.JoinBoard(userEmail, creatorEmail, boardName);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }

        }
        public Response AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            try
            {
                boardController.AssignTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId, emailAssignee);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        public Response<IList<String>> GetBoardNames(string userEmail)
        {
            try
            {
                List<String> c = boardController.GetBoardNames(userEmail);
                return Response<IList<String>>.FromValue(c);
            }
            catch (Exception e)
            {
                return Response<IList<String>>.FromError(e.Message);
            }

        }

    }
}