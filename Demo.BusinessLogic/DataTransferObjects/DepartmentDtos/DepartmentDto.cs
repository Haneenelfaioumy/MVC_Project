﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Models;

namespace Demo.BusinessLogic.DataTransferObjects.DepartmentDtos
{
    public class DepartmentDto
    {
        public int DeptId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DateOfCreation { get; set; }
    }
}
