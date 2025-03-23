using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Factories;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services.EmployeeServices
{
    public class EmployeeServices(IEmployeeRepository _employeeRepository) : IEmployeeServices
    {
        // Get All Employees
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = _employeeRepository.GetAll();

            return employees.Select(E => E.ToEmployeeDto());
        }

        // Get Employee By Id
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);

            return employee is null ? null : employee.ToEmployeeDetailsDto();
        }

        // Create New Employee
        public int AddEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = employeeDto.ToEntity();
            return _employeeRepository.Add(employee);
        }

        // Update Department
        public int UpdateDepartment(UpdatedEmployeeDto employeeDto)
        {
            return _employeeRepository.Update(employeeDto.ToEntity());
        }

        // Delete Employee
        public bool DeleteEmployee(int id)
        {
            var Emp = _employeeRepository.GetById(id);
            if (Emp is null) return false;
            else
            {
                int Result = _employeeRepository.Remove(Emp);

                return Result > 0 ? true : false;
            }
        }
    }
}
