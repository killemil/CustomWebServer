namespace SimpleMvc.App.Controllers
{
    using SimpleMcv.Framework.Attributes.Methods;
    using SimpleMcv.Framework.Controllers;
    using SimpleMcv.Framework.Interfaces;

    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
