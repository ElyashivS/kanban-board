using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTO
{
    class ColumnDTO : DTO
    {
        private int _boardId;
        private string _name;
        private int _columnLimiter;

        public const string BoardIdColumnName = "BoardId";
        public const string ColumnNameColumnName = "ColumnName";
        public const string ColumnLimiterColumnName = "ColumnLimiter";

        public ColumnDTO(int boardId,string name, int columnLimiter ) : base(new ColumnDalController())
        {
            _boardId = boardId;
            _name = name;
            _columnLimiter = columnLimiter;
        }
       
        public string Name { get => _name; set { _name = value; } } 
        public int ColumnLimiter { get => _columnLimiter; set { _columnLimiter = value; } } 
        public int BoardId { get => _boardId; set { _boardId = value; } }
    }
}
