using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Core.Services.Interfaces;

namespace Portfolio.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}