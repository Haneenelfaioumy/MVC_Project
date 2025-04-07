using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentServices(IUnitOfWork _unitOfWork) : IDepartmentServices
    {

        #region Department Module [ Repository ]

        //private readonly IDepartmentRepository departmentRepository;

        //public DepartmentService(IDepartmentRepository departmentRepository) // 1. Injection
        //{
        //    this.departmentRepository = departmentRepository;
        //} 

        #endregion

        // Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            return departments.Select(D => D.ToDepartmentDto());
        }


        // Get Department By Id
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);

            // Manual Mapping
            #region Manual Mapping

            //return department is null ? null : new DepartmentDetailsDto
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    CreatedOn = DateOnly.FromDateTime(department.CreatedOn)
            //};

            #endregion

            // Auto Mapper

            // Constructor Mapping
            #region Constructor - Based Mapping

            //return department is null ? null : new DepartmentDetailsDto(department);

            #endregion

            // Extension Method
            #region Extension Method

            return department is null ? null : department.ToDepartmentDetailsDto();

            #endregion
        }

        // Create New Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChanges();
        }

        // Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var Department = _unitOfWork.DepartmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Remove(Department);
                int Result = _unitOfWork.SaveChanges();
                //if (Result > 0) return true;
                //else return false;
                return Result > 0 ? true : false;
            }

        }

    }
}
