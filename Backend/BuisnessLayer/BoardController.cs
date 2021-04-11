using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class BoardController
    {
        Dictionary<string, Dictionary<string, Board>> boardController ;
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
                    boardController[email].Add(name, new Board(boardIdCounter, name));
                    boardIdCounter = boardIdCounter + 1;
                }
                else
                    throw new Exception($"Board with the name {name} already exist");

        }
        public Board RemoveBoard(string email, string name)
        {
            Board c = FindBoard(email, name);
            boardController[email].Remove(name);
            return c;


        }

        public Task AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            Board c=FindBoard(email, boardName);     
            Task b = c.AddTask(dueDate, title, description);
            return b;
        }
        public void MoveTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            Board c = FindBoard(email, boardName);
            c.MoveTask(taskId, columnOrdinal);
        }
        public void ChangeDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Board c = FindBoard(email, boardName);
            c.ChangeDueDate(taskId, columnOrdinal, dueDate);
        }
        public void ChangeTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            Board c = FindBoard(email, boardName);
            c.ChangeTitle(taskId, columnOrdinal, title);
        }
        public void ChangeDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            Board c = FindBoard(email, boardName);
            c.ChangeTitle(taskId, columnOrdinal, description);
        }
        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            Board c = FindBoard(email, boardName);
            return c.GetColumnName(columnOrdinal);

        }

        public void LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            if (columnOrdinal == 2)
                throw new Exception("cannot limit done column");
            Board c = FindBoard(email, boardName);
            c.LimitColunm(columnOrdinal, limit);
        }
        public int GetcolumnLimit(string email, string boardName, int columnOrdinal)
        {
            Board c = FindBoard(email, boardName);
            return c.GetColumnLimit(columnOrdinal);
        }
        public List<Task> GetColunm(string email, string boardName, int columnOrdinal)
        {
            Board c = FindBoard(email, boardName);
            List<Task> b = c.GetColumn(columnOrdinal);
            return b;

        }
        public List<Task> InProgressTasks(string email)
        {
            List<Task> c = new List<Task>();
            List<Board> boards = BoardsToList(email);
            foreach (Board a in boards)
            {
                List<Task> temp = a.GetColumn(1);
                foreach (Task i in temp)
                {
                    c.Add(i);
                }
            }
            return c;
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
    }
}
