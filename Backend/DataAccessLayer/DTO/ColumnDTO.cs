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
        private int _columnId;
        private string _name;
        private int _columnLimiter;
        private int _columnOrdinal;

        public const string BoardIdColumnName = "BoardId";
        public const string ColumnIdColumnName = "ColumnId";
        public const string ColumnNameColumnName = "ColumnName";
        public const string ColumnLimiterColumnName = "ColumnLimiter";
        public const string ColumnOrdinalColumnName = "ColumnOrdinal";

        // Constructor
        public ColumnDTO(int boardId,int columnId,int columnOrdinal,string name, int columnLimiter ) : base(new ColumnDalController())
        {
            _boardId = boardId;
            _name = name;
            _columnLimiter = columnLimiter;
            _columnId = columnId;
            _columnOrdinal = columnOrdinal;
        }
       
        // Getters and setters
        public string Name { get => _name; set { _name = value; } } 
        public int ColumnLimiter { get => _columnLimiter; set { _columnLimiter = value; } } 
        public int BoardId { get => _boardId; set { _boardId = value; } }
        public int ColumnId { get => _columnId; set { _columnId = value; } }
        public int ColumnOrdinal { get => _columnOrdinal; set { _columnOrdinal = value; } }
    }
}
