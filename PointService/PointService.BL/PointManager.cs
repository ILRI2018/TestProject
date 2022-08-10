using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PointService.BL.Interfaces;
using PointService.DataAccess.Interfaces;
using PointService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointService.BL
{
    public class PointManager : IPointManager
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        private const decimal FiftyDollars = 50;
        private const decimal OneHundredDollars = 100;
        private const int One = 1;

        public PointManager(IUow uow, IMapper mapper, ILoggerManager logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public PointHistoryClientsVM GetPointHistoryClients()
        {
            var result = new PointHistoryClientsVM();

            try
            {
                _logger.LogInfo("process point history");
                var clients =   _uow.ClientEntity.GetQueryable()
                    .Include(x => x.Transactions)
                    .ToList();

                var clientVM = _mapper.Map<List<ClientVM>>(clients);
                var total = decimal.Zero;
                foreach (var client in clientVM)
                {
                    foreach (var month in client.Transactions.GroupBy(x => x.DateCreated.Month).OrderBy(x=> x.Key).Select(x => x.Key))
                    {
                        var totalTransactions = client.Transactions.Where(x => x.DateCreated.Month == month).Sum(x => GetPoints(x.Cost));
                        client.TotalSumPointsMonths.Add(month, totalTransactions);
                    }
                    total = client.TotalSumPointsMonths.Sum(x => x.Value);
                    client.OverTotalPointsForThreeMonth = total;

                    total = decimal.Zero;
                }
                result.Clients = clientVM;

                decimal GetPoints(decimal costTransaction)
                {
                    var resultPoint = decimal.Zero;

                    if (costTransaction > FiftyDollars)
                    {
                        resultPoint += (costTransaction - FiftyDollars) * One;
                    }

                    if (costTransaction > OneHundredDollars)
                    {
                        resultPoint += (costTransaction - OneHundredDollars) * One;
                    }

                    return resultPoint;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("process point history failed" + ex.Message);
            }

            return result;
        }
    }
}
