using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal abstract class DalController
    {
        protected readonly string _connectionString;
        private readonly string _tableName;
        public DalController(string tableName)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = tableName;
        }
    }
}
