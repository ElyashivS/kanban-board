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
     internal class TaskDalController : DalController
     {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public TaskDalController() : base("Task")
        {

        }
        /// <summary>
        /// Insert new task to the database
        /// </summary>
        /// <param name="task">The task to insert</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool Insert(TaskDTO task)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName} ({TaskDTO.BoardIdColumnName},{TaskDTO.ColumnIdColumnName},{TaskDTO.IdColumnName},{TaskDTO.ColumnNameColumnName} ,{TaskDTO.EmailAssigneeColumnName},{TaskDTO.CreationTimeColumnName},{TaskDTO.DueDateColumnName},{TaskDTO.TitleColumnName},{TaskDTO.DescriptionColumnName}) " +
                        $"VALUES (@BoardIdVal ,@ColumnIdVal,@IdVal,@ColumnNameVal,@AssigneeVal,@CreationTimeVal,@DueDateVal,@TitleVal,@DescriptionVal);";

                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal", task.BoardId);
                    SQLiteParameter columnidParam = new SQLiteParameter(@"ColumnIdVal", task.ColumnId);
                    SQLiteParameter idParam = new SQLiteParameter(@"IdVal", task.ID);;
                    SQLiteParameter columnnameParam = new SQLiteParameter(@"ColumnNameVal", task.ColumnName);
                    SQLiteParameter assigneeParam = new SQLiteParameter(@"AssigneeVal", task.Assignee);
                    SQLiteParameter creationtimeParam = new SQLiteParameter(@"CreationTimeVal", task.CreationTime);
                    SQLiteParameter duedateParam = new SQLiteParameter(@"DueDateVal", task.DueDate);
                    SQLiteParameter titleParam = new SQLiteParameter(@"TitleVal", task.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"DescriptionVal", task.Description);


                    command.Parameters.Add(boardidParam);
                    command.Parameters.Add(columnidParam);
                    command.Parameters.Add(columnnameParam);
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(assigneeParam);
                    command.Parameters.Add(creationtimeParam);
                    command.Parameters.Add(duedateParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(descriptionParam);
                    

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
        protected override TaskDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
        TaskDTO result = new TaskDTO(reader.GetInt32(0),reader.GetInt32(1),reader.GetInt32(2),reader.GetString(3),reader.GetString(4),reader.GetDateTime(5),reader.GetDateTime(6),reader.GetString(7),reader.GetString(8));

        return result;
        }
        /// <summary>
        /// Delete task from the database
        /// </summary>
        /// <param name="task">The task to delete</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool Delete(TaskDTO task)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_tableName} where {TaskDTO.IdColumnName}=@IdVal AND {TaskDTO.BoardIdColumnName}=@BoardIdVal AND {TaskDTO.ColumnIdColumnName}=@ColumnIdVal ; "
                };
                try
                {
                    connection.Open();
                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal", task.BoardId);
                    SQLiteParameter columnidParam = new SQLiteParameter(@"ColumnIdVal", task.ColumnId);

                    SQLiteParameter idParam = new SQLiteParameter(@"IdVal", task.ID);
                    command.Parameters.Add(boardidParam);
                    command.Parameters.Add(columnidParam);
                    command.Parameters.Add(idParam);
                    res = command.ExecuteNonQuery();
                   
                }
                catch
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
        /// Update task in the database
        /// </summary>
        /// <param name="boardId">The board ID</param>
        /// <param name="id">The ID of the task</param>
        /// <param name="attributeName">The attribute to update</param>
        /// <param name="attributeValue">The new value</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool Update(int boardId,int columnId, int id, string attributeName, DateTime attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET {attributeName}=@attributeParam WHERE {TaskDTO.IdColumnName}=@idParam  AND {TaskDTO.BoardIdColumnName}=@BoardIdParam AND {TaskDTO.BoardIdColumnName}=@ColumnIdParam"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@BoardIdParam", boardId));
                    command.Parameters.Add(new SQLiteParameter("@ColumnIdParam", columnId));
                    command.Parameters.Add(new SQLiteParameter("@attributeParam", attributeValue));
                    command.Parameters.Add(new SQLiteParameter("@idParam", id));
                    
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
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
        /// <summary>
        /// Update task in the database
        /// </summary>
        /// <param name="boardId">The board ID</param>
        /// <param name="id">The task ID</param>
        /// <param name="attributeName">The attribute to update</param>
        /// <param name="attributeValue">The new value</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool Update(int boardId, int id,  string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET {attributeName}=@attributeParam WHERE (id=@idParam  AND BoardId=@BoardIdParam)"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@BoardIdParam", boardId));
                    command.Parameters.Add(new SQLiteParameter("@attributeParam", attributeValue));
                    command.Parameters.Add(new SQLiteParameter("@idParam", id));
                    
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
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
        public bool Update(int boardId, int id, string attributeName, int attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET {attributeName}=@attributeParam WHERE (id=@idParam  AND BoardId=@BoardIdParam)"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@BoardIdParam", boardId));
                    command.Parameters.Add(new SQLiteParameter("@attributeParam", attributeValue));
                    command.Parameters.Add(new SQLiteParameter("@idParam", id));

                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
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
        /// <summary>
        /// Select specific task from the database
        /// </summary>
        /// <param name="Id">The board ID to update</param>
        /// <param name="taskId">The task ID to update</param>
        /// <returns>The updated task</returns>
        public TaskDTO SpecificSelect(int boardId, int columnId ,int taskId)
        {
            TaskDTO result = null;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_tableName} where {TaskDTO.BoardIdColumnName}=@BoardIdVal AND {TaskDTO.BoardIdColumnName}=@ColumnIdVal AND  {TaskDTO.IdColumnName}=@IdVal ;";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal", boardId);
                    SQLiteParameter columnidParam = new SQLiteParameter(@"ColumnIdVal", boardId);
                    SQLiteParameter idParam = new SQLiteParameter(@"IdVal", taskId);
                    command.Parameters.Add(boardidParam);
                    command.Parameters.Add(columnidParam);

                    command.Parameters.Add(idParam);
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
           
            return result;
        }
        /// <summary>
        /// Delete all tasks from database
        /// </summary>
        /// <returns>True if seucced, false if failed</returns>
        public bool DeleteTaskTable()
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

