using EHealthConsult.Models;
using EHealthConsult.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EHealthConsult.Repository.RepositoryClasses
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _dbContext { get; set; }

        public RepositoryBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }


        //for delete, I changed the approach
        //i am not deleting any record, but simply set isdeleted to true via update
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Update(entity);
           // _dbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).AsNoTracking();

        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}


