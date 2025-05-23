﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.AttachementService;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeServices(IUnitOfWork _unitOfWork , IMapper _mapper , 
                                  IAttachmentService _attachmentService) : IEmployeeServices
    {
        // Get All Employees
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            #region IEnumerable VS IQueryable

            //var Employees = _employeeRepository.GetAll(E => new EmployeeDto()
            //{
            //    Id = E.Id,
            //    Name = E.Name,
            //    Salary = E.Salary,
            //    Age = E.Age
            //}).Where(E => E.Age > 25);
            //return Employees;

            #endregion

            //var employees = _employeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            //// Src = Employee
            //// Dest = EmployeeDto
            //var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            //return employeesDto;
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return employeesDto;
        }

        // Get Employee By Id
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee ,  EmployeeDetailsDto>(employee);
        }

        // Create New Employee
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            if(employeeDto is not null)
            {
                employee.ImageName = _attachmentService.Upload(employeeDto.Image, "Images");
            }
            _unitOfWork.EmployeeRepository.Add(employee); // Add Locally
            return _unitOfWork.SaveChanges();
        }

        // Update Employee
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            #region Another Solution
            //if (employeeDto.Image != null)
            //{
            //    var filePath = Path.Combine(
            //        Directory.GetCurrentDirectory(),
            //        "wwwroot\\Files\\Images",
            //        employeeDto.ExistingImage
            //    );
            //    var isDeleted = _attachmentService.Delete(filePath);
            //}
            //var employee = _mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto);
            //employee.ImageName = _attachmentService.Upload(employeeDto.Image, "Images");
            //_unitOfWork.EmployeeRepository.Update(employee);

            //return _unitOfWork.SaveChanges(); 
            #endregion

            // Get the existing employee from DB
            var existingEmployee = _unitOfWork.EmployeeRepository.GetById(employeeDto.Id);
            if (existingEmployee == null)
                throw new ArgumentException("Employee not found.");

            // Handle image replacement
            if (employeeDto.Image != null && !string.IsNullOrEmpty(employeeDto.ExistingImage))
            {
                var existingFilePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot\\Files\\Images",
                    employeeDto.ExistingImage
                );

                _attachmentService.Delete(existingFilePath);
            }

            // Upload new image (if any)
            if (employeeDto.Image != null)
            {
                var uploadedFileName = _attachmentService.Upload(employeeDto.Image, "Images");
                existingEmployee.ImageName = uploadedFileName;
            }

            // Map updated fields (except image name, already handled)
            _mapper.Map(employeeDto, existingEmployee);

            _unitOfWork.EmployeeRepository.Update(existingEmployee);
            return _unitOfWork.SaveChanges();

            //_unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
            //return _unitOfWork.SaveChanges();
        }

        // Delete Employee
        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(employee);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }
    }
}
