using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTO
{
    class AssigneeDTO:DTO
    {
        private int _id;
        private string _assignee;
        
        public const string IDColumnName = "BoardId";
        public const string AssigneeColumnName = "EmailAssignee";
        
        // Constructor
        public AssigneeDTO(int id, string assignee) : base(new BoardDalController())
        {
            _id = id;
            _assignee = assignee;
        }

        // Getters and setters
        public int ID { get => _id; set { _id = value; } }
        public string Assignee { get => _assignee; set { _assignee = value; } }
        
    }
}

