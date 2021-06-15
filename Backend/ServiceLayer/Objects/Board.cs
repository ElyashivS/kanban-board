using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer.Objects
{
    public class Board
    {
        public readonly int BoardId;
        public readonly string Creator;
        public readonly string BoardName;
        public IReadOnlyCollection<int> listBoards;

        internal Board(int BoardId,string Creator,string BoardName)
        {
            this.BoardId = BoardId;
            this.Creator = Creator;
            this.BoardName = BoardName;
        }
    }
}
