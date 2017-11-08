using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PICECAR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PICECAR.Controllers
{
    [Authorize]
    public class TransactController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public TransactController()
        {
        }

        public TransactController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Transact
        public ActionResult Index()
        {
            return RedirectToAction("AddSeminar");
        }

        public ActionResult AddSeminar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddSeminar(Seminar model)
        {
            // TODO: Add validation
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            string userId = User.Identity.GetUserId();
            Seminar seminar = new Seminar
            {
                Id = userId,
                Code = model.Code,
                Title = model.Title,
                Topic = model.Topic,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                Hours = model.Hours,
                CpdUnitsEarned = model.CpdUnitsEarned
            };
            db.Seminars.Add(seminar);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public ActionResult AddPaymentOfDue()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddPaymentOfDue(PaymentOfDues model)
        {
            // TODO: Add validation
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            string userId = User.Identity.GetUserId();
            PaymentOfDues pod = new PaymentOfDues
            {
                Id = userId,
                InclusiveYearFrom = model.InclusiveYearFrom,
                InclusiveYearTo = model.InclusiveYearTo,
                LifeMemberPayment = model.LifeMemberPayment,
                PaymentDate = model.PaymentDate,
                PaymentAmount = model.PaymentAmount,
                OrNum = model.OrNum
            };
            db.PaymentOfDues.Add(pod);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public ActionResult EditMembership()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditMembership(MembershipStatus model)
        {
            // TODO: Add validation
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            MembershipStatus memstat = new MembershipStatus
            {
                Id = user.Id,
                CurrentStatus = user.MembershipInfo.TypeOfMembership.ToString(),
                NewStatus = model.NewStatus
            };
            db.MembershipStatuses.Add(memstat);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public ActionResult EditChapter()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditChapter(Chapter model)
        {
            // TODO: Add validation
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            Chapter chapter = new Chapter
            {
                Id = user.Id,
                CurrentChapter = model.CurrentChapter,  // TODO: Change to get user's chapter
                NewChapter = model.NewChapter
            };
            db.Chapters.Add(chapter);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}