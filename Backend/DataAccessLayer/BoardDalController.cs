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
    internal class BoardDalController : DalController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); // Logs

        // Constructor
        public BoardDalController() : base("Board")
        {

        }
        /// <summary>
        /// Insert Board to database
        /// </summary>
        /// <param name="board">The board to insert</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool Insert(BoardDTO board)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName} ({BoardDTO.IDColumnName} ,{BoardDTO.BoardNameColumnName},{BoardDTO.CreatorColumnName}) " +
                        $"VALUES (@IdVal,@BoardNameVal,@CreatorVal);";

                    SQLiteParameter boardIdParam = new SQLiteParameter(@"IdVal", board.ID);
                    SQLiteParameter boardNameParam = new SQLiteParameter(@"BoardNameVal", board.Name);
                    SQLiteParameter creatorParam = new SQLiteParameter(@"CreatorVal", board.Creator);

                    command.Parameters.Add(boardIdParam);
                    command.Parameters.Add(boardNameParam);
                    command.Parameters.Add(creatorParam);

                    command.Prepare();
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
                return res > 0;
            }
        }
        protected override BoardDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            BoardDTO result = new BoardDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

            return result;
        }

        /// <summary>
        /// Upddate board in the database
        /// </summary>
        /// <param name="id">The ID of the board</param>
        /// <param name="attributeName">The attribute to update</param>
        /// <param name="attributeValue">The new value</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool Update(int id, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET {attributeName}=@attributeParam WHERE id=@idParam"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@attributeParam", attributeValue));
                    command.Parameters.Add(new SQLiteParameter("@idParam", id));
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
        /// <summary>
        /// Delete board from the database
        /// </summary>
        /// <param name="board">The board to delete</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool Delete(BoardDTO board)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_tableName} where {BoardDTO.IDColumnName}=@BoardIdVal; "
                };
                try
                {
                    connection.Open();
                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal", board.ID);

                    command.Parameters.Add(boardidParam);

                    res = command.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    log.Error("Failed to run query");
                    Console.WriteLine(e);
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
        /// Select board from database by ID
        /// </summary>
        /// <param name="id">The ID</param>
        /// <returns>The board</returns>
        public BoardDTO SpecificSelect(int id)
        {
            BoardDTO result=null;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_tableName} where {BoardDTO.IDColumnName}=@IdVal ;";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    SQLiteParameter boardidParam = new SQLiteParameter(@"IdVal",id );
                    command.Parameters.Add(boardidParam);
                    dataReader = command.ExecuteReader();

                   if(dataReader.Read())
                        result=(ConvertReaderToObject(dataReader));
                   
                   
                }
                catch(Exception e)
                {
                    log.Debug(e.Message+"\n"+e.StackTrace);
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
            
            return result;
        }
        /// <summary>
        /// Insert user to assignee list
        /// </summary>
        /// <param name="BoardId">The board that the user inserted to</param>
        /// <param name="emailAssignee">The email of the user</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool InsertToAsigneeList(int BoardId, string emailAssignee)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {"AssigneeList"} ({AssigneeDTO.IDColumnName} ,{AssigneeDTO.AssigneeColumnName}) " +
                        $"VALUES (@BoardIdVal,@EmailAssigneeVal);";

                    SQLiteParameter boardIdParam = new SQLiteParameter(@"BoardIdVal", BoardId);
                    SQLiteParameter assigneeParam = new SQLiteParameter(@"EmailAssigneeVal", emailAssignee);
                    


                    command.Parameters.Add(boardIdParam);
                    command.Parameters.Add(assigneeParam);
                    


                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    log.Error("could not insert new Column");

                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }
                return res > 0;
            }
        }
        /// <summary>
        /// Delete user from assignee list
        /// </summary>
        /// <param name="BoardId">The board that the user deleted from</param>
        /// <param name="EmailAssignee">The email of the user</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool DeleteFromAssigneeList(int BoardId,string EmailAssignee)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {"AssigneeList"} where {AssigneeDTO.IDColumnName}=@BoardIdVal AND {AssigneeDTO.AssigneeColumnName}=@EmailAssigneeVal; "
                };
                try
                {
                    connection.Open();
                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal", BoardId);
                    SQLiteParameter emailAssigneeParam = new SQLiteParameter(@"EmailAssigneeVal", EmailAssignee);


                    command.Parameters.Add(boardidParam);
                    command.Parameters.Add(emailAssigneeParam);


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
        /// <summary>
        /// Select all the user that assignee to at list one board
        /// </summary>
        /// <returns>List of the users</returns>
        public List<DTO.DTO> SelectAssigneeList()
        {
            List<DTO.DTO> results = new List<DTO.DTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {"AssigneeList"};";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        results.Add(ConvertReaderToObjectAssignee(dataReader));

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    log.Error("Failed to run query ");
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
        protected  AssigneeDTO ConvertReaderToObjectAssignee(SQLiteDataReader reader)
        {
            AssigneeDTO result = new AssigneeDTO(reader.GetInt32(0), reader.GetString(1));

            return result;
        }
        /// <summary>
        /// Delete all boards
        /// </summary>
        /// <returns>True if seucced, false if failed</returns>
        public bool DeleteBoardTable()
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
        /// <summary>
        /// Delete all assignee list
        /// </summary>
        /// <returns>True if seucced, false if failed</returns>
        public bool DeleteAssigneeTable()
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {"AssigneeList"} ; "
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
