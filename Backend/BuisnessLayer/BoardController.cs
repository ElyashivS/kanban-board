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
                
                

                boardController[b.Creator].Add(new Tuple<string,string>(b.Creator,b.Name), new Board(b.ID, b.Name, b.Creator));
                
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
                        if (t.ID == b.ID)
                        {
                        if (t.ColumnName.Equals("backlog"))
                        {
                            AddTaskForLoad(b.Creator, b.Creator, b.Name, t.Title, t.Description, t.DueDate);
                            
                        }
                        if(t.ColumnName.Equals("in progress")){
                            AddTaskForLoad(b.Creator, b.Creator, b.Name, t.Title, t.Description, t.DueDate);
                            MoveTaskForLoad(b.Creator, b.Creator, b.Name, 0, t.ID);
                            
                        }
                        if (t.ColumnName.Equals("done"))
                        {
                            AddTaskForLoad(b.Creator, b.Creator, b.Name, t.Title, t.Description, t.DueDate);
                            MoveTaskForLoad(b.Creator, b.Creator, b.Name, 0, t.ID);
                            MoveTaskForLoad(b.Creator, b.Creator, b.Name, 1, t.ID);
                            
                        }
                        }
                    AssignTaskForLoad(b.Creator, b.Creator, b.Name, GetColumnOrdinal(t.ColumnName), t.ID, t.Assignee);
                }

                }
            



        }
        public void DeleteData()
        {
            BoardTable.DeleteBoardTable();
            BoardTable.DeleteAssigneeTable();
            ColumnTable.DeleteColumnTable();
            TaskTable.DeleteTaskTable();
        }



        public BoardController()
        {
            this.boardController = new Dictionary<string, Dictionary<Tuple<string,string>, Board>>();

        }
        public void Register(string email)
        {
            boardController.Add(email, new Dictionary<Tuple<string,string>,Board>());
        }
        public Board AddBoard(string email, string name)
        {
            Tuple<string, string> a = new Tuple<string, string>(email, name);
            if (!boardController[email].ContainsKey(a))
            {
                Board b = new Board(boardIdCounter, name, email);
                boardController[email].Add(a, b);
                b.AddtoBoardUsers(email);

                BoardTable.Insert(new BoardDTO(boardIdCounter, email, name));
                BoardTable.InsertToAsigneeList(b.id, email);
                boardIdCounter = boardIdCounter + 1;
                return b;
            }
            else
                throw new Exception($"Board with the name {name} already exist");

        }
        public Board RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {
            Board c = FindBoard(creatorEmail, boardName);
            if (!userEmail.Equals(c.GetCreator()))
                throw new Exception("only the creator of the board may delete the board");
            RemoveBoardFromAssigneeList(c.GetAssigneeList(), boardName, c.id,c.GetCreator());
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
            Tuple<string, string> t = new Tuple<string, string>(creatorEmail, boardName);
            boardController[creatorEmail].Remove(t);
            
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
            boardController[userEmail].Add(new Tuple<string, string>(creatorEmail,boardName), c);
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
        }
        public int GetColumnOrdinal(string columnOrdinal)
        {
            if (columnOrdinal == "backlog")
                return 0;
            else if (columnOrdinal == "in progress")
                return 1;
            else
                throw new Exception("could limit only columns 0 or 1");
        }
        private void LimitColumnForLoad (string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            if (columnOrdinal == 2)
                throw new Exception("cannot limit done column");
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);  
            c.LimitColunm(columnOrdinal, limit);
            
        }
        private void JoinBoardForLoad(string userEmail, string creatorEmail, string boardName)
        {
            Tuple<string, string> t = new Tuple<string, string>(creatorEmail, boardName);
            Board c = FindBoard(creatorEmail, boardName);
            c.AddtoBoardUsers(userEmail);
            boardController[userEmail].Add(t, c);
        }
        private void AddTaskForLoad(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            Task b = c.AddTask(userEmail, dueDate, title, description);
        }
        private void MoveTaskForLoad(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {

            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            c.TaskAssigneeVerify(userEmail, c.GetTask(taskId, columnOrdinal));
            c.MoveTask(userEmail, taskId, columnOrdinal);
        }
        private void AssignTaskForLoad(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            Board c = FindBoard(creatorEmail, boardName);
            c.BoardMemberVerify(userEmail);
            c.BoardMemberVerify(emailAssignee);
            Task b = c.GetTask(taskId, columnOrdinal);
            c.TaskAssigneeVerify(userEmail, b);
            c.ChangeEmailAssignee(taskId, columnOrdinal, emailAssignee);
        }
    }
}