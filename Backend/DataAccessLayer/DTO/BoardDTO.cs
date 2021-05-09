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

        public const string IDColumnName = "ID";
        public const string BoardNameColumnName = "Name";
        public const string CreatorColumnName = "Creator";

        public BoardDTO(int id, string name, string creator) : base(new UserDalController())
        {
            _id = id;
            _name = name;
            _creator = creator;
        }

        //public int ID { get => _id; set { _id = value; _controller.Update(IDColumnName, value); } }
        //public string Name { get => _name; set { _name = value; _controller.Update(BoardNameColumnName, value); } }
        //public string Creator { get => _creator; set { _creator = value; _controller.Update(CreatorColumnName, value); } }
    }
}
