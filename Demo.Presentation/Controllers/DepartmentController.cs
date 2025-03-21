using Demo.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService departmentServices) : Controller
    {
        //private readonly IDepartmentService _departmentServices;
        //public DepartmentController(IDepartmentService departmentServices)
        //{
        //    this._departmentServices = departmentServices;
        //}

        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();

            return View();
        }
    }
}
