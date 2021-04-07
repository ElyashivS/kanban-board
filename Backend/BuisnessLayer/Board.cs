using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class Board
    {
        public int id { get; }
        public string name;
        private List<Colunm> board;
        private int idcounter = 1;


        public Board(int id, string name)
        {
            this.id = id;
            this.name = name;
            board = new List<Colunm>();
            board.Add(new Colunm("backlog", new Dictionary<int, Task>()));
            board.Add(new Colunm("inprogress", new Dictionary<int, Task>()));
            board.Add(new Colunm("done", new Dictionary<int, Task>()));


        }
        public Task AddTask(DateTime duedate, string title, string descripton)
        {

            Task c = board[0].AddTask(idcounter, duedate, title, descripton);
            idcounter = idcounter + 1;
            return c;
        }
        public Task RemoveTask(int id, int columnOrdinal)
        {
            Task c = board[columnOrdinal].RemoveTask(id);
            return c;
        }
        public void MoveTask(int id, int columnOrdinal)
        {
            if (columnOrdinal == 0)
            {
                Task toprogress = board[0].RemoveTask(id);
                board[1].AddTask(id, toprogress.DueDate, toprogress.Title, toprogress.Description);

            }
            else if (columnOrdinal == 1)
            {
                Task todone = board[1].RemoveTask(id);
                board[2].AddTask(id, todone.DueDate, todone.Title, todone.Description);

            }
            else
                throw new Exception("columnOrdinal can be only 0 or 1");
        }

        public void ChangeDueDate(int id, int columnOrdinal, DateTime newdueDate)
        {
            if (columnOrdinal == 0 || columnOrdinal == 1)
                board[columnOrdinal].ChangeDueDate(id, newdueDate);

            else
                throw new Exception("you can change Duedate only from backlog column or inprogress column");


        }
        public void ChangeTitle(int id, int columnOrdinal, string title)
        {
            if (columnOrdinal == 0 || columnOrdinal == 1 || columnOrdinal == 2)
                board[columnOrdinal].ChangeTitle(id, title);
            else
                throw new Exception("column not found");



        }
        public void ChangeDescription(int id, int columnOrdinal, string description)
        {
            if (columnOrdinal == 0 || columnOrdinal == 1 || columnOrdinal == 2)
                board[columnOrdinal].ChangeDescription(id, description);
            else
                throw new Exception("column not found");
        }
        public string GetColumnName(int columnOrdinal)
        {
            if (columnOrdinal != 0 && columnOrdinal != 1 && columnOrdinal != 2)
            {
                throw new Exception("column number can be 0,1 or 2");
            }
            return board[columnOrdinal].name;
        }
        public void LimitColunm(int columnOrdinal, int limit)
        {
            if (columnOrdinal != 0 && columnOrdinal != 1 && columnOrdinal != 2)
                throw new Exception("column number can be 0,1 or 2");

            board[columnOrdinal].LimitTasks(limit);
        }
        public int GetColumnLimit(int columnOrdinal)
        {
            if (columnOrdinal != 0 && columnOrdinal != 1 && columnOrdinal != 2)
                throw new Exception("column number can be 0,1 or 2");

            return board[columnOrdinal].MaxTaskCheck();
        }

        public List<Task> GetColumn(int columnOrdinal)
        {
            if (columnOrdinal != 0 && columnOrdinal != 1 && columnOrdinal != 2)
                throw new Exception("column number can be 0,1 or 2");

            return board[columnOrdinal].Tasks;
        }
    }
}
