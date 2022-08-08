using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PointService.BL.Interfaces;

namespace PointService.Web.Controllers
{
    public class PointController : Controller
    {
        private readonly IPointManager _pointManager;
        private readonly ILogger<PointController> _logger;

        public PointController(ILogger<PointController> logger, IPointManager pointManager)
        {
            _logger = logger;
            _pointManager = pointManager;
        }

        public IActionResult Index()
        {
            var result = _pointManager.GetPointHistoryClients();

            return result == null ? NotFound() : View(result);
        }
    }
}
