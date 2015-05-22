using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using Auction.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace Auction.Controllers
{
    public class AdminController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();
        
        [HttpGet]
        public ActionResult ManageUserRoles()
        {
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            return View(list);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RoleAddToUser(string userName, string roleName)
        {
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            var account = new AccountController();
            if (user == null)
            {
                return Json("Unknown user");
            }
            account.UserManager.AddToRole(user.Id, roleName);
            return Json(String.Format("The user {0} has been successfully added to the right {1}",user.UserName,roleName));
        }

        [HttpGet]
        public JsonResult GetRoles(string userName)
        {
            RolesModel model = new RolesModel();
            if (string.IsNullOrWhiteSpace(userName))
                return Json("Type something", JsonRequestBehavior.AllowGet);

            ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            var account = new AccountController();
            if (user == null)
               return Json("Unknown user", JsonRequestBehavior.AllowGet);

            // prepopulat roles for the view dropdown
            var list = account.UserManager.GetRoles(user.Id);
            return Json(list, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteRoleForUser(string userName, string roleName)
        {
            var account = new AccountController();
            ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            if (user == null)
                return Json("Unknown User");
            if (account.UserManager.IsInRole(user.Id, roleName))
                account.UserManager.RemoveFromRole(user.Id, roleName);
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            return Json(String.Format("The user {0} has the right successfully removed {1}",userName,roleName));
        }
    }
}