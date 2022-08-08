
using AutoMapper;
using Moq;
using PointService.API;
using PointService.API.Controllers;
using PointService.BL;
using PointService.BL.Interfaces;
using PointService.DataAccess.Interfaces;
using PointService.DataAccess.Models;
using PointService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PointService.Tests
{
    public class PointControllerTests
    {
        private readonly IMapper _mapper;

        public PointControllerTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public void GetPointHistoryClients()
        {
            var client = new Client { Id = Guid.NewGuid(), Name = "Ilya Bogomya" };

            var clientData = new List<Transaction>
                {
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id, DateCreated = DateTime.Now, Cost = 120 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id,  DateCreated = DateTime.Now, Cost = 220 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id,  DateCreated = DateTime.Now, Cost = 20 },

                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 420 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 720 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 10 },

                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id, DateCreated = DateTime.Now.AddMonths(-2), Cost = 120 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id, DateCreated = DateTime.Now.AddMonths(-2), Cost = 520 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id, DateCreated = DateTime.Now.AddMonths(-2), Cost = 5 },
             };

            client.Transactions = clientData;

            var clients = new Client[] { client };

            var _mockUow = new Mock<IUow>();
            _mockUow.Setup(x => x.ClientEntity.GetQueryable()).Returns(clients.AsQueryable());

            var _mockLogger = new Mock<ILoggerManager>();

            PointManager pointManager = new PointManager(_mockUow.Object, _mapper, _mockLogger.Object);
            PointController pointController = new PointController(pointManager);

            PointHistoryClientsVM result = pointController.PointHistoryClients();

            Assert.NotNull(result.Clients);
            Assert.True(result.Clients.Any());
            Assert.True(client.Name == result.Clients.First().Name);
            Assert.True(client.Id == result.Clients.First().Id);
            Assert.Equal(9, result.Clients.First().Transactions.Count);

        }
    }
}
