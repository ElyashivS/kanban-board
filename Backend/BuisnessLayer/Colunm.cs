using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class Colunm
    {
        public string name { get; }
        private Dictionary<int, Task> tasks;
        private bool taskMaxVerifier = false;
        private int taskMax;




        public Colunm(string name, Dictionary<int, Task> Tasks)
        {
            this.name = name;
            this.tasks = Tasks;
        }
        public void LimitTasks(int maxtasks)
        {
            if (maxtasks == 0)
            {
                throw new Exception("The maximum of tasks cant be 0");
            }
            taskMaxVerifier = true;
            taskMax = maxtasks;
        }
        public int MaxTaskCheck()
        {
            if (taskMaxVerifier)
                return taskMax;
            else
                throw new Exception("The column isnt limited to a number of Tasks");
        }
        public Task AddTask(int id, DateTime dueDate, string title, string description)
        {
            if (!taskMaxVerifier || (taskMaxVerifier && Tasks.Count() < taskMax))
            {
                Task c = new Task(id, dueDate, title, description);
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
                tasks[id].DueDate = newDuedate;
            else
                throw new Exception("Task is not be found");
        }
        public void ChangeTitle(int id, string newTitle)
        {
            if (tasks.ContainsKey(id))
                tasks[id].Title = newTitle;
            else
                throw new Exception("Task could not be found");
        }
        public void ChangeDescription(int id, string newDescription)
        {
            if (tasks.ContainsKey(id))
                tasks[id].Description = newDescription;
            else
                throw new Exception("Task could not not found");
        }

        public List<Task> Tasks
        {
            get
            {
                return tasks.Values.ToList();
            }
        }
    }
}
