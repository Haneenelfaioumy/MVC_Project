using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IDepartmentServices
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
    }
}