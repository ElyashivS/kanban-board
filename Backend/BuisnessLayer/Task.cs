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
        public DateTime creationTime;
        private DateTime duedate;
        private string title;
        private string description;
        private const int TitleLength = 50;
        private const int DescriptionLength = 300;

        public Task(int id, DateTime dueDate, string title, string description)
        {
            this.id = id;
            this.creationTime = DateTime.Now;
            
            CheckDuedate(dueDate);
            this.duedate = dueDate;
            CheckTitle(title);
            this.title = title;
            CheckDescription(description);
            this.description = description;

        }
        
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

        //
        private void CheckDuedate(DateTime dueDate)
        {
            if (dueDate < creationTime)
                throw new Exception("cannot input a Duedate that has passed");

            }

        private void CheckDescription(string description)
        {

                if (description == null)
                    throw new NullReferenceException();
                if (description.Length > DescriptionLength || description.Length < 1)
                {
                    throw new Exception("Description is empty or over 300 characters");
                } 
        }
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
