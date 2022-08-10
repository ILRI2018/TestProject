using AutoMapper;
using Moq;
using PointService.API;
using PointService.BL;
using PointService.BL.Interfaces;
using PointService.DataAccess.Interfaces;
using PointService.DataAccess.Models;
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
        public void GetPointHistoryClient()
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

        [Fact]
        public void GetPointHistoryClients()
        {
            var client = new Client { Id = Guid.NewGuid(), Name = "Ilya Bogomya" };
            var client2 = new Client { Id = Guid.NewGuid(), Name = "Dmitriy Kavalenka", };

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

            var clientData2 = new List<Transaction>
                {
                    new Transaction { Id = Guid.NewGuid(), ClientId = client2.Id, DateCreated = DateTime.Now, Cost = 120 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client2.Id,  DateCreated = DateTime.Now, Cost = 830 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client2.Id,  DateCreated = DateTime.Now, Cost = 20 },

                    new Transaction { Id = Guid.NewGuid(), ClientId = client2.Id,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 420 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client2.Id,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 740 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client2.Id,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 10 },

                    new Transaction { Id = Guid.NewGuid(), ClientId = client2.Id, DateCreated = DateTime.Now.AddMonths(-2), Cost = 120 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client2.Id, DateCreated = DateTime.Now.AddMonths(-2), Cost = 520 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client2.Id, DateCreated = DateTime.Now.AddMonths(-2), Cost = 5 },
             };


            client.Transactions = clientData;
            client2.Transactions = clientData2;

            var clients = new Client[] { client, client2 };

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

            var totalFirstMonthClient2 = result.Clients.Last().TotalSumPointsMonths[DateTime.Now.AddMonths(-2).Month];
            var totalSecondMonthClient2 = result.Clients.Last().TotalSumPointsMonths[DateTime.Now.AddMonths(-1).Month];
            var totalThirdMonthClient2 = result.Clients.Last().TotalSumPointsMonths[DateTime.Now.Month];

            var totalAllMonthClient2 = totalFirstMonthClient2 + totalSecondMonthClient2 + totalThirdMonthClient2;

            Assert.Equal(980, totalFirstMonthClient2);
            Assert.Equal(2020, totalSecondMonthClient2);
            Assert.Equal(1600, totalThirdMonthClient2);
            Assert.Equal(4600, totalAllMonthClient2);

            Assert.NotEqual(result.Clients.First().Id, result.Clients.Last().Id);
            Assert.NotEqual(result.Clients.First().Name, result.Clients.Last().Name);
        }

        [Fact]
        public void GetPointHistoryClientWithAnotherData()
        {
            var client = new Client { Id = Guid.NewGuid(), Name = "Ilya Bogomya" };

            var clientData = new List<Transaction>
                {
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id, DateCreated = DateTime.Now, Cost = 0.5M },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id,  DateCreated = DateTime.Now, Cost = 220 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id,  DateCreated = DateTime.Now, Cost = 20 },

                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 420 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 101.82M },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id,  DateCreated = DateTime.Now.AddMonths(-1), Cost = 10 },

                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id, DateCreated = DateTime.Now.AddMonths(-2), Cost = 120 },
                    new Transaction { Id = Guid.NewGuid(), ClientId = client.Id, DateCreated = DateTime.Now.AddMonths(-2), Cost = 7.67M },
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
            Assert.Equal(90, totalFirstMonth);
            Assert.Equal(743.64M, totalSecondMonth);
            Assert.Equal(290, totalThirdMonth);
            Assert.Equal(1123.64M, totalAllMonth);
        }

        [Fact]
        public void GetOnePoint()
        {
            var client = new Client { Id = Guid.NewGuid(), Name = "Ilya Bogomya" };

            var clientData = new List<Transaction> { new Transaction { Id = Guid.NewGuid(), ClientId = client.Id, DateCreated = DateTime.Now, Cost = 50.1M }};
            client.Transactions = clientData;

            var clients = new Client[] { client };

            var _mockUow = new Mock<IUow>();
            _mockUow.Setup(x => x.ClientEntity.GetQueryable()).Returns(clients.AsQueryable());

            var _mockLogger = new Mock<ILoggerManager>();

            PointManager pointManager = new PointManager(_mockUow.Object, _mapper, _mockLogger.Object);
            var result = pointManager.GetPointHistoryClients();
           
            Assert.Equal(0.1M, result.Clients.First().TotalSumPointsMonths[DateTime.Now.Month]);
        }

        [Fact]
        public void GetOnePointAnotherData()
        {
            var client = new Client { Id = Guid.NewGuid(), Name = "Ilya Bogomya" };

            var clientData = new List<Transaction> { new Transaction { Id = Guid.NewGuid(), ClientId = client.Id, DateCreated = DateTime.Now, Cost = 51 } };
            client.Transactions = clientData;

            var clients = new Client[] { client };

            var _mockUow = new Mock<IUow>();
            _mockUow.Setup(x => x.ClientEntity.GetQueryable()).Returns(clients.AsQueryable());

            var _mockLogger = new Mock<ILoggerManager>();

            PointManager pointManager = new PointManager(_mockUow.Object, _mapper, _mockLogger.Object);
            var result = pointManager.GetPointHistoryClients();

            Assert.Equal(1M, result.Clients.First().TotalSumPointsMonths[DateTime.Now.Month]);
        }

        [Fact]
        public void GetZeroPoint()
        {
            var client = new Client { Id = Guid.NewGuid(), Name = "Ilya Bogomya" };

            var clientData = new List<Transaction> { new Transaction { Id = Guid.NewGuid(), ClientId = client.Id, DateCreated = DateTime.Now, Cost = 49.9M } };
            client.Transactions = clientData;

            var clients = new Client[] { client };

            var _mockUow = new Mock<IUow>();
            _mockUow.Setup(x => x.ClientEntity.GetQueryable()).Returns(clients.AsQueryable());

            var _mockLogger = new Mock<ILoggerManager>();

            PointManager pointManager = new PointManager(_mockUow.Object, _mapper, _mockLogger.Object);
            var result = pointManager.GetPointHistoryClients();

            Assert.Equal(decimal.Zero, result.Clients.First().TotalSumPointsMonths[DateTime.Now.Month]);
        }

        [Fact]
        public void GetTwoPoints()
        {
            var client = new Client { Id = Guid.NewGuid(), Name = "Ilya Bogomya" };

            var clientData = new List<Transaction> { new Transaction { Id = Guid.NewGuid(), ClientId = client.Id, DateCreated = DateTime.Now, Cost = 101 } };
            client.Transactions = clientData;

            var clients = new Client[] { client };

            var _mockUow = new Mock<IUow>();
            _mockUow.Setup(x => x.ClientEntity.GetQueryable()).Returns(clients.AsQueryable());

            var _mockLogger = new Mock<ILoggerManager>();

            PointManager pointManager = new PointManager(_mockUow.Object, _mapper, _mockLogger.Object);
            var result = pointManager.GetPointHistoryClients();

            Assert.Equal(52, result.Clients.First().TotalSumPointsMonths[DateTime.Now.Month]);
        }
    }
}
