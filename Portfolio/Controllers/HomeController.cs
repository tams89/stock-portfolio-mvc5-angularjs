using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Services;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        #region Dependencies

        private IYahooFinanceService YahooFinanceService;

        public HomeController(IYahooFinanceService YahooFinanceService)
        {
            this.YahooFinanceService = YahooFinanceService;
        }

        #endregion Dependencies

        public ActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AutoComplete(string symbol)
        {
            var results = YahooFinanceService.SymbolSearch(symbol);
            return Json(results, JsonRequestBehavior.DenyGet);
        }
    }
}