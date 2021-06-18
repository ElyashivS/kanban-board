using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class TaskModel : NotifiableModelObject
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                this.id = value;
                RaisePropertyChanged("Id");
            }
        }
        private string emailAssignee;
        public string EmailAssignee
        {
            get => emailAssignee;
            set
            {
                this.emailAssignee = value;
                RaisePropertyChanged("EmailAssignee");
            }
        }
        private DateTime creationTime;
        public DateTime CreationTime
        {
            get => creationTime;
            set
            {
                this.creationTime = value;
                RaisePropertyChanged("CreationTime");
            }
        }
        private DateTime duedate;
        public DateTime Duedate
        {
            get => duedate;
            set
            {
                this.duedate = value;
                RaisePropertyChanged("Duedate");
            }
        }
        private string title;
        public string Title
        {
            get => title;
            set
            {
                this.title = value;
                RaisePropertyChanged("Title");
            }
        }
        private string description;
        public string Description
        {
            get => description;
            set
            {
                this.description = value;
                RaisePropertyChanged("Description");
            }
        }
        public TaskModel(BackendController backendController, int id, DateTime creationTime, string title, string description, DateTime dueDate, string emailAssignee) : base(backendController)
        {
            Id = id;
            CreationTime = creationTime;
            Title = title;
            Description = description;
            Duedate = dueDate;
            EmailAssignee = emailAssignee;
        }
    }
}
