﻿using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeServices
    {
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
    }
}