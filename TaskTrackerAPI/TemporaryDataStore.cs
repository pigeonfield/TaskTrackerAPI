using TaskTrackerAPI.DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI
{
    public class TemporaryDataStore
    {
        public static TemporaryDataStore DummyData { get; } = new TemporaryDataStore();

        public List<Category> Categories { get; set; }
               
        public List<TaskModel> Tasks { get; set; }

        public TemporaryDataStore()
        {
            Categories = new List<Category>()
            {
                new Category()
                {
                    CategoryId = 1,
                    Name = "Work",
                    Description = "Lorem ipsum, lorem ipsum and lorem ipsum"
                },

                new Category()
                {
                    CategoryId = 2,
                    Name = "Private",
                    Description = "Lorem ipsum, lorem ipsum and lorem ipsum"
                }
            };

            Tasks = new List<TaskModel>()
            {
                new TaskModel()
                {
                    TaskId = 1,
                    Name = "Concert",
                    ShortDescription = "Lorem Ipsum",
                    LongDescription = "Lorem ipsum and lorem ipsum",
                    Date = $"{DateTime.Now:yyyy-MM-dd}",
                    Priority = 1,
                    IsDone = false,
                    CategoryId = 2,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            CommentId = 1,
                            Content = "Central Park lorem ipsum Central park",
                            TaskId = 1
                        },
                        new Comment()
                        {
                            CommentId = 2,
                            Content = "Central Park lorem ipsum Central park ipsum Central park",
                            TaskId = 1
                        },
                    }


                },

                new TaskModel()
                {
                    TaskId = 2,
                    Name = "Appointment",
                    ShortDescription = "Lorem Ipsum",
                    LongDescription = "Lorem ipsum and lorem ipsum. Central Park lorem ipsum Central park ipsum Central park, Central Park lorem, Central Park lorem,Central Park lorem, Central Park lorem",
                    Date = $"{DateTime.Now:yyyy-MM-dd}",
                    Priority = 3,
                    IsDone = true,

                    CategoryId = 1,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            CommentId = 1,
                            Content = "Central lorem ipsum",
                            TaskId = 2
                        },
                        new Comment()
                        {
                            CommentId = 2,
                            Content = "Central Park lorem ipsum Central park ipsum Central park, Central Park lorem, Central Park lorem,Central Park lorem, Central Park lorem",
                            TaskId = 2
                        },
                        new Comment()
                        {
                            CommentId = 3,
                            Content = "Central Park lorem Central Park lorem ipsum Central park ipsum Central park",
                            TaskId = 2
                        },
                    }
                },

                new TaskModel()
                {
                    TaskId = 3,
                    Name = "Meeting",
                    ShortDescription = "Lorem Ipsum",
                    LongDescription = "Lorem ipsum and lorem ipsum",
                    Date = $"{DateTime.Now:yyyy-MM-dd}",
                    Priority = 2,
                    IsDone = false,

                    CategoryId = 2,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            CommentId = 1,
                            Content = "Central Park ",
                            TaskId = 3
                        },
                        new Comment()
                        {
                            CommentId = 2,
                            Content = "Central Park lorem ipsum Central park ipsum Central park",
                            TaskId = 3
                        },
                        new Comment()
                        {
                            CommentId = 3,
                            Content = "Central Park lorem ipsum Central park ipsum Central park, Central Park lorem ipsum Central park ipsum Central park",
                            TaskId = 3
                        },
                    }
                },

                new TaskModel()
                {
                    TaskId = 4,
                    Name = "Toilet paper",
                    ShortDescription = "Lorem Ipsum",
                    LongDescription = "Lorem ipsum and lorem ipsum. Central Park lorem ipsum Central park ipsum Central park, Central Park lorem, Central Park lorem,Central Park lorem, Central Park lorem.Central Park lorem ipsum Central park ipsum Central park, Central Park lorem, Central Park lorem,Central Park lorem, Central Park lorem",
                    Date = $"{DateTime.Now:yyyy-MM-dd}",
                    Priority = 2,
                    IsDone = true,

                    CategoryId = 2,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            CommentId = 1,
                            Content = "Central Park ",
                            TaskId = 3
                        },
                        new Comment()
                        {
                            CommentId = 2,
                            Content = "Central Park lorem ipsum Central park ipsum Central park",
                            TaskId = 3
                        },
                        new Comment()
                        {
                            CommentId = 3,
                            Content = "Central Park lorem ipsum Central park ipsum Central park, Central Park lorem ipsum Central park ipsum Central park",
                            TaskId = 3
                        },
                    }
                },

                new TaskModel()
                {
                    TaskId = 5,
                    Name = "Onion smell",
                    ShortDescription = "Lorem Ipsum",
                    LongDescription = "Lorem ipsum and lorem ipsum",
                    Date = $"{DateTime.Now:yyyy-MM-dd}",
                    Priority = 2,
                    IsDone = false,

                    CategoryId = 2,
                    Comments = new List<Comment>()

                },

                new TaskModel()
                {
                    TaskId = 6,
                    Name = "Unicorns",
                    ShortDescription = "Lorem Ipsum",
                    LongDescription = "Lorem ipsum and lorem ipsum",
                    Date = $"{DateTime.Now:yyyy-MM-dd}",
                    Priority = 2,
                    IsDone = false,

                    CategoryId = 2,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            CommentId = 1,
                            Content = "Central Park ",
                            TaskId = 3
                        },
                        new Comment()
                        {
                            CommentId = 2,
                            Content = "Central Park lorem ipsum Central park ipsum Central park",
                            TaskId = 3
                        },

                    }
                },
            };
        }

    }
}

