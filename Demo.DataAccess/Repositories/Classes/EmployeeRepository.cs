using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.DbContexts;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext dbContext) 
        : GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
        
    }
}
