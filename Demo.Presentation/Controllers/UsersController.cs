using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.DataAccess.Models.EmployeeModel;
using System;
using Demo.DataAccess.Models.IdentityModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.Presentation.ViewModels.EmployeeViewModel;
using Demo.Presentation.ViewModels.UsersViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Presentation.Controllers
{
    public class UsersController(UserManager<ApplicationUser> _userManager,
                                 IWebHostEnvironment environment,
                                 ILogger<EmployeesController> logger) : Controller
    {
        // GetAll , GetById , Update , Delete
        // Index , Details , Edit , Delete
        #region All Users [Index]
        public IActionResult Index(string UserSearchName)
        {
            var userQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(UserSearchName))
                userQuery = userQuery.Where(U => U.FirstName.ToLower().Contains(UserSearchName.ToLower()));

            var usersList = userQuery.Select(U => new UserViewModel
            {
                Id = U.Id,
                FName = U.FirstName,
                LName = U.LastName,
                Email = U.Email,
                PhoneNumber = U.PhoneNumber,

            }).ToList();
            foreach (var user in usersList)
            {
                var Roles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(user.Id).Result).Result;
            }
            return View(usersList);
        }

        #endregion

        #region Details Of User

        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if(user is null) 
                return NotFound();
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                FName = user.FirstName,
                LName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userViewModel);
        }

        #endregion

        #region Edit User

        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                FName = user.FirstName,
                LName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] string id, UserViewModel userViewModel)
        {
            if (string.IsNullOrEmpty(id) || id is null) return BadRequest();
            if (!ModelState.IsValid) return View(userViewModel);
            try
            {
                var user = _userManager.FindByIdAsync(id).Result;
                if (user is null) return NotFound();
                user.FirstName = userViewModel.FName;
                user.LastName = userViewModel.LName;
                user.PhoneNumber = userViewModel.PhoneNumber;
                var Result = _userManager.UpdateAsync(user).Result;
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "User Is Not Updated");
                    return View(userViewModel);
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(userViewModel);
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }

        #endregion

        #region Delete User

        [HttpGet]
        public IActionResult Delete(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            return View(new UserViewModel
            {
                Id = id,
                FName = user.FirstName,
                LName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }

        [HttpPost]
        public IActionResult ConfirmDelete(string id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            try
            {
                if (user is not null)
                {
                    _userManager.DeleteAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Is Not Deleted [An Error Happened]!!");
                    return RedirectToAction(nameof(Delete), new { id });
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }

        #endregion
    }
}
