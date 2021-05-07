using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTO
{
    class ColumnDTO : DTO
    {
        private string _columnLimiter;
        private string _name;

        public const string NameColumnName = "Name";
        public const string ColumnLimiterColumnName = "ColumnLimiter";

        public ColumnDTO(string name, string columnLimiter) : base(new UserDalController())
        {
            _name = name;
            _columnLimiter = columnLimiter;
        }
        public string Name { get => _name; set { _name = value; _controller.Update(NameColumnName, value); } }
        public string ColumnLimiter { get => _columnLimiter; set { _columnLimiter = value; _controller.Update(NameColumnName, value); } }
    }
}
