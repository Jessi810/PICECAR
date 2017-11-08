﻿using Microsoft.AspNet.Identity;
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
            return View();
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
            //if (sem == null)
            //{

            //}
            //else
            //{
            //    db.Entry(seminar).State = EntityState.Modified;
            //    await db.SaveChangesAsync();
            //}

            return RedirectToAction("Index");
        }
    }
}