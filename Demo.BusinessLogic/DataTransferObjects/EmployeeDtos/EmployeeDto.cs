using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;

namespace Demo.BusinessLogic.DataTransferObjects.EmployeeDtos
{
    public class EmployeeDto
    {
        public int Id { get; set; } // PK
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
