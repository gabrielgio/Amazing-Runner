using Ranking.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ranking.Controllers
{
    public class RankController : Controller
    {
        private RankingContext _rankContext = new RankingContext();

        // GET: Rank
        public ActionResult Index()
        {
            
            return View("RankView",_rankContext.Ranks.ToList());
        }

        [HttpGet]
        public ActionResult Rank()
        {
            return Json(_rankContext.Ranks.Take(10), JsonRequestBehavior.AllowGet);
        }
    }
}