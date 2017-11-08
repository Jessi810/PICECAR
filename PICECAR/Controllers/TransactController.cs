using PICECAR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PICECAR.Controllers
{
    [Authorize]
    public class TransactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transact
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("Seminar")]
        public ActionResult AddSeminar()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Seminar")]
        public ActionResult AddSeminar(Seminar model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}