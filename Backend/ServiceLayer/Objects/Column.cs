using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer.Objects
{
    class Column
    {
        public readonly string ColumnId;
        public readonly string ColumnName;
        public readonly int ColumnOrdinal;
        public readonly int ColumnLimit;
        
        internal Column(string ColumnId,string ColumnName,int ColumnOrdianl,int ColumnLimit)
        {
            this.ColumnId = ColumnId;
            this.ColumnName = ColumnName;
            this.ColumnOrdinal = ColumnOrdianl;
            this.ColumnLimit = ColumnLimit;

        }
    }
}
