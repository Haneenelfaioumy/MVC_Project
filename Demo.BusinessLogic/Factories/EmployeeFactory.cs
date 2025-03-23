using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.DataAccess.Models.EmployeeModel;

namespace Demo.BusinessLogic.Factories
{
    static class EmployeeFactory
    {
        public static EmployeeDto ToEmployeeDto(this Employee E)
        {
            return new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Age = E.Age,
                Salary = E.Salary,
                Email = E.Email,
                IsActive = E.IsActive,
                Gender = E.Gender,
                EmployeeType = E.EmployeeType
            };
        }

        public static EmployeeDetailsDto ToEmployeeDetailsDto(this Employee employee)
        {
            return new EmployeeDetailsDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Email = employee.Email,
                IsActive = employee.IsActive,
                HiringDate = DateOnly.FromDateTime(employee.HiringDate) ,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType
            };
        }

        public static Employee ToEntity(this CreatedEmployeeDto employeeDto)
        {
            return new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                IsActive = employeeDto.IsActive,
                HiringDate = employeeDto.HiringDate.ToDateTime(new TimeOnly()),
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy = employeeDto.CreatedBy,
                LastModifiedBy = employeeDto.LastModifiedBy
            };
        }

        public static Employee ToEntity(this UpdatedEmployeeDto employeeDto) => new Employee()
        {
            Id = employeeDto.Id,
            Name = employeeDto.Name,
            Age = employeeDto.Age,
            Salary = employeeDto.Salary,
            Email = employeeDto.Email,
            IsActive = employeeDto.IsActive,
            HiringDate = employeeDto.HiringDate.ToDateTime(new TimeOnly()),
            Gender = employeeDto.Gender,
            EmployeeType = employeeDto.EmployeeType,
            CreatedBy = employeeDto.CreatedBy,
            LastModifiedBy = employeeDto.LastModifiedBy
        };
    }
}
