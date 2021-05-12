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
                    command.CommandText = $"INSERT INTO {_tableName} ({TaskDTO.IdColumnName} ,{TaskDTO.EmailAssigneeColumnName},{TaskDTO.CreationTimeColumnName},{TaskDTO.DueDateColumnName},{TaskDTO.TitleColumnName},{TaskDTO.DescriptionColumnName}, {TaskDTO.ColumnNameColumnName}) " +
                        $"VALUES (@IdVal,@AssigneeVal,@CreationTimeVal,@DueDateVal,@TitleVal,@DescriptionVal,@ColumnNameVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"IdVal", task.ID);
                    SQLiteParameter assigneeParam = new SQLiteParameter(@"AssigneeVal", task.Assignee);
                    SQLiteParameter creationtimeParam = new SQLiteParameter(@"CreationTimeVal", task.CreationTime);
                    SQLiteParameter duedateParam = new SQLiteParameter(@"DueDateVal", task.DueDate);
                    SQLiteParameter titleParam = new SQLiteParameter(@"TitleVal", task.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"DescriptionVal", task.Description);
                    SQLiteParameter columnnameParam = new SQLiteParameter(@"ColumnNameVal", task.ColumnName);



                    command.Parameters.Add(idParam);
                    command.Parameters.Add(assigneeParam);
                    command.Parameters.Add(creationtimeParam);
                    command.Parameters.Add(duedateParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(descriptionParam);
                    command.Parameters.Add(columnnameParam);

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
            TaskDTO result = new TaskDTO((int)(long)reader.GetValue(0),reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(4),reader.GetString(5),reader.GetString(6));

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
                    CommandText = $"delete from {_tableName} where id={task.ID}"
                };
                try
                {
                    connection.Open();
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

        public bool Update(int id, int columnID, string attributeName, DateTime attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET {attributeName}=@attributeParam WHERE id=@idParam AND columnID=@columnParam"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@attributeParam", attributeValue));
                    command.Parameters.Add(new SQLiteParameter("@idParam", id));
                    command.Parameters.Add(new SQLiteParameter("@columnParam", columnID));
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

        public bool Update(int id, string columnName, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET {attributeName}=@attributeParam WHERE (id=@idParam AND ColumnName=@columnParam)"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@attributeParam", attributeValue));
                    command.Parameters.Add(new SQLiteParameter("@idParam", id));
                    command.Parameters.Add(new SQLiteParameter("@columnParam", columnName));
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

