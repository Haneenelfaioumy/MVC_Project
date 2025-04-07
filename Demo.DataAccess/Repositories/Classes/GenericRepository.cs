using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.DbContexts;
using Demo.DataAccess.Models.DepartmentModel;
using Demo.DataAccess.Models.Shared;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        // CRUD Operations

        // Get All
        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                //return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).ToList();
                return _dbContext.Set<TEntity>().ToList();
            else
                //return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        // Get By Id
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);

        // Update
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity); // Update Locally
        }

        // Delete
        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        // Insert | Create | Add
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _dbContext.Set<TEntity>()
                             .Where(E => E.IsDeleted != true)
                             .Select(selector)
                             .ToList();
        }

        IEnumerable<TEntity> IGenericRepository<TEntity>.GetAll(Expression<Func<TEntity, bool>> Predicate)
        {
            return _dbContext.Set<TEntity>()
                             .Where(Predicate)
                             .ToList();
        }
    }
}
