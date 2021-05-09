using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class BoardDalController : DalController
    {
        private const string MessageTableName = "Board";
        public BoardDalController() : base(MessageTableName)
        {

        }
    }
}
