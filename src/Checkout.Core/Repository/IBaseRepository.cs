using Checkout.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Repository
{
    public interface IBaseRepository<T> : IDisposable where T : Entity
    {
        IEnumerable<T> GetAllBase();
        IEnumerable<T> GetByConditionBase(Expression<Func<T, bool>> expression);
        T GetBase(int id);
        void CreateBase(T entity);
        void UpdateBase(T entity);
        void DeleteBase(T entity);
        void SaveBase();
    }
}
