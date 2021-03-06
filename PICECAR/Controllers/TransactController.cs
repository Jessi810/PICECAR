﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PICECAR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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

            Seminar seminar = new Seminar
            {
                Id = User.Identity.GetUserId(),
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

            return RedirectToAction("AddSeminar");
        }

        public ActionResult AddPaymentOfDue()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddPaymentOfDue([Bind(Include = "PaymentOfDuesId,Id,InclusiveYearFrom,InclusiveYearTo,LifeMemberPayment,PaymentDate,PaymentAmount,OrNum")] PaymentOfDues model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var item in errors)
                {
                    Debug.WriteLine(item.ErrorMessage);
                }
                return View(model);
            }

            PaymentOfDues paymentOfDues = new PaymentOfDues
            {
                Id = User.Identity.GetUserId(),
                InclusiveYearFrom = model.InclusiveYearFrom,
                InclusiveYearTo = model.InclusiveYearTo,
                LifeMemberPayment = model.LifeMemberPayment,
                PaymentDate = model.PaymentDate,
                PaymentAmount = model.PaymentAmount,
                OrNum = model.OrNum
            };
            db.PaymentOfDues.Add(paymentOfDues);
            await db.SaveChangesAsync();

            return RedirectToAction("AddPaymentOfDue");
        }

        public ActionResult EditMembership()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditMembership(MembershipStatus model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Id = User.Identity.GetUserId();
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("EditMembership");
        }

        public ActionResult EditChapter()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditChapter(Chapter model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Id = User.Identity.GetUserId();
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("EditChapter");
        }
    }
}