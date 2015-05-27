using System;
using System.Linq;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Models;
using Microsoft.AspNet.Identity;

namespace Auction.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly ICategoriesRepository categoriesRepository;
        public AdminController(ICategoriesRepository categoriesRepository, ApplicationDbContext context)
        {
            this.context = context;
            this.categoriesRepository = categoriesRepository;
        }
        
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult ManageUserRoles()
        {
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            return View(list);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public JsonResult RoleAddToUser(string userName, string roleName)
        {
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            var account = new AccountController();
            if (user == null)
                return Json("Unknown user");
            account.UserManager.AddToRole(user.Id, roleName);
            if (roleName == "seller")
            {
                user.IsSeller = true;
                context.SaveChanges();
            }
            return Json(String.Format("The user {0} has been successfully added to the right {1}",user.UserName,roleName));
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public JsonResult GetRoles(string userName)
        {
            RolesModel model = new RolesModel();
            if (string.IsNullOrWhiteSpace(userName))
                return Json("Type something", JsonRequestBehavior.AllowGet);

            ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            var account = new AccountController();
            if (user == null)
               return Json("Unknown user", JsonRequestBehavior.AllowGet);

            var list = account.UserManager.GetRoles(user.Id);
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public JsonResult DeleteRoleForUser(string userName, string roleName)
        {
            var account = new AccountController();
            ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            if (user == null)
                return Json("Unknown User");
            if (account.UserManager.IsInRole(user.Id, roleName))
            {
                account.UserManager.RemoveFromRole(user.Id, roleName);
                if (roleName == "seller")
                {
                    user.IsSeller = null;
                    context.SaveChanges();
                }
            }
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            return Json(String.Format("The user {0} has the right successfully removed {1}",userName,roleName));
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "moderator")]
        public ViewResult ModeraotorControl()
        {
            var list = categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x);
            var users = context.Users.Where(x => x.IsSeller == false).Select(x=>x.UserName);
            return View(new ModeratorModel{
               Categories = list,
               Users = users
        });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "moderator")]
        public PartialViewResult AddCategory(string newCategory)
        {
            if(newCategory!=null)
                categoriesRepository.Add(newCategory);
            return PartialView("CategoriesPartial", categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "moderator")]
        public PartialViewResult RemoveCategory(string removeCategory)
        {
            var cat = categoriesRepository.Categories.FirstOrDefault(x => x.CategoryName == removeCategory);
            if (cat != null)
                categoriesRepository.Remove(cat);
            return PartialView("CategoriesPartial", categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "moderator")]
        public ActionResult AddSeller(string userName)
        {
            ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            var account = new AccountController();
            if (user != null)
            {
                user.IsSeller = true;
                context.SaveChanges();
                account.UserManager.AddToRole(user.Id, "seller");
            }
            return RedirectToAction("ModeraotorControl");
        }
    }
}