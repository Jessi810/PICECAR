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
    }
}