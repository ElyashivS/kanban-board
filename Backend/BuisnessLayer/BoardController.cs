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
        private Dictionary<string, Dictionary<string, Board>> boardController;
        private BoardDalController BoardTable = new BoardDalController();
        private ColumnDalController ColumnTable = new ColumnDalController();
        private TaskDalController TaskTable = new TaskDalController();

        int boardIdCounter = 1;


        public BoardController()
        {
            this.boardController = new Dictionary<string, Dictionary<string, Board>>();

        }
        public void Register(string email)
        {
            boardController.Add(email, new Dictionary<string, Board>());
        }
        public void AddBoard(string email, string name)
        {

            if (!boardController[email].ContainsKey(name))
            {
                boardController[email].Add(name, new Board(boardIdCounter, name, email));
                BoardTable.Insert(new BoardDTO(boardIdCounter, email, name));
                boardIdCounter = boardIdCounter + 1;

            }
            else
                throw new Exception($"Board with the name {name} already exist");

        }
        public Board RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {
            Board c = FindBoard(creatorEmail, boardName);
            if (userEmail != c.GetCreator())
                throw new Exception("only the creator of the board may delete the board");
            RemoveBoardFromAssigneeList(c.GetAssigneeList(), boardName, c.id);
            List<Colunm> todeleteColumns = c.GetBoardColumns();
            for (int i = 0; i < todeleteColumns.Count; i++)
            {

                if(c.GetColumnIfLimited(i))
                {

                    {
                        ColumnDTO deleting = new ColumnDTO(c.id, c.GetColumnName(i), c.GetColumnLimit(i));
                        ColumnTable.Delete(deleting);
                    }
                }
            }
            boardController[creatorEmail].Remove(boardName);
            
            List<Colunm> toremove = c.GetBoardColumns();
            foreach (Colunm i in toremove)
            {
                foreach (Task a in i.Tasks)
                {
                    TaskDTO todelete = TaskTable.SpecificSelect(c.id, a.id);
                    TaskTable.Delete(todelete);

                }

            }
            BoardTable.Delete(BoardTable.SpecificSelect(c.id));

            return c;


        }

        public Task AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            Task b = c.AddTask(userEmail, dueDate, title, description);
            TaskDTO toadd = new TaskDTO(c.id, b.id, "backlog", userEmail, b.GetCreationTime(), b.GetDueDate(), b.GetTitle(), b.GetDescription());
            TaskTable.Insert(toadd);
            return b;
        }
        public void MoveTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            c.TaskAssigneeVerify(userEmail, c.GetTask(taskId, columnOrdinal));
            c.MoveTask(userEmail, taskId, columnOrdinal);
            if (columnOrdinal == 0)
                TaskTable.Update(c.id, taskId, "columnName", "in progress");
            if (columnOrdinal == 1)
                TaskTable.Update(c.id, taskId, "columnName", "done");
        }
        public void ChangeDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            c.TaskAssigneeVerify(userEmail, c.GetTask(taskId, columnOrdinal));
            c.ChangeDueDate(taskId, columnOrdinal, dueDate);
            TaskTable.Update(c.id, taskId, "DueDate", dueDate);
        }
        public void ChangeTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            c.TaskAssigneeVerify(userEmail, c.GetTask(taskId, columnOrdinal));
            c.ChangeTitle(taskId, columnOrdinal, title);
            TaskTable.Update(c.id, taskId, "Title", title);
        }
        public void ChangeDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            c.TaskAssigneeVerify(userEmail, c.GetTask(taskId, columnOrdinal));
            c.ChangeDescription(taskId, columnOrdinal, description);
            TaskTable.Update(c.id, taskId, "Description", description);
        }
        public string GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            return c.GetColumnName(columnOrdinal);

        }

        public void LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            if (columnOrdinal == 2)
                throw new Exception("cannot limit done column");
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            bool iflimited = c.GetColumnIfLimited(columnOrdinal);
            c.LimitColunm(columnOrdinal, limit);
            string fordata = c.GetColumnName(columnOrdinal);
            if (!iflimited)
            {
                ColumnDTO toadd = new ColumnDTO(c.id, c.GetColumnName(columnOrdinal), limit);
                ColumnTable.Insert(toadd);
            }
            if (iflimited)
            {
                ColumnTable.Update(c.id, c.GetColumnName(columnOrdinal), "ColumnLimiter", limit);
            }

            


        }
        public int GetcolumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            return c.GetColumnLimit(columnOrdinal);
        }
        public List<Task> GetColunm(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            List<Task> b = c.GetColumn(columnOrdinal);
            return b;

        }
        public void JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.AddtoBoardUsers(userEmail);
            boardController[userEmail].Add(boardName, c);
            BoardTable.InsertToAsigneeList(c.id, userEmail);
        }
        //brings a list of Tasks that the user is Assignee for and in -"in progress column"
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


        private Board FindBoard(string email, string boardName)
        {
            Board c;
            try
            {
                c = boardController[email][boardName];
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
        private void RemoveBoardFromAssigneeList(List<string> AssigneeList,string boardName,int boardId)
        {
            foreach (string user in AssigneeList)
            {
                boardController[user].Remove(boardName);
                BoardTable.DeleteFromAssigneeList(boardId, user);
            }
        }

    }
}