using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTO
{
    class BoardDTO : DTO
    {
        private int _id;
        private string _name;
        private string _creator;

        public const string IDColumnName = "Id";
        public const string BoardNameColumnName = "BoardName";
        public const string CreatorColumnName = "Creator";
        public const string AssigneeColumnName = "EmailAssignee";

        // Constructor
        public BoardDTO(int id,string name ,string creator  ) : base(new BoardDalController())
        {
            _id = id;
            _name = name;
            _creator = creator;
        }

        // Getters and setters
        public int ID { get => _id; set { _id = value; } }
        public string Name { get => _name; set { _name = value; } }
        public string Creator { get => _creator; set { _creator = value; } }
    }
}
