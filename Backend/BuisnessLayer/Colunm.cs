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
        // Constructor
        public Colunm(string name, Dictionary<int, Task> Tasks)
        {
            this.name = name;
            this.tasks = Tasks;
        }
        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="maxtasks">The new limit</param>
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
        public void LimitTasksForData(int maxtasks)
        {
            columnLimitVerifier = true;
            columnLimiter = maxtasks;
        }
        // Getter
        public int GetColumnLimit()
        {
            if (columnLimitVerifier)
                return this.columnLimiter;
            else              
                throw new Exception("The column isnt limited to a number of Tasks");
        }
        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="id">The ID of the new task.</param>
        /// <param name="dueDate">The due date if the new task.</param>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="title">Title of the new task.</param>
        /// <param name="description">Description of the new task.</param>
        /// <returns></returns>
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
        public Task AddTaskForData(int id, DateTime dueDate, string email, string title, string description)
        {
            Task c = new Task(id, dueDate, email, title, description);
            tasks.Add(id, c);
            return c;
        }
        /// <summary>
        /// Remove the task.
        /// </summary>
        /// <param name="id">The id of the task.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Change the due date of a task
        /// </summary>
        /// <param name="id">The task's ID</param>
        /// <param name="newDuedate">The new due date</param>
        public void ChangeDueDate(int id, DateTime newDuedate)
        {
            if (tasks.ContainsKey(id))
                tasks[id].ChangeDueDate(newDuedate);
            else
                throw new Exception("Task is not be found");
        }
        /// <summary>
        /// Change the title of a task
        /// </summary>
        /// <param name="id">The task's ID</param>
        /// <param name="newTitle">The new title</param>
        public void ChangeTitle(int id, string newTitle)
        {
            if (tasks.ContainsKey(id))
                tasks[id].ChangeTitle(newTitle);
            else
                throw new Exception("Task could not be found");
        }
        /// <summary>
        /// Change the description of a task
        /// </summary>
        /// <param name="id">The task's ID</param>
        /// <param name="newDescription">The new description</param>
        public void ChangeDescription(int id, string newDescription)
        {
            if (tasks.ContainsKey(id))
                tasks[id].ChangeDescription(newDescription);
            else
                throw new Exception("Task could not not found");
        }
        // Getter
        public string GetColumnName()
        {
            return this.name;
        }
        /// <summary>
        /// Change the mail of assignee
        /// </summary>
        /// <param name="taskId">The task's ID</param>
        /// <param name="newEmail">The new email</param>
        public void ChangeEmailAssignee(int taskId, string newEmail)
        {
            if (!tasks.ContainsKey(taskId))
                throw new Exception("task was not found");
            tasks[taskId].ChangeEmailAssignee(newEmail);
        }
        public void ChangeEmailAssigneeForData(int taskId, string newEmail)
        {
            tasks[taskId].ChangeEmailAssignee(newEmail);
        }
        // Getters
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
        public bool GetColumnIfLimited()
        {
            return columnLimitVerifier;
        }
    }
}