using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class TaskDalController : DalController
    {
        private const string MessageTableName = "Task";
        public TaskDalController() : base(MessageTableName)
        {

        }
    }
}
