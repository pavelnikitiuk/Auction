using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction.Models
{
    public class ManageUserRoleModel
    {
        public IEnumerable<UserRolesModel> Roles { get; set; }
        public IEnumerable<UsersModel> Users { get; set; }
    }

    public class UserRolesModel
    {
        public string Role { get; set; }
        public string RoleId { get; set; }
    }

    public class UsersModel
    {
        public string Username { get; set; }
        public string UserId { get; set; }
    }
}