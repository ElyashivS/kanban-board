﻿using System.Collections.Generic;
using System;
using System.Linq;
using IntroSE.Kanban.Backend.BuisnessLayer;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Service
    {
        UserController userController;
        BoardController boardController;


        public Service()
        {
            userController = new UserController();
            boardController = new BoardController();
        }
        ///<summary>This method loads the data from the persistance.
        ///         You should call this function when the program starts. </summary>
        public Response LoadData()
        {
            throw new NotImplementedException();
        }
        ///<summary>Removes all persistent data.</summary>
        public Response DeleteData()
        {
            throw new NotImplementedException();
        }
        ///<summary>This method registers a new user to the system.</summary>
        ///<param name="email">the user e-mail address, used as the username for logging the system.</param>
        ///<param name="password">the user password.</param>
        ///<returns cref="Response">The response of the action</returns>
        public Response Register(string email, string password)
        {
            try
            {
                userController.Register(email, password);
                boardController.Register(email);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            
        }
        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            try
            {
                BuisnessLayer.User user = userController.Login(email, password);
                return Response<User>.FromValue(new User(user.email));
            }
            catch (Exception e)
            {
                return Response<User>.FromError(e.Message);
            }
        }
        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            
            try
            {
                ValidateUserLoggin(email);
                userController.Logout(email);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        private void ValidateUserLoggin(string email)
        {
             userController.ValidateUserLoggin(email);
        }
        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>

        public Response LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                ValidateUserLoggin(email);
                boardController.LimitColumn(email, boardName, columnOrdinal, limit);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }

        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>
        public Response<int> GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            
            try
            {
                ValidateUserLoggin(email);
                int c=boardController.GetcolumnLimit(email, boardName, columnOrdinal);
                return Response<int>.FromValue(c);
            }
            catch (Exception e)
            {
                return Response<int>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>
        public Response<string> GetColumnName(string email, string boardName, int columnOrdinal)
        {
            try
            {
                ValidateUserLoggin(email);

                string c = boardController.GetColumnName(email, boardName, columnOrdinal);
                return Response<string>.FromValue(c);
            }
            catch (Exception e)
            {
                return Response<string>.FromError(e.Message);
            }
        }

        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<Task> AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            try
            {
                ValidateUserLoggin(email);
                BuisnessLayer.Task c = boardController.AddTask(email, boardName, title, description, dueDate);
                
                return Response<Task>.FromValue(new Task(c.GetId(), c.GetCreationTime(), c.GetTitle(), c.GetDescription(), c.GetDueDate()));
            }
            catch (Exception e)
            {
                return Response<Task>.FromError(e.Message);
            }
        }
        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                ValidateUserLoggin(email);
                boardController.ChangeDueDate(email, boardName, columnOrdinal, taskId, dueDate);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            try
            {
                ValidateUserLoggin(email);
                boardController.ChangeTitle(email, boardName, columnOrdinal, taskId, title);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            try
            {
                ValidateUserLoggin(email);
                boardController.ChangeDescription(email, boardName, columnOrdinal, taskId, description);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                ValidateUserLoggin(email);
                boardController.MoveTask(email, boardName, columnOrdinal, taskId);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> GetColumn(string email, string boardName, int columnOrdinal)
        {
            
            try
            {
                ValidateUserLoggin(email);
                List<BuisnessLayer.Task> c = boardController.GetColunm(email, boardName, columnOrdinal);
                List<Task> d = new List<Task>();
                foreach (BuisnessLayer.Task a in c)
                {
                    d.Add(new Task(a.id, DateTime.Now, a.GetTitle(), a.GetDescription(), a.GetDueDate()));

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
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string email, string name)
        {
            try
            {
                ValidateUserLoggin(email);
                boardController.AddBoard(email, name);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveBoard(string email, string name)
        {
            try
            {
                ValidateUserLoggin(email);
                boardController.RemoveBoard(email, name);
                return new Response();
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
        }
        /// <summary>
        /// Returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>A response object with a value set to the list of tasks, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> InProgressTasks(string email)
        {
            
            
            try
            {
                ValidateUserLoggin(email);
                List<BuisnessLayer.Task> c=boardController.InProgressTasks(email);
                List<Task> d = new List<Task>();
                foreach (BuisnessLayer.Task a in c)
                {
                    d.Add(new Task(a.GetId(),a.GetCreationTime(),a.GetTitle(),a.GetDescription(),a.GetDueDate()));
                }
                return Response<IList<Task>>.FromValue(d);
            }
            catch (Exception e)
            {
                return Response<IList<Task>>.FromError(e.Message);
            }
        }
    }
}