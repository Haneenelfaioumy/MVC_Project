using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.DbContexts;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
        }
        IEmployeeRepository IUnitOfWork.EmployeeRepository => _employeeRepository.Value;

        IDepartmentRepository IUnitOfWork.DepartmentRepository => _departmentRepository.Value;

        int IUnitOfWork.SaveChanges() => _dbContext.SaveChanges();

    }
}
