using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class ColumnModel : NotifiableModelObject
    {
        public ColumnModel(BackendController backendController, int id, string columnName, string creatorEmail, int limit, bool lastColumn, string userEmail) : base(backendController)
        {
            Id = id;
            ColumnName = columnName;
            CreatorEmail = creatorEmail;
            Limit = limit;
            LastColumn = lastColumn;
        }
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
        public bool LastColumn;
        private string columnName;
        public string ColumnName
        {
            get => columnName;
            set
            {
                this.columnName = value;
                RaisePropertyChanged("ColumnName");
            }
        }
        private int limit;
        public int Limit
        {
            get => limit;
            set
            {
                this.limit = value;
                RaisePropertyChanged("Limit");
            }
        }
        private string boardName;
        public string BoardName
        {
            get => boardName;
            set
            {
                this.boardName = value;
                RaisePropertyChanged("BoardName");
            }
        }
        private string creatorEmail;
        public string CreatorEmail
        {
            get => creatorEmail;
            set
            {
                this.creatorEmail = value;
                RaisePropertyChanged("CreatorEmail");
            }
        }
    }
}
