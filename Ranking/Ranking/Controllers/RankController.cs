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

            return View("RankView", _rankContext.Ranks.OrderByDescending(x => x.Score).ToList());
        }

        [HttpGet]
        public ActionResult Rank()
        {
            return Json(_rankContext.Ranks.OrderByDescending(x => x.Score).Take(10), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SendRank(string name, int score)
        {
            _rankContext.Ranks.Add(new Models.RankModel() { Name = name, Score = score });
            _rankContext.SaveChanges();
            return Json(new { Status = "OK", Name = name ,Score = score }, JsonRequestBehavior.AllowGet);
        }
    }
}