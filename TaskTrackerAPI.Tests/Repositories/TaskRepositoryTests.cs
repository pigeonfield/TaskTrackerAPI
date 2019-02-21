using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerAPI.AppExceptions;
using TaskTrackerAPI.DAL.DAO;
using TaskTrackerAPI.DAL.Repositories;
using TaskTrackerAPI.DataFilters;
using TaskTrackerAPI.Models;
using Xunit;

namespace TaskTrackerAPI.Tests.Repositories
{
    public class TaskRepositoryTest : IDisposable
    {
        private readonly AppDbContext _appDbContext;

        private TaskRepository Sut;

        #region DataForTests

        private readonly List<TaskModel> _data = new List<TaskModel>()
        {
                            new TaskModel()
                            {
                                Name = "Concert",
                                ShortDescription = "Lorem Ipsum",
                                LongDescription = "Lorem ipsum and lorem ipsum",
                                CreatedAt = DateTime.Now,
                                Priority = DAL.DAO.Enums.PriorityEnum.High,
                                IsDone = false,
                                Category = DAL.DAO.Enums.CategoryEnum.Private,
                                Comments = new List<Comment>()
                                {
                                    new Comment()
                                    {
                                    Content = "Central Park lorem ipsum Central park",
                                    TaskId = 1
                                    },
                                    new Comment()
                                    {
                                    Content = "Central Park lorem ipsum Central park ipsum Central park",
                                    TaskId = 1
                                    },
                                }
                            },

                        new TaskModel()
                        {
                            Name = "Appointment",
                            ShortDescription = "Lorem Ipsum",
                            LongDescription = "Lorem ipsum and lorem ipsum. Central Park lorem ipsum Central park ipsum Central park, Central Park lorem, Central Park lorem,Central Park lorem, Central Park lorem",
                            CreatedAt = DateTime.Now,
                            Priority = DAL.DAO.Enums.PriorityEnum.Low,
                            IsDone = true,

                            Category = DAL.DAO.Enums.CategoryEnum.Travelling,
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Content = "Central lorem ipsum",
                                    TaskId = 2
                                },
                                new Comment()
                                {
                                    Content = "Central Park lorem ipsum Central park ipsum Central park, Central Park lorem, Central Park lorem,Central Park lorem, Central Park lorem",
                                    TaskId = 2
                                },
                                new Comment()
                                {
                                    Content = "Central Park lorem Central Park lorem ipsum Central park ipsum Central park",
                                    TaskId = 2
                                },
                            }
                        },

                        new TaskModel()
                        {
                            Name = "Meeting",
                            ShortDescription = "Lorem Ipsum",
                            LongDescription = "Lorem ipsum and lorem ipsum",
                            CreatedAt = DateTime.Now,
                            Priority = DAL.DAO.Enums.PriorityEnum.Medium,
                            IsDone = false,

                            Category = DAL.DAO.Enums.CategoryEnum.Travelling,
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Content = "Central Park ",
                                    TaskId = 3
                                },
                                new Comment()
                                {
                                    Content = "Central Park lorem ipsum Central park ipsum Central park",
                                    TaskId = 3
                                },
                                new Comment()
                                {
                                    Content = "Central Park lorem ipsum Central park ipsum Central park, Central Park lorem ipsum Central park ipsum Central park",
                                    TaskId = 3
                                },
                            }
                        },

                        new TaskModel()
                        {
                            Name = "Toilet paper",
                            ShortDescription = "Lorem Ipsum",
                            LongDescription = "Lorem ipsum and lorem ipsum. Central Park lorem ipsum Central park ipsum Central park, Central Park lorem, Central Park lorem,Central Park lorem, Central Park lorem.Central Park lorem ipsum Central park ipsum Central park, Central Park lorem, Central Park lorem,Central Park lorem, Central Park lorem",
                            CreatedAt = DateTime.Now,
                            Priority = DAL.DAO.Enums.PriorityEnum.Medium,
                            IsDone = true,

                            Category = DAL.DAO.Enums.CategoryEnum.Private,
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Content = "Central Park ",
                                    TaskId = 3
                                },
                                new Comment()
                                {
                                    Content = "Central Park lorem ipsum Central park ipsum Central park",
                                    TaskId = 3
                                },
                                new Comment()
                                {
                                    Content = "Central Park lorem ipsum Central park ipsum Central park, Central Park lorem ipsum Central park ipsum Central park",
                                    TaskId = 3
                                },
                            }
                        },

                        new TaskModel()
                        {
                            Name = "Onion smell",
                            ShortDescription = "Lorem Ipsum",
                            LongDescription = "Lorem ipsum and lorem ipsum",
                            CreatedAt = DateTime.Now,
                            Priority = DAL.DAO.Enums.PriorityEnum.Medium,
                            IsDone = false,

                            Category = DAL.DAO.Enums.CategoryEnum.Shopping,
                            Comments = new List<Comment>()

                        },

                        new TaskModel()
                        {
                            Name = "Unicorns",
                            ShortDescription = "Lorem Ipsum",
                            LongDescription = "Lorem ipsum and lorem ipsum",
                            CreatedAt = DateTime.Now,
                            Priority = DAL.DAO.Enums.PriorityEnum.Medium,
                            IsDone = false,

                            Category = DAL.DAO.Enums.CategoryEnum.Work,
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Content = "Central Park ",
                                    TaskId = 3
                                },
                                new Comment()
                                {
                                    Content = "Central Park lorem ipsum Central park ipsum Central park",
                                    TaskId = 3
                                },

                            }
                        }
        };

        #endregion

        public TaskRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Run the test against one instance of the context
            _appDbContext = new AppDbContext(options);
            _appDbContext.Database.EnsureCreated();

            if (!_appDbContext.Tasks.Any())
            {


                _appDbContext.Tasks.AddRange(_data);
                _appDbContext.SaveChanges();



                Sut = new TaskRepository(_appDbContext);
            }
        }

        public void Dispose()
        {
            _appDbContext.Database.EnsureDeleted();
            _appDbContext.Dispose();
        }

        #region GetTask Tests

        [Fact]
        public async Task GetExistingTaskByIdAsync()
        {
            int taskId = _appDbContext.Tasks.Min(taskDb => taskDb.TaskId);

            TaskModel task = await Sut.GetTask(taskId);

            Assert.NotNull(task);
            Assert.Equal(_data.First().Name, task.Name);
            Assert.Equal("Lorem Ipsum", task.ShortDescription);
            Assert.Equal("Lorem ipsum and lorem ipsum", task.LongDescription);
            Assert.Equal(DAL.DAO.Enums.PriorityEnum.High, task.Priority);
            Assert.False(task.IsDone);
            Assert.Equal(DAL.DAO.Enums.CategoryEnum.Private, task.Category);
            Assert.Equal(2, task.Comments.Count());
        }

        [Fact]
        public async Task GetNoTaskByInCorectIdAsync()
        {
            TaskModel task = await Sut.GetTask(int.MaxValue);

            Assert.Null(task);
        }

        #endregion

        #region GetAllTasks Tests

        [Fact]
        public async Task GetAllTasksAsyncTest()
        {
            List<TaskModel> tasks = await Sut.GetAllTasks();

            Assert.NotEmpty(tasks);
            Assert.Equal(6, tasks.Count);

        }

        #endregion


        #region GetFilteredTasks Tests

        [Fact]
        public async Task GetAllWhenFilterIsNull()
        {
            TaskFilter filter = new TaskFilter { };

            List<TaskModel> tasks = await Sut.GetFilteredResult(filter);

            Assert.NotEmpty(tasks);
            Assert.Equal(6, tasks.Count);

        }

        [Fact]
        public async Task GetAllDoneTasks()
        {
            TaskFilter filter = new TaskFilter
            {
                IsDone = true
            };

            List<TaskModel> tasks = await Sut.GetFilteredResult(filter);

            Assert.NotEmpty(tasks);
            Assert.Equal(2, tasks.Count);

        }

        [Fact]
        public async Task GetNotDoneTasksOfWorkCategory()
        {
            TaskFilter filter = new TaskFilter
            {
                IsDone = false,
                Category = DAL.DAO.Enums.CategoryEnum.Work
            };

            List<TaskModel> tasks = await Sut.GetFilteredResult(filter);

            Assert.NotEmpty(tasks);
            Assert.Single(tasks);

        }

        [Fact]
        public async Task GetFilteredTasksReturnEmptyList()
        {
            TaskFilter filter = new TaskFilter
            {
                IsDone = true,
                Category = DAL.DAO.Enums.CategoryEnum.Work
            };

            List<TaskModel> tasks = await Sut.GetFilteredResult(filter);

            Assert.Empty(tasks);

        }
        #endregion

        #region AddTask Tests

        [Fact]
        public async Task AddTaskTestPassed()
        {
            TaskModel newTask = new TaskModel
            {
                Name = "Dupa",
                ShortDescription = "DupaDupa",
                LongDescription = "DupaDupaDupa",
                Priority = DAL.DAO.Enums.PriorityEnum.High,
                CreatedAt = DateTime.Now,
                IsDone = false,
                Category = DAL.DAO.Enums.CategoryEnum.Work,
                Comments = new List<Comment>()
            };

            int numberOfRecordsBeforeAdd = _appDbContext.Tasks.Count();

            await Sut.AddTask(newTask);

            int numberOfRecordsAfterAdd = _appDbContext.Tasks.Count();
            var addedRecord = _appDbContext.Tasks.Last();

            Assert.NotNull(newTask);
            Assert.Equal(numberOfRecordsBeforeAdd + 1, numberOfRecordsAfterAdd);
            Assert.Equal(newTask.Name, addedRecord.Name);
            Assert.Equal(newTask.ShortDescription, addedRecord.ShortDescription);
            Assert.Equal(newTask.LongDescription, addedRecord.LongDescription);
            Assert.Equal(newTask.Priority, addedRecord.Priority);
            Assert.Equal(newTask.IsDone, addedRecord.IsDone);
            Assert.Equal(newTask.Category, addedRecord.Category);
        }


        [Fact]
        public async Task AddTaskTestFailed()
        {
            TaskModel newTask = new TaskModel
            {
                Priority = DAL.DAO.Enums.PriorityEnum.High,
                CreatedAt = DateTime.Now,

            };

            int numberOfRecordsBeforeAdd = _appDbContext.Tasks.Count();

            await Sut.AddTask(newTask);

            int numberOfRecordsAfterAdd = _appDbContext.Tasks.Count();
            var addedRecord = _appDbContext.Tasks.Last();

            Assert.NotNull(newTask);
            Assert.Equal(numberOfRecordsBeforeAdd + 1, numberOfRecordsAfterAdd);
            Assert.Equal(newTask.Name, addedRecord.Name);
            Assert.Equal(newTask.ShortDescription, addedRecord.ShortDescription);
            Assert.Equal(newTask.LongDescription, addedRecord.LongDescription);
            Assert.Equal(newTask.Priority, addedRecord.Priority);
            Assert.Equal(newTask.IsDone, addedRecord.IsDone);
            Assert.Equal(newTask.Category, addedRecord.Category);
        }


        #endregion

        #region UpdateTask Tests 

        [Fact]
        public async Task TaskToUpdateNotExist()
        {
            TaskModel taskEdited = new TaskModel
            {
                Name = "Dupa",
                ShortDescription = "DupaDupa",
                LongDescription = "DupaDupaDupa",
                Priority = DAL.DAO.Enums.PriorityEnum.High,
                CreatedAt = DateTime.Now,
                IsDone = false,
                Category = DAL.DAO.Enums.CategoryEnum.Work,
                Comments = new List<Comment>()
            };

            Func<Task> result = () => Sut.UpdateTask(int.MaxValue, taskEdited);
            await Assert.ThrowsAsync<TaskNotFoundException>(result);
        }

        //[Fact]
        //public async Task TaskToUpdateNotExist()
        //{
        //    TaskModel taskEdited = new TaskModel
        //    {
        //        Name = "Dupa",
        //        ShortDescription = "DupaDupa",
        //        LongDescription = "DupaDupaDupa",
        //        Priority = DAL.DAO.Enums.PriorityEnum.High,
        //        CreatedAt = DateTime.Now,
        //        IsDone = false,
        //        Category = DAL.DAO.Enums.CategoryEnum.Work,
        //        Comments = new List<Comment>()

        //    };

        //    Action result = async () => await Sut.UpdateTask(int.MaxValue, taskEdited);
        //    Assert.Throws<TaskNotFoundException>(result);


        //}

        [Fact]
        public async Task UpdateTaskTestsPassed()
        {
            int numberOfRecord = _appDbContext.Tasks.First().TaskId;
            TaskModel taskToUpdate = _appDbContext.Tasks.FirstOrDefault(t => t.TaskId == numberOfRecord);

            TaskModel taskEdited = new TaskModel

            {
                Name = "Name",
                Priority = DAL.DAO.Enums.PriorityEnum.High,
                Category = DAL.DAO.Enums.CategoryEnum.Shopping,
            };

            await Sut.UpdateTask(taskToUpdate.TaskId, taskEdited);

            Assert.Equal(taskEdited.Name, taskToUpdate.Name);
            Assert.Equal(taskEdited.Priority, taskToUpdate.Priority);
            Assert.Equal(taskEdited.Category, taskToUpdate.Category);
        }

        [Fact]
        public async Task UpdateTaskDeleteLongDesc()
        {

            TaskModel taskEdited = new TaskModel
            {
                LongDescription = String.Empty
            };

            int numberOfRecord = _appDbContext.Tasks.First().TaskId;
            TaskModel taskToUpdate = _appDbContext.Tasks.FirstOrDefault(t => t.TaskId == numberOfRecord);


            string longDescBeforeUpdate = taskToUpdate.LongDescription;
            Assert.NotEmpty(longDescBeforeUpdate);

            var updateModel = await Sut.UpdateTask(taskToUpdate.TaskId, taskEdited);

            TaskModel taskAfterUpdate = _appDbContext.Tasks.FirstOrDefault(t => t.TaskId == numberOfRecord);

            Assert.Empty(taskAfterUpdate.LongDescription);


        }
        #endregion

        #region MarkTaskAsDone Tests 

        [Fact]
        public async Task MarkTaskAsDoneTestPassed()
        {
            int numberOfRecord = _appDbContext.Tasks.First().TaskId;
            TaskModel taskToUpdate = _appDbContext.Tasks.FirstOrDefault(t => t.TaskId == numberOfRecord);

            Assert.False(taskToUpdate.IsDone);

            await Sut.MarkTaskAsDone(taskToUpdate.TaskId);

            Assert.True(taskToUpdate.IsDone);

        }

        [Fact]
        public void MarkTaskAsDoneTestFailed()
        {
            Action result = () => Sut.MarkTaskAsDone(int.MaxValue);
            Assert.Throws<TaskNotFoundException>(result);
        }

        [Fact]
        public void MarkTaskAsDoneTestFailedNegativeID()
        {
            Action result = () => Sut.MarkTaskAsDone(-4);
            Assert.Throws<ArgumentOutOfRangeException>(result);
        }

        #endregion


        #region DeleteTask Tests 

        [Fact]
        public async Task DeleteExistingTask()
        {
            int numberOfRecord = _appDbContext.Tasks.First().TaskId;

            await Sut.DeleteTask(numberOfRecord);

            Assert.DoesNotContain(_appDbContext.Tasks, task => task.TaskId == numberOfRecord);

        }

        #endregion

        #region Comments Tests 



        #endregion
    }
}


