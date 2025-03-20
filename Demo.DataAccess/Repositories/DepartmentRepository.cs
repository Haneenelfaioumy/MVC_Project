using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;

namespace Demo.DataAccess.Repositories
{
    // Primary Constructor .Net 8 C#12
    class DepartmentRepository(ApplicationDbContext dbContext)
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        #region Without Using Primary Constructor

        //public DepartmentRepository(ApplicationDbContext dbContext) // 1. Injection
        //                                                            // Ask CLR For Creating Object From ApplicationDbContext
        //{
        //    this._dbContext = dbContext;
        //} 

        #endregion


        // CRUD Operations
        // Get All
        // Get By Id
        public Department? GetById(int id)
        {
            var department = _dbContext.Departments.Find(id);
            return department;
        }
        // Update
        // Delete
        // Insert

    }
}
