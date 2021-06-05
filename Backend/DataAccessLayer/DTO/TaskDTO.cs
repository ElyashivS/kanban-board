using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTO
{
    internal class TaskDTO : DTO
    {
        private int _id;
        private string _emailAssignee;
        private DateTime _creationTime;
        private DateTime _dueDate;
        private string _title;
        private string _description;
        private string _columnName;
        private int _boardId;
        private int _columnId;

        public const string BoardIdColumnName = "BoardId";
        public const string ColumnIdColumnName = "ColumnId";
        public const string IdColumnName = "Id";
        public const string EmailAssigneeColumnName = "Assignee";
        public const string CreationTimeColumnName = "CreationTime";
        public const string DueDateColumnName = "DueDate";
        public const string TitleColumnName = "Title";
        public const string DescriptionColumnName = "Description";
        public const string ColumnNameColumnName = "ColumnName";
        

        // Constructor
        public TaskDTO (int boardid,int columnid,int id, string columnName, string emailAssignee, DateTime creationTime, DateTime dueDate, string title, string description) : base(new TaskDalController())
        {
            _id = id;
            _emailAssignee = emailAssignee;
            _creationTime = creationTime;
            _dueDate = dueDate;
            _title = title;
            _description = description;
            _columnName = columnName;
            _boardId = boardid;
            _columnId = columnid;

        }

        // Getters and setters
        public int ID { get => _id; set { _id = value; } }
        public string Assignee { get => _emailAssignee; set { _emailAssignee = value; } }
        public DateTime CreationTime { get => _creationTime; set { _creationTime = value; } }
        public DateTime DueDate { get => _dueDate; set { _dueDate = value; } }
        public string Title { get => _title; set { _title = value; } }
        public string Description { get => _description; set { _description = value; } }
        public string ColumnName { get => _columnName; set { _columnName = value; } }
        public int BoardId { get => _boardId; set { _boardId = value; } }
        public int ColumnId { get => _columnId; set { _columnId = value; } }
    }
};