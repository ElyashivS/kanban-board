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
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); // Logs
        
        // Constructor
        public UserDalController() : base("User")
        {
            
        }
        /// <summary>
        /// Insert user to database
        /// </summary>
        /// <param name="user">The new user</param>
        /// <returns>True if seucced, false if failed</returns>
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
        protected override UserDTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            UserDTO result = new UserDTO(reader.GetString(0), reader.GetString(1));

            return result;
        }
        /// <summary>
        /// Update user in the database
        /// </summary>
        /// <param name="email">The email of the user to update</param>
        /// <param name="attributeName">The attribute to update</param>
        /// <param name="attributeValue">The new value</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool Update(string email, string attributeName, string attributeValue)
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
        /// Delete user from the database
        /// </summary>
        /// <param name="Email">The email of the user to delete</param>
        /// <returns>True if seucced, false if failed</returns>
        public bool Delete(string Email)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_tableName} where {UserDTO.usernameColumnName}=@EmailVal; "
                };
                try
                {
                    connection.Open();
                    SQLiteParameter emailParam = new SQLiteParameter(@"EmailVal", Email);


                    command.Parameters.Add(emailParam);


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
        /// Select specific user from the database
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>The user that selected</returns>
        public UserDTO SpecificSelect(string email)
        {
           UserDTO result = null;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_tableName} where {UserDTO.usernameColumnName}=@EmailVal ;";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    SQLiteParameter emailParam = new SQLiteParameter(@"EmailVal", email);
                    command.Parameters.Add(emailParam);
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
        /// Delete all users from the database
        /// </summary>
        /// <returns>True if seucced, false if failed</returns>
        public bool DeleteUserTable()
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
