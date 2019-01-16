using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerAPI.DAL.DAO;
using TaskTrackerAPI.Models;

namespace TaskTrackerAPI.DAL
{
    public static class DataInitializer
    {
        public static void Seed(this AppDbContext ctx)
        {
            if (!ctx.Tasks.Any())
            {
                ctx.Tasks.AddRange
                    (
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
                    );

                ctx.SaveChanges();
                
            }
        }
    
    }
}
