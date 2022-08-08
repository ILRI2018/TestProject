using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PointService.BL.Interfaces;
using System.Threading.Tasks;

namespace PointService.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPointManager _pointManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IPointManager pointManager)
        {
            _logger = logger;
            _pointManager = pointManager;
        }

        public IActionResult Index()
        {
            var result = _pointManager.GetPointHistoryClients();
            return View(result);
        }
    }
}
