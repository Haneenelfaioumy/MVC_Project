using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.DepartmentModel;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace Demo.DataAccess.Repositories.Classes
{
    // Primary Constructor .Net 8 C#12
    public class DepartmentRepository(ApplicationDbContext dbContext) 
        : GenericRepository<Department>(dbContext) , IDepartmentRepository
    {
        
    }
}
