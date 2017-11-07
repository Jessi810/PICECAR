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

namespace PICECAR.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
