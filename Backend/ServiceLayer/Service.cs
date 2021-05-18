﻿using System.Collections.Generic;
using System;
using System.Linq;
using IntroSE.Kanban.Backend.BuisnessLayer;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Service
    {
        UserService userService;
        BoardService boardService;


        public Service()
        {
            userService = new UserService();
            boardService = new BoardService();
        }
        ///<summary>This method loads the data from the persistance.
        ///         You should call this function when the program starts. </summary>
        public Response LoadData()
        {
            Response a = userService.LoadData();

            if (a.ErrorOccured)
                return a;
            
            List<string> users = userService.BringAllUsersEmail();
            boardService.LoadData(users);
            return new Response();

        }
        ///<summary>Removes all persistent data.</summary>
        public Response DeleteData()
        {
            List<string> users = userService.BringAllUsersEmail();
            Response a = boardService.DeleteData(users);
           
            if (a.ErrorOccured)
               return a;

            userService.DeleteData();
            return new Response();
            
        }
        ///<summary>This method registers a new user to the system.</summary>
        ///<param name="email">the user e-mail address, used as the username for logging the system.</param>
        ///<param name="password">the user password.</param>
        ///<returns cref="Response">The response of the action</returns>
        public Response Register(string email, string password)
        {

            Response a = userService.Register(email, password);
            if (a.ErrorOccured)
                return a;
            boardService.Register(email);
            return new Response();

        }
        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            return userService.Login(email, password);
        }
        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            Response a = userService.ValidateUserLoggin(email);
            if (a.ErrorOccured)
                return a;
            return userService.Logout(email);
        }

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>

        public Response LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {

            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return a;
            return boardService.LimitColumn(userEmail, creatorEmail, boardName, columnOrdinal, limit);

        }

        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>
        public Response<int> GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {

            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return Response<int>.FromError(a.ErrorMessage);
            return boardService.GetColumnLimit(userEmail, creatorEmail, boardName, columnOrdinal);
        }



        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>
        public Response<string> GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return Response<string>.FromError(a.ErrorMessage);
            return boardService.GetColumnName(userEmail, creatorEmail, boardName, columnOrdinal);
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
        public Response<Task> AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return Response<Task>.FromError(a.ErrorMessage);
            return boardService.AddTask(userEmail, creatorEmail, boardName, title, description, dueDate);

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
        public Response UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {

            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return a;
            return boardService.UpdateTaskDueDate(userEmail, creatorEmail, boardName, columnOrdinal, taskId, dueDate);


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
        public Response UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {

            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return a;
            return boardService.UpdateTaskTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, title);


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
        public Response UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {

            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return a;
            return boardService.UpdateTaskDescription(userEmail, creatorEmail, boardName, columnOrdinal, taskId, description); ;


        }
        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {

            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return a;
            return boardService.AdvanceTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId);

        }
        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {


            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return Response<IList<Task>>.FromError(a.ErrorMessage);
            return boardService.GetColumn(userEmail, creatorEmail, boardName, columnOrdinal);

        }
        /// <summary>
        /// Adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string email, string name)
        {
            
            Response a = userService.ValidateUserLoggin(email);
            if (a.ErrorOccured)
                return a;
            return boardService.AddBoard(email, name);

        }
        /// <summary>
        /// Removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {

            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return a;
            return boardService.RemoveBoard(userEmail, creatorEmail, boardName);


        }
        /// <summary>
        /// Returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>A response object with a value set to the list of tasks, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> InProgressTasks(string email)
        {
            Response a = userService.ValidateUserLoggin(email);
            if (a.ErrorOccured)
                return Response<IList<Task>>.FromError(a.ErrorMessage);

            return boardService.InProgressTasks(email);
        }
        /// <summary>
        /// Adds a board created by another user to the logged-in user. 
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return a;
            return boardService.JoinBoard(userEmail, creatorEmail, boardName);
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
            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return a;
            return boardService.AssignTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId, emailAssignee);
        }
        /// <summary>
        /// Returns the list of board of a user. The user must be logged-in. The function returns all the board names the user created or joined.
        /// </summary>
        /// <param name="userEmail">The email of the user. Must be logged-in.</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public Response<IList<String>> GetBoardNames(string userEmail)
        {
            Response a = userService.ValidateUserLoggin(userEmail);
            if (a.ErrorOccured)
                return Response<IList<String>>.FromError(a.ErrorMessage);

            return boardService.GetBoardNames(userEmail);
        }
    }
}