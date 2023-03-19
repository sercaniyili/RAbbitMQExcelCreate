using Microsoft.AspNetCore.Mvc;

namespace RabbitMQWeb.Excel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
