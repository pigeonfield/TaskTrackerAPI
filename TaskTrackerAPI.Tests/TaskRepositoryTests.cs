using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTrackerAPI.Tests
{
    class TaskRepositoryTests
    {
        //AppDbContext - UseInMemoryDatabase - tak setupowac context
        //TaskRepository contructor should have manually created DbContext passed
        //Db context member Database - EnsureDeleted after each test
        //Xunit calls constructor before each test and Dispose after each test
        //in memory DB name - best to be unique - often Guid is used (Guid.NewGuid.ToString)

        //In Memory database jest trzymane w pamieci przez caly okres unit testow
        //tzn ze pierwszy unit test moze zaafektowac drugi


        public TaskRepositoryTests()
        {

        }
    }
}
