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
        public Task AddTask(string email, DateTime duedate, string title, string descripton)
        {

            Task c = board[0].AddTask(idcounter, duedate, email, title, descripton);
            idcounter = idcounter + 1;
            return c;
        }
        public Task RemoveTask(int id, int columnOrdinal)
        {
            Task c = board[columnOrdinal].RemoveTask(id);
            return c;
        }
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
        public void ChangeEmailAssignee(int id, int columnOrdinal, string newEmail)
        {
            if (columnOrdinal <= 2 || columnOrdinal >= 0)
            {
                board[columnOrdinal].ChangeEmailAssignee(id, newEmail);
            }
            else
                throw new Exception("columnOrdinal should be between 0 and 2");
        }
        public string GetColumnName(int columnOrdinal)
        {
            if (columnOrdinal != 0 && columnOrdinal != 1 && columnOrdinal != 2)
            {
                throw new Exception("column number can be 0,1 or 2");
            }
            return board[columnOrdinal].GetColumnName();
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

            return board[columnOrdinal].GetColumnLimit();
        }

        public List<Task> GetColumn(int columnOrdinal)
        {
            if (columnOrdinal != 0 && columnOrdinal != 1 && columnOrdinal != 2)
                throw new Exception("column number can be 0,1 or 2");

            return board[columnOrdinal].Tasks;
        }
        private int GetBoardId()
        {
            return this.id;
        }
        public string GetCreator()
        {
            return this.creator;
        }
        public void BoardMemberVerify(string email)
        {
            if (!(creator == email))
            {
                if (!(users.Contains(email)))
                    throw new Exception("user is not a board member");


            }

        }
        public void TaskAssigneeVerify(string email, Task task)
        {
            if (!(email == task.GetAssignee()))
                throw new Exception($"user with the email {email} is not assign for the task");
        }
        public void AddtoBoardUsers(string email)
        {
            if (users.Contains(email)||email==this.creator)
                throw new Exception("user is already a member in the board");
            users.Add(email);
        }
        public Task GetTask(int id, int columnOrdinal)
        {
            if (columnOrdinal <= 2 || columnOrdinal >= 0)
                return board[columnOrdinal].GetTask(id);
            else
                throw new Exception("Column doesnt exist");
        }
       
    }
   
}