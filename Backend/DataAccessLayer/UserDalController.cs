using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class UserDalController : DalController
    {
        private const string MessageTableName = "User";

        public UserDalController() : base(MessageTableName)
        {

        }
    }
}
