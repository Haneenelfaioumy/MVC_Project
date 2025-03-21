using Demo.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService departmentServices) : Controller
    {
        
        public IActionResult Index()
        {
            var Departments = departmentServices.GetAllDepartments();

            return View();
        }
    }
}
