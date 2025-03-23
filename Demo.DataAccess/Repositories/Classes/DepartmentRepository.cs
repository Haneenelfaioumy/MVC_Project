using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Models.DepartmentModel;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace Demo.DataAccess.Repositories
{
    // Primary Constructor .Net 8 C#12
    public class DepartmentRepository(ApplicationDbContext dbContext) : IDepartmentRepository
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
        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Departments.ToList();
            else
                return _dbContext.Departments.AsNoTracking().ToList();
        }

        // Get By Id
        public Department? GetById(int id) => _dbContext.Departments.Find(id);

        // Update
        public int Update(Department department)
        {
            _dbContext.Departments.Update(department); // Update Locally
            return _dbContext.SaveChanges();
        }

        // Delete
        public int Remove(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        // Insert | Create | Add
        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

    }
}
