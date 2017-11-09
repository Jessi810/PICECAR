using PICECAR.Extension;
using PICECAR.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PICECAR.Controllers
{
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
            active.MembershipTypeItems.Add(new MembershipTypeProp() { MembershipType = EnumData.MembershipType.All, IsSelected = true });

            active.EmploymentSectorItems = new List<EmploymentSectorProp>();
            active.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.Government, IsSelected = false });
            active.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.Private, IsSelected = false });
            active.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.OCW, IsSelected = false });
            active.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.Others, IsSelected = false });
            active.EmploymentSectorItems.Add(new EmploymentSectorProp() { EmploymentSector = EnumData.EmploymentSector.All, IsSelected = true });

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
            var userId1 = new List<string>();
            var userId2 = new List<string>();
            var userId3 = new List<string>();
            var userId4 = new List<string>();
            foreach (var user in db.Users)
            {
                if (user.LastActive.Subtract(model.Date).Days > -1 && user.LastActive.Subtract(model.Date).Days < 30)
                {
                    userId1.Add(user.Id);
                    users1.Add(user);
                    Debug.WriteLine(user.LastActive.Subtract(model.Date).Days + " | " + user.Email);
                }
            }
            TempData["ActiveFilter"] = users1;

            if (users1.Count() == 0)
            {
                ModelState.AddModelError("", "No active members found.");
                return RedirectToAction("GenerateActiveMember", model);
            }

            var selectedTypes = model.MembershipTypeItems.Where(p => p.IsSelected);
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
            TempData["TypeMatch"] = users2;



            var selectedSectors = model.EmploymentSectorItems.Where(p => p.IsSelected);
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
            TempData["SectorMatch"] = users3;



            var selectedPractices = model.AreaOfPracticeItems.Where(p => p.IsSelected);
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
            TempData["PracticeMatch"] = users4;

            return RedirectToAction("GenerateActiveMember");
        }

        public ActionResult GenerateActiveMember()
        {
            ViewData["ActiveFilter"] = (List<ApplicationUser>) TempData["ActiveFilter"];
            ViewData["TypeMatch"] = (List<ApplicationUser>) TempData["TypeMatch"];
            ViewData["SectorMatch"] = (List<ApplicationUser>)TempData["SectorMatch"];
            ViewData["PracticeMatch"] = (List<ApplicationUser>)TempData["PracticeMatch"];

            return View();
        }
    }
}