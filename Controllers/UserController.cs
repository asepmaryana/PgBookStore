using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PgBookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PgBookStore.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = new List<UserViewModel>();
            var userList = userManager.Users.ToList();
            foreach (var user in userList)
            {
                var item = new UserViewModel();
                item.UserName = user.UserName;
                item.Email = user.Email;
                item.FullName = user.FullName;

                string roleNameList = string.Empty;
                var roleList = await userManager.GetRolesAsync(user);
                foreach (var role in roleList)
                {
                    roleNameList = roleNameList + role + ", ";                    
                }
                item.RoleName = roleNameList.Substring(0, roleNameList.Length - 2);

                items.Add(item);
            }

            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new MultiSelectList(roleManager.Roles.ToList(), "Name","NormalizedName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateFormViewModel item)
        {
            ViewBag.Roles = new MultiSelectList(roleManager.Roles.ToList(), "Name", "NormalizedName"); 

            if(ModelState.IsValid){
                ApplicationUser user = new ApplicationUser();
                user.FullName = item.FullName;
                user.UserName = item.UserName;
                user.Email = item.Email;
                IdentityResult result = await userManager.CreateAsync(user, item.Password);
                
                if (result.Succeeded) {
                    result = await userManager.AddToRolesAsync(user, item.RoleID);
                    if(result.Succeeded){
                        return RedirectToAction("Index");
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Roles = new MultiSelectList(roleManager.Roles.ToList(), "Name", "NormalizedName");
            var user = await userManager.FindByNameAsync(id);

            UserEditFormViewModel item = new UserEditFormViewModel();
            item.UserName = user.UserName;
            item.Email = user.Email;
            item.FullName = user.FullName;

            var roles = await userManager.GetRolesAsync(user);
            item.RoleID = roles.ToArray();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("UserName,RoleID,Email,Password,PasswordConfirm,FullName")] UserEditFormViewModel item)
        {
            ViewBag.Roles = new MultiSelectList(roleManager.Roles.ToList(), "Name", "NormalizedName");
            if (ModelState.IsValid) {
                ApplicationUser user = await userManager.FindByNameAsync(item.UserName);
                user.Email = item.Email;
                user.FullName = item.FullName;
                
                var existingRoles = await userManager.GetRolesAsync(user);
                IdentityResult result = await userManager.UpdateAsync(user);

                if (result.Succeeded) {
                    result = await userManager.RemoveFromRolesAsync(user, existingRoles);
                    if (result.Succeeded) {
                        result = await userManager.AddToRolesAsync(user, item.RoleID);
                        if (result.Succeeded) return RedirectToAction("Index");
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid) {
                var user = await userManager.FindByNameAsync(id);
                var result = await userManager.DeleteAsync(user);
                
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}