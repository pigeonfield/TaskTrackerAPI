using System;
using System.Collections.Generic;
using System.Text;
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

            Assert.True(false);
        }

        [Theory]
        [InlineData(1, "dupa")]
        [InlineData(2, "dupa")]
        [InlineData(3, "dupa")]
        [InlineData(4, "cat")]
        public void InvalidPriorityRangeTest(int priority, string desc)
        { }
    }
}
