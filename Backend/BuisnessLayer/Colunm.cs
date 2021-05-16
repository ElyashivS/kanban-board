using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class Colunm
    {
        public string name;
        private Dictionary<int, Task> tasks;
        private bool columnLimitVerifier = false;
        private int columnLimiter=-1;




        public Colunm(string name, Dictionary<int, Task> Tasks)
        {
            this.name = name;
            this.tasks = Tasks;
        }
        public void LimitTasks(int maxtasks)
        {
            if (maxtasks <= 0)
            {
                throw new Exception("The maximum of tasks cant be 0 or below");
            }
            if (tasks.Count > maxtasks)
            {
                throw new Exception("Tasks are already exceeding the maxtasks input");
            }
            columnLimitVerifier = true;
            columnLimiter = maxtasks;
        }
        public int GetColumnLimit()
        {
            if (columnLimitVerifier)
                return this.columnLimiter;
            else              
                throw new Exception("The column isnt limited to a number of Tasks");
        }
        public Task AddTask(int id, DateTime dueDate, string email, string title, string description)
        {
            if (!columnLimitVerifier || (columnLimitVerifier && Tasks.Count() < columnLimiter))
            {
                Task c = new Task(id, dueDate, email, title, description);
                tasks.Add(id, c);
                return c;
            }
            else
                throw new Exception("Column has reached to its maximum tasks");
        }
        public Task RemoveTask(int id)
        {

            if (tasks.ContainsKey(id))
            {

                Task c = tasks[id];
                tasks.Remove(id);
                return c;
            }
            else
                throw new Exception("Task is not found");
        }
        public void ChangeDueDate(int id, DateTime newDuedate)
        {
            if (tasks.ContainsKey(id))
                tasks[id].ChangeDueDate(newDuedate);
            else
                throw new Exception("Task is not be found");
        }
        public void ChangeTitle(int id, string newTitle)
        {
            if (tasks.ContainsKey(id))
                tasks[id].ChangeTitle(newTitle);
            else
                throw new Exception("Task could not be found");
        }
        public void ChangeDescription(int id, string newDescription)
        {
            if (tasks.ContainsKey(id))
                tasks[id].ChangeDescription(newDescription);
            else
                throw new Exception("Task could not not found");
        }
        public string GetColumnName()
        {
            return this.name;
        }
        public void ChangeEmailAssignee(int taskId, string newEmail)
        {
            if (!tasks.ContainsKey(taskId))
                throw new Exception("task was not found");
            tasks[taskId].ChangeEmailAssignee(newEmail);
        }
        public Task GetTask(int id)
        {
            if (!tasks.ContainsKey(id))
            {
                throw new Exception("task not found");
            }
            return tasks[id];
        }
        public List<Task> Tasks
        {
            get
            {
                return tasks.Values.ToList();
            }

        }
        //returns if the column is limi
        public bool GetColumnIfLimited()
        {
            return columnLimitVerifier;
        }
    }
}