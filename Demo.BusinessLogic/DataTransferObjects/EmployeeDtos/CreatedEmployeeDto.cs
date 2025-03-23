using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;

namespace Demo.BusinessLogic.DataTransferObjects.EmployeeDtos
{
    public class CreatedEmployeeDto
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Address { get; set; } = null!;
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateOnly HiringDate { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int CreatedBy { get; set; } 
        public int LastModifiedBy { get; set; }
    }
}
