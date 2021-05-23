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
    internal class ColumnDalController : DalController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); // Logs

        // Constructor
        public ColumnDalController() : base("Column")
        {

        }
        /// <summary>
        /// Insert new column to database
        /// </summary>
        /// <param name="column">The new column</param>
        /// <returns>True if seucced, false if failed</returns>
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
            
            ColumnDTO result = new ColumnDTO(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));

            return result;
        }
        /// <summary>
        /// Update column in the database
        /// </summary>
        /// <param name="boardId">The board ID to update</param>
        /// <param name="columnName">The column name to update</param>
        /// <param name="attributeName">The attribute to update</param>
        /// <param name="attributeValue">The new value</param>
        /// <returns></returns>
        public bool Update(int boardId,string columnName, string attributeName, int attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET {attributeName}=@attributeParam WHERE ({ColumnDTO.ColumnNameColumnName} = @ColumnNameParam AND {ColumnDTO.BoardIdColumnName}=@BoardIdParam)"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@BoardIdParam", boardId));
                    command.Parameters.Add(new SQLiteParameter("@attributeParam", attributeValue));
                    command.Parameters.Add(new SQLiteParameter("@ColumnNameParam", columnName));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    log.Error("Failed to run query");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return res > 0;
        }
        /// <summary>
        /// Delete column fron the database
        /// </summary>
        /// <param name="column">The column to update</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool Delete(ColumnDTO column)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_tableName} where {ColumnDTO.BoardIdColumnName}=@BoardIdVal AND {ColumnDTO.ColumnNameColumnName}=@ColumnNameVal ; "
                };
                try
                {
                    connection.Open();
                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal", column.BoardId);
                    SQLiteParameter columnnameParam = new SQLiteParameter(@"ColumnNameVal", column.Name);
                    
                    command.Parameters.Add(boardidParam);
                    command.Parameters.Add(columnnameParam);
                    
                    res = command.ExecuteNonQuery();

                }
                catch(Exception e)
                {
                    log.Error("Failed to run query");
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        /// <summary>
        /// Select column from the database
        /// </summary>
        /// <param name="BoardId">The board ID</param>
        /// <param name="ColumnName">The column to select</param>
        /// <returns>The column that selected</returns>
        public ColumnDTO SpecificSelect(int BoardId,string ColumnName)
        {
            ColumnDTO result = null;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_tableName} where {ColumnDTO.BoardIdColumnName}=@BoardIdVal AND {ColumnDTO.ColumnNameColumnName}=@ColumnNameVal;";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal", BoardId);
                    SQLiteParameter columnnameParam = new SQLiteParameter(@"ColumnNameVal", ColumnName);
                    command.Parameters.Add(boardidParam);
                    command.Parameters.Add(columnnameParam);
                    dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                        result = (ConvertReaderToObject(dataReader));


                }
                catch (Exception e)
                {

                    log.Error("Failed to run query");

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
            if (result == null)
                throw new Exception("Column could not be found");
            return result;
        }
        /// <summary>
        /// Delete column from the database
        /// </summary>
        /// <returns>True if seucced, false if failed</returns>
        public bool DeleteColumnTable()
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_tableName} ; "
                };
                try
                {
                    connection.Open();

                    res = command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    log.Error("Failed to run query");
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
