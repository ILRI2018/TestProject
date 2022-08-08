using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PointService.BL.Interfaces;
using PointService.DataAccess.Interfaces;
using PointService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

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

        public async Task<PointHistoryClientsVM> GetPointHistoryClients()
        {
            var result = new PointHistoryClientsVM();

            try
            {
                _logger.LogInfo("process point history");
                var clients = await _uow.ClientEntity.GetQueryable()
                    .Include(x => x.Transactions)
                    .ToListAsync();

                var clientVM = result.Clients = _mapper.Map<List<ClientVM>>(clients);
                var summ = decimal.Zero;
                foreach (var client in clientVM)
                {
                    foreach (var month in client.Transactions.GroupBy(x => x.DateCreated.Month).Select(x => x.Key))
                    {
                        var totalTransactions = client.Transactions.Where(x => x.DateCreated.Month == month).Sum(x => GetPoints(x.Cost));
                        client.TotalSumPointsMonths.Add(month, totalTransactions);
                    }

                    foreach (var ff in client.TotalSumPointsMonths)
                    {
                        summ += ff.Value;
                    }

                    client.OverTotalPointsForThreeMonth = summ;
                    summ = decimal.Zero;

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
            }
            catch (Exception ex)
            {
                _logger.LogError("process point history failed" + ex.Message);
            }

            return result;
        }
    }
}
