using IntroSE.Kanban.Backend.DataAccessLayer.DTO;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class ColumnDalController : DalController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public ColumnDalController() : base("Column")
        {
            
        }
        public bool Insert(ColumnDTO column)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName} ({ColumnDTO.BoardIdColumnName} ,{ColumnDTO.ColumnNameColumnName},{ColumnDTO.ColumnLimiterColumnName}) " +
                        $"VALUES (@BoardIdVal,@ColumnName,@ColumnLimiter);";

                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal", column.BoardId);
                    SQLiteParameter columnnameParam = new SQLiteParameter(@"ColumnName", column.Name);
                    SQLiteParameter columnlimiterParam = new SQLiteParameter(@"ColumnLimiter", column.ColumnLimiter);


                    command.Parameters.Add(boardidParam);
                    command.Parameters.Add(columnnameParam);
                    command.Parameters.Add(columnlimiterParam);

                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("could not insert new user");
                    
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }
                return res > 0;
            }
        }
        protected override ColumnDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            ColumnDTO result = new ColumnDTO((int)(long)reader.GetValue(0), reader.GetString(1),(int)(long)reader.GetValue(2));

            return result;
        }

    }
}
