﻿using System;
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

        public const string IdColumnName = "Id";
        public const string EmailAssigneeColumnName = "Assignee";
        public const string CreationTimeColumnName = "CreationTime";
        public const string DueDateColumnName = "DueDate";
        public const string TitleColumnName = "Title";
        public const string DescriptionColumnName = "Description";

        public TaskDTO (int id, string emailAssignee, DateTime creationTime, DateTime dueDate, string title, string description) : base(new UserDalController())
        {
            _id = id;
            _emailAssignee = emailAssignee;
            _creationTime = creationTime;
            _dueDate = dueDate;
            _title = title;
            _description = description;
        }
        public int ID { get => _id; set { _id = value; } }
        public string Assignee { get => _emailAssignee; set { _emailAssignee = value; } }
        public DateTime CreationTime { get => _creationTime; set { _creationTime = value; } }
        public DateTime DueDate { get => _dueDate; set { _dueDate = value; } }
        public string Title { get => _title; set { _title = value; } }
        public string Description { get => _description; set { _description = value; } }

    }
};