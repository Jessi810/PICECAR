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
using PICECAR.Extension;

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

        public async Task<ActionResult> PersonalInfo()
        {
            PersonalInfo personalInfo = await db.PersonalInfos.FindAsync(User.Identity.GetUserId());
            if (personalInfo == null)
            {
                return HttpNotFound();
            }
            return View(personalInfo);
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
                TelNum = model.TelNum,
                Email = model.Email
            });
            PersonalInfo personalInfo = await db.PersonalInfos.FindAsync(user.Id);
            if (personalInfo == null)
            {
                // TODO: Change to bad request
                return View(model);
            }
            db.Entry(personalInfo).State = EntityState.Modified;
            await db.SaveChangesAsync();

            user.Email = model.Email;
            user.UserName = model.Email;
            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // TODO: Change to bad request
                return View();
            }

            return RedirectToAction("Index", "Profile");
        }

        public async Task<ActionResult> Membership()
        {
            MembershipInfo memInfo = await db.MembershipInfos.FindAsync(User.Identity.GetUserId());
            if (memInfo == null)
            {
                return HttpNotFound();
            }
            return View(memInfo);
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

        public ActionResult Profession()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Profession(Profession model)
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

            db.Professions.Add(new Profession
            {
                Id = user.Id,
                EmploymentSector = model.EmploymentSector,
                OtherEmploymentSector = model.OtherEmploymentSector,
                AreaOfPractice = model.AreaOfPractice,
                OtherAreaOfPractice = model.OtherAreaOfPractice,
                CurrentCompany = model.CurrentCompany,
                EmployeeType = model.EmployeeType,
                CurrentJobPosition = model.CurrentJobPosition,
                WorkAddress = model.WorkAddress,
                CompanyTelNum = model.CompanyTelNum,
                CompanyFaxNum = model.CompanyFaxNum,
                CompanyEmail = model.CompanyEmail
            });
            Profession profession = await db.Professions.FindAsync(user.Id);
            if (profession == null)
            {
                // TODO: Change to bad request
                return View(model);
            }
            db.Entry(profession).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Profile");
        }

        public async Task<ActionResult> Education()
        {
            string userId = User.Identity.GetUserId();
            if (String.IsNullOrEmpty(userId))
            {
                // TODO: Replace with bad request
                return View();
            }
            var education = db.Educations.Include(p => p.User).Where(p => p.User.Id == userId);
            return View(await education.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Education(Education model)
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

            db.Educations.Add(new Education
            {
                User = user,
                Course = model.Course,
                School = model.School,
                YearGraduated = model.YearGraduated
            });
            Education education = await db.Educations.FindAsync(user.Id);
            if (education == null)
            {
                // TODO: Change to bad request
                return View(model);
            }
            db.Entry(education).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Profile");
        }

        public async Task<ActionResult> EditEducation(int id)
        {
            // TODO: Add validation
            var education = await db.Educations.FindAsync(id);
            return View(education);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEducation([Bind(Include = "EducationId,Course,School,YearGraduated")] Education model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Education", "Profile");
        }

        public async Task<ActionResult> DeleteEducation(int id)
        {
            // TODO: Add validation
            var education = await db.Educations.FindAsync(id);
            return View(education);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteEducationConfirmed(int id)
        {
            // TODO: Add validation
            Education education = await db.Educations.FindAsync(id);
            db.Educations.Remove(education);
            await db.SaveChangesAsync();
            return RedirectToAction("Education", "Profile");
        }

        public ActionResult AddEducation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEducation([Bind(Include = "EducationId,Course,School,YearGraduated")] Education model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                // TODO: Change to bad request
                return View();
            }
            var education = new Education
            {
                Id = user.Id,
                Course = model.Course,
                School = model.School,
                YearGraduated = model.YearGraduated
            };
            
            db.Educations.Add(education);
            await db.SaveChangesAsync();
            return RedirectToAction("Education", "Profile");
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
