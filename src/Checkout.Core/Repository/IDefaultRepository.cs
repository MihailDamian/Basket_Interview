using Checkout.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Repository
{
    public interface IDefaultRepository<T> where T:Entity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Create(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}
