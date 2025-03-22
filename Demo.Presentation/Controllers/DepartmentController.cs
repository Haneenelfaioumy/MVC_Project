using Demo.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService) : Controller
    {
        //private readonly IDepartmentService _departmentServices;
        //public DepartmentController(IDepartmentService departmentServices)
        //{
        //    this._departmentServices = departmentServices;
        //}

        // GET BaseUrl/Department/Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
    }
}
