using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeServices _employeeServices) : Controller
    {
        public IActionResult Index()
        {
            var employees = _employeeServices.GetAllEmployees();

            return View(employees);
        }
    }
}
