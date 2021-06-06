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
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); // Logs
        public BoardService()
        {
            boardController = new BoardController();
        }

        /// <summary>
        /// This method loads the data from the persistance.
        /// </summary>
        /// <param name="users">The users to load</param>
        /// <returns>The response of the action</returns>
        public Response LoadData(List<string> users)
        {
            try
            {
                boardController.LoadData(users);
                log.Info("Boards data has been loaded");
                return new Response();
            }
            catch (Exception e)
            {
                
                log.Warn("Failed load boards data");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Delete all boards from the database
        /// </summary>
        /// <returns>The response of the action</returns>
        public Response DeleteData()
        {
            try
            {
                boardController.DeleteData();
                log.Info("all boards data has been deleted");
                return new Response();
            }
            catch(Exception e)
            {
                log.Warn("Failed Delete all boards data");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="email">The email of the new user</param>
        public void Register(string email)
        {
            boardController.Register(email);
        }
        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="userEmail">The email address of the user, must be logged in</param>
        /// <param name="creatorEmail">The email address of the creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID.</param>
        /// <param name="limit">The new limit</param>
        /// <returns>The response of the action</returns>
        public Response LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                boardController.LimitColumn(userEmail, creatorEmail, boardName, columnOrdinal, limit);
                log.Info($"Colunm has been Limited with {limit} Tasks successfuly");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn("Failed to LimitColunm");
                return new Response(e.Message);
            }
        }
        // Getters
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
        /// <summary>
        /// Add a new task to the database
        /// </summary>
        /// <param name="userEmail">Email of the user. The user must be logged in.</param>
        /// <param name="creatorEmail">Email of the creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">The title of the task</param>
        /// <param name="description">The description of the task</param>
        /// <param name="dueDate">The due date of the task</param>
        /// <returns>A response object with a value set to the Task</returns>
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
                Console.WriteLine(e);
                log.Warn("Failed to add task");
                return Response<Task>.FromError(e.Message);
            }
        }
        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID</param>
        /// <param name="taskId">The task ID</param>
        /// <param name="dueDate">The new duedate</param>
        /// <returns></returns>
        public Response UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                boardController.ChangeDueDate(userEmail, creatorEmail, boardName, columnOrdinal, taskId, dueDate);
                log.Info("Tasks Dudate has been updated");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn("Failed to update tasks Duedate");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Update the title date of a task
        /// </summary>
        /// <param name="userEmail">Email of user. Must be logged in</param>
        /// <param name="creatorEmail">Email of creator</param>
        /// <param name="boardName">The board name</param>
        /// <param name="columnOrdinal">The column ID</param>
        /// <param name="taskId">The task ID</param>
        /// <param name="title">The new title</param>
        /// <returns>A response object</returns>
        public Response UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            try
            {
                boardController.ChangeTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, title);
                log.Info("Tasks Title has been updated");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn("Failed to update tasks Title");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="userEmail">Email of user. Must be logged in</param>
        /// <param name="creatorEmail">Email of creator</param>
        /// <param name="boardName">The board ID</param>
        /// <param name="columnOrdinal">The column ID</param>
        /// <param name="taskId">The task ID</param>
        /// <param name="description">The new description</param>
        /// <returns></returns>
        public Response UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            try
            {
                boardController.ChangeDescription(userEmail, creatorEmail, boardName, columnOrdinal, taskId, description);
                log.Info("Tasks Description has been updated");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn("Failed to update tasks Description");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Advance task to the next column
        /// </summary>
        /// <param name="userEmail">Email of user. Must be logged in</param>
        /// <param name="creatorEmail">Email of creator</param>
        /// <param name="boardName">The board ID</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="taskId">The task ID</param>
        /// <returns>The response of the action</returns>
        public Response AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                boardController.MoveTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId);
                log.Info("Task has been advenced successfuly");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn("Failed to advence task ");
                return new Response(e.Message);
            }
        }
        // Getter
        public Response<IList<Task>> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                List<BuisnessLayer.Task> c = boardController.GetColumn(userEmail, creatorEmail, boardName, columnOrdinal);
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
        /// <summary>
        /// Adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>A response object</returns>
        public Response AddBoard(string email, string name)
        {
            try
            {
                boardController.AddBoard(email, name);
                log.Info($"Board {name} has been added");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn("Failed to add board");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Removes a board to the specific user.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the creator</param>
        /// <param name="boardName">The board name</param>
        /// <returns></returns>
        public Response RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                boardController.RemoveBoard(userEmail, creatorEmail, boardName);
                log.Info($"Board {boardName} and all his tasks has been removed");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn($"Failed to Remove board {boardName}");
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Get all in progress task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>A response object</returns>
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
        /// <summary>
        /// Adds a board created by another user to the logged-in user. 
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns></returns>
        public Response JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                boardController.JoinBoard(userEmail, creatorEmail, boardName);
                log.Info($"user with the email {userEmail} has joined {creatorEmail} board, on the board name {boardName}");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn($"User with the email {userEmail} has failed to join to board {boardName}");
                return new Response(e.Message);
            }

        }

        /// <summary>
        /// Assigns a task to a user
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>        
        /// <param name="emailAssignee">Email of the user to assign to task to</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            try
            {
                boardController.AssignTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId, emailAssignee);
                log.Info($"user with the email {userEmail} has assigned Task to user with the email {emailAssignee}");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn($"Failed to assign task");
                return new Response(e.Message);
            }
        }
        // Getter
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
        public Response AddColumn (string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {
            try
            {
                boardController.AddColumn(userEmail, creatorEmail, boardName, columnOrdinal,columnName);
                log.Info($"Column with the name {columnName} has been created");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn($"Failed to create Column");
                return new Response(e.Message);
            }

        }
        public Response RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                boardController.RemoveColumn(userEmail, creatorEmail, boardName, columnOrdinal);
                log.Info($"Column has been removed");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn($"Failed to Remove Column");
                return new Response(e.Message);
            }

        }
        public Response RenameColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal,string newColumnName)
        {
            try
            {
                boardController.RenameColumn(userEmail, creatorEmail, boardName, columnOrdinal,newColumnName);
                log.Info($"Column number {columnOrdinal} has been renamed");
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn($"Failed to Rename Column");
                return new Response(e.Message);
            }
        }
        public Response MoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize)
        {
            try
            {
                boardController.MoveColumn(userEmail, creatorEmail, boardName, columnOrdinal, shiftSize);
                log.Info($"Column number {columnOrdinal} has been transfered to be Column number {columnOrdinal+shiftSize}" );
                return new Response();
            }
            catch (Exception e)
            {
                log.Warn($"Failed to Move Column");
                return new Response(e.Message);
            }
        }


    }
}
