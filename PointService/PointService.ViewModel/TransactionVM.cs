using System;

namespace PointService.ViewModels
{
    public class TransactionVM
    {
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid ClientId { get; set; }
    }
}
