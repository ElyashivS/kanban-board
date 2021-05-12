using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using IntroSE.Kanban.Backend.DataAccessLayer;
using log4net;
using System.Reflection;
using IntroSE.Kanban.Backend.DataAccessLayer.DTO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class UserDalController : DalController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        

        public UserDalController() : base("User")
        {
            
        }
        public bool Insert(UserDTO user)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName} ({UserDTO.usernameColumnName} ,{UserDTO.passwordColumnName}) " +
                        $"VALUES (@emailVal,@passwordVal);";

                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", user.Email);
                    SQLiteParameter passwordParam = new SQLiteParameter(@"passwordVal",user.Password);
                    

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(passwordParam);
                    
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    log.Error("Could not insert new Column");
                   
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }
        protected override UserDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            UserDTO result = new UserDTO(reader.GetString(1), reader.GetString(2));

            return result;
        }

        public bool Update(string email, string attributeName, string attributeValue) // Update for User and Column
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET {attributeName}=@attributeParam WHERE email = @emailParam"
                };
                try
                {
                    command.Parameters.Add(new SQLiteParameter("@attributeParam", attributeValue));
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
