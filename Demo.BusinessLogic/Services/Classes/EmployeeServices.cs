using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeServices(IEmployeeRepository _employeeRepository) : IEmployeeServices
    {
        // Get All Employees
        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking = false)
        {
            var Employees = _employeeRepository.GetAll(WithTracking);
            var employeesDto = Employees.Select(Emp => new EmployeeDto
            {
                Id = Emp.Id,
                Name = Emp.Name,
                Age = Emp.Age,
                Email = Emp.Email,
                IsActive = Emp.IsActive,
                Salary = Emp.Salary,
                EmployeeType = Emp.EmployeeType.ToString(),
                Gender = Emp.Gender.ToString()
            });
            return employeesDto;
        }

        // Get Employee By Id
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is null ? null : new EmployeeDetailsDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Email = employee.Email,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                HiringDate = DateOnly.FromDateTime(employee.HiringDate),
                PhoneNumber = employee.PhoneNumber,
                EmployeeType = employee.EmployeeType.ToString(),
                Gender = employee.Gender.ToString(),
                CreatedBy = 1,
                CreatedOn = employee.CreatedOn,
                LastModifiedBy = 1,
                LastModifiedOn = employee.LastModifiedOn
            };
        }

        // Create New Employee
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        // Delete Employee
        public bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        // Update Employee
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }
    }
}
