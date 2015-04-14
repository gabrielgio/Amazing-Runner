using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ranking.Controllers
{
    public class HostController : Controller
    {
        // GET: Host
        public ActionResult Index()
        {
            return View("HostView");
        }
    }
}