﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCproject_Elearning.Models;
using MVCproject_Elearning.ViewModels.Users;

namespace MVCproject_Elearning.Areas.Admin.Controllers
{
    [Area("admin")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class UserController : Controller
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public UserController(UserManager<AppUser> userManager,
                                  RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
                _userManager = userManager;
            }
            [HttpGet]
            public async Task<IActionResult> Index()
            {
                var users = _userManager.Users.ToList();

                List<UserRoleVM> userWithRoles = new();
                foreach (var item in users)
                {
                    var roles = await _userManager.GetRolesAsync(item);
                    var userRoles = String.Join(",", roles.ToArray());
                    userWithRoles.Add(new UserRoleVM
                    {
                        FullName = item.FullName,
                        Email = item.Email,
                        Username = item.UserName,
                        Roles = userRoles
                    });
                }
                return View(userWithRoles);
            }
            [HttpGet]
            [Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> AddRole()
            {
                var users = _userManager.Users.ToList();
                var roles = _roleManager.Roles.ToList();
                ViewBag.users = new SelectList(users, "Id", "UserName");
                ViewBag.roles = new SelectList(roles, "Id", "Name");
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            [Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> AddRole(UserAddRoleVM request)
            {
                var users = _userManager.Users.ToList();
                var roles = _roleManager.Roles.ToList();
                ViewBag.users = new SelectList(users, "Id", "UserName");
                ViewBag.roles = new SelectList(roles, "Id", "Name");
                var user = await _userManager.FindByIdAsync(request.UsernameId);
                var role = await _roleManager.FindByIdAsync(request.RoleId);
                var existRole = await _userManager.IsInRoleAsync(user, role.Name);
                if (existRole)
                {
                    ModelState.AddModelError(string.Empty, "This role is exist at User");
                    return View();
                }
                await _userManager.AddToRoleAsync(user, role.Name);
                return RedirectToAction(nameof(Index));
            }
        }
    
}
