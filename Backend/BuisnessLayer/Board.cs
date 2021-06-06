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
        private List<Column> board;
        private int taskIdCounter = 1;
        private int ColumnIdCounter;
        private List<string> users;
        private readonly string creator;

        // Constructor
        public Board(int id, string name, string creator)
        {

            this.id = id;
            this.name = name;
            this.creator = creator;
            board = new List<Column>();
            board.Add(new Column(1,"backlog", new Dictionary<int, Task>()));
            board.Add(new Column(2,"in progress", new Dictionary<int, Task>()));
            board.Add(new Column(3,"done", new Dictionary<int, Task>()));
            ColumnIdCounter = 4;
            this.users = new List<string>();
        }
        
        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="duedate">The due date if the new task.</param>
        /// <param name="title">Title of the new task.</param>
        /// <param name="descripton">Description of the new task.</param>
        /// <returns>The new task</returns>
        public Task AddTask(string email, DateTime duedate, string title, string descripton)
        {

            Task c = board[0].AddTask(taskIdCounter, duedate, email, title, descripton);
            taskIdCounter = taskIdCounter + 1;
            return c;
        }
        public Task AddTaskForData(int taskId,string email, DateTime duedate, string title, string descripton)
        {
            Task c = board[0].AddTaskForData(taskIdCounter, duedate, email, title, descripton);
            if (taskId > taskIdCounter)
                taskIdCounter = taskId;
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

            Task toprogress = board[columnOrdinal].RemoveTask(id);
            board[columnOrdinal+1].AddTask(toprogress.GetId(), toprogress.GetDueDate(), toprogress.GetAssignee(), toprogress.GetTitle(), toprogress.GetDescription());


        } 

        /// <summary>
        /// Change the due date of a task
        /// </summary>
        /// <param name="id">The task's ID</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="newdueDate">The new due date</param>
        public void ChangeDueDate(int id, int columnOrdinal, DateTime newdueDate)
        {
            CheckOrdinalValidality(columnOrdinal);
            if (columnOrdinal == board.Count)
                throw new Exception("cannot change a finished Task Duedate");
            board[columnOrdinal].ChangeDueDate(id, newdueDate);

            
        }
        /// <summary>
        /// Change the title of a task
        /// </summary>
        /// <param name="id">The task's ID</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="title">The new title</param>
        public void ChangeTitle(int id, int columnOrdinal, string title)
        {
            CheckOrdinalValidality(columnOrdinal);
            board[columnOrdinal].ChangeTitle(id, title);
            
        }
        /// <summary>
        /// Change description of a task
        /// </summary>
        /// <param name="id">The task's ID</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="description">The new description</param>
        public void ChangeDescription(int id, int columnOrdinal, string description)
        {
            CheckOrdinalValidality(columnOrdinal);
            board[columnOrdinal].ChangeDescription(id, description);
            
        }
        /// <summary>
        /// Change the mail of assignee
        /// </summary>
        /// <param name="id">The Task's ID</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="newEmail">The new mail</param>
        public void ChangeEmailAssignee(int id, int columnOrdinal, string newEmail)
        {
            CheckOrdinalValidality(columnOrdinal);
            board[columnOrdinal].ChangeEmailAssignee(id, newEmail);
            
        }
        public void ChangeEmailAssigneeForData(int id, int columnOrdinal, string newEmail)
        {
            board[columnOrdinal].ChangeEmailAssigneeForData(id, newEmail);
        }
        // Getter
        public string GetColumnName(int columnOrdinal)
        {
            CheckOrdinalValidality(columnOrdinal);
            return board[columnOrdinal].name;
        }
        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="limit">The new limit</param>
        public void LimitColumn(int columnOrdinal, int limit)
        {
            CheckOrdinalValidality(columnOrdinal);

            board[columnOrdinal].LimitTasks(limit);
        }
        public void LimitColumnForData(int columnOrdinal, int limit)
        {
            board[columnOrdinal].LimitTasksForData(limit);
        }
        // Getters
        public int GetColumnLimit(int columnOrdinal)
        {
            CheckOrdinalValidality(columnOrdinal);

            return board[columnOrdinal].GetColumnLimit();
        }
        public List<Task> GetColumn(int columnOrdinal)
        {
            CheckOrdinalValidality(columnOrdinal);

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
        public void AddtoBoardUsersForData(string email)
        {
            users.Add(email);
        }
        // Getters
        public Task GetTask(int id, int columnOrdinal)
        {
            CheckOrdinalValidality(columnOrdinal);
            return board[columnOrdinal].GetTask(id);
            
        }
        public List<Column> GetBoardColumns()
        {
            return board;
        }
        public bool GetColumnIfLimited(int columnOrdinal)
        {
            CheckOrdinalValidality(columnOrdinal);
            return board[columnOrdinal].GetColumnIfLimited(); 
            
                

        }
        public List<string> GetAssigneeList()
        {
            return users;
        }
        //checks if the ColumnOrdinal number is valid
        public void CheckOrdinalValidality(int columnOrdinal)
        {
            if (columnOrdinal > board.Count || columnOrdinal < 0)
                throw new Exception("Column doesnt exist");
        }
        //returns the columnId  by giving a task in it
        public int ColumnIdByColumnOrdinal(int ColumnOrdinal)
            
	{
            return board[ColumnOrdinal].columnId;

    }
        public int ColumnOrdinalByColumnId(int ColumnId)
        {
            
            
                for (int i = 0; i < board.Count; i++)
                {
                    if (ColumnId == board[i].GetColumnId())
                        return i;
                }

            return -1;
        }
        public Column AddColumn(int columnOrdinal,string columnName)
        {
            
            Column toadd = new Column(ColumnIdCounter, columnName, new Dictionary<int, Task>());
            ColumnIdCounter = ColumnIdCounter + 1;
            board.Add(toadd);
            for (int i = board.Count-1; i > 0; i=i-1)
            {
                if (columnOrdinal < i)
                {
                    Column temp = board[i];
                    board[i] = board[i-1];
                    board[i -1] = temp;
                }
                else
                    return toadd;
                    
            }
            return toadd;
        }
        public Column RemoveColumn(int columnOrdinal)
        {
            if (board.Count == 2)
                throw new Exception("board reached his minimal state");
            CheckOrdinalValidality(columnOrdinal);
            if (board[columnOrdinal].isEmpty())
            {
                Column toRemove = board[columnOrdinal];
                board.Remove(toRemove);
                return board[columnOrdinal];
            }
            if (columnOrdinal == 0)
            {
                if ((!board[columnOrdinal + 1].GetColumnIfLimited()) || (board[columnOrdinal + 1].GetColumnLimit().CompareTo(board[columnOrdinal + 1].Size() + board[columnOrdinal].Size()) > 0))
                {
                    foreach (Task t in board[columnOrdinal].Tasks)
                    {
                        board[columnOrdinal].RemoveTask(t.id);
                        board[columnOrdinal + 1].AddTask(t);
                    }
                    Column toRemove = board[columnOrdinal];
                    board.Remove(toRemove);
                    return board[columnOrdinal];
                }
                else
                    throw new Exception("Column could not be deleted");
            }
            else if (columnOrdinal < board.Count-1 || columnOrdinal > 0)
            {
                if ((!board[columnOrdinal - 1].GetColumnIfLimited()) || (board[columnOrdinal - 1].GetColumnLimit().CompareTo(board[columnOrdinal - 1].Size() + board[columnOrdinal].Size()) > 0))
                {
                    foreach (Task t in board[columnOrdinal].Tasks)
                    {
                        board[columnOrdinal].RemoveTask(t.id);
                        board[columnOrdinal - 1].AddTask(t);

                    }
                    Column toRemove = board[columnOrdinal];
                    board.Remove(toRemove);
                    return board[columnOrdinal-1];
                }
                else if ((!board[columnOrdinal + 1].GetColumnIfLimited()) || (board[columnOrdinal + 1].GetColumnLimit().CompareTo(board[columnOrdinal + 1].Size() + board[columnOrdinal].Size()) > 0))
                {
                    foreach (Task t in board[columnOrdinal].Tasks)
                    {
                        board[columnOrdinal].RemoveTask(t.id);
                        board[columnOrdinal + 1].AddTask(t);
                    }
                    Column toRemove = board[columnOrdinal];
                    board.Remove(toRemove);
                    return board[columnOrdinal];
                }
                else
                    throw new Exception("Column could not be deleted");
   
            }
            else 
            {
                if ((!board[columnOrdinal - 1].GetColumnIfLimited()) || (board[columnOrdinal - 1].GetColumnLimit().CompareTo(board[columnOrdinal - 1].Size() + board[columnOrdinal].Size()) > 0))
                {
                    foreach (Task t in board[columnOrdinal].Tasks)
                    {
                        board[columnOrdinal].RemoveTask(t.id);
                        board[columnOrdinal - 1].AddTask(t);

                    }
                    Column toRemove = board[columnOrdinal];
                    board.Remove(toRemove);
                    return board[columnOrdinal-1];
                }
                else
                    throw new Exception("Column Couldnt be deleted");

            }
            



        }
        public Column RenameColumn(int columnOrdinal,string newColumnName)
        {
            board[columnOrdinal].name = newColumnName;
            return board[columnOrdinal];
        }
        public void CheckIfNotLastOrdinal(int ColumnOrdinal)
        {
            if (ColumnOrdinal == board.Count - 1)
                throw new Exception("Cannot limit done Column");
        }
        public Column MoveColumn(int columnOrdinal,int shiftSize)
        {
            if (!board[columnOrdinal].isEmpty())
                throw new Exception("only empty columns can be moved");
            Column toshift = board[columnOrdinal];
            if (shiftSize > 0)
            {
                for (int i = columnOrdinal; i < columnOrdinal + shiftSize; i++)
                {
                    board[i] = board[i + 1];
                }
            }
            else if (shiftSize < 0)
            {
                for (int i = columnOrdinal; i > columnOrdinal+shiftSize ; i=i-1)
                {
                    board[i] = board[i -1];
                }
            }
            board[columnOrdinal + shiftSize] = toshift;
            return board[columnOrdinal + shiftSize];
        }
        
            
        
        
    }
}