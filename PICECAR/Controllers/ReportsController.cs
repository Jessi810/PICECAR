using PICECAR.Extension;
using PICECAR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PICECAR.Controllers
{
    [Authorize(Roles = "Administrator,Secretary")]
    public class ReportsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reports
        public ActionResult Index()
        {
            return RedirectToAction("ActiveMember");
        }

        public ActionResult ActiveMember()
        {
            ActiveMember active = new ActiveMember();
            active.MembershipTypeItems = new List<MembershipTypeProp>();
            active.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.Regular, IsSelected = false });
            active.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.Life, IsSelected = false });
            active.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.Student, IsSelected = false });
            active.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.Honorary, IsSelected = false });
            active.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.Fellow, IsSelected = false });
            active.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.All, IsSelected = false });

            active.EmploymentSectorItems = new List<EmploymentSectorProp>();
            active.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.Government, IsSelected = false });
            active.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.Private, IsSelected = false });
            active.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.OCW, IsSelected = false });
            active.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.Others, IsSelected = false });
            active.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.All, IsSelected = false });

            active.AreaOfPracticeItems = new List<AreaOfPracticeProp>();
            active.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.Construction, IsSelected = false });
            active.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.Design, IsSelected = false });
            active.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.Academy, IsSelected = false });
            active.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.Commercial, IsSelected = false });
            active.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.Others, IsSelected = false });
            active.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.All, IsSelected = false });

            return View(active);
        }

        [HttpPost]
        public ActionResult ActiveMember(ActiveMember model)
        {
            var users1 = new List<ApplicationUser>();
            var users2 = new List<ApplicationUser>();
            var users3 = new List<ApplicationUser>();
            var users4 = new List<ApplicationUser>();
            foreach (var user in db.Users)
            {
                if (user.LastActive.Subtract(model.Date).Days > -1 && user.LastActive.Subtract(model.Date).Days < 30)
                {
                    users1.Add(user);
                    Debug.WriteLine(user.LastActive.Subtract(model.Date).Days + " | " + user.Email);
                }
            }

            if (users1.Count() == 0)
            {
                ModelState.AddModelError("NoActive", "No active members found.");
                return View("ActiveMember", model);
            }

            var selectedTypes = model.MembershipTypeItems.Where(p => p.IsSelected);
            if (selectedTypes.Count() == 0 || selectedTypes.Any(p => p.MembershipType == EnumData.MembershipType.All))
            {
                users2.AddRange(users1);
            }
            else
            {
                foreach (var user in users1)
                {
                    foreach (var type in selectedTypes)
                    {
                        if (user.MembershipInfo.TypeOfMembership == type.MembershipType)
                        {
                            users2.Add(user);
                            break;
                        }
                    }
                }
            }



            var selectedSectors = model.EmploymentSectorItems.Where(p => p.IsSelected);
            if (selectedSectors.Count() == 0 || selectedSectors.Any(p => p.EmploymentSector == EnumData.EmploymentSector.All))
            {
                users3.AddRange(users2);
            }
            else
            {
                foreach (var user in users2)
                {
                    foreach (var sector in selectedSectors)
                    {
                        if (user.Profession.EmploymentSector == sector.EmploymentSector)
                        {
                            users3.Add(user);
                            break;
                        }
                    }
                }
            }



            var selectedPractices = model.AreaOfPracticeItems.Where(p => p.IsSelected);
            if (selectedPractices.Count() == 0 || selectedPractices.Any(p => p.AreaOfPractice == EnumData.AreaOfPractice.All))
            {
                users4.AddRange(users3);
            }
            else
            {
                foreach (var user in users3)
                {
                    foreach (var practice in selectedPractices)
                    {
                        if (user.Profession.AreaOfPractice == practice.AreaOfPractice)
                        {
                            users4.Add(user);
                            break;
                        }
                    }
                }
            }

            if (users4.Count() == 0)
            {
                ModelState.AddModelError("NoActive", "No active members found.");
                return View("ActiveMember", model);
            }
            TempData["FilteredMembers"] = users4;

            return RedirectToAction("GenerateActiveMember");
        }

        public ActionResult GenerateActiveMember()
        {
            ViewData["FilteredMembers"] = (List<ApplicationUser>) TempData["FilteredMembers"];

            return View();
        }

        public ActionResult UnpaidMember()
        {
            UnpaidMember unpaid = new UnpaidMember();
            unpaid.MembershipTypeItems = new List<MembershipTypeProp>();
            unpaid.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.Regular, IsSelected = false });
            unpaid.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.Life, IsSelected = false });
            unpaid.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.Student, IsSelected = false });
            unpaid.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.Honorary, IsSelected = false });
            unpaid.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.Fellow, IsSelected = false });
            unpaid.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.All, IsSelected = false });

            unpaid.EmploymentSectorItems = new List<EmploymentSectorProp>();
            unpaid.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.Government, IsSelected = false });
            unpaid.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.Private, IsSelected = false });
            unpaid.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.OCW, IsSelected = false });
            unpaid.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.Others, IsSelected = false });
            unpaid.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.All, IsSelected = false });

            unpaid.AreaOfPracticeItems = new List<AreaOfPracticeProp>();
            unpaid.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.Construction, IsSelected = false });
            unpaid.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.Design, IsSelected = false });
            unpaid.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.Academy, IsSelected = false });
            unpaid.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.Commercial, IsSelected = false });
            unpaid.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.Others, IsSelected = false });
            unpaid.AreaOfPracticeItems.Add(new AreaOfPracticeProp() { AreaOfPractice = EnumData.AreaOfPractice.All, IsSelected = false });

            return View(unpaid);
        }

        [HttpPost]
        public ActionResult UnpaidMember(UnpaidMember model)
        {
            var users1 = new List<ApplicationUser>();
            var users2 = new List<ApplicationUser>();
            var users3 = new List<ApplicationUser>();
            var users4 = new List<ApplicationUser>();
            foreach (var user in db.Users)
            {
                if (user.LastActive.Subtract(model.Date).Days > -1 && user.LastActive.Subtract(model.Date).Days < 30)
                {
                    users1.Add(user);
                    Debug.WriteLine(user.LastActive.Subtract(model.Date).Days + " | " + user.Email);
                }
            }

            if (users1.Count() == 0)
            {
                ModelState.AddModelError("NoActive", "No unpaid members found.");
                return View("UnpaidMember", model);
            }

            var selectedTypes = model.MembershipTypeItems.Where(p => p.IsSelected);
            if (selectedTypes.Count() == 0 || selectedTypes.Any(p => p.MembershipType == EnumData.MembershipType.All))
            {
                users2.AddRange(users1);
            }
            else
            {
                foreach (var user in users1)
                {
                    foreach (var type in selectedTypes)
                    {
                        if (user.MembershipInfo.TypeOfMembership == type.MembershipType)
                        {
                            users2.Add(user);
                            break;
                        }
                    }
                }
            }



            var selectedSectors = model.EmploymentSectorItems.Where(p => p.IsSelected);
            if (selectedSectors.Count() == 0 || selectedSectors.Any(p => p.EmploymentSector == EnumData.EmploymentSector.All))
            {
                users3.AddRange(users2);
            }
            else
            {
                foreach (var user in users2)
                {
                    foreach (var sector in selectedSectors)
                    {
                        if (user.Profession.EmploymentSector == sector.EmploymentSector)
                        {
                            users3.Add(user);
                            break;
                        }
                    }
                }
            }



            var selectedPractices = model.AreaOfPracticeItems.Where(p => p.IsSelected);
            if (selectedPractices.Count() == 0 || selectedPractices.Any(p => p.AreaOfPractice == EnumData.AreaOfPractice.All))
            {
                users4.AddRange(users3);
            }
            else
            {
                foreach (var user in users3)
                {
                    foreach (var practice in selectedPractices)
                    {
                        if (user.Profession.AreaOfPractice == practice.AreaOfPractice)
                        {
                            users4.Add(user);
                            break;
                        }
                    }
                }
            }

            if (users4.Count() == 0)
            {
                ModelState.AddModelError("NoActive", "No unpaid members found.");
                return View("UnpaidMember", model);
            }
            TempData["FilteredMembers"] = users4;

            return RedirectToAction("GenerateUnpaidMember");
        }

        public ActionResult GenerateUnpaidMember()
        {
            ViewData["FilteredMembers"] = (List<ApplicationUser>)TempData["FilteredMembers"];

            return View();
        }

        public ActionResult Seminar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Seminar(SeminarReport model)
        {
            IQueryable<Seminar> filter = from c in db.Seminars select c;

            if (!String.IsNullOrEmpty(model.Code))
            {
                filter = from c in db.Seminars where c.Code.ToLower().Contains(model.Code.ToLower()) select c;
            }
            else
            {
                filter = from c in db.Seminars select c;
            }

            if (!String.IsNullOrEmpty(model.Title))
            {
                filter = from c in db.Seminars where c.Title.ToLower().Contains(model.Title.ToLower()) select c;
            }

            if (model.DateFrom != null)
            {
                filter = from c in db.Seminars where (int) SqlFunctions.DateDiff("day", model.DateFrom, c.DateFrom) > -1 select c;
            }

            if (model.DateTo != null)
            {
                filter = from c in db.Seminars where (int) SqlFunctions.DateDiff("day", c.DateTo, model.DateTo) > -1 select c;
            }

            TempData["FilteredSeminars"] = filter;

            return RedirectToAction("GenerateSeminar");
        }

        public ActionResult GenerateSeminar()
        {
            var list = new List<Seminar>((IQueryable<Seminar>) TempData["FilteredSeminars"]);
            ViewData["FilteredSeminars"] = list;

            return View();
        }
    }
}