using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class BoardController
    {
        private Dictionary<string, Dictionary<Tuple<string,string>, Board>> boardController;
        private BoardDalController BoardTable = new BoardDalController();
        private ColumnDalController ColumnTable = new ColumnDalController();
        private TaskDalController TaskTable = new TaskDalController();
        int boardIdCounter = 1;
        // Constructor
        public BoardController()
        {
            this.boardController = new Dictionary<string, Dictionary<Tuple<string, string>, Board>>();
        }
        /// <summary>
        /// Load data from the persistance.
        /// </summary>
        /// <param name="users">List of all users</param>
        public void LoadData(List<string> users)
        {
            foreach (string user in users)
            {
                Register(user);
            }
            List<DTO> boards = BoardTable.Select();
            List<DTO> columns = ColumnTable.Select();
            List<DTO> tasks = TaskTable.Select();
            List<DTO> assignees = BoardTable.SelectAssigneeList();
            foreach (BoardDTO b in boards)
            {
                Board k = new Board(b.ID, b.Name, b.Creator);
                boardController[b.Creator].Add(new Tuple<string,string>(b.Creator,b.Name),k );
                if (b.ID >= boardIdCounter)
                    boardIdCounter = b.ID;
                foreach (ColumnDTO c in columns)
                {
                    if (c.BoardId == b.ID)
                    {
                        LimitColumnForLoad(b.Creator, b.Creator, b.Name, GetColumnOrdinal(c.Name), c.ColumnLimiter);
                    }
                }
                foreach (AssigneeDTO a in assignees)
                {
                    if (a.ID == b.ID)
                    {
                                if (a.Assignee != b.Creator)
                                JoinBoardForLoad(a.Assignee, b.Creator, b.Name);
                    }
                }
                foreach (TaskDTO t in tasks)
                {
                    if (t.BoardId == b.ID)
                    {
                        if (t.ColumnName.Equals("backlog"))
                        {
                            AddTaskForLoad(t.ID,b.Creator, b.Creator, b.Name, t.Title, t.Description, t.DueDate);
                            AssignTaskForLoad(b.Creator, b.Creator, b.Name, GetColumnOrdinal(t.ColumnName), t.ID, t.Assignee);
                        }
                        if(t.ColumnName.Equals("in progress")){
                            AddTaskForLoad(t.ID,b.Creator, b.Creator, b.Name, t.Title, t.Description, t.DueDate);
                            MoveTaskForLoad(b.Creator, b.Creator, b.Name, 0, t.ID);
                            AssignTaskForLoad(b.Creator, b.Creator, b.Name, GetColumnOrdinal(t.ColumnName), t.ID, t.Assignee);
                        }
                        if (t.ColumnName.Equals("done"))
                        {
                            AddTaskForLoad(t.ID,b.Creator, b.Creator, b.Name, t.Title, t.Description, t.DueDate);
                            MoveTaskForLoad(b.Creator, b.Creator, b.Name, 0, t.ID);
                            MoveTaskForLoad(b.Creator, b.Creator, b.Name, 1, t.ID);
                            AssignTaskForLoad(b.Creator, b.Creator, b.Name, GetColumnOrdinal(t.ColumnName), t.ID, t.Assignee);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Removes all persistent data.
        /// </summary>
        public void DeleteData()
        {
            BoardTable.DeleteBoardTable();
            BoardTable.DeleteAssigneeTable();
            ColumnTable.DeleteColumnTable();
            TaskTable.DeleteTaskTable();
        }
        
        /// <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="email">The email</param>
        public void Register(string email)
        {
            boardController.Add(email, new Dictionary<Tuple<string,string>,Board>());
        }
        /// <summary>
        /// Adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>The new Board</returns>
        public Board AddBoard(string email, string name)
        {
            Tuple<string, string> a = new Tuple<string, string>(email, name);
            if (!boardController[email].ContainsKey(a))
            {
                Board b = new Board(boardIdCounter, name, email);
                boardController[email].Add(a, b);
                b.AddtoBoardUsers(email);

                BoardTable.Insert(new BoardDTO(boardIdCounter, name, email));
                BoardTable.InsertToAsigneeList(b.id, email);
                boardIdCounter = boardIdCounter + 1;
                ColumnDTO backlog = new ColumnDTO(b.id, 1, 0, "backlog",-1);
                ColumnDTO inprogress = new ColumnDTO(b.id, 2, 1, "in progress", -1);
                ColumnDTO done = new ColumnDTO(b.id, 3, 2, "done", -1);
                ColumnTable.Insert(backlog);
                ColumnTable.Insert(inprogress);
                ColumnTable.Insert(done);
                return b;
            }
            else
                throw new Exception($"Board with the name {name} already exist");
        }
        /// <summary>
        /// Removes a board to the specific user.
        /// </summary>
        /// <param name="userEmail">Email of the user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>The board to remove</returns>
        public Board RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {
            Board c = FindBoard(creatorEmail, boardName);
            if (!userEmail.Equals(c.GetCreator()))
                throw new Exception("only the creator of the board may delete the board");
            RemoveBoardFromAssigneeList(c.GetAssigneeList(), boardName, c.id,c.GetCreator());
            List<Column> todeleteColumns = c.GetBoardColumns();
            for (int i = 0; i < todeleteColumns.Count; i++)
            {
                if(c.GetColumnIfLimited(i))
                {
                    {
                        ColumnDTO deleting = new ColumnDTO(c.id,todeleteColumns[i].GetColumnId(),c.ColumnOrdinalByColumnId(todeleteColumns[i].GetColumnId()),todeleteColumns[i].name, c.GetColumnLimit(i));
                        ColumnTable.Delete(deleting);
                    }
                }
            }
            Tuple<string, string> t = new Tuple<string, string>(creatorEmail, boardName);
            boardController[creatorEmail].Remove(t);
            
            List<Column> toremove = c.GetBoardColumns();
            foreach (Column i in toremove)
            {
                foreach (Task a in i.Tasks)
                {
                    TaskDTO todelete = TaskTable.SpecificSelect(c.id,i.GetColumnId(), a.id);
                    TaskTable.Delete(todelete);
                }
            }
            BoardTable.Delete(BoardTable.SpecificSelect(c.id));

            return c;
        }
        /// <summary>
        /// Add a new task
        /// </summary>
        /// <param name="userEmail">Email of the user. The user must be logged in.</param>
        /// <param name="creatorEmail">Email of creator.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>The new task.</returns>
        public Task AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            Task b = c.AddTask(userEmail, dueDate, title, description);
            TaskDTO toadd = new TaskDTO(c.id, c.ColumnIdByColumnOrdinal(0), b.id, c.GetColumnName(0), userEmail, b.GetCreationTime(), b.GetDueDate(), b.GetTitle(), b.GetDescription()) ;
            TaskTable.Insert(toadd);
            return b;
        }
        /// <summary>
        /// Move a task to the next column ordinal.
        /// </summary>
        /// <param name="userEmail">The user's email</param>
        /// <param name="creatorEmail">The creator's email</param>
        /// <param name="boardName">The board name</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="taskId">The task's ID</param>
        public void MoveTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.CheckIfNotLastOrdinal(columnOrdinal);
            c.BoardMemberVerify(userEmail);
            c.TaskAssigneeVerify(userEmail, c.GetTask(taskId, columnOrdinal));
            c.MoveTask(userEmail, taskId, columnOrdinal);
            TaskTable.Update(c.id, taskId, "ColumnName", c.GetColumnName(columnOrdinal + 1));
            TaskTable.Update(c.id, taskId, "ColumnId", c.ColumnIdByColumnOrdinal(columnOrdinal + 1));


           // if (columnOrdinal == 0)
              //  TaskTable.Update(c.id, taskId, "columnName", "in progress");
           // if (columnOrdinal == 1)
               // TaskTable.Update(c.id, taskId, "columnName", "done");
        }
        /// <summary>
        /// Change the due date of a task
        /// </summary>
        /// <param name="userEmail">The user's email</param>
        /// <param name="creatorEmail">The creator's email</param>
        /// <param name="boardName">The board name</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="taskId">The task ID</param>
        /// <param name="dueDate">The new due date</param>
        public void ChangeDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            c.TaskAssigneeVerify(userEmail, c.GetTask(taskId, columnOrdinal));
            c.ChangeDueDate(taskId, columnOrdinal, dueDate);
            TaskTable.Update(c.id,c.ColumnIdByColumnOrdinal(columnOrdinal), taskId, "DueDate", dueDate);
        }
        /// <summary>
        /// Change the title of a task
        /// </summary>
        /// <param name="userEmail">The user's email</param>
        /// <param name="creatorEmail">The creator's email</param>
        /// <param name="boardName">The board name</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="taskId">The task ID</param>
        /// <param name="title">The new title</param>
        public void ChangeTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            c.TaskAssigneeVerify(userEmail, c.GetTask(taskId, columnOrdinal));
            c.ChangeTitle(taskId, columnOrdinal, title);
            TaskTable.Update(c.id, taskId, "Title", title);
        }
        /// <summary>
        /// Change description of a task
        /// </summary>
        /// <param name="userEmail">The user's email</param>
        /// <param name="creatorEmail">The creator's email</param>
        /// <param name="boardName">The board ID</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="taskId">The task ID</param>
        /// <param name="description">The new description</param>
        public void ChangeDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            c.TaskAssigneeVerify(userEmail, c.GetTask(taskId, columnOrdinal));
            c.ChangeDescription(taskId, columnOrdinal, description);
            TaskTable.Update(c.id, taskId, "Description", description);
        }
        // Getter
        public string GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            return c.GetColumnName(columnOrdinal);
        }
        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="userEmail">The user's email</param>
        /// <param name="creatorEmail">The creator's email</param>
        /// <param name="boardName">The voard name</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="limit">The new limit</param>
        public void LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            
            Board c = FindBoard(creatorEmail, boardName);
            c.CheckIfNotLastOrdinal(columnOrdinal);
            c.BoardMemberVerify(userEmail);
            bool iflimited = c.GetColumnIfLimited(columnOrdinal);
            c.LimitColumn(columnOrdinal, limit);
            string fordata = c.GetColumnName(columnOrdinal);
                ColumnTable.Update(c.id,c.ColumnIdByColumnOrdinal(columnOrdinal),  "ColumnLimiter", limit);
            
        }
        // Getters
        public int GetcolumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            return c.GetColumnLimit(columnOrdinal);
        }
        public List<Task> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            List<Task> b = c.GetColumn(columnOrdinal);
            return b;
        }
        /// <summary>
        /// Insert a user to a board as assignee
        /// </summary>
        /// <param name="userEmail">The user's email</param>
        /// <param name="creatorEmail">The creator's email</param>
        /// <param name="boardName">The board name</param>
        public void JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.AddtoBoardUsers(userEmail);
            boardController[userEmail].Add(new Tuple<string, string>(creatorEmail,boardName), c);
            BoardTable.InsertToAsigneeList(c.id, userEmail);
        }
        /// <summary>
        /// Brings a list of Tasks that the user is Assignee for and in -"in progress column"
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns></returns>
        public List<Task> InProgressTasks(string email)
        {
            List<Task> c = new List<Task>();
            List<Board> boards = BoardsToList(email);
            foreach (Board a in boards)
            {
                List<Task> temp = a.GetColumn(1);
                foreach (Task i in temp)
                {
                    if (TaskVerForList(email, i))
                        c.Add(i);
                }
            }
            return c;
        }
        /// <summary>
        /// Assign task to a new user
        /// </summary>
        /// <param name="userEmail">vThe user's email</param>
        /// <param name="creatorEmail">The creator's email</param>
        /// <param name="boardName">The board name</param>
        /// <param name="columnOrdinal">The current column ordinal</param>
        /// <param name="taskId">The task ID</param>
        /// <param name="emailAssignee">The email of the user to assignee</param>
        public void AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            c.BoardMemberVerify(emailAssignee);
            Task b = c.GetTask(taskId, columnOrdinal);
            c.TaskAssigneeVerify(userEmail, b);
            c.ChangeEmailAssignee(taskId, columnOrdinal, emailAssignee);
            TaskTable.Update(c.id, b.id, "Assignee", emailAssignee);
        }
        // Getter
        public List<String> GetBoardNames(string userEmail)
        {
            List<String> a = new List<String>();
            List<Board> b = BoardsToList(userEmail);
            foreach (Board i in b)
            {
                String k = i.name;
                a.Add(k);
            }
            return a;
        }
        // Getter
        public int GetColumnOrdinal(string columnOrdinal)
        {
            if (columnOrdinal == "backlog")
                return 0;
            else if (columnOrdinal == "in progress")
                return 1;
            else
                throw new Exception("could limit only columns 0 or 1");
        }
        // Private functions
        private Board FindBoard(string email, string boardName)
        {
            Board c;
            try
            {
                Tuple<string, string> t = new Tuple<string, string>(email, boardName);
                c = boardController[email][t];
                return c;
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Board does not exist");
            }
        }
        private List<Board> BoardsToList(string email)
        {
            return boardController[email].Values.ToList();
        }
        
        // TODO change the function name later =D ^.- 
        private bool TaskVerForList(string email, Task task)
        {
            if (email == task.GetAssignee())
                return true;
            else
                return false;
        }
        private void RemoveBoardFromAssigneeList(List<string> AssigneeList,string boardName,int boardId,string creatorName)
        {
            foreach (string user in AssigneeList)
            {
                Tuple<string, string> t = new Tuple<string, string>(creatorName, boardName);
                boardController[user].Remove(t);
                BoardTable.DeleteFromAssigneeList(boardId, user);
                
            }
            BoardTable.DeleteFromAssigneeList(boardId, creatorName);
        }
        private void LimitColumnForLoad (string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {

            Tuple<string, string> t = new Tuple<string, string>(creatorEmail, boardName);
            Board c = boardController[userEmail][t];

            c.LimitColumnForData(columnOrdinal, limit);
            
        }
        private void JoinBoardForLoad(string userEmail, string creatorEmail, string boardName)
        {
            Tuple<string, string> t = new Tuple<string, string>(creatorEmail, boardName);
            Board c = boardController[userEmail][t];
            c.AddtoBoardUsers(userEmail);
            boardController[userEmail].Add(t, c);
        }
        private void AddTaskForLoad(int taskId,string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            Tuple<string, string> t = new Tuple<string, string>(creatorEmail, boardName);
            Board c = boardController[userEmail][t];
            Task b = c.AddTaskForData(taskId,userEmail, dueDate, title, description);
        }
        private void MoveTaskForLoad(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            Tuple<string, string> t = new Tuple<string, string>(creatorEmail, boardName);
            Board c = boardController[userEmail][t];
            c.MoveTask(userEmail, taskId, columnOrdinal);
        }
        private void AssignTaskForLoad(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            Tuple<string, string> t = new Tuple<string, string>(creatorEmail, boardName);
            Board c = boardController[userEmail][t];
            Task b = c.GetTask(taskId, columnOrdinal); 
            c.ChangeEmailAssignee(taskId, columnOrdinal, emailAssignee);
        }
        public void AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            c.CheckOrdinalValidality(columnOrdinal);
            Column toinsert=c.AddColumn(columnOrdinal, columnName);
            ColumnDTO toadd = new ColumnDTO(c.id, toinsert.columnId, columnOrdinal, columnName, -1);
            ColumnTable.Insert(toadd);
            List<Column> ListOfColumns = c.GetBoardColumns();
            for (int i = columnOrdinal + 1; i < ListOfColumns.Count; i++)
                ColumnTable.Update(c.id, c.ColumnIdByColumnOrdinal(i), "ColumnOrdinal", i);
        }
        public void RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.CheckOrdinalValidality(columnOrdinal);
            c.BoardMemberVerify(userEmail);
            ColumnDTO toremove = ColumnTable.SpecificSelect(c.id, c.ColumnIdByColumnOrdinal(columnOrdinal));
            Column newhouse=c.RemoveColumn(columnOrdinal);
            ColumnTable.Delete(toremove);
            List<Column> ListOfColumns = c.GetBoardColumns();
            for (int i = columnOrdinal ; i < ListOfColumns.Count; i++)
                ColumnTable.Update(c.id, c.ColumnIdByColumnOrdinal(i), "ColumnOrdinal", i);
            foreach (TaskDTO t in TaskTable.Select())
            {
                if (t.BoardId == toremove.BoardId && t.ColumnId == toremove.ColumnId)
                {
                    TaskTable.Update(c.id, t.ID, "ColumnId", newhouse.columnId);
                    TaskTable.Update(c.id, t.ID, "ColumnName", newhouse.name);
                }
            }




        }
    }
}