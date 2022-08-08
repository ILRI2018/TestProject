using System;
using System.Collections.Generic;

namespace PointService.ViewModels
{
    public class ClientVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TransactionVM> Transactions { get; set; } = new List<TransactionVM>();
        public Dictionary<int, decimal> TotalSumPointsMonths { get; set; } = new Dictionary<int, decimal>();
        public decimal OverTotalPointsForThreeMonth { get; set; }
    }
}
