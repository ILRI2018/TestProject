using Microsoft.AspNetCore.Mvc;
using PointService.BL.Interfaces;
using PointService.BL.Models;

namespace PointService.API.Controllers
{

    [Route("api/[controller]/[action]")]
    public class PointController : ControllerBase
    {
        private readonly IPointManager _pointManager;
        public PointController(IPointManager pointManager)
        {
            _pointManager = pointManager;
        }

        [HttpGet]
        public PointHistoryClientsVM Index()
        {
            return _pointManager.GetPointHistoryClients();
        }
    }
}
