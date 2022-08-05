using Microsoft.EntityFrameworkCore;
using PointService.DataAccess.Models;
using System;

namespace Calefy.DataAccesLayer
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var clien1 = new Client { Id = Guid.NewGuid(), Name = "Ilya Bogomya", };
            var clien2 = new Client { Id = Guid.NewGuid(), Name = "Dmitriy Kavalenka", };
            var client3 = new Client { Id = Guid.NewGuid(), Name = "Karina Kashuba", };

            modelBuilder.Entity<Client>().HasData(
                new Client[]
                {
                    clien1,
                    clien2,
                    client3
                });

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction[]
                {
                    new Transaction { Id = Guid.NewGuid(), Client = clien1, DateCreated = DateTime.Now, Cost = 120 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien1,  DateCreated = DateTime.Now, Cost = 220 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien1,  DateCreated = DateTime.Now, Cost = 20 },

                    new Transaction { Id = Guid.NewGuid(), Client = clien1,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 420 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien1,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 720 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien1,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 10 },

                    new Transaction { Id = Guid.NewGuid(), Client = clien1, DateCreated = DateTime.Now.AddMonths(-2), Cost = 120 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien1, DateCreated = DateTime.Now.AddMonths(-2), Cost = 520 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien1, DateCreated = DateTime.Now.AddMonths(-2), Cost = 5 },


                    new Transaction { Id = Guid.NewGuid(), Client = clien2, DateCreated = DateTime.Now, Cost = 120 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien2,  DateCreated = DateTime.Now, Cost = 220 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien2,  DateCreated = DateTime.Now, Cost = 20 },

                    new Transaction { Id = Guid.NewGuid(), Client = clien2,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 420 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien2,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 720 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien2,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 10 },

                    new Transaction { Id = Guid.NewGuid(), Client = clien2, DateCreated = DateTime.Now.AddMonths(-2), Cost = 120 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien2, DateCreated = DateTime.Now.AddMonths(-2), Cost = 520 },
                    new Transaction { Id = Guid.NewGuid(), Client = clien2, DateCreated = DateTime.Now.AddMonths(-2), Cost = 5 },


                    new Transaction { Id = Guid.NewGuid(), Client = client3, DateCreated = DateTime.Now, Cost = 120 },
                    new Transaction { Id = Guid.NewGuid(), Client = client3,  DateCreated = DateTime.Now, Cost = 220 },
                    new Transaction { Id = Guid.NewGuid(), Client = client3,  DateCreated = DateTime.Now, Cost = 20 },

                    new Transaction { Id = Guid.NewGuid(), Client = client3,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 420 },
                    new Transaction { Id = Guid.NewGuid(), Client = client3,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 720 },
                    new Transaction { Id = Guid.NewGuid(), Client = client3,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 10 },

                    new Transaction { Id = Guid.NewGuid(), Client = client3, DateCreated = DateTime.Now.AddMonths(-2), Cost = 120 },
                    new Transaction { Id = Guid.NewGuid(), Client = client3, DateCreated = DateTime.Now.AddMonths(-2), Cost = 520 },
                    new Transaction { Id = Guid.NewGuid(), Client = client3, DateCreated = DateTime.Now.AddMonths(-2), Cost = 5 },

                });
        }
    }
}
