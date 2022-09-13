using Checkout.Core.Models;
using Checkout.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Infrastructure.Database
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        public ShopContext context { get; set; }
        public BaseRepository(ShopContext c)
        {
            context = c;
        }
        public IEnumerable<T> GetAllBase()
        {
            return context.Set<T>();
        }
        public IEnumerable<T> GetByConditionBase(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression).AsNoTracking();
        }

        public T GetBase(int id)
        {
            return context.Set<T>().AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        public void CreateBase(T entity)
        {
            context.Set<T>().Add(entity);
        }
        public void UpdateBase(T entity)
        {
            context.Set<T>().Update(entity);
        }
        public void DeleteBase(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void SaveBase()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
