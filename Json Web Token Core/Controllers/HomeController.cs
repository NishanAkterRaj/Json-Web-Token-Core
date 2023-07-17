using Microsoft.AspNetCore.Mvc;

namespace Json_Web_Token_Core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
