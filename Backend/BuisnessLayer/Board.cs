using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class Board
    {
        public int id;
        public string name;
        private List<Colunm> board;
        private int idcounter = 1;
        private List<string> users;
        private readonly string creator;

        // Constructor
        public Board(int id, string name, string creator)
        {

            this.id = id;
            this.name = name;
            this.creator = creator;
            board = new List<Colunm>();
            board.Add(new Colunm("backlog", new Dictionary<int, Task>()));
            board.Add(new Colunm("in progress", new Dictionary<int, Task>()));
            board.Add(new Colunm("done", new Dictionary<int, Task>()));
            this.users = new List<string>();
        }
        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="duedate">The due date if the new task.</param>
        /// <param name="title">Title of the new task.</param>
        /// <param name="descripton">Description of the new task.</param>
        /// <returns></returns>
        public Task AddTask(string email, DateTime duedate, string title, string descripton)
        {

            Task c = board[0].AddTask(idcounter, duedate, email, title, descripton);
            idcounter = idcounter + 1;
            return c;
        }
        /// <summary>
        /// Remove the task.
        /// </summary>
        /// <param name="id">The id of the task.</param>
        /// <param name="columnOrdinal">The column Ordinal of the task.</param>
        /// <returns>The task.</returns>
        public Task RemoveTask(int id, int columnOrdinal)
        {
            Task c = board[columnOrdinal].RemoveTask(id);
            return c;
        }
        /// <summary>
        /// Move a task to the next column ordinal.
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <param name="id">The task's ID</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        public void MoveTask(string email, int id, int columnOrdinal)
        {

            if (columnOrdinal == 0)
            {
                Task toprogress = board[0].RemoveTask(id);
                board[1].AddTask(toprogress.GetId(), toprogress.GetDueDate(), toprogress.GetAssignee(), toprogress.GetTitle(), toprogress.GetDescription());

            }
            else if (columnOrdinal == 1)
            {
                Task todone = board[1].RemoveTask(id);
                board[2].AddTask(todone.GetId(), todone.GetDueDate(), todone.GetAssignee(), todone.GetTitle(), todone.GetDescription());

            }
            else
                throw new Exception("columnOrdinal can be only 0 or 1");
        }

        /// <summary>
        /// Change the due date of a task
        /// </summary>
        /// <param name="id">The task's ID</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="newdueDate">The new due date</param>
        public void ChangeDueDate(int id, int columnOrdinal, DateTime newdueDate)
        {
            if (columnOrdinal == 0 || columnOrdinal == 1)
                board[columnOrdinal].ChangeDueDate(id, newdueDate);

            else
                throw new Exception("you can change Duedate only from backlog column or inprogress column");


        }
        /// <summary>
        /// Change the title of a task
        /// </summary>
        /// <param name="id">The task's ID</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="title">The new title</param>
        public void ChangeTitle(int id, int columnOrdinal, string title)
        {
            if (columnOrdinal == 0 || columnOrdinal == 1 || columnOrdinal == 2)
                board[columnOrdinal].ChangeTitle(id, title);
            else
                throw new Exception("column not found");
        }
        /// <summary>
        /// Change description of a task
        /// </summary>
        /// <param name="id">The task's ID</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="description">The new description</param>
        public void ChangeDescription(int id, int columnOrdinal, string description)
        {
            if (columnOrdinal == 0 || columnOrdinal == 1 || columnOrdinal == 2)
                board[columnOrdinal].ChangeDescription(id, description);
            else
                throw new Exception("column not found");
        }
        /// <summary>
        /// Change the mail of assignee
        /// </summary>
        /// <param name="id">The Task's ID</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="newEmail">The new mail</param>
        public void ChangeEmailAssignee(int id, int columnOrdinal, string newEmail)
        {
            if (columnOrdinal <= 2 || columnOrdinal >= 0)
            {
                board[columnOrdinal].ChangeEmailAssignee(id, newEmail);
            }
            else
                throw new Exception("columnOrdinal should be between 0 and 2");
        }
        // Getter
        public string GetColumnName(int columnOrdinal)
        {
            if (columnOrdinal != 0 && columnOrdinal != 1 && columnOrdinal != 2)
            {
                throw new Exception("column number can be 0,1 or 2");
            }
            return board[columnOrdinal].GetColumnName();
        }
        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="limit">The new limit</param>
        public void LimitColunm(int columnOrdinal, int limit)
        {
            if (columnOrdinal != 0 && columnOrdinal != 1 && columnOrdinal != 2)
                throw new Exception("column number can be 0,1 or 2");

            board[columnOrdinal].LimitTasks(limit);
        }
        // Getters
        public int GetColumnLimit(int columnOrdinal)
        {
            if (columnOrdinal != 0 && columnOrdinal != 1 && columnOrdinal != 2)
                throw new Exception("column number can be 0,1 or 2");

            return board[columnOrdinal].GetColumnLimit();
        }
        public List<Task> GetColumn(int columnOrdinal)
        {
            if (columnOrdinal != 0 && columnOrdinal != 1 && columnOrdinal != 2)
                throw new Exception("column number can be 0,1 or 2");

            return board[columnOrdinal].Tasks;
        }
        // Getter
        private int GetBoardId()
        {
            return this.id;
        }
        public string GetCreator()
        {
            return this.creator;
        }
        /// <summary>
        /// Verify the board member
        /// </summary>
        /// <param name="email">The mail of the user</param>
        public void BoardMemberVerify(string email)
        {
            if (!(creator == email))
            {
                if (!(users.Contains(email)))
                    throw new Exception("user is not a board member");
            }
        }
        /// <summary>
        /// Verify the task assignee
        /// </summary>
        /// <param name="email">The mail of the user</param>
        /// <param name="task">The task to verify</param>
        public void TaskAssigneeVerify(string email, Task task)
        {
            if (!(email == task.GetAssignee()))
                throw new Exception($"user with the email {email} is not assign for the task");
        }
        /// <summary>
        /// Add user to be a membet in the board.
        /// </summary>
        /// <param name="email">The mail of the user</param>
        public void AddtoBoardUsers(string email)
        {
            if (users.Contains(email))
                throw new Exception("user is already a member in the board");
            users.Add(email);
        }
        // Getters
        public Task GetTask(int id, int columnOrdinal)
        {
            if (columnOrdinal <= 2 || columnOrdinal >= 0)
                return board[columnOrdinal].GetTask(id);
            else
                throw new Exception("Column doesnt exist");
        }
        public List<Colunm> GetBoardColumns()
        {
            return board;
        }
        public bool GetColumnIfLimited(int columnOrdinal)
        {
            if (columnOrdinal <= 2 || columnOrdinal >= 0)
            return board[columnOrdinal].GetColumnIfLimited(); 
            else
                throw new Exception("Column doesnt exist");

        }
        public List<string> GetAssigneeList()
        {
            return users;
        }
    }
}