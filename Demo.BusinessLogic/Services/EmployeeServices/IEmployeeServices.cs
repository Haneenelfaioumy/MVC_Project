using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;

namespace Demo.BusinessLogic.Services.EmployeeServices
{
    public interface IEmployeeServices
    {
        int AddEmployee(CreatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDetailsDto? GetEmployeeById(int id);
        int UpdateDepartment(UpdatedEmployeeDto employeeDto);
    }
}