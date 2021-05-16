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
        public bool Insert(TaskDTO task)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName} ({TaskDTO.BoardIdColumnName},{TaskDTO.ColumnNameColumnName},{TaskDTO.IdColumnName} ,{TaskDTO.EmailAssigneeColumnName},{TaskDTO.CreationTimeColumnName},{TaskDTO.DueDateColumnName},{TaskDTO.TitleColumnName},{TaskDTO.DescriptionColumnName}) " +
                        $"VALUES (@BoardIdVal ,@ColumnNameVal,@IdVal,@AssigneeVal,@CreationTimeVal,@DueDateVal,@TitleVal,@DescriptionVal);";

                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal", task.BoardId);
                    SQLiteParameter columnnameParam = new SQLiteParameter(@"ColumnNameVal", task.ColumnName);
                    SQLiteParameter idParam = new SQLiteParameter(@"IdVal", task.ID);
                    SQLiteParameter assigneeParam = new SQLiteParameter(@"AssigneeVal", task.Assignee);
                    SQLiteParameter creationtimeParam = new SQLiteParameter(@"CreationTimeVal", task.CreationTime);
                    SQLiteParameter duedateParam = new SQLiteParameter(@"DueDateVal", task.DueDate);
                    SQLiteParameter titleParam = new SQLiteParameter(@"TitleVal", task.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"DescriptionVal", task.Description);


                    command.Parameters.Add(boardidParam);
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
                    Console.WriteLine(e);
                    log.Error("could not insert the Task");

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
            TaskDTO result = new TaskDTO(reader.GetInt32(0),reader.GetInt32(1),reader.GetString(2),reader.GetString(3),reader.GetDateTime(4),reader.GetDateTime(5),reader.GetString(6),reader.GetString(7));

            return result;
        }
        public bool Delete(TaskDTO task)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_tableName} where {TaskDTO.IdColumnName}=@IdVal AND {TaskDTO.BoardIdColumnName}=@BoardIdVal ; "
                };
                try
                {
                    connection.Open();
                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal", task.BoardId);
                    
                    SQLiteParameter idParam = new SQLiteParameter(@"IdVal", task.ID);
                    command.Parameters.Add(boardidParam);
                    
                    command.Parameters.Add(idParam);
                    res = command.ExecuteNonQuery();
                   
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }

        public bool Update(int boardId, string columnname, int id, string attributeName, DateTime attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET {attributeName}=@attributeParam WHERE id=@idParam  AND BoardId=@BoardIdParam"
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

        public bool Update(int boardId, string columnName, int id,  string attributeName, string attributeValue)
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
        public TaskDTO SpecificSelect(int Id, string ColumnName,int taskId)
        {
            TaskDTO result = null;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_tableName} where {TaskDTO.BoardIdColumnName}=@BoardIdVal AND  {TaskDTO.IdColumnName}=@IdVal ;";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal", Id);
                    
                    SQLiteParameter idParam = new SQLiteParameter(@"IdVal", taskId);
                    command.Parameters.Add(boardidParam);
                    
                    command.Parameters.Add(idParam);
                    dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                        result = (ConvertReaderToObject(dataReader));


                }
                catch (Exception e)
                {
                    log.Debug(e.Message + "\n" + e.StackTrace);
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
                throw new Exception("Board could not be found");
            return result;
        }



    }
}

