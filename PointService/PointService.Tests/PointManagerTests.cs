using AutoMapper;
using Moq;
using PointService.API;
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
    public class PointManagerTests
    {
        private readonly IMapper _mapper;
        public PointManagerTests()
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
            var result = pointManager.GetPointHistoryClients();

            var totalFirstMonth = result.Clients.First().TotalSumPointsMonths[DateTime.Now.AddMonths(-2).Month];
            var totalSecondMonth = result.Clients.First().TotalSumPointsMonths[DateTime.Now.AddMonths(-1).Month];
            var totalThirdMonth = result.Clients.First().TotalSumPointsMonths[DateTime.Now.Month];

            var totalAllMonth = totalFirstMonth + totalSecondMonth + totalThirdMonth;

            Assert.NotNull(result.Clients);
            Assert.Equal(980, totalFirstMonth);
            Assert.Equal(1980, totalSecondMonth);
            Assert.Equal(380, totalThirdMonth);
            Assert.Equal(3340, totalAllMonth);
        }
    }
}
