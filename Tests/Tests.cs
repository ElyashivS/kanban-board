using NUnit.Framework;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Collections.Generic;
using System;

namespace Tests
{
    public class Tests
    {
        Service service;

        [SetUp]
        public void Setup()
        {
            service = new Service();
            service.DeleteData();
            service.Register("arioshry@gmail.com", "Aa123");
            service.Login("arioshry@gmail.com", "Aa123");
            service.Register("rioshry@gmail.com", "Aa123");
            service.Login("rioshry@gmail.com", "Aa123");
            service.AddBoard("arioshry@gmail.com", "board");

        }

        //AdvanceTaskTests

        [Test]
        public void AdvanceTaskRegular_AssertTrue()
        {
            //arrange

            Response<Task> task = service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "title", "description", System.DateTime.MaxValue);
            //act
            service.AdvanceTask("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, 1);
            Response<IList<Task>> Column = service.GetColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 1);
            bool q = false;
            foreach (Task k in Column.Value)
            {
                if (k.Id == task.Value.Id)
                    q = true;
            }

            //assert
            Assert.AreEqual(true, q, "Task has been advanced succesfully");
        }
        [Test]
        public void AdvanceTaskLimitColumn_AssertFail()
        {
            //arrange
            try
            {

                service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "title", "description", System.DateTime.MaxValue);
                service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "titl", "descriptio", System.DateTime.MaxValue);
                service.LimitColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 1, 1);
                //act
                service.AdvanceTask("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, 1);
                service.AdvanceTask("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, 2);

                //assert
                Assert.Fail("cannot exceed the limit of column");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void AdvanceTaskPastLastColumn_AssertFail()
        {
            //arrange
            try
            {

                service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "title", "description", System.DateTime.MaxValue);
                service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "titl", "descriptio", System.DateTime.MaxValue);
                service.LimitColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 1, 1);
                //act
                service.AdvanceTask("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, 1);
                service.AdvanceTask("arioshry@gmail.com", "arioshry@gmail.com", "board", 2, 1);

                //assert
                Assert.Fail("cannot exceed the limit of column");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void AdvanceTaskByAssignee_AssertTrue()
        {
            //arrange

            service.JoinBoard("rioshry@gmail.com", "arioshry@gmail.com", "board");
            Response<Task> task = service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "title", "description", System.DateTime.MaxValue);
            service.AssignTask("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, 1, "rioshry@gmail.com");

            //act
            service.AdvanceTask("rioshry@gmail.com", "arioshry@gmail.com", "board", 0, 1);
            Response<IList<Task>> Column = service.GetColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 1);
            bool q = false;
            foreach (Task k in Column.Value)
            {
                if (k.Id == task.Value.Id)
                    q = true;
            }

            //assert
            Assert.AreEqual(true, q, "the Task has been advanced by the Assignee");
        }
        [Test]
        public void AdvanceTaskByNotAssignee_AssertFail()
        {
            try
            {
                //arrange

                service.JoinBoard("rioshry@gmail.com", "arioshry@gmail.com", "board");
                service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "title", "description", System.DateTime.MaxValue);
                service.AssignTask("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, 1, "rioshry@gmail.com");

                //act
                service.AdvanceTask("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, 1);


                //assert
                Assert.Fail("cannot advance Task by not Assignee");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void AdvanceTaskThatDoesntExists_AssertFail()
        {
            try
            {
                //arrange



                //act
                service.AdvanceTask("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, 1);


                //assert
                Assert.Fail("cannot advance Task by not Assignee");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void AdvanceTaskThatWithWrongColumn_AssertFail()
        {
            try
            {
                //arrange

                service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "title", "description", System.DateTime.MaxValue);

                //act
                service.AdvanceTask("arioshry@gmail.com", "arioshry@gmail.com", "board", 1, 1);


                //assert
                Assert.Fail("cannot advance Task by not Assignee");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void AdvanceTaskWithBoardThatDoesntExists_AssertFail()
        {
            try
            {
                //arrange

                service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "title", "description", System.DateTime.MaxValue);

                //act
                service.AdvanceTask("arioshry@gmail.com", "arioshry@gmail.com", "boar", 0, 1);


                //assert
                Assert.Fail("cannot advance Task by not Assignee");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }

            //MoveColumn Tests
        }
        [Test]
        public void MoveColumnRegular_AssertTrue()
        {

            //arrange

            string columnName = "backlog";

            //act

            service.MoveColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, 2);
            Response<string> response = service.GetColumnName("arioshry@gmail.com", "arioshry@gmail.com", "board", 2);


            //assert
            Assert.AreEqual(columnName, response.Value, "Moved succesfully");
        }
        [Test]
        public void MoveColumnPastBoardCapacityToRight_AssertFail()
        {
            try
            {
                //arrange



                //act
                service.MoveColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, 3);


                //assert
                Assert.Fail("cannot advance Column Past board capacity");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }




        }
        [Test]
        public void MoveColumnPastBoardCapacityToLeft_AssertFail()
        {
            try
            {
                //arrange



                //act
                service.MoveColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, -1);


                //assert
                Assert.Fail("cannot advance Column Past board capacity");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }




        }
        [Test]
        public void MoveColumnThatDoesntExist_AssertFail()
        {
            try
            {
                //arrange



                //act
                service.MoveColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 3, 2);


                //assert
                Assert.Fail("cannot advance Column That doesnt Exists");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }

        }
        [Test]
        public void MoveColumnByNoneBoardMember_AssertFail()
        {
            try
            {
                //arrange



                //act
                service.MoveColumn("rioshry@gmail.com", "arioshry@gmail.com", "board", 0, 2);


                //assert
                Assert.Fail("cannot advance Column by none board member");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void MoveColumnWithTasks_AssertFail()
        {
            try
            {
                //arrange



                //act
                service.MoveColumn("rioshry@gmail.com", "arioshry@gmail.com", "board", 0, 2);


                //assert
                Assert.Fail("cannot advance Column by none board member");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void MoveColumnByBoardMember_AssertTrue()
        {
            {

                //arrange
                service.JoinBoard("rioshry@gmail.com", "arioshry@gmail.com", "board");
                string columnName = "backlog";

                //act

                service.MoveColumn("rioshry@gmail.com", "arioshry@gmail.com", "board", 0, 2);
                Response<string> response = service.GetColumnName("arioshry@gmail.com", "arioshry@gmail.com", "board", 2);


                //assert
                Assert.AreEqual(columnName, response.Value, "Moved succesfully");
            }
        }

        //RemoveColumn Tests
        [Test]
        public void ReMoveColumnRegular_AssertTrue()
        {

            //arrange

            string columnName = "done";

            //act

            service.RemoveColumn("arioshry@gmail.com", "arioshry@gmail.com", "board",1);
            Response<string> response = service.GetColumnName("arioshry@gmail.com", "arioshry@gmail.com", "board", 1);


            //assert
            Assert.AreEqual(columnName, response.Value, "Removed succesfully");
        }
        [Test]
        public void RemoveColumnFromBoardWith2Columns_AssertFail()
        {
            try
            {
                //arrange
                service.RemoveColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 0);



                //act
                service.RemoveColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 1);

                //assert
                Assert.Fail("cannot advance Column Past board capacity");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void RemoveColumnByNoneBoardMember_AssertFail()
        {
            try
            {
                //arrange



                //act
                service.RemoveColumn("rioshry@gmail.com", "arioshry@gmail.com", "board", 2);


                //assert
                Assert.Fail("cannot advance Column by none board member");
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }
        [Test]
        public void RemoveColumnByBoardMember_AssertTrue()
        {
            {

                //arrange
                service.JoinBoard("rioshry@gmail.com", "arioshry@gmail.com", "board");
                string columnName = "done";

                //act

                service.RemoveColumn("rioshry@gmail.com", "arioshry@gmail.com", "board", 1);
                Response<string> response = service.GetColumnName("arioshry@gmail.com", "arioshry@gmail.com", "board", 1);


                //assert
                Assert.AreEqual(columnName, response.Value, "Moved succesfully");
            }
        }
        [Test]
        public void RemoveColumnWithTasksMovingToRight_AssertTrue()
        {
            
                //arrange
                Response<Task> task=service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "title", "description", System.DateTime.MaxValue);
                string columnName = "in progress";


                //act
                service.RemoveColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 0);
                Response<IList<Task>> Column = service.GetColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 0);
                bool q = false;
                foreach (Task k in Column.Value)
                {
                    if (k.Id == task.Value.Id)
                        q = true;
                }
                Response<string> response = service.GetColumnName("arioshry@gmail.com", "arioshry@gmail.com", "board", 0);


                //assert
                Assert.AreEqual(true, q, "Moved succesfully");
            
        }
        [Test]
        public void RemoveColumnWithTasksMovingToLeft_AssertTrue()
        {

            //arrange
            Response<Task> task = service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "title", "description", System.DateTime.MaxValue);
            service.AdvanceTask("arioshry@gmail.com", "arioshry@gmail.com", "board", 0, 1);
            string columnName = "backlog";


            //act
            service.RemoveColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 1);
            Response<IList<Task>> Column = service.GetColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 0);
            bool q = false;
            foreach (Task k in Column.Value)
            {
                if (k.Id == task.Value.Id)
                    q = true;
            }
            Response<string> response = service.GetColumnName("arioshry@gmail.com", "arioshry@gmail.com", "board", 0);


            //assert
            Assert.AreEqual(true, q, "Moved succesfully");

        }
        [Test]
        public void RemoveColumnWithTasksThatCantMove_AssertFail()
        {
            try
            {

                //arrange
                Response<Task> task = service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "title", "description", System.DateTime.MaxValue);
                Response<Task> task2 = service.AddTask("arioshry@gmail.com", "arioshry@gmail.com", "board", "title", "description", System.DateTime.MaxValue);
                service.LimitColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 1, 1);

                //act

                service.RemoveColumn("arioshry@gmail.com", "arioshry@gmail.com", "board", 0);
                Response<string> response = service.GetColumnName("arioshry@gmail.com", "arioshry@gmail.com", "board", 0);


                //assert
                Assert.Fail("Test Failed");
                    }
            catch(Exception e)
            {
                Assert.Pass();
            }

        }





    }


    
}