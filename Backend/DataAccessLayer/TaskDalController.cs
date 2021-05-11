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
                    command.CommandText = $"INSERT INTO {_tableName} ({TaskDTO.IdColumnName} ,{TaskDTO.EmailAssigneeColumnName},{TaskDTO.CreationTimeColumnName},{TaskDTO.DueDateColumnName},{TaskDTO.TitleColumnName},{TaskDTO.DescriptionColumnName}) " +
                        $"VALUES (@IdVal,@AssigneeVal,@CreationTimeVal,@DueDateVal,@TitleVal,@DescriptionVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"IdVal", task.ID);
                    SQLiteParameter assigneeParam = new SQLiteParameter(@"AssigneeVal", task.Assignee);
                    SQLiteParameter creationtimeParam = new SQLiteParameter(@"CreationTimeVal", task.CreationTime);
                    SQLiteParameter duedateParam = new SQLiteParameter(@"DueDateVal", task.DueDate);
                    SQLiteParameter titleParam = new SQLiteParameter(@"TitleVal", task.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"DescriptionVal", task.Description);



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
            TaskDTO result = new TaskDTO((int)(long)reader.GetValue(0),reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(4),reader.GetString(5));

            return result;
        }
    }
 }

