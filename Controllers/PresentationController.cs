using Microsoft.AspNetCore.Mvc;

namespace PresenterAPI.Controllers
{
    public class PresentationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
