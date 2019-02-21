using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerAPI.Controllers;
using TaskTrackerAPI.DAL.Repositories;
using Xunit;

namespace TaskTrackerAPI.Tests
{
    public class TaskControllerTests
    {
        private readonly Mock<ILogger<TaskController>> _logger = new Mock<ILogger<TaskController>>();
        private readonly Mock<ITaskRepository> _taskRepository = new Mock<ITaskRepository>();

        private TaskController Sut;

        public TaskControllerTests()
        {
            _taskRepository
                .Setup(repo => repo.DeleteTask(It.IsAny<int>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _taskRepository.
                Setup(repo => repo.MarkTaskAsDone(It.IsAny<int>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            Sut = new TaskController(_logger.Object, _taskRepository.Object);
        }

        #region DeleteTask Tests

        [Fact]
        public async Task Delete_Calls_Delete_On_Repository()
        {
            Random random = new Random();
            int taskId = random.Next(1, int.MaxValue);

            var result = await Sut.Delete(taskId);

            Assert.IsAssignableFrom<OkResult>(result);
            OkResult okResult = (OkResult)result;
            Assert.Equal((int)System.Net.HttpStatusCode.OK, okResult.StatusCode);

            _taskRepository
                .Verify(repo => repo.DeleteTask(taskId), Times.Once);
        }

        

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Delete_Not_Calls_Delete_On_Repository(int taskId)
        {
            var result = await Sut.Delete(taskId);

            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            BadRequestObjectResult badResult = (BadRequestObjectResult)result;
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, badResult.StatusCode);
            Assert.Equal("taskId", badResult.Value);

            _taskRepository
                .Verify(repo => repo.DeleteTask(taskId), Times.Never);
        }

        #endregion

        #region MarkTaskAsDone Tests

        
        [Fact]
        public async Task MarkTaskAsDone_Test()
        {
            Random random = new Random();
            int taskId = random.Next(1, int.MaxValue);

            var result = await Sut.MarkTaskAsDone(taskId);
            OkResult okResult = (OkResult)result;
            Assert.Equal((int)System.Net.HttpStatusCode.OK, okResult.StatusCode);

            //_taskRepository
            //    .Verify(repo => repo.DeleteTask(taskId), Times.Once);

        }

        #endregion

    }
}
