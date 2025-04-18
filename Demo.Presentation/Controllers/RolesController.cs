using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels.EmployeeViewModel;
using Demo.Presentation.ViewModels.RoleViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class RolesController(RoleManager<IdentityRole> _roleManager,
                                 IWebHostEnvironment environment,
                                 ILogger<EmployeesController> logger) : Controller
    {
        #region All Role Managers [Index]
        public IActionResult Index(string RoleSearchName)
        {
            var roleQuery = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(RoleSearchName))
                roleQuery = roleQuery.Where(R => R.Name.ToLower().Contains(RoleSearchName.ToLower()));

            var roleList = roleQuery.Select(R => new RoleViewModel()
            {
                Id = R.Id,
                RoleName = R.Name
            }).ToList();

            return View(roleList);
        }

        #endregion

        #region Create Role Manager

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if(ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = roleViewModel.RoleName
                });
                return RedirectToAction(nameof(Index));
            }
            return View(roleViewModel);
        }

        #endregion

        #region Details Of Role Manager

        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null)
                return NotFound();
            var roleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            return View(roleViewModel);
        }

        #endregion

        #region Edit Role Manager

        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            var roleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            return View(roleViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] string id, RoleViewModel roleViewModel)
        {
            if (string.IsNullOrEmpty(id) || id is null) return BadRequest();
            if (!ModelState.IsValid) return View(roleViewModel);
            try
            {
                var role = _roleManager.FindByIdAsync(id).Result;
                if (role is null) return NotFound();
                role.Name = roleViewModel.RoleName;
                var Result = _roleManager.UpdateAsync(role).Result;
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Role Manager Can't Be Updated");
                    return View(roleViewModel);
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(roleViewModel);
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }

        #endregion

        #region Delete Role Manager

        [HttpGet]
        public IActionResult Delete(string? id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            return View(new RoleViewModel
            {
                Id = id,
                RoleName = role.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            if (id is null) return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            try
            {
                if (role is not null)
                {
                    await _roleManager.DeleteAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "role Is Not Deleted [An Error Happened]!!");
                    return RedirectToAction(nameof(Index));
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
