using PointService.BL.Interfaces;
using PointService.BL.Models;
using PointService.DataAccess.Interfaces;
using PointService.DataAccess.Models;
using System.Collections.Generic;

namespace PointService.BL
{
    public class PointManager : IPointManager
    {
        private readonly IUow _uow;

        public PointManager(IUow uow)
        {
            _uow = uow;
        }

        public PointHistoryClientsVM GetPointHistoryClients()
        {
            var result = new PointHistoryClientsVM();
            var clients = _uow.ClientEntity.GetAll();
            if (clients != null)
            {
                result.Clients = (List<Client>)clients;
            }

            return result;
        }
    }
}
