﻿using IntroSE.Kanban.Backend.DataAccessLayer.DTO;
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
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public BoardDalController() : base("Board")
        {

        }
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
                    Console.WriteLine(e);
                    log.Error("could not insert new Board");
                    
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
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        public BoardDTO SpecificSelect(int id)
        {
            BoardDTO result=null;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                command.CommandText = $"select * from {_tableName} where {BoardDTO.IDColumnName}=@BoardIdVal ;";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    SQLiteParameter boardidParam = new SQLiteParameter(@"BoardIdVal",id );
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
            if (result == null)
                throw new Exception("Board could not be found");
            return result;
        }
        public bool InsertToAsigneeList(int BoardId, string emailAssignee)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {"AssigneeList"} ({BoardDTO.IDColumnName} ,{BoardDTO.AssigneeColumnName}) " +
                        $"VALUES (@IdVal,@EmailAssigneeVal);";

                    SQLiteParameter boardIdParam = new SQLiteParameter(@"IdVal", BoardId);
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

        // TODO add function to remove assginee from board

        
    }
}

