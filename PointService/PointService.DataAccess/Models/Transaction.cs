using System;

namespace PointService.DataAccess.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateCreated { get; set; }
        public Client Client { get; set; }
    }
}
