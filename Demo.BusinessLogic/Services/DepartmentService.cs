using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.Factories;
using Demo.DataAccess.Models;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
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
            var departments = _departmentRepository.GetAll();

            return departments.Select(D => D.ToDepartmentDto());
        }


        // Get Department By Id
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);

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
            return _departmentRepository.Add(department);
        }

        // Update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var Department = _departmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                int Result = _departmentRepository.Remove(Department);
                //if (Result > 0) return true;
                //else return false;
                return Result > 0 ? true : false;
            }

        }

    }
}
