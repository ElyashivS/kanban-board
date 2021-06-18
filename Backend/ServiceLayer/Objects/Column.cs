using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer.Objects
{
    public class Column
    {
        public readonly int ColumnId;
        public readonly string ColumnName;
        public readonly int ColumnOrdinal;
        public readonly int ColumnLimit;
        private List<Column> columnList;
        public List<Column> ColumnList { get => columnList; }

        internal Column(int ColumnId,string ColumnName,int ColumnOrdianl,int ColumnLimit)
        {
            this.ColumnId = ColumnId;
            this.ColumnName = ColumnName;
            this.ColumnOrdinal = ColumnOrdianl;
            this.ColumnLimit = ColumnLimit;

        }
    }
}
