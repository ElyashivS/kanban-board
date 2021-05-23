using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class Task
    {
        public int id;
        public string emailAssignee;
        public DateTime creationTime;
        private DateTime duedate;
        private string title;
        private string description;
        private const int TitleLength = 50;
        private const int DescriptionLength = 300;

        // Constructor
        public Task(int id, DateTime dueDate, string assignee, string title, string description)
        {
            this.id = id;
            this.creationTime = DateTime.Now;
            this.emailAssignee = assignee;
            CheckDuedate(dueDate);
            this.duedate = dueDate;
            CheckTitle(title);
            this.title = title;
            CheckDescription(description);
            this.description = description;

        }
        // Setters
        public void ChangeDueDate(DateTime NewDueDate)
        {
            CheckDuedate(NewDueDate);
            this.duedate = NewDueDate;
        }
        public void ChangeTitle(string NewTitle)
        {
            CheckTitle(NewTitle);
            this.title = NewTitle;
        }
        public void ChangeDescription(string NewDescription)
        {
            CheckDescription(NewDescription);
            this.description = NewDescription;
        }
        public void ChangeEmailAssignee(string newemail)
        {
            this.emailAssignee = newemail;
        }
        // Getters
        public DateTime GetDueDate()
        {
            return this.duedate;
        }
        public string GetDescription()
        {
            return this.description;
        }
        public string GetTitle()
        {
            return this.title;
        }
        public int GetId()
        {
            return this.id;
        }
        public DateTime GetCreationTime()
        {
            return this.creationTime;
        }
        public string GetAssignee()
        {
            return this.emailAssignee;
        }
        /// <summary>
        /// Checks if the date is valid
        /// </summary>
        /// <param name="dueDate">The new date</param>
        private void CheckDuedate(DateTime dueDate)
        {
            if (dueDate < creationTime)
                throw new Exception("cannot input a Duedate that has passed");
        }
        /// <summary>
        /// Checks if the description is valid
        /// </summary>
        /// <param name="description">The description</param>
        private void CheckDescription(string description)
        {
            if (description.Length > DescriptionLength)
            {
                throw new Exception("is over 300 characters");
            }
        }
        /// <summary>
        /// Check if the title is valid
        /// </summary>
        /// <param name="title">The title</param>
        private void CheckTitle(string title)
        {
            if (title == null)
                throw new NullReferenceException();
            if (title.Length > TitleLength || title.Length < 1)
            {
                throw new Exception("Title is empty or over 50 characters");
            }
        }
    }
}
