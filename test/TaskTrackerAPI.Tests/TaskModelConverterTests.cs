using System;
using System.Collections.Generic;
using TaskTrackerAPI.DAL.DAO;
using TaskTrackerAPI.DAL.DAO.Enums;
using TaskTrackerAPI.DAL.ExtensionMethods;
using TaskTrackerAPI.Models;
using Xunit;

namespace TaskTrackerAPI.Tests
{
    public class TaskModelConverterTests
    {
        [Fact]
        public void IdFieldMissingTest()
        {

            //arrange
            //setup test data
            //Moq - Moq Nuget

            //act
            //test your classes/code

            //assert
            //check outcome
            //Assert

            Assert.True(true);
        }

        [Theory]
        [InlineData("Sraatta", "Dupa, dupa dupa dupa.", "24.01.2019 12:35:25", PriorityEnum.High, true, CategoryEnum.Private, null)]
        [InlineData("Sraatta", "Dupa, dupa dupa dupa.", "24.01.2019 12:35:25", PriorityEnum.Low, true, CategoryEnum.Shopping, null)]
        [InlineData("Sraatta", "Dupa, dupa dupa dupa.", "24.01.2019 12:35:25", PriorityEnum.Medium, true, CategoryEnum.Shopping, null)]
        public void InvalidPriorityRangeTest(string name, string desc, string createdAt,
                                            PriorityEnum priority, bool isDone, CategoryEnum category, ICollection<Comment> comments)
        {
            DateTime createdAtDate = DateTime.Parse(createdAt);

            var obj = new TaskModel()
            {
                Name = name,
                ShortDescription = desc,
                CreatedAt = createdAtDate,
                Priority = priority,
                IsDone = isDone,
                Category = category,
                Comments = comments ?? new List<Comment>()

            };

            var objConverted = TaskModelConverter.ConvertTaskToShowAllView(obj);

            Assert.Equal(obj.Name, objConverted.Name);
            Assert.Equal(obj.ShortDescription, objConverted.ShortDescription);

            Assert.Equal(priority, objConverted.Priority);

            Assert.Equal(obj.Comments.Count, objConverted.NumberOfComments);
        }
    }
}
