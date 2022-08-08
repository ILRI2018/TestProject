using Microsoft.AspNetCore.Mvc;
using PointService.BL.Interfaces;
using PointService.ViewModels;
using System.Threading.Tasks;

namespace PointService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PointController : ControllerBase
    {
        private readonly IPointManager _pointManager;
        public PointController(IPointManager pointManager) => _pointManager = pointManager;

        /// <summary>
        ///  Get point history clients 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PointHistoryClientsVM> PointHistoryClients()
        {
            return await _pointManager.GetPointHistoryClients();
        }
    }
}
