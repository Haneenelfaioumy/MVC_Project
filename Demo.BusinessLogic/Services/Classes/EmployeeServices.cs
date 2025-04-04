using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeServices(IEmployeeRepository _employeeRepository , IMapper _mapper) : IEmployeeServices
    {
        // Get All Employees
        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking = false)
        {
            #region IEnumerable VS IQueryable

            //var Result = _employeeRepository.GetIEnumerable()
            //                                    .Where(E => E.IsDeleted != true)
            //                                    .Select(E => new EmployeeDto()
            //                                    {
            //                                        Id = E.Id,
            //                                        Name = E.Name,
            //                                        Age = E.Age
            //                                    });
            //return Result.ToList(); 

            //var Result = _employeeRepository.GetIQueryable()
            //                                    .Where(E => E.IsDeleted != true)
            //                                    .Select(E => new EmployeeDto()
            //                                    {
            //                                        Id = E.Id,
            //                                        Name = E.Name,
            //                                        Age = E.Age
            //                                    });
            //return Result.ToList();

            #endregion

            //var Employees = _employeeRepository.GetAll(WithTracking);
            var Employees = _employeeRepository.GetAll(E => new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Salary = E.Salary,
                Age = E.Age
            }).Where(E => E.Age > 25);
            // Src = Employee
            // Dest = EmployeeDto
            //var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(Employees);
            //return employeesDto;
            return Employees;
        }

        // Get Employee By Id
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee ,  EmployeeDetailsDto>(employee);
        }

        // Create New Employee
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            return _employeeRepository.Add(employee);
        }

        // Update Employee
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            return _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDto , Employee>(employeeDto));
        }

        // Delete Employee
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) > 0 ? true : false;
            }
        }
    }
}
