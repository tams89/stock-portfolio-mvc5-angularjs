using System.Linq;
using System.Web.Mvc;
using Core.Services.Interfaces;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        #region Dependencies

        private readonly IYahooFinanceService _yahooFinanceService;

        public HomeController(IYahooFinanceService yahooFinanceService)
        {
            _yahooFinanceService = yahooFinanceService;
        }

        #endregion Dependencies

        public ActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AutoComplete(string symbol)
        {
            var results = _yahooFinanceService.SymbolSearch(symbol).ToArray();
            if (!results.Any()) return null;

            var json = results.Select(x => new { x.Name, x.Symbol });
            return Json(json);
        }

        [HttpPost]
        public JsonResult MarketData(string symbol)
        {
            var marketData = _yahooFinanceService.GetData(symbol, null, null);
            return Json(marketData);
        }
    }
}