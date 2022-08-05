using System;
using System.Collections.Generic;

namespace PointService.DataAccess.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
