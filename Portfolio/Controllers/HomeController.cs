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

        public HomeController(IYahooFinanceService yahooFinanceService)
        {
            YahooFinanceService = yahooFinanceService;
        }

        #endregion Dependencies

        public ActionResult Main()
        {
            return View();
        }

        public JsonResult AutoComplete(string symbol)
        {
            var results = YahooFinanceService.SymbolSearch(symbol).ToArray();
            if (!results.Any()) return null;

            var json = results.Select(x => new { x.Name, x.Symbol });
            //var json = results.SelectMany(x => new[] { x.Name });
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}