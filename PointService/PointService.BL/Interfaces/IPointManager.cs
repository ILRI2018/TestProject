using PointService.BL.Models;
using System.Threading.Tasks;

namespace PointService.BL.Interfaces
{
    public interface IPointManager
    {
        PointHistoryClientsVM GetPointHistoryClients();
    }
}
