using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;
using Demo.DataAccess.Models.DepartmentModel;

namespace Demo.BusinessLogic.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department D)
        {
            return new DepartmentDto()
            {
                DeptId = D.Id ,
                Code = D.Code ,
                Description = D.Description ,
                Name = D.Name ,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)
            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name ,
                Code = department.Code,
                Description = department.Description,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn),
                CreatedBy = department.CreatedBy,
                LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn),
                LastModifiedBy = department.LastModifiedBy
            };
        }

        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }

        public static Department ToEntity(this UpdatedDepartmentDto departmentDto) => new Department()
        {
            Id = departmentDto.Id,
            Name = departmentDto.Name,
            Code = departmentDto.Code,
            Description = departmentDto.Description,
            CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
        };

    }
}
