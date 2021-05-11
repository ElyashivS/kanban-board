using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using log4net;
using System.Reflection;
using IntroSE.Kanban.Backend.DataAccessLayer.DTO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal abstract class DalController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected readonly string _connectionString;
        protected readonly string _tableName;
        public DalController(string tableName)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = tableName;
        }
        public List<DTO.DTO> Select()
        {
            List<DTO.DTO> results = new List<DTO.DTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_tableName};";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObject(dataReader));

                    }
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }

                    command.Dispose();
                    connection.Close();
                }

            }
            return results;
        }
        protected abstract DTO.DTO ConvertReaderToObject(SQLiteDataReader reader);

        public bool Update (int id, string attributeName, string attributeValue) // Update for Board and Task
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} set [{attributeName}]=@param where id={id}"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@param", attributeValue));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch
                {
                    log.Warn("Failed to run query");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;
        }

        public bool Update(string email, string attributeName, string attributeValue) // Update for User and Column
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} set {attributeName}=@param where email = @emailParam" //TODO implement Column
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@param", attributeValue));
                    command.Parameters.Add(new SQLiteParameter("@emailParam", email));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Warn("Failed to run query");
                    Console.WriteLine(e.Message); // Prints that helps to debug
                    Console.WriteLine(command.CommandText);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;
        }
    }
}
