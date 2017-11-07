using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PICECAR.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace PICECAR.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ProfileController()
        {
        }

        public ProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        public ActionResult PersonalInfo()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PersonalInfo(PersonalInfo model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View(model);
            }

            db.PersonalInfos.Add(new PersonalInfo
            {
                Id = user.Id,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                PlaceOfBirth = model.PlaceOfBirth,
                HomeAddress = model.HomeAddress,
                BaguioAddress = model.BaguioAddress,
                CellNum = model.CellNum,
                TelNum = model.TelNum
            });
            PersonalInfo personalInfo = await db.PersonalInfos.FindAsync(user.Id);
            if (personalInfo == null)
            {
                // TODO: Change to bad request
                return View(model);
            }
            db.Entry(personalInfo).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Profile");
        }

        public ActionResult Membership()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Membership(MembershipInfo model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View(model);
            }

            db.MembershipInfos.Add(new MembershipInfo
            {
                Id = user.Id,
                PrcNum = model.PrcNum,
                PrcDateIssued = model.PrcDateIssued,
                TypeOfMembership = model.TypeOfMembership,
                MembershipNum = model.MembershipNum,
                DateOfMembership = model.DateOfMembership
            });
            MembershipInfo membershipInfo = await db.MembershipInfos.FindAsync(user.Id);
            if (membershipInfo == null)
            {
                // TODO: Change to bad request
                return View(model);
            }
            db.Entry(membershipInfo).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Profile");
        }

        // GET: Profile
        public async Task<ActionResult> Index()
        {
            var personalInfos = db.PersonalInfos.Include(p => p.User);
            return View(await personalInfos.ToListAsync());
        }

        // GET: Profile/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = await db.PersonalInfos.FindAsync(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            return View(personalInfo);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.PersonalInfos, "Id", "Email");
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,MiddleName,LastName,PlaceOfBirth,BirthDate,HomeAddress,BaguioAddress,TelNum,CellNum")] PersonalInfo personalInfo)
        {
            if (ModelState.IsValid)
            {
                db.PersonalInfos.Add(personalInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.PersonalInfos, "Id", "Email", personalInfo.Id);
            return View(personalInfo);
        }

        // GET: Profile/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = await db.PersonalInfos.FindAsync(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.PersonalInfos, "Id", "Email", personalInfo.Id);
            return View(personalInfo);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,MiddleName,LastName,PlaceOfBirth,BirthDate,HomeAddress,BaguioAddress,TelNum,CellNum")] PersonalInfo personalInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.PersonalInfos, "Id", "Email", personalInfo.Id);
            return View(personalInfo);
        }

        // GET: Profile/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInfo personalInfo = await db.PersonalInfos.FindAsync(id);
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            return View(personalInfo);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            PersonalInfo personalInfo = await db.PersonalInfos.FindAsync(id);
            db.PersonalInfos.Remove(personalInfo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
