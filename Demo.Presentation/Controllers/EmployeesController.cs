using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.Presentation.ViewModels.EmployeeViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeServices _employeeServices ,
                                     IWebHostEnvironment environment ,
                                     ILogger<EmployeesController> logger) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employees = _employeeServices.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }

        #region Create Employee

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeViewModel.Name,
                        Address = employeeViewModel.Address,
                        Age = employeeViewModel.Age,
                        Email = employeeViewModel.Email,
                        EmployeeType = employeeViewModel.EmployeeType,
                        Gender = employeeViewModel.Gender,
                        HiringDate = employeeViewModel.HiringDate,
                        IsActive = employeeViewModel.IsActive,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        Salary = employeeViewModel.Salary,
                        DepartmentId = employeeViewModel.DepartmentId,
                        Image = employeeViewModel.Image
                    };
                    int Result = _employeeServices.CreateEmployee(employeeDto);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't Be Created");
                    }
                }
                catch (Exception ex)
                {
                    if (environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        logger.LogError(ex.Message);
                    }
                }
            }
            return View(employeeViewModel);
        }

        #endregion

        #region Details Of Employee

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest(); 
            var employee = _employeeServices.GetEmployeeById(id.Value);
            return employee is null ? NotFound() : View(employee);
        }

        #endregion

        #region Edit Employee

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeServices.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            var employeeViewModel = new EmployeeViewModel()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = employee.HiringDate,
                IsActive = employee.IsActive,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId,
                ExistingImage = employee.Image
            };
            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
        {
            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid) return View(employeeViewModel);
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeViewModel.Name,
                    Salary = employeeViewModel.Salary,
                    Address = employeeViewModel.Address,
                    Age = employeeViewModel.Age,
                    Email = employeeViewModel.Email,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    HiringDate = employeeViewModel.HiringDate,
                    IsActive = employeeViewModel.IsActive,
                    Gender = employeeViewModel.Gender,
                    EmployeeType = employeeViewModel.EmployeeType,
                    DepartmentId = employeeViewModel.DepartmentId,
                    Image = employeeViewModel.Image,
                    ExistingImage = employeeViewModel.ExistingImage
                };
                var Result = _employeeServices.UpdateEmployee(employeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Updated");
                    return View(employeeDto);
                }
            }
            catch (Exception ex) 
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeViewModel);
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView" , ex);
                }
            }
        }

        #endregion

        #region Delete Employee

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _employeeServices.DeleteEmployee(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Is Not Deleted");
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
