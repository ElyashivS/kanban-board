using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BuisnessLayer
{
    public class Task
    {
        public int id { get; }
        public DateTime creationTime { get; }
        private DateTime duedate;
        private string title;
        private string description;
        private const int TitleLength = 50;
        private const int DescriptionLength = 300;

        public Task(int id, DateTime dueDate, string title, string description)
        {
            this.id = id;
            this.creationTime = DateTime.Now;
            DueDate = dueDate;
            Title = title;
            Description = description;

        }
        
        public void ChangeDueDate(DateTime NewDueDate)
        {
            this.duedate = NewDueDate;
        }
        public void ChangeTitle(string NewTitle)
        {
            if (NewTitle.Length > TitleLength || NewTitle.Length < 1)
            {
                throw new Exception("Title is empty or over 50 characters");
            }
            this.title = NewTitle;
        }
        public void ChangeDescription(string NewDescription)
        {
            if (NewDescription.Length > DescriptionLength || NewDescription.Length < 1)
            {
                throw new Exception("Description is empty or over 300 characters");
            }
            this.Description = NewDescription;
        }
        

        //
        public DateTime DueDate
        {
            get { return duedate; }
            set
            {
               
                 duedate=value;
            }

            }

        public string Description
        {
            get { return description; }
            set
            {
                if (value == null)
                    throw new NullReferenceException();
                if (value.Length > DescriptionLength || value.Length < 1)
                {
                    throw new Exception("Description is empty or over 300 characters");
                }
                this.description = Description;
            }
        }
        public string Title
        {
            get { return title; }
            set
            {
                if (value == null)
                    throw new NullReferenceException();
                if (value.Length > TitleLength || value.Length < 1)
                {
                    throw new Exception("Title is empty or over 50 characters");
                }
                this.title = Title;
            }

        }
    }

}
