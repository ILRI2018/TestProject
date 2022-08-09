using Microsoft.AspNetCore.Mvc;
using PointService.BL.Interfaces;

namespace PointService.Web.Controllers
{
    public class PointController : Controller
    {
        private readonly IPointManager _pointManager;

        public PointController(IPointManager pointManager)
        {
            _pointManager = pointManager;
        }

        public IActionResult Index()
        {
            var result = _pointManager.GetPointHistoryClients();

            return result == null ? NotFound() : View(result);
        }
    }
}
