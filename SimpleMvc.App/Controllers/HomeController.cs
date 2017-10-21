namespace SimpleMvc.App.Controllers
{
    using SimpleMcv.Framework.Controllers;
    using SimpleMcv.Framework.Interfaces;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
