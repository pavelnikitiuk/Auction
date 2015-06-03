using System;
using System.Linq;
using System.Web.Mvc;
using Auction.Domain.Abstract;
using Auction.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
        /// <summary>
        /// ManageUserRoles action
        /// </summary>
        /// <returns>ManageUserRole view</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult ManageUserRoles()
        {
            var roles =
                context.Roles.OrderBy(r => r.Name)
                    .Select(rr => new UserRolesModel() {RoleId = rr.Id, Role = rr.Name})
                    .ToList();
            var users =
                context.Users.OrderBy(r => r.UserName)
                    .Select(rr => new UsersModel {UserId = rr.Id, Username = rr.UserName});

            var model = new ManageUserRoleModel {Roles = roles, Users = users};
            
    
           // var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Id, Text = rr.Name }).ToList();
            return View(model);
        }

        /// <summary>
        /// Add role to user
        /// </summary>
        /// <param name="userId">Username</param>
        /// <param name="roleId">Role to add</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public JsonResult RoleAddToUser(string userId, string roleId)
        {
            if (!ModelState.IsValid)
                return Json("Illegal operation");
            ApplicationUser user = context.Users.Find(userId);
            IdentityRole role = context.Roles.Find(roleId);
            if (user==null || role == null)
                return Json("Unknown user or role");
            var account = new AccountController();
            account.UserManager.AddToRole(user.Id, role.Name);
            if (role.Name == "seller")
            {
                user.IsSeller = true;
                context.SaveChanges();
            }
            return Json(String.Format("The user {0} has been successfully added to the right {1}",user.UserName,role.Name));
        }
        /// <summary>
        /// Get user rolers
        /// </summary>
        /// <param name="userId">Username</param>
        /// <returns>User roles</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public JsonResult GetRoles(string userId)
        {
            if (!ModelState.IsValid)
                return Json("Illegal operation");
            if (string.IsNullOrWhiteSpace(userId))
                return Json("Type something", JsonRequestBehavior.AllowGet);
            ApplicationUser user = context.Users.Find(userId);
            if (user == null)
               return Json("Unknown user", JsonRequestBehavior.AllowGet);
            var account = new AccountController();
            var list = account.UserManager.GetRoles(user.Id);
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// Delete user role
        /// </summary>
        /// <param name="userId">Username</param>
        /// <param name="roleId">Role to delete</param>
        /// <returns>Json result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public JsonResult DeleteRoleForUser(string userId, string roleId)
        {
            if (!ModelState.IsValid)
                return Json("Illegal operation");
            var account = new AccountController();
            ApplicationUser user = context.Users.Find(userId);
            IdentityRole role = context.Roles.Find(roleId);
            if (user == null)
                return Json("Unknown User");
            if (account.UserManager.IsInRole(user.Id, role.Name))
            {
                account.UserManager.RemoveFromRole(user.Id, role.Name);
                if (role.Name == "seller")
                {
                    user.IsSeller = null;
                    context.SaveChanges();
                }
            }

            return Json(String.Format("The user {0} has the right successfully removed {1}",user.UserName,role.Name));
        }

        /// <summary>
        /// Moderator control action
        /// </summary>
        /// <returns>Moderator control page</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "moderator")]
        public ViewResult ModeraotorControl()
        {
            var list = categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x);
            return View(new ModeratorModel
            {
                Categories = list,
                Users = context.Users.Where(x => x.IsSeller == false).Select(x=>new UsersModel{UserId = x.Id, Username = x.UserName})
            });
        }
        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="newCategory">Name of new category</param>
        /// <returns>Partial view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "moderator")]
        public PartialViewResult AddCategory(string newCategory)
        {
            if (!ModelState.IsValid)
                return PartialView("CategoriesPartial", categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x));
            if(newCategory!=null)
                categoriesRepository.Add(newCategory);
            return PartialView("CategoriesPartial", categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x));
        }
        /// <summary>
        /// Remove category
        /// </summary>
        /// <param name="removeCategory">Name of remove category</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "moderator")]
        public PartialViewResult RemoveCategory(string removeCategory)
        {
            if (!ModelState.IsValid)
                return PartialView("CategoriesPartial", categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x));
            var cat = categoriesRepository.Categories.FirstOrDefault(x => x.CategoryName == removeCategory);
            if (cat != null)
                categoriesRepository.Remove(cat);
            return PartialView("CategoriesPartial", categoriesRepository.Categories.Select(x => x.CategoryName).OrderBy(x => x));
        }
        /// <summary>
        /// Add role seller to user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>ModeraorControl page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "moderator")]
        public ActionResult AddSeller(string userId)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("ModeraotorControl");
            ApplicationUser user = context.Users.Find(userId);
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